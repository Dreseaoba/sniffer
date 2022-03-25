namespace sniffer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button_device = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_cf = new System.Windows.Forms.TextBox();
            this.button_start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_display = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_df = new System.Windows.Forms.TextBox();
            this.listView = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Src = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Dst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Protocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Device";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(86, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(534, 23);
            this.comboBox1.TabIndex = 1;
            // 
            // button_device
            // 
            this.button_device.Location = new System.Drawing.Point(627, 10);
            this.button_device.Name = "button_device";
            this.button_device.Size = new System.Drawing.Size(75, 25);
            this.button_device.TabIndex = 2;
            this.button_device.Text = "Confirm";
            this.button_device.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Capture filters";
            // 
            // textBox_cf
            // 
            this.textBox_cf.Location = new System.Drawing.Point(28, 69);
            this.textBox_cf.Name = "textBox_cf";
            this.textBox_cf.Size = new System.Drawing.Size(592, 25);
            this.textBox_cf.TabIndex = 4;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(627, 69);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 25);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(708, 69);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 25);
            this.button_stop.TabIndex = 2;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            // 
            // button_display
            // 
            this.button_display.Location = new System.Drawing.Point(627, 124);
            this.button_display.Name = "button_display";
            this.button_display.Size = new System.Drawing.Size(75, 25);
            this.button_display.TabIndex = 2;
            this.button_display.Text = "Display";
            this.button_display.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Display filters";
            // 
            // textBox_df
            // 
            this.textBox_df.Location = new System.Drawing.Point(28, 124);
            this.textBox_df.Name = "textBox_df";
            this.textBox_df.Size = new System.Drawing.Size(592, 25);
            this.textBox_df.TabIndex = 4;
            // 
            // listView
            // 
            this.listView.AutoArrange = false;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Time,
            this.Src,
            this.Dst,
            this.Protocol,
            this.Length});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(28, 165);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(1004, 277);
            this.listView.TabIndex = 5;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(28, 477);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1004, 184);
            this.textBox1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 459);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Package Info";
            // 
            // Id
            // 
            this.Id.Text = "Id";
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 140;
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
            // Protocol
            // 
            this.Protocol.Text = "Protocol";
            this.Protocol.Width = 80;
            // 
            // Length
            // 
            this.Length.Text = "Length";
            this.Length.Width = 70;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 673);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.textBox_df);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_cf);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_display);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_device);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Sniffer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button_device;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_cf;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_display;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_df;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Src;
        private System.Windows.Forms.ColumnHeader Dst;
        private System.Windows.Forms.ColumnHeader Protocol;
        private System.Windows.Forms.ColumnHeader Length;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
    }
}

