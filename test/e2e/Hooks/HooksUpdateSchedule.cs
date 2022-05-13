using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using System;
namespace pre.test.Hooks

{
  [Binding]
  public class HooksUpdateSchedule
  {

    [BeforeScenario("UpdatingSchedule", Order = 1)]
    public async Task goToUpdateSchedule()
    {
      // using sandbox url whilst test is aligned, change in future
      await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      var book = HooksInitializer._context.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Book a Recording\")");

      await Task.Run(() => book.IsVisibleAsync().Result);
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      UpdateSchedule.CaseRefDate = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
      UpdateSchedule.stringCase = $"ScheduleUpdateAutoTest{UpdateSchedule.CaseRefDate}";



      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{UpdateSchedule.stringCase}");

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{UpdateSchedule.court}\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "defendants 1,\ndefendants 2");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]",
          "Witness surname1,\nWitness surname2");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{UpdateSchedule.currentday}\\ {UpdateSchedule.currentmonthword}\\ {UpdateSchedule.currentdate}\\ {UpdateSchedule.currentyear}\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"Witness surname1\")");
      
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"defendants 1\")");
    }
  }
}