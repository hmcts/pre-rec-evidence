using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Globalization;

namespace pre.test.pages
{
  public class ManageCase : BasePage
  {
    public ManageCase(IPage page) : base(page) { }
    protected String caseRef = "";
    protected String court = "";
    protected String newCourt = "";
    protected String caseId = "";
    protected String scheduleId = "";
    protected String scheduleDateString = "";
    protected String newScheduleDate = "";

    protected DateTime scheduleDate;

    protected string tomoMonthWord = "";
    protected string tomorrow = "";
    protected string month = "";
    protected string tomoMonth = "";
    protected string dateNum = "";
    protected string tomoDateNum = "";
    protected string year = "";
    protected string tomoYear = "";

    protected string monthWord = "";
    protected string today = "";

    public async Task goToManageCase()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
    }

    public async Task updateCourt()
    {
      var caseRefLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(45) div.virtualized-gallery > div > div > div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div >  div:nth-child(2)");
      caseRef = caseRefLocator.TextContentAsync().Result.ToString().Trim();
      caseRef = caseRef.Substring(caseRef.LastIndexOf(':') + 1).Trim();

      var courtLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First;
      court = courtLocator.TextContentAsync().Result.ToString().Trim();
      court = court.Substring(court.LastIndexOf(':') + 1).Trim();

      var caseIdLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div:nth-child(45) div.virtualized-gallery  div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)");
      caseId = caseIdLocator.TextContentAsync().Result.ToString().Trim();
      caseId = caseId.Substring(caseId.LastIndexOf(':') + 1).Trim();

      if (court == "Leeds") { newCourt = "Chris"; } else { newCourt = "Leeds"; }

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case ID: {caseId} Case Ref: {caseRef} Court: {court} >> [aria-label=\"Edit Case\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {court}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newCourt}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(119) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      await Task.Run(() => Assert.That(courtLocator.InnerTextAsync().Result, Does.Contain($"{newCourt}")));
    }

    public async Task checkCourtManageRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{caseRef}");

      var caseInfo = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p div:nth-child(4)");
      await Task.Run(() => Assert.That(caseInfo.AllInnerTextsAsync().Result, Does.Contain($"Court: {newCourt}")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      var courtDropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Courts\\. Selected\\: {newCourt}\"]");
      await Task.Run(() => Assert.IsTrue(courtDropdown.IsVisibleAsync().Result));
    }

