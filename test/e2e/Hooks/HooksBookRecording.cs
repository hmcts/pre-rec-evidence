using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;

namespace pre.test.Hooks
{

  [Binding]
  public class HooksBookRecording
  {
    [BeforeScenario("ScheduleCreate", Order = 1)]
    public async Task goToAdminManageRecordings()
    {
      // using sandbox url whilst test is aligned, change in future
      await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      var book = HooksInitializer._context.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Book a Recording\")");

      await Task.Run(() => book.IsVisibleAsync().Result);
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
    }

    [AfterScenario("cleanUpRecordings", Order = 0)]
    public async Task cleanUpRecordings()
    {
      for (int i = 0; i < BookRecording.count; i++)
      {
         await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Date\"]").ClickAsync();
         await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{(BookRecording.originalDay.AddDays(+i)).ToString("ddd")} {(BookRecording.originalDay.AddDays(+i)).ToString("MMM")} {(BookRecording.originalDay.AddDays(+i)).ToString("dd")} {(BookRecording.originalDay.AddDays(+i)).ToString("yyyy")}\"]").ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Finish\")").First.ClickAsync();
      }
    }
  }
}
