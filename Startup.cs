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
          program.Pesquisar("0413645431", String.Empty);
          program.SuperTela();
        }
        catch (System.Exception)
        {
          
        }
      }
    }
  }
}