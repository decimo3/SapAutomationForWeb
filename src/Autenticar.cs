using OpenQA.Selenium;
namespace sap_automation;
public partial class Program
{
  public void Autenticar()
  {
    if(!this.driver.FindElements(By.Id("ZAT01C00001")).Any())
    {
    this.driver.FindElement(By.Id("sap-user")).SendKeys(this.configuracoes["USUARIO"]);
    this.driver.FindElement(By.Id("sap-password")).SendKeys(this.configuracoes["PALAVRA"]);
    this.driver.FindElement(By.Id("LOGON_BUTTON")).Click();
    }
    this.driver.FindElement(By.Id("ZAT01C00001")).Click();
    Esperar("WorkAreaFrame1", By.XPath(caminho["PESQUISA_TIPO"]), espera["LONGA"]);
  }
}