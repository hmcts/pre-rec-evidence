using BoDi;
using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using Microsoft.Extensions.Configuration;


namespace pre.test.Hooks
{
  [Binding]
  public class HooksInitializer
  {
    public IBrowser browser { get; private set; }
    public IBrowserContext context;
    public IPlaywright playwright;
    private readonly IObjectContainer _objectContainer;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
    public static PageSetters _context { get; private set; }

    protected static Microsoft.Extensions.Configuration.IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("secrets.json")
    .Build();
    protected string authPath = config["authPath"];
    public HooksInitializer(IObjectContainer objectContainer, ScenarioContext scenarioContext, PageSetters context,
      ISpecFlowOutputHelper outputHelper)
    {
      _objectContainer = objectContainer;
      _scenarioContext = scenarioContext;
      _context = context;
      _specFlowOutputHelper = outputHelper;
    }

    [AfterScenario(Order = 1)]
    public async Task closeBrowser()
    {
      if (_scenarioContext.TestError != null)
      {
        await Helpers.ScreenShotHelper.Screenshot(_context.Page);
      }
      await browser.DisposeAsync();
      //Generating living docs
      _specFlowOutputHelper.WriteLine("Browser Closed");
    }

    [BeforeScenario( Order = 0)]
    public async Task createBrowser()
    {
      playwright = await Playwright.CreateAsync();
      //BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions{ Headless = false };
      BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions { Headless = false, SlowMo = 1500 };
      browser = await playwright.Chromium.LaunchAsync(typeLaunchOptions);
      //context = await browser.NewContextAsync();
      context = await browser.NewContextAsync(new BrowserNewContextOptions{ StorageStatePath = $"{authPath}", });
      _context.Page = await context.NewPageAsync();
      _objectContainer.RegisterInstanceAs(_context.Page);
      //Generating living docs
      _specFlowOutputHelper.WriteLine("Browser Launched");
    }
  }
}
