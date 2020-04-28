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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersApps>()
                .HasKey(t => new { t.UserId, t.AppId });

            modelBuilder.Entity<UsersApps>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.apps)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UsersApps>()
                .HasOne(sc => sc.App)
                .WithMany(c => c.users)
                .HasForeignKey(sc => sc.AppId);
        }

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
        //public bool CheckUser(Users u, long a)
        //{
        //    Users.Include(c => c.apps).ThenInclude(sc => sc.App).ToList();
        //    for (int i=0; i<u.apps.Count(); i++)
        //    {
        //        if (u.apps[i].AppId == a)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        public string SetAppsIdForUser( long appid, long userid)
        {
            
            foreach (var u in Users)
            {
                
                if (u.Id == userid)
                {
                    try
                    {
                        u.apps.Add(new UsersApps { AppId = appid, UserId = u.Id });
                        SaveChanges();
                        return "Okey";
                    }
                    catch
                    {
                        return "Error. Item not inserted";
                    }
                    
                }
                
            }

            return "Error. Not Found this UserId";
        }

        public Dictionary<string, List<string>> GetAppsOfUsers()
        {
            Users.Include(c => c.apps).ThenInclude(sc => sc.App).ToList();

            Dictionary<string, List<string>> buf = new Dictionary<string, List<string>>();

            foreach (var c in Users)
            {
                List<string> app_names = new List<string>();
                var app = c.apps.Select(sc => sc.App).ToList();
                foreach (appsItem a in app)
                    app_names.Add(a.Name);

                buf.Add(c.Name, app_names);
            }

            return buf;

        }

        public Dictionary<string, List<string>> GetUsersOfApp()
        {
            appsItems.Include(c => c.users).ThenInclude(sc => sc.User).ToList();

            Dictionary<string, List<string>> buf = new Dictionary<string, List<string>>();

            foreach (var a in appsItems)
            {
                List<string> users_names = new List<string>();
                var us = a.users.Select(sc => sc.User).ToList();
                foreach (Users u in us)
                    users_names.Add(u.Name);

                buf.Add(a.Name, users_names);
            }

            return buf;

        }



    }
}
