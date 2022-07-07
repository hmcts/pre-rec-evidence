using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class ManageRecording : BasePage
  {
    public static string caseRef = "";
    public static string day = DateTime.UtcNow.ToString("ddd");
    public static string month = DateTime.UtcNow.ToString("MMM");
    public static string date = DateTime.UtcNow.ToString("dd");
    public static string year = DateTime.UtcNow.ToString("yyyy");
    protected string yesterday = ((DateTime.UtcNow.AddDays(-1)).ToString("ddd"));
    protected string yesterDateNum = ((DateTime.UtcNow.AddDays(-1)).ToString("dd"));
    protected string yesterMonth = ((DateTime.UtcNow.AddDays(-1)).ToString("MMM"));

    protected string yesterYear = ((DateTime.UtcNow.AddDays(-1)).ToString("yyyy"));

    public ManageRecording(IPage page) : base(page) { }

    public async Task checkStreamButton()
    {
      var streamButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Check Stream\")");
      await Task.Run(() => Assert.IsFalse(streamButton.IsVisibleAsync().Result));
    }

    public async Task checkStream()
    {
      var recordButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Record\")");
      await Task.Run(() => Assert.IsTrue(recordButton.IsVisibleAsync().Result));
    }

    public async Task updateToPastDate()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Session Date\"]").ClickAsync();
      if (month != yesterMonth)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Previous Month\"]").ClickAsync();
      }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{yesterday} {yesterMonth} {yesterDateNum} {yesterYear}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task pastDateErrorMessage()
    {
      var error = Page.Locator("text=Date cannot be in the past.");
      await Task.Run(() => Assert.That(error.TextContentAsync().Result, Does.Contain("Date cannot be in the past.")));
    }

    public async Task RemoveCourt()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Courts\\. Selected\\: Leeds\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Remove Leeds from selection\"]").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo Dev Home Manage Recordings Court Court NameOpen popup to select items").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

    }

    public async Task checkErrorMessageSaveButton()
    {
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var message = Page.Locator("text=Please ensure all fields are completed.");
      await message.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(message.IsVisibleAsync().Result));
    }


    public async Task UpdateRecording()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Toggle\"] div >> nth=2");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task updaterecordingConfirmationcheck()
    {
      var ConfirmationMessage = Page.Frame("fullscreen-app-host").Locator("text=Recording Updated");
      await Task.Run(() => Assert.That(ConfirmationMessage.TextContentAsync().Result, Does.Contain("Recording Updated")));
    }

    public async Task createdCase()
    {
      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.canvasContentDiv.container_1vt1y2p div:nth-child(2)").First;
      await Task.Run(() => Assert.That(results.InnerTextAsync().Result, Does.Contain($"{caseRef}")));
    }

    public async Task checkVersionNumber()
    {
      var version = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=V.1");
      await Task.Run(() => Assert.IsTrue(version.IsVisibleAsync().Result));
    }

    public async Task DeleteRecording()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
    }

    public async Task DeleterecordingConfirmationcheck()
    {
      var ConfirmationMessage = Page.Frame("fullscreen-app-host").Locator("text= Scheduled Recording Deleted");
      await Task.Run(() => Assert.That(ConfirmationMessage.TextContentAsync().Result, Does.Contain("Scheduled Recording Deleted")));
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task noRecordingCheck()
    {
      var amendButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")");
      var manageButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")");
      var recordButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Record\")");
      var status = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p div:nth-child(10)");

      await Task.Run(() => Assert.IsTrue(amendButton.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(manageButton.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(recordButton.IsVisibleAsync().Result));
      await Task.Run(() => Assert.That(status.InnerTextAsync().Result, Does.Contain("No Recording")));
    }

    public async Task startRecording()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Record\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_1lj5p80").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("ul[role=\"listbox\"] div:has-text(\"PRE007\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Start Recording\")").ClickAsync();

      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.WaitForLoadStateAsync();
    }

    public async Task recordingStartedCheck()
    {
      var amendButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")");
      var recordButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Record\")");
      var viewButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View\")");
      var status = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p div:nth-child(10)");
      var finishButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Finish\")");
      var checkButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Check Stream\")");
      var rtmps = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search Recording ID\"]").Nth(2);
      var manageButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")");

      var time = (DateTime.UtcNow);
      var futureTime = (DateTime.UtcNow).AddMinutes(7);

      while (rtmps.IsVisibleAsync().Result == false) { if (time > futureTime){break;}}

      await rtmps.WaitForAsync();
      await viewButton.WaitForAsync();
      await manageButton.WaitForAsync();
      await finishButton.WaitForAsync();
      await checkButton.WaitForAsync();
      await status.WaitForAsync();

      await Task.Run(() => Assert.IsTrue(viewButton.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(manageButton.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(finishButton.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(checkButton.IsVisibleAsync().Result));
      await Task.Run(() => Assert.That(status.InnerTextAsync().Result, Does.Contain("Ready to stream")));

      await Task.Run(() => Assert.IsFalse(amendButton.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsFalse(recordButton.IsVisibleAsync().Result));

      await Task.Run(() => Assert.IsTrue(rtmps.IsVisibleAsync().Result));
      await Task.Run(() => Assert.That(rtmps.InputValueAsync().Result, Does.Contain("rtmps")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Ok\")").First.ClickAsync();
      await finishButton.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Yes\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task manageRecordingCheck()
    {
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var message= Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=There are no recordings matching your search criteria. Consider changing or remo");
      await message.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(message.IsVisibleAsync().Result));
      
    }
    public async Task schedulePageCheck()
    {
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Home\")").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Leeds\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{ManageRecording.caseRef}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var exists = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First;
      await exists.ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var box = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Booked Recordings\"] div").Nth(1);
      await Task.Run(() => Assert.That(box.InnerTextAsync().Result, Does.Not.Contain("wit1")));
    }
     public async Task adminCheck()
    {
      
    var month = DateTime.UtcNow.ToString("MM");
    
    var year = DateTime.UtcNow.ToString("yyyy");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
        
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").FillAsync($"{ManageRecording.caseRef}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Court: Leeds").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Schedule Date: {date}/{month}/{year}").ClickAsync();
        
        var deleted= Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Recording Status: Deleted");
        await deleted.WaitForAsync();
        await Task.Run(() => Assert.IsTrue(deleted.IsVisibleAsync().Result));
      }
    }
  }
