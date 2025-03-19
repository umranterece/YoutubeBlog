using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubBlog.Entity.Entities;

namespace YoutubeBlog.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(100);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var superadmin=new AppUser
            {
                Id = Guid.Parse("CB94223B-CCB8-4F2F-93D7-0DF96A7F065C"),
                UserName = "superadmin@gmail.com",
                NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                PhoneNumber = "+905550001122",
                FirsName = "Admin",
                LastName = "Super",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp=Guid.NewGuid().ToString(),
                ImageId = Guid.Parse("F71F4B9A-AA60-461D-B398-DE31001BF214")
            };
            superadmin.PasswordHash = CreatePasswordHash(superadmin,"123456");
            
            var admin = new AppUser
            {
                Id = Guid.Parse("3AA42229-1C0F-4630-8C1A-DB879ECD0427"),
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "+905350001122",
                FirsName = "Admin",
                LastName = "Standart",
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageId = Guid.Parse("D16A6EC7-8C50-4AB0-89A5-02B9A551F0FA")

            };
            admin.PasswordHash = CreatePasswordHash(admin, "123456");

            //var user = new AppUser
            //{
            //    Id = Guid.Parse("665517F5-E77E-4A24-BFED-2C1616399121"),
            //    UserName = "user@gmail.com",
            //    NormalizedUserName = "USER@GMAIL.COM",
            //    Email = "user@gmail.com",
            //    NormalizedEmail = "USER@GMAIL.COM",
            //    PhoneNumber = "+905450001122",
            //    FirsName = "User",
            //    LastName = "Standart",
            //    PhoneNumberConfirmed = true,
            //    EmailConfirmed = true,
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            //user.PasswordHash = CreatePasswordHash(user, "123456");
            builder.HasData(superadmin,admin);
        }

        private string CreatePasswordHash(AppUser user, string password)
        {
            var passwordhHasher = new PasswordHasher<AppUser>();
            return passwordhHasher.HashPassword(user, password);
        }
    }
}
