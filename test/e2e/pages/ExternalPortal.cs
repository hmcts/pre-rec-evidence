using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace pre.test.pages
{
  public class ExternalPortal : BasePage
  {
    protected static Microsoft.Extensions.Configuration.IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("secrets.json")
    .Build();
    public ExternalPortal(IPage page) : base(page) { }
    public static string date = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
    protected string emailToShare = config["portalEmail"];
    protected string emailPassword = config["portalPassword"];
    protected string caseName = "AUTOMATEDTESTRECORDINGDONOTDELETE";
    protected string witnessName = "null";
    protected string UpdatedWitnessName = "null";
    protected string courtName = "null";
    protected string UpdatedCourtName = "null";

    public async Task PortalLogin()
    {

      await Page.Locator("input[name=\"Email\"]").ClickAsync();
      await Page.Locator("input[name=\"Email\"]").FillAsync($"{emailToShare}");
      await Page.Locator("input[name=\"PasswordValue\"]").ClickAsync();
      await Page.Locator("input[name=\"PasswordValue\"]").FillAsync($"{emailPassword}");
      await Page.Locator("input[name=\"PasswordValue\"]").PressAsync("Enter");
    }
    public async Task ShareCase()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{caseName}");

      var witness = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(5)");
      witnessName = witness.TextContentAsync().Result.ToString().Trim();
      witnessName = witnessName.Substring(witnessName.LastIndexOf(':') + 1);

      var court = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(4)");
      courtName = court.TextContentAsync().Result.ToString().Trim();
      courtName = courtName.Substring(courtName.LastIndexOf(':') + 1);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Share\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Find Users to Share Recording\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Find items\"]").FillAsync($"{emailToShare}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role='option'] >> text={emailToShare}").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo Dev Home Manage Recordings Court Court NameOpen popup to select items").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Grant Access\")").ClickAsync();
    }

    public async Task ViewRecording()
    {
      var tableCaseRef = Page.Locator($"text={caseName}");
      await Task.Run(() => Assert.IsTrue(tableCaseRef.IsVisibleAsync().Result));
      await Task.Run(() => tableCaseRef.ClickAsync());
      var Status = Page.Locator("text=Requesting Video");
      await Task.Run(() => Assert.IsTrue(Status.IsVisibleAsync().Result));
    }

    public async Task UnShareCase()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{caseName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Recordings Gallery\"] div:has-text(\"Item 1. Selected. {emailToShare}\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Cases Gallery\"]").ClickAsync();
      var emailsSharedWith = ExternalPortals._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings Gallery\"] div").Nth(1);/*  */
      await Task.Run(() => Assert.That(emailsSharedWith.TextContentAsync().Result, Does.Not.Contain($"{emailToShare}")));
    }

    public async Task checkUnshared()
    {
      var tableCaseRef = Page.Locator($"text={caseName}");
      await Task.Run(() => Assert.IsFalse(tableCaseRef.IsVisibleAsync().Result));
    }

    public async Task CheckSharedRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{caseName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div .react-knockout-control .appmagic-svg").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")").ClickAsync();
      var emailsSharedWith = ExternalPortals._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings Gallery\"] div").Nth(1);/*  */
      await Task.Run(() => Assert.That(emailsSharedWith.TextContentAsync().Result, Does.Not.Contain($"{emailToShare}")));
    }

    // public async Task NoRecordingsMessage(){
    //   // bug S28-419 - once fixed add an assertion to check for the message
    // }

    public async Task checkCourts()
    {
      var table = ExternalPortals._pagesetters.Page.Locator(".xrm-attribute-value div:nth-child(4)");
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Contain($"{courtName.Trim()}")));
    }
    public async Task checkWitnesses()
    {
      var table = ExternalPortals._pagesetters.Page.Locator(".xrm-attribute-value div:nth-child(4)");
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Contain($"{witnessName.Trim()}")));
    }

    public async Task updateWitnesses()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").FillAsync($"{caseName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("ul[role=\"listbox\"] div:has-text(\"Shehreem\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync($"automated 2 {date}");
      UpdatedWitnessName = $"automated 2 {date}";
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
    }

    public async Task checkUpdatedWitnesses()
    {
      var table = ExternalPortals._pagesetters.Page.Locator(".xrm-attribute-value div:nth-child(4)");
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Not.Contain($"{witnessName.Trim()}")));
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Contain($"{UpdatedWitnessName.Trim()}")));
    }

  }
}

