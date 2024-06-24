using OpenQA.Selenium;
namespace sap_automation
{
  public partial class Program
  {
    public void Atualizar()
    {
      this.driver.Navigate().Refresh();
      System.Threading.Thread.Sleep(espera["CURTA"]);
    }
  }
}