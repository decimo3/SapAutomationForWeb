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
    var qnt_passivas = 0;
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
      var referencia = linha.FindElement(By.XPath(".//td[6]")).Text;
      var vencimento = linha.FindElement(By.XPath(".//td[8]")).Text;
      var montante = linha.FindElement(By.XPath(".//td[9]")).Text;
      this.resposta.Append($"{qnt_passivas}ª Fatura\n");
      this.resposta.Append($"Referencia: {referencia}\n");
      this.resposta.Append($"Vencimento: {vencimento}\n");
      this.resposta.Append($"Montante: R$ {montante}\n\n");
      linha.FindElement(By.XPath(".//td[9]/a")).Click();
      System.Threading.Thread.Sleep(this.espera["LONGA"]);
      this.janelas = this.driver.WindowHandles;
      this.driver.SwitchTo().Window(this.janelas[1]);
      this.driver.Manage().Window.Maximize();
      GotoFrame("EmbedFramePDF");
      this.driver.FindElement(By.XPath(caminho["DOWNLOAD_BUTTON"])).Click();
      // var embed = this.driver.FindElement(By.Id("C102"));
      // var url = embed.GetDomAttribute("src");
      // this.driver.Navigate().GoToUrl($"{configuracoes["BASEURL"]}/{url}");
      System.Threading.Thread.Sleep(this.espera["CURTA"]);
      this.driver.Close();
      this.janelas = this.driver.WindowHandles;
      this.driver.SwitchTo().Window(this.janelas[0]);
      apontador++;
      qnt_passivas++;
    } while (apontador <= n_faturas);
    if(qnt_passivas == 0)
    {
      this.resposta.Append($"Não foram encontradas faturas pendentes para a instalação {instalacao}\n\n Verifique os débitos pendentes enviando `pendente` para o chatbot.");
    }
  }
}
