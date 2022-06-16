using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
namespace pre.test.Hooks
{
  [Binding]
  public class HooksAdminManageRecordings
  {

    [BeforeScenario("EditingRecordingDate", Order = 1)]
    public async Task goToManageRecordings()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Recordings\"]").ClickAsync();
    }
    [BeforeScenario("SuperUserEditingRecordingDate", Order = 1)]
    public async Task goToSuperUserManageRecordings()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Super User\")").First.ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Recordings\"]").ClickAsync();
    }

    [AfterScenario("RevertDate", Order = 0)]
    public async Task revertDate()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(AdminManageRecording.n).ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(AdminManageRecording.n).FillAsync($"{AdminManageRecording.oldDate}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(AdminManageRecording.n).ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
  }
}
