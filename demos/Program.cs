using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demos
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = "Data Source=DELL-PC/LQTDELL;INITIAL CATALOG=demos;integrated Security=true";
            using(SqlConnection con = new SqlConnection(constr)){
                con.Open();
                Console.WriteLine("打开链接成功！！！");
            }
            Console.WriteLine("关闭连接，释放资源！！！");
            Console.ReadKey();
        }
    }
}
