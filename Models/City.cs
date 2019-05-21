using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class City:Province
    {
        private string provincecode;

        public string Provincecode
        {
            get { return provincecode; }
            set { provincecode = value; }
        }
    }
}
