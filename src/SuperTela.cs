using OpenQA.Selenium;
using System.Collections.ObjectModel;
namespace sap_automation;
public partial class Program
{
  public void SuperTela(Int64 instalacao)
  {
    Pesquisar(instalacao);
    GotoFrame("WorkAreaFrame1");
    this.driver.FindElement(By.XPath(caminho["SUPER_TELA"])).Click();
    System.Threading.Thread.Sleep(this.espera["CURTA"]);
    GotoFrame("WorkAreaFrame2");
    var apontador = 1;
    var n_faturas = this.driver.FindElements(By.XPath(caminho["TABELA_DEBITOS_LINHAS"])).Count;
    do
    {
      GotoFrame("WorkAreaFrame2");
      var tabela = this.driver.FindElement(By.XPath(caminho["TABELA_DEBITOS"]));
      var linha = tabela.FindElement(By.XPath(caminho["TABELA_DEBITOS_LINHA"].Replace("_", apontador.ToString())));
      var status = linha.FindElement(By.XPath(".//td[11]")).Text;
      if(status != "Fat. Acumulada" && status != "Fat. Normal")
      {
        apontador++;
        continue;
      }
      linha.FindElement(By.XPath(".//td[9]/a")).Click();
      System.Threading.Thread.Sleep(this.espera["LONGA"]);
      this.janelas = this.driver.WindowHandles;
      this.driver.SwitchTo().Window(this.janelas[1]);
      this.driver.Manage().Window.Maximize();
      GotoFrame("EmbedFramePDF");
      var embed = this.driver.FindElement(By.Id("C102"));
      var url = embed.GetDomAttribute("src");
      this.driver.Navigate().GoToUrl($"http://sspcldb1.light.com.br:8001/{url}");
      System.Threading.Thread.Sleep(this.espera["CURTA"]);
      this.driver.Close();
      this.janelas = this.driver.WindowHandles;
      this.driver.SwitchTo().Window(this.janelas[0]);
      apontador++;
    } while (apontador <= n_faturas);
  }
}
