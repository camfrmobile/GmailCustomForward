using MailKit.Net.Imap;
using MailKit;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MailKit.Search;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using MimeKit;
using MailKit.Net.Smtp;

namespace GmailCustomForward
{
    public partial class frmMain : Form
    {
        static double version = 1.1;
        static string appname = "Gmail Custom Forward";
        static bool isActive = false;
        static bool isRunning = false;
        static DataTable eTable = new DataTable();

        static string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward");

        static string htmlFolder = Path.Combine(Path.GetTempPath(), "GmailCustomForward");

        static string settingSave = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting.txt");
        
        static List<string> skipList = new List<string>();
        static string skipListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting_skip.txt");
        
        static List<string> hiddenList = new List<string>();
        static string hiddenListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting_hidden.txt");
        
        static List<string> afterList = new List<string>();
        static string afterListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting_after.txt");
        
        static List<string> specialList = new List<string>();
        static string specialListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting_special.txt");

        static List<string> excludeList = new List<string>();
        static string excludeListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting_exclude.txt");

        static List<string> histories = new List<string>();
        static string historyFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "histories.txt");

        static int timerInterval = 5 * 60 * 1000;
        static int timerIndex = 0;
        static int number = 0;

        public frmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = $"{appname} {version.ToString("0.0")}";

            dataGridEmail.Dock = DockStyle.Fill;
            dataGridEmail.Parent = splitContainer.Panel1;

            timerIndex = timerInterval / 1000;
            timerUpdate.Enabled = true;

            timerForward.Interval = timerInterval;
            timerForward.Enabled = true;

            eTable = new DataTable();
            eTable.Columns.Add("_eUid", typeof(string));
            eTable.Columns.Add("_eMid", typeof(string));
            eTable.Columns.Add("_eSubject", typeof(string));
            eTable.Columns.Add("_eFrom", typeof(string));
            eTable.Columns.Add("_eLabels", typeof(string));
            eTable.Columns.Add("_eDate", typeof(string));

            dataGridEmail.DataSource = eTable;
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (!Directory.Exists(appFolder)) Directory.CreateDirectory(appFolder);

            LoadSetting();

            timerStart.Enabled = true;

            string listFile = Path.Combine(Application.StartupPath, Path.GetFileName(hiddenListFile));
            if (File.Exists(listFile))
            {
                if (File.Exists(hiddenListFile)) File.Delete(hiddenListFile);
                File.Move(listFile, hiddenListFile);
            }

            listFile = Path.Combine(Application.StartupPath, Path.GetFileName(afterListFile));
            if (File.Exists(listFile))
            {
                if (File.Exists(afterListFile)) File.Delete(afterListFile);
                File.Move(listFile, afterListFile);
            }

            listFile = Path.Combine(Application.StartupPath, Path.GetFileName(specialListFile));
            if (File.Exists(listFile))
            {
                if (File.Exists(specialListFile)) File.Delete(specialListFile);
                File.Move(listFile, specialListFile);
            }

            listFile = Path.Combine(Application.StartupPath, Path.GetFileName(excludeListFile));
            if (File.Exists(listFile))
            {
                if (File.Exists(excludeListFile)) File.Delete(excludeListFile);
                File.Move(listFile, excludeListFile);
            }

            listFile = Path.Combine(Application.StartupPath, Path.GetFileName(skipListFile));
            if (File.Exists(listFile))
            {
                if (File.Exists(skipListFile)) File.Delete(skipListFile);
                File.Move(listFile, skipListFile);
            }

