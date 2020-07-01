using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2_1.Models
{
    public class UsersApps
    {
        public long UserId { get; set; }
        public Users User { get; set; }

        public long AppId { get; set; }
        public appsItem App { get; set; }
    }
}
