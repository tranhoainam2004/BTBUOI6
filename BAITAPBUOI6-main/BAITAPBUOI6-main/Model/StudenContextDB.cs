using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Lab04_01.Model
{
    public partial class StudenContextDB : DbContext
    {
        public StudenContextDB()
            : base("name=Model11")
        {
        }

        public virtual DbSet<FACULTY> FACULTies { get; set; }
        public virtual DbSet<STUDENT> STUDENTs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
