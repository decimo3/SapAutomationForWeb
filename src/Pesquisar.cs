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
    Esperar(this.espera["CURTA"], throw_exception: false);
    if(this.driver.Title == "Lista de parceiros")
    {
      var parceiro = Esperar("ParceiroFrame1", By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PRIMEIRO_STATUS"]), this.espera["CURTA"]).First();
      if(!parceiro.GetDomAttribute("src").Contains("LEDG"))
        throw new InvalidOperationException("A instalação não tem cliente vinculado, não é possível processar essa solicitação! Verifique com o controlador!");
      Esperar("ParceiroFrame1", By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PRIMEIRO_CHECK"]), this.espera["CURTA"]).First().Click();
      Esperar("ParceiroFrame2", By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PROSSEGUIR"]), this.espera["CURTA"]).First().Click();
      this.driver.Close();
    }
    System.Threading.Thread.Sleep(this.espera["CURTA"]);
    ClosePopup();
  }
}
