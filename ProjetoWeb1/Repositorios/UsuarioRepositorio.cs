using MySql.Data.MySqlClient;
using ProjetoWeb1.Interfaces;
using ProjetoWeb1.Models;

namespace ProjetoWeb1.Repositorios
{
    //Classe que implementa a interface IUsuarioRepositorio(contrato de métodos)
    public class UsuarioRepositorio(IConfiguration config) : IUsuarioRepositoriocs // herança (: indica a herança)
    {
        // variavel privada e somente leitura para armazenar a string de conexão
        private readonly string _connectionString = config.GetConnectionString("Conexão");

        //método que valida se o usuario existe no banco com base em email e senha
        public LoginViewModel Validar(string email, string senha)
        {
            //cria a conexão com o banco de dados MySql, o using garante que ela seja fechada automaticamente
            using var conn = new MySqlConnection(_connectionString);
            //abre a conexão com o banco de dados
            conn.Open();
            //Define a string do sql usando parametros (@) evita ataques sql injection
            var sql = "SELECT * FROM Usuarios WHERE Email =@email AND Senha =@senha";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new LoginViewModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Nivel = reader["Nivel"].ToString()!
                };
                return null;
            }

        }
    }
}