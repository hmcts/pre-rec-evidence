using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using pre.test.Hooks;
using Microsoft.Extensions.Configuration;

namespace pre.test.pages
{
  public class EndToEnd : BasePage
  {
    protected static Microsoft.Extensions.Configuration.IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("secrets.json")
    .Build();
    public EndToEnd(IPage page) : base(page) { }
    public static string day = DateTime.UtcNow.ToString("ddd");
    public static string month = DateTime.UtcNow.ToString("MMM");
    public static string datee = DateTime.UtcNow.ToString("dd");
    public static string year = DateTime.UtcNow.ToString("yyyy");
    public static String stringCourt = "Leeds";
    public static String stringCase = "";
    public static string wit1 = "Witness surname1";
    public static string wit2 = "Witness surname2";
    public static string def1 = "defendants 1";
    public static string def2 = "defendants 2";
    public static string stringRtmps = "";
    public static string cvpHostLink = config["cvpHostLink"];
    public static string cvpLogInUsername = config["cvpLogInUsername"];
    public static string cvpLogInPassword = config["cvpLogInPassword"];
    public static string cvpService = config["cvpService"];
    public static string cvpLocation = config["cvpLocation"];
    public static string cvpJoinLink = config["cvpJoinLink"];
    public static string cvpRoom = config["cvpRoom"];
    public static string cvpHostPin = config["cvpHostPin"];
    public async Task createCaseSched()
    {
      await Page.GotoAsync($"{HooksInitializer.testUrl}");
      await Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Book a Recording\")").WaitForAsync();
      await Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Book a Recording\")").ClickAsync();

      var date = DateTime.UtcNow.ToString("MMddmmss");
      stringCase = $"AutoE{date}";
      await Page.Frame("fullscreen-app-host").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{stringCase}");

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ Court\"]").First.ClickAsync();
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", $"{def1},\n{def2}");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]", $"{wit1},\n{wit2}");
      await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
      HooksInitializer.contacts.Add(def1);
      HooksInitializer.contacts.Add(def2);
      HooksInitializer.contacts.Add(wit1);
      HooksInitializer.contacts.Add(wit2);
      HooksInitializer.caseRef.Add(stringCase);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {datee}\\ {year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");

      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={wit1}").ClickAsync();
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{def1}\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      HooksInitializer.scheduleCount++;
      HooksInitializer.recordings.Add(stringCase);

      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
    public async Task getRtmps()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{stringCase}");
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Case Reference\"]");
      await results.WaitForAsync();

      var time = (DateTime.UtcNow);
      var futureTime = (DateTime.UtcNow).AddMinutes(7);

      while (results.InputValueAsync().Result != ManageRecording.caseRef) { if (time > futureTime) { break; } }
      await Task.Run(() => Assert.That(results.InputValueAsync().Result, Does.Contain($"{ManageRecording.caseRef}")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Close Amend Recording\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Record\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_1lj5p80").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("ul[role=\"listbox\"] div:has-text(\"PRE009\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Start Recording\")").ClickAsync();

      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.WaitForLoadStateAsync();

      var rtmps = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search Recording ID\"]").Nth(2);
      var time2 = (DateTime.UtcNow);
      var futureTime2 = (DateTime.UtcNow).AddMinutes(7);

      while (rtmps.IsVisibleAsync().Result == false) { if (time2 > futureTime2) { break; } }
      await Task.Run(() => Assert.That(rtmps.InputValueAsync().Result, Does.Contain("rtmps")));
      stringRtmps = rtmps.InputValueAsync().Result.Trim();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Ok\")").First.ClickAsync();
    }
    public async Task startRecording()
    {
      await Page.GotoAsync($"{cvpHostLink}");
      await Page.Locator("[placeholder=\"Username\"]").WaitForAsync();
      await Page.Locator("[placeholder=\"Username\"]").FillAsync($"{cvpLogInUsername}");
      await Page.Locator("[placeholder=\"Password\"]").FillAsync($"{cvpLogInPassword}");
      await Page.Locator("text=Sign in").ClickAsync();

      await Page.Locator("#modal_cloudroom >> text=×").WaitForAsync();
      await Page.Locator("#modal_cloudroom >> text=×").ClickAsync();
      await Page.Locator("a[role=\"button\"]:has-text(\"Pre Recorded Evidence Team's room\")").ClickAsync();
      await Page.Locator("text=PRE009").ClickAsync();

      await Page.Locator("text=Edit room settings").WaitForAsync();
      await Page.Locator("text=Edit room settings").ClickAsync();
      await Page.Locator("input[name=\"recording_uri\"]").FillAsync($"{stringRtmps}");
      await Page.Locator("button:has-text(\"Save\")").ClickAsync();
      await Page.Locator("a[role=\"button\"]:has-text(\"Record\")").ClickAsync();

      await Page.Locator("[placeholder=\"Service ID\"]").FillAsync($"{cvpService}");
      await Page.Locator("[placeholder=\"Location code\"]").FillAsync($"{cvpLocation}");
      await Page.Locator("[placeholder=\"Case ID\"]").FillAsync($"{stringCase}");
      await Page.Locator("[aria-label=\"Ok\"]").ClickAsync();
      await Page.Locator("[aria-label=\"Save\"]").ClickAsync();
      await Page.Locator("input:has-text(\"Close\")").ClickAsync();
      await Page.Locator("a[role=\"button\"]:has-text(\"Record\")").ClickAsync();

      await Task.Run(() => Assert.IsTrue(Page.Locator("a[role=\"button\"]:has-text(\"Starting\")").IsVisibleAsync().Result));

      await Page.GotoAsync($"{cvpJoinLink}");
      await Page.Locator("#conference-alias").FillAsync($"{cvpRoom}");
      await Page.Locator("[placeholder=\"Enter your name\"]").FillAsync($"automatedTest - {stringCase}");
      await Page.Locator("button:has-text(\"Connect\")").ClickAsync();
      await Page.Locator("select").Nth(1).SelectOptionAsync(new[] { "boolean:false" });
      await Page.Locator("button:has-text(\"Start\")").ClickAsync();
      await Page.Locator("input[type=\"password\"]").FillAsync($"{cvpHostPin}");
      await Page.Locator("form[name=\"pinForm\"] button:has-text(\"Connect\")").ClickAsync();
    }
    public async Task livestreamCheck()
    {
      await Page.GotoAsync($"{HooksInitializer.testUrl}");

    }
    public async Task finishRecording()
    {

    }
    public async Task checkView()
    {

    }
    public async Task viewRecording()
    {

    }
    public async Task shareRecording()
    {

    }
    public async Task viewInPortal()
    {

    }
    public async Task unshareRecording()
    {

    }
    public async Task noViewPortal()
    {

    }

  }
}