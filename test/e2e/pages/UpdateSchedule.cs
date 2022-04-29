using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class UpdateSchedule : BasePage
  {
    public static string date = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
    private String stringCase = "";
    public UpdateSchedule(IPage page) : base(page)
    {

        
    }

public async Task NavigateToBookings()
    {
      var book = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("button:has-text(\"Book a Recording\")");

      await Task.Run(() => book.IsVisibleAsync().Result);
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
    }
public async Task Bookschedule()
    {
        if (UpdateSchedules.use == "C")
      {
        stringCase = $"ScheduleUpdateChildAutoTest{date}";
      }
      else if (UpdateSchedules.use == "D")
      {
        stringCase = $"ScheduleUpdateDateAutoTest{date}";
      }
      else if (UpdateSchedules.use == "DE")
      {
        stringCase = $"ScheduleRemoveDefAutoTest{date}";
      }
      else if (UpdateSchedules.use == "E")
      {
        stringCase = $"ScheduleUpdateDefAutoTest{date}";
      }
      else if (UpdateSchedules.use == "W")
      {
        stringCase = $"ScheduleUpdateWitAutoTest{date}";
      }
      else if (UpdateSchedules.use == "O")
      {
        stringCase = $"ScheduleUpdateCourtAutoTest{date}";
      }
      else if (UpdateSchedules.use == "V")
      {
        stringCase = $"AmendCloseViewAutoTest{date}";
      }
      else
      {
        stringCase = $"ScheduleUpdateAllAutoTest{date}";
      }

      

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase}");

      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Birmingham\")");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "defendants 1,\ndefendants 2");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]",
          "Witness surname1,\nWitness surname2");
      await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
    }


    public async Task Schedule()
    {
      var day = DateTime.UtcNow.ToString("ddd");
      var month = DateTime.UtcNow.ToString("MMM");
      var date = DateTime.UtcNow.ToString("dd");
      var year = DateTime.UtcNow.ToString("yyyy");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"Witness surname1\")");
      //await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\\.\\ Selected\\:\\ z\\ z\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"defendants 1\")");
        if (UpdateSchedules.use == "DE")
        {
        await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"defendants 2\")");
        }
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");

    }
    public async Task FindSchedule()
    {
        
        
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Manage Recordings\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
        var updatecaseScheduled = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
      await Task.Run(() =>
        Assert.That(updatecaseScheduled.TextContentAsync().Result, Does.Contain($"{stringCase}")));
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      

    }
    public async Task Updateaschedule()
    {
        //var day = DateTime.UtcNow.ToString("ddd");
        
      var month = DateTime.UtcNow.ToString("MM");
      var date = DateTime.UtcNow.ToString("dd");
      var year = DateTime.UtcNow.ToString("yyyy");
      var today = DateTime.Today;
      var tomorrow = today.AddDays(1);
      var updatedmonth = tomorrow.ToString("MMM");
      var updateddate = tomorrow.ToString("dd");
      var updatedyear = tomorrow.ToString("yyyy");
      var updatedday = tomorrow.ToString("ddd");


        //await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      if (UpdateSchedules.use == "C")
      {
       
       await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Toggle\"] div >> nth=2");
       await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      }
      else if (UpdateSchedules.use == "D")
      {
          await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Scheduled\\ Date\\,\\ default\\ Today{date}\\/{month}\\/{year}\\.\\ Open\\ calendar\\ to\\ select\\ a\\ date\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{updatedday}\\ {updatedmonth}\\ {updateddate}\\ {updatedyear}\"]");                                        
         await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");
         await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      }
      else if (UpdateSchedules.use == "E")
      {
          await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Defendants\\.\\ Selected\\:\\ z\\ z\\,\\ defendants\\ 1\"]");
        await Page.Frame("fullscreen-app-host")
        .ClickAsync("text=defendants 2");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      }
      else if (UpdateSchedules.use == "W")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Defendants\\.\\ Selected\\:\\ Witness\\ surname1\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=Witness surname2");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      }
      else if (UpdateSchedules.use == "DE")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Defendants\\.\\ Selected\\:\\ z\\ z\\,\\ defendants\\ 1\\,\\ defendants\\ 2\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Remove\\ defendants\\ 2\\ from\\ selection\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      }
      else if (UpdateSchedules.use == "O")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Courts\\.\\ Selected\\:\\ Birmingham\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=Leeds");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      }
      else if (UpdateSchedules.use == "V")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Close\\ Amend\\ Recording\"]");
      }
      else
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Toggle\"] div >> nth=2");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Scheduled\\ Date\\,\\ default\\ Today{date}\\/{month}\\/{year}\\.\\ Open\\ calendar\\ to\\ select\\ a\\ date\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{updatedday}\\ {updatedmonth}\\ {updateddate}\\ {updatedyear}\"]");                                        
         await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");
         await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Courts\\.\\ Selected\\:\\ Birmingham\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=Leeds");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Defendants\\.\\ Selected\\:\\ z\\ z\\,\\ defendants\\ 1\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=defendants 2");
         await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Defendants\\.\\ Selected\\:\\ Witness\\ surname1\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=Witness surname2");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
      }

    }
    public async Task Updatechildwitness()
    {
       await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Toggle\"] div >> nth=2");
       await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }
      public async Task CheckUpdatechildwitness()
      {
       var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
      await Task.Run(() =>
        Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Adult")));
      }

    
    public async Task CheckUpdatedschedule()
    {
      
      var today = DateTime.Today;
      var tomorrow = today.AddDays(1);
      var updatedmonth = tomorrow.ToString("MM");
      var updateddate = tomorrow.ToString("dd");
      var updatedyear = tomorrow.ToString("yyyy");
      var updatedday = tomorrow.ToString("ddd");
      var day = tomorrow.ToString("ddd");
      var month = tomorrow.ToString("MMM");
      var date = tomorrow.ToString("dd");
      var year = tomorrow.ToString("yyyy");

        if (UpdateSchedules.use == "C")
      {
        var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
      await Task.Run(() =>
        Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Child")));
        //await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
        
      }
      else if (UpdateSchedules.use == "D")
      {
          await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
          var datefield = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"[aria-label=\'Select\\ Scheduled\\ Date\\,\\ default\\ Today{updateddate}\\/{updatedmonth}\\/{updatedyear}\\.\\ Open\\ calendar\\ to\\ select\\ a\\ date\']");
        await Task.Run(() =>Assert.IsTrue(datefield.IsVisibleAsync().Result));
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Clear\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Date\\,\\ default\\ TodayOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
        await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
        var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
        await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"{stringCase}")));
        

      }
      else if (UpdateSchedules.use == "E")
      {
         var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
         await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("defendants 2")));
         

      }
      else if (UpdateSchedules.use == "W")
      {
        var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
      await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));

      }

      else if (UpdateSchedules.use == "DE")
      {
         var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
      await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
      }
      else if (UpdateSchedules.use == "O")
      {
         
        var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Leeds")));
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain("Birmingham")));
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Clear\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("div[role=\"button\"]:has-text(\"Court Name\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("ul[role=\"listbox\"] >> text=Leeds");
        await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
        await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
        
        await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Leeds")));
        await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain("Birmingham")));
      }
      else if (UpdateSchedules.use == "V")
     {
       var savebutton = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Save\")");
        await Task.Run(() =>Assert.IsFalse(savebutton.IsVisibleAsync().Result));
     }
      else
      {
        var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Child")));
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
        var datefield = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"[aria-label=\'Select\\ Scheduled\\ Date\\,\\ default\\ Today{updateddate}\\/{updatedmonth}\\/{updatedyear}\\.\\ Open\\ calendar\\ to\\ select\\ a\\ date\']");
        await Task.Run(() =>Assert.IsTrue(datefield.IsVisibleAsync().Result));
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));
        await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("defendants 2")));
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Leeds")));
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain("Birmingham")));
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Clear\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Date\\,\\ default\\ TodayOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("button[role=\"button\"]:has-text(\"Ok\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("div[role=\"button\"]:has-text(\"Court Name\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("ul[role=\"listbox\"] >> text=Leeds");
        await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
        await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");
        
        await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Leeds")));
        await Task.Run(() => Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain("Birmingham")));

      }

    }

    public async Task CheckUpdateinbookandviewrecordings()
      {

      var today = DateTime.Today;
      var tomorrow = today.AddDays(1);
      var updatedmonth = tomorrow.ToString("MM");
      var updateddate = tomorrow.ToString("dd");
      var updatedyear = tomorrow.ToString("yyyy");
      var updatedday = tomorrow.ToString("ddd");
      var day = tomorrow.ToString("ddd");
      var month = tomorrow.ToString("MMM");
      var date = tomorrow.ToString("dd");
      var year = tomorrow.ToString("yyyy");
      var Scheduledcase = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");  
      if(UpdateSchedules.use == "O")
      {
         await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Leeds\")");
      }
      else
      {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Birmingham\")");
      }

      var caseInput = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase.Trim()}");
      var caseLocation = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
      if(UpdateSchedules.use == "O")
      {
        await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain("Leeds")));
      }
      else
      {
      await
       Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain("Birmingham")));
      }
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      if (UpdateSchedules.use == "E")
      {
      var defendantbox =UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Task.Run(() => Assert.That(defendantbox.InputValueAsync().Result, Does.Contain("defendants 2")));
      }
      else if (UpdateSchedules.use == "W")
      {
        var Witnessbox =UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
        await Task.Run(() =>Assert.That(Witnessbox.InputValueAsync().Result, Does.Contain("Witness surname2")));
      }
      else if (UpdateSchedules.use == "DE")
      {
      var defendantbox = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      //await Task.Run(() =>Assert.That(defendantbox.InputValueAsync().Result, Does.Not.Contain("defendants 2")));
      }
      if (UpdateSchedules.use == "O")
      {
      var courtdropdown =UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("[aria-label=\"Select\\ Court\\.\\ Selected\\:\\ Leeds\"]");
      await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Contain("Leeds")));
      await Task.Run(() => Assert.That(courtdropdown.AllInnerTextsAsync().Result, Does.Not.Contain("Birmingham")));
      }
      var saveButton = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div div:nth-child(55)  button");
      await Task.Run(() => Assert.IsTrue(saveButton.IsEnabledAsync().Result));

      await Task.Run(() => saveButton.ClickAsync());
      //await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      
      var scheduleddate = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(49)");

      if (UpdateSchedules.use == "D")
      {
      
      await Task.Run(() =>Assert.That(scheduleddate.TextContentAsync().Result, Does.Contain($"Scheduled Date:{updateddate}/{updatedmonth}/{updatedyear}")));
      await Task.Run(() =>Assert.That(scheduleddate.TextContentAsync().Result, Does.Contain($"Recording Start: {updateddate}/{updatedmonth}/{updatedyear}")));
      }
      else if (UpdateSchedules.use == "E")
      {
        await Task.Run(() =>Assert.That(scheduleddate.TextContentAsync().Result, Does.Contain("defendants 2")));
      }
      else if (UpdateSchedules.use == "W")
      {
        await Task.Run(() =>Assert.That(scheduleddate.TextContentAsync().Result, Does.Contain("Witness surname2")));
      }
      else if (UpdateSchedules.use == "DE")
      {
        await Task.Run(() =>Assert.That(scheduleddate.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
      }
      

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"View Recordings\")");
      if (UpdateSchedules.use == "O")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Court\\ Name\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=Leeds");
        var greyboxdef = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
        System.Threading.Thread.Sleep(5000);
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Leeds")));
        await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Not.Contain("Birmingham")));
      }
      else if (UpdateSchedules.use == "A")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Search\\ Recording\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=Ok");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Court\\ Name\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=Leeds");

      }
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ case\\ ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ case\\ ref\"]", $"{stringCase}");
      

      
      if (UpdateSchedules.use == "D")
      {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Search\\ Recording\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("text=Ok");
      await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
      var greybox = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(17) > div > div > div > div > div");
      await Task.Run(() =>Assert.That(greybox.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
      }
      else if (UpdateSchedules.use == "E")
      {
        var greyboxdef2 = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
        //System.Threading.Thread.Sleep(5000);
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("defendants 2")));
        
        await Task.Run(() =>Assert.That(greyboxdef2.TextContentAsync().Result, Does.Contain("defendants 2")));
      }
      else if (UpdateSchedules.use == "W")
      {
        var greyboxwit = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(18) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
        System.Threading.Thread.Sleep(5000);
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));
        
        await Task.Run(() =>Assert.That(greyboxwit.TextContentAsync().Result, Does.Contain("Witness surname2")));
      }
      else if (UpdateSchedules.use == "DE")
      {
        var greyboxdef = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
        //System.Threading.Thread.Sleep(5000);
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
        await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Not.Contain("defendants 2")));
      }
      else if (UpdateSchedules.use == "O")
      {
        var greyboxcourt = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(19) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
       // System.Threading.Thread.Sleep(5000);
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Leeds")));
        await Task.Run(() =>Assert.That(greyboxcourt.TextContentAsync().Result, Does.Not.Contain("Birmingham")));
      }
      else
      {
        var greyboxwit = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(18) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
        var greyboxcourt = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(19) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
        var greyboxdef = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(20) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label");
        //System.Threading.Thread.Sleep(5000);
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Leeds")));
        await Task.Run(() =>Assert.That(greyboxcourt.TextContentAsync().Result, Does.Not.Contain("Birmingham")));
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Witness surname2")));
        await Task.Run(() =>Assert.That(greyboxwit.TextContentAsync().Result, Does.Contain("Witness surname2")));
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("defendants 2")));
        await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Contain("defendants 2")));
        
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
        var greybox = UpdateSchedules._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(17) > div > div > div > div > div");
        await Task.Run(() =>Assert.That(greybox.TextContentAsync().Result, Does.Contain($"Date: {updateddate}/{updatedmonth}/{updatedyear}")));
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Court\\ Name\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("text=Leeds");
        await Task.Run(() =>Assert.That(Scheduledcase.TextContentAsync().Result, Does.Contain("Leeds")));
        await Task.Run(() =>Assert.That(greyboxdef.TextContentAsync().Result, Does.Not.Contain("Birmingham")));

      }
      
      
      
    }











  }
}