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
    public static string caseref = "";
    public IPlaywright playwright;
    private readonly IObjectContainer _objectContainer;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
    public static PageSetters _context { get; private set; }
    public static int caseCount = 0;
    public static int scheduleCount = 0;
    protected static Microsoft.Extensions.Configuration.IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("secrets.json")
    .Build();
    protected string authPath = config["authPath"];
    public static string testUrl = config["testUrl"];
    public static string demoUrl = config["demoUrl"];
    public static string sboxUrl = config["sboxUrl"];
    public static string deleteCaseUrlTest = config["deleteCaseUrlTest"];
    public static string deleteScheduleUrlTest = config["deleteScheduleUrlTest"];
    public static string deleteRecordingUrlTest = config["deleteRecordingUrlTest"];
    public static string deleteCaseUrlSbox = config["deleteCaseUrlSbox"];
    public static string deleteScheduleUrlSbox = config["deleteScheduleUrlSbox"];
    public static string deleteRecordingUrlSbox = config["deleteRecordingUrlSbox"];
    public static string deleteOwner = config["deleteOwner"];
    public HooksInitializer(IObjectContainer objectContainer, ScenarioContext scenarioContext, PageSetters context,
      ISpecFlowOutputHelper outputHelper)
    {
      _objectContainer = objectContainer;
      _scenarioContext = scenarioContext;
      _context = context;
      _specFlowOutputHelper = outputHelper;
    }

    [AfterScenario(Order = 1)]
    public async Task cleanUpEnv()
    {
      if (scheduleCount > 0)
      {
        await HooksInitializer._context.Page.GotoAsync($"{deleteScheduleUrlTest}");
        await HooksInitializer._context.Page.Locator("button:has-text(\"+20 more\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("owner");
        await HooksInitializer._context.Page.Locator("text=").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Newer to older\"]").ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        for (int i = 0; i < scheduleCount; i++)
        {
          await HooksInitializer._context.Page.Locator($"text={deleteOwner}").Nth(1).ClickAsync();
          await HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").ClickAsync();
          await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        }
      }

      if (caseCount > 0)
      {
        await HooksInitializer._context.Page.ReloadAsync();
        await HooksInitializer._context.Page.GotoAsync($"{deleteCaseUrlTest}");
        await HooksInitializer._context.Page.Locator("button:has-text(\"+19 more\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("caseref");
        await HooksInitializer._context.Page.Locator("text=").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Newer to older\"]").ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        for (int i = 0; i < caseCount; i++)
        {
          await HooksInitializer._context.Page.Locator($"text={caseref}").First.ClickAsync();
          await HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").ClickAsync();
          await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        }
      }
      if (scheduleCount > 0)
      {
        await HooksInitializer._context.Page.ReloadAsync();
        await HooksInitializer._context.Page.GotoAsync($"{deleteRecordingUrlTest}");
        await HooksInitializer._context.Page.Locator("button:has-text(\"+38 more\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("case ref");
        await HooksInitializer._context.Page.Locator("text=").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Newer to older\"]").ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        for (int i = 0; i < scheduleCount; i++)
        {
          await HooksInitializer._context.Page.Locator($"text={caseref}").First.ClickAsync();
          await HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").ClickAsync();
          await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        }
      }
      caseCount = 0;
      scheduleCount = 0;
    }

    [AfterScenario(Order = 2)]
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

    [BeforeScenario(Order = 0)]
    public async Task createBrowser()
    {
      playwright = await Playwright.CreateAsync();
      //BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions{ Headless = false };
      BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions { Headless = false };
      browser = await playwright.Chromium.LaunchAsync(typeLaunchOptions);
      //context = await browser.NewContextAsync();
      context = await browser.NewContextAsync(new BrowserNewContextOptions { StorageStatePath = $"{authPath}", });
      _context.Page = await context.NewPageAsync();
      _objectContainer.RegisterInstanceAs(_context.Page);
      //Generating living docs
      _specFlowOutputHelper.WriteLine("Browser Launched");
    }
  }
}
