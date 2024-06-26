﻿namespace GmailCustomForward
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.linkListSkipEmail = new System.Windows.Forms.LinkLabel();
            this.buttonClearHistory = new System.Windows.Forms.Button();
            this.checkExitOnClose = new System.Windows.Forms.CheckBox();
            this.linkListSpecial = new System.Windows.Forms.LinkLabel();
            this.linkListAfter = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.linkListExclude = new System.Windows.Forms.LinkLabel();
            this.linkListSkip = new System.Windows.Forms.LinkLabel();
            this.linkListRemove = new System.Windows.Forms.LinkLabel();
            this.textSend = new System.Windows.Forms.TextBox();
            this.textPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textGmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGmail = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridEmail = new System.Windows.Forms.DataGridView();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerForward = new System.Windows.Forms.Timer(this.components);
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this._eUid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eLabels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eLocalDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eSentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eMid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.dateTimePicker);
            this.groupBox.Controls.Add(this.linkListSkipEmail);
            this.groupBox.Controls.Add(this.buttonClearHistory);
            this.groupBox.Controls.Add(this.checkExitOnClose);
            this.groupBox.Controls.Add(this.linkListSpecial);
            this.groupBox.Controls.Add(this.linkListAfter);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.linkListExclude);
            this.groupBox.Controls.Add(this.linkListSkip);
            this.groupBox.Controls.Add(this.linkListRemove);
            this.groupBox.Controls.Add(this.textSend);
            this.groupBox.Controls.Add(this.textPass);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.textGmail);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.buttonGmail);
            this.groupBox.Location = new System.Drawing.Point(5, -2);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(1474, 130);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "Quét từ lúc";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(254, 102);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(234, 24);
            this.dateTimePicker.TabIndex = 19;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // linkListSkipEmail
            // 
            this.linkListSkipEmail.AutoSize = true;
            this.linkListSkipEmail.Location = new System.Drawing.Point(797, 43);
            this.linkListSkipEmail.Name = "linkListSkipEmail";
            this.linkListSkipEmail.Size = new System.Drawing.Size(211, 18);
            this.linkListSkipEmail.TabIndex = 18;
            this.linkListSkipEmail.TabStop = true;
            this.linkListSkipEmail.Text = "Bỏ qua những địa chỉ email này";
            this.linkListSkipEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListSkipEmail_LinkClicked);
            // 
            // buttonClearHistory
            // 
            this.buttonClearHistory.Location = new System.Drawing.Point(6, 100);
            this.buttonClearHistory.Name = "buttonClearHistory";
            this.buttonClearHistory.Size = new System.Drawing.Size(150, 26);
            this.buttonClearHistory.TabIndex = 17;
            this.buttonClearHistory.Text = "Xóa lịch sử";
            this.buttonClearHistory.UseVisualStyleBackColor = true;
            this.buttonClearHistory.Click += new System.EventHandler(this.buttonClearHistory_Click);
            // 
            // checkExitOnClose
            // 
            this.checkExitOnClose.AutoSize = true;
            this.checkExitOnClose.Location = new System.Drawing.Point(1370, 14);
            this.checkExitOnClose.Name = "checkExitOnClose";
            this.checkExitOnClose.Size = new System.Drawing.Size(115, 22);
            this.checkExitOnClose.TabIndex = 16;
            this.checkExitOnClose.Text = "Exit on close";
            this.checkExitOnClose.UseVisualStyleBackColor = true;
            // 
            // linkListSpecial
            // 
            this.linkListSpecial.AutoSize = true;
            this.linkListSpecial.Location = new System.Drawing.Point(509, 73);
            this.linkListSpecial.Name = "linkListSpecial";
            this.linkListSpecial.Size = new System.Drawing.Size(269, 18);
            this.linkListSpecial.TabIndex = 13;
            this.linkListSpecial.TabStop = true;
            this.linkListSpecial.Text = "Danh sách từ đặc biệt có trong nội dung";
            this.linkListSpecial.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListSpecial_LinkClicked);
            // 
            // linkListAfter
            // 
            this.linkListAfter.AutoSize = true;
            this.linkListAfter.Location = new System.Drawing.Point(509, 43);
            this.linkListAfter.Name = "linkListAfter";
            this.linkListAfter.Size = new System.Drawing.Size(273, 18);
            this.linkListAfter.TabIndex = 12;
            this.linkListAfter.TabStop = true;
            this.linkListAfter.Text = "Loại bỏ toàn bộ nội dung phía sau từ này";
            this.linkListAfter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListAfter_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Gmail nhận";
            // 
            // linkListExclude
            // 
            this.linkListExclude.AutoSize = true;
            this.linkListExclude.Location = new System.Drawing.Point(797, 73);
            this.linkListExclude.Name = "linkListExclude";
            this.linkListExclude.Size = new System.Drawing.Size(143, 18);
            this.linkListExclude.TabIndex = 14;
            this.linkListExclude.TabStop = true;
            this.linkListExclude.Text = "Danh sách từ loại trừ";
            this.linkListExclude.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListExclude_LinkClicked);
            // 
            // linkListSkip
            // 
            this.linkListSkip.AutoSize = true;
            this.linkListSkip.Location = new System.Drawing.Point(797, 11);
            this.linkListSkip.Name = "linkListSkip";
            this.linkListSkip.Size = new System.Drawing.Size(261, 18);
            this.linkListSkip.TabIndex = 15;
            this.linkListSkip.TabStop = true;
            this.linkListSkip.Text = "Bỏ qua email nếu nội dung chứa từ này";
            this.linkListSkip.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListSkip_LinkClicked);
            // 
            // linkListRemove
            // 
            this.linkListRemove.AutoSize = true;
            this.linkListRemove.Location = new System.Drawing.Point(509, 11);
            this.linkListRemove.Name = "linkListRemove";
            this.linkListRemove.Size = new System.Drawing.Size(263, 18);
            this.linkListRemove.TabIndex = 11;
            this.linkListRemove.TabStop = true;
            this.linkListRemove.Text = "Loại bỏ những từ này có trong nội dung";
            this.linkListRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListRemove_LinkClicked);
            // 
            // textSend
            // 
            this.textSend.Location = new System.Drawing.Point(254, 70);
            this.textSend.Name = "textSend";
            this.textSend.Size = new System.Drawing.Size(234, 24);
            this.textSend.TabIndex = 3;
            // 
            // textPass
            // 
            this.textPass.Location = new System.Drawing.Point(254, 40);
            this.textPass.Name = "textPass";
            this.textPass.Size = new System.Drawing.Size(234, 24);
            this.textPass.TabIndex = 2;
            this.textPass.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "App pass";
            // 
            // textGmail
            // 
            this.textGmail.Location = new System.Drawing.Point(254, 11);
            this.textGmail.Name = "textGmail";
            this.textGmail.Size = new System.Drawing.Size(234, 24);
            this.textGmail.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gmail";
            // 
            // buttonGmail
            // 
            this.buttonGmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGmail.Location = new System.Drawing.Point(6, 14);
            this.buttonGmail.Name = "buttonGmail";
            this.buttonGmail.Size = new System.Drawing.Size(150, 80);
            this.buttonGmail.TabIndex = 5;
            this.buttonGmail.Text = "Thực hiện";
            this.buttonGmail.UseVisualStyleBackColor = true;
            this.buttonGmail.Click += new System.EventHandler(this.buttonGmail_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Location = new System.Drawing.Point(5, 150);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dataGridEmail);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer.Size = new System.Drawing.Size(1474, 600);
            this.splitContainer.SplitterDistance = 491;
            this.splitContainer.TabIndex = 19;
            // 
            // dataGridEmail
            // 
            this.dataGridEmail.AllowUserToAddRows = false;
            this.dataGridEmail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEmail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._eUid,
            this._eSubject,
            this._eFrom,
            this._eLabels,
            this._eLocalDate,
            this._eSentDate,
            this._eMid});
            this.dataGridEmail.Location = new System.Drawing.Point(3, 3);
            this.dataGridEmail.Name = "dataGridEmail";
            this.dataGridEmail.ReadOnly = true;
            this.dataGridEmail.RowHeadersWidth = 24;
            this.dataGridEmail.RowTemplate.Height = 24;
            this.dataGridEmail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridEmail.Size = new System.Drawing.Size(485, 594);
            this.dataGridEmail.TabIndex = 20;
            this.dataGridEmail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridEmail_CellClick);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(979, 600);
            this.webBrowser.TabIndex = 21;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(5, 130);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1474, 12);
            this.progressBar.TabIndex = 10;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Gmail Custom Forward";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // timerForward
            // 
            this.timerForward.Interval = 1000;
            this.timerForward.Tick += new System.EventHandler(this.timerForward_Tick);
            // 
            // timerStart
            // 
            this.timerStart.Interval = 500;
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // _eUid
            // 
            this._eUid.DataPropertyName = "_eUid";
            this._eUid.HeaderText = "Unique id";
            this._eUid.MinimumWidth = 6;
            this._eUid.Name = "_eUid";
            this._eUid.ReadOnly = true;
            this._eUid.Visible = false;
            this._eUid.Width = 125;
            // 
            // _eSubject
            // 
            this._eSubject.DataPropertyName = "_eSubject";
            this._eSubject.HeaderText = "Subject";
            this._eSubject.MinimumWidth = 6;
            this._eSubject.Name = "_eSubject";
            this._eSubject.ReadOnly = true;
            this._eSubject.Width = 500;
            // 
            // _eFrom
            // 
            this._eFrom.DataPropertyName = "_eFrom";
            this._eFrom.HeaderText = "From";
            this._eFrom.MinimumWidth = 6;
            this._eFrom.Name = "_eFrom";
            this._eFrom.ReadOnly = true;
            this._eFrom.Width = 200;
            // 
            // _eLabels
            // 
            this._eLabels.DataPropertyName = "_eLabels";
            this._eLabels.HeaderText = "Labels";
            this._eLabels.MinimumWidth = 6;
            this._eLabels.Name = "_eLabels";
            this._eLabels.ReadOnly = true;
            this._eLabels.Width = 200;
            // 
            // _eLocalDate
            // 
            this._eLocalDate.DataPropertyName = "_eLocalDate";
            this._eLocalDate.HeaderText = "Local date";
            this._eLocalDate.MinimumWidth = 6;
            this._eLocalDate.Name = "_eLocalDate";
            this._eLocalDate.ReadOnly = true;
            this._eLocalDate.Width = 125;
            // 
            // _eSentDate
            // 
            this._eSentDate.DataPropertyName = "_eSentDate";
            this._eSentDate.HeaderText = "Sent date";
            this._eSentDate.MinimumWidth = 6;
            this._eSentDate.Name = "_eSentDate";
            this._eSentDate.ReadOnly = true;
            this._eSentDate.Width = 125;
            // 
            // _eMid
            // 
            this._eMid.DataPropertyName = "_eMid";
            this._eMid.HeaderText = "Msg Id";
            this._eMid.MinimumWidth = 6;
            this._eMid.Name = "_eMid";
            this._eMid.ReadOnly = true;
            this._eMid.Width = 125;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.groupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Gmail Custom Forward";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGmail;
        private System.Windows.Forms.TextBox textGmail;
        private System.Windows.Forms.TextBox textPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textSend;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.DataGridView dataGridEmail;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.LinkLabel linkListExclude;
        private System.Windows.Forms.LinkLabel linkListSkip;
        private System.Windows.Forms.LinkLabel linkListRemove;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer timerForward;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkListAfter;
        private System.Windows.Forms.LinkLabel linkListSpecial;
        private System.Windows.Forms.CheckBox checkExitOnClose;
        private System.Windows.Forms.Timer timerStart;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Button buttonClearHistory;
        private System.Windows.Forms.LinkLabel linkListSkipEmail;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eUid;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eLabels;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eLocalDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eSentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eMid;
    }
}