            SetupStartup();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!checkExitOnClose.Checked)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            int reWidth = this.Width - 25;
            groupBox.Width = reWidth;
            progressBar.Width = reWidth;
            splitContainer.Width = reWidth;
            splitContainer.Height = this.Height - groupBox.Height - progressBar.Height - 50;
            checkExitOnClose.Location = new System.Drawing.Point(groupBox.Width - checkExitOnClose.Width - 5, groupBox.Location.X + 5);
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void buttonGmail_Click(object sender, EventArgs e)
        {
            if (isRunning) return;
            isRunning = true;
            string gmail = textGmail.Text.Trim();
            string pass = textPass.Text.Trim();
            string sendAddress = textSend.Text.Trim();

            if (Directory.Exists(htmlFolder)) Directory.Delete(htmlFolder, true);

            if (string.IsNullOrEmpty(gmail) || string.IsNullOrEmpty(pass) ||string.IsNullOrEmpty(sendAddress))
            {
                MessageBox.Show("Gmail, App pass, Gmail nhận không được để trống", this.Text);
                return;
            }

            if (!Directory.Exists(htmlFolder)) Directory.CreateDirectory(htmlFolder);

            if (File.Exists(hiddenListFile))
            {
                hiddenList = File.ReadAllLines(hiddenListFile).ToList();
            }
            if (File.Exists(afterListFile))
            {
                afterList = File.ReadAllLines(afterListFile, Encoding.UTF8).ToList();
            }
            if (File.Exists(specialListFile))
            {
                specialList = File.ReadAllLines(specialListFile, Encoding.UTF8).ToList();
            }
            if (File.Exists(skipListFile))
            {
                skipList = File.ReadAllLines(skipListFile).ToList();
            }
            if (File.Exists(excludeListFile))
            {
                excludeList = File.ReadAllLines(excludeListFile).ToList();
            }
            if (File.Exists(historyFile))
            {
                histories = File.ReadAllLines(historyFile).ToList();
            }

            SaveSetting();

            ReadInboxAndSend(gmail, pass, sendAddress);

            isRunning = false;
        }

        private void dataGridEmail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridEmail.CurrentCell.RowIndex;
            string uid = dataGridEmail.Rows[index].Cells["_eMid"].Value.ToString();

            string htmlfile = Path.Combine(htmlFolder, uid + ".html");
            htmlfile = File.ReadAllText(htmlfile);

            webBrowser.DocumentText = htmlfile;
        }

        private void linkListRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!File.Exists(hiddenListFile))
            {
                File.WriteAllText(hiddenListFile, "", Encoding.UTF8);
            }
            Process.Start(hiddenListFile);
        }

        private void linkListSkip_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!File.Exists(skipListFile))
            {
                File.WriteAllText(skipListFile, "", Encoding.UTF8);
            }
            Process.Start(skipListFile);
        }

        private void linkListExclude_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!File.Exists(excludeListFile))
            {
                File.WriteAllText(excludeListFile, "", Encoding.UTF8);
            }
            Process.Start(excludeListFile);
        }

        private void linkListAfter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!File.Exists(afterListFile))
            {
                File.WriteAllText(afterListFile, "", Encoding.UTF8);
            }
            Process.Start(afterListFile);
        }

        private void linkListSpecial_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!File.Exists(specialListFile))
            {
                File.WriteAllText(specialListFile, "", Encoding.UTF8);
            }
            Process.Start(specialListFile);
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            timerStart.Enabled = false;
            try
            {
                string text = new System.Net.WebClient().DownloadString("https://raw.githubusercontent.com/camfrmobile/GmailCustomForward/main/isActive?t=" + new Random().Next(100, 999));
                isActive = Convert.ToBoolean(text);
                groupBox.Enabled = isActive;
                splitContainer.Enabled = isActive;
                dataGridEmail.Enabled = isActive;
                if (!isActive)
                {
                    checkExitOnClose.Checked = true;
                    MessageBox.Show("Bạn chưa kích hoạt giấy phép.", this.Text);
                    Application.Exit();
                }
            }
            catch (Exception)
            {
            }
        }

        private void timerForward_Tick(object sender, EventArgs e)
        {
            timerUpdate.Enabled = false;

            buttonGmail_Click(null, null);

            timerIndex = timerInterval / 1000;
            timerUpdate.Enabled = true;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            this.Text = $"{appname} {version.ToString("0.0")} - {timerIndex--}s";
        }

        private void LoadSetting()
        {
            try
            {
                if (File.Exists(settingSave))
                {
                    string[] lines = File.ReadAllLines(settingSave);
                    string gmail = lines[0];
                    textGmail.Text = gmail;

                    string pass = lines[1];
                    pass = Decrypt(pass);
                    textPass.Text = pass;

                    string send = lines[2];
                    textSend.Text = send;
                }
            }
            catch (Exception)
            {
            }
        }

        private void SaveSetting()
        {
            try
            {
                string gmail = textGmail.Text.Trim();
                string pass = textPass.Text.Trim();
                string send = textSend.Text.Trim();
                pass = Encrypt(pass);
                string setting = $"{gmail}\n{pass}\n{send}\n";
                File.WriteAllText(settingSave, setting);
            }
            catch (Exception)
            {
            }
        }

        private void SetupProgress(int value, int maximum = 100)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke((MethodInvoker)delegate
                {
                    progressBar.Maximum = maximum;
                    progressBar.Value = value;
                });
            }
            else
            {
                progressBar.Maximum = maximum;
                progressBar.Value = value;
            }
        }

        private void SetupTableSort()
        {
            if (dataGridEmail.InvokeRequired)
            {
                dataGridEmail.Invoke((MethodInvoker)delegate
                {
                    dataGridEmail.Sort(dataGridEmail.Columns["_eDate"], ListSortDirection.Ascending);
                });
            }
            else
            {
                dataGridEmail.Sort(dataGridEmail.Columns["_eDate"], ListSortDirection.Ascending);
            }

        }

        private void ReadInboxAndSend(string email, string pass, string sendAddress)
        {
            eTable.Rows.Clear();
            SetupProgress(1, 100);

            string ipmap = "smtp.gmail.com";
            int port = 993;
            var client = new ImapClient();
            int progress = 0;
            int maximum = 10;
            DateTime sinceTime = number == 0 ? DateTime.Now.AddHours(-12) : DateTime.Now.AddHours(-1);
            string space = "-";

            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect(ipmap, port, true);
            client.Authenticate(email, pass);
            IMailFolder inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);
            // get list index
            IList<UniqueId> inboxuids = inbox.Search(SearchQuery.SentSince(sinceTime));
            // get list labels
            IList<IMessageSummary> summaries = client.Inbox.Fetch(inboxuids, MessageSummaryItems.GMailLabels);
            maximum = inboxuids.Count;

            // SMTP send mail
            MimeMessage sendmsg = new MimeMessage();
            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, true);
            smtp.Authenticate(email, pass);

            for (int i = 0; i < inboxuids.Count; i++)
            {
                SetupProgress(++progress, maximum);
                UniqueId uniqueid = inboxuids[i];
                IList<string> gmailabels = summaries[i].GMailLabels;

                // skip mail not in label
                if (gmailabels.Count == 1) continue;

                //MimeMessage message = inbox.GetMessage(i);
                MimeMessage message = inbox.GetMessage(uniqueid);
                string msgid = message.MessageId;

                // skip old email
                if (histories.Contains(msgid)) continue;

                string date = message.Date.ToString("yyyy-MM-dd HH:mm:ss");
                string fromName = message.From[0].Name;
                string fromAddress = message.From.Mailboxes.Single().Address;
                string toName = message.To[0].Name;
                string subject = message.Subject;
                string htmlbody = message.HtmlBody;
                string textbody = message.TextBody;
                string labels = string.Join(space, gmailabels).Replace("\\Important" + space, "");
                bool isSkip = false;

                // skip list
                foreach (var word in skipList)
                {
                    if (htmlbody.Contains(word))
                    {
                        isSkip = true;
                    }
                }
                if (isSkip) continue;

                // special list
                foreach (var item in specialList)
                {
                    Match match = Regex.Match(textbody, @"\W" + item + @"\W");
                    if (match.Success)
                    {
                        labels += $"{space}{item}";
                    }
                }

                // save html
                htmlbody = ClearSecret(htmlbody, fromName, fromAddress);
                string htmlfile = Path.Combine(htmlFolder, msgid + ".html");
                File.WriteAllText(htmlfile, htmlbody);

                // attachments
                IEnumerable<MimeEntity> attachments = message.Attachments;

                // SMTP send mail
                sendmsg.From.Add(new MailboxAddress(toName, email));
                sendmsg.To.Add(new MailboxAddress(sendAddress.Replace("@gmail.com", ""), sendAddress));
                sendmsg.Subject = $"{labels}: {subject}";
                sendmsg.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlbody };
                foreach (var item in attachments)
                {
                    sendmsg.Attachments.Append(item);
                }
                smtp.Send(sendmsg);

                // add form
                eTable.Rows.Add(uniqueid, msgid, subject, $"{fromName} | {fromAddress}", labels, date);

                // seen
                //inbox.Append(message, MessageFlags.Seen);                

                // history
                histories.Add(msgid);
            }

            /* seen
            FolderNamespaceCollection namespaces = client.PersonalNamespaces;
            foreach (FolderNamespace nspace in namespaces)
            {
                IMailFolder folder = client.GetFolder(nspace);
                IList<IMailFolder> subfolders = folder.GetSubfolders();
                foreach (IMailFolder subfolder in subfolders)
                {
                    if (subfolder.FullName == "[Gmail]" || subfolder.FullName == "INBOX") continue;
                    subfolder.Open(FolderAccess.ReadOnly);
                }
            }*/

            client.Disconnect(true);
            smtp.Disconnect(true);

            if (histories.Count > 500) histories.RemoveAt(0);
            File.WriteAllLines(historyFile, histories);

            SetupTableSort();
            SetupProgress(0);
            ++number;
        }

        private string ClearSecret(string html, string fromName, string fromAddress)
        {
            string source = html;

            // remove regex
            string[] lines = source.Replace("><", ">|<").Split('|');
            bool isHasSign = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                bool isExclude = false;

                // remove signature
                foreach (var item in afterList)
                {
                    if (line.Contains(item) || line.ToLower().Contains(item.ToLower()))
                    {
                        isHasSign = true;
                    }
                }
                foreach (var item in excludeList)
                {
                    if (line.Contains(item) || line.ToLower().Contains(item.ToLower()))
                    {
                        isExclude = true;
                    }
                }

                if (isExclude)
                {
                }
                else if (isHasSign)
                {
                    line = string.Empty;
                }
                else
                {
                    // regex replace
                    foreach (var pattern in hiddenList)
                    {
                        line = Regex.Replace(line, pattern, "", RegexOptions.IgnoreCase);
                    }
                }
                lines[i] = line;
            }
            source = string.Join("", lines);

            // remove from name
            source = source.Replace(fromName, "");
            // remove from address
            source = source.Replace(fromAddress, "").Replace(fromAddress.Replace("@gmail.com", ""), "");
            
            /* remove each name form name
            string[] array = fromName.Split(' ');
            foreach (var name in array)
            {
                source.Replace(name, "[?]");
            }
            */

            // remove money
            source = Regex.Replace(source, @"\$\d+.\d+|\$\d+|\$.\d+", "XX");

            // text replace
            foreach (var text in hiddenList)
            {
                source = source.Replace(text, "");
            }
            return source;
        }

        private void SetupStartup()
        {
            try
            {
                string startup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Application.ProductName + ".lnk");
                string script = Path.Combine(Path.GetTempPath(), "startup.vbs");
                File.WriteAllText(script, "Set oWS = WScript.CreateObject(\"WScript.Shell\") \nSet oLink = oWS.CreateShortcut(\"" + startup + "\") \noLink.TargetPath = \"" + Application.ExecutablePath + "\" \noLink.Save");
                Process process = Process.Start(new ProcessStartInfo() { FileName = script, WindowStyle = ProcessWindowStyle.Hidden });
                process.WaitForExit();
                Thread.Sleep(1000);
                File.Delete(script);
            }
            catch (Exception)
            {
            }
        }

        public string Encrypt(string plainText, string password = "langkhach")
        {
            var data = Encoding.Default.GetBytes(plainText);
            var pwd = !string.IsNullOrEmpty(password) ? Encoding.Default.GetBytes(password) : Array.Empty<byte>();
            var cipher = ProtectedData.Protect(data, pwd, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(cipher);
        }

        public string Decrypt(string cipherText, string password = "langkhach")
        {
            var cipher = Convert.FromBase64String(cipherText);
            var pwd = !string.IsNullOrEmpty(password) ? Encoding.Default.GetBytes(password) : Array.Empty<byte>();
            var data = ProtectedData.Unprotect(cipher, pwd, DataProtectionScope.CurrentUser);
            return Encoding.Default.GetString(data);
        }

    }
}
