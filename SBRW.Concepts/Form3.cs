using SBRW.Launcher.Core.Extension.Hash_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBRW.Concepts
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            //string originalString = "MySecretPassword";
            //string encryptionKey = "12345678901234567890123456789012"; // 32 chars for AES-256

            // Encrypting and salting the string
            string saltedEncryptedString = TextBox_Password.Text.Encrypt_AES();
            Console.WriteLine("Salted and Encrypted String: " + saltedEncryptedString);
            TextBox_Password_Hash.Text = saltedEncryptedString;

            // Decrypting the salted and encrypted string
            TextBox_Password_Decrypt.Text = string.Empty;
            string decryptedString = saltedEncryptedString.Decrypt_AES();
            Console.WriteLine("Decrypted String: " + decryptedString);
            TextBox_Password_Decrypt.Text = decryptedString;
        }

        private void Button_Decrypt_Click(object sender, EventArgs e)
        {
            // Decrypting the salted and encrypted string
            TextBox_Password_Decrypt.Text = string.Empty;
            string decryptedString = TextBox_Password_Hash.Text.Decrypt_AES();
            Console.WriteLine("Decrypted String: " + decryptedString);
            TextBox_Password_Decrypt.Text = decryptedString;
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            TextBox_Password.Text = string.Empty;
            TextBox_Password_Hash.Text = string.Empty;
            TextBox_Password_Decrypt.Text = string.Empty;
        }
    }
}
