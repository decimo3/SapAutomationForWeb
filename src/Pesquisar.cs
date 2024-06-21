using OpenQA.Selenium;
using System.Collections.ObjectModel;
namespace sap_automation;
public partial class Program
{
  private ReadOnlyCollection<String> janelas = new(new List<String>());
  public void Pesquisar(String instalacao, String parceiro)
  {
    this.janelas = this.driver.WindowHandles;
    GotoFrame("WorkAreaFrame1");
    this.driver.FindElement(By.XPath(caminho["PESQUISA_TIPO"])).Click();
    this.driver.FindElement(By.XPath(caminho["PESQUISA_OPCAO_INSTALACAO"])).Click();
    GotoFrame("WorkAreaFrame2");
    this.driver.FindElement(By.XPath(caminho["PESQUISA_TEXTO_INSTALACAO"])).SendKeys(instalacao);
    this.driver.FindElement(By.XPath(caminho["PESQUISA_BOTAO_PESQUISAR"])).Click();
    System.Threading.Thread.Sleep(this.espera["CURTA"]);
    this.janelas = this.driver.WindowHandles;
    this.driver.SwitchTo().Window(this.janelas[1]);
    GotoFrame("ParceiroFrame1");
    this.driver.FindElement(By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PRIMEIRO"])).Click();
    // TODO - Verificar parceiro de negócio
    GotoFrame("ParceiroFrame2");
    this.driver.FindElement(By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PROSSEGUIR"])).Click();
    this.driver.Close();
    System.Threading.Thread.Sleep(this.espera["MEDIA"]);
    this.janelas = this.driver.WindowHandles;
    if(this.janelas.Count != 1)
    {
      this.driver.SwitchTo().Window(this.janelas[1]);
      this.driver.Close();
    }
    this.driver.SwitchTo().Window(this.janelas.First());
  }
}