    public async Task checkCourtViewRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search case ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search case ref\"]").FillAsync($"{caseRef}");

      var caseInfo = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div:nth-child(20) div.virtualized-gallery.hideScrollbar div div div:nth-child(1)  div.canvasContentDiv.container_1vt1y2p");
      await Task.Run(() => Assert.That(caseInfo.InnerTextAsync().Result, Does.Contain($"{newCourt}")));

      var caseInfoGreyBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(39) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text");
      await Task.Run(() => Assert.That(caseInfo.InnerTextAsync().Result, Does.Contain($"{newCourt}")));
    }

    public async Task revertCourt()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case ID: {caseId} Case Ref: {caseRef} Court: {newCourt} >> [aria-label=\"Edit Case\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {newCourt}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{court}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(119) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      var courtLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First;
      await Task.Run(() => Assert.That(courtLocator.InnerTextAsync().Result, Does.Contain($"{court}")));
    }

    public async Task checkCourtBookRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] div:has-text(\"{newCourt}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(37) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(37) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").FillAsync($"{caseRef}");

      var caseInfo = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p  div:nth-child(3)").First;
      await Task.Run(() => Assert.That(caseInfo.InnerTextAsync().Result, Does.Contain($"{newCourt}")));
    }

    public async Task updateDate()
    {
      var caseRefLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(45) div.virtualized-gallery > div > div > div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div >  div:nth-child(2)");
      caseRef = caseRefLocator.TextContentAsync().Result.ToString().Trim();
      caseRef = caseRef.Substring(caseRef.LastIndexOf(':') + 1).Trim();

      var courtLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First;
      court = courtLocator.TextContentAsync().Result.ToString().Trim();
      court = court.Substring(court.LastIndexOf(':') + 1).Trim();

      var caseIdLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div:nth-child(45) div.virtualized-gallery  div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)");
      caseId = caseIdLocator.TextContentAsync().Result.ToString().Trim();
      caseId = caseId.Substring(caseId.LastIndexOf(':') + 1).Trim();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First.ClickAsync();

      var scheduleIdLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(46)  div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)").First;
      scheduleId = scheduleIdLocator.TextContentAsync().Result.ToString().Trim();
      scheduleId = scheduleId.Substring(scheduleId.LastIndexOf(':') + 1).Trim();

      var scheduleDateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(46)  div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)").First;
      scheduleDateString = scheduleDateLocator.TextContentAsync().Result.ToString().Trim();
      scheduleDateString = scheduleDateString.Substring(scheduleDateString.LastIndexOf(':') + 1).Trim();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Schedule ID: {scheduleId} Schedule Date: {scheduleDateString} >> [aria-label=\"Edit Schedule\"]").ClickAsync();

      scheduleDate = DateTime.Parse(scheduleDateString, CultureInfo.CurrentUICulture);
      dateNum = scheduleDate.ToString("dd");
      month = scheduleDate.ToString("MM");
      year = scheduleDate.ToString("yyyy");
      monthWord = scheduleDate.ToString("MMMM");
      today = scheduleDate.ToString("ddd");

      tomorrow = (scheduleDate.AddDays(+1)).ToString("ddd");
      tomoMonthWord = (scheduleDate.AddDays(+1)).ToString("MMMM");
      tomoMonth = (scheduleDate.AddDays(+1)).ToString("MM");
      tomoDateNum = (scheduleDate.AddDays(+1)).ToString("dd");
      tomoYear = (scheduleDate.AddDays(+1)).ToString("yyyy");

      newScheduleDate = $"{tomoDateNum}/{tomoMonth}/{tomoYear}";

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Schedule Date{dateNum}\\/{month}\\/{year}\\. Open calendar to select a date\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{tomorrow} {tomoMonthWord} {tomoDateNum} {tomoYear}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(122) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      await Task.Run(() => Assert.That(scheduleDateLocator.InnerTextAsync().Result, Does.Contain($"{newScheduleDate}")));
    }

    public async Task checkDateBookRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"]:has-text(\"{court}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(37) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(37) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").FillAsync($"{caseRef}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Recordings").First.ClickAsync();

      var recordingDateBook = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Recording Start: {newScheduleDate}");
      await Task.Run(() => Assert.IsTrue(recordingDateBook.IsVisibleAsync().Result));
    }

    public async Task checkDateManageRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{caseRef}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Session Date\"]").ClickAsync();

      var scheduleDateManage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{tomorrow} {tomoMonthWord} {tomoDateNum} {tomoYear}\\. Selected\\.\"]");
      await Task.Run(() => Assert.IsTrue(scheduleDateManage.IsVisibleAsync().Result));
    }

    public async Task checkDateViewRecordings()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search case ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search case ref\"]").FillAsync($"{caseRef}");

      var scheduleDateView = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas  div:nth-child(20) div.virtualized-gallery.hideScrollbar div div div:nth-child(1)  div.canvasContentDiv.container_1vt1y2p");
      await Task.Run(() => Assert.That(scheduleDateView.InnerTextAsync().Result, Does.Contain($"{newScheduleDate}")));

      var scheduleDateViewGreyBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Date: {newScheduleDate}").Nth(1);
      await Task.Run(() => Assert.IsTrue(scheduleDateViewGreyBox.IsVisibleAsync().Result));
    }

    public async Task revertDate()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Item 1. Selected. Case ID: {caseId} Case Ref: {caseRef}").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label .appmagic-label-text").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Schedule ID: {scheduleId} Schedule Date: {tomoDateNum}/{tomoMonth}/{tomoYear} >> [aria-label=\"Edit Schedule\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Schedule Date{tomoDateNum}\\/{tomoMonth}\\/{tomoYear}\\. Open calendar to select a date\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{today} {monthWord} {dateNum} {year}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(122) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();

      var scheduleDateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas div:nth-child(46)  div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)").First;
      await Task.Run(() => Assert.That(scheduleDateLocator.InnerTextAsync().Result, Does.Contain($"{dateNum}/{month}/{year}")));
    }
  }
}

