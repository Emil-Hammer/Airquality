﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Airquality
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Compounds> Compounds { get; set; }
        public virtual DbSet<Equipments> Equipments { get; set; }
        public virtual DbSet<Instruments> Instruments { get; set; }
        public virtual DbSet<Measurements> Measurements { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<UTM> UTM { get; set; }
    }
}
