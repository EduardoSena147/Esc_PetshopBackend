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
        public DbSet<TipoAnimal> TiposAnimais { get; set; }
        public DbSet<Cliente> Clientes{ get; set; } 
        public DbSet<Pet> Pets { get; set; }

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

            modelBuilder.Entity<TipoAnimal>(entity =>
            {
                entity.ToTable("tipo_animais");

                entity.HasKey(t => t.Id)
                      .HasName("tipo_animais_pkey");

                entity.Property(t => t.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(t => t.Descricao)
                      .HasColumnName("descricao")
                      .HasColumnType("varchar(50)")
                      .HasMaxLength(50)
                      .IsRequired();

                // Índice único na descrição
                entity.HasIndex(t => t.Descricao)
                      .IsUnique()
                      .HasDatabaseName("uk_tipo_animais_descricao");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(c => c.UsuarioId)
                      .HasColumnName("usuario_id")
                      .IsRequired();

                entity.Property(c => c.Cpf)
                      .HasColumnName("cpf")
                      .HasMaxLength(14);

                entity.Property(c => c.Telefone)
                      .HasColumnName("telefone")
                      .HasMaxLength(15);

                entity.Property(c => c.DataNascimento)
                      .HasColumnName("data_nascimento")
                      .HasColumnType("date");

                entity.Property(c => c.Cep)
                      .HasColumnName("cep")
                      .HasMaxLength(9);

                entity.Property(c => c.Endereco)
                      .HasColumnName("endereco")
                      .HasMaxLength(200);

                entity.Property(c => c.Numero)
                      .HasColumnName("numero")
                      .HasMaxLength(10);

                entity.Property(c => c.Complemento)
                      .HasColumnName("complemento")
                      .HasMaxLength(100);

                entity.Property(c => c.Bairro)
                      .HasColumnName("bairro")
                      .HasMaxLength(100);

                entity.Property(c => c.Cidade)
                      .HasColumnName("cidade")
                      .HasMaxLength(100);

                entity.Property(c => c.Estado)
                      .HasColumnName("estado")
                      .HasMaxLength(2);

                entity.Property(c => c.CadastroPendente)
                      .HasColumnName("cadastro_pendente")
                      .HasDefaultValue(true);

                // Relacionamento com Usuario
                entity.HasOne(c => c.Usuario)
                      .WithMany()
                      .HasForeignKey(c => c.UsuarioId)
                      .HasConstraintName("fk_clientes_usuarios")
                      .OnDelete(DeleteBehavior.Cascade);

                // Índices
                entity.HasIndex(c => c.UsuarioId).IsUnique();
                entity.HasIndex(c => c.Cpf).IsUnique();
                entity.HasIndex(c => c.Telefone);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("pets");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                      .HasColumnName("id");

                entity.Property(p => p.ClienteId)
                      .HasColumnName("cliente_id")
                      .IsRequired();

                entity.Property(p => p.TipoAnimalId)
                      .HasColumnName("tipo_animal_id")
                      .IsRequired();

                entity.Property(p => p.Nome)
                      .HasColumnName("nome")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(p => p.Raca)
                      .HasColumnName("raca")
                      .HasMaxLength(100);

                entity.Property(p => p.Cor)
                      .HasColumnName("cor")
                      .HasMaxLength(50);

                entity.Property(p => p.Sexo)
                      .HasColumnName("sexo")
                      .HasMaxLength(1);

                entity.Property(p => p.DataNascimento)
                      .HasColumnName("data_nascimento")
                      .HasColumnType("date");

                entity.Property(p => p.Peso)
                      .HasColumnName("peso")
                      .HasColumnType("decimal(5,2)");

                entity.Property(p => p.Porte)
                      .HasColumnName("porte")
                      .HasMaxLength(20);

                entity.Property(p => p.Castrado)
                      .HasColumnName("castrado")
                      .HasDefaultValue(false);

                entity.Property(p => p.Observacoes)
                      .HasColumnName("observacoes");

                entity.Property(p => p.DataCadastro)
                      .HasColumnName("data_cadastro")
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
                      .HasConversion(
                          v => v.ToUniversalTime(), 
                          v => DateTime.SpecifyKind(v, DateTimeKind.Utc) 
                      );

                entity.Property(p => p.Ativo)
                      .HasColumnName("ativo")
                      .HasDefaultValue(true);

                // Relacionamentos
                entity.HasOne(p => p.Cliente)
                      .WithMany()
                      .HasForeignKey(p => p.ClienteId)
                      .HasConstraintName("fk_pet_cliente")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.TipoAnimal)
                      .WithMany()
                      .HasForeignKey(p => p.TipoAnimalId)
                      .HasConstraintName("fk_pet_tipo_animal")
                      .OnDelete(DeleteBehavior.Restrict);

                // Índices
                entity.HasIndex(p => p.ClienteId);
                entity.HasIndex(p => p.TipoAnimalId);
                entity.HasIndex(p => p.Nome);
            });
        }
    }
}