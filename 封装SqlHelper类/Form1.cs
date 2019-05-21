using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 封装SqlHelper类
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OnLoad();
        }
        private void OnLoad() {
            List<Users> list = new List<Users>();
            string sql = "select * from users";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, null)) {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.ID = reader.GetInt32(0);
                        user.Username = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Phone = reader.GetString(3);
                        list.Add(user);
                    }
                }
                dataGridView1.DataSource = list;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Users> list = new List<Users>();
            string sql = "select * from users where username=@username";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@username",SqlDbType.NVarChar,50){Value=username_text.Text.Trim()}
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, pms))
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.ID = reader.GetInt32(0);
                        user.Username = reader.GetString(1) == null ? "--" : reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Phone = reader.GetString(3);
                        list.Add(user);
                    }
                }
                dataGridView1.DataSource = list;
            }
        }
    }
}
