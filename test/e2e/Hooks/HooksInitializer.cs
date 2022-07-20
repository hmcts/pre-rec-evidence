using BoDi;
using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace pre.test.Hooks
{
  [Binding]
  public class HooksInitializer
  {
    public static IBrowser browser { get; private set; }
    public IBrowserContext context;
    public static List<string> caseRef = new List<string>();
    public static List<string> recordings = new List<string>();
    public static List<string> contacts = new List<string>();
    public IPlaywright playwright;
    public static string browserType = "";
    private readonly IObjectContainer _objectContainer;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
    public static PageSetters _context { get; set; }
    public static int scheduleCount = 0;
    protected static Microsoft.Extensions.Configuration.IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("secrets.json")
    .Build();
    protected string authPath = config["authPath"];
    protected string videoPath = config["videoPath"];
    public static string testUrl = config["testUrl"];
    public static string demoUrl = config["demoUrl"];
    public static string sboxUrl = config["sboxUrl"];
    public static string testPortalUrl = config["testPortalUrl"];
    public static string deleteCaseUrlTest = config["deleteCaseUrlTest"];
    public static string deleteScheduleUrlTest = config["deleteScheduleUrlTest"];
    public static string deleteRecordingUrlTest = config["deleteRecordingUrlTest"];
    public static string deleteCaseUrlSbox = config["deleteCaseUrlSbox"];
    public static string deleteScheduleUrlSbox = config["deleteScheduleUrlSbox"];
    public static string deleteRecordingUrlSbox = config["deleteRecordingUrlSbox"];
    public static string deleteContactsUrlTest = config["deleteContactsUrlTest"];
    public static string deleteContactsUrlSbox = config["deleteContactsUrlSbox"];
    public static string deleteOwner = config["deleteOwner"];
    public static bool headless = bool.Parse(config["headless"]);
    public HooksInitializer(IObjectContainer objectContainer, ScenarioContext scenarioContext, PageSetters context,
      ISpecFlowOutputHelper outputHelper)
    {
      _objectContainer = objectContainer;
      _scenarioContext = scenarioContext;
      _context = context;
      _specFlowOutputHelper = outputHelper;
    }

    [BeforeScenario("Chrome", Order = 1)]
    public async Task createBrowserChrome()
    {
      scheduleCount = 0;
      contacts.Clear();
      caseRef.Clear();
      recordings.Clear();
      browserType = "Chrome";

      playwright = await Playwright.CreateAsync();
      //BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions{ Headless = false };
      BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions { Headless = headless, SlowMo = 50, Channel = /*"msedge"*/"chrome" };
      browser = await playwright.Chromium.LaunchAsync(typeLaunchOptions);
      //context = await browser.NewContextAsync();
      context = await browser.NewContextAsync(new BrowserNewContextOptions { StorageStatePath = $"{authPath}",  RecordVideoDir = $"{videoPath}"});
      _context.Page = await context.NewPageAsync();
      _objectContainer.RegisterInstanceAs(_context.Page);
      //Generating living docs
      _specFlowOutputHelper.WriteLine("Browser Launched");
    }

    public async Task createBrowserEdge()
    {
      scheduleCount = 0;
      contacts.Clear();
      caseRef.Clear();
      recordings.Clear();
      browserType = "Edge";

      playwright = await Playwright.CreateAsync();
      //BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions{ Headless = false };
      BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions { Headless = headless, SlowMo = 50, Channel = "msedge" };
      browser = await playwright.Chromium.LaunchAsync(typeLaunchOptions);
      //context = await browser.NewContextAsync();
      context = await browser.NewContextAsync(new BrowserNewContextOptions { StorageStatePath = $"{authPath}",  RecordVideoDir = $"{videoPath}"});
      _context.Page = await context.NewPageAsync();
      _objectContainer.RegisterInstanceAs(_context.Page);
      //Generating living docs
      _specFlowOutputHelper.WriteLine("Browser Launched");
    }

    [BeforeScenario(Order = 0)]
    public async Task createBrowserChromium()
    {
      scheduleCount = 0;
      contacts.Clear();
      caseRef.Clear();
      recordings.Clear();
      browserType = "Chromium";

      playwright = await Playwright.CreateAsync();
      BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions { Headless = headless, SlowMo = 1000};
      browser = await playwright.Chromium.LaunchAsync(typeLaunchOptions);
      context = await browser.NewContextAsync(new BrowserNewContextOptions { StorageStatePath = $"{authPath}", RecordVideoDir = $"{videoPath}"});
      _context.Page = await context.NewPageAsync();
      _objectContainer.RegisterInstanceAs(_context.Page);
    }
    public async Task createBrowserFirefox()
    {
      scheduleCount = 0;
      contacts.Clear();
      caseRef.Clear();
      recordings.Clear();
      browserType = "Firefox";

      var firefoxUserPrefs = new Dictionary<string, object>
      {
        ["security.insecure_field_warning.contextual.enabled"] = false,
        ["security.certerrors.permanentOverride"] = false,
        ["network.stricttransportsecurity.preloadlist"] = false,
        ["security.enterprise_roots.enabled"] = true
      };

      playwright = await Playwright.CreateAsync();
      BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions { Headless = headless, SlowMo = 50, FirefoxUserPrefs = firefoxUserPrefs, Channel = "firefox" };
      browser = await playwright.Firefox.LaunchAsync(typeLaunchOptions);
      context = await browser.NewContextAsync(new BrowserNewContextOptions { StorageStatePath = $"{authPath}",  RecordVideoDir = $"{videoPath}"});
      _context.Page = await context.NewPageAsync();
      _objectContainer.RegisterInstanceAs(_context.Page);
      // await HooksInitializer._context.Page.PauseAsync();
      HooksInitializer._context = _context;
      HooksInitializer._context.Page = _context.Page;
    }
    public async Task createBrowserWebit()
    {
      scheduleCount = 0;
      contacts.Clear();
      caseRef.Clear();
      recordings.Clear();
      browserType = "Webkit";

      playwright = await Playwright.CreateAsync();
      BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions { Headless = headless, SlowMo = 50, };
      browser = await playwright.Webkit.LaunchAsync(typeLaunchOptions);
      context = await browser.NewContextAsync(new BrowserNewContextOptions { StorageStatePath = $"{authPath}",  RecordVideoDir = $"{videoPath}"});
      _context.Page = await context.NewPageAsync();
      _objectContainer.RegisterInstanceAs(_context.Page);
      // await HooksInitializer._context.Page.PauseAsync();
      HooksInitializer._context = _context;
      HooksInitializer._context.Page = _context.Page;
    }

    [AfterScenario(Order = 3)]
    public async Task takeScreenshotIfFailed()
    {
      if (_scenarioContext.TestError != null)
      {
        await Helpers.ScreenShotHelper.Screenshot(_context.Page);
      }
    }

    [AfterScenario(Order = 4)]
    public async Task cleanUpEnvSchedule()
    {
      if (scheduleCount > 0)
      {
        await HooksInitializer._context.Page.GotoAsync($"{deleteScheduleUrlTest}");
        await HooksInitializer._context.Page.Locator("button:has-text(\"more\")").WaitForAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"more\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("owner");
        await HooksInitializer._context.Page.Locator("text=").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").WaitForAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Newer to older\"]").ClickAsync();

        for (int i = 0; i < scheduleCount; i++)
        {
          await HooksInitializer._context.Page.Locator($"text={deleteOwner}").Nth(1).ClickAsync();
          if (HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").IsVisibleAsync().Result == false)
          {
            await HooksInitializer._context.Page.ReloadAsync();
            await HooksInitializer._context.Page.Locator($"text={deleteOwner}").Nth(1).ClickAsync();
          }
          await HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").ClickAsync();
        }

      }
    }

    [AfterScenario(Order = 5)]
    public async Task cleanUpEnvCase()
    {
      if (caseRef.Count > 0)
      {
        await HooksInitializer._context.Page.GotoAsync($"{deleteCaseUrlTest}");
        await HooksInitializer._context.Page.Locator("button:has-text(\"more\")").WaitForAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"more\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").WaitForAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("caseref");
        await HooksInitializer._context.Page.Locator("text=").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").WaitForAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Newer to older\"]").ClickAsync();

        for (int j = 0; j < caseRef.Count; j++)
        {
          await HooksInitializer._context.Page.Locator($"text={caseRef[j]}").First.ClickAsync();
          if (HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").IsVisibleAsync().Result == false)
          {
            await HooksInitializer._context.Page.ReloadAsync();
            await HooksInitializer._context.Page.Locator($"text={caseRef[j]}").First.ClickAsync();
          }
          await HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").ClickAsync();
        }

      }
    }

    [AfterScenario(Order = 6)]
    public async Task cleanUpEnvRecordings()
    {
      if (recordings.Count > 0)
      {
        await HooksInitializer._context.Page.GotoAsync($"{deleteRecordingUrlTest}");
        await HooksInitializer._context.Page.Locator("button:has-text(\"more\")").WaitForAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"more\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").WaitForAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("case ref");
        await HooksInitializer._context.Page.Locator("text=").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").WaitForAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Newer to older\"]").ClickAsync();

        for (int j = 0; j < recordings.Count; j++)
        {
          await HooksInitializer._context.Page.Locator($"text={recordings[j]}").First.ClickAsync();
          if (HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").IsVisibleAsync().Result == false)
          {
            await HooksInitializer._context.Page.ReloadAsync();
            await HooksInitializer._context.Page.Locator($"text={recordings[j]}").First.ClickAsync();
          }
          await HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").ClickAsync();
        }
      }
    }

    [AfterScenario(Order = 7)]
    public async Task cleanUpEnvContacts()
    {
      if (contacts.Count > 0)
      {
        await HooksInitializer._context.Page.GotoAsync($"{deleteContactsUrlTest}");
        await HooksInitializer._context.Page.Locator("button:has-text(\"more\")").WaitForAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"more\")").ClickAsync();
        await HooksInitializer._context.Page.Locator(".ms-Checkbox-checkbox").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("text=Full Name (Primary) >> i").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("owner");
        await HooksInitializer._context.Page.Locator("text=").ClickAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").WaitForAsync();
        await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("created on");
        await HooksInitializer._context.Page.Locator("text=").First.ClickAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").WaitForAsync();
        await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Created On\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Newer to older\"]").ClickAsync();
        await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Owner\")").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Filter by\"]").WaitForAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Filter by\"]").ClickAsync();
        await HooksInitializer._context.Page.Locator("[aria-label=\"Filter by value\"]").FillAsync($"{deleteOwner}");
        await HooksInitializer._context.Page.Locator("button:has-text(\"Apply\")").ClickAsync();

        for (int i = 0; i < contacts.Count; i++)
        {
          await HooksInitializer._context.Page.Locator($"text={contacts[i]}").First.ClickAsync();
          if (HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").IsVisibleAsync().Result == false)
          {
            await HooksInitializer._context.Page.ReloadAsync();
            await HooksInitializer._context.Page.Locator($"text={contacts[i]}").First.ClickAsync();
          }
          await HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete\")").ClickAsync();
        }
      }
    }

    [AfterScenario(Order = 8)]
    public async Task takeScreenshotIfFailed2()
    {
      if (_scenarioContext.TestError != null)
      {
        await Helpers.ScreenShotHelper.Screenshot(_context.Page);
      }
    }

    [AfterScenario(Order = 9)]
    public async Task closeBrowser()
    {
      if (browserType == "Chromium")
      {
        await browser.DisposeAsync();
        // Generating living docs
        _specFlowOutputHelper.WriteLine("Browser Closed");
      }
      await context.CloseAsync();
    }
  }
}