using API.Models;
using MySql.Data.MySqlClient;

namespace API.Repository
{
  public class MensagensRepository
  {
    readonly WrapperMySQL MySQL = new();

    public List<Mensagem> ObterTodas()
    {
      List<Mensagem> mensagens = new();
      try
      {
        MySQL.Comando.CommandText = "SELECT * FROM Mensagem";
        MySQL.Abrir();
        MySqlDataReader resultado = MySQL.Comando.ExecuteReader();
        while (resultado.Read())
        {
          mensagens.Add(new Mensagem()
          {
            Id = (int)resultado["Id"],
            Texto = (string)resultado["Texto"],
            Data = (DateTime)resultado["Data"],
            Usuario = (string)resultado["Usuario"]
          });
        }

      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        MySQL.Fechar();
      }
      return mensagens;
    }

    public Mensagem ObterPorId(int id)
    {
      Mensagem mensagem = null;

      try
      {
        MySQL.Comando.CommandText = $"SELECT * FROM Mensagem WHERE id = {id}";
        MySQL.Abrir();
        MySqlDataReader resultado = MySQL.Comando.ExecuteReader();
        if (resultado.Read())
        {
          mensagem = new()
          {
            Id = (int)resultado["Id"],
            Texto = (string)resultado["Texto"],
            Data = (DateTime)resultado["Data"],
            Usuario = (string)resultado["Usuario"]
          };
        }
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        MySQL.Fechar();
      }
      return mensagem;
    }

    public bool Salvar(Mensagem mensagem)
    {
      bool sucesso = false;
      try
      {
        MySQL.Comando.CommandText = $@"INSERT INTO Mensagem (Texto, Data, Usuario) VALUES (@texto, @data, @usuario)";
        MySQL.Comando.Parameters.AddWithValue("@texto", mensagem.Texto);
        MySQL.Comando.Parameters.AddWithValue("@data", mensagem.Data);
        MySQL.Comando.Parameters.AddWithValue("@usuario", mensagem.Usuario);
        MySQL.Abrir();
        int linhasAfetadas = MySQL.Comando.ExecuteNonQuery();
        if(linhasAfetadas > 0)
        {
          mensagem.Id = (int)MySQL.Comando.LastInsertedId;
          sucesso = true;
        }
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        MySQL.Fechar();
      }
      return sucesso;
    }

    public bool Excluir(int id)
    {
      bool sucesso;
      try
      {
        MySQL.Comando.CommandText = $"DELETE FROM Mensagem WHERE Id = {id}";
        MySQL.Abrir();
        sucesso = MySQL.Comando.ExecuteNonQuery() > 0 ? true : false;
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        MySQL.Fechar();
      }
      return sucesso;
    }
  }
}
