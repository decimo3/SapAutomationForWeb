using OpenQA.Selenium;
namespace sap_automation
{
  public partial class Program
  {
    public void Verificar(Int32 attempt = 0)
    {
      if(attempt > 2)
      {
        resposta.Append("A quantidade de tentativas excedidas! Não foi possível acessar o sistema PRL!");
        System.IO.File.WriteAllText(PRL_LOCKFILE, resposta.ToString());
        System.Environment.Exit(1);
      }
      try
      {
        GotoFrame("TabAreaFrame");
        var elemento = this.driver.FindElement(By.XPath(caminho["TAB_IDENTIFICACAO"]));
        elemento.Click();
        GotoFrame("MIDAREA");
        elemento = this.driver.FindElement(By.XPath(caminho["TRY_NOVA_SESSAO"]));
        elemento.Click();
        Autenticar();
        return;
      }
      catch (System.Exception erro)
      {
        this.driver.Navigate().Refresh();
        System.Threading.Thread.Sleep(espera["MEDIA"]);
        Verificar(attempt++);
      }
    }
  }
}