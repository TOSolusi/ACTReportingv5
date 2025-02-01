using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Models
{
    public class UserModel
    {
        public string UserNumber { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string UserGroup { get; set; }
        public string UserField1 { get; set; }
        public string UserField2 { get; set; }
        public string CardNo { get; set; }
        public string Name
        {
            get
            {
                return $"{Forename} {Surname}";
            }
        }
    }
}
