namespace Go2MusicStore.Platform.Implementation.DataLayer
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Go2MusicStore.Models;

    public class AlbumStoreInitializer : DropCreateDatabaseIfModelChanges<AlbumStoreContext>
    {
        protected override void Seed(AlbumStoreContext context)
        {
            var genres = new List<Genre>()
                             {
                                 new Genre { Name = "Rock", Description = "Rock music" },
                                 new Genre { Name = "Pop", Description = "Pop music" }
                             };
            genres.ForEach(s => context.Genres.Add(s));
            context.SaveChanges();

            //var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<SchoolContext>()));
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser()
            {
                UserName = "mbullock976@hotmail.com",
                Email = "mbullock976@hotmail.com",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true
            };

            if (!userManager.Users.Any(m => m.UserName.Equals("mbullock976@hotmail.com")))
            {
                userManager.Create(user, "Password1-");
            }

            base.Seed(context);
        }
    }
}