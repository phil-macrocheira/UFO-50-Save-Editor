namespace UFO_50_Save_Editor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            BtnOpen = new Button();
            BtnOverwrite = new Button();
            BtnAdd = new Button();
            VariableHeader = new ColumnHeader();
            ValueHeader = new ColumnHeader();
            dataGridView1 = new DataGridView();
            VariableCol = new DataGridViewTextBoxColumn();
            ValueCol = new DataGridViewTextBoxColumn();
            filterTextBox = new TextBox();
            label1 = new Label();
            BtnGift = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // BtnOpen
            // 
            BtnOpen.Location = new Point(21, 20);
            BtnOpen.Margin = new Padding(2);
            BtnOpen.Name = "BtnOpen";
            BtnOpen.Size = new Size(111, 37);
            BtnOpen.TabIndex = 1;
            BtnOpen.Text = "Open Save";
            BtnOpen.UseVisualStyleBackColor = true;
            BtnOpen.Click += BtnOpen_Click;
            // 
            // BtnOverwrite
            // 
            BtnOverwrite.Location = new Point(156, 20);
            BtnOverwrite.Margin = new Padding(2);
            BtnOverwrite.Name = "BtnOverwrite";
            BtnOverwrite.Size = new Size(140, 37);
            BtnOverwrite.TabIndex = 2;
            BtnOverwrite.Text = "Overwrite Save";
            BtnOverwrite.UseVisualStyleBackColor = true;
            BtnOverwrite.Click += BtnOverwrite_Click;
            // 
            // BtnAdd
            // 
            BtnAdd.Location = new Point(877, 70);
            BtnAdd.Margin = new Padding(2);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(103, 37);
            BtnAdd.TabIndex = 7;
            BtnAdd.Text = "Add New";
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += BtnAdd_Click;
            // 
            // BtnGift
            // 
            BtnGift.Location = new Point(853, 17);
            BtnGift.Margin = new Padding(2);
            BtnGift.Name = "BtnGift";
            BtnGift.Size = new Size(143, 37);
            BtnGift.TabIndex = 8;
            BtnGift.Text = "Collect All Gifts";
            BtnGift.UseVisualStyleBackColor = true;
            BtnGift.Click += BtnGift_Click;
            // 
            // VariableHeader
            // 
            VariableHeader.Text = "Variable";
            VariableHeader.Width = 200;
            // 
            // ValueHeader
            // 
            ValueHeader.Text = "Value";
            ValueHeader.Width = 150;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { VariableCol, ValueCol });
            dataGridView1.Location = new Point(10, 118);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.ShowCellErrors = false;
            dataGridView1.ShowCellToolTips = false;
            dataGridView1.ShowRowErrors = false;
            dataGridView1.Size = new Size(996, 539);
            dataGridView1.TabIndex = 4;
            // 
            // VariableCol
            // 
            VariableCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            VariableCol.HeaderText = "Variable";
            VariableCol.MaxInputLength = 256;
            VariableCol.MinimumWidth = 8;
            VariableCol.Name = "VariableCol";
            VariableCol.ReadOnly = true;
            VariableCol.Resizable = DataGridViewTriState.False;
            VariableCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            VariableCol.Width = 80;
            // 
            // ValueCol
            // 
            ValueCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ValueCol.HeaderText = "Value";
            ValueCol.MaxInputLength = 256;
            ValueCol.MinimumWidth = 8;
            ValueCol.Name = "ValueCol";
            ValueCol.Resizable = DataGridViewTriState.False;
            ValueCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // filterTextBox
            // 
            filterTextBox.Location = new Point(21, 73);
            filterTextBox.Margin = new Padding(2);
            filterTextBox.MaxLength = 256;
            filterTextBox.Name = "filterTextBox";
            filterTextBox.PlaceholderText = "Search";
            filterTextBox.Size = new Size(383, 31);
            filterTextBox.TabIndex = 5;
            filterTextBox.WordWrap = false;
            filterTextBox.KeyDown += filterTextBox_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(310, 29);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(223, 25);
            label1.TabIndex = 6;
            label1.Text = "No save file currently open";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1016, 667);
            Controls.Add(BtnGift);
            Controls.Add(BtnAdd);
            Controls.Add(label1);
            Controls.Add(filterTextBox);
            Controls.Add(dataGridView1);
            Controls.Add(BtnOverwrite);
            Controls.Add(BtnOpen);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "Form1";
            Text = "UFO 50 Save Editor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnOpen;
        private Button BtnOverwrite;
        private Button BtnAdd;
        private ColumnHeader VariableHeader;
        private ColumnHeader ValueHeader;
        private DataGridView dataGridView1;
        private TextBox filterTextBox;
        private Label label1;
        private DataGridViewTextBoxColumn VariableCol;
        private DataGridViewTextBoxColumn ValueCol;
        private Button BtnGift;
    }
}
