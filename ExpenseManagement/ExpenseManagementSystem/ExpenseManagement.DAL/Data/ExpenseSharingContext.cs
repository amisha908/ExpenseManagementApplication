using ExpenseManagementSystem.Models;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.ExpenseManagement.DAL
{
    public class ExpenseSharingContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseSplit> ExpenseSplits { get; set; }

        public ExpenseSharingContext(DbContextOptions<ExpenseSharingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Members)
                .WithOne(m => m.Group)
                .HasForeignKey(m => m.GroupId);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Expenses)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<Expense>()
                .HasKey(e => e.Id); // Ensure primary key is set


            modelBuilder.Entity<ExpenseSplit>()
                .HasOne(es => es.Expense)
                .WithMany(e => e.ExpenseSplits)
                .HasForeignKey(es => es.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ExpenseSplit>()
                .Property(es => es.Amount)
                .HasColumnType("decimal(18,2)");

            // Ensure precision for Owe and Own fields
            modelBuilder.Entity<ExpenseSplit>()
                .Property(es => es.Owe)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ExpenseSplit>()
                .Property(es => es.Own)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Expenses)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.PaidBy)
                .WithMany()
                .HasForeignKey(e => e.PaidById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExpenseSplit>()
                .HasOne(es => es.Expense)
                .WithMany(e => e.ExpenseSplits)
                .HasForeignKey(es => es.ExpenseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExpenseSplit>()
                .HasOne(es => es.User)
                .WithMany()
                .HasForeignKey(es => es.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            var userID1 = Guid.NewGuid().ToString();
            var userID2 = Guid.NewGuid().ToString();
            var userID3 = Guid.NewGuid().ToString();
            var userID4 = Guid.NewGuid().ToString();
            var userID5 = Guid.NewGuid().ToString();
            var userID6 = Guid.NewGuid().ToString();
            var userID7 = Guid.NewGuid().ToString();
            var userID8 = Guid.NewGuid().ToString();
            var userID9 = Guid.NewGuid().ToString();
            var userID10 = Guid.NewGuid().ToString();
            var userID11 = Guid.NewGuid().ToString();



            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
               new ApplicationUser
               {
                   Id = userID1,
                   Name = "User1",
                   UserName = "user1@test.com",
                   Email = "user1@test.com",
                   NormalizedEmail = "user1@test.com".ToUpper(),
                   NormalizedUserName = "user1@test.com".ToUpper(),
                   PasswordHash = hasher.HashPassword(null, "Pass@123")
               },
                new ApplicationUser
                {
                    Id = userID2,
                    Name = "User2",
                    UserName = "user2@test.com",
                    Email = "user2@test.com",
                    NormalizedEmail = "user2@test.com".ToUpper(),
                    NormalizedUserName = "user2@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                },
               new ApplicationUser
               {
                   Id = userID3,
                   Name = "User3",
                   UserName = "user3@test.com",
                   Email = "user3@test.com",
                   NormalizedEmail = "user3@test.com".ToUpper(),
                   NormalizedUserName = "user3@test.com".ToUpper(),
                   PasswordHash = hasher.HashPassword(null, "Pass@123")
               },
                new ApplicationUser
                {
                    Id = userID4,
                    Name = "User4",
                    UserName = "user4@test.com",
                    Email = "user4@test.com",
                    NormalizedEmail = "user4@test.com".ToUpper(),
                    NormalizedUserName = "user4@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                },
                new ApplicationUser
                {
                    Id = userID5,
                    Name = "User5",
                    UserName = "user5@test.com",
                    Email = "user5@test.com",
                    NormalizedEmail = "user5@test.com".ToUpper(),
                    NormalizedUserName = "user5@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                },
                new ApplicationUser
                {
                    Id = userID6,
                    Name = "User6",
                    UserName = "user6@test.com",
                    Email = "user6@test.com",
                    NormalizedEmail = "user6@test.com".ToUpper(),
                    NormalizedUserName = "user6@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                },
                new ApplicationUser
                {
                    Id = userID7,
                    Name = "User7",
                    UserName = "user7@test.com",
                    Email = "user7@test.com",
                    NormalizedEmail = "user7@test.com".ToUpper(),
                    NormalizedUserName = "user7@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                },
                new ApplicationUser
                {
                    Id = userID8,
                    Name = "User8",
                    UserName = "user8@test.com",
                    Email = "user8@test.com",
                    NormalizedEmail = "user8@test.com".ToUpper(),
                    NormalizedUserName = "user8@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                },
                new ApplicationUser
                {
                    Id = userID9,
                    Name = "User9",
                    UserName = "user9@test.com",
                    Email = "user9@test.com",
                    NormalizedEmail = "user9@test.com".ToUpper(),
                    NormalizedUserName = "user9@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                },
                new ApplicationUser
                {
                    Id = userID10,
                    Name = "User10",
                    UserName = "user10@test.com",
                    Email = "user10@test.com",
                    NormalizedEmail = "user10@test.com".ToUpper(),
                    NormalizedUserName = "user10@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                },
                new ApplicationUser
                {
                    Id = userID11,
                    Name = "Admin",
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    NormalizedEmail = "admin@test.com".ToUpper(),
                    NormalizedUserName = "admin@test.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Pass@123")
                }
           );
            var userRoleId = "af4f00c7-d42d-48ff-8cc5-949cdc43d10e";
            var adminRoleId = "95cb1e1c-d8b6-45a2-b240-6d211c06fd00";
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = null
                },
                new IdentityRole()
                {
                    Id = adminRoleId,
                    Name  = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = null
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            var assignRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = userID1,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID2,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID3,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID4,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID5,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID6,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID7,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID8,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID9,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID10,
                    RoleId = userRoleId
                },
                new()
                {
                    UserId = userID11,
                    RoleId = adminRoleId
                }
                
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(assignRoles);


        }
      
    }
}

