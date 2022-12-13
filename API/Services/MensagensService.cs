using API.Models;
using API.Repository;
using static API.ValidationResult;

namespace API.Services
{
  public class MensagensService
  {
    MensagensRepository mensagensRepository = new();
    
    public MensagensService() { }

    public List<Mensagem> ObterTodas()
    {
      return mensagensRepository.ObterTodas();
    }

    public Mensagem ObterPorId(int id)
    {
      return mensagensRepository.ObterPorId(id);
    }

    public (bool, ValidationResult) Salvar(Mensagem mensagem)
    {
      ValidationResult vr = new();
      bool sucesso = false;


      if(string.IsNullOrEmpty(mensagem.Usuario))
      {
        vr.Tipo = TpMensagem.CampoInvalido;
        vr.Mensagem = "Nome usuário vazio!";

      } 
      else if (string.IsNullOrEmpty(mensagem.Texto))
      {
        vr.Tipo = TpMensagem.CampoInvalido;
        vr.Mensagem = "Texto de mensagem vazio!";
      } 
      else
      {
        string[] palavrasProibidas = { "ProvaMuitoGrande", "C# é Ruim", "Chato", "Bolsonaro", "Lula" };
        foreach (string item in palavrasProibidas)
        {
          mensagem.Texto = mensagem.Texto.Replace(item, "***");
          Console.WriteLine();
        }
        sucesso = mensagensRepository.Salvar(mensagem);
      }
      return (sucesso, vr);
    }

    public (bool sucesso, ValidationResult vr) Excluir(int id)
    {
      ValidationResult vr = new();

      if (mensagensRepository.ObterPorId(id) != null)
      {
        bool sucesso = false;
        if (mensagensRepository.Excluir(id))
        {
          vr.Tipo = TpMensagem.Sucesso;
          vr.Mensagem = "Exclusão realizada!";
          sucesso = true;
        } 
        else
        {
          vr.Tipo = TpMensagem.CampoInvalido;
          vr.Mensagem = "Não foi possível excluir!";
        }
        return (sucesso, vr);
      } 
      else
      {
        vr.Tipo = TpMensagem.CampoInvalido;
        vr.Mensagem = "ID de mensagem inexistente!";
        return (false, vr);
      }
    }
  }
}
