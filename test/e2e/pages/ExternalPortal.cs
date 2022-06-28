using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace pre.test.pages
{
  public class ExternalPortal : BasePage
  {
    protected static Microsoft.Extensions.Configuration.IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("secrets.json")
    .Build();
    public ExternalPortal(IPage page) : base(page) { }
    public static string use = "";
    public static string date = DateTime.UtcNow.ToString("MMddmmss");
    public static string emailToShare = config["portalEmail"];
    public static string emailPassword = config["portalPassword"];
    public static string FAemailToShare = config["2FAportalEmail"];
    public static string FAemailPassword = config["2FAportalPassword"];
    public static string badPwPortalEmail = config["badPwPortalEmail"];
    public static string caseName = "NODELETEPLS";
    public static string witnessName = "null";
    public static string recordingUID = "null";
    protected string UpdatedWitnessName = "null";
    protected string courtName = "null";
    public static string caseRef = $"AutoT{date}";
    public static string day = DateTime.UtcNow.ToString("ddd");
    public static string month = DateTime.UtcNow.ToString("MMM");
    public static string dateNum = DateTime.UtcNow.ToString("dd");
    public static string year = DateTime.UtcNow.ToString("yyyy");
    public async Task ViewRecording()
    {
      var tableCaseRef = Page.Locator($"text={caseName}");
      await Task.Run(() => Assert.IsTrue(tableCaseRef.IsVisibleAsync().Result));
      await Task.Run(() => tableCaseRef.ClickAsync());
      var mobileWarning = Page.Locator("text=Please note: Playback is preferred on non-mobile devices. If possible, please us");
      await Task.Run(() => Assert.That(mobileWarning.TextContentAsync().Result, Does.Contain("Playback is preferred on non-mobile devices")));
    }
    public async Task checkUnshared()
    {
      var tableCaseRef = Page.Locator($"text={caseName}");
      await Task.Run(() => Assert.IsFalse(tableCaseRef.IsVisibleAsync().Result));
    }

    public async Task CheckSharedRecordings()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").FillAsync($"{caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(11) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var emailsSharedWith = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings Gallery\"] div").Nth(1);
      var text = emailsSharedWith.TextContentAsync().Result;
      if (text.Contains($"{emailToShare}"))
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Item 1. Selected. {ExternalPortal.emailToShare} >> [aria-label=\"Cases Gallery\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Remove Access\")").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }
      await Task.Run(() => Assert.That(emailsSharedWith.TextContentAsync().Result, Does.Not.Contain($"{emailToShare}")));
    }
    public async Task checkWitnesses()
    {
      var table = Page.Locator(".xrm-attribute-value div:nth-child(4)");
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Contain($"{witnessName.Trim()}")));
    }
    public async Task checkRecordingUID()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https"));
      var table = Page.Locator(".xrm-attribute-value div:nth-child(4)");
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Contain($"{recordingUID.Trim()}")));
    }

    public async Task updateWitnesses()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]").FillAsync($"{caseName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("ul[role=\"listbox\"] div:has-text(\"Leeds\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync($"automated 2 {date}");
      UpdatedWitnessName = $"automated 2 {date}";
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      var checkWitnesses = Page.Locator(".container_1f0sgyp div .react-knockout-control .appmagic-svg");
      await Task.Run(() => Assert.That(checkWitnesses.InnerTextAsync().Result, Does.Not.Contain($"{witnessName.Trim()}")));
      await Task.Run(() => Assert.That(checkWitnesses.InnerTextAsync().Result, Does.Contain($"{UpdatedWitnessName.Trim()}")));
    }

    public async Task checkUpdatedWitnesses()
    {
      var table = Page.Locator(".xrm-attribute-value div:nth-child(4)");
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Not.Contain($"{witnessName.Trim()}")));
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Contain($"{UpdatedWitnessName.Trim()}")));
    }

    public async Task checkCourt()
    {
      var table = Page.Locator(".xrm-attribute-value div:nth-child(4)");
      await Task.Run(() => Assert.That(table.InnerTextAsync().Result, Does.Contain($"{courtName.Trim()}")));
    }

    public async Task goToManageCases()
    {

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
    }

    public async Task findRecordingId()
    {
      var test2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".react-gallery-items-window").First;
      var test = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {caseName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {caseName}");
      await Task.Run(() => Assert.IsTrue(results.IsVisibleAsync().Result));
      await results.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(48)  div.virtualized-gallery > div > div > div").ClickAsync();
      recordingUID = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"File\\ path\\.\\.\\.\"]").First.InputValueAsync().Result;
    }

    public async Task removeAccess()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Cases Gallery\"]").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Remove Access\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task checkRemoved()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Manage\")").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var check = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings Gallery\"] div").Nth(1);
      await Task.Run(() => Assert.That(check.InnerTextAsync().Result, Does.Not.Contain($"{emailToShare}")));
    }

    public async Task NoRecordingsMessage()
    {
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var errorMessage = Page.Locator("text=No records found");
      await Task.Run(() => Assert.IsTrue(errorMessage.IsVisibleAsync().Result));
    }

    public async Task enterWrongLogIn()
    {
      await Page.Locator("input[name=\"Email\"]").ClickAsync();
      await Page.Locator("input[name=\"Email\"]").FillAsync($"{badPwPortalEmail}");
      await Page.Locator("input[name=\"PasswordValue\"]").ClickAsync();
      await Page.Locator("input[name=\"PasswordValue\"]").FillAsync("wrongmwahahah");
      await Page.Locator("input[name=\"PasswordValue\"]").PressAsync("Enter");
    }

    public async Task enterWrong2FA()
    {
      await Page.Locator("input[name=\"Email\"]").FillAsync($"{FAemailToShare}");
      await Page.Locator("input[name=\"PasswordValue\"]").FillAsync($"{FAemailPassword}");
      await Page.Locator("input[name=\"PasswordValue\"]").PressAsync("Enter");
      await Page.Locator("input[name=\"Code\"]").FillAsync("hahaimwrong");
      await Page.Locator("text=Verify").ClickAsync();
    }

    public async Task errorMessage()
    {
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var error = "";
      if (use == "pw")
      {
        error = "Invalid sign-in attempt.";
      }
      else if (use == "locked")
      {
        error = "The user account is currently locked. Please try again later.";
      }
      else
      {
        error = "Invalid code.";
      }
      var message = Page.Locator($"text={error}");
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await message.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(message.IsVisibleAsync().Result));
    }

    public async Task invalidFiveTimes()
    {
      for (int i = 0; i < 4; i++)
      {
        if (use == "pw")
        {
          await Page.Locator("input[name=\"Email\"]").ClickAsync();
          await Page.Locator("input[name=\"Email\"]").FillAsync($"{badPwPortalEmail}");
          await Page.Locator("input[name=\"PasswordValue\"]").ClickAsync();
          await Page.Locator("input[name=\"PasswordValue\"]").FillAsync($"{badPwPortalEmail}wrongmwahahah");
          await Page.Locator("input[name=\"PasswordValue\"]").PressAsync("Enter");
        }
        else
        {
          await Page.Locator("input[name=\"Code\"]").ClickAsync();
          await Page.Locator("input[name=\"Code\"]").FillAsync("hahaimwrong");
          await Page.Locator("text=Verify").ClickAsync();
        }

        if (i != 3)
        {
          await errorMessage();
        }
        else
        {
          use = "locked";
        }
      }
    }
  }
}


