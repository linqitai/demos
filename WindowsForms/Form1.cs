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

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitialData();
        }
        #region 链接数据库
        public void linkSqlDatabase() {
            string constr = "Data Source=DELL-PC\\LQTDELL;Initial Catalog=demos;Integrated Security=True";// Pooling=false 关闭连接池
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                this.Text = "链接成功~";
            }
        }
        #endregion
        #region 数据库操作语句
        public int ExecuteNonQuery(string sql)
        {
            string constr = "Data Source=DELL-PC\\LQTDELL;Initial Catalog=demos;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int r = cmd.ExecuteNonQuery();//insert\delete\update语句时
                    this.Text = "成功影响了"+ r +"行数据";
                    //cmd.ExecuteScalar();//当执行单个结果时候
                    //cmd.ExecuteReader();//当查询出多行、多结果的时候
                    return r;
                }
            }
        }
        public void ExecuteScalar(string sql)
        {
            string constr = "Data Source=DELL-PC\\LQTDELL;Initial Catalog=demos;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    object o = cmd.ExecuteScalar();//insert\delete\update语句时
                    this.Text = "刚刚插入的记录自动编号是：" + o.ToString();
                    //cmd.ExecuteScalar();//当执行单个结果时候
                    //cmd.ExecuteReader();//当查询出多行、多结果的时候
                }
            }
        }
        #endregion
        #region 数据库查询语句
        public void ExecuteScalar()
        {
            string constr = "Data Source=DELL-PC\\LQTDELL;Initial Catalog=demos;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "select count(*) from test where name='李四'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    //int r = cmd.ExecuteNonQuery();//insert\delete\update语句时
                    object r = cmd.ExecuteScalar();//当执行单个结果时候
                    //cmd.ExecuteReader();//当查询出多行、多结果的时候
                    label1.Text = "表中共有" + r + "条数据";
                }
            }
        }
        public void SqlDataReader()
        {
            string constr = "Data Source=DELL-PC\\LQTDELL;Initial Catalog=demos;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "select * from test";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        if (reader.HasRows)
                        {
                            label1.Text = "";
                            while(reader.Read()){
                                label1.Text += reader.IsDBNull(1) ? "null" : reader.GetString(1) + "\r\t";
                            }
                        }
                        else {
                            label1.Text = "没有查到任何数据！！！";
                        }
                    }
                }
            }
        }
        public void InitialData() {
            List<Person> list = new List<Person>();
            string constr = "Data Source=DELL-PC\\LQTDELL;Initial Catalog=demos;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "select * from test";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Person person = new Person();
                                person.ID = reader.GetInt32(0);
                                person.姓名 = reader.GetString(1);
                                person.年龄 = reader.GetInt32(2);
                                person.性别 = reader.GetString(3);
                                list.Add(person);
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
            dataGridView1.DataSource = list;
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            linkSqlDatabase();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = string.Format("insert into test values('张三4',22,'男')");
            ExecuteNonQuery(sql);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataReader();
            //ExecuteScalar();
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            string name = name_add_text.Text;
            int age = Convert.ToInt32(age_add_text.Text);
            string sex = sex_add_text.Text;
            string sql = string.Format("insert into test output inserted.id values('{0}','{1}','{2}')",name,age,sex);
            ExecuteScalar(sql);
            InitialData();
        }
        //获取当前选中的对象
        DataGridViewRow currentRow;
        Person person;
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //获取当前选中的对象
            currentRow = this.dataGridView1.Rows[e.RowIndex];
            person = currentRow.DataBoundItem as Person;
            if (person != null) {
                name_edit_text.Text = person.姓名;
                age_edit_text.Text = Convert.ToString(person.年龄);
                sex_edit_text.Text = person.性别;
            }
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            string name = name_edit_text.Text.Trim();
            int age = Convert.ToInt32(age_edit_text.Text.Trim());
            string sex = sex_edit_text.Text.Trim();
            string sql = string.Format("update test set name='{0}',age={1},sex='{2}' where id={3}", name, age, sex, person.ID);
            int r = ExecuteNonQuery(sql);
            if (r > 0)
            {
                InitialData();
            }
            else
            {
                this.Text = "Error!";
            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要删除吗？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK) {
                string sql = string.Format("delete from test where id={0}", person.ID);
                int r = ExecuteNonQuery(sql);
                if (r > 0)
                {
                    InitialData();
                }
                else
                {
                    this.Text = "Error!";
                }
            }
            else
            {
                this.Text = "操作失败！";
            }
            
        }
    }
}
