namespace sap_automation
{
  public partial class Program
  {
    public void ClosePopup()
    {
      this.janelas = this.driver.WindowHandles;
      if(this.janelas.Count > 1)
      {
        for (int i = 1; i < this.janelas.Count; i++)
        {
          this.driver.SwitchTo().Window(this.janelas[i]);
          this.driver.Close();
        }
      }
      this.driver.SwitchTo().Window(this.janelas.First());
    }
  }
}
