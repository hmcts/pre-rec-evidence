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

    }
    public async Task FindSchedule()
    {


      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
      var updatecaseScheduled = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
      await Task.Run(() => Assert.That(updatecaseScheduled.TextContentAsync().Result, Does.Contain($"{stringCase}")));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");


    }


    public async Task FindupdatedCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      if (UpdateSchedules.use == "O")
      {
        // await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{newCourt}\")");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");

      }
      else if (UpdateSchedules.use == "A")
      {
        // await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{newCourt}\")");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");
      }
      else
      {
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");
      }

      var caseInput = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase.Trim()}");

      var caseLocation = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");

      if (UpdateSchedules.use == "O")
      {
        // await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{newCourt}")));
        await
          Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{court}")));
      }
      else if (UpdateSchedules.use == "A")
      {
        // await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{newCourt}")));
        await
          Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{court}")));
      }
      else
      {
        await
         Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{court}")));
      }

      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
    }
    public async Task Updatechildwitness()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Toggle\"] div >> nth=2");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }
    public async Task ManageRecordingsCheckUpdatechildwitness()
    {
      var Scheduledcase = Page.Frame("fullscreen-app-host").Locator($" div:nth-child(19) > div div.virtualized-gallery:has-text(\"{stringCase}\")");
      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Child")));
    }
    public async Task Updatedate()
    {

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");



      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Session\\ Date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{updatedday}\\ {updatedmonthword}\\ {updateddate}\\ {updatedyear}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }

    public async Task ManageRecordingsCheckUpdatedDate()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Clear\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
      var Scheduledcase = Page.Frame("fullscreen-app-host").Locator($" div:nth-child(19) > div div.virtualized-gallery:has-text(\"{stringCase}\")");

      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{stringCase}")));
    }


    public async Task BookRecordingsCheckUpdatedDate()
    {

      var scheduledate = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"text=Recording Start: {updateddate}/{updatedmonth}/{updatedyear}");
      await Task.Run(() => Assert.That(scheduledate.TextContentAsync().Result, Does.Contain($"{updateddate}/{updatedmonth}/{updatedyear}")));
    }

    public async Task UpdateDefendant()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Defendants\\.\\ Selected\\:\\ {def1}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"text={def2}");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }

    public async Task ManageRceordingsCheckUpdatedDefendant()
    {
      var Scheduledcase = Page.Frame("fullscreen-app-host").Locator($" div:nth-child(19) > div div.virtualized-gallery:has-text(\"{stringCase}\")");

      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{def2}")));
    }

    // public async Task BookRecordingsCheckUpdatedDefendant()
    // {
      //Bug S28-496
      // else if (UpdateSchedules.use == "E")
      // {
      //   //var defendant = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("text=Defendants: defendants 2");
      //   //await Task.Run(() =>Assert.That(defendant.TextContentAsync().Result, Does.Contain("defendants 2")));
      // }
    //}

    public async Task UpdateWitness()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Defendants\\.\\ Selected\\:\\ Witness\\ surname1\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("text=Witness surname2");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }
    public async Task ManageRecordingsCheckUpdatedWitness()
    {
      var Scheduledcase = Page.Frame("fullscreen-app-host").Locator($" div:nth-child(19) > div div.virtualized-gallery:has-text(\"{stringCase}\")");

      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));
    }
    public async Task BookRecordingsCheckUpdatedWitness()
    {

      var witness = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("text=Witness Name: Witness surname2");
      await Task.Run(() => Assert.That(witness.TextContentAsync().Result, Does.Contain("Witness surname2")));

    }
    public async Task RemoveDefendant()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {def1}\\, {def2}\"]").ClickAsync();
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Remove\\ {def2}\\ from\\ selection\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }
    public async Task ManageRecordingsCheckRemovedDefendant()
    {
      var Scheduledcase = Page.Frame("fullscreen-app-host").Locator($" div:nth-child(19) > div div.virtualized-gallery:has-text(\"{stringCase}\")");

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

      var courtdropdown = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"[aria-label=\"Select\\ Court\\.\\ Selected\\:\\ {newCourt}\"]");
      await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Contain($"{newCourt}")));
      await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{court}")));

    }

    public async Task UpdateAllFields()
    {


      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Toggle\"] div >> nth=2");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Scheduled\\ Date\\,\\ default\\ Today{currentdate}\\/{currentmonth}\\/{currentyear}\\.\\ Open\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{updatedday}\\ {updatedmonthword}\\ {updateddate}\\ {updatedyear}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");

      // - Only one court in MVP - test not needed currently
      // await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Courts\\.\\ Selected\\:\\ {court}\"]");
      // await Page.Frame("fullscreen-app-host").ClickAsync($"text={newCourt}");

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Defendants\\. Selected\\: {def1}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"text={def2}");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Defendants\\. Selected\\: {wit1}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"text={wit2}");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      //await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase.Trim()}");


    }

    public async Task ManageRecordingsCheckAllUpdatedFields()
    {

      var Scheduledcase = Page.Frame("fullscreen-app-host").Locator($" div:nth-child(19) > div div.virtualized-gallery:has-text(\"{stringCase}\")");


      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Child")));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      var datefield = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"[aria-label=\'Select\\ Scheduled\\ Date\\,\\ default\\ Today{updateddate}\\/{updatedmonth}\\/{updatedyear}\\.\\ Open\\ calendar\\ to\\ select\\ a\\ date\']");
      await Task.Run(() => Assert.IsTrue(datefield.IsVisibleAsync().Result));
      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{wit2}")));
      await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{def2}")));

      // - Only one court in MVP - test not needed currently
      // await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{newCourt}")));
      // await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain($"{court}")));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Clear\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Date\\,\\ default\\ TodayOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{updatedday}\\ {updatedmonthword}\\ {updateddate}\\ {updatedyear}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");

      // - Only one court in MVP - test not needed currently
      await Page.Frame("fullscreen-app-host").ClickAsync("div[role=\"button\"]:has-text(\"Court Name\")");
      // - Only one court in MVP - test not needed currently
      // await Page.Frame("fullscreen-app-host").ClickAsync($"ul[role=\"listbox\"] >> text={newCourt}");

      await Page.Frame("fullscreen-app-host").ClickAsync($"ul[role=\"listbox\"] >> text={court}");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");

      // Only one court in MVP - test not needed currently
      // await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{newCourt}")));
      // await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain($"{court}")));
    }

    public async Task BookRecordingsCheckAllUpdatedFields()
    {


      var scheduledate = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"text=Recording Start: {updateddate}/{updatedmonth}/{updatedyear}");
      await Task.Run(() => Assert.That(scheduledate.TextContentAsync().Result, Does.Contain($"{updateddate}/{updatedmonth}/{updatedyear}")));

      var witness = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"text=Witness Name: {wit2}");
      await Task.Run(() => Assert.That(witness.TextContentAsync().Result, Does.Contain($"{wit2}")));

      var defendant = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(59) div div .container_w3lqqm .appmagic-gallery .virtualized-gallery div .react-gallery-items-window .virtualized-gallery-item .canvasContentDiv.container_1vt1y2p .container_1f0sgyp div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
      //await Task.Run(() =>Assert.That(defendant.TextContentAsync().Result, Does.Contain("defendants 2")));

      // var courtdropdown = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"[aria-label=\"Select\\ Court\\.\\ Selected\\:\\ {newCourt}\"]");
      // await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Contain($"{newCourt}")));
      // await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{court}")));

      var courtdropdown = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"[aria-label=\"Select\\ Court\\.\\ Selected\\:\\ {court}\"]");
      await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Contain($"{court}")));
    }

    public async Task CloseAmendView()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Close\\ Amend\\ Recording\"]");
    }
    public async Task ManageRceordingsCheckCloseAmendView()
    {
      var savebutton = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Save\")");
      await Task.Run(() => Assert.IsFalse(savebutton.IsVisibleAsync().Result));
    }




    // await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
    // await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"View Recordings\")");
    // if (UpdateSchedules.use == "O")
    // {
    //   await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Court\\ Name\"]");
    //   await Page.Frame("fullscreen-app-host").ClickAsync("text={newCourt}");
    //   var greyboxdef = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
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
    // var greybox = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(17) > div > div > div > div > div");
    // await Task.Run(() =>Assert.That(greybox.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
    // }
    // else if (UpdateSchedules.use == "E")
    // {
    //   var greyboxdef2 = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   //System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("defendants 2")));

    //   await Task.Run(() =>Assert.That(greyboxdef2.TextContentAsync().Result, Does.Contain("defendants 2")));
    // }
    // else if (UpdateSchedules.use == "W")
    // {
    //   var greyboxwit = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(18) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));

    //   await Task.Run(() =>Assert.That(greyboxwit.TextContentAsync().Result, Does.Contain("Witness surname2")));
    // }
    // else if (UpdateSchedules.use == "DE")
    // {
    //   var greyboxdef = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   //System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
    //   await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
    // }
    // else if (UpdateSchedules.use == "O")
    // {
    //   var greyboxcourt = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(19) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //  // System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("{newCourt}")));
    //   await Task.Run(() =>Assert.That(greyboxcourt.TextContentAsync().Result, Does.Not.Contain("{court}")));
    // }
    // else
    // {
    //   var greyboxwit = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(18) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   var greyboxcourt = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(19) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   var greyboxdef = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
    //   //System.Threading.Thread.Sleep(5000);
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("{newCourt}")));
    //   await Task.Run(() =>Assert.That(greyboxcourt.TextContentAsync().Result, Does.Not.Contain("{court}")));
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));
    //   await Task.Run(() =>Assert.That(greyboxwit.TextContentAsync().Result, Does.Contain("Witness surname2")));
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("defendants 2")));
    //   await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Contain("defendants 2")));

    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
    //   var greybox = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(17) > div > div > div > div > div");
    //   await Task.Run(() =>Assert.That(greybox.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
    //   await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Court\\ Name\"]");
    //   await Page.Frame("fullscreen-app-host").ClickAsync("text={newCourt}");
    //   await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("{newCourt}")));
    //   await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Not.Contain("{court}")));

    // }



  }











}
