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


    [BeforeScenario("createCase", Order = 1)]
    public async Task goToAdminManageRecordings()
    {
      // using sandbox url whilst test is aligned, change in future
      await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      var date = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
      UpdateBookedRecording.stringCase = $"UpdateAutoTest{date}";

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
      var day = DateTime.UtcNow.ToString("ddd");
      var month = DateTime.UtcNow.ToString("MMM");
      var datee = DateTime.UtcNow.ToString("dd");
      var year = DateTime.UtcNow.ToString("yyyy");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host")
          .ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {datee}\\ {year}\"]");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host")
          .ClickAsync($"[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"{UpdateBookedRecording.wit1}\")");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host")
          .ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{UpdateBookedRecording.def1}\")");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
        await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
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
        var caseLocation = HooksInitializer._context.Page.Frame("fullscreen-app-host")
          .Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
        await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{UpdateBookedRecording.stringCourt.Trim()}")));

        var caseName = HooksInitializer._context.Page.Frame("fullscreen-app-host").Locator(
          "div:nth-child(45) div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1) ");
        await Task.Run(() => Assert.That(caseName.InnerTextAsync().Result, Does.Contain($"{UpdateBookedRecording.stringCase.Trim()}")));
    }
  }
}