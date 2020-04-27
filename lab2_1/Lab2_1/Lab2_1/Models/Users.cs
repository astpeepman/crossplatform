using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2_1.Models
{
    public class Users
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public bool paid { get; set; }
        public string Phone { get; set; }

        [NotMapped]
        public long[] appsId { get; set; }

        public void SetPhone()
        {
            string buf = "";
            for (int i = 0; i <= 1; i++)
                buf += Phone[i];

            buf += " ";

            for (int i = 2; i <= 4; i++)
                buf += Phone[i];
            buf += " ";

            for (int i = 5; i <= 7; i++)
                buf += Phone[i];
            buf += " ";

            for (int i = 8; i <= 9; i++)
                buf += Phone[i];
            buf += " ";

            for (int i = 10; i <= 11; i++)
                buf += Phone[i];

            Phone = buf;
        }


        //public void SetApps(int appid)
        //{
        //    appsId.Add(appid);
        //}

        //public List<int> GetAppsId()
        //{
        //    return appsId;
        //}

    }

}
