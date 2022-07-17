using BoDi;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

[assembly: Parallelizable(ParallelScope.Fixtures)]
namespace pre.test.Hooks
{
[Binding]
    class HooksInitializer
    {
        public IBrowser browser;
        public IBrowserContext context;
        public IPage page;
        public IPlaywright playwright;
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        PageSetters _context;

        public HooksInitializer(IObjectContainer objectContainer, ScenarioContext scenarioContext, PageSetters context, ISpecFlowOutputHelper outputHelper)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _context = context;
            _specFlowOutputHelper = outputHelper;

         }

        [AfterScenario()]
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

        [BeforeScenario()]
        public async Task createBrowser()
        {
            playwright = await Playwright.CreateAsync();
            BrowserTypeLaunchOptions typeLaunchOptions = new BrowserTypeLaunchOptions{ Headless = false };
            browser = await playwright.Chromium.LaunchAsync(typeLaunchOptions);
            //context = await browser.NewContextAsync();
            context = await browser.NewContextAsync(new BrowserNewContextOptions{StorageStatePath = "<place your auth file generated>",});
            _context.Page = await context.NewPageAsync();
            _objectContainer.RegisterInstanceAs(_context.Page);
           //Generating living docs
            _specFlowOutputHelper.WriteLine("Browser Launched");
        }
    }
}
