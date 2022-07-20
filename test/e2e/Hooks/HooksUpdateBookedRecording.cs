using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using NUnit.Framework;
using System;
using Microsoft.Playwright;

namespace pre.test.Hooks
{

  [Binding]
  public class HooksUpdateBookedRecording
  {
    public static string day = DateTime.UtcNow.ToString("ddd");
    public static string month = DateTime.UtcNow.ToString("MMM");
    public static string datee = DateTime.UtcNow.ToString("dd");
    public static string year = DateTime.UtcNow.ToString("yyyy");

    [BeforeScenario("createCase", Order = 2)]
    public async Task goToUpdateBookedRecording()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Book a Recording\")").WaitForAsync();
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Book a Recording\")").ClickAsync();

      var date = DateTime.UtcNow.ToString("MMddmmss");
      UpdateBookedRecording.stringCase = $"AutoT{date}";
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.WaitForAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{UpdateBookedRecording.stringCase}");

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ Court\"]").First.ClickAsync();
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{UpdateBookedRecording.stringCourt}\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", $"{UpdateBookedRecording.def1},\n{UpdateBookedRecording.def2}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]", $"{UpdateBookedRecording.wit1},\n{UpdateBookedRecording.wit2}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
      HooksInitializer.contacts.Add(UpdateBookedRecording.def1);
      HooksInitializer.contacts.Add(UpdateBookedRecording.def2);
      HooksInitializer.contacts.Add(UpdateBookedRecording.wit1);
      HooksInitializer.contacts.Add(UpdateBookedRecording.wit2);
      HooksInitializer.caseRef.Add(UpdateBookedRecording.stringCase);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {datee}\\ {year}\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");

      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={UpdateBookedRecording.wit1}").ClickAsync();
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{UpdateBookedRecording.def1}\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      HooksInitializer.scheduleCount++;
      HooksInitializer.recordings.Add(UpdateBookedRecording.stringCase);

      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ Court\"]").First.WaitForAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ Court\"]").First.ClickAsync();
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{UpdateBookedRecording.stringCourt}\")");

      var caseInput = HooksInitializer._context.Page.Frame("fullscreen-app-host").Locator("[placeholder=\"Case Number \\\\ URN\"]").First;
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.IsVisibleAsync();

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{UpdateBookedRecording.stringCase.Trim()}");
    }
  }
}