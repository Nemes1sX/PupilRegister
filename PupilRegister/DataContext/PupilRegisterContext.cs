using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PupilRegister.Models.Entities;

namespace PupilRegister.DataContext
{
    public class PupilRegisterContext : DbContext
    {
        public PupilRegisterContext(DbContextOptions<PupilRegisterContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            PupilRegisterSeed.Seed(builder);
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Parent> Parents { get; set; }
    }
}
