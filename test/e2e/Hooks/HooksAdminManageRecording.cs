using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
namespace pre.test.Hooks
{
  [Binding]
  public class HooksAdminManageRecordings
  {

    [BeforeScenario("EditingRecordingDate", Order = 1)]
    public async Task goToAdminManageRecordings()
    {
      // using sandbox url whilst test is aligned, change in future
      await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Recordings\"]").ClickAsync();
    }

    [AfterScenario("EditingRecordingDate", Order = 0)]
    public async Task revertDate()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").FillAsync($"{AdminManageRecording.oldDate}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Save\"]").ClickAsync();
    }
  }
}
