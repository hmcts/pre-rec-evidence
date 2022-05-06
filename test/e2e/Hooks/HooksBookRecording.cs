using System.Threading.Tasks;
using TechTalk.SpecFlow;
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
  }
}
