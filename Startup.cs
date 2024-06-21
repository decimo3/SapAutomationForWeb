namespace sap_automation;
public class Startup
{
  public static void Main(string[] args)
  {
    using (var program = new Program())
    {
      program.Autenticar();
      while(true)
      {
        try
        {
          Thread.Sleep(10_000);
          if(!System.IO.File.Exists(program.PRL_LOCKFILE)) return;
          var solicitacao = System.IO.File.ReadAllText(program.PRL_LOCKFILE, System.Text.Encoding.UTF8);
          if(solicitacao.Length == 0 || solicitacao.Length > 50) return;
          Console.WriteLine($"{DateTime.Now} - Solicitação recebida: {solicitacao}.");
          program.SuperTela(solicitacao);
        }
        catch (System.Exception erro)
        {
          System.IO.File.WriteAllText(program.PRL_LOCKFILE, erro.Message, System.Text.Encoding.UTF8);
          Console.WriteLine($"{DateTime.Now} - {erro.Message}.");
          return;
        }
      }
    }
  }
}