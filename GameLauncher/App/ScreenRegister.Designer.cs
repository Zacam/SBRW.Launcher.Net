
namespace GameLauncher.App
{
    partial class ScreenRegister
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
            this.UserPasswordBox = new System.Windows.Forms.TextBox();
            this.UserConfirmPasswordBox = new System.Windows.Forms.TextBox();
            this.UserTicketBox = new System.Windows.Forms.TextBox();
            this.UserEmailBox = new System.Windows.Forms.TextBox();
            this.UserEmailText = new System.Windows.Forms.Label();
            this.UserPasswordText = new System.Windows.Forms.Label();
            this.UserConfirmPasswordText = new System.Windows.Forms.Label();
            this.UserTicketText = new System.Windows.Forms.Label();
            this.ButtonRegister = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ActionText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserPasswordBox
            // 
            this.UserPasswordBox.Location = new System.Drawing.Point(217, 65);
            this.UserPasswordBox.Name = "UserPasswordBox";
            this.UserPasswordBox.Size = new System.Drawing.Size(306, 20);
            this.UserPasswordBox.TabIndex = 1;
            // 
            // UserConfirmPasswordBox
            // 
            this.UserConfirmPasswordBox.Location = new System.Drawing.Point(217, 102);
            this.UserConfirmPasswordBox.Name = "UserConfirmPasswordBox";
            this.UserConfirmPasswordBox.Size = new System.Drawing.Size(306, 20);
            this.UserConfirmPasswordBox.TabIndex = 2;
            // 
            // UserTicketBox
            // 
            this.UserTicketBox.Location = new System.Drawing.Point(217, 140);
            this.UserTicketBox.Name = "UserTicketBox";
            this.UserTicketBox.Size = new System.Drawing.Size(306, 20);
            this.UserTicketBox.TabIndex = 3;
            // 
            // UserEmailBox
            // 
            this.UserEmailBox.Location = new System.Drawing.Point(217, 27);
            this.UserEmailBox.Name = "UserEmailBox";
            this.UserEmailBox.Size = new System.Drawing.Size(306, 20);
            this.UserEmailBox.TabIndex = 0;
            // 
            // UserEmailText
            // 
            this.UserEmailText.AutoSize = true;
            this.UserEmailText.Location = new System.Drawing.Point(52, 27);
            this.UserEmailText.Name = "UserEmailText";
            this.UserEmailText.Size = new System.Drawing.Size(35, 13);
            this.UserEmailText.TabIndex = 4;
            this.UserEmailText.Text = "Email:";
            // 
            // UserPasswordText
            // 
            this.UserPasswordText.AutoSize = true;
            this.UserPasswordText.Location = new System.Drawing.Point(52, 65);
            this.UserPasswordText.Name = "UserPasswordText";
            this.UserPasswordText.Size = new System.Drawing.Size(56, 13);
            this.UserPasswordText.TabIndex = 5;
            this.UserPasswordText.Text = "Password:";
            // 
            // UserConfirmPasswordText
            // 
            this.UserConfirmPasswordText.AutoSize = true;
            this.UserConfirmPasswordText.Location = new System.Drawing.Point(52, 102);
            this.UserConfirmPasswordText.Name = "UserConfirmPasswordText";
            this.UserConfirmPasswordText.Size = new System.Drawing.Size(94, 13);
            this.UserConfirmPasswordText.TabIndex = 6;
            this.UserConfirmPasswordText.Text = "Confirm Password:";
            // 
            // UserTicketText
            // 
            this.UserTicketText.AutoSize = true;
            this.UserTicketText.Location = new System.Drawing.Point(52, 140);
            this.UserTicketText.Name = "UserTicketText";
            this.UserTicketText.Size = new System.Drawing.Size(40, 13);
            this.UserTicketText.TabIndex = 7;
            this.UserTicketText.Text = "Ticket:";
            // 
            // ButtonRegister
            // 
            this.ButtonRegister.Location = new System.Drawing.Point(422, 188);
            this.ButtonRegister.Name = "ButtonRegister";
            this.ButtonRegister.Size = new System.Drawing.Size(101, 23);
            this.ButtonRegister.TabIndex = 8;
            this.ButtonRegister.Text = "Register";
            this.ButtonRegister.UseVisualStyleBackColor = true;
            this.ButtonRegister.Click += new System.EventHandler(this.ButtonRegister_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(55, 188);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(101, 23);
            this.ButtonCancel.TabIndex = 9;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ActionText
            // 
            this.ActionText.Location = new System.Drawing.Point(55, 223);
            this.ActionText.Name = "ActionText";
            this.ActionText.Size = new System.Drawing.Size(468, 51);
            this.ActionText.TabIndex = 10;
            // 
            // ScreenRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 283);
            this.Controls.Add(this.ActionText);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonRegister);
            this.Controls.Add(this.UserTicketText);
            this.Controls.Add(this.UserConfirmPasswordText);
            this.Controls.Add(this.UserPasswordText);
            this.Controls.Add(this.UserEmailText);
            this.Controls.Add(this.UserTicketBox);
            this.Controls.Add(this.UserConfirmPasswordBox);
            this.Controls.Add(this.UserPasswordBox);
            this.Controls.Add(this.UserEmailBox);
            this.Name = "ScreenRegister";
            this.Text = "ScreenRegister";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox UserPasswordBox;
        private System.Windows.Forms.TextBox UserConfirmPasswordBox;
        private System.Windows.Forms.TextBox UserTicketBox;
        private System.Windows.Forms.TextBox UserEmailBox;
        private System.Windows.Forms.Label UserEmailText;
        private System.Windows.Forms.Label UserPasswordText;
        private System.Windows.Forms.Label UserConfirmPasswordText;
        private System.Windows.Forms.Label UserTicketText;
        private System.Windows.Forms.Button ButtonRegister;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label ActionText;
    }
}