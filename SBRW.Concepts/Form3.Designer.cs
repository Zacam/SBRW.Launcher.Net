namespace SBRW.Concepts
{
    partial class Form3
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
            this.Label_Password_Hash = new System.Windows.Forms.Label();
            this.TextBox_Password = new System.Windows.Forms.TextBox();
            this.Label_Password = new System.Windows.Forms.Label();
            this.Button_Add = new System.Windows.Forms.Button();
            this.TextBox_Hash_Key = new System.Windows.Forms.TextBox();
            this.Label_Hash_Key = new System.Windows.Forms.Label();
            this.TextBox_Password_Decrypt = new System.Windows.Forms.TextBox();
            this.Label__Password_Decrypt = new System.Windows.Forms.Label();
            this.TextBox_Password_Hash = new System.Windows.Forms.TextBox();
            this.Button_Decrypt = new System.Windows.Forms.Button();
            this.Button_Clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_Password_Hash
            // 
            this.Label_Password_Hash.AutoSize = true;
            this.Label_Password_Hash.Location = new System.Drawing.Point(46, 103);
            this.Label_Password_Hash.Name = "Label_Password_Hash";
            this.Label_Password_Hash.Size = new System.Drawing.Size(84, 13);
            this.Label_Password_Hash.TabIndex = 20;
            this.Label_Password_Hash.Text = "Password Hash:";
            // 
            // TextBox_Password
            // 
            this.TextBox_Password.Location = new System.Drawing.Point(46, 71);
            this.TextBox_Password.Name = "TextBox_Password";
            this.TextBox_Password.Size = new System.Drawing.Size(231, 20);
            this.TextBox_Password.TabIndex = 19;
            // 
            // Label_Password
            // 
            this.Label_Password.AutoSize = true;
            this.Label_Password.Location = new System.Drawing.Point(46, 54);
            this.Label_Password.Name = "Label_Password";
            this.Label_Password.Size = new System.Drawing.Size(56, 13);
            this.Label_Password.TabIndex = 18;
            this.Label_Password.Text = "Password:";
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(344, 79);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(118, 37);
            this.Button_Add.TabIndex = 23;
            this.Button_Add.Text = "Create Hash";
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // TextBox_Hash_Key
            // 
            this.TextBox_Hash_Key.Location = new System.Drawing.Point(46, 172);
            this.TextBox_Hash_Key.Name = "TextBox_Hash_Key";
            this.TextBox_Hash_Key.Size = new System.Drawing.Size(231, 20);
            this.TextBox_Hash_Key.TabIndex = 25;
            // 
            // Label_Hash_Key
            // 
            this.Label_Hash_Key.AutoSize = true;
            this.Label_Hash_Key.Location = new System.Drawing.Point(46, 155);
            this.Label_Hash_Key.Name = "Label_Hash_Key";
            this.Label_Hash_Key.Size = new System.Drawing.Size(56, 13);
            this.Label_Hash_Key.TabIndex = 24;
            this.Label_Hash_Key.Text = "Hash Key:";
            // 
            // TextBox_Password_Decrypt
            // 
            this.TextBox_Password_Decrypt.Enabled = false;
            this.TextBox_Password_Decrypt.Location = new System.Drawing.Point(344, 172);
            this.TextBox_Password_Decrypt.Name = "TextBox_Password_Decrypt";
            this.TextBox_Password_Decrypt.Size = new System.Drawing.Size(242, 20);
            this.TextBox_Password_Decrypt.TabIndex = 27;
            // 
            // Label__Password_Decrypt
            // 
            this.Label__Password_Decrypt.AutoSize = true;
            this.Label__Password_Decrypt.Location = new System.Drawing.Point(341, 155);
            this.Label__Password_Decrypt.Name = "Label__Password_Decrypt";
            this.Label__Password_Decrypt.Size = new System.Drawing.Size(108, 13);
            this.Label__Password_Decrypt.TabIndex = 26;
            this.Label__Password_Decrypt.Text = "Password Decrypted:";
            // 
            // TextBox_Password_Hash
            // 
            this.TextBox_Password_Hash.Location = new System.Drawing.Point(46, 119);
            this.TextBox_Password_Hash.Name = "TextBox_Password_Hash";
            this.TextBox_Password_Hash.Size = new System.Drawing.Size(231, 20);
            this.TextBox_Password_Hash.TabIndex = 28;
            // 
            // Button_Decrypt
            // 
            this.Button_Decrypt.Location = new System.Drawing.Point(479, 79);
            this.Button_Decrypt.Name = "Button_Decrypt";
            this.Button_Decrypt.Size = new System.Drawing.Size(118, 37);
            this.Button_Decrypt.TabIndex = 29;
            this.Button_Decrypt.Text = "Decrypt Hash";
            this.Button_Decrypt.UseVisualStyleBackColor = true;
            this.Button_Decrypt.Click += new System.EventHandler(this.Button_Decrypt_Click);
            // 
            // Button_Clear
            // 
            this.Button_Clear.Location = new System.Drawing.Point(616, 79);
            this.Button_Clear.Name = "Button_Clear";
            this.Button_Clear.Size = new System.Drawing.Size(118, 37);
            this.Button_Clear.TabIndex = 30;
            this.Button_Clear.Text = "Cleaar";
            this.Button_Clear.UseVisualStyleBackColor = true;
            this.Button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Button_Clear);
            this.Controls.Add(this.Button_Decrypt);
            this.Controls.Add(this.TextBox_Password_Hash);
            this.Controls.Add(this.TextBox_Password_Decrypt);
            this.Controls.Add(this.Label__Password_Decrypt);
            this.Controls.Add(this.TextBox_Hash_Key);
            this.Controls.Add(this.Label_Hash_Key);
            this.Controls.Add(this.Button_Add);
            this.Controls.Add(this.Label_Password_Hash);
            this.Controls.Add(this.TextBox_Password);
            this.Controls.Add(this.Label_Password);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Password_Hash;
        private System.Windows.Forms.TextBox TextBox_Password;
        private System.Windows.Forms.Label Label_Password;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.TextBox TextBox_Hash_Key;
        private System.Windows.Forms.Label Label_Hash_Key;
        private System.Windows.Forms.TextBox TextBox_Password_Decrypt;
        private System.Windows.Forms.Label Label__Password_Decrypt;
        private System.Windows.Forms.TextBox TextBox_Password_Hash;
        private System.Windows.Forms.Button Button_Decrypt;
        private System.Windows.Forms.Button Button_Clear;
    }
}