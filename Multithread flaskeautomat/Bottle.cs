using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithread_flaskeautomat
{
    public class Bottle
    { 
        private string bottleName;
        private int bottleNumber;

        public Bottle(string Bottle, int count)
        {
            bottleNumber = count;
            bottleName = Bottle;   
        }

        public int BottleNumber
        {
            get { return bottleNumber; }
            set { bottleNumber = value; }
        }

        public string BottleName
        {
            get { return bottleName; }
            set { bottleName = value; }
        }

        public override string ToString()
        {
            return BottleName + bottleNumber;
        }
    }
}