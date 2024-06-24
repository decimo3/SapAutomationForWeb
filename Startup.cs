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
          if(!Int64.TryParse(argumentos[1], out Int64 instalacao)) throw new InvalidCastException("A instalação não é um número!");
          if(!Int64.TryParse(argumentos[2], out Int64 parceiro)) throw new InvalidCastException("O parceiro não é um número!");
          program.Verificar();
          switch (argumentos[0])
          {
            case "fatura":
            case "debito":
              program.SuperTela(instalacao, parceiro);
            break;
            default:
              throw new InvalidOperationException($"A solicitação {argumentos[0]} é inválida!\n\nAs aplicações aceitas no momento são: `fatura` e `debito` somente.");
          }
          program.Encerrar();
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