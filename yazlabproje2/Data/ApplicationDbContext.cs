using Microsoft.EntityFrameworkCore;
using yazlabproje2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using yazlabproje2.Data;

namespace yazlabproje2.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<yazlabproje2.Models.User> User { get; set; } = default!;
        public DbSet<yazlabproje2.Models.Admin> Admin { get; set; }
        public DbSet<yazlabproje2.Models.Trainer> Trainer { get; set; }
        public DbSet<yazlabproje2.Models.UserInfo> UserInfo { get; set; }
        public DbSet<yazlabproje2.Models.Programs> Programs { get; set; }
        public DbSet<yazlabproje2.Models.Nutrition> Nutrition { get; set; }

    }
}