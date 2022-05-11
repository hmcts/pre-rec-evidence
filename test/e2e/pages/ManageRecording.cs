using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class ManageRecording : BasePage
  {
    protected string caseRef = $"AutoManageTestCase{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}";
    protected string day = DateTime.UtcNow.ToString("ddd");
    protected string month = DateTime.UtcNow.ToString("MMM");
    protected string date = DateTime.UtcNow.ToString("dd");
    protected string year = DateTime.UtcNow.ToString("yyyy");

    public ManageRecording(IPage page) : base(page) { }

    public async Task createAndScheduleCase()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("ul[role=\"listbox\"] div:has-text(\"Leeds\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{caseRef}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").FillAsync("def1");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").PressAsync("Tab");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("wit1");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Scheduled Start DateOpen calendar to select a date\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("li[role=\"option\"] div:has-text(\"wit1 wit1\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=def1 def1").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").First.ClickAsync();
    }

    public async Task goToManageRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{caseRef}");

      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(19) div.virtualized-gallery div:nth-child(1) div.canvasContentDiv.container_1vt1y2p div div:nth-child(2)").First;
      await Task.Run(() => Assert.That(results.InnerTextAsync().Result, Does.Contain($"{caseRef}")));
    }

    public async Task checkStreamButton()
    {
      var streamButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Check Stream\")");
      await Task.Run(() => Assert.IsFalse(streamButton.IsVisibleAsync().Result));
    }
  }
}