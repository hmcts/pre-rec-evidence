using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using pre.test.Hooks;

namespace pre.test.pages
{
  public class BookRecording : BasePage
  {

    public BookRecording(IPage page) : base(page) { }
    public int quotaNum = 10;
    public int changeMonthCount = 0;
    public int changeMonthCountManage = 0;
    public static int count = 0;
    public string day = DateTime.UtcNow.ToString("ddd");
    public static DateTime originalDay = DateTime.UtcNow;
    public static string caseName = "";
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
    protected string court = "Leeds";
    protected Microsoft.Playwright.ILocator DuplicateError;

    public async Task EnterCaseDetails()
    {
      if (BookRecordings.use == "D")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      }
      else
      {
        //await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var date = DateTime.UtcNow.ToString("MMddmmss");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.ClickAsync();
        caseName = $"AutoT{date}";


      }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{caseName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", $"{defendantName},\n{defendantName}2");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]", $"{witnessName},\n{witnessName}2");
      if (BookRecordings.use == "D")
      {
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }
      await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");

      if (BookRecordings.use != "D")
      {
        Hooks.HooksInitializer.caseRef.Add(caseName);
        Hooks.HooksInitializer.contacts.Add(defendantName);
        Hooks.HooksInitializer.contacts.Add(witnessName);
        Hooks.HooksInitializer.contacts.Add($"{defendantName}2");
        Hooks.HooksInitializer.contacts.Add($"{witnessName}2");
      }
    }
    public async Task gotoBook()
    {
      var home = Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Home\")");

