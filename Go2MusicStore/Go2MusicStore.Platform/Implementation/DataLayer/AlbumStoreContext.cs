using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2MusicStore.Platform.Implementation.DataLayer
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Go2MusicStore.Models;

    public class AlbumStoreContext : IdentityDbContext<ApplicationUser>
    {
        public AlbumStoreContext()
             : base("AlbumStoreContext", throwIfV1Schema: false)
        {            
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<StoreAccount> StoreAccounts { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<CreditCardType> CreditCardTypes { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }

        public static AlbumStoreContext Create()
        {
            return new AlbumStoreContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //        .MapRightKey("InstructorID")
            //        .ToTable("CourseInstructor"));

            ////this is mapping entity to a sql stored proc
            ////this is an example of code-first generating CRUD stored procedures for Department Table
            //modelBuilder.Entity<Department>().MapToStoredProcedures();

            modelBuilder.Entity<StoreAccount>().HasOptional(p => p.ShoppingCart);
            modelBuilder.Entity<ShoppingCart>().HasRequired(p => p.StoreAccount);

            modelBuilder.Entity<ShoppingCart>()
                .Property(x => x.ShoppingCartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<StoreAccount>()
               .HasKey(p => new { p.StoreAccountId, p.UserIdentityName });

            modelBuilder.Entity<StoreAccount>()
                .Property(x => x.StoreAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // the all important base class call! Add this line to make your problems go away.
            base.OnModelCreating(modelBuilder);
        }        
    }
}
