using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using NUnit.Framework;
using System;

namespace pre.test.Hooks
{

  [Binding]
  public class HooksUpdateBookedRecording
  {
    protected string day = DateTime.UtcNow.ToString("ddd");
    protected string month = DateTime.UtcNow.ToString("MMM");
    protected string datee = DateTime.UtcNow.ToString("dd");
    protected string year = DateTime.UtcNow.ToString("yyyy");

    [BeforeScenario("createCase", Order = 1)]
    public async Task goToUpdateBookedRecording()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      var date = DateTime.UtcNow.ToString("MMddmmss");
      UpdateBookedRecording.stringCase = $"AutoT{date}";
      
      Hooks.HooksInitializer.caseref = UpdateBookedRecording.stringCase;

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{UpdateBookedRecording.stringCase}");

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{UpdateBookedRecording.stringCourt}\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", $"{UpdateBookedRecording.def1},\n{UpdateBookedRecording.def2}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]",
          $"{UpdateBookedRecording.wit1},\n{UpdateBookedRecording.wit2}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      HooksInitializer.caseCount++;

      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {datee}\\ {year}\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"{UpdateBookedRecording.wit1}\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{UpdateBookedRecording.def1}\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");

      HooksInitializer.scheduleCount++;

      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");

      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{UpdateBookedRecording.stringCourt}\")");

      var caseInput = HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await HooksInitializer._context.Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{UpdateBookedRecording.stringCase.Trim()}");

      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
  }
}