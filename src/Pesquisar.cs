using OpenQA.Selenium;
using System.Collections.ObjectModel;
namespace sap_automation;
public partial class Program
{
  private ReadOnlyCollection<String> janelas = new(new List<String>());
  public void Pesquisar(Int64 instalacao)
  {
    this.janelas = this.driver.WindowHandles;
    GotoFrame("WorkAreaFrame1");
    this.driver.FindElement(By.XPath(caminho["PESQUISA_TIPO"])).Click();
    this.driver.FindElement(By.XPath(caminho["PESQUISA_OPCAO_INSTALACAO"])).Click();
    GotoFrame("WorkAreaFrame2");
    this.driver.FindElement(By.XPath(caminho["PESQUISA_TEXTO_INSTALACAO"])).SendKeys(instalacao.ToString());
    this.driver.FindElement(By.XPath(caminho["PESQUISA_BOTAO_PESQUISAR"])).Click();
    System.Threading.Thread.Sleep(this.espera["CURTA"]);
    this.janelas = this.driver.WindowHandles;
    this.driver.SwitchTo().Window(this.janelas[1]);
    if(this.driver.Title == "Lista de parceiros")
    {
      GotoFrame("ParceiroFrame1");
      var parceiro = this.driver.FindElement(By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PRIMEIRO_STATUS"]));
      if(!parceiro.GetDomAttribute("src").Contains("LEDG"))
        throw new InvalidOperationException("A instalação não tem cliente vinculado, não é possível processar essa solicitação! Verifique com o controlador!");
      this.driver.FindElement(By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PRIMEIRO_CHECK"])).Click();
      GotoFrame("ParceiroFrame2");
      this.driver.FindElement(By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PROSSEGUIR"])).Click();
      this.driver.Close();
    }
    System.Threading.Thread.Sleep(this.espera["CURTA"]);
    this.janelas = this.driver.WindowHandles;
    if(this.janelas.Count > 1)
    {
      for (int i = 1; i < this.janelas.Count; i++)
      {
        this.driver.SwitchTo().Window(this.janelas[i]);
        this.driver.Close();
      }
    }
    this.driver.SwitchTo().Window(this.janelas.First());
  }
}
