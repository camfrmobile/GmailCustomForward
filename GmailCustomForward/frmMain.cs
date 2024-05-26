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
        static double version = 2.0;
        static string appname = "Gmail Custom Forward";
        static bool isActive = false;
        static bool isRunning = false;
        static DataTable eTable = new DataTable();

        static string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward");

        static string htmlFolder = Path.Combine(Path.GetTempPath(), "GmailCustomForward");

        static string settingSave = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting.txt");

        static List<string> skipList = new List<string>();
        static string skipListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting_skip.txt");

        static List<string> skipEmailList = new List<string>();
        static string skipEmailListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GmailCustomForward", "setting_skipemail.txt");

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

            dateTimePicker.CustomFormat = "dd-MM-yyyy HH:mm";
            dateTimePicker.Value = DateTime.Today;

            eTable = new DataTable();
            eTable.Columns.Add("_eUid", typeof(string));
            eTable.Columns.Add("_eSubject", typeof(string));
            eTable.Columns.Add("_eFrom", typeof(string));
            eTable.Columns.Add("_eLabels", typeof(string));
            eTable.Columns.Add("_eLocalDate", typeof(string));
            eTable.Columns.Add("_eSentDate", typeof(string));
            eTable.Columns.Add("_eMid", typeof(string));

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

            listFile = Path.Combine(Application.StartupPath, Path.GetFileName(skipEmailListFile));
            if (File.Exists(listFile))
            {
                if (File.Exists(skipEmailListFile)) File.Delete(skipEmailListFile);
                File.Move(listFile, skipEmailListFile);
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
#if DEBUG
            timerForward.Enabled = false;
#endif
            if (isRunning) return;
            isRunning = true;
            string gmail = textGmail.Text.Trim();
            string pass = textPass.Text.Trim();
            string sendAddress = textSend.Text.Trim();

            if (Directory.Exists(htmlFolder)) Directory.Delete(htmlFolder, true);

            if (string.IsNullOrEmpty(gmail) || string.IsNullOrEmpty(pass) ||string.IsNullOrEmpty(sendAddress))
            {
                isRunning = false;
                MessageBox.Show("Gmail, App pass, Gmail nhận không được để trống", this.Text);
                return;
            }

            SetupProgress(1, 100);

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
            if (File.Exists(skipEmailListFile))
            {
                skipEmailList = File.ReadAllLines(skipEmailListFile).ToList();
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

            timerStart_Tick(null, null);
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

        private void linkListSkipEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!File.Exists(skipEmailListFile))
            {
                File.WriteAllText(skipEmailListFile, "", Encoding.UTF8);
            }
            Process.Start(skipEmailListFile);
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
                    timerForward.Enabled = isActive;
                    timerUpdate.Enabled = isActive;
                    timerStart.Enabled = isActive;
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
            timerForward.Enabled = false;

            buttonGmail_Click(null, null);

            timerForward.Enabled = true;
            timerIndex = timerInterval / 1000;
            timerUpdate.Enabled = true;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            this.Text = $"{appname} {version.ToString("0.0")} - {timerIndex--}s";
        }

        private void buttonClearHistory_Click(object sender, EventArgs e)
        {
            number = 0;
            string button = buttonClearHistory.Text;
            histories.Clear();
            File.WriteAllText(historyFile, "");
            buttonClearHistory.Text = "Đã xóa lịch sử";
            Task mytask = new Task(() =>
            {
                Thread.Sleep(500);
                if (buttonClearHistory.InvokeRequired)
                {
                    buttonClearHistory.Invoke((MethodInvoker)delegate
                    {
                        buttonClearHistory.Text = button;
                    });
                }
                else
                {
                    buttonClearHistory.Text = button;
                }
            });
            mytask.Start();
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            number = 0;
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
                    dataGridEmail.Sort(dataGridEmail.Columns["_eLocalDate"], ListSortDirection.Ascending);
                });
            }
            else
            {
                dataGridEmail.Sort(dataGridEmail.Columns["_eLocalDate"], ListSortDirection.Ascending);
            }

        }

        private void ReadInboxAndSend(string email, string pass, string sendAddress)
        {
            eTable.Rows.Clear();

            string ipmap = "smtp.gmail.com";
            int port = 993;
            var client = new ImapClient();
            int progress = 0;
            int maximum = 10;
            DateTime inputTime = number <= 0 ? dateTimePicker.Value : DateTime.Now;
            DateTime queryTime = inputTime.AddHours(-8);
            string space = " | ";

            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect(ipmap, port, true);
            client.Authenticate(email, pass);
            IMailFolder inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);
            // get list index
            IList<UniqueId> inboxuids = inbox.Search(SearchQuery.DeliveredAfter(queryTime));
            //IList<UniqueId> inboxuids = inbox.Search(SearchQuery.SentSince(queryTime));
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

                List<string> gmailabels = new List<string>();
                foreach (var glabel in summaries[i].GMailLabels)
                {
                    if (glabel == @"\Important" || glabel == @"\Sent") continue;
                    gmailabels.Add(glabel);
                }

                //MimeMessage message = inbox.GetMessage(i);
                MimeMessage message = inbox.GetMessage(uniqueid);
                string msgid = message.MessageId;

                string localDate = message.Date.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                string sentDate = message.Date.ToString("yyyy-MM-dd HH:mm:ss");
                string fromName = message.From[0].Name;
                string fromAddress = message.From.Mailboxes.Single().Address;
                string toName = message.To[0].Name;
                string subject = message.Subject;
                string bodycontent = message.HtmlBody != null ? message.HtmlBody : message.TextBody;
                string labels = string.Join(space, gmailabels);
                bool isSkip = false;

                // skip mail not in label
                if (gmailabels.Count == 0)
                {
                    isSkip = true;
                }
                // skip old email
                if (histories.Contains(msgid))
                {
                    isSkip = true;
                }
                // skip list
                if (inputTime > message.Date.LocalDateTime)
                {
                    isSkip = true;
                }
                foreach (var word in skipList)
                {
                    if (bodycontent.Contains(word))
                    {
                        isSkip = true;
                    }
                }
                foreach (var address in skipEmailList)
                {
                    if (fromAddress.Contains(address))
                    {
                        isSkip = true;
                    }
                }
                if (isSkip)
                {
                    continue;
                }
