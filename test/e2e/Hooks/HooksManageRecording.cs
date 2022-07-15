using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using NUnit.Framework;
using System;
using Microsoft.Playwright;

namespace pre.test.Hooks
{

  [Binding]
  public class HooksManageRecording
  {
    [BeforeScenario("CaseAndScheduleCreate", Order = 1)]
    public async Task createAndScheduleCase()
    {
      var date = DateTime.UtcNow.ToString("MMddmmss");
      ManageRecording.caseRef = $"AutoM{date}";

      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.sboxUrl}");
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").WaitForAsync();

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] div:has-text(\"{ManageRecording.courtName}\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{ManageRecording.caseRef}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").FillAsync("def1");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").PressAsync("Tab");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("wit1");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(1).ClickAsync();
      HooksInitializer.caseRef.Add(ManageRecording.caseRef);
      HooksInitializer.contacts.Add("def1");
      HooksInitializer.contacts.Add("wit1");
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Scheduled Start DateOpen calendar to select a date\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{ManageRecording.day}\\ {ManageRecording.month}\\ {ManageRecording.date}\\ {ManageRecording.year}\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("li[role=\"option\"] div:has-text(\"wit1 wit1\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=def1 def1").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").First.ClickAsync();
      HooksInitializer.scheduleCount++;
      HooksInitializer.recordings.Add(ManageRecording.caseRef);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).WaitForAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
    }

    [BeforeScenario("findCase", Order = 2)]
    public async Task findCase()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").WaitForAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{ManageRecording.caseRef}");
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.WaitForAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      var results = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Case Reference\"]");
      await results.WaitForAsync();

      var time = (DateTime.UtcNow);
      var futureTime = (DateTime.UtcNow).AddMinutes(7);

      while (results.InputValueAsync().Result != ManageRecording.caseRef) { if (time > futureTime) { break; } }
      await Task.Run(() => Assert.That(results.InputValueAsync().Result, Does.Contain($"{ManageRecording.caseRef}")));
    }


  }
}
