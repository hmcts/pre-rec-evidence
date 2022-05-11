using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using NUnit.Framework;

namespace pre.test.Hooks{
  [Binding]
  public class HooksExternalPortal
  {

     // there'll need to be a recording created through cvp called 'AUTOMATEDTESTRECORDINGDONOTDELETE' for these tests
    [BeforeScenario("SharedRecordingAtPortal", Order = 1)]
    public async Task SharedRecordingAtPortal()
    {
     await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9");
     await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{ExternalPortal.caseName}");

      var witness = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(5)");
      ExternalPortal.witnessName = witness.TextContentAsync().Result.ToString().Trim();
      ExternalPortal.witnessName = ExternalPortal.witnessName.Substring(ExternalPortal.witnessName.LastIndexOf(':') + 1);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Share\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Find Users to Share Recording\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Find items\"]").FillAsync($"{ExternalPortal.emailToShare}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role='option'] >> text={ExternalPortal.emailToShare}").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo Dev Home Manage Recordings Court Court NameOpen popup to select items").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Grant Access\")").ClickAsync();

      var shareBox = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings Gallery\"] div").Nth(1);
      await Task.Run(() => Assert.That(shareBox.TextContentAsync().Result.Trim(), Does.Contain($"{ExternalPortal.emailToShare}")));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Home\")").ClickAsync();
     
      await HooksInitializer._context.Page.GotoAsync("https://pre-test.powerappsportals.com/");
      var checkLogin = HooksInitializer._context.Page.Locator("text=Welcome to the Pre-recorded Evidence Portal‌‌...");
      var flag = await Task.Run(() => (checkLogin.IsVisibleAsync().Result));
      if (flag == false) { await PortalLogin(); }
      await HooksInitializer._context.Page.IsVisibleAsync("text=Welcome to the Pre-recorded Evidence Portal‌‌...");
    }

    [AfterScenario("SharedRecordingAtPortal", Order = 0)]
    public async Task UnsharedRecordingAtPortal(){
      await HooksInitializer._context.Page.GotoAsync("https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{ExternalPortal.caseName}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Recordings Gallery\"] div:has-text(\"Item 1. Selected. {ExternalPortal.emailToShare}\")").Nth(1).ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Cases Gallery\"]").ClickAsync();
      
      var emailsSharedWith = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings Gallery\"] div").Nth(1);
      await Task.Run(() => Assert.That(emailsSharedWith.TextContentAsync().Result, Does.Not.Contain($"{ExternalPortal.emailToShare}")));
      await HooksInitializer._context.Page.GotoAsync("https://pre-test.powerappsportals.com/");
      var checkLogin = HooksInitializer._context.Page.Locator("text=Welcome to the Pre-recorded Evidence Portal‌‌...");
      var flag = await Task.Run(() => (checkLogin.IsVisibleAsync().Result));
      if (flag == false) { await PortalLogin(); }
      await HooksInitializer._context.Page.IsVisibleAsync("text=Welcome to the Pre-recorded Evidence Portal‌‌...");
      var tableCaseRef = HooksInitializer._context.Page.Locator($"text={ExternalPortal.caseName}");
      await Task.Run(() => Assert.IsFalse(tableCaseRef.IsVisibleAsync().Result));
    }

    public static async Task PortalLogin()
    {
      await HooksInitializer._context.Page.Locator("input[name=\"Email\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("input[name=\"Email\"]").FillAsync($"{ExternalPortal.emailToShare}");
      await HooksInitializer._context.Page.Locator("input[name=\"PasswordValue\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("input[name=\"PasswordValue\"]").FillAsync($"{ExternalPortal.emailPassword}");
      await HooksInitializer._context.Page.Locator("input[name=\"PasswordValue\"]").PressAsync("Enter");
    }
  }
}