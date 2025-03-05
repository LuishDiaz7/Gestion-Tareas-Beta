using System;

namespace GestionTareasBeta
{
    partial class LoginForm
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
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.txtUserOrEmail = new System.Windows.Forms.TextBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.centerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // txtUserOrEmail
            // 
            this.txtUserOrEmail.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserOrEmail.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUserOrEmail.Location = new System.Drawing.Point(241, 134);
            this.txtUserOrEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserOrEmail.Multiline = true;
            this.txtUserOrEmail.Name = "txtUserOrEmail";
            this.txtUserOrEmail.Size = new System.Drawing.Size(261, 30);
            this.txtUserOrEmail.TabIndex = 0;
            this.txtUserOrEmail.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(8)))), ((int)(((byte)(97)))));
            this.btnLogIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogIn.FlatAppearance.BorderSize = 0;
            this.btnLogIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.btnLogIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.btnLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogIn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.ForeColor = System.Drawing.Color.White;
            this.btnLogIn.Location = new System.Drawing.Point(227, 301);
            this.btnLogIn.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(128, 38);
            this.btnLogIn.TabIndex = 2;
            this.btnLogIn.Text = "Login";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(222, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "Login Account";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPassword.Location = new System.Drawing.Point(241, 222);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(261, 30);
            this.txtPassword.TabIndex = 4;
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.leftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftPanel.Controls.Add(this.pictureBox1);
            this.leftPanel.Controls.Add(this.label5);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(200, 400);
            this.leftPanel.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GestionTareasBeta.Properties.Resources.tarea;
            this.pictureBox1.Location = new System.Drawing.Point(30, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 127);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "Task Manager App";
            // 
            // centerPanel
            // 
            this.centerPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.centerPanel.Controls.Add(this.chkShowPassword);
            this.centerPanel.Controls.Add(this.btnLogIn);
            this.centerPanel.Controls.Add(this.label4);
            this.centerPanel.Controls.Add(this.label3);
            this.centerPanel.Controls.Add(this.txtPassword);
            this.centerPanel.Controls.Add(this.exit);
            this.centerPanel.Controls.Add(this.label1);
            this.centerPanel.Controls.Add(this.txtUserOrEmail);
            this.centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerPanel.Location = new System.Drawing.Point(0, 0);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(575, 400);
            this.centerPanel.TabIndex = 6;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPassword.Location = new System.Drawing.Point(382, 259);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(120, 20);
            this.chkShowPassword.TabIndex = 6;
            this.chkShowPassword.Text = "Show Password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(237, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(237, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Email or Username:";
            // 
            // exit
            // 
            this.exit.AutoSize = true;
            this.exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exit.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.Location = new System.Drawing.Point(543, 9);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(20, 22);
            this.exit.TabIndex = 0;
            this.exit.Text = "X";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(575, 400);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.centerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.centerPanel.ResumeLayout(false);
            this.centerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.TextBox txtUserOrEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel centerPanel;
        private System.Windows.Forms.Label exit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private EventHandler btnLogIn_Click;
    }
}