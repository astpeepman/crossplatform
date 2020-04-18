using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab2_1.Models
{
    public class appsContext : DbContext
    {
        public appsContext(DbContextOptions<appsContext> options)
            : base(options)
        {
        }

        public DbSet<appsItem> appsItems { get; set; }
        public DbSet<Users> Users { get; set; }

        

        public IEnumerable<Users> getUserPaid(IEnumerable<Users> users)
        {
            return
                from user in users
                where user.paid == true
                select user;
        }

        public IEnumerable<appsItem> getapps(IEnumerable<appsItem> apps)
        {
            return
                from a in apps
                where a.secret == false
                select a;
        }

        public void SetAppsIdForUser(IEnumerable<Users> user, int appid, long userid)
        {
            foreach(var u in user)
            {
                if (u.appsId == null)
                {
                    u.appsId = new long[1];
                    u.appsId[0] = appid;
                }
                else
                {
                    long[] buf = u.appsId;
                    Array.Resize(ref buf, buf.Length + 1);
                    buf[buf.Length - 1] = appid;
                    u.appsId = buf;
                }
            }
            SaveChanges();
        }

        public Dictionary<string, List<string>> GetAppsOfUsers(IEnumerable<Users> user, IEnumerable<appsItem> apps)
        {
            Dictionary<string, List<string>> buf = new Dictionary<string, List<string>>();
            
            foreach (var u in user) {
                
                List<string> bufList = new List<string>();
                var result =
                    from i in u.appsId
                    join a in apps
                    on i equals a.Id
                    select new {a.Name};
                foreach(var item in result)
                {
                    bufList.Add(item.Name);
                }
                buf.Add(u.Name, bufList);
             }

            return buf;

        }



    }
}
