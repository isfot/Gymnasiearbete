﻿namespace Ant_test
{
    partial class MainForm
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.Timer_button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Ant_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.Reset_button = new System.Windows.Forms.Button();
            this.checkBox_random = new System.Windows.Forms.CheckBox();
            this.button_UP = new System.Windows.Forms.Button();
            this.button_RIGHT = new System.Windows.Forms.Button();
            this.button_LEFT = new System.Windows.Forms.Button();
            this.button_DOWN = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.checkBox_Field_1 = new System.Windows.Forms.CheckBox();
            this.checkBox_Field_2 = new System.Windows.Forms.CheckBox();
            this.checkBox_Field_3 = new System.Windows.Forms.CheckBox();
            this.checkBox_Field_4 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Densitet_Textbox = new System.Windows.Forms.RichTextBox();
            this.button_Custom_Flow = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_render_form = new System.Windows.Forms.Button();
            this.button_Data_Form = new System.Windows.Forms.Button();
            this.label_renderTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_fps = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_real = new System.Windows.Forms.CheckBox();
            this.textBox_speed = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(18, 18);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(548, 483);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // Timer_button
            // 
            this.Timer_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Timer_button.Location = new System.Drawing.Point(728, 123);
            this.Timer_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Timer_button.Name = "Timer_button";
            this.Timer_button.Size = new System.Drawing.Size(129, 71);
            this.Timer_button.TabIndex = 1;
            this.Timer_button.Text = "Timer";
            this.Timer_button.UseVisualStyleBackColor = true;
            this.Timer_button.Click += new System.EventHandler(this.Timer_button_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Ant_button
            // 
            this.Ant_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ant_button.Location = new System.Drawing.Point(728, 203);
            this.Ant_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Ant_button.Name = "Ant_button";
            this.Ant_button.Size = new System.Drawing.Size(129, 71);
            this.Ant_button.TabIndex = 2;
            this.Ant_button.Text = "Ant Start";
            this.Ant_button.UseVisualStyleBackColor = true;
            this.Ant_button.Click += new System.EventHandler(this.Ant_button_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(728, 283);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 71);
            this.button1.TabIndex = 3;
            this.button1.Text = "Step";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Steg_Button_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(282, 522);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(283, 64);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(606, 522);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(248, 252);
            this.richTextBox2.TabIndex = 9;
            this.richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox3.Location = new System.Drawing.Point(282, 598);
            this.richTextBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(283, 64);
            this.richTextBox3.TabIndex = 10;
            this.richTextBox3.Text = "";
            // 
            // richTextBox4
            // 
            this.richTextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox4.Location = new System.Drawing.Point(282, 672);
            this.richTextBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(283, 64);
            this.richTextBox4.TabIndex = 11;
            this.richTextBox4.Text = "";
            // 
            // Reset_button
            // 
            this.Reset_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Reset_button.Location = new System.Drawing.Point(728, 363);
            this.Reset_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Reset_button.Name = "Reset_button";
            this.Reset_button.Size = new System.Drawing.Size(129, 71);
            this.Reset_button.TabIndex = 12;
            this.Reset_button.Text = "Restart";
            this.Reset_button.UseVisualStyleBackColor = true;
            this.Reset_button.Click += new System.EventHandler(this.Reset_button_Click);
            // 
            // checkBox_random
            // 
            this.checkBox_random.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_random.AutoSize = true;
            this.checkBox_random.Location = new System.Drawing.Point(577, 330);
            this.checkBox_random.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_random.Name = "checkBox_random";
            this.checkBox_random.Size = new System.Drawing.Size(96, 24);
            this.checkBox_random.TabIndex = 13;
            this.checkBox_random.Text = "Random";
            this.checkBox_random.UseVisualStyleBackColor = true;
            // 
            // button_UP
            // 
            this.button_UP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_UP.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.button_UP.Location = new System.Drawing.Point(93, 566);
            this.button_UP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_UP.Name = "button_UP";
            this.button_UP.Size = new System.Drawing.Size(66, 66);
            this.button_UP.TabIndex = 16;
            this.button_UP.Text = "↑";
            this.button_UP.UseVisualStyleBackColor = true;
            this.button_UP.Click += new System.EventHandler(this.button_UP_Click);
            // 
            // button_RIGHT
            // 
            this.button_RIGHT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RIGHT.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.button_RIGHT.Location = new System.Drawing.Point(158, 631);
            this.button_RIGHT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_RIGHT.Name = "button_RIGHT";
            this.button_RIGHT.Size = new System.Drawing.Size(66, 66);
            this.button_RIGHT.TabIndex = 17;
            this.button_RIGHT.Text = "→ ";
            this.button_RIGHT.UseVisualStyleBackColor = true;
            this.button_RIGHT.Click += new System.EventHandler(this.button_RIGHT_Click);
            // 
            // button_LEFT
            // 
            this.button_LEFT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LEFT.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.button_LEFT.Location = new System.Drawing.Point(27, 631);
            this.button_LEFT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_LEFT.Name = "button_LEFT";
            this.button_LEFT.Size = new System.Drawing.Size(66, 66);
            this.button_LEFT.TabIndex = 18;
            this.button_LEFT.Text = "←";
            this.button_LEFT.UseVisualStyleBackColor = true;
            this.button_LEFT.Click += new System.EventHandler(this.button_LEFT_Click);
            // 
            // button_DOWN
            // 
            this.button_DOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_DOWN.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.button_DOWN.Location = new System.Drawing.Point(93, 698);
            this.button_DOWN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_DOWN.Name = "button_DOWN";
            this.button_DOWN.Size = new System.Drawing.Size(66, 66);
            this.button_DOWN.TabIndex = 19;
            this.button_DOWN.Text = "↓";
            this.button_DOWN.UseVisualStyleBackColor = true;
            this.button_DOWN.Click += new System.EventHandler(this.button_DOWN_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F);
            this.button8.Location = new System.Drawing.Point(104, 642);
            this.button8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(44, 46);
            this.button8.TabIndex = 20;
            this.button8.Text = "Start";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // checkBox_Field_1
            // 
            this.checkBox_Field_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Field_1.AutoSize = true;
            this.checkBox_Field_1.Checked = true;
            this.checkBox_Field_1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Field_1.Location = new System.Drawing.Point(612, 150);
            this.checkBox_Field_1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Field_1.Name = "checkBox_Field_1";
            this.checkBox_Field_1.Size = new System.Drawing.Size(44, 24);
            this.checkBox_Field_1.TabIndex = 21;
            this.checkBox_Field_1.Text = "1";
            this.checkBox_Field_1.UseVisualStyleBackColor = true;
            // 
            // checkBox_Field_2
            // 
            this.checkBox_Field_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Field_2.AutoSize = true;
            this.checkBox_Field_2.Checked = true;
            this.checkBox_Field_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Field_2.Location = new System.Drawing.Point(612, 185);
            this.checkBox_Field_2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Field_2.Name = "checkBox_Field_2";
            this.checkBox_Field_2.Size = new System.Drawing.Size(44, 24);
            this.checkBox_Field_2.TabIndex = 22;
            this.checkBox_Field_2.Text = "2";
            this.checkBox_Field_2.UseVisualStyleBackColor = true;
            // 
            // checkBox_Field_3
            // 
            this.checkBox_Field_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Field_3.AutoSize = true;
            this.checkBox_Field_3.Checked = true;
            this.checkBox_Field_3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Field_3.Location = new System.Drawing.Point(612, 220);
            this.checkBox_Field_3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Field_3.Name = "checkBox_Field_3";
            this.checkBox_Field_3.Size = new System.Drawing.Size(44, 24);
            this.checkBox_Field_3.TabIndex = 23;
            this.checkBox_Field_3.Text = "3";
            this.checkBox_Field_3.UseVisualStyleBackColor = true;
            // 
            // checkBox_Field_4
            // 
            this.checkBox_Field_4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Field_4.AutoSize = true;
            this.checkBox_Field_4.Checked = true;
            this.checkBox_Field_4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Field_4.Location = new System.Drawing.Point(612, 256);
            this.checkBox_Field_4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Field_4.Name = "checkBox_Field_4";
            this.checkBox_Field_4.Size = new System.Drawing.Size(44, 24);
            this.checkBox_Field_4.TabIndex = 24;
            this.checkBox_Field_4.Text = "4";
            this.checkBox_Field_4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F);
            this.button2.Location = new System.Drawing.Point(170, 566);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 54);
            this.button2.TabIndex = 25;
            this.button2.Text = "Trace";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 702);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Timer";
            // 
            // Densitet_Textbox
            // 
            this.Densitet_Textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Densitet_Textbox.Location = new System.Drawing.Point(374, 748);
            this.Densitet_Textbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Densitet_Textbox.Name = "Densitet_Textbox";
            this.Densitet_Textbox.Size = new System.Drawing.Size(148, 61);
            this.Densitet_Textbox.TabIndex = 27;
            this.Densitet_Textbox.Text = "";
            // 
            // button_Custom_Flow
            // 
            this.button_Custom_Flow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Custom_Flow.Location = new System.Drawing.Point(728, 442);
            this.button_Custom_Flow.Name = "button_Custom_Flow";
            this.button_Custom_Flow.Size = new System.Drawing.Size(129, 71);
            this.button_Custom_Flow.TabIndex = 28;
            this.button_Custom_Flow.Text = "Custom Flow";
            this.button_Custom_Flow.UseVisualStyleBackColor = true;
            this.button_Custom_Flow.Click += new System.EventHandler(this.button_Custom_Flow_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(764, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 95);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button_render_form
            // 
            this.button_render_form.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_render_form.Location = new System.Drawing.Point(573, 442);
            this.button_render_form.Name = "button_render_form";
            this.button_render_form.Size = new System.Drawing.Size(129, 71);
            this.button_render_form.TabIndex = 30;
            this.button_render_form.Text = "Render Screen";
            this.button_render_form.UseVisualStyleBackColor = true;
            this.button_render_form.Click += new System.EventHandler(this.button_render_form_Click);
            // 
            // button_Data_Form
            // 
            this.button_Data_Form.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Data_Form.Location = new System.Drawing.Point(573, 365);
            this.button_Data_Form.Name = "button_Data_Form";
            this.button_Data_Form.Size = new System.Drawing.Size(129, 71);
            this.button_Data_Form.TabIndex = 31;
            this.button_Data_Form.Text = "Data Form";
            this.button_Data_Form.UseVisualStyleBackColor = true;
            this.button_Data_Form.Click += new System.EventHandler(this.button_Data_Form_Click);
            // 
            // label_renderTime
            // 
            this.label_renderTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_renderTime.AutoSize = true;
            this.label_renderTime.Location = new System.Drawing.Point(573, 20);
            this.label_renderTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_renderTime.Name = "label_renderTime";
            this.label_renderTime.Size = new System.Drawing.Size(18, 20);
            this.label_renderTime.TabIndex = 32;
            this.label_renderTime.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(644, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "ms";
            // 
            // label_fps
            // 
            this.label_fps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_fps.AutoSize = true;
            this.label_fps.Location = new System.Drawing.Point(574, 54);
            this.label_fps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_fps.Name = "label_fps";
            this.label_fps.Size = new System.Drawing.Size(18, 20);
            this.label_fps.TabIndex = 34;
            this.label_fps.Text = "0";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(644, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 20);
            this.label4.TabIndex = 35;
            this.label4.Text = "fps";
            // 
            // checkBox_real
            // 
            this.checkBox_real.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_real.AutoSize = true;
            this.checkBox_real.Location = new System.Drawing.Point(578, 300);
            this.checkBox_real.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_real.Name = "checkBox_real";
            this.checkBox_real.Size = new System.Drawing.Size(68, 24);
            this.checkBox_real.TabIndex = 36;
            this.checkBox_real.Text = "Real";
            this.checkBox_real.UseVisualStyleBackColor = true;
            // 
            // textBox_speed
            // 
            this.textBox_speed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_speed.Location = new System.Drawing.Point(578, 86);
            this.textBox_speed.Name = "textBox_speed";
            this.textBox_speed.Size = new System.Drawing.Size(100, 26);
            this.textBox_speed.TabIndex = 37;
            this.textBox_speed.Text = "1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 794);
            this.Controls.Add(this.textBox_speed);
            this.Controls.Add(this.checkBox_real);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_fps);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_renderTime);
            this.Controls.Add(this.button_Data_Form);
            this.Controls.Add(this.button_render_form);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_Custom_Flow);
            this.Controls.Add(this.Densitet_Textbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox_Field_4);
            this.Controls.Add(this.checkBox_Field_3);
            this.Controls.Add(this.checkBox_Field_2);
            this.Controls.Add(this.checkBox_Field_1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button_DOWN);
            this.Controls.Add(this.button_LEFT);
            this.Controls.Add(this.button_RIGHT);
            this.Controls.Add(this.button_UP);
            this.Controls.Add(this.checkBox_random);
            this.Controls.Add(this.Reset_button);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Ant_button);
            this.Controls.Add(this.Timer_button);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Trafik simulering";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button Timer_button;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Ant_button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.Button Reset_button;
        private System.Windows.Forms.CheckBox checkBox_random;
        private System.Windows.Forms.Button button_UP;
        private System.Windows.Forms.Button button_RIGHT;
        private System.Windows.Forms.Button button_LEFT;
        private System.Windows.Forms.Button button_DOWN;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox checkBox_Field_1;
        private System.Windows.Forms.CheckBox checkBox_Field_2;
        private System.Windows.Forms.CheckBox checkBox_Field_3;
        private System.Windows.Forms.CheckBox checkBox_Field_4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox Densitet_Textbox;
        private System.Windows.Forms.Button button_Custom_Flow;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_render_form;
        private System.Windows.Forms.Button button_Data_Form;
        private System.Windows.Forms.Label label_renderTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_fps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_real;
        private System.Windows.Forms.TextBox textBox_speed;
    }
}

