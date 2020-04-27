using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2_1.Models
{
    public class appsItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Free { get; set; }
        public string Autor {get; set;}
        public bool secret { get; set; }

        public List<UsersApps> users { get; set; }

        public appsItem()
        {
            users = new List<UsersApps>();
        }
    }
}