#if DEBUG
                File.WriteAllText(msgid + ".html", bodycontent, Encoding.UTF8);
#endif
                // special list
                foreach (var item in specialList)
                {
                    Match match = Regex.Match(bodycontent, @"\W" + item + @"\W");
                    if (match.Success)
                    {
                        labels += $"{space}{item}";
                    }
                }

                subject = ClearSecret(subject, fromName, fromAddress);

                // save html
                bodycontent = ClearSecret(bodycontent, fromName, fromAddress);
                string htmlfile = Path.Combine(htmlFolder, msgid + ".html");
                File.WriteAllText(htmlfile, bodycontent);

                // attachments
                IEnumerable<MimeEntity> attachments = message.Attachments;

                // SMTP send mail
                sendmsg.From.Add(new MailboxAddress(toName, email));
                sendmsg.To.Add(new MailboxAddress(sendAddress.Replace("@gmail.com", ""), sendAddress));
                sendmsg.Subject = $"{labels}: {subject}";
                sendmsg.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = bodycontent };
                foreach (var item in attachments)
                {
                    sendmsg.Attachments.Append(item);
                }
                
                string result = smtp.Send(sendmsg);
#if DEBUG
                File.AppendAllText("sents.log", $"{result}\n", Encoding.UTF8);
#endif
                if (!result.Contains("2.0.0 OK"))
                {
                    MessageBox.Show("Sent email error!");
                    break;
                }
                // add form
                eTable.Rows.Add(uniqueid, subject, $"{fromName} | {fromAddress}", labels, localDate, sentDate, msgid);

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
            string[] names = fromName.Split(' ');

            // remove regex
            string[] lines = source.Replace("><", ">|<").Split('|');
            bool isHasSign = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                bool isExclude = false;

                // remove signature
                if (i >= lines.Length / 4 && i > 0)
                {
                    foreach (var item in afterList)
                    {
                        if (line.Contains(item) || line.ToLower().Contains(item.ToLower()))
                        {
                            isHasSign = true;
                        }
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
                    line = "<div style=\"margin:10px\"></div>";
                }
                else
                {
                    // regex replace
                    foreach (var pattern in hiddenList)
                    {
                        try { line = Regex.Replace(line, pattern, "", RegexOptions.IgnoreCase); } catch { }
                    }
                    // remove each name form name
                    try
                    {
                        foreach (var name in names)
                        {
                            if (Regex.Match(line, @"\W" + name + @"\W").Success)
                            {
                                line = line.Replace(name, "");
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                lines[i] = line;
            }
            source = string.Join("", lines);

            // remove from name
            try { source = source.Replace(fromName, ""); } catch { }

            // remove from address
            try { source = source.Replace(fromAddress, "").Replace(fromAddress.Replace("@gmail.com", ""), ""); } catch { }

            // remove money
            try { source = Regex.Replace(source, @"\$\d+.\d+|\$\d+|\$.\d+|$(.{0,6})\d+.\d+(.{0,6})|\d+(.{0,6})\d+(.{0,6})USD", "XX"); } catch { }

            // text replace
            foreach (var text in hiddenList)
            {
                try { source = source.Replace(text, ""); } catch { }
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
