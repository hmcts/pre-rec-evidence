using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class BookRecording : BasePage
  {
    public static string date = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

    public BookRecording(IPage page) : base(page) { }
    public int quotaNum = 10;
    public int changeMonthCount = 0;
    public int changeMonthCountManage = 0;
    public static int count = 0;
    public string day = DateTime.UtcNow.ToString("ddd");
    public static DateTime originalDay = DateTime.UtcNow;
    protected string caseName = "";
    protected string yesterday = ((DateTime.UtcNow.AddDays(-1)).ToString("ddd"));
    public string month = DateTime.UtcNow.ToString("MMM");
    public string monthNum = DateTime.UtcNow.ToString("MM");
    protected string yesterMonth = ((DateTime.UtcNow.AddDays(-1)).ToString("MMM"));
    public string dateNum = DateTime.UtcNow.ToString("dd");
    protected string yesterDateNum = ((DateTime.UtcNow.AddDays(-1)).ToString("dd"));
    public string year = DateTime.UtcNow.ToString("yyyy");
    protected string yesterYear = ((DateTime.UtcNow.AddDays(-1)).ToString("yyyy"));
    protected string witnessName = "Witness surname";
    protected string defendantName = "defendants 1";

    public async Task EnterCaseDetails()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      if (BookRecordings.use == "Schedule")
      {
        caseName = $"ScheduleAutoTest{date}";
      }

      if (BookRecordings.use == "Case")
      {
        caseName = $"CaseAutoTest{date}";
      }

      if (BookRecordings.use == "PastDate")
      {
        caseName = $"PastDateAutoTest{date}";
      }

      if (BookRecordings.use == "ScheduleTen")
      {
        caseName = $"TenQuotaAutoTest{date}";
      }

      await Page.Frame("fullscreen-app-host")
                .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{caseName}");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Leeds\")");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", $"{defendantName},\n{defendantName}2");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]",
          $"{witnessName},\n{witnessName}2");
      await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
    }

    public async Task ScheduleRecording()
    {
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
        for (int i = 0; i < changeMonthCount; i++)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
      }
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {dateNum}\\ {year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"{witnessName}\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{defendantName}\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }

    public async Task CheckCaseScheduled()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{caseName}");

      var caseScheduled = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator($"div.virtualized-gallery:has-text(\"{caseName}\")");
      await Task.Run(() =>
        Assert.That(caseScheduled.TextContentAsync().Result, Does.Contain($"{caseName}")));
    }

    public async Task CheckCaseCreated()
    {
      var home = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Home\")");

      await Task.Run(() => Assert.That(home.TextContentAsync().Result, Does.Contain("Home")));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");

      var book = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("button:has-text(\"Book a Recording\")");

      await Task.Run(() => Assert.That(book.TextContentAsync().Result, Does.Contain("Book")));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Leeds\")");

      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{caseName}");

      var caseLocation = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("#publishedCanvas div:nth-child(5) div.canvasContentDiv.container_1vt1y2p div:nth-child(3)").First;
      await Task.Run(() => Assert.That(caseLocation.TextContentAsync().Result.Trim(), Does.Contain("Leeds")));

      var caseNameLocator = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "#publishedCanvas div.canvasContentDiv.container_1vt1y2p div div:nth-child(1) div div div div div");
      await Task.Run(() => Assert.That(caseNameLocator.AllTextContentsAsync().Result, Does.Contain($"{caseName}")));
    }

    public async Task SelectCourt()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      //await Page.Frame("fullscreen-app-host").ClickAsync("#powerapps-flyout-react-combobox-view-0:has-text(\"Leeds01\")");
    }

    public async Task CheckCourt()
    {
      var book = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("#powerapps-flyout-react-combobox-view-0");

      await Page.Frame("fullscreen-app-host")
        .ClickAsync("#powerapps-flyout-react-combobox-view-0:has-text(\"Leeds 01\")");

    }

    public async Task Enterfields()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"AutoTest{date}");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Leeds\")");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", $"{defendantName},\n{defendantName}2");
    }

    public async Task selectPastDate()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Scheduled Start DateOpen calendar to select a date\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{yesterday} {yesterMonth} {yesterDateNum} {yesterYear}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
    }

    public async Task pastDateErrorMessage()
    {
      var error = Page.Locator("text=You can't select a date in the past");
      await Task.Run(() => Assert.IsTrue(error.IsVisibleAsync().Result));
    }

    public async Task checkRecordingBox()
    {
      var dateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Recording Start: {dateNum}/{monthNum}/{year}");
      await Task.Run(() => Assert.IsTrue(dateLocator.IsVisibleAsync().Result));
      var witnessLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Witness Name: {witnessName}");
      await Task.Run(() => Assert.IsTrue(witnessLocator.IsVisibleAsync().Result));
      var defendantLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Defendants: {defendantName}");
      await Task.Run(() => Assert.IsTrue(defendantLocator.IsVisibleAsync().Result));
      var deleteLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Recording\"]");
      await Task.Run(() => Assert.IsTrue(deleteLocator.IsVisibleAsync().Result));
      var refreshLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Refresh Recordings\"]");
      await Task.Run(() => Assert.IsTrue(refreshLocator.IsVisibleAsync().Result));
    }

    public async Task startTenRecordings()
    {
      var orginalMonth = DateTime.UtcNow.ToString("MMM");
      for (int i = 0; i < quotaNum; i++)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Date\"]").ClickAsync();
        if (orginalMonth != (DateTime.UtcNow.AddDays(+i)).ToString("MMM") && (DateTime.UtcNow.AddDays(+i)).ToString("dd") == "01"){
            changeMonthCountManage = changeMonthCountManage + 1;
        }

        var dateee = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{(originalDay.AddDays(+i)).ToString("ddd")} {(originalDay.AddDays(+i)).ToString("MMM")} {(originalDay.AddDays(+i)).ToString("dd")} {(originalDay.AddDays(+i)).ToString("yyyy")}\"]");

        if (dateee.IsVisibleAsync().Result != true){
          for (int j = 0; j < changeMonthCountManage; j++){
           await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
           }
        }

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{(originalDay.AddDays(+i)).ToString("ddd")} {(originalDay.AddDays(+i)).ToString("MMM")} {(originalDay.AddDays(+i)).ToString("dd")} {(originalDay.AddDays(+i)).ToString("yyyy")}\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Record\")").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Room #\")").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=PRE008").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Start Recording\")").ClickAsync();
        var rtmps = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search Recording ID\"]").Nth(2);
        while (rtmps.IsVisibleAsync().Result != true) { }
        await Task.Run(() => Assert.IsTrue(rtmps.IsVisibleAsync().Result));
        await Task.Run(() => Assert.That(rtmps.InputValueAsync().Result, Does.Contain("rtmps")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Ok\")").First.ClickAsync();
        while (rtmps.IsVisibleAsync().Result != false) { }
        count = count + 1;
      }
    }

  }
}

