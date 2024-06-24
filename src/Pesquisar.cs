using OpenQA.Selenium;
using System.Collections.ObjectModel;
namespace sap_automation;
public partial class Program
{
  private ReadOnlyCollection<String> janelas = new(new List<String>());
  public void Pesquisar(Int64 instalacao, Int64 parceiro)
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
      var p_txt = this.driver.FindElement(By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PRIMEIRO"])).Text;
      if(p_txt == null || !Int64.TryParse(p_txt, out Int64 p_num))
        throw new InvalidCastException("Não foi encontrado parceiro na lista de parceiros da instalação!");
      if(p_num != parceiro)
        throw new InvalidOperationException("O parceiro encontrado não confere com o solicitado!");
      this.driver.FindElement(By.XPath(caminho["LISTA_PARCEIROS_NEGOCIO_PRIMEIRO"])).Click();
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
