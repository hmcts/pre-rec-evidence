using Microsoft.Playwright;

using Xunit;



namespace pre.test.pages

{

  public class BasePage : IClassFixture<Hooks.HooksInitializer>

  {

    protected readonly Hooks.HooksInitializer fixture;

    protected IPage Page;

    public BasePage(IPage page) => Page = page;

    public IPage GetPage() => Page;



    public BasePage(Hooks.HooksInitializer fixture)

    {

      this.fixture = fixture;
    }
  }
}
