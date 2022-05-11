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
      // using sandbox url whilst test is aligned, change in future
      await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"View Recordings\")");
    }
  }
}
