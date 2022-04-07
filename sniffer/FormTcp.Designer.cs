namespace sniffer
{
    partial class FormTcp
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
            this.label_left = new System.Windows.Forms.Label();
            this.label_right = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Src = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Dst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox_info = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_left
            // 
            this.label_left.ForeColor = System.Drawing.Color.DarkBlue;
            this.label_left.Location = new System.Drawing.Point(31, 22);
            this.label_left.Name = "label_left";
            this.label_left.Size = new System.Drawing.Size(201, 15);
            this.label_left.TabIndex = 8;
            this.label_left.Text = "label1";
            this.label_left.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_right
            // 
            this.label_right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_right.ForeColor = System.Drawing.Color.DeepPink;
            this.label_right.Location = new System.Drawing.Point(281, 22);
            this.label_right.Name = "label_right";
            this.label_right.Size = new System.Drawing.Size(188, 15);
            this.label_right.TabIndex = 8;
            this.label_right.Text = "label1";
            this.label_right.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(238, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "←→";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listView
            // 
            this.listView.AutoArrange = false;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Time,
            this.Src,
            this.Dst,
            this.Length});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(30, 60);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(1004, 277);
            this.listView.TabIndex = 9;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            // 
            // Id
            // 
            this.Id.Text = "Id";
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 210;
            // 
            // Src
            // 
            this.Src.Text = "Src";
            this.Src.Width = 180;
            // 
            // Dst
            // 
            this.Dst.Text = "Dst";
            this.Dst.Width = 180;
            // 
            // Length
            // 
            this.Length.Text = "Length";
            this.Length.Width = 70;
            // 
            // textBox_info
            // 
            this.textBox_info.Location = new System.Drawing.Point(30, 361);
            this.textBox_info.Multiline = true;
            this.textBox_info.Name = "textBox_info";
            this.textBox_info.ReadOnly = true;
            this.textBox_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_info.Size = new System.Drawing.Size(1004, 221);
            this.textBox_info.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 343);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Package Info";
            // 
            // FormTcp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 599);
            this.Controls.Add(this.textBox_info);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.label_right);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_left);
            this.MaximizeBox = false;
            this.Name = "FormTcp";
            this.Text = "Tcp Stream";
            this.Load += new System.EventHandler(this.FormTcp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_left;
        private System.Windows.Forms.Label label_right;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Src;
        private System.Windows.Forms.ColumnHeader Dst;
        private System.Windows.Forms.ColumnHeader Length;
        private System.Windows.Forms.TextBox textBox_info;
        private System.Windows.Forms.Label label4;
    }
}