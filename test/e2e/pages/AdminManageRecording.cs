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

    public async Task changeDate()
    {
      oldDate = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").InputValueAsync().Result.Trim();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").ClickAsync();
      if (AdminManageRecordings.use == "normal")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").FillAsync($"{date}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Save\"]").ClickAsync();
      }
      else if (AdminManageRecordings.use == "past")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]").FillAsync($"{pastDate}");
        var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Save\"]");
        await Task.Run(() => Assert.IsTrue(saveButton.IsDisabledAsync().Result));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Recording ID").First.ClickAsync();
      }
    }

    public async Task checkDateChange()
    {
      var dateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. On >> [aria-label=\"Recording Start\"]");
      await Task.Run(() => Assert.That(dateLocator.InputValueAsync().Result, Does.Contain($"{date}")));
    }

    public async Task pastDateError()
    {
      var error = Page.Locator("text=Date cannot be in the past.");
      await Task.Run(() => Assert.IsTrue(error.IsVisibleAsync().Result));
    }

    public async Task PageCheck()
    {
      var Header = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Recording Start");
      await Task.Run(() => Assert.That(Header.TextContentAsync().Result, Does.Contain("Recording Start")));
    }
    public async Task CheckSaveButtonDisabled()
    {
      for(int i = 2; i < 7; i ++)
      {
         var Button = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Item {i} On >> [aria-label=\"Save\"]");
        await Task.Run(() => Assert.IsTrue(Button.IsDisabledAsync().Result));
      }
    }

  }
}


