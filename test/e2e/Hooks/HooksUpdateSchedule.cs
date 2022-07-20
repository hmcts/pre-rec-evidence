using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using System;
namespace pre.test.Hooks

{
  [Binding]
  public class HooksUpdateSchedule
  {

    [BeforeScenario("UpdatingSchedule", Order = 2)]
    public async Task goToUpdateSchedule()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").WaitForAsync();
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");

      UpdateSchedule.CaseRefDate = DateTime.UtcNow.ToString("MMddmmss");
      UpdateSchedule.stringCase = $"AutoT{UpdateSchedule.CaseRefDate}";

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case Number \\\\ URN\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case Number \\\\ URN\"]", $"{UpdateSchedule.stringCase}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{UpdateSchedule.court}\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", $"{UpdateSchedule.def1},\n{UpdateSchedule.def2}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]", $"{UpdateSchedule.wit1},\n{UpdateSchedule.wit2}");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
      HooksInitializer.caseRef.Add(UpdateSchedule.stringCase);
      HooksInitializer.contacts.Add(UpdateSchedule.def1);
      HooksInitializer.contacts.Add(UpdateSchedule.def2);
      HooksInitializer.contacts.Add(UpdateSchedule.wit1);
      HooksInitializer.contacts.Add(UpdateSchedule.wit2);

      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{UpdateSchedule.currentday}\\ {UpdateSchedule.currentmonthword}\\ {UpdateSchedule.currentdate}\\ {UpdateSchedule.currentyear}\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"{UpdateSchedule.wit1}\")");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await HooksInitializer._context.Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{UpdateSchedule.def1}\")");
    }
  }
}