using OpenQA.Selenium;
namespace sap_automation
{
  public partial class Program
  {
    public void Encerrar()
    {
      GotoFrame("HeadAreaFrame");
      this.driver.FindElement(By.XPath(caminho["ENCERRAR_BUTTON"])).Click();
      System.IO.File.WriteAllText(PRL_LOCKFILE, resposta.ToString());
      resposta = new();
      return;
    }
  }
}