namespace Trafikdata_Kompilering
{
    partial class TrafikForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_filename = new System.Windows.Forms.TextBox();
            this.textBox_total = new System.Windows.Forms.TextBox();
            this.Total_label = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.Color.Yellow;
            this.panel1.Location = new System.Drawing.Point(12, 338);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            // 
            // textBox_filename
            // 
            this.textBox_filename.Location = new System.Drawing.Point(12, 312);
            this.textBox_filename.Name = "textBox_filename";
            this.textBox_filename.Size = new System.Drawing.Size(200, 20);
            this.textBox_filename.TabIndex = 1;
            // 
            // textBox_total
            // 
            this.textBox_total.Location = new System.Drawing.Point(52, 16);
            this.textBox_total.Name = "textBox_total";
            this.textBox_total.Size = new System.Drawing.Size(100, 20);
            this.textBox_total.TabIndex = 2;
            // 
            // Total_label
            // 
            this.Total_label.AutoSize = true;
            this.Total_label.Location = new System.Drawing.Point(12, 19);
            this.Total_label.Name = "Total_label";
            this.Total_label.Size = new System.Drawing.Size(34, 13);
            this.Total_label.TabIndex = 3;
            this.Total_label.Text = "Total:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(420, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(368, 426);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TrafikForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Total_label);
            this.Controls.Add(this.textBox_total);
            this.Controls.Add(this.textBox_filename);
            this.Controls.Add(this.panel1);
            this.Name = "TrafikForm";
            this.Text = "Trafik Kompilering";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_filename;
        private System.Windows.Forms.TextBox textBox_total;
        private System.Windows.Forms.Label Total_label;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
    }
}

