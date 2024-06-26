using OpenQA.Selenium;
namespace sap_automation
{
  public partial class Program
  {
    public void Verificar(Int32 attempt = 0)
    {
      try
      {
        if(attempt > 2)
        {
          resposta.Append("A quantidade de tentativas excedidas! Não foi possível acessar o sistema PRL!");
          System.IO.File.WriteAllText(PRL_LOCKFILE, resposta.ToString());
          System.Environment.Exit(1);
        }
        ClosePopup();
        var titulo = this.driver.Title;
        switch (this.driver.Title)
        {
          case "Logon":
          case "SAP - [Selecionar uma função do usuário ]":
            Autenticar();
            Verificar(attempt++);
          return;
          // TODO - Caso de desconexão
          case "":
            GotoFrame("MIDAREA");
            this.driver.FindElement(By.XPath(caminho["TRY_NOVA_SESSAO"])).Click();
            Autenticar();
            Verificar(attempt++);
          return;
          case "Interaction Center - [Identificação ]":
          
          return;
          default:
            throw new InvalidOperationException("Tela desconhecida!");
        }
      }
      catch (System.Exception erro)
      {
        Console.WriteLine(erro.Message);
        Console.WriteLine(erro.StackTrace);
        Atualizar();
        Verificar(attempt++);
      }
    }
  }
}