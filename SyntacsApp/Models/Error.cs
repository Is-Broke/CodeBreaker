using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyntacsApp.Models
{
    //PLACEHOLDER CLASS
    public class Error
    {
        public int ID { get; set; }
        public int ErrorCategoryID { get; set; }
        public string CodeExample { get; set; }
        public string DetailedName { get; set; }
        public int Vote { get; set; }
        public int Rating { get; set; }
    }
}
