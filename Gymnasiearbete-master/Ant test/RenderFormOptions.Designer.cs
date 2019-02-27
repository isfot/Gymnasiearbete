namespace Ant_test
{
    partial class RenderFormOptions
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
            this.button_Fullscreen = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_Normal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Fullscreen
            // 
            this.button_Fullscreen.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button_Fullscreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Fullscreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.button_Fullscreen.Location = new System.Drawing.Point(12, 12);
            this.button_Fullscreen.Name = "button_Fullscreen";
            this.button_Fullscreen.Size = new System.Drawing.Size(187, 93);
            this.button_Fullscreen.TabIndex = 0;
            this.button_Fullscreen.Text = "Fullscreen";
            this.button_Fullscreen.UseVisualStyleBackColor = false;
            this.button_Fullscreen.Click += new System.EventHandler(this.button_Fullscreen_Click);
            // 
            // button_Close
            // 
            this.button_Close.BackColor = System.Drawing.Color.Red;
            this.button_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.button_Close.Location = new System.Drawing.Point(205, 12);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(131, 59);
            this.button_Close.TabIndex = 1;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = false;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_Normal
            // 
            this.button_Normal.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button_Normal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Normal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.button_Normal.Location = new System.Drawing.Point(12, 111);
            this.button_Normal.Name = "button_Normal";
            this.button_Normal.Size = new System.Drawing.Size(187, 93);
            this.button_Normal.TabIndex = 2;
            this.button_Normal.Text = "Normal";
            this.button_Normal.UseVisualStyleBackColor = false;
            this.button_Normal.Click += new System.EventHandler(this.button_Normal_Click);
            // 
            // RenderFormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(443, 249);
            this.Controls.Add(this.button_Normal);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_Fullscreen);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RenderFormOptions";
            this.Text = "RenderFormOptions";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RenderFormOptions_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Fullscreen;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Button button_Normal;
    }
}