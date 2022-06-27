using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Globalization;
using pre.test.Hooks;

namespace pre.test.pages
{
  public class AdminManageCase : BasePage
  {
    public AdminManageCase(IPage page) : base(page) { }
    public static String caseRef = "";
    protected String caseIdToSearch = "";
    public static String court = "";
    public static String newCourt = "";
    public static String caseId = "";
    public static String scheduleId = "";
    public static String scheduleDateString = "";
    public static String newScheduleDate = "";
    public static DateTime scheduleDate;
    public static string recording = "NODELETEPLS";
    public static string tomoMonthWord = "";
    public static string tomorrow = "";
    public static string month = DateTime.UtcNow.ToString("MM");
    public static string tomoMonth = "";
    public static string dateNum = DateTime.UtcNow.ToString("dd");
    public static string tomoDateNum = "";
    public static string year = DateTime.UtcNow.ToString("yyyy");
    public static string tomoYear = "";
    public static string monthWord = "";
    public static string today = "";

    public async Task updateCourt()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");

      var caseRefLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(47) div.virtualized-gallery > div > div > div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p div:nth-child(2)");
      await caseRefLocator.WaitForAsync();
      caseRef = caseRefLocator.TextContentAsync().Result.ToString().Trim();
      caseRef = caseRef.Substring(caseRef.LastIndexOf(':') + 1).Trim();

      var courtLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(47) div.virtualized-gallery > div > div > div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p div:nth-child(3)").First;
      court = courtLocator.TextContentAsync().Result.ToString().Trim();
      court = court.Substring(court.LastIndexOf(':') + 1).Trim();

      var caseIdLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(47) div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)");
      caseId = caseIdLocator.TextContentAsync().Result.ToString().Trim();
      caseId = caseId.Substring(caseId.LastIndexOf(':') + 1).Trim();

