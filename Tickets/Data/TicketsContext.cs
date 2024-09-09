using Microsoft.EntityFrameworkCore;
using Tickets.Models;

namespace Tickets.Data
{
    public class TicketsContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<TicketEntregue> TicketsEntregue { get; set; }
        public TicketsContext(DbContextOptions<TicketsContext> options)
            : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>()
           .Property(f => f.DataAlteracao)
           .HasColumnType("timestamp without time zone") 
           .ValueGeneratedOnAddOrUpdate() 
           .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<TicketEntregue>()
           .Property(f => f.DataEntrega)
           .HasColumnType("timestamp without time zone")
           .ValueGeneratedOnAddOrUpdate()
           .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Funcionario>()
                .HasIndex(f => f.Cpf)
                .IsUnique()
                .HasDatabaseName("IX_Funcionario_Cpf");           
        }
    }
}
