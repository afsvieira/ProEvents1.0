using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Context
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options){ }
        public DbSet<Event> Events { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SpeakerEvent> SpeakerEvents { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<SpeakerEvent>()
                .HasKey(SE => new {SE.EventId, SE.SpeakerId});
            
            modelBuilder.Entity<Event>()
                .HasMany(e => e.SocialMedias)
                .WithOne(sm => sm.Event)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Speaker>()
                .HasMany(s => s.SocialMedias)
                .WithOne(sm => sm.Speaker)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}