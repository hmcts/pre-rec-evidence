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

    protected string day = DateTime.UtcNow.ToString("ddd");
    protected string yesterday = ((DateTime.UtcNow.AddDays(-1)).ToString("ddd"));
    protected string month = DateTime.UtcNow.ToString("MMM");
    protected string yesterMonth = ((DateTime.UtcNow.AddDays(-1)).ToString("MMM"));
    protected string dateNum = DateTime.UtcNow.ToString("dd");
    protected string yesterDateNum = ((DateTime.UtcNow.AddDays(-1)).ToString("dd"));
    protected string year = DateTime.UtcNow.ToString("yyyy");
    protected string yesterYear = ((DateTime.UtcNow.AddDays(-1)).ToString("yyyy"));


    public async Task NavigateToBooking()
    {
      var book = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("button:has-text(\"Book a Recording\")");

      await Task.Run(() => book.IsVisibleAsync().Result);
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
    }

    public async Task EnterCaseDetails()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      if (BookRecordings.use == "Schedule")
      {
        await Page.Frame("fullscreen-app-host")
          .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"ScheduleAutoTest{date}");
      }

      if (BookRecordings.use == "Case")
      {
        await Page.Frame("fullscreen-app-host")
          .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"CaseAutoTest{date}");
      }

      if (BookRecordings.use == "PastDate")
      {
        await Page.Frame("fullscreen-app-host")
          .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"PastDateAutoTest{date}");
      }

      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Leeds\")");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "defendants 1,\ndefendants 2");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]",
          "Witness surname,\nWitness surname");
      await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
    }

    public async Task ScheduleRecording()
    {
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {dateNum}\\ {year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"Witness surname\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"defendants 1\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }

    public async Task CheckCaseScheduled()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"ScheduleAutoTest{date}");

      var caseScheduled = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator($"div.virtualized-gallery:has-text(\"ScheduleAutoTest{date}\")");
      await Task.Run(() =>
        Assert.That(caseScheduled.TextContentAsync().Result, Does.Contain($"ScheduleAutoTest{date}")));
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
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"CaseAutoTest{date}");

      var caseLocation = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("#publishedCanvas div:nth-child(5) div.canvasContentDiv.container_1vt1y2p div:nth-child(3)").First;
      await Task.Run(() => Assert.That(caseLocation.TextContentAsync().Result.Trim(), Does.Contain("Leeds")));

      var caseName = BookRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "#publishedCanvas div.canvasContentDiv.container_1vt1y2p div div:nth-child(1) div div div div div");
      await Task.Run(() => Assert.That(caseName.AllTextContentsAsync().Result, Does.Contain($"CaseAutoTest{date}")));
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
        .FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "defendants 1,\ndefendants 2");
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
  }
}
