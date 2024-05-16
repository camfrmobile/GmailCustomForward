namespace GmailCustomForward
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonOpenHidden = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.textSend = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textGmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGmail = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridEmail = new System.Windows.Forms.DataGridView();
            this._eID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eLabels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonOpenSkip = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonOpenSkip);
            this.groupBox3.Controls.Add(this.buttonOpenHidden);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dateTimePicker);
            this.groupBox3.Controls.Add(this.textSend);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.buttonSend);
            this.groupBox3.Controls.Add(this.textPass);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textGmail);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.buttonGmail);
            this.groupBox3.Location = new System.Drawing.Point(5, -2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1474, 74);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // buttonOpenHidden
            // 
            this.buttonOpenHidden.Location = new System.Drawing.Point(661, 11);
            this.buttonOpenHidden.Name = "buttonOpenHidden";
            this.buttonOpenHidden.Size = new System.Drawing.Size(339, 30);
            this.buttonOpenHidden.TabIndex = 10;
            this.buttonOpenHidden.Text = "Loại bỏ những từ này trong nội dung";
            this.buttonOpenHidden.UseVisualStyleBackColor = true;
            this.buttonOpenHidden.Click += new System.EventHandler(this.buttonOpenHidden_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(443, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Từ ngày";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(509, 13);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(132, 24);
            this.dateTimePicker.TabIndex = 8;
            // 
            // textSend
            // 
            this.textSend.Location = new System.Drawing.Point(1268, 13);
            this.textSend.Name = "textSend";
            this.textSend.Size = new System.Drawing.Size(200, 24);
            this.textSend.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1179, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Gmail nhận";
            // 
            // buttonSend
            // 
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.Location = new System.Drawing.Point(1023, 11);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(150, 50);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "Gửi email";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textPass
            // 
            this.textPass.Location = new System.Drawing.Point(237, 40);
            this.textPass.Name = "textPass";
            this.textPass.Size = new System.Drawing.Size(200, 24);
            this.textPass.TabIndex = 4;
            this.textPass.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "App pass";
            // 
            // textGmail
            // 
            this.textGmail.Location = new System.Drawing.Point(237, 11);
            this.textGmail.Name = "textGmail";
            this.textGmail.Size = new System.Drawing.Size(200, 24);
            this.textGmail.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Gmail";
            // 
            // buttonGmail
            // 
            this.buttonGmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGmail.Location = new System.Drawing.Point(6, 14);
            this.buttonGmail.Name = "buttonGmail";
            this.buttonGmail.Size = new System.Drawing.Size(150, 54);
            this.buttonGmail.TabIndex = 0;
            this.buttonGmail.Text = "Lấy danh sách";
            this.buttonGmail.UseVisualStyleBackColor = true;
            this.buttonGmail.Click += new System.EventHandler(this.buttonGmail_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(5, 95);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridEmail);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(1474, 657);
            this.splitContainer1.SplitterDistance = 491;
            this.splitContainer1.TabIndex = 3;
            // 
            // dataGridEmail
            // 
            this.dataGridEmail.AllowUserToAddRows = false;
            this.dataGridEmail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEmail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._eID,
            this._eSubject,
            this._eFrom,
            this._eLabels,
            this._eDate});
            this.dataGridEmail.Location = new System.Drawing.Point(3, 3);
            this.dataGridEmail.Name = "dataGridEmail";
            this.dataGridEmail.ReadOnly = true;
            this.dataGridEmail.RowHeadersWidth = 24;
            this.dataGridEmail.RowTemplate.Height = 24;
            this.dataGridEmail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridEmail.Size = new System.Drawing.Size(485, 651);
            this.dataGridEmail.TabIndex = 0;
            this.dataGridEmail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridEmail_CellClick);
            // 
            // _eID
            // 
            this._eID.DataPropertyName = "_eID";
            this._eID.HeaderText = "ID";
            this._eID.MinimumWidth = 6;
            this._eID.Name = "_eID";
            this._eID.ReadOnly = true;
            this._eID.Visible = false;
            this._eID.Width = 125;
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
            // _eDate
            // 
            this._eDate.HeaderText = "Date";
            this._eDate.MinimumWidth = 6;
            this._eDate.Name = "_eDate";
            this._eDate.ReadOnly = true;
            this._eDate.Width = 125;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(979, 657);
            this.webBrowser.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(5, 77);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1474, 12);
            this.progressBar.TabIndex = 4;
            // 
            // buttonOpenSkip
            // 
            this.buttonOpenSkip.Location = new System.Drawing.Point(661, 44);
            this.buttonOpenSkip.Name = "buttonOpenSkip";
            this.buttonOpenSkip.Size = new System.Drawing.Size(339, 30);
            this.buttonOpenSkip.TabIndex = 11;
            this.buttonOpenSkip.Text = "Bỏ qua email nếu nội dung chứa từ này";
            this.buttonOpenSkip.UseVisualStyleBackColor = true;
            this.buttonOpenSkip.Click += new System.EventHandler(this.buttonOpenSkip_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 753);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Gmail Custom Forward";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGmail;
        private System.Windows.Forms.TextBox textGmail;
        private System.Windows.Forms.TextBox textPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.DataGridView dataGridEmail;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eID;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eLabels;
        private System.Windows.Forms.DataGridViewTextBoxColumn _eDate;
        private System.Windows.Forms.Button buttonOpenHidden;
        private System.Windows.Forms.Button buttonOpenSkip;
    }
}

