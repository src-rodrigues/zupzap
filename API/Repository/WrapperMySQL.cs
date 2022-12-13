using MySql.Data.MySqlClient;

namespace API.Repository
{
    public class WrapperMySQL
    {
        public MySqlConnection Conexao { get; set; }
        public MySqlCommand Comando { get; set; }

        public WrapperMySQL()
        {
            string strCon = Environment.GetEnvironmentVariable("StringConexao");

            Conexao = new MySqlConnection(strCon);
            Comando = Conexao.CreateCommand();
        }

        public void Abrir()
        {
            if (Conexao.State != System.Data.ConnectionState.Open)
                Conexao.Open();
        }

        public void Fechar()
        {
            Conexao.Close();
        }
    }

}
