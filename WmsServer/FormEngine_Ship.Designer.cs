namespace WmsServer
{
    partial class FormEngine_Ship
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvEngineList = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromplant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noship = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvShippingList = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToPlant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEngineList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShippingList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvEngineList);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvShippingList);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.splitContainer1.Size = new System.Drawing.Size(1079, 407);
            this.splitContainer1.SplitterDistance = 541;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 28;
            // 
            // dgvEngineList
            // 
            this.dgvEngineList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvEngineList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvEngineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEngineList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.code,
            this.indatetime,
            this.fromplant,
            this.noship});
            this.dgvEngineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEngineList.Location = new System.Drawing.Point(3, 47);
            this.dgvEngineList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvEngineList.Name = "dgvEngineList";
            this.dgvEngineList.ReadOnly = true;
            this.dgvEngineList.RowHeadersWidth = 20;
            this.dgvEngineList.RowTemplate.Height = 27;
            this.dgvEngineList.Size = new System.Drawing.Size(538, 357);
            this.dgvEngineList.TabIndex = 25;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // code
            // 
            this.code.HeaderText = "Model";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.code.Width = 70;
            // 
            // indatetime
            // 
            this.indatetime.HeaderText = "In DateTime";
            this.indatetime.Name = "indatetime";
            this.indatetime.ReadOnly = true;
            this.indatetime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.indatetime.Width = 150;
            // 
            // fromplant
            // 
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromplant.DefaultCellStyle = dataGridViewCellStyle31;
            this.fromplant.HeaderText = "From Plant";
            this.fromplant.Name = "fromplant";
            this.fromplant.ReadOnly = true;
            this.fromplant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // noship
            // 
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noship.DefaultCellStyle = dataGridViewCellStyle32;
            this.noship.HeaderText = "No Ship";
            this.noship.Name = "noship";
            this.noship.ReadOnly = true;
            this.noship.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.noship.Width = 50;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(538, 44);
            this.label1.TabIndex = 19;
            this.label1.Text = "Engine List";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvShippingList
            // 
            this.dgvShippingList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShippingList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvShippingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShippingList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Sequence,
            this.Model,
            this.Quantity,
            this.CurrentCount,
            this.ToPlant});
            this.dgvShippingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShippingList.Location = new System.Drawing.Point(0, 47);
            this.dgvShippingList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvShippingList.Name = "dgvShippingList";
            this.dgvShippingList.ReadOnly = true;
            this.dgvShippingList.RowHeadersWidth = 20;
            this.dgvShippingList.RowTemplate.Height = 27;
            this.dgvShippingList.Size = new System.Drawing.Size(530, 357);
            this.dgvShippingList.TabIndex = 21;
            // 
            // Date
            // 
            this.Date.Frozen = true;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Sequence
            // 
            this.Sequence.Frozen = true;
            this.Sequence.HeaderText = "Sequence";
            this.Sequence.Name = "Sequence";
            this.Sequence.ReadOnly = true;
            this.Sequence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sequence.Width = 80;
            // 
            // Model
            // 
            this.Model.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Model.HeaderText = "Model";
            this.Model.MinimumWidth = 65;
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Quantity
            // 
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle33;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Quantity.Width = 80;
            // 
            // CurrentCount
            // 
            this.CurrentCount.HeaderText = "Current Count";
            this.CurrentCount.Name = "CurrentCount";
            this.CurrentCount.ReadOnly = true;
            this.CurrentCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentCount.Width = 80;
            // 
            // ToPlant
            // 
            this.ToPlant.HeaderText = "To Plant";
            this.ToPlant.Name = "ToPlant";
            this.ToPlant.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(0, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(530, 44);
            this.label2.TabIndex = 20;
            this.label2.Text = "Shipping List";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormEngine_Ship
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1079, 407);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormEngine_Ship";
            this.Text = "Engine_Ship";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEngine_Ship_FormClosing);
            this.Load += new System.EventHandler(this.FormEngine_Ship_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEngineList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShippingList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvEngineList;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn indatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromplant;
        private System.Windows.Forms.DataGridViewTextBoxColumn noship;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvShippingList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToPlant;
        private System.Windows.Forms.Label label2;
    }
}