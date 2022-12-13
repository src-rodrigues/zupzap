using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TesteController : ControllerBase
  {
    [Route("[action]")]
    public IActionResult Testando()
    {
      return Ok(new
      {
        msg = "Funcionando..."
      });
    }
  }
}
