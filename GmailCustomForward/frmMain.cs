using MailKit.Net.Imap;
using MailKit;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Asn1.X509;
using MailKit.Search;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GmailCustomForward
{
    public partial class frmMain : Form
    {
        private bool isActive = false;
        private bool isRunning = false;
        private DataTable eTable = new DataTable();
        private string htmlSave = Path.Combine(Application.StartupPath, "temp");
        private string settingSave = Path.Combine(Application.StartupPath, "setting.txt");
        private string skipListSave = Path.Combine(Application.StartupPath, "setting_skiplist.txt");
        private List<string> skipList = new List<string>();
        private string hiddenListSave = Path.Combine(Application.StartupPath, "setting_hiddenlist.txt");
        private List<string> hiddenList = new List<string>();

        public frmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            dateTimePicker.CustomFormat = "dd-MM-yyyy";

            dataGridEmail.Dock = DockStyle.Fill;
            dataGridEmail.Parent = splitContainer1.Panel1;

            try
            {
                string text = new System.Net.WebClient().DownloadString("https://raw.githubusercontent.com/camfrmobile/GmailCustomForward/main/isActive?t=" + new Random().Next(100, 999));
                isActive = Convert.ToBoolean(text);
                this.Enabled = isActive;
                splitContainer1.Enabled = isActive;
                dataGridEmail.Enabled = isActive;
            }
            catch (Exception)
            {
            }
        }

        private void buttonGmail_Click(object sender, EventArgs e)
        {
            if (isRunning) return;
            isRunning = true;
            string gmail = textGmail.Text.Trim();
            string pass = textPass.Text.Trim();
            DateTime fromdate = dateTimePicker.Value;

            if (string.IsNullOrEmpty(gmail) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Gmail và App pass không được để trống", this.Text);
                return;
            }
            if (!Directory.Exists(htmlSave))
            {
                Directory.CreateDirectory(htmlSave);
            }

            if (File.Exists(hiddenListSave))
            {
                hiddenList = File.ReadAllLines(hiddenListSave).ToList();
            }
            if (File.Exists(skipListSave))
            {
                skipList = File.ReadAllLines(skipListSave).ToList();
            }

            SaveSetting();

            dataGridEmail.Rows.Clear();

            progressBar.Maximum = 100;
            progressBar.Value = 10;

            Task mytask = new Task(() =>
            {
                ReadInbox(gmail, pass, fromdate);

                isRunning = false;
            });
            mytask.Start();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SaveSetting();


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

        private void ReadInbox(string email, string pass, DateTime fromdate)
        {
            eTable.Rows.Clear();

            string ipmap = "smtp.gmail.com";
            int port = 993;
            var client = new ImapClient();

            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            client.Connect(ipmap, port, true);

            client.Authenticate(email, pass);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            var inboxuids = inbox.Search(SearchQuery.SentSince(fromdate));

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = 0;
                    progressBar.Maximum = inboxuids.Count;
                });
            }
            else
            {
                progressBar.Value = 0;
                progressBar.Maximum = inboxuids.Count;
            }

            for (int i = 0; i < inboxuids.Count; i++)
            {
                var message = inbox.GetMessage(inboxuids[i]);

                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke((MethodInvoker)delegate { progressBar.Value = i + 1; });
                }
                else
                {
                    progressBar.Value = i + 1;
                }

                //var message = inbox.GetMessage(i);
                string uid = message.MessageId;
                string date = message.Date.ToString("yyyy-MM-dd HH:mm:ss");
                string fromName = message.From[0].Name;
                string fromAddress = message.From.Mailboxes.Single().Address;
                string subject = message.Subject;
                string htmlbody = message.HtmlBody;
                bool isSkip = false;

                // skip list
                foreach (var word in skipList)
                {
                    if (string.IsNullOrEmpty(word)) continue;
                    if (htmlbody.Contains(word))
                    {
                        isSkip = true;
                    }
                }

                if (!isSkip)
                {
                    // label
                    string labels = string.Empty;
                    var summaries = client.Inbox.Fetch(0, -1, MessageSummaryItems.GMailLabels);
                    foreach (var label in summaries)
                    {
                        labels = string.Join("|", label.GMailLabels);
                    }

                    // save html
                    //htmlbody = SimpleHTML(htmlbody);
                    htmlbody = ClearSecret(htmlbody, fromName, fromAddress);
                    string htmlfile = Path.Combine(htmlSave, uid + ".html");
                    File.WriteAllText(htmlfile, htmlbody);

                    // attachments
                    var attachments = message.Attachments;




                    //inbox.Append(message, MessageFlags.Seen);
                    if (dataGridEmail.InvokeRequired)
                    {
                        dataGridEmail.Invoke((MethodInvoker)delegate
                        {
                            dataGridEmail.Rows.Add(uid, subject, $"{fromName}|{fromAddress}", labels, date);
                        });
                    }
                    else
                    {
                        dataGridEmail.Rows.Add(uid, subject, $"{fromName}|{fromAddress}", labels, date);
                    }
                }
            }

            client.Disconnect(true);

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
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke((MethodInvoker)delegate { progressBar.Value = 0; });
            }
            else
            {
                progressBar.Value = 0;
            }

        }

        private string ClearSecret(string html, string fromName, string fromAddress)
        {
            string source = html;
            
            // remove from name
            source = source.Replace(fromName, "[?]");
            // remove from address
            source = source.Replace(fromAddress, "[?]");
            
            // remove each name form name
            string[] array = fromName.Split(' ');
            foreach (var name in array)
            {
                source.Replace(name, "[?]");
            }
            // remove money
            source = Regex.Replace(source, @"\$\d+.\d+|\$\d+", "$xx");

            // text replace
            foreach (var text in hiddenList)
            {
                source = source.Replace(text, "[?]");
            }
            // remove regex
            string[] lines = source.Replace("><", ">|<").Split('|');
            bool isHasSign = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // remove signature
                if (i > lines.Length * 0.7 && (line.Contains("Thank you") || line.Contains("Thanks")))
                {
                    isHasSign = true;
                }
                if (isHasSign)
                {
                    line = string.Empty;
                }
                else if (line.Contains("<img "))
                {

                }
                else
                {
                    // regex replace
                    foreach (var pattern in hiddenList)
                    {
                        line = Regex.Replace(line, pattern, "[remove]", RegexOptions.IgnoreCase);
                    }
                }
                lines[i] = line;
            }
            source = string.Join("", lines);

            return source;
        }

        private void dataGridEmail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridEmail.CurrentCell.RowIndex;
            string uid = dataGridEmail.Rows[index].Cells["_eID"].Value.ToString();

            string htmlfile = Path.Combine(htmlSave, uid + ".html");
            htmlfile = File.ReadAllText(htmlfile);

            webBrowser.DocumentText = htmlfile;
        }

        private void buttonOpenHidden_Click(object sender, EventArgs e)
        {
            if (!File.Exists(hiddenListSave))
            {
                File.WriteAllText(hiddenListSave, "");
            }
            Process.Start(hiddenListSave);
        }

        private void buttonOpenSkip_Click(object sender, EventArgs e)
        {
            if (!File.Exists(skipListSave))
            {
                File.WriteAllText(skipListSave, "");
            }
            Process.Start(skipListSave);
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
