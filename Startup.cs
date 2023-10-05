using System.Diagnostics.Contracts;

namespace sap_automation;
public class Startup
{
  public static void Main(string[] args)
  {
    var program = new Program();
    program.Fatura(args[0]);
  }
}