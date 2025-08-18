using Dapper;
using System.Data;
using Esc_PetshopBackend.Data.Dapper;
using Esc_PetshopBackend.Data.Entities;

namespace Esc_PetshopBackend.Data.Repositories
{
    public class UsuarioDapperRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public UsuarioDapperRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Usuario>("SELECT * FROM usuarios");
            }
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM usuarios WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<Usuario> GetByUsernameAsync(string username)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM usuarios WHERE username = @Username", new { Username = username });
            }
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM usuarios WHERE email = @Email", new { Email = email });
            }
        }

        public async Task<int> AddAsync(Usuario usuario)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = @"INSERT INTO usuarios (username, nome, email, senha) 
                            VALUES (@Username, @Nome, @Email, @Senha) 
                            RETURNING id";

                return await connection.ExecuteScalarAsync<int>(sql, usuario);
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = @"UPDATE usuarios 
                            SET username = @Username, nome = @Nome, 
                                email = @Email, senha = @Senha 
                            WHERE id = @Id";

                await connection.ExecuteAsync(sql, usuario);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "DELETE FROM usuarios WHERE id = @Id", new { Id = id });
            }
        }
    }
}