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
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.demoUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Button\")").ClickAsync(); // Bug S28-522, clicking skip security button whilst, remove when fixed
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Users\"]").First.ClickAsync();
    }
  }
}