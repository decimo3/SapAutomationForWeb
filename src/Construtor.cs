using OpenQA.Selenium.Chrome;
namespace sap_automation;
public partial class Program : IDisposable
{
  private readonly ChromeDriver driver;
  private readonly ChromeOptions options;
  private readonly ChromeDriverService service;
  private readonly Boolean is_development = false;
  private readonly Dictionary<String, String> configuracoes = new();
  private readonly Dictionary<String, String> caminho = new();
  private readonly Dictionary<String, Int32> espera = new()
  {
    {"CURTA", 3_000},
    {"MEDIA", 6_000},
    {"LONGA", 10_000},
    {"TOTAL", 30_000},
  };
  public Program()
  {
    is_development = System.Environment.GetCommandLineArgs().Contains("debug");
    if(System.Environment.GetCommandLineArgs().Contains("slower"))
    {
      this.espera["CURTA"] *= 2;
      this.espera["MEDIA"] *= 2;
      this.espera["LONGA"] *= 2;
      this.espera["TOTAL"] *= 2;
    }
    if(System.Environment.GetCommandLineArgs().Contains("faster"))
    {
      this.espera["CURTA"] /= 2;
      this.espera["MEDIA"] /= 2;
      this.espera["LONGA"] /= 2;
      this.espera["TOTAL"] /= 2;
    }
    this.service = is_development ?
      ChromeDriverService.CreateDefaultService() :
      ChromeDriverService.CreateDefaultService(System.IO.Directory.GetCurrentDirectory());
    this.configuracoes = ArquivoConfiguracao("prl.conf", ' ');
    this.caminho = ArquivoConfiguracao("prl.path", '=');
    var caminho = $"{System.IO.Directory.GetCurrentDirectory()}\\www";
    var temporario = $"{System.IO.Directory.GetCurrentDirectory()}\\tmp";
    this.options = new ChromeOptions();
    this.options.AddArgument($@"--user-data-dir={caminho}");
    this.options.AddArgument($@"--app={configuracoes["WEBSITE"]}");
    this.options.AddUserProfilePreference("download.prompt_for_download", false);
    this.options.AddUserProfilePreference("download.directory_upgrade", true);
    this.options.AddUserProfilePreference("download.default_directory", temporario);
    this.options.AddUserProfilePreference("savefile.default_directory", temporario);
    this.options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
    this.options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
    var impressora = "{'recentDestinations':[{'id':'Save as PDF','origin':'local','account': '',}],'selectedDestinationId':'Save as PDF','version':2}";
    this.options.AddUserProfilePreference("printing.print_preview_sticky_settings.appState", impressora);
    this.options.AddArgument("--kiosk-printing");
    this.options.BinaryLocation = configuracoes["GCHROME"];
    this.driver = new ChromeDriver(this.service, this.options);
    this.driver.Manage().Window.Maximize();
  }
  protected virtual void Dispose(bool disposing)
  {
    if (disposing)
    {
      // Dispose managed resources here.
      this.driver.Quit();
    }
    // Dispose unmanaged resources here.
  }
  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }
  private Dictionary<string,string> ArquivoConfiguracao(String arquivo, Char separador)
  {
    var parametros = new Dictionary<string,string>();
    if(!System.IO.File.Exists(arquivo))
      throw new InvalidOperationException($"O arquivo {arquivo} não foi encontrado!");
    var file = System.IO.File.ReadAllLines(arquivo);
    foreach (var line in file)
    {
      if(String.IsNullOrEmpty(line)) continue;
      var args = line.Split(separador);
      if (args.Length != 2) continue;
      var cfg = args[0];
      var val = args[1];
      parametros.Add(cfg, val);
    }
    return parametros;
  }
}