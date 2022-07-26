using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.Playwright;
using pre.test.pages;

namespace pre.test.Hooks
{
  [Binding]
  public class HooksEditing
  {
    public static string caseRef;

    [BeforeScenario("goToViewRecordings", Order = 3)]
    public async Task goToViewRecordings()
    {
      caseRef = ManageRecording.caseRef;
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
  }
}