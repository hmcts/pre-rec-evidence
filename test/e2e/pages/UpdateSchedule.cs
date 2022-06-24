using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using pre.test.Hooks;

namespace pre.test.pages
{
  public class UpdateSchedule : BasePage
  {
    public static string CaseRefDate = "";
    public static String stringCase = "";

    public static String court = "Leeds";

    public static String newCourt = "Birmingham";

    public static DateTime today = DateTime.Today;
    public static DateTime tomorrow = today.AddDays(1);
    public static String updatedmonth = tomorrow.ToString("MM");
    public static String updateddate = tomorrow.ToString("dd");
    public static String updatedyear = tomorrow.ToString("yyyy");
    public static String updatedday = tomorrow.ToString("ddd");
    //public static String day = tomorrow.ToString("ddd");
    public static String updatedmonthword = tomorrow.ToString("MMM");

    public static String currentmonth = today.ToString("MM");
    public static String currentdate = today.ToString("dd");
    public static String currentyear = today.ToString("yyyy");
    public static String currentday = today.ToString("ddd");
    //public static String day = tomorrow.ToString("ddd");
    public static String currentmonthword = today.ToString("MMM");
    // public static String date = tomorrow.ToString("dd");
    // public static String year = tomorrow.ToString("yyyy");

    public static string wit1 = "Witness surname1";
    public static string wit2 = "Witness surname2";
    public static string def1 = "defendants 1";
    public static string def2 = "defendants 2";

    public static string Uwit1 = "UWitness surname1";
    public static string Uwit2 = "UWitness surname2";
    public static string Udef1 = "Udefendants 1";
    public static string Udef2 = "Udefendants 2";

    public UpdateSchedule(IPage page) : base(page)
    {


    }

