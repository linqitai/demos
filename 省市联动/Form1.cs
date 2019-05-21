using Models;
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

namespace 省市联动
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OnLoadProvince();
        }
        private void OnLoadProvince() {
            string sql = "select * from province";
            //SqlParameter p1 = new SqlParameter(){}
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, null)) {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Province province = new Province();
                        province.Id = reader.GetInt32(0);
                        province.Code = reader.GetString(1);
                        province.Name = reader.GetString(2);
                        comboBox1.Items.Add(province);
                    }
                }
            }
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Province p = comboBox1.SelectedItem as Province;
            status1.Text = p.Name;

            comboBox2.DataSource = getCityList(p.Code);

        }
        private List<City> getCityList(string provincecode)
        {
            List<City> List = new List<City>();
            string sql = "select * from city where provincecode=@provincecode";
            SqlParameter[] p1 = new SqlParameter[]{ 
                new SqlParameter("@provincecode",SqlDbType.VarChar,6){Value=provincecode}
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, p1)) {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        City City = new City();
                        City.Id = reader.GetInt32(0);
                        City.Code = reader.GetString(1);
                        City.Name = reader.GetString(2);
                        City.Provincecode = reader.GetString(3);
                        List.Add(City);
                    }
                }
            }
            return List;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            City city = comboBox2.SelectedItem as City;
            status1.Text = (comboBox1.SelectedItem as Province).Name + city.Name;
        }
    }
}
