using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class ManageCase : BasePage
  {
    public ManageCase(IPage page) : base(page) { }
    protected String caseRef = "";
    protected String court = "";
    protected String newCourt = "";
    protected String caseId = "";

    public async Task goToManageCase()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
    }

    public async Task updateCourt()
    {
      var caseRefLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(45) div.virtualized-gallery > div > div > div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div >  div:nth-child(2)");
      caseRef = caseRefLocator.TextContentAsync().Result.ToString().Trim();
      caseRef = caseRef.Substring(caseRef.LastIndexOf(':') + 1).Trim();

      var courtLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First;
      court = courtLocator.TextContentAsync().Result.ToString().Trim();
      court = court.Substring(court.LastIndexOf(':') + 1).Trim();

      var caseIdLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div:nth-child(45) div.virtualized-gallery  div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)");
      caseId = caseIdLocator.TextContentAsync().Result.ToString().Trim();
      caseId = caseId.Substring(caseId.LastIndexOf(':') + 1).Trim();

      if (court == "Leeds") { newCourt = "Chris"; } else { newCourt = "Leeds"; }

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case ID: {caseId} Case Ref: {caseRef} Court: {court} >> [aria-label=\"Edit Case\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {court}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newCourt}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(119) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      await Task.Run(() => Assert.That(courtLocator.InnerTextAsync().Result, Does.Contain($"{newCourt}")));
    }

    public async Task checkCourtManageRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{caseRef}");

      var caseInfo = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p div:nth-child(4)");
      await Task.Run(() => Assert.That(caseInfo.AllInnerTextsAsync().Result, Does.Contain($"Court: {newCourt}")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      var courtDropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Courts\\. Selected\\: {newCourt}\"]");
      await Task.Run(() => Assert.IsTrue(courtDropdown.IsVisibleAsync().Result));
    }

    public async Task checkCourtViewRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search case ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search case ref\"]").FillAsync($"{caseRef}");

      var caseInfo = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div:nth-child(20) div.virtualized-gallery.hideScrollbar div div div:nth-child(1)  div.canvasContentDiv.container_1vt1y2p");
      await Task.Run(() => Assert.That(caseInfo.InnerTextAsync().Result, Does.Contain($"{newCourt}")));

      var caseInfoGreyBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(39) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text");
      await Task.Run(() => Assert.That(caseInfo.InnerTextAsync().Result, Does.Contain($"{newCourt}")));
    }

    public async Task revertCourt()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case ID: {caseId} Case Ref: {caseRef} Court: {newCourt} >> [aria-label=\"Edit Case\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {newCourt}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{court}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(119) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      var courtLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First;
      await Task.Run(() => Assert.That(courtLocator.InnerTextAsync().Result, Does.Contain($"{court}")));
    }
  }
}