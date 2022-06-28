using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using NUnit.Framework;
using Microsoft.Playwright;

namespace pre.test.Hooks
{
  [Binding]
  public class HooksExternalPortal
  {
    [BeforeScenario("AddingAndRemovingParticipant", Order = 1)]
    public async Task AddingAndRemovingParticipant()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("ul[role=\"listbox\"] div:has-text(\"Leeds\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{ExternalPortal.caseRef}");

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").FillAsync("def1");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").PressAsync("Tab");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("wit1");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(1).ClickAsync();

      Hooks.HooksInitializer.caseRef.Add(ExternalPortal.caseRef);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Scheduled Start DateOpen calendar to select a date\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{ExternalPortal.day}\\ {ExternalPortal.month}\\ {ExternalPortal.dateNum}\\ {ExternalPortal.year}\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("li[role=\"option\"] div:has-text(\"wit1 wit1\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=def1 def1").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").First.ClickAsync();
      HooksInitializer.scheduleCount++;
      HooksInitializer.recordings.Add(ExternalPortal.caseRef);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{ExternalPortal.caseRef}");
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      var results = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Case Reference\"]");
      await Task.Run(() => Assert.That(results.InputValueAsync().Result, Does.Contain($"{ExternalPortal.caseRef}")));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Share\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Find Users to Share Recording\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={ExternalPortal.emailToShare}").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo Dev Home Manage Recordings Court Court NameOpen popup to select items").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Grant Access\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    [BeforeScenario("SharedRecordingAtPortal", Order = 1)]
    public async Task SharedRecordingAtPortal()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").FillAsync($"{ExternalPortal.caseName}");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0")); ;

      var witness = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(5)");
      ExternalPortal.witnessName = witness.TextContentAsync().Result.ToString().Trim();
      ExternalPortal.witnessName = ExternalPortal.witnessName.Substring(ExternalPortal.witnessName.LastIndexOf(':') + 1);

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(11) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Share\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Find Users to Share Recording\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Find items\"]").FillAsync($"{ExternalPortal.emailToShare}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role='option'] >> text={ExternalPortal.emailToShare}").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Version: 1").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Grant Access\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var shareBox = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings Gallery\"] div").Nth(1);
      await Task.Run(() => Assert.That(shareBox.TextContentAsync().Result.Trim(), Does.Contain($"{ExternalPortal.emailToShare}")));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Home\")").ClickAsync();

      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testPortalUrl}");

      var checkLogin = HooksInitializer._context.Page.Locator("text=Signin");
      var flag = await Task.Run(() => (checkLogin.IsVisibleAsync().Result));
      if (flag == true) { await PortalLogin(); }
      while (HooksInitializer._context.Page.Locator("text=Please note: Playback is preferred on non-mobile devices. If possible, please us").IsVisibleAsync().Result == false) { }

      var tableCaseRef = HooksInitializer._context.Page.Locator($"text={ExternalPortal.caseName}");
      await Task.Run(() => Assert.IsTrue(tableCaseRef.IsVisibleAsync().Result));
      await Task.Run(() => tableCaseRef.ClickAsync());
      var mobileWarning = HooksInitializer._context.Page.Locator("text=Please note: Playback is preferred on non-mobile devices. If possible, please us");
      await Task.Run(() => Assert.That(mobileWarning.TextContentAsync().Result, Does.Contain("Playback is preferred on non-mobile devices")));
    }

    [BeforeScenario("createAccount", Order = 1)]
    public async Task createAccount()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Users\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add Users\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).FillAsync("testacc");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).PressAsync("Tab");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).FillAsync("2");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).PressAsync("Tab");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").FillAsync($"{ExternalPortal.badPwPortalEmail}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").PressAsync("Tab");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).FillAsync("o");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).PressAsync("Tab");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).FillAsync("o");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).PressAsync("Tab");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#react-combobox-view-5").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=External Access via Portal").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Add New User\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.deleteContactsUrlTest}");
      await HooksInitializer._context.Page.Locator("button:has-text(\"+221 more\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("text=(Select All) >> div").Nth(2).ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("email");
      await HooksInitializer._context.Page.Locator(".ms-Checkbox-checkmark").First.ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("email conf");
      await HooksInitializer._context.Page.Locator("text=").ClickAsync();
      await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();

      await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Email\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("div[role=\"button\"]:has-text(\"Email\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("[aria-label=\"Filter by\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[aria-label=\"Filter by value\"]").FillAsync($"{ExternalPortal.badPwPortalEmail}");
      await HooksInitializer._context.Page.Locator("button:has-text(\"Apply\")").ClickAsync();

      await HooksInitializer._context.Page.Locator("[aria-label=\"\\$Email Confirmed\\, row 0\"]").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.deleteContactsUrlTest}");
      await HooksInitializer._context.Page.Locator("button:has-text(\"+223 more\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("text=(Select All) >> div").Nth(2).ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("email");
      await HooksInitializer._context.Page.Locator(".ms-Checkbox-checkmark").First.ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("lockout enabled");
      await HooksInitializer._context.Page.Locator("text=").ClickAsync();
      await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("[aria-label=\"\\$Lockout Enabled\\, row 0\"]").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.deleteContactsUrlTest}");
      await HooksInitializer._context.Page.Locator("button:has-text(\"+223 more\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("text=(Select All) >> div").Nth(2).ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("email");
      await HooksInitializer._context.Page.Locator(".ms-Checkbox-checkmark").First.ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("login enabled");
      await HooksInitializer._context.Page.Locator("text=").ClickAsync();
      await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("[aria-label=\"\\$Login Enabled\\, row 0\"]").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.deleteContactsUrlTest}");
      await HooksInitializer._context.Page.Locator("button:has-text(\"+223 more\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("text=(Select All) >> div").Nth(2).ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("email");
      await HooksInitializer._context.Page.Locator(".ms-Checkbox-checkmark").First.ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").ClickAsync();
      await HooksInitializer._context.Page.Locator("[placeholder=\"Search\"]").FillAsync("two factor enabled");
      await HooksInitializer._context.Page.Locator("text=").ClickAsync();
      await HooksInitializer._context.Page.Locator("button:has-text(\"Save\")").ClickAsync();
      await HooksInitializer._context.Page.Locator("[aria-label=\"\\$Two Factor Enabled\\, row 0\"]").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    [AfterScenario("unSharedRecordingAtPortal", Order = 0)]
    public async Task UnsharedRecordingAtPortal()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").FillAsync($"{ExternalPortal.caseName}");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0")); ;


      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(11) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0")); ;


      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Item 1. Selected. {ExternalPortal.emailToShare} >> [aria-label=\"Cases Gallery\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Remove Access\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0")); ;

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Close\\ Manage\\ Sessions\"]").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(11) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      var emailsSharedWith = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings Gallery\"] div").Nth(1);
      await Task.Run(() => Assert.That(emailsSharedWith.TextContentAsync().Result, Does.Not.Contain($"{ExternalPortal.emailToShare}")));
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.testPortalUrl}");

      var checkLogin = HooksInitializer._context.Page.Locator("text=Signin");
      var flag = await Task.Run(() => (checkLogin.IsVisibleAsync().Result));
      if (flag == true) { await PortalLogin(); }
      while (HooksInitializer._context.Page.Locator("text=Please note: Playback is preferred on non-mobile devices. If possible, please us").IsVisibleAsync().Result == false) { }

      await HooksInitializer._context.Page.IsVisibleAsync("text=Welcome to the Pre-recorded Evidence Portal‌‌");
      var tableCaseRef = HooksInitializer._context.Page.Locator($"text={ExternalPortal.caseName}");
      await Task.Run(() => Assert.IsFalse(tableCaseRef.IsVisibleAsync().Result));
    }

    [AfterScenario("unlockAccount", Order = 1)]
    public async Task unlockAccount()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.deleteContactsUrlTest}");
      await HooksInitializer._context.Page.Locator("[aria-label=\"select or deselect the row\"]").First.ClickAsync();
      await HooksInitializer._context.Page.Locator("button[role=\"menuitem\"]:has-text(\"Delete 1 record(s)\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
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