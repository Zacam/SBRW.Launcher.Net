namespace GameLauncher
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ServerLabel = new System.Windows.Forms.Label();
            this.ServerDropDownList = new System.Windows.Forms.ComboBox();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.LoginTab = new System.Windows.Forms.TabPage();
            this.ForgotPassLink = new System.Windows.Forms.LinkLabel();
            this.LoginButton = new System.Windows.Forms.Button();
            this.LoginPasswordBox = new System.Windows.Forms.TextBox();
            this.LoginPasswordLabel = new System.Windows.Forms.Label();
            this.LoginEmailBox = new System.Windows.Forms.TextBox();
            this.LoginEmailLabel = new System.Windows.Forms.Label();
            this.RegisterTab = new System.Windows.Forms.TabPage();
            this.RegisterTicketBox = new System.Windows.Forms.TextBox();
            this.RegisterTicketLabel = new System.Windows.Forms.Label();
            this.RegisterConfirmPassword = new System.Windows.Forms.TextBox();
            this.RegisterConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.RegisterPassword = new System.Windows.Forms.TextBox();
            this.RegisterPasswordLabel = new System.Windows.Forms.Label();
            this.RegisterEmail = new System.Windows.Forms.TextBox();
            this.RegisterEmailLabel = new System.Windows.Forms.Label();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ActionText = new System.Windows.Forms.ToolStripStatusLabel();
            this.TabControl1.SuspendLayout();
            this.LoginTab.SuspendLayout();
            this.RegisterTab.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(9, 10);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(74, 13);
            this.ServerLabel.TabIndex = 0;
            this.ServerLabel.Text = "Select Server:";
            // 
            // ServerDropDownList
            // 
            this.ServerDropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerDropDownList.FormattingEnabled = true;
            this.ServerDropDownList.Location = new System.Drawing.Point(90, 6);
            this.ServerDropDownList.Name = "ServerDropDownList";
            this.ServerDropDownList.Size = new System.Drawing.Size(195, 21);
            this.ServerDropDownList.TabIndex = 1;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.LoginTab);
            this.TabControl1.Controls.Add(this.RegisterTab);
            this.TabControl1.Location = new System.Drawing.Point(12, 33);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(275, 191);
            this.TabControl1.TabIndex = 2;
            // 
            // LoginTab
            // 
            this.LoginTab.Controls.Add(this.ForgotPassLink);
            this.LoginTab.Controls.Add(this.LoginButton);
            this.LoginTab.Controls.Add(this.LoginPasswordBox);
            this.LoginTab.Controls.Add(this.LoginPasswordLabel);
            this.LoginTab.Controls.Add(this.LoginEmailBox);
            this.LoginTab.Controls.Add(this.LoginEmailLabel);
            this.LoginTab.Location = new System.Drawing.Point(4, 22);
            this.LoginTab.Name = "LoginTab";
            this.LoginTab.Padding = new System.Windows.Forms.Padding(3);
            this.LoginTab.Size = new System.Drawing.Size(267, 165);
            this.LoginTab.TabIndex = 0;
            this.LoginTab.Text = "Login";
            this.LoginTab.UseVisualStyleBackColor = true;
            // 
            // ForgotPassLink
            // 
            this.ForgotPassLink.AutoSize = true;
            this.ForgotPassLink.Location = new System.Drawing.Point(4, 65);
            this.ForgotPassLink.Name = "ForgotPassLink";
            this.ForgotPassLink.Size = new System.Drawing.Size(107, 13);
            this.ForgotPassLink.TabIndex = 7;
            this.ForgotPassLink.TabStop = true;
            this.ForgotPassLink.Text = "I forgot my password!";
            this.ForgotPassLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ForgotPass_LinkClicked);
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(6, 126);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(253, 32);
            this.LoginButton.TabIndex = 6;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginPasswordBox
            // 
            this.LoginPasswordBox.Location = new System.Drawing.Point(73, 37);
            this.LoginPasswordBox.Name = "LoginPasswordBox";
            this.LoginPasswordBox.PasswordChar = '*';
            this.LoginPasswordBox.Size = new System.Drawing.Size(185, 20);
            this.LoginPasswordBox.TabIndex = 5;
            // 
            // LoginPasswordLabel
            // 
            this.LoginPasswordLabel.AutoSize = true;
            this.LoginPasswordLabel.Location = new System.Drawing.Point(4, 40);
            this.LoginPasswordLabel.Name = "LoginPasswordLabel";
            this.LoginPasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.LoginPasswordLabel.TabIndex = 4;
            this.LoginPasswordLabel.Text = "Password:";
            // 
            // LoginEmailBox
            // 
            this.LoginEmailBox.Location = new System.Drawing.Point(73, 11);
            this.LoginEmailBox.Name = "LoginEmailBox";
            this.LoginEmailBox.Size = new System.Drawing.Size(185, 20);
            this.LoginEmailBox.TabIndex = 3;
            // 
            // LoginEmailLabel
            // 
            this.LoginEmailLabel.AutoSize = true;
            this.LoginEmailLabel.Location = new System.Drawing.Point(4, 14);
            this.LoginEmailLabel.Name = "LoginEmailLabel";
            this.LoginEmailLabel.Size = new System.Drawing.Size(39, 13);
            this.LoginEmailLabel.TabIndex = 2;
            this.LoginEmailLabel.Text = "E-Mail:";
            // 
            // RegisterTab
            // 
            this.RegisterTab.Controls.Add(this.RegisterTicketBox);
            this.RegisterTab.Controls.Add(this.RegisterTicketLabel);
            this.RegisterTab.Controls.Add(this.RegisterConfirmPassword);
            this.RegisterTab.Controls.Add(this.RegisterConfirmPasswordLabel);
            this.RegisterTab.Controls.Add(this.RegisterButton);
            this.RegisterTab.Controls.Add(this.RegisterPassword);
            this.RegisterTab.Controls.Add(this.RegisterPasswordLabel);
            this.RegisterTab.Controls.Add(this.RegisterEmail);
            this.RegisterTab.Controls.Add(this.RegisterEmailLabel);
            this.RegisterTab.Location = new System.Drawing.Point(4, 22);
            this.RegisterTab.Name = "RegisterTab";
            this.RegisterTab.Padding = new System.Windows.Forms.Padding(3);
            this.RegisterTab.Size = new System.Drawing.Size(267, 165);
            this.RegisterTab.TabIndex = 1;
            this.RegisterTab.Text = "Register";
            this.RegisterTab.UseVisualStyleBackColor = true;
            // 
            // RegisterTicketBox
            // 
            this.RegisterTicketBox.Location = new System.Drawing.Point(73, 89);
            this.RegisterTicketBox.Name = "RegisterTicketBox";
            this.RegisterTicketBox.Size = new System.Drawing.Size(185, 20);
            this.RegisterTicketBox.TabIndex = 15;
            // 
            // RegisterTicketLabel
            // 
            this.RegisterTicketLabel.AutoSize = true;
            this.RegisterTicketLabel.Location = new System.Drawing.Point(4, 92);
            this.RegisterTicketLabel.Name = "RegisterTicketLabel";
            this.RegisterTicketLabel.Size = new System.Drawing.Size(40, 13);
            this.RegisterTicketLabel.TabIndex = 14;
            this.RegisterTicketLabel.Text = "Ticket:";
            // 
            // RegisterConfirmPassword
            // 
            this.RegisterConfirmPassword.Location = new System.Drawing.Point(73, 63);
            this.RegisterConfirmPassword.Name = "RegisterConfirmPassword";
            this.RegisterConfirmPassword.PasswordChar = '*';
            this.RegisterConfirmPassword.Size = new System.Drawing.Size(185, 20);
            this.RegisterConfirmPassword.TabIndex = 13;
            // 
            // RegisterConfirmPasswordLabel
            // 
            this.RegisterConfirmPasswordLabel.AutoSize = true;
            this.RegisterConfirmPasswordLabel.Location = new System.Drawing.Point(4, 66);
            this.RegisterConfirmPasswordLabel.Name = "RegisterConfirmPasswordLabel";
            this.RegisterConfirmPasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.RegisterConfirmPasswordLabel.TabIndex = 12;
            this.RegisterConfirmPasswordLabel.Text = "Password:";
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(6, 126);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(253, 32);
            this.RegisterButton.TabIndex = 11;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // RegisterPassword
            // 
            this.RegisterPassword.Location = new System.Drawing.Point(73, 37);
            this.RegisterPassword.Name = "RegisterPassword";
            this.RegisterPassword.PasswordChar = '*';
            this.RegisterPassword.Size = new System.Drawing.Size(185, 20);
            this.RegisterPassword.TabIndex = 10;
            // 
            // RegisterPasswordLabel
            // 
            this.RegisterPasswordLabel.AutoSize = true;
            this.RegisterPasswordLabel.Location = new System.Drawing.Point(4, 40);
            this.RegisterPasswordLabel.Name = "RegisterPasswordLabel";
            this.RegisterPasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.RegisterPasswordLabel.TabIndex = 9;
            this.RegisterPasswordLabel.Text = "Password:";
            // 
            // RegisterEmail
            // 
            this.RegisterEmail.Location = new System.Drawing.Point(73, 11);
            this.RegisterEmail.Name = "RegisterEmail";
            this.RegisterEmail.Size = new System.Drawing.Size(185, 20);
            this.RegisterEmail.TabIndex = 8;
            // 
            // RegisterEmailLabel
            // 
            this.RegisterEmailLabel.AutoSize = true;
            this.RegisterEmailLabel.Location = new System.Drawing.Point(4, 14);
            this.RegisterEmailLabel.Name = "RegisterEmailLabel";
            this.RegisterEmailLabel.Size = new System.Drawing.Size(39, 13);
            this.RegisterEmailLabel.TabIndex = 7;
            this.RegisterEmailLabel.Text = "E-Mail:";
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActionText});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 227);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(299, 22);
            this.StatusStrip1.SizingGrip = false;
            this.StatusStrip1.TabIndex = 3;
            this.StatusStrip1.Text = "statusStrip1";
            // 
            // ActionText
            // 
            this.ActionText.Name = "ActionText";
            this.ActionText.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 249);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.ServerDropDownList);
            this.Controls.Add(this.ServerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameLauncher";
            this.TabControl1.ResumeLayout(false);
            this.LoginTab.ResumeLayout(false);
            this.LoginTab.PerformLayout();
            this.RegisterTab.ResumeLayout(false);
            this.RegisterTab.PerformLayout();
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.ComboBox ServerDropDownList;
        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage LoginTab;
        private System.Windows.Forms.TabPage RegisterTab;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TextBox LoginPasswordBox;
        private System.Windows.Forms.Label LoginPasswordLabel;
        private System.Windows.Forms.TextBox LoginEmailBox;
        private System.Windows.Forms.Label LoginEmailLabel;
        private System.Windows.Forms.StatusStrip StatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ActionText;
        private System.Windows.Forms.TextBox RegisterTicketBox;
        private System.Windows.Forms.Label RegisterTicketLabel;
        private System.Windows.Forms.TextBox RegisterConfirmPassword;
        private System.Windows.Forms.Label RegisterConfirmPasswordLabel;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.TextBox RegisterPassword;
        private System.Windows.Forms.Label RegisterPasswordLabel;
        private System.Windows.Forms.TextBox RegisterEmail;
        private System.Windows.Forms.Label RegisterEmailLabel;
        private System.Windows.Forms.LinkLabel ForgotPassLink;
    }
}