      if (court == "Leeds") { newCourt = "Chris"; } else { newCourt = "Leeds"; }

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Case\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {court}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newCourt}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(121) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();
      await courtLocator.WaitForAsync();
      await Task.Run(() => Assert.That(courtLocator.InnerTextAsync().Result, Does.Contain($"{newCourt}")));
    }
    public async Task checkCourtManageRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{caseRef}");

      var caseInfo = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p div:nth-child(4)");
      await caseInfo.WaitForAsync();
      await Task.Run(() => Assert.That(caseInfo.AllInnerTextsAsync().Result, Does.Contain($"Court: {newCourt}")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      var courtDropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Courts\\. Selected\\: {newCourt}\"]");
      await Task.Run(() => Assert.IsTrue(courtDropdown.IsVisibleAsync().Result));
    }

    public async Task checkCourtBookRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] div:has-text(\"{newCourt}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{caseRef}");

      var caseInfo = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p  div:nth-child(3)").First;
      await caseInfo.WaitForAsync();
      await Task.Run(() => Assert.That(caseInfo.InnerTextAsync().Result, Does.Contain($"{newCourt}")));
    }
    public async Task updateDate()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var caseRefLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(47) div.virtualized-gallery > div > div > div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p div:nth-child(2)");
      caseRef = caseRefLocator.TextContentAsync().Result.ToString().Trim();
      caseRef = caseRef.Substring(caseRef.LastIndexOf(':') + 1).Trim();

      var courtLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(47) div.virtualized-gallery > div > div > div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p div:nth-child(3)").First;
      court = courtLocator.TextContentAsync().Result.ToString().Trim();
      court = court.Substring(court.LastIndexOf(':') + 1).Trim();

      var caseIdLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(47) div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)");
      caseId = caseIdLocator.TextContentAsync().Result.ToString().Trim();
      caseId = caseId.Substring(caseId.LastIndexOf(':') + 1).Trim();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div .react-knockout-control .appmagic-svg").First.ClickAsync();
      var scheduleIdLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(48)  div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)").First;
      scheduleId = scheduleIdLocator.TextContentAsync().Result.ToString().Trim();
      scheduleId = scheduleId.Substring(scheduleId.LastIndexOf(':') + 1).Trim();

      var scheduleDateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(48)  div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)").First;
      scheduleDateString = scheduleDateLocator.TextContentAsync().Result.ToString().Trim();
      scheduleDateString = scheduleDateString.Substring(scheduleDateString.LastIndexOf(':') + 1).Trim();

      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label='Edit Schedule']").WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label='Edit Schedule']").ClickAsync();

      scheduleDate = DateTime.Parse(scheduleDateString, CultureInfo.CurrentUICulture);
      dateNum = scheduleDate.ToString("dd");
      month = scheduleDate.ToString("MM");
      year = scheduleDate.ToString("yyyy");
      monthWord = scheduleDate.ToString("MMM");
      today = scheduleDate.ToString("ddd");

      tomorrow = (scheduleDate.AddDays(+1)).ToString("ddd");
      tomoMonthWord = (scheduleDate.AddDays(+1)).ToString("MMM");
      tomoMonth = (scheduleDate.AddDays(+1)).ToString("MM");
      tomoDateNum = (scheduleDate.AddDays(+1)).ToString("dd");
      tomoYear = (scheduleDate.AddDays(+1)).ToString("yyyy");

      newScheduleDate = $"{tomoDateNum}/{tomoMonth}/{tomoYear}";

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Schedule Date{dateNum}\\/{month}\\/{year}\\. Open calendar to select a date\"]").ClickAsync();

      if (month != tomoMonth)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
      }
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{tomorrow} {tomoMonthWord} {tomoDateNum} {tomoYear}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(128) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Task.Run(() => Assert.That(scheduleDateLocator.InnerTextAsync().Result, Does.Contain($"{newScheduleDate}")));
    }
    public async Task checkDateBookRecordings()
    {
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();

      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"]:has-text(\"{HooksAdminManageCases.court}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First.WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var recordingDateBook = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Recording Start: {newScheduleDate}");
      await Task.Run(() => Assert.IsTrue(recordingDateBook.IsVisibleAsync().Result));
    }
    public async Task checkDateManageRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Session Date\"]").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      var scheduleDateManage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{tomorrow} {tomoMonthWord} {tomoDateNum} {tomoYear}\\. Selected\\.\"]");
      await Task.Run(() => Assert.IsTrue(scheduleDateManage.IsVisibleAsync().Result));
    }
    public async Task checkCaseCreated()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Leeds").First.ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var exists = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First;
      await exists.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(exists.IsVisibleAsync().Result));
      await exists.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      caseIdToSearch = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(16)").InnerTextAsync().Result.ToString().Trim();
      caseIdToSearch = caseIdToSearch.Substring(caseIdToSearch.LastIndexOf(':') + 1).Trim();
    }
    public async Task search()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.ClickAsync();

      if (AdminManageCases.use == "caseRef")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");

        var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {HooksAdminManageCases.caseName}").First;
        await results.WaitForAsync();
        await Task.Run(() => Assert.IsTrue(results.IsVisibleAsync().Result));
      }
      else if (AdminManageCases.use == "caseId")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{caseIdToSearch}");

        var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {HooksAdminManageCases.caseName}");
        await results.WaitForAsync();
        await Task.Run(() => Assert.IsTrue(results.IsVisibleAsync().Result));
      }
      else if (AdminManageCases.use == "court")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").FillAsync("leeds");
        var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First;
        await results.WaitForAsync();
        await Task.Run(() => Assert.That(results.TextContentAsync().Result, Does.Contain("Leeds")));
      }
    }
    public async Task deleteCase()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Case\"]").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Close Case\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Case\"]").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var title = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=There are no scheduled Recordings for this Case.");
      await Task.Run(() => Assert.IsTrue(title.IsVisibleAsync().Result));

      var courtTitle = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Court: {HooksAdminManageCases.court}").First;
      await Task.Run(() => Assert.That(courtTitle.TextContentAsync().Result, Does.Contain($"{HooksAdminManageCases.court}")));

      var caseRefTitle = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label").First;
      await Task.Run(() => Assert.That(caseRefTitle.TextContentAsync().Result, Does.Contain($"{HooksAdminManageCases.caseName}")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").Nth(1).ClickAsync();

      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }
    public async Task checkCaseDelete()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] div:has-text(\"{HooksAdminManageCases.court}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First;
      await Task.Run(() => Assert.IsFalse(results.IsVisibleAsync().Result));
    }

    public async Task restoreCase()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);


      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Restore Case\"]").WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Restore Case\"]").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

    }
    public async Task checkBookNotSchedule()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={HooksAdminManageCases.court}").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");

      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await results.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(results.IsVisibleAsync().Result));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();

      var checkSchedule = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Recording\"]");
      await Task.Run(() => Assert.IsFalse(checkSchedule.IsVisibleAsync().Result));
    }

    public async Task deleteSchedule()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {HooksAdminManageCases.caseName}").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Schedule\"]").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Schedule\"]").ClickAsync();

      var caseRefTitle = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".appmagic-label.no-focus-outline.top").First;
      await caseRefTitle.WaitForAsync();
      await Task.Run(() => Assert.That(caseRefTitle.TextContentAsync().Result, Does.Contain($"{HooksAdminManageCases.caseName}")));

      var dateTable = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Gallery\"] >> text={dateNum}/{month}/{year}");
      await Task.Run(() => Assert.IsTrue(dateTable.IsVisibleAsync().Result));

      var witnessesTable = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Gallery\"] >> text={HooksAdminManageCases.witnesses}");
      await witnessesTable.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(witnessesTable.IsVisibleAsync().Result));

      var availableRecordings = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Gallery\"] >> text=No");
      await Task.Run(() => Assert.IsTrue(availableRecordings.IsVisibleAsync().Result));

      var courtTitle = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Court: {HooksAdminManageCases.court}").First;
      await Task.Run(() => Assert.IsTrue(courtTitle.IsVisibleAsync().Result));

      var caseRefTable = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {HooksAdminManageCases.caseName}").First;
      await Task.Run(() => Assert.IsTrue(caseRefTable.IsVisibleAsync().Result));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").Nth(1).ClickAsync();
    }
    public async Task ScheduleDeleteCheckManage()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{HooksAdminManageCases.caseName}");

      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=There are no recordings matching your search criteria. Consider changing or remo");
      await results.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(results.IsVisibleAsync().Result));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
    }

    public async Task removeCaseRefCase()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);


      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);


      if (AdminManageCases.use == "schedule")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {HooksAdminManageCases.caseName}").WaitForAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {HooksAdminManageCases.caseName}").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Schedule\"]").WaitForAsync();

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Schedule\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Case reference\\. Selected\\: {HooksAdminManageCases.caseName}\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Remove {HooksAdminManageCases.caseName} from selection\"]").WaitForAsync();

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Remove {HooksAdminManageCases.caseName} from selection\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Schedule Date").Nth(1).ClickAsync();

        var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(128) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon");
        await saveButton.WaitForAsync();
        await Task.Run(() => Assert.IsTrue(saveButton.IsDisabledAsync().Result));
      }
      else
      {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Case\"]").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Reference\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Reference\"]").FillAsync("");

        var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Save Case\")");
        await saveButton.WaitForAsync();
        await Task.Run(() => Assert.IsTrue(saveButton.IsDisabledAsync().Result));
      }
    }
    public async Task updateWithDuplicateCaseRef()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.court}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));


      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Case\"]").Nth(1).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);


      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Reference\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Reference\"]").FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Save Case\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }
    public async Task checkDuplicateErrorMessage()
    {
      var DuplicateError = Page.Locator("text=Case Reference already exists.");
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Task.Run(() => Assert.IsTrue(DuplicateError.IsVisibleAsync().Result));
    }


    public async Task goToAdmin()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").FillAsync($"{recording}"); await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {recording}").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(48)  div.virtualized-gallery > div > div > div").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task restoreScheduleCase()
    {
      await Page.ReloadAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").First.FillAsync($"{HooksAdminManageCases.caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Restore Case\"]").WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Restore Case\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {HooksAdminManageCases.caseName}").Nth(1).WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {HooksAdminManageCases.caseName}").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Schedules\"] [aria-label=\"Restore Case\"]").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Schedules\"] [aria-label=\"Restore Case\"]").ClickAsync();
    }

    public async Task checkScheduleRestore()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").WaitForAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{HooksAdminManageCases.caseName}");

      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div .react-knockout-control .appmagic-svg").First;
      await results.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(results.IsVisibleAsync().Result));
    }

    public async Task cannotEditOrDeleteCase()
    {
      var editButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Case\"]");
      await Task.Run(() => Assert.IsTrue(editButton.IsDisabledAsync().Result));

      var deleteButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Case\"]");
      await Task.Run(() => Assert.IsTrue(deleteButton.IsDisabledAsync().Result));
    }

    public async Task cannotEditOrDeleteSchedule()
    {
      await Page.WaitForLoadStateAsync(LoadState.Load);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      var editButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Schedule\"]");
      await Task.Run(() => Assert.IsTrue(editButton.IsDisabledAsync().Result));

      var deleteButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Schedule\"]");
      await Task.Run(() => Assert.IsTrue(deleteButton.IsDisabledAsync().Result));
    }

    public async Task cannotEditOrDeleteRecording()
    {
      var editButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Recording\"]");
      await Task.Run(() => Assert.IsTrue(editButton.IsDisabledAsync().Result));

      var deleteButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Recording\"]");
      await Task.Run(() => Assert.IsTrue(deleteButton.IsDisabledAsync().Result));
    }

    public async Task cannotEditOrDeleteWitDef()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(" div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Leeds").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{recording}");

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.Load);
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Role:").First.ClickAsync();

      for (int i = 0; i < 2; i++)
      {
        var saveButton = Page.Frame("fullscreen-app-host").Locator("div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i);
        await Task.Run(() => Assert.That(saveButton.InnerHTMLAsync().Result, Does.Contain("rgba(244, 244, 244, 1)"))); //light grey
        var deleteButton = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(6) > div").Nth(i);
        await Task.Run(() => Assert.That(deleteButton.InnerHTMLAsync().Result, Does.Contain("rgba(244, 244, 244, 1)"))); //light grey
      }
    }
  }
}


