using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInformation.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
: base("name=DbConnectionString")
        {
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().HasKey(t => t.TeacherId); //primary key defination  
            modelBuilder.Entity<Teacher>().Property(t => t.TeacherId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);  //identity col            
            modelBuilder.Entity<Student>().HasKey(s => s.StudentId);
            modelBuilder.Entity<Student>().Property(s => s.StudentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Student>().HasRequired(s => s.Teacher)
                .WithMany(s => s.Students).HasForeignKey(s => s.TeacherId); //Foreign Key             
            base.OnModelCreating(modelBuilder);
        }


    }
}
