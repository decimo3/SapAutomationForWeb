using OpenQA.Selenium;
namespace sap_automation
{
  public partial class Program
  {
    public IWebElement Esperar(String frame_index, By caminho, Int32 espera)
    {
      var agora = DateTime.Now;
      while(true)
      {
        if((DateTime.Now - agora) > TimeSpan.FromSeconds(espera))
        {
          throw new TimeoutException("O elemento não foi encontrado!");
        }
        try
        {
          GotoFrame(frame_index);
          var elemento = this.driver.FindElements(caminho);
          if(elemento.Any()) return elemento.First();
        }
        catch
        {
          Thread.Sleep(1_000);
          continue;
        }
      }
    }
    public void Esperar(Int32 espera)
    {
      var agora = DateTime.Now;
      while(true)
      {
        if((DateTime.Now - agora) > TimeSpan.FromSeconds(espera))
        {
          throw new TimeoutException("A janela não foi encontrada!");
        }
        var janelas = this.driver.WindowHandles;
        if(janelas.Count == 1)
        {
          System.Threading.Thread.Sleep(1_000);
        }
        else
        {
          this.driver.SwitchTo().Window(this.janelas[1]);
        }
      }
    }
  }
}
