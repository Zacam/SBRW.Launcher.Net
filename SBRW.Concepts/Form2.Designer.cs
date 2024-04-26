namespace SBRW.Concepts
{
    partial class Form2
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
            this.DataGridView_Account_List = new System.Windows.Forms.DataGridView();
            this.Label_ID = new System.Windows.Forms.Label();
            this.TextBox_ID = new System.Windows.Forms.TextBox();
            this.TextBox_Email = new System.Windows.Forms.TextBox();
            this.Label_Email = new System.Windows.Forms.Label();
            this.TextBox_Password = new System.Windows.Forms.TextBox();
            this.Label_Password = new System.Windows.Forms.Label();
            this.Button_Add = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Update = new System.Windows.Forms.Button();
            this.TextBox_Nickname = new System.Windows.Forms.TextBox();
            this.Label_Nickname = new System.Windows.Forms.Label();
            this.TextBox_ID_Account = new System.Windows.Forms.TextBox();
            this.Label_ID_Account = new System.Windows.Forms.Label();
            this.CheckBox_Password_Reveal = new System.Windows.Forms.CheckBox();
            this.TextBox_Min = new System.Windows.Forms.TextBox();
            this.TextBox_Max = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Account_List)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView_Account_List
            // 
            this.DataGridView_Account_List.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_Account_List.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DataGridView_Account_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Account_List.Location = new System.Drawing.Point(12, 159);
            this.DataGridView_Account_List.Name = "DataGridView_Account_List";
            this.DataGridView_Account_List.Size = new System.Drawing.Size(626, 308);
            this.DataGridView_Account_List.TabIndex = 0;
            this.DataGridView_Account_List.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_Account_List_CellClick);
            // 
            // Label_ID
            // 
            this.Label_ID.AutoSize = true;
            this.Label_ID.Location = new System.Drawing.Point(269, 15);
            this.Label_ID.Name = "Label_ID";
            this.Label_ID.Size = new System.Drawing.Size(21, 13);
            this.Label_ID.TabIndex = 1;
            this.Label_ID.Text = "ID:";
            // 
            // TextBox_ID
            // 
            this.TextBox_ID.Enabled = false;
            this.TextBox_ID.Location = new System.Drawing.Point(272, 32);
            this.TextBox_ID.Name = "TextBox_ID";
            this.TextBox_ID.Size = new System.Drawing.Size(242, 20);
            this.TextBox_ID.TabIndex = 2;
            // 
            // TextBox_Email
            // 
            this.TextBox_Email.Location = new System.Drawing.Point(12, 32);
            this.TextBox_Email.Name = "TextBox_Email";
            this.TextBox_Email.Size = new System.Drawing.Size(231, 20);
            this.TextBox_Email.TabIndex = 4;
            // 
            // Label_Email
            // 
            this.Label_Email.AutoSize = true;
            this.Label_Email.Location = new System.Drawing.Point(12, 15);
            this.Label_Email.Name = "Label_Email";
            this.Label_Email.Size = new System.Drawing.Size(35, 13);
            this.Label_Email.TabIndex = 3;
            this.Label_Email.Text = "Email:";
            // 
            // TextBox_Password
            // 
            this.TextBox_Password.Location = new System.Drawing.Point(12, 81);
            this.TextBox_Password.Name = "TextBox_Password";
            this.TextBox_Password.Size = new System.Drawing.Size(231, 20);
            this.TextBox_Password.TabIndex = 6;
            this.TextBox_Password.UseSystemPasswordChar = true;
            // 
            // Label_Password
            // 
            this.Label_Password.AutoSize = true;
            this.Label_Password.Location = new System.Drawing.Point(12, 64);
            this.Label_Password.Name = "Label_Password";
            this.Label_Password.Size = new System.Drawing.Size(56, 13);
            this.Label_Password.TabIndex = 5;
            this.Label_Password.Text = "Password:";
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(272, 73);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(118, 37);
            this.Button_Add.TabIndex = 7;
            this.Button_Add.Text = "Add";
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(520, 73);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(118, 37);
            this.button_Delete.TabIndex = 9;
            this.button_Delete.Text = "Delete";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Update
            // 
            this.button_Update.Location = new System.Drawing.Point(396, 73);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(118, 37);
            this.button_Update.TabIndex = 10;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // TextBox_Nickname
            // 
            this.TextBox_Nickname.Location = new System.Drawing.Point(12, 129);
            this.TextBox_Nickname.Name = "TextBox_Nickname";
            this.TextBox_Nickname.Size = new System.Drawing.Size(231, 20);
            this.TextBox_Nickname.TabIndex = 14;
            // 
            // Label_Nickname
            // 
            this.Label_Nickname.AutoSize = true;
            this.Label_Nickname.Location = new System.Drawing.Point(12, 112);
            this.Label_Nickname.Name = "Label_Nickname";
            this.Label_Nickname.Size = new System.Drawing.Size(58, 13);
            this.Label_Nickname.TabIndex = 13;
            this.Label_Nickname.Text = "Nickname:";
            // 
            // TextBox_ID_Account
            // 
            this.TextBox_ID_Account.Enabled = false;
            this.TextBox_ID_Account.Location = new System.Drawing.Point(520, 32);
            this.TextBox_ID_Account.Name = "TextBox_ID_Account";
            this.TextBox_ID_Account.Size = new System.Drawing.Size(118, 20);
            this.TextBox_ID_Account.TabIndex = 12;
            // 
            // Label_ID_Account
            // 
            this.Label_ID_Account.AutoSize = true;
            this.Label_ID_Account.Location = new System.Drawing.Point(517, 15);
            this.Label_ID_Account.Name = "Label_ID_Account";
            this.Label_ID_Account.Size = new System.Drawing.Size(64, 13);
            this.Label_ID_Account.TabIndex = 11;
            this.Label_ID_Account.Text = "Account ID:";
            // 
            // CheckBox_Password_Reveal
            // 
            this.CheckBox_Password_Reveal.AutoSize = true;
            this.CheckBox_Password_Reveal.Checked = true;
            this.CheckBox_Password_Reveal.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.CheckBox_Password_Reveal.Location = new System.Drawing.Point(250, 85);
            this.CheckBox_Password_Reveal.Name = "CheckBox_Password_Reveal";
            this.CheckBox_Password_Reveal.Size = new System.Drawing.Size(15, 14);
            this.CheckBox_Password_Reveal.TabIndex = 17;
            this.CheckBox_Password_Reveal.UseVisualStyleBackColor = true;
            this.CheckBox_Password_Reveal.CheckedChanged += new System.EventHandler(this.CheckBox_Password_Reveal_CheckedChanged);
            this.CheckBox_Password_Reveal.CheckStateChanged += new System.EventHandler(this.CheckBox_Password_Reveal_CheckedChanged);
            this.CheckBox_Password_Reveal.CausesValidationChanged += new System.EventHandler(this.CheckBox_Password_Reveal_CheckedChanged);
            this.CheckBox_Password_Reveal.Click += new System.EventHandler(this.CheckBox_Password_Reveal_CheckedChanged);
            // 
            // TextBox_Min
            // 
            this.TextBox_Min.Enabled = false;
            this.TextBox_Min.Location = new System.Drawing.Point(272, 129);
            this.TextBox_Min.Name = "TextBox_Min";
            this.TextBox_Min.Size = new System.Drawing.Size(118, 20);
            this.TextBox_Min.TabIndex = 18;
            // 
            // TextBox_Max
            // 
            this.TextBox_Max.Enabled = false;
            this.TextBox_Max.Location = new System.Drawing.Point(396, 129);
            this.TextBox_Max.Name = "TextBox_Max";
            this.TextBox_Max.Size = new System.Drawing.Size(118, 20);
            this.TextBox_Max.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Min:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(396, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Max:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 490);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox_Max);
            this.Controls.Add(this.TextBox_Min);
            this.Controls.Add(this.CheckBox_Password_Reveal);
            this.Controls.Add(this.TextBox_Nickname);
            this.Controls.Add(this.Label_Nickname);
            this.Controls.Add(this.TextBox_ID_Account);
            this.Controls.Add(this.Label_ID_Account);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.Button_Add);
            this.Controls.Add(this.TextBox_Password);
            this.Controls.Add(this.Label_Password);
            this.Controls.Add(this.TextBox_Email);
            this.Controls.Add(this.Label_Email);
            this.Controls.Add(this.TextBox_ID);
            this.Controls.Add(this.Label_ID);
            this.Controls.Add(this.DataGridView_Account_List);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Account_List)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_Account_List;
        private System.Windows.Forms.Label Label_ID;
        private System.Windows.Forms.TextBox TextBox_ID;
        private System.Windows.Forms.TextBox TextBox_Email;
        private System.Windows.Forms.Label Label_Email;
        private System.Windows.Forms.TextBox TextBox_Password;
        private System.Windows.Forms.Label Label_Password;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.TextBox TextBox_Nickname;
        private System.Windows.Forms.Label Label_Nickname;
        private System.Windows.Forms.TextBox TextBox_ID_Account;
        private System.Windows.Forms.Label Label_ID_Account;
        private System.Windows.Forms.CheckBox CheckBox_Password_Reveal;
        private System.Windows.Forms.TextBox TextBox_Min;
        private System.Windows.Forms.TextBox TextBox_Max;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}