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
       await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.demoUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Button\")").ClickAsync(); // Bug S28-522, clicking skip security button whilst, remove when fixed
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"View Recordings\")");
    }
  }
}
