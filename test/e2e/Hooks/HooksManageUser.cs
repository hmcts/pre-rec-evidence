using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;

namespace pre.test.Hooks
{

  [Binding]
  public class HooksManageUsers
  {

    [BeforeScenario("ManageUsers", Order = 1)]
    public async Task goToManageUsers()
    {
      // using sandbox url whilst test is aligned, change in future
      await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Users\"]").First.ClickAsync();
    }
  }
}