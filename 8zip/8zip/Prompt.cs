using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8zip
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public static void PerformSomeActionAsAdmin(NetworkCredential adminCredential)
        {
            using (LdapConnection connection = new LdapConnection(@"\\172.28.253.21"))
            {
                NetworkCredential cred = new NetworkCredential(adminCredential.UserName, adminCredential.Password);
                try
                {
                    connection.Credential = cred;
                    DirectoryRequest req = new SearchRequest();
                    connection.SendRequest(req);
                }
                catch (LdapException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
