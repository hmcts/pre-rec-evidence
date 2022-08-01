using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using pre.test.Hooks;

namespace pre.test.pages
{
  public class Editing : BasePage
  {
    public Editing(IPage page) : base(page) { }

    protected string casefound;
    protected string caseInfoVersion;
    protected string caseInfoRecId;
    protected string caseInfoRecWit;
    protected string caseInfoRecDef;

    public async Task findRecordedVideo()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search case ref\"]").FillAsync($"{ExternalPortal.caseName}");
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var casefoundLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p div:nth-child(3)").First;
      await Task.Run(() => Assert.That(casefoundLocator.InnerTextAsync().Result, Does.Contain($"{ExternalPortal.caseName}")));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]").First.WaitForAsync();

      var caseInfoVersionLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(4)").Last.TextContentAsync().Result;
      caseInfoVersion = (caseInfoVersionLocator.Substring(caseInfoVersionLocator.LastIndexOf('.') + 1)).Trim();

      var caseInfoRecIdLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(7)").Last.TextContentAsync().Result;
      caseInfoRecId = (caseInfoRecIdLocator.Substring(caseInfoRecIdLocator.LastIndexOf(':') + 1)).Trim();

      var caseInfoRecWitLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(8)").Last.TextContentAsync().Result;
      caseInfoRecWit = (caseInfoRecWitLocator.Substring(caseInfoRecWitLocator.LastIndexOf(':') + 1)).Trim();

      var caseInfoRecDefLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(10)").Last.TextContentAsync().Result;
      caseInfoRecDef = (caseInfoRecDefLocator.Substring(caseInfoRecDefLocator.LastIndexOf(':') + 1)).Trim();
    }

    public async Task clickEdit()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(6) > .appmagic-borderfill-container > .appmagic-border-inner > .react-knockout-control > .powerapps-icon").Last.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Please confirm the below details are correct and then click the Edit Request Det").WaitForAsync();

      var caseRef = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=CaseRef: {ExternalPortal.caseName}");
      var wit = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Witness: {caseInfoRecWit}").Nth(2);
      var def = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Defendants: {caseInfoRecDef}").Nth(2);
      var recordingId = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=RecordingID: {caseInfoRecId}");

      await wit.WaitForAsync();

      await Task.Run(() => Assert.IsTrue(caseRef.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(wit.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(def.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(recordingId.IsVisibleAsync().Result));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Edit Request Details\")").ClickAsync();
    }
    public async Task viewEditDetails()
    {
      var form = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Please copy the text below and paste it in your editing request form:");
      var now = DateTime.Now.AddMinutes(5);

      while (!(form.IsVisibleAsync().Result)) {if (now < DateTime.Now){break;}}

      await Task.Run(() => Assert.IsTrue(form.IsVisibleAsync().Result));
      var details = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search Recording ID\"]").InputValueAsync().Result;

      await Task.Run(() => Assert.That(details, Does.Contain($"CaseRef: {ExternalPortal.caseName}")));
    }
  }
}
