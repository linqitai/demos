﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Province
    {
        public Province() { }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public override string ToString() {
            return this.Name;
        }
    }
}
