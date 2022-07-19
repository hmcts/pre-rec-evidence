using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class AdminManageRecording : BasePage
  {
    public AdminManageRecording(IPage page) : base(page)
    {
    }
    protected string date = DateTime.UtcNow.ToString("dd/MM/yyyy");
    protected string pastDate = (DateTime.UtcNow.AddDays(-1)).ToString("dd/MM/yyyy");
    public static string oldDate = "";
    public static int n = 1;

    public async Task changeDate()
    {
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Video Link\"]").Nth(n).InputValueAsync().Result.Contains("http"))
      {
        n++;
      }

      oldDate = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n).InputValueAsync().Result.Trim();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n).ClickAsync();
      if (AdminManageRecordings.use == "normal")
      {
        if (date == oldDate)
        {
          date = (DateTime.UtcNow.AddDays(1)).ToString("dd/MM/yyyy");
        }
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n).FillAsync($"{date}");
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n).ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }
      else if (AdminManageRecordings.use == "past")
      {
        var dateLocation = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n);
        if (pastDate == dateLocation.InputValueAsync().Result)
        {
          pastDate = (DateTime.UtcNow.AddDays(-2)).ToString("dd/MM/yyyy");
        }
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n).FillAsync($"{pastDate}");

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n).ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }
    }
    public async Task checkDateChange()
    {
      var dateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n);
      await Task.Run(() => Assert.That(dateLocator.InputValueAsync().Result, Does.Contain($"{date}")));

    }
    public async Task pastDateError()
    {
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var error = Page.Locator("text=Date cannot be in the past.");
      await error.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(error.IsVisibleAsync().Result));
    }
    public async Task PageCheck()
    {
      var Header = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Recording Start");
      await Task.Run(() => Assert.That(Header.TextContentAsync().Result, Does.Contain("Recording Start")));
    }
    public async Task CheckSaveButtonDisabled()
    {
      for (int i = 2; i < 7; i++)
      {
        var Button = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n);
        await Task.Run(() => Assert.IsFalse(Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n).IsVisibleAsync().Result));
      }
    }
  }
}