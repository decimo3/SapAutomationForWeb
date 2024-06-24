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
          if(!System.IO.File.Exists(program.PRL_LOCKFILE)) continue;
          var solicitacao = System.IO.File.ReadAllText(program.PRL_LOCKFILE, System.Text.Encoding.UTF8);
          if(solicitacao.Length == 0 || solicitacao.Length > 50) continue;
          Console.WriteLine($"{DateTime.Now} - Solicitação recebida: {solicitacao}.");
          var argumentos = solicitacao.Split(' ');
          if(argumentos.Length != 3) throw new IndexOutOfRangeException("A solicitação está malformada! As solicitações devem seguir o formato:\n\n`aplicação` `n. instalação` `n. parceiro`");
          switch (argumentos[0])
          {
            case "fatura":
            case "debito":
              program.SuperTela(argumentos[1], argumentos[2]);
            break;
            default:
              throw new InvalidOperationException($"A solicitação {argumentos[0]} é inválida!\n\nAs aplicações aceitas no momento são: `fatura` e `debito` somente.");
          }
        }
        catch (System.Exception erro)
        {
          System.IO.File.WriteAllText(program.PRL_LOCKFILE, erro.Message, System.Text.Encoding.UTF8);
          Console.WriteLine($"{DateTime.Now} - {erro.Message}.");
          continue;
        }
      }
    }
  }
}