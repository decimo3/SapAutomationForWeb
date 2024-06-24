using OpenQA.Selenium;
namespace sap_automation
{
  public partial class Program
  {
    private Dictionary<String, String> camadas = new()
    {
      {"MAIN", "CRMApplicationFrame"},
      {"TOPAREA", "CRMApplicationFrame>FRAME_TOPAREA"},
      {"TABAREA", "CRMApplicationFrame>FRAME_TABAREA"},
      {"MIDAREA", "CRMApplicationFrame>.//div/iframe"},
      {"BOTAREA", "CRMApplicationFrame>FRAME_BOTAREA"},
      {"APPLICATION", "CRMApplicationFrame>.//div/iframe>CRMApplicationFrame>FRAME_APPLICATION"},
      {"WorkAreaFrame1", "CRMApplicationFrame>.//div/iframe>CRMApplicationFrame>FRAME_APPLICATION>WorkAreaFrame1"},
      {"WorkAreaFrame2", "CRMApplicationFrame>.//div/iframe>CRMApplicationFrame>FRAME_APPLICATION>WorkAreaFrame2"},
      {"ParceiroFrame1", "WorkAreaFrame1"},
      {"ParceiroFrame2", "WorkAreaFrame2"},
      {"EmbedFramePDF", "WorkAreaFrame1>ITSFRAME1"},
      {"ChromeViewerPDF", ".//body/embed"},
      {"HeadAreaFrame", "CRMApplicationFrame>.//div/iframe>CRMApplicationFrame>FRAME_APPLICATION>HeaderFrame"},
      {"TabAreaFrame", "CRMApplicationFrame>.//div/iframe>CRMApplicationFrame>FRAME_APPLICATION>HeaderFrame>WorkAreaFrame1"},
    };

    public void GotoFrame(String frame_index)
    {
      this.driver.SwitchTo().DefaultContent();
      if(!camadas.TryGetValue(frame_index, out String? frames) || frames == null)
      {
        var erro = "O frame selecionado não foi mapeado ou é inválido!";
        throw new InvalidOperationException(erro);
      }
      foreach (var frame in frames.Split('>'))
      {
        var by = frame.StartsWith('.') ? By.XPath(frame) : By.Id(frame);
        var element = this.driver.FindElement(by);
        this.driver.SwitchTo().Frame(element);
      }
    }
  }
}