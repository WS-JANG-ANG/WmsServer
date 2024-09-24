namespace SiemensPLC
{
    partial class frmSet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSet));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbLocalIp = new System.Windows.Forms.ComboBox();
            this.tbPLCName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudWriteNo = new System.Windows.Forms.NumericUpDown();
            this.nudReadNo = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPlcPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPlcIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonTest = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvPLC = new System.Windows.Forms.DataGridView();
            this.PLC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLC)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(459, 326);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Save";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(551, 326);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(290, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(333, 290);
            this.tabControl1.TabIndex = 30;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbLocalIp);
            this.tabPage1.Controls.Add(this.tbPLCName);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.nudWriteNo);
            this.tabPage1.Controls.Add(this.nudReadNo);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tbLocalPort);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tbPlcPort);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tbPlcIp);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(325, 263);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Network";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbLocalIp
            // 
            this.cbLocalIp.FormattingEnabled = true;
            this.cbLocalIp.Location = new System.Drawing.Point(115, 125);
            this.cbLocalIp.Name = "cbLocalIp";
            this.cbLocalIp.Size = new System.Drawing.Size(177, 22);
            this.cbLocalIp.TabIndex = 50;
            // 
            // tbPLCName
            // 
            this.tbPLCName.Location = new System.Drawing.Point(115, 13);
            this.tbPLCName.Name = "tbPLCName";
            this.tbPLCName.Size = new System.Drawing.Size(177, 22);
            this.tbPLCName.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 14);
            this.label7.TabIndex = 48;
            this.label7.Text = "PLC Name";
            // 
            // nudWriteNo
            // 
            this.nudWriteNo.Location = new System.Drawing.Point(115, 225);
            this.nudWriteNo.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudWriteNo.Name = "nudWriteNo";
            this.nudWriteNo.Size = new System.Drawing.Size(120, 22);
            this.nudWriteNo.TabIndex = 11;
            // 
            // nudReadNo
            // 
            this.nudReadNo.Location = new System.Drawing.Point(115, 197);
            this.nudReadNo.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudReadNo.Name = "nudReadNo";
            this.nudReadNo.Size = new System.Drawing.Size(120, 22);
            this.nudReadNo.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "Write number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "Read Number";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Location = new System.Drawing.Point(115, 155);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(177, 22);
            this.tbLocalPort.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Local Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "Local IP";
            // 
            // tbPlcPort
            // 
            this.tbPlcPort.Location = new System.Drawing.Point(115, 86);
            this.tbPlcPort.Name = "tbPlcPort";
            this.tbPlcPort.Size = new System.Drawing.Size(177, 22);
            this.tbPlcPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "PLC Port";
            // 
            // tbPlcIp
            // 
            this.tbPlcIp.Location = new System.Drawing.Point(115, 58);
            this.tbPlcIp.Name = "tbPlcIp";
            this.tbPlcIp.Size = new System.Drawing.Size(177, 22);
            this.tbPlcIp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "PLC IP";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(325, 264);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Option";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(286, 326);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 31;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::SiemensPLC.Properties.Resources.siemens;
            this.pictureBox1.InitialImage = global::SiemensPLC.Properties.Resources.siemens;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(87, 337);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvPLC);
            this.groupBox5.Controls.Add(this.buttonSave);
            this.groupBox5.Controls.Add(this.buttonDelete);
            this.groupBox5.Controls.Add(this.buttonNew);
            this.groupBox5.Location = new System.Drawing.Point(105, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(179, 292);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            // 
            // dgvPLC
            // 
            this.dgvPLC.AllowUserToAddRows = false;
            this.dgvPLC.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPLC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPLC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPLC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PLC});
            this.dgvPLC.Location = new System.Drawing.Point(5, 21);
            this.dgvPLC.MultiSelect = false;
            this.dgvPLC.Name = "dgvPLC";
            this.dgvPLC.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPLC.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPLC.RowHeadersVisible = false;
            this.dgvPLC.RowTemplate.Height = 23;
            this.dgvPLC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPLC.Size = new System.Drawing.Size(167, 236);
            this.dgvPLC.TabIndex = 37;
            this.dgvPLC.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPLC_CellClick);
            // 
            // PLC
            // 
            this.PLC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PLC.DefaultCellStyle = dataGridViewCellStyle2;
            this.PLC.HeaderText = "PLC";
            this.PLC.Name = "PLC";
            this.PLC.ReadOnly = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(61, 263);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(56, 23);
            this.buttonSave.TabIndex = 36;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click_1);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(116, 263);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(56, 23);
            this.buttonDelete.TabIndex = 35;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(6, 263);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(56, 23);
            this.buttonNew.TabIndex = 34;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // frmSet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(631, 361);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSet";
            this.Text = "Siemens PLC Device를 설정합니다. ";
            this.Load += new System.EventHandler(this.frmSet_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPlcPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPlcIp;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudWriteNo;
        private System.Windows.Forms.NumericUpDown nudReadNo;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.TextBox tbPLCName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvPLC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLC;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.ComboBox cbLocalIp;
    }
}