using System.Threading.Tasks;
using TechTalk.SpecFlow;
namespace pre.test.Hooks
{
  [Binding]
  public class HooksViewRecording
  {

    [BeforeScenario("View", Order = 1)]
    public async Task goToViewRecordings()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"View Recordings\")");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
  }
}
