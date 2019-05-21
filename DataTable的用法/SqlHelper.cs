using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTable的用法
{
    public static class SqlHelper
    {
        //定义一个链接字符串
        private static readonly string conStr = ConfigurationManager.ConnectionStrings["mssqlserver"].ConnectionString;

        //1.执行增(insert)、删(delete)、改(update)的方法
        //ExecuteNonQuery
        public static int ExecuteNonQuery(string sql, params SqlParameter[] pms)
        { 
            using(SqlConnection con = new SqlConnection(conStr)){
                using(SqlCommand cmd = new SqlCommand(sql,con)){
                    if (pms != null) {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        //2.执行查询,返回单个值的方法
        //ExecuteScalar()
        public static object ExecuteScalar(string sql, params SqlParameter[] pms)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        //3.执行查询，返回多行，多列的方法
        //ExecuteReader()
        /// <summary>
        /// 获取表中的多行数据（有带参数）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] pms)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                try
                {
                    con.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch {
                    con.Close();
                    con.Dispose();
                    throw;
                }
            }
        }
        /// <summary>
        /// 获取表中的多行数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                try
                {
                    con.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        /// <summary>
        /// 返回一个DataTable的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] pms) {
            DataTable dt = new DataTable();
            using(SqlDataAdapter adapter = new SqlDataAdapter(sql,conStr)){
                if (pms != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(pms);
                }   
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
