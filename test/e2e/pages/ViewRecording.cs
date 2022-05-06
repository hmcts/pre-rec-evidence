using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class ViewRecording : BasePage
  {
    public ViewRecording(IPage page) : base(page)
    {
    }

    private String stringCase = "";

    public async Task FindCaseToView()
    {
      var caseLocation = ViewRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "div.virtualized-gallery.hideScrollbar  div:nth-child(1) >div.canvasContentDiv.container_1vt1y2p div:nth-child(3)");
      stringCase = caseLocation.TextContentAsync().Result.ToString().Trim();
      stringCase = stringCase.Substring(stringCase.LastIndexOf(':') + 1);

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder='Search case ref']");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder='Search case ref']", $"{stringCase.Trim()}");
    }

    public async Task CheckSearch()
    {
      var results = ViewRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("#publishedCanvas  div.virtualized-gallery.hideScrollbar div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").First;
      await Task.Run(() => Assert.That(results.TextContentAsync().Result, Does.Contain($"{stringCase.Trim()}")));
    }

    public async Task SwitchTimestamp()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("div[role='switch']");
    }

    public async Task CheckTimeStampOn()
    {
      var playButton = ViewRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[aria-label='Play Video Recording']");
      await Task.Run(() => Assert.IsTrue(playButton.IsVisibleAsync().Result));

      var timeStamp = ViewRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[aria-label=\"Show controls\"] div:has-text(\"Elapsed time 00:00:00 / Total time 00:00:00\")").Nth(1);
      await Task.Run(() => Assert.IsTrue(timeStamp.IsVisibleAsync().Result));
      await Task.Run(() => Assert.That(timeStamp.TextContentAsync().Result, Does.Contain("00:00:00")));
    }

    public async Task CheckTimeStampOff()
    {
      var playButton = ViewRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[aria-label='Play Video Recording']");
      await Task.Run(() => Assert.IsFalse(playButton.IsVisibleAsync().Result));

      var timeStamp = ViewRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("#publishedCanvas div:nth-child(8) > div > div > div > div > div > div > div");
      await Task.Run(() => Assert.IsFalse(timeStamp.IsVisibleAsync().Result));
    }
  }
}
