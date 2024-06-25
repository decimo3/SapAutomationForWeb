using OpenQA.Selenium;
using System.Collections.ObjectModel;
namespace sap_automation
{
  public partial class Program
  {
    public ReadOnlyCollection<IWebElement> Esperar(String frame_index, By caminho, Int32 espera)
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
          if(elemento.Any()) return elemento;
        }
        catch
        {
          Thread.Sleep(1_000);
          continue;
        }
      }
    }
    public void Esperar(Int32 espera, Boolean throw_exception = true)
    {
      var agora = DateTime.Now;
      while(true)
      {
        if((DateTime.Now - agora) > TimeSpan.FromSeconds(espera))
        {
          if(throw_exception)
          {
            throw new TimeoutException("A janela não foi encontrada!");
          }
          return;
        }
        this.janelas = this.driver.WindowHandles;
        if(this.janelas.Count == 1)
        {
          System.Threading.Thread.Sleep(1_000);
        }
        else
        {
          try
          {
            this.driver.SwitchTo().Window(this.janelas[1]);
            return;
          }
          catch
          {
            System.Threading.Thread.Sleep(1_000);
          }
        }
      }
    }
  }
}
