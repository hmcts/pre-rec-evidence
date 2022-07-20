using System.Threading.Tasks;
using TechTalk.SpecFlow;
namespace pre.test.Hooks
{
  [Binding]
  public class HooksViewRecording
  {

    [BeforeScenario("View", Order = 2)]
    public async Task goToViewRecordings()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"View Recordings\")");
    }
  }
}
