namespace GameLauncher
{
    partial class ScreenLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenLogin));
            this.ServerLabel = new System.Windows.Forms.Label();
            this.ServerDropDownList = new System.Windows.Forms.ComboBox();
            this.ForgotPassLink = new System.Windows.Forms.LinkLabel();
            this.LoginButton = new System.Windows.Forms.Button();
            this.LoginPasswordBox = new System.Windows.Forms.TextBox();
            this.LoginPasswordLabel = new System.Windows.Forms.Label();
            this.LoginEmailBox = new System.Windows.Forms.TextBox();
            this.LoginEmailLabel = new System.Windows.Forms.Label();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ActionText = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.ServerDropDownList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ServerDropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerDropDownList.FormattingEnabled = true;
            this.ServerDropDownList.Location = new System.Drawing.Point(90, 6);
            this.ServerDropDownList.Name = "ServerDropDownList";
            this.ServerDropDownList.Size = new System.Drawing.Size(195, 21);
            this.ServerDropDownList.TabIndex = 1;
            // 
            // ForgotPassLink
            // 
            this.ForgotPassLink.AutoSize = true;
            this.ForgotPassLink.Location = new System.Drawing.Point(20, 97);
            this.ForgotPassLink.Name = "ForgotPassLink";
            this.ForgotPassLink.Size = new System.Drawing.Size(107, 13);
            this.ForgotPassLink.TabIndex = 7;
            this.ForgotPassLink.TabStop = true;
            this.ForgotPassLink.Text = "I forgot my password!";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(23, 123);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(253, 32);
            this.LoginButton.TabIndex = 6;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginPasswordBox
            // 
            this.LoginPasswordBox.Location = new System.Drawing.Point(89, 69);
            this.LoginPasswordBox.Name = "LoginPasswordBox";
            this.LoginPasswordBox.PasswordChar = '*';
            this.LoginPasswordBox.Size = new System.Drawing.Size(185, 20);
            this.LoginPasswordBox.TabIndex = 5;
            // 
            // LoginPasswordLabel
            // 
            this.LoginPasswordLabel.AutoSize = true;
            this.LoginPasswordLabel.Location = new System.Drawing.Point(20, 72);
            this.LoginPasswordLabel.Name = "LoginPasswordLabel";
            this.LoginPasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.LoginPasswordLabel.TabIndex = 4;
            this.LoginPasswordLabel.Text = "Password:";
            // 
            // LoginEmailBox
            // 
            this.LoginEmailBox.Location = new System.Drawing.Point(89, 43);
            this.LoginEmailBox.Name = "LoginEmailBox";
            this.LoginEmailBox.Size = new System.Drawing.Size(185, 20);
            this.LoginEmailBox.TabIndex = 3;
            // 
            // LoginEmailLabel
            // 
            this.LoginEmailLabel.AutoSize = true;
            this.LoginEmailLabel.Location = new System.Drawing.Point(20, 46);
            this.LoginEmailLabel.Name = "LoginEmailLabel";
            this.LoginEmailLabel.Size = new System.Drawing.Size(39, 13);
            this.LoginEmailLabel.TabIndex = 2;
            this.LoginEmailLabel.Text = "E-Mail:";
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(23, 161);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(253, 32);
            this.RegisterButton.TabIndex = 11;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActionText});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 204);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(297, 22);
            this.StatusStrip1.SizingGrip = false;
            this.StatusStrip1.TabIndex = 3;
            this.StatusStrip1.Text = "statusStrip1";
            // 
            // ActionText
            // 
            this.ActionText.Name = "ActionText";
            this.ActionText.Size = new System.Drawing.Size(0, 17);
            // 
            // ScreenLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 226);
            this.Controls.Add(this.ForgotPassLink);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.LoginPasswordBox);
            this.Controls.Add(this.ServerDropDownList);
            this.Controls.Add(this.LoginPasswordLabel);
            this.Controls.Add(this.ServerLabel);
            this.Controls.Add(this.LoginEmailBox);
            this.Controls.Add(this.LoginEmailLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScreenLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SBRW Launcher";
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.ComboBox ServerDropDownList;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TextBox LoginPasswordBox;
        private System.Windows.Forms.Label LoginPasswordLabel;
        private System.Windows.Forms.TextBox LoginEmailBox;
        private System.Windows.Forms.Label LoginEmailLabel;
        private System.Windows.Forms.StatusStrip StatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ActionText;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.LinkLabel ForgotPassLink;
    }
}

