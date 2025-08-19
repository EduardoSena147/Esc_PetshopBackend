using Microsoft.EntityFrameworkCore;
using Esc_PetshopBackend.Data.Entities;

namespace Esc_PetshopBackend.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nome");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("senha");

                entity.Property(e => e.DataCriacao)
                    .HasColumnName("data_criacao")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()")
                    .HasConversion(
                        v => v, // Ao salvar, já é DateTime (UTC)
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Ao ler, marca como UTC
                    );


                entity.Property(e => e.Ativo)
                    .HasColumnName("ativo")
                    .HasDefaultValue(true);

                entity.Property(e => e.CargoId)
                    .HasColumnName("cargo_id")
                    .IsRequired();

                entity.HasOne(u => u.Cargo)             // Um Usuario tem um Cargo
                    .WithMany(c => c.Usuarios)          // Um Cargo tem muitos Usuarios
                    .HasForeignKey(u => u.CargoId)      // Chave estrangeira
                    .OnDelete(DeleteBehavior.Restrict)  // Impede deletar cargo com usuários
                    .IsRequired();

                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(u => u.CargoId).HasDatabaseName("idx_usuarios_cargo_id");

            });

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.ToTable("cargos"); // Nome da tabela no banco

                entity.HasKey(c => c.Id)
                      .HasName("cargos_pkey"); // Nome da PK

                entity.Property(c => c.Id)
                      .HasColumnName("id") // Nome da coluna
                      .ValueGeneratedOnAdd(); // Auto incremento

                entity.Property(c => c.Descricao)
                      .HasColumnName("descricao") // Nome da coluna
                      .HasColumnType("varchar(50)") // Tipo específico
                      .HasMaxLength(50)
                      .IsRequired();

                // Índice único na descrição (se necessário)
                entity.HasIndex(c => c.Descricao)
                      .IsUnique()
                      .HasDatabaseName("uk_cargos_descricao");

                // Comentários para documentação (opcional)
                entity.HasComment("Tabela de cargos do sistema");
            });
        }
    }
}