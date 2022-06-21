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
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsVisibleAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
    }

    [AfterScenario("cleanUpRecordings", Order = 0)]
    public async Task cleanUpRecordings()
    {
      for (int i = 0; i < BookRecording.count; i++)
      {
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Date\"]").ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{(BookRecording.originalDay.AddDays(+i)).ToString("ddd")} {(BookRecording.originalDay.AddDays(+i)).ToString("MMM")} {(BookRecording.originalDay.AddDays(+i)).ToString("dd")} {(BookRecording.originalDay.AddDays(+i)).ToString("yyyy")}\"]").ClickAsync();

        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Finish\")").First.ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }
    }
  }
}
