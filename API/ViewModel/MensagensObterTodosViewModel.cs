namespace API.ViewModel
{
  public class MensagensObterTodosViewModel
  {
    public int Id { get; set; }
    public string Texto { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;
    public string Usuario { get; set; }

    public MensagensObterTodosViewModel() { }
  }
}
