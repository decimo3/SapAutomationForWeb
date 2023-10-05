using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace sap_automation;
public class Program
{
  private readonly string usuario;
  private readonly string palavra;
  private readonly ChromeOptions options;
  private readonly string caminho;
  private readonly string website;
  private readonly string gchrome;
  public Program()
  {
    try
    {
      var file = System.IO.File.ReadAllLines("Configuration.cfg");
      foreach (var line in file)
      {
        var cfg = line.Split(" ")[0];
        var val = line.Split(" ")[1];
        switch(cfg)
        {
          case "WEBSITE": this.website = val; break;
          case "USUARIO": this.usuario = val; break;
          case "PALAVRA": this.palavra = val; break;
          case "GCHROME": this.gchrome = val; break;
          default: throw new InvalidOperationException("O arquivo de configuração é inválido!");
        }
      }
    }
    catch
    {
      throw new InvalidOperationException("O arquivo de configuração não foi encontrado!");
    }
    this.caminho = @$"{System.IO.Directory.GetCurrentDirectory()}\www";
    this.options = new ChromeOptions();
    this.options.AddArgument($@"--user-data-dir={this.caminho}");
    this.options.AddArgument($@"--app={this.website}");
    this.options.BinaryLocation = this.gchrome;
  }
  public void Fatura(string nota)
  {
    // System.Threading.Thread.Sleep(1000);
    using var driver = new ChromeDriver(options: this.options);
    driver.Manage().Window.Maximize();
    driver.FindElement(By.Id("sap-user")).SendKeys(usuario);
    driver.FindElement(By.Id("sap-password")).SendKeys(palavra);
    driver.FindElement(By.Id("LOGON_BUTTON")).Click();
    driver.FindElement(By.Id("ZAT01C00001")).Click();
    driver.FindElement(By.Id("C6_W24_V25_vnsearchvar_variant")).Click();
    driver.FindElements(By.LinkText("Instalação por contrato"))[0].Click();
    Console.Read();
    driver.Quit();
  }
}