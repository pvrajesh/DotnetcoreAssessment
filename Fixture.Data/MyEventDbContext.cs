using Microsoft.EntityFrameworkCore;
using Fixture.Core.Models;
using System.Reflection;
using System;


namespace Fixture.Data
{
    public class EventDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        
        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options)
        {
         
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

        }     



    }
}