      await Task.Run(() => Assert.That(home.TextContentAsync().Result, Does.Contain("Home")));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
    }
    public async Task ScheduleRecording()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      for (int i = 0; i < changeMonthCount; i++)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
      }
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {dateNum}\\ {year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"{witnessName}\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{defendantName}\")");
      if (BookRecordings.use == "Child")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Is\\ your\\ Witness\\ a\\ Child\\,\\ default\\ No\"] div").Nth(2).ClickAsync();
      }
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");

      HooksInitializer.scheduleCount++;
      HooksInitializer.recordings.Add(caseName);

      var successMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Save Successful");
      await successMessage.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(successMessage.IsVisibleAsync().Result));
      await Task.Run(() => Assert.That(successMessage.TextContentAsync().Result, Does.Contain("Save Successful")));
    }
    public async Task CheckCaseScheduled()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var caseScheduled = Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{caseName}\")");
      await Task.Run(() => Assert.That(caseScheduled.TextContentAsync().Result, Does.Contain($"{caseName}")));
      if (BookRecordings.use == "Child")
      {
        await Task.Run(() => Assert.That(caseScheduled.TextContentAsync().Result, Does.Contain("Child")));
      }
    }
    public async Task CheckCaseCreated()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var exists = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First;
      await exists.ClickAsync();

      var caseLocation = Page.Frame("fullscreen-app-host").Locator("#publishedCanvas div:nth-child(5) div.canvasContentDiv.container_1vt1y2p div:nth-child(3)").First;
      await Task.Run(() => Assert.That(caseLocation.TextContentAsync().Result.Trim(), Does.Contain($"{court}")));

      var caseNameLocator = Page.Frame("fullscreen-app-host").Locator("#publishedCanvas div.canvasContentDiv.container_1vt1y2p div div:nth-child(1) div div div div div");
      await Task.Run(() => Assert.That(caseNameLocator.AllTextContentsAsync().Result, Does.Contain($"{caseName}")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(14)").ClickAsync();

    }
    public async Task SelectCourt()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();

      //await Page.Frame("fullscreen-app-host").ClickAsync("#powerapps-flyout-react-combobox-view-0:has-text(\"Leeds01\")");
    }
    public async Task CheckCourt()
    {
      var book = Page.Frame("fullscreen-app-host").Locator("#powerapps-flyout-react-combobox-view-0");

      await Page.Frame("fullscreen-app-host").ClickAsync($"#powerapps-flyout-react-combobox-view-0:has-text(\"{court}\")");

    }
    public async Task Enterfields()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", $"{defendantName},\n{defendantName}2");
    }
    public async Task selectPastDate()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Scheduled Start DateOpen calendar to select a date\"]").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      if (yesterMonth != month)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Previous Month\"]").ClickAsync();
      }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{yesterday} {yesterMonth} {yesterDateNum} {yesterYear}\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task pastDateErrorMessage()
    {
      var error = Page.Locator("text=You can't select a date in the past");
      await Task.Run(() => Assert.IsTrue(error.IsVisibleAsync().Result));
    }
    public async Task checkRecordingBox()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      var defendantLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Defendants: {defendantName}");
      var dateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Recording Start: {dateNum}/{monthNum}/{year}");
      await Task.Run(() => Assert.IsTrue(dateLocator.IsVisibleAsync().Result));
      var witnessLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Witness Name: {witnessName}");
      await Task.Run(() => Assert.IsTrue(witnessLocator.IsVisibleAsync().Result));
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
        if (orginalMonth != (DateTime.UtcNow.AddDays(+i)).ToString("MMM") && (DateTime.UtcNow.AddDays(+i)).ToString("dd") == "01")
        {
          changeMonthCountManage = changeMonthCountManage + 1;
        }

        var dateee = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{(originalDay.AddDays(+i)).ToString("ddd")} {(originalDay.AddDays(+i)).ToString("MMM")} {(originalDay.AddDays(+i)).ToString("dd")} {(originalDay.AddDays(+i)).ToString("yyyy")}\"]");

        if (dateee.IsVisibleAsync().Result != true)
        {
          for (int j = 0; j < changeMonthCountManage; j++)
          {
            await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
          }
        }

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{(originalDay.AddDays(+i)).ToString("ddd")} {(originalDay.AddDays(+i)).ToString("MMM")} {(originalDay.AddDays(+i)).ToString("dd")} {(originalDay.AddDays(+i)).ToString("yyyy")}\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Gallery\"] button:has-text(\"Record\")").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Room #\")").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=PRE008").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Start Recording\")").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        var rtmps = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search Recording ID\"]").Nth(2);
        await Task.Run(() => Assert.IsTrue(rtmps.IsVisibleAsync().Result));
        await Task.Run(() => Assert.That(rtmps.InputValueAsync().Result, Does.Contain("rtmps")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Ok\")").First.ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        count = count + 1;
      }
    }
    public async Task BlankValues()
    {

      var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(1);

      if (BookRecordings.use == "blankCourt")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync("caseTest12");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").FillAsync("def1");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").PressAsync("Tab");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("wit1");
      }
      if (BookRecordings.use == "blankCaseRef")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").FillAsync("def1");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").PressAsync("Tab");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("wit1");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] div:has-text(\"{court}\")").ClickAsync();
      }
      if (BookRecordings.use == "blankWitnesses")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync("caseTest12");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").FillAsync("def1");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] div:has-text(\"{court}\")").ClickAsync();
      }
      if (BookRecordings.use == "blankDefendants")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync("caseTest12");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("wit1");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] div:has-text(\"{court}\")").ClickAsync();
      }
      await saveButton.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task UpdateBlank()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      if (BookRecordings.use == "updateToBlankWitDef")
      {
        for (int i = 0; i < 4; i++)
        {
          var saveButton = Page.Frame("fullscreen-app-host").Locator("div:nth-child(5) div.canvasContentDiv.container_1vt1y2p div:nth-child(3) > div").Nth(i + 1);
          await Task.Run(() => Assert.That(saveButton.InnerHTMLAsync().Result, Does.Contain("rgba(11, 12, 12, 1)"))); //black

          var inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i);
          await inputBoxes.FillAsync("");

          await Task.Run(() => Assert.AreEqual(inputBoxes.InputValueAsync().Result, ""));
          await Task.Run(() => Assert.That(saveButton.InnerHTMLAsync().Result, Does.Contain("rgba(244, 244, 244, 1)"))); //light grey
        }
      }
      else
      {
        for (int i = 0; i < 4; i++)
        {
          await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p div:nth-child(6) > div > div > div > div").First.ClickAsync();
          await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
          await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        }
        var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2);
        await Task.Run(() => Assert.IsTrue(saveButton.IsDisabledAsync().Result));
      }
    }
    public async Task listBlankValues()
    {
      var date = DateTime.UtcNow.ToString("MMddmmss");

      caseName = $"AutoT{date}";


      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"]:has-text(\"{court}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{caseName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Defendants\\, comma seperated\"]").FillAsync("value 1, ,value 2");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter your Witnesses\\, comma seperated\"]").FillAsync("value 3, ,value 4");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(1).ClickAsync();
      Hooks.HooksInitializer.caseRef.Add(caseName);
      Hooks.HooksInitializer.contacts.Add("value 1");
      Hooks.HooksInitializer.contacts.Add("value 2");
      Hooks.HooksInitializer.contacts.Add("value 3");
      Hooks.HooksInitializer.contacts.Add("value 4");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task checkBlanksIgnored()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{court}\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var exists = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First;
      await exists.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var options = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("li[role=\"option\"]");
      await Task.Run(() => Assert.AreEqual(options.CountAsync().Result, 2));
      await Task.Run(() => Assert.That(options.AllInnerTextsAsync().Result, Does.Contain("value 1")));
      await Task.Run(() => Assert.That(options.AllInnerTextsAsync().Result, Does.Contain("value 2")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Task.Run(() => Assert.AreEqual(options.CountAsync().Result, 2));
      await Task.Run(() => Assert.That(options.AllInnerTextsAsync().Result, Does.Contain("value 3")));
      await Task.Run(() => Assert.That(options.AllInnerTextsAsync().Result, Does.Contain("value 4")));
    }
    public async Task checkDuplicateErrorMessage()
    {
      var DuplicateError = Page.Locator("text=This case already exists.");
      await Task.Run(() => Assert.That(DuplicateError.InnerTextAsync().Result, Does.Contain("This case already exists")));
    }

    public async Task emptyField()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      for (int i = 0; i < changeMonthCount; i++)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Next Month\"]").ClickAsync();
      }
      if (BookRecordings.use == "W")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {dateNum}\\ {year}\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{defendantName}\")");
      }
      if (BookRecordings.use == "D")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {dateNum}\\ {year}\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"{witnessName}\")");
      }
      if (BookRecordings.use == "date")
      {
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"{witnessName}\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"{defendantName}\")");
      }
    }
    public async Task SaveDisabled()
    {
      var save = Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Save\")").First;
      await Task.Run(() => Assert.IsTrue(save.IsDisabledAsync().Result));
    }
    public async Task blankErrorMessage()
    {
      if (BookRecordings.use == "blankCaseRef")
      {
        DuplicateError = Page.Locator("text=Please enter a case reference longer than 9 characters.");
      }
      else if (BookRecordings.use == "blankWitnesses")
      {
        DuplicateError = Page.Locator("text=Please enter witnesses for the case.");
      }
      else if (BookRecordings.use == "blankDefendants")
      {
        DuplicateError = Page.Locator("text=Please enter defendants for the case.");
      }
      else
      {
        DuplicateError = Page.Locator("text=Please select a Court from the dropdown.");
      }
      Console.WriteLine(DuplicateError);
      await DuplicateError.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(DuplicateError.IsVisibleAsync().Result));
    }
    public async Task entermorethanthirteencharacters()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync("morethan13char");
    }
    public async Task cannotentermorethanthirteencharacters()
    {
      var casref = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First;
      await Task.Run(() => Assert.That(casref.InputValueAsync().Result, Does.Contain("morethan13cha")));
      await Task.Run(() => Assert.That(casref.InputValueAsync().Result, Does.Not.Contain("morethan13char")));
    }

    public async Task deleteSchedule()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Recording\"]").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var box = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Booked Recordings\"] div").Nth(1);
      await Task.Run(() => Assert.That(box.InnerTextAsync().Result, Does.Not.Contain(witnessName)));
    }

    public async Task checkDeletedSchedule()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var noResults = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=There are no recordings matching your search criteria. Consider changing or remo");
      await Task.Run(() => Assert.IsTrue(noResults.IsVisibleAsync().Result));
    }

    public async Task findScheduleWithRecording()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_rv6t10").First.ClickAsync();

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Leeds\")");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Number \\\\ URN\"]").First.FillAsync($"{ExternalPortal.caseName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var exists = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").First;
      await exists.ClickAsync();

      var caseLocation = Page.Frame("fullscreen-app-host").Locator("#publishedCanvas div:nth-child(5) div.canvasContentDiv.container_1vt1y2p div:nth-child(3)").First;
      await Task.Run(() => Assert.That(caseLocation.TextContentAsync().Result.Trim(), Does.Contain($"Leeds")));

      var caseNameLocator = Page.Frame("fullscreen-app-host").Locator("#publishedCanvas div.canvasContentDiv.container_1vt1y2p div div:nth-child(1) div div div div div");
      await Task.Run(() => Assert.That(caseNameLocator.AllTextContentsAsync().Result, Does.Contain($"{ExternalPortal.caseName}")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task checkCannotDeleteScheduleWithRecording()
    {
      var deletebutton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete Recording\"]");
      await Task.Run(() => Assert.IsTrue(deletebutton.IsDisabledAsync().Result));
    }

    public async Task clickTermsandConditions()
    {

      var termslink = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Terms & Conditions\")");
      await termslink.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(termslink.IsVisibleAsync().Result));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Terms & Conditions\")").ClickAsync();

    }

    public async Task checkTermsandConditions()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").FrameLocator("iframe").Locator("#viewer").WaitForAsync();
      var TermsandConditions = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").FrameLocator("iframe").Locator("#viewer");
      //await TermsandConditions.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(TermsandConditions.IsVisibleAsync().Result));
    }
    public async Task clickBack()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Back\")").ClickAsync();
    }
    public async Task checkPage()
    {
      var landingpage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Pre-Recorded Evidence");
      await Task.Run(() => Assert.IsTrue(landingpage.IsVisibleAsync().Result));
    }

    public async Task clickOpenPage()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Open New Case\")").ClickAsync();
    }

  }
}