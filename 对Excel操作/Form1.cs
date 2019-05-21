using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 封装SqlHelper类;

namespace 对Excel操作
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Users> user = new List<Users>() { 
                new Users(){Username="lqt",Password="123456",Phone="13958776325"},
                new Users(){Username="lqt1",Password="123456",Phone="13958776322"},
                new Users(){Username="lqt2",Password="123456",Phone="13958776321"}
            };
        }
    }
}
