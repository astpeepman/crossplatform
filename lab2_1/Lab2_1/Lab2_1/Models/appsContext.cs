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

        public void SetAppsIdForUser( long appid, long userid)
        {

            foreach (var u in Users)
            {
                if (u.Id == userid)
                {
                    u.apps.Add(new UsersApps { AppId = appid, UserId = u.Id });
                }

            }

            SaveChanges();

            var apps = appsItems.Include(c => c.users).ThenInclude(sc => sc.User).ToList();
            //foreach (var u in Users)
            //{
            //    if (u.Id == userid)
            //    {

            //    }

            //}

            //foreach (var a in appsItems)
            //{
            //    if (a.Id == appid)
            //    {

            //    }
            //}


        }

        public Dictionary<string, List<string>> GetAppsOfUsers()
        {
            Dictionary<string, List<string>> buf = new Dictionary<string, List<string>>();

            

            foreach (var c in Users)
            {
                List<string> app_names = new List<string>();
                Console.WriteLine($"\n Course: {c.Name}");
                // выводим всех студентов для данного кура
                var app = c.apps.Select(sc => sc.App).ToList();
                foreach (appsItem a in app)
                    app_names.Add(a.Name);

                buf.Add(c.Name, app_names);
            }

            return buf;

        }



    }
}
