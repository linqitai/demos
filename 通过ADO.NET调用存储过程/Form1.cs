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

namespace 通过ADO.NET调用存储过程
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int pageIndex = 1;
        private int pageSize = 7;
        private int pageCount;//总页数
        private int recordCount;//总条数
        //窗体加载的时候显示第一页的数据
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            //根据pageIndex来加载数据
            string conStr = "Data Source=DELL-PC\\LQTDELL;Initial Catalog=demos;Integrated Security=True";
            #region 1
            using(SqlConnection conn = new SqlConnection(conStr)){
                string sql ="";
                using(SqlCommand cmd = new SqlCommand(sql,conn)){
                    //告诉SqlCommand对象，现在执行的存储过程不是SQL语句
                    cmd.CommandType = CommandType.StoredProcedure;
                    //增加参数（存储过程中有几个参数，这里就需要增加几个参数）
                    SqlParameter[] pms = new SqlParameter[]{
                        new SqlParameter("@pagesize",SqlDbType.Int){Value = pageSize},
                        new SqlParameter("@pageindex",SqlDbType.Int){Value = pageIndex},
                        new SqlParameter("@recordcount",SqlDbType.Int){Direction=ParameterDirection.Output},
                        new SqlParameter("@pagecount",SqlDbType.Int){Direction=ParameterDirection.Output}
                    };
                    cmd.Parameters.AddRange(pms);
                    //打开链接
                    conn.Open();
                    //执行
                }
            }
            #endregion
            DataTable dt = new DataTable();
            using(SqlDataAdapter adapter = new SqlDataAdapter("ssp_getStudentsByPage",conStr)){
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //增加参数（存储过程中有几个参数，这里就需要增加几个参数）
                SqlParameter[] pms = new SqlParameter[]{
                    new SqlParameter("@pagesize",SqlDbType.Int){Value = pageSize},
                    new SqlParameter("@pageindex",SqlDbType.Int){Value = pageIndex},
                    new SqlParameter("@recordcount",SqlDbType.Int){Direction=ParameterDirection.Output},
                    new SqlParameter("@pagecount",SqlDbType.Int){Direction=ParameterDirection.Output}
                };
                adapter.SelectCommand.Parameters.AddRange(pms);
                adapter.Fill(dt);
                //获取参数并赋值给label
                label1.Text = "总条数:"+pms[2].Value.ToString();
                label2.Text = "总页数:" + pms[3].Value.ToString();
                label3.Text = "当前页:" + pageIndex;
                //数据绑定
                this.dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pageIndex--;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pageIndex++;
            LoadData();
        }
    }
}
