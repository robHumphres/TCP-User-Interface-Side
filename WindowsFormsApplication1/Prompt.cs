using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Prompt : Form
    {
        public static String ShowDialog()
        {
            Form prompt = new Form();
            prompt.Width = 400;
            prompt.Height = 300;
            Label textAristBox = new Label() { Left = 5, Top = 50, Width = 50, Text = "Enter Username :" };
            TextBox artistBox = new TextBox() { Left = 50, Top = 50, Width = 300 };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 100 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(artistBox);
            prompt.Controls.Add(textAristBox);
            prompt.Controls.Add(confirmation);
            prompt.ShowDialog();
            return artistBox.Text;
        }
    }
}
