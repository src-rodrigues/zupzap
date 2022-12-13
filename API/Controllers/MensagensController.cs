using API.Models;
using API.Services;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MensagensController : ControllerBase
  {
    // GET: api/<MensagensController>
    [HttpGet]
    public IActionResult GetAll()
    {
      MensagensService mensagensService = new();
      List<MensagensObterTodosViewModel> mensagensVm = new();
      List<Mensagem> mensagens = mensagensService.ObterTodas();
      mensagens.ForEach(item =>
      {
        mensagensVm.Add(new MensagensObterTodosViewModel()
        {
          Id = item.Id,
          Data = item.Data,
          Texto = item.Texto,
          Usuario = item.Usuario
        });
      });

      return Ok(mensagensVm);
    }

    // GET api/<MensagensController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      MensagensService mensagensService = new();
      Mensagem mensagem = mensagensService.ObterPorId(id);
      return mensagem != null ? Ok(mensagem) : NotFound(new { msg = "Mensagem não encontrada!" });
    }

    // POST api/<MensagensController>
    [HttpPost]
    public IActionResult Post([FromBody] MensagemCriarViewModel mensagemCriarVm)
    {
      Mensagem mensagem = new()
      {
        Id = 0,
        Data = DateTime.Now,
        Texto = mensagemCriarVm.Texto,
        Usuario = mensagemCriarVm.Usuario
      };

      MensagensService mensagensService = new();

      (bool sucesso, ValidationResult vr) = mensagensService.Salvar(mensagem);
      return sucesso ? Ok(mensagem) : BadRequest(vr);
    }


    // DELETE api/<MensagensController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      MensagensService mensagensService = new();
      (bool sucesso, ValidationResult vr) = mensagensService.Excluir(id);

      return sucesso ? Ok(ValidationResult.Sucesso("Mensagem Excluida!")) : BadRequest(vr);
    }
  }
}