    public async Task Schedule()
    {
      if (UpdateSchedules.use == "DE")
      {
        await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{def2}\")");
      }
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      HooksInitializer.scheduleCount++;
      HooksInitializer.recordings.Add(stringCase);
    }
    public async Task FindSchedule()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var updatecaseScheduled = Page.Frame("fullscreen-app-host").Locator("[aria-label=\"Edit Case Reference\"]");
      await Task.Run(() => Assert.That(updatecaseScheduled.InputValueAsync().Result, Does.Contain($"{stringCase}")));
    }


    public async Task FindupdatedCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      // don't need to update court for MVP
      // if (UpdateSchedules.use == "O")
      // {
      //   // await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{newCourt}\")");
      //   await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");

      // }
      // else if (UpdateSchedules.use == "A")
      // {
      //   // await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{newCourt}\")");
      //   await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");
      // }
      // else
      // {
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");
      //}

      var caseInput = Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase.Trim()}");

      var caseLocation = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");

      // don't need to update court for MVP
      // if (UpdateSchedules.use == "O")
      // {
      //   // await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{newCourt}")));
      //   await
      //     Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{court}")));
      // }
      // else if (UpdateSchedules.use == "A")
      // {
      //   // await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{newCourt}")));
      //   await
      //     Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{court}")));
      // }
      // else
      // {
      // await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{court}")));
      //}

      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      var courtLocation = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Leeds\")");
      await Task.Run(() => Assert.That(courtLocation.InnerTextAsync().Result, Does.Contain($"{court}")));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task Updatechildwitness()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Toggle\"] div >> nth=2");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task ManageRecordingsCheckUpdatechildwitness()
    {
      var child = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p  div:nth-child(7)");
      await Task.Run(() => Assert.That(child.TextContentAsync().Result, Does.Contain("Child")));
    }
    public async Task Updatedate()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Session\\ Date\"]");
      if (currentmonth != updatedmonth)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
      }
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{updatedday}\\ {updatedmonthword}\\ {updateddate}\\ {updatedyear}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task ManageRecordingsCheckUpdatedDate()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Clear\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Select Scheduled Date\\, default Today{updateddate}\\/{updatedmonth}\\/{updatedyear}\\. Open calendar to select a date\"]").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      var dateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{updatedday} {updatedmonthword} {updateddate} {updatedyear}\\. Selected\\.\"]");
      await Task.Run(() => Assert.IsTrue(dateLocator.IsVisibleAsync().Result));
    }


    public async Task BookRecordingsCheckUpdatedDate()
    {
      var scheduledate = Page.Frame("fullscreen-app-host").Locator($"text=Recording Start: {updateddate}/{updatedmonth}/{updatedyear}");
      await Task.Run(() => Assert.That(scheduledate.TextContentAsync().Result, Does.Contain($"{updateddate}/{updatedmonth}/{updatedyear}")));
    }

    public async Task UpdateDefendant()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Defendants\\.\\ Selected\\:\\ {def1}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"text={def2}");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task ManageRceordingsCheckUpdatedDefendant()
    {
      var def = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p  div:nth-child(9)");
      await Task.Run(() => Assert.That(def.TextContentAsync().Result, Does.Contain($"{def2}")));
    }

    // public async Task BookRecordingsCheckUpdatedDefendant()
    // {
    //Bug S28-496
    // else if (UpdateSchedules.use == "E")
    // {
    //   //var defendant = Page.Frame("fullscreen-app-host").Locator("text=Defendants: defendants 2");
    //   //await Task.Run(() =>Assert.That(defendant.TextContentAsync().Result, Does.Contain("defendants 2")));
    // }
    //}

    public async Task UpdateWitness()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Witness\\.\\ Selected\\:\\ Witness\\ surname1\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("text=Witness surname2");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task ManageRecordingsCheckUpdatedWitness()
    {
      var wit = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p  div:nth-child(6)");
      await Task.Run(() => Assert.That(wit.TextContentAsync().Result, Does.Contain($"{wit2}")));
    }
    public async Task BookRecordingsCheckUpdatedWitness()
    {

      var witness = Page.Frame("fullscreen-app-host").Locator("text=Witness Name: Witness surname2");
      await Task.Run(() => Assert.That(witness.TextContentAsync().Result, Does.Contain("Witness surname2")));

    }
    public async Task RemoveDefendant()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_1096g8n").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Remove\\ {def2}\\ from\\ selection\"]");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task ManageRecordingsCheckRemovedDefendant()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      var Scheduledcase = Page.Frame("fullscreen-app-host").Locator(".container_1f0sgyp div .react-knockout-control .appmagic-svg").First;
      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain($"{def2}")));
    }
    public async Task BookRecordingsCheckRemovedDefendant()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      var defendant = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(59) div div .container_w3lqqm .appmagic-gallery .virtualized-gallery div .react-gallery-items-window .virtualized-gallery-item .canvasContentDiv.container_1vt1y2p .container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
      //Bug S28-496
      //await Task.Run(() =>Assert.That(defendant.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
    }

    public async Task UpdateCourt()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Courts\\.\\ Selected\\:\\ {court}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"text={newCourt}");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task ManageRecordingsCheckUpdatedCourt()
    {
      var Scheduledcase = Page.Frame("fullscreen-app-host").Locator($" div:nth-child(19) > div div.virtualized-gallery:has-text(\"{stringCase}\")");

      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{newCourt}")));
      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain($"{court}")));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Clear\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("div[role=\"button\"]:has-text(\"Court Name\")");
      await Page.Frame("fullscreen-app-host").ClickAsync($"ul[role=\"listbox\"] >> text={newCourt}");
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");

      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{newCourt}")));
      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain($"{court}")));
    }

    public async Task BookRecordingsCheckUpdatedCourt()
    {
      var courtdropdown = Page.Frame("fullscreen-app-host").Locator($"[aria-label=\"Select\\ Court\\.\\ Selected\\:\\ {newCourt}\"]");
      await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Contain($"{newCourt}")));
      await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{court}")));
    }

    public async Task UpdateAllFields()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Toggle\"] div >> nth=2");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Scheduled\\ Date\\,\\ default\\ Today{currentdate}\\/{currentmonth}\\/{currentyear}\\.\\ Open\\ calendar\\ to\\ select\\ a\\ date\"]");
      if (currentmonth != updatedmonth)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
      }
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{updatedday}\\ {updatedmonthword}\\ {updateddate}\\ {updatedyear}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");

      // - Only one court in MVP - test not needed currently
      // await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Courts\\.\\ Selected\\:\\ {court}\"]");
      // await Page.Frame("fullscreen-app-host").ClickAsync($"text={newCourt}");

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Defendants\\. Selected\\: {def1}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"text={def2}");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Witness\\. Selected\\: {wit1}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"text={wit2}");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      //await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase.Trim()}");
    }

    public async Task ManageRecordingsCheckAllUpdatedFields()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var child = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p  div:nth-child(7)");
      await Task.Run(() => Assert.That(child.TextContentAsync().Result, Does.Contain("Child")));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      var datefield = Page.Frame("fullscreen-app-host").Locator($"[aria-label=\'Select\\ Scheduled\\ Date\\,\\ default\\ Today{updateddate}\\/{updatedmonth}\\/{updatedyear}\\.\\ Open\\ calendar\\ to\\ select\\ a\\ date\']");
      await Task.Run(() => Assert.IsTrue(datefield.IsVisibleAsync().Result));

      var def = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p  div:nth-child(9)");
      var wit = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p  div:nth-child(6)");
      await Task.Run(() => Assert.That(wit.TextContentAsync().Result, Does.Contain($"{wit2}")));
      await Task.Run(() => Assert.That(def.TextContentAsync().Result, Does.Contain($"{def2}")));

      // - Only one court in MVP - test not needed currently
      // await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{newCourt}")));
      // await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain($"{court}")));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Clear\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Date\\,\\ default\\ TodayOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      if (currentmonth != updatedmonth)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
      }
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{updatedday}\\ {updatedmonthword}\\ {updateddate}\\ {updatedyear}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      // - Only one court in MVP - test not needed currently
      await Page.Frame("fullscreen-app-host").ClickAsync("div[role=\"button\"]:has-text(\"Court Name\")");
      // - Only one court in MVP - test not needed currently
      // await Page.Frame("fullscreen-app-host").ClickAsync($"ul[role=\"listbox\"] >> text={newCourt}");

      await Page.Frame("fullscreen-app-host").ClickAsync($"ul[role=\"listbox\"] >> text={court}");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      // Only one court in MVP - test not needed currently
      // await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{newCourt}")));
      // await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain($"{court}")));
    }

    public async Task BookRecordingsCheckAllUpdatedFields()
    {
      var scheduledate = Page.Frame("fullscreen-app-host").Locator($"text=Recording Start: {updateddate}/{updatedmonth}/{updatedyear}");
      await Task.Run(() => Assert.That(scheduledate.TextContentAsync().Result, Does.Contain($"{updateddate}/{updatedmonth}/{updatedyear}")));

      var witness = Page.Frame("fullscreen-app-host").Locator($"text=Witness Name: {wit2}");
      await Task.Run(() => Assert.That(witness.TextContentAsync().Result, Does.Contain($"{wit2}")));

      var defendant = Page.Frame("fullscreen-app-host").Locator("div:nth-child(59) div div .container_w3lqqm .appmagic-gallery .virtualized-gallery div .react-gallery-items-window .virtualized-gallery-item .canvasContentDiv.container_1vt1y2p .container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
      //await Task.Run(() =>Assert.That(defendant.TextContentAsync().Result, Does.Contain("defendants 2")));

      // var courtdropdown = Page.Frame("fullscreen-app-host").Locator($"[aria-label=\"Select\\ Court\\.\\ Selected\\:\\ {newCourt}\"]");
      // await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Contain($"{newCourt}")));
      // await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{court}")));

      var courtdropdown = Page.Frame("fullscreen-app-host").Locator($"[aria-label=\"Select\\ Court\\.\\ Selected\\:\\ {court}\"]");
      await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Contain($"{court}")));
    }

    public async Task CloseAmendView()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Close\\ Amend\\ Recording\"]");
    }
    public async Task ManageRceordingsCheckCloseAmendView()
    {
      var savebutton = Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Save\")");
      await Task.Run(() => Assert.IsFalse(savebutton.IsVisibleAsync().Result));
    }

    // await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
    // await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"View Recordings\")");
    // if (UpdateSchedules.use == "O")
    // {
    //   await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Court\\ Name\"]");
    //   await Page.Frame("fullscreen-app-host").ClickAsync("text={newCourt}");
    //   var greyboxdef = Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("{newCourt}")));
    //   await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Not.Contain("{court}")));
    // }
    // else if (UpdateSchedules.use == "A")
    // {
    //   await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Search\\ Recording\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
    //   await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
    //   await Page.Frame("fullscreen-app-host").ClickAsync("text=Ok");
    //   await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Court\\ Name\"]");
    //   await Page.Frame("fullscreen-app-host").ClickAsync("text={newCourt}");

    // }
    // await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ case\\ ref\"]");
    // await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ case\\ ref\"]", $"{stringCase}");



    // if (UpdateSchedules.use == "D")
    // {
    // await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Search\\ Recording\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
    // await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
    // await Page.Frame("fullscreen-app-host").ClickAsync("text=Ok");
    // await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
    // var greybox = Page.Frame("fullscreen-app-host").Locator("div:nth-child(17) > div > div > div > div > div");
    // await Task.Run(() =>Assert.That(greybox.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
    // }
    // else if (UpdateSchedules.use == "E")
    // {
    //   var greyboxdef2 = Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   //System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("defendants 2")));

    //   await Task.Run(() =>Assert.That(greyboxdef2.TextContentAsync().Result, Does.Contain("defendants 2")));
    // }
    // else if (UpdateSchedules.use == "W")
    // {
    //   var greyboxwit = Page.Frame("fullscreen-app-host").Locator("div:nth-child(18) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));

    //   await Task.Run(() =>Assert.That(greyboxwit.TextContentAsync().Result, Does.Contain("Witness surname2")));
    // }
    // else if (UpdateSchedules.use == "DE")
    // {
    //   var greyboxdef = Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   //System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
    //   await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
    // }
    // else if (UpdateSchedules.use == "O")
    // {
    //   var greyboxcourt = Page.Frame("fullscreen-app-host").Locator("div:nth-child(19) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //  // System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("{newCourt}")));
    //   await Task.Run(() =>Assert.That(greyboxcourt.TextContentAsync().Result, Does.Not.Contain("{court}")));
    // }
    // else
    // {
    //   var greyboxwit = Page.Frame("fullscreen-app-host").Locator("div:nth-child(18) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   var greyboxcourt = Page.Frame("fullscreen-app-host").Locator("div:nth-child(19) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   var greyboxdef = Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   //System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("{newCourt}")));
    //   await Task.Run(() =>Assert.That(greyboxcourt.TextContentAsync().Result, Does.Not.Contain("{court}")));
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));
    //   await Task.Run(() =>Assert.That(greyboxwit.TextContentAsync().Result, Does.Contain("Witness surname2")));
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("defendants 2")));
    //   await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Contain("defendants 2")));

    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
    //   var greybox = Page.Frame("fullscreen-app-host").Locator("div:nth-child(17) > div > div > div > div > div");
    //   await Task.Run(() =>Assert.That(greybox.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
    //   await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Court\\ Name\"]");
    //   await Page.Frame("fullscreen-app-host").ClickAsync("text={newCourt}");
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("{newCourt}")));
    //   await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Not.Contain("{court}")));

    // }

    public async Task checkSuccessMessage()
    {
      var successMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Recording Updated");
      await Task.Run(() => Assert.IsTrue(successMessage.IsVisibleAsync().Result));
      await Task.Run(() => Assert.That(successMessage.TextContentAsync().Result, Does.Contain("Recording Updated")));
    }
  }
}
