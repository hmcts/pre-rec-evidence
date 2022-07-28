using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
    public int finalCount;
    public static List<string> DeletedIds = new List<string>();
    public static List<string> RecordingsManageCases = new List<string>();
    public static List<string> RecordingsManageRecordings = new List<string>();
    public static bool isDeleted = false;
    private String stringID = "";

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
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
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
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      for (int i = 2; i < 7; i++)
      {
        var Button = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n);
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await Task.Run(() => Assert.IsFalse(Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n).IsVisibleAsync().Result));
      }
    }
    public async Task findRecording()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]", $"{ExternalPortal.caseName}");
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {ExternalPortal.caseName}").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(48) div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p div:nth-child(2)").First.WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(48) div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p div:nth-child(2)").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      var RceordingLocation = Page.Frame("fullscreen-app-host").Locator("div:nth-child(49) div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div >div:nth-child(1)").First;
      stringID = RceordingLocation.TextContentAsync().Result.ToString().Trim();

      stringID = stringID.Substring(stringID.LastIndexOf(':') + 1);
      if (AdminManageRecordings.use == "F")
      {
        var Date = DateTime.Now.AddSeconds(10);
        while (DateTime.Now < Date)
        {
          await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings\"]").PressAsync("ArrowDown");
        }
        var gallery = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas > div > div:nth-child(3) > div > div > div:nth-child(49) > div > div > div > div > div.virtualized-gallery > div > div > div").Last.InnerHTMLAsync().Result;
        var removinghtml1 = gallery.Substring(gallery.IndexOf("Item") + 5);
        var removinghtml2 = Regex.Replace(removinghtml1.Split()[0], @"[^0-9a-zA-Z\ ]+", "");
        var finalCountString = removinghtml2.Substring(0, (removinghtml2.IndexOf("divdiv")));
        finalCount = Int32.Parse(finalCountString);

        var times = Math.Ceiling((double)finalCount / 2);
        for (int j = 0; j <= times; j++)
        {
          for (int i = finalCount; i >= 0; i--)
          {
            if (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"div:nth-child(49)  div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)").Nth(i).IsVisibleAsync().Result)
            {
              var UID = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"div:nth-child(49)  div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)").Nth(i);
              var temp = UID.TextContentAsync().Result.Trim();
              if (!(RecordingsManageCases.Contains(temp))) { RecordingsManageCases.Add(temp); }
            }
          }
          var Date2 = DateTime.Now.AddSeconds(1);
          while (DateTime.Now < Date2)
          {
            await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(49)  div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)").Last.PressAsync("ArrowUp");
          }
        }
      }
    }
    public async Task search()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Recordings\"]").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\.\\.\\.\"]").ClickAsync();
      if (AdminManageRecordings.use == "caseref")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\.\\.\\.\"]").FillAsync($"{ExternalPortal.caseName}");

        var Date = DateTime.Now.AddSeconds(7);
        while (DateTime.Now < Date)
        {
          await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]").Last.ClickAsync();
          await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]").Last.PressAsync("ArrowDown");
        }
        var gallery = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(3) > div > div > div:nth-child(71) > div > div > div > div > div.virtualized-gallery > div > div > div").Last.InnerHTMLAsync().Result;

        var removinghtml1 = gallery.Substring(gallery.IndexOf(">Item") + 5);
        var result = (removinghtml1.Remove(removinghtml1.Length - (removinghtml1.Length - 3))).Trim();
        var finalResult = Int32.Parse(result);

        await Task.Run(() => Assert.AreEqual(finalResult, finalCount));
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        var times = Math.Ceiling((double)finalResult / 17);
        for (int j = 0; j <= times; j++)
        {
          for (int i = finalResult; i >= 0; i--)
          {
            if (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Recording ID\"]").Nth(i).IsVisibleAsync().Result)
            {
              var UID = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Recording ID\"]").Nth(i);
              var temp = UID.InputValueAsync().Result.Trim();
              if (!(RecordingsManageRecordings.Contains(temp))) { RecordingsManageRecordings.Add(temp); }
            }
          }
          var Date2 = DateTime.Now.AddMilliseconds(550);
          while (DateTime.Now < Date2)
          {
            await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]").Last.PressAsync("ArrowUp");
          }
        }

        await Task.Run(() => Assert.AreEqual(RecordingsManageRecordings.Count, RecordingsManageCases.Count));

        for (int i = 0; i < RecordingsManageRecordings.Count; i++)
        {
          await Task.Run(() => Assert.That(RecordingsManageCases[i], Does.Contain(RecordingsManageRecordings[i])));
        }
      }
    }

    public async Task delete()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Yes\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      isDeleted = true;
      var message = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=There are no recordings matching your search criteria. Consider changing or remo").Nth(1);
      await Task.Run(() => Assert.That(message.TextContentAsync().Result, Does.Contain("no recordings")));
    }
    public async Task checkView()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").FillAsync($"{stringID.Trim()}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      var message = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(29)");
      await message.WaitForAsync();
      await Task.Run(() => Assert.That(message.TextContentAsync().Result, Does.Contain("no recordings")));

    }
    public async Task checkDelete()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("label rect").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      var count = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]").CountAsync().Result;

      for (int i = 0; i < count; i++)
      {
        DeletedIds.Add(Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]").Nth(i).InputValueAsync().Result);
      }
      await Task.Run(() => Assert.That(DeletedIds, Does.Contain(stringID.Trim())));

      var index = DeletedIds.IndexOf(stringID.Trim());
      var deleted = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ Status\"]").Nth(index);
      await Task.Run(() => Assert.That(deleted.InputValueAsync().Result, Does.Contain("Deleted")));
    }
    public async Task gotoManageCases()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Home\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Cases\"]").First.ClickAsync();


    }
    public async Task checkManageCases()
    {
      var restoreButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Restore\\ Recording\"]").First;
      await restoreButton.WaitForAsync();
      await Task.Run(() => Assert.IsFalse(restoreButton.IsVisibleAsync().Result));
      await restoreButton.ClickAsync();
    }
  }
}



