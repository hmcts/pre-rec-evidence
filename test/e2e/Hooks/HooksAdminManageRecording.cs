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
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.demoUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Button\")").ClickAsync(); // Bug S28-522, clicking skip security button whilst, remove when fixed
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Recordings\"]").ClickAsync();
    }

    [AfterScenario("RevertDate", Order = 0)]
    public async Task revertDate()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").FillAsync($"{AdminManageRecording.oldDate}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Save\"]").ClickAsync();
    }
  }
}
