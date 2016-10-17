using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class Utility
    {
        public byte[] GetBytes(string message)
        {
            byte[] toBytes = Encoding.ASCII.GetBytes(message);
            return toBytes;
        }

        public string GetString(byte[] byteParam)
        {
            string toString = Encoding.ASCII.GetString(byteParam);
            return toString;
        }
    }
}
