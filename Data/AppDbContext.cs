using Microsoft.EntityFrameworkCore;
using MecHub.Models;

namespace MecHub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {   
        }
    public DbSet<Cliente> cliente { get; set; } = null!;
    public DbSet<Usuario> usuario {get; set; } = null!;
    public DbSet<Mecanico> mecanico {get; set; } = null!;
    public DbSet<Veiculo> veiculos {get; set; } = null!;
    public DbSet<Servico> servicos {get; set; } = null!;
    public DbSet<StatusOrdem> statusOrdens {get; set; } = null!;
    public DbSet<OrdemServico> ordensServicos {get; set; } = null!;
    public DbSet<ItemOrdemServico> itensOrdemServico {get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Unique
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Cpf)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

             modelBuilder.Entity<Veiculo>()
                .HasIndex(v => v.Placa)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
            .Property(u => u.TipoLogin)
            .HasConversion<string>();

            // 🔗 RELACIONAMENTOS

            // Usuario 1:1 Mecanico
            modelBuilder.Entity<Mecanico>()
                .HasOne(m => m.Usuario)
                .WithOne(u => u.Mecanico)
                .HasForeignKey<Mecanico>(m => m.UsuarioId);

            // Cliente 1:N Veiculo
            modelBuilder.Entity<Veiculo>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Veiculos)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrdemServico -> Cliente
            modelBuilder.Entity<OrdemServico>()
                .HasOne(o => o.Cliente)
                .WithMany()
                .HasForeignKey(o => o.ClienteId);

            // OrdemServico -> Mecanico
            modelBuilder.Entity<OrdemServico>()
                .HasOne(o => o.Mecanico)
                .WithMany(m => m.OrdensServico)
                .HasForeignKey(o => o.MecanicoId);

            // OrdemServico -> Veiculo
            modelBuilder.Entity<OrdemServico>()
                .HasOne(o => o.Veiculo)
                .WithMany(v => v.OrdensServico)
                .HasForeignKey(o => o.VeiculoId);

            // OrdemServico -> Status
            modelBuilder.Entity<OrdemServico>()
                .HasOne(o => o.Status)
                .WithMany(s => s.OrdensServico)
                .HasForeignKey(o => o.StatusId);

            // ItemOrdemServico -> OrdemServico
            modelBuilder.Entity<ItemOrdemServico>()
                .HasOne(i => i.OrdemServico)
                .WithMany(o => o.Itens)
                .HasForeignKey(i => i.OrdemServicoId);

            // ItemOrdemServico -> Servico
            modelBuilder.Entity<ItemOrdemServico>()
                .HasOne(i => i.Servico)
                .WithMany(s => s.Itens)
                .HasForeignKey(i => i.ServicoId);
        }
    }    
}
