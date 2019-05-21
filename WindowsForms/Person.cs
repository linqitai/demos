using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms
{
    public class Person
    {
        public Person() { }
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string 姓名
        {
            get { return name; }
            set { name = value; }
        }
        private int age;

        public int 年龄
        {
            get { return age; }
            set { age = value; }
        }
        private string sex;

        public string 性别
        {
            get { return sex; }
            set { sex = value; }
        }
    }
}
