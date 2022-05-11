using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using NUnit.Framework;

namespace pre.test.Hooks
{
  [Binding]
  public class HooksAdminAdminManageCases
  {

    [BeforeScenario("AdminManageCases", Order = 1)]
    public async Task goToAdminAdminManageCase()
    {
      // using sandbox url whilst test is aligned, change in future
      await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
    }

    [AfterScenario("RevertCourt", Order = 0)]
    public async Task RevertCourt()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case ID: {AdminManageCase.caseId} Case Ref: {AdminManageCase.caseRef} Court: {AdminManageCase.newCourt} >> [aria-label=\"Edit Case\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {AdminManageCase.newCourt}\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{AdminManageCase.court}\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(119) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      var courtLocator = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First;
      await Task.Run(() => Assert.That(courtLocator.InnerTextAsync().Result, Does.Contain($"{AdminManageCase.court}")));
    }

    [AfterScenario("RevertDate", Order = 0)]
    public async Task RevertDate()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Item 1. Selected. Case ID: {AdminManageCase.caseId} Case Ref: {AdminManageCase.caseRef}").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Schedule ID: {AdminManageCase.scheduleId} Schedule Date: {AdminManageCase.tomoDateNum}/{AdminManageCase.tomoMonth}/{AdminManageCase.tomoYear} >> [aria-label=\"Edit Schedule\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Schedule Date{AdminManageCase.tomoDateNum}\\/{AdminManageCase.tomoMonth}\\/{AdminManageCase.tomoYear}\\. Open calendar to select a date\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{AdminManageCase.today} {AdminManageCase.monthWord} {AdminManageCase.dateNum} {AdminManageCase.year}\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(122) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      var scheduleDateLocator = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(46)  div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)").First;
      await Task.Run(() => Assert.That(scheduleDateLocator.InnerTextAsync().Result, Does.Contain($"{AdminManageCase.dateNum}/{AdminManageCase.month}/{AdminManageCase.year}")));
    }
  }
}