using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTable的用法
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string sql = "select * from test";
            //DataTable dt = 封装SqlHelper类.SqlHelper.ExecuteDataTable(sql, null);
            //this.dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string sql = "select * from test";
            string sql = "select c.id,c.name as '市',p.name as '省' from city as c inner join province as p on c.provincecode=p.code";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            this.dataGridView1.DataSource = dt;
        }
    }
}
