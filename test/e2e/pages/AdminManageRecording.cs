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
    protected string oldDate = "";

    public async Task goToAdminManageRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Recordings\"]").ClickAsync();
    }

    public async Task changeDate()
    {
      oldDate = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").InputValueAsync().Result.Trim();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").FillAsync($"{date}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Save\"]").ClickAsync();
    }

    public async Task checkDateChange()
    {
      var dateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]");
      await Task.Run(() => Assert.That(dateLocator.InputValueAsync().Result, Does.Contain($"{date}")));
    }

    public async Task revertDate()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").FillAsync($"{oldDate}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Save\"]").ClickAsync();

      var dateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]");
      await Task.Run(() => Assert.That(dateLocator.InputValueAsync().Result, Does.Contain($"{oldDate}")));
    }
  }
}


