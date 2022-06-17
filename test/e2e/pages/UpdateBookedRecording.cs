using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using pre.test.Hooks;

namespace pre.test.pages
{
  public class UpdateBookedRecording : BasePage
  {

    public UpdateBookedRecording(IPage page) : base(page)
    {
    }
    public static String stringCourt = "Leeds";
    public static String stringCase = "";
    public static string wit1 = "Witness surname1";
    public static string wit2 = "Witness surname2";
    public static string def1 = "defendants 1";
    public static string def2 = "defendants 2";
    public static string Uwit1 = "UWitness surname1";
    public static string Uwit2 = "UWitness surname2";
    public static string Udef1 = "Udefendants 1";
    public static string Udef2 = "Udefendants 2";
    public static Boolean flag = false;
    protected static ILocator inputBoxes;
    public string dateNum = (DateTime.UtcNow.AddDays(+1)).ToString("dd");
    public string monthNum = (DateTime.UtcNow.AddDays(+1)).ToString("MM");
    public string year = (DateTime.UtcNow.AddDays(+1)).ToString("yyyy");
    public string month = (DateTime.UtcNow.AddDays(+1)).ToString("MMM");
    public string day = (DateTime.UtcNow.AddDays(+1)).ToString("ddd");
    public static string newWit = "NewWitnessWow";
    public static string newDef = "WowNewDef";


    public async Task FindCaseToView()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Manage Recordings\")");
      var caseLocation = Page.Frame("fullscreen-app-host").Locator("div:nth-child(19) div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)");
      stringCase = caseLocation.TextContentAsync().Result.ToString().Trim();
      stringCase = stringCase.Substring(stringCase.LastIndexOf(':') + 1);

      var courtLocation = Page.Frame("fullscreen-app-host").Locator("div:nth-child(19)  div.virtualized-gallery > div > div > div:nth-child(1)  div:nth-child(4)");
      stringCourt = courtLocation.TextContentAsync().Result.ToString().Trim();
      stringCourt = stringCourt.Substring(stringCourt.LastIndexOf(':') + 1);
    }
    public async Task SearchCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase.Trim()}");
    }
    public async Task FindCase()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var caseLocation = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
      await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{stringCourt.Trim()}")));

      var caseName = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)");
      await Task.Run(() => Assert.That(caseName.AllInnerTextsAsync().Result, Does.Contain($"{stringCase.Trim()}")));
    }
    public async Task UpdateCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      for (int i = 0; i < 4; i++)
      {
        var inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i);

        if (UpdateBookedRecordings.use == "W")
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Uwit1}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit2}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Uwit2}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
        }
        else if (UpdateBookedRecordings.use == "D")
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Udef1}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def2}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Udef2}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
        }
        else
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Uwit1}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit2}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Uwit2}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Udef1}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def2}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Udef2}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
        }
      }
      var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2);
      await Task.Run(() => Assert.IsTrue(saveButton.IsEnabledAsync().Result));
      await Task.Run(() => saveButton.ClickAsync());
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

    }

    public async Task CheckUpdatedCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase}");
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");

      for (int i = 0; i < 4; i++)
      {
        var inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i);

        if (UpdateBookedRecordings.use == "W")
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{Uwit1}"))
          {
            await Task.Run(() => Assert.That(inputBoxes.InputValueAsync().Result, Does.Contain($"{Uwit1}")));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{Uwit2}"))
          {
            await Task.Run(() => Assert.That(inputBoxes.InputValueAsync().Result, Does.Contain($"{Uwit2}")));
          }
        }
        else if (UpdateBookedRecordings.use == "D")
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{Udef1}"))
          {
            await Task.Run(() => Assert.That(inputBoxes.InputValueAsync().Result, Does.Contain($"{Udef1}")));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{Udef2}"))
          {
            await Task.Run(() => Assert.That(inputBoxes.InputValueAsync().Result, Does.Contain($"{Udef2}")));
          }
        }
        else
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{Uwit1}"))
          {
            await Task.Run(() => Assert.That(inputBoxes.InputValueAsync().Result, Does.Contain($"{Uwit1}")));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{Uwit2}"))
          {
            await Task.Run(() => Assert.That(inputBoxes.InputValueAsync().Result, Does.Contain($"{Uwit2}")));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{Udef1}"))
          {
            await Task.Run(() => Assert.That(inputBoxes.InputValueAsync().Result, Does.Contain($"{Udef1}")));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{Udef2}"))
          {
            await Task.Run(() => Assert.That(inputBoxes.InputValueAsync().Result, Does.Contain($"{Udef2}")));
          }
        }
      }
    }
    public async Task checkManage()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var results = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)");
      await Task.Run(() => Assert.That(results.AllInnerTextsAsync().Result, Does.Contain($"Case Ref: {stringCase}")));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();

      var dropdown = Page.Frame("fullscreen-app-host").Locator("li[role='option']");

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Witness\\. Selected\\: {Uwit1}\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Uwit1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Uwit2}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {Udef1}\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Udef1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Udef2}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      }
      else if (UpdateBookedRecordings.use == "schedule")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Date\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"{day} {month} {dateNum} {year}\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        var results2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)").First;
        await Task.Run(() => Assert.That(results2.AllInnerTextsAsync().Result, Does.Contain($"Case Ref: {stringCase}")));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Witness\\. Selected\\: {Uwit1}\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Uwit1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Uwit2}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));
        await Page.Frame("fullscreen-app-host").ClickAsync("text=HMCTS Logo Dev Home Manage Recordings Court Court NameOpen popup to select items");

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {Udef1}\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Udef1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Udef2}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      }
    }
    public async Task removeWitDefNotScheduled()
    {
      var first = true;
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      for (int i = 0; i < 4; i++)
      {
        inputBoxes = null;
        if (!(first))
        {
          await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        }

        if (Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p input").Nth(i).CountAsync().Result > 0)
        {
          inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p input").Nth(i);
        }

        if (UpdateBookedRecordings.use == "W" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit2}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

          }
        }
        else if (UpdateBookedRecordings.use == "D" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def2}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }

        }
        else if (UpdateBookedRecordings.use == "WD" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit2}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
            flag = true;
            UpdateBookedRecordings.use = "W";
            await checkErrorMessage();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def2}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
            flag = true;
            UpdateBookedRecordings.use = "D";
            await checkErrorMessage();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
        }
        first = false;
      }
    }

    public async Task checkErrorMessage()
    {
      if (UpdateBookedRecordings.use == "W")
      {
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=You are deleting {wit2} from the system");
        var errorMessage2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={wit2} is associated with: 0 scheduled recordings.");
        var errorMessage3 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=If you choose to delete {wit2}, all associated recordings will also be deleted.");

        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain($"You are deleting {wit2} from the system")));
        await Task.Run(() => Assert.That(errorMessage2.TextContentAsync().Result, Does.Contain($"{wit2} is associated with: 0 scheduled recordings.")));
        await Task.Run(() => Assert.That(errorMessage3.TextContentAsync().Result, Does.Contain($"If you choose to delete {wit2}, all associated recordings will also be deleted.")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }

      if (UpdateBookedRecordings.use == "D")
      {
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=You are deleting {def2} from the system");
        var errorMessage2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={def2} is associated with: 0 scheduled recordings.");
        var errorMessage3 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=If you choose to delete {def2}, all associated recordings will also be deleted.");

        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain($"You are deleting {def2} from the system")));
        await Task.Run(() => Assert.That(errorMessage2.TextContentAsync().Result, Does.Contain($"{def2} is associated with: 0 scheduled recordings.")));
        await Task.Run(() => Assert.That(errorMessage3.TextContentAsync().Result, Does.Contain($"If you choose to delete {def2}, all associated recordings will also be deleted.")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }
      if (flag) { UpdateBookedRecordings.use = "WD"; flag = false; }
    }

    public async Task createAdditonalSchedule()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Scheduled Start DateOpen calendar to select a date\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label='{day} {month} {dateNum} {year}']").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button[role=\"button\"]:has-text(\"Ok\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{wit1}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{def1}\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").First.ClickAsync();

      HooksInitializer.scheduleCount++;
      var savedDate = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Recording Start: {dateNum}/{monthNum}/{year}");
      var savedWitness = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Witness Name: {wit1}").First;
      var savedDefendant = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Defendants: {def1}").First;

      for (int i = 0; i < 2; i++)
      {
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }

      await Task.Run(() => Assert.IsTrue(savedDate.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(savedWitness.IsVisibleAsync().Result));
      await Task.Run(() => Assert.IsTrue(savedDefendant.IsVisibleAsync().Result));
    }
    public async Task checkRemovedWitDef()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");

      if (UpdateBookedRecordings.use == "W")
      {
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
        await Task.Run(() => Assert.That(witBox.InputValueAsync().Result, Does.Not.Contain($"{wit2}")));
      }

      else if (UpdateBookedRecordings.use == "D")
      {
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
        await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Not.Contain($"{def2}")));
      }
      else if (UpdateBookedRecordings.use == "WD")
      {
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
        await Task.Run(() => Assert.That(witBox.InputValueAsync().Result, Does.Not.Contain($"{wit2}")));

        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
        await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Not.Contain($"{def2}")));
      }
    }
    public async Task checkScheduleRemovedWitDef()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{HooksUpdateBookedRecording.day}\\ {HooksUpdateBookedRecording.month}\\ {HooksUpdateBookedRecording.datee}\\ {HooksUpdateBookedRecording.year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Witness\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var witdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {wit2}");
        await Task.Run(() => Assert.IsFalse(witdropdown.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Defendants\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        var defdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {def2}");
        await Task.Run(() => Assert.IsFalse(defdropdown.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "WD")
      {
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Defendants\"]").ClickAsync();

        var defdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {def2}");
        await Task.Run(() => Assert.IsFalse(defdropdown.IsVisibleAsync().Result));
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Witness\"]").ClickAsync();
        var witdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {wit2}");
        await Task.Run(() => Assert.IsFalse(witdropdown.IsVisibleAsync().Result));
      }
    }
    public async Task checkManageRemovedWitDef()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{stringCase}");

      var updatecaseScheduled = Page.Frame("fullscreen-app-host").Locator($"div.virtualized-gallery:has-text(\"{stringCase}\")");
      await Task.Run(() => Assert.That(updatecaseScheduled.InnerTextAsync().Result, Does.Contain($"{stringCase}")));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();

      var dropdown = Page.Frame("fullscreen-app-host").Locator("li[role='option']");

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {wit1}\"]").ClickAsync();

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {def1}\"]").ClickAsync();

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      }
      else if (UpdateBookedRecordings.use == "WD")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {wit1}\"]").ClickAsync();

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {def1}\"]").ClickAsync();

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      }
    }
    public async Task checkAdminRemovedWitDef()
    {
      var day = DateTime.UtcNow.ToString("ddd");
      var month = DateTime.UtcNow.ToString("MM");
      var datee = DateTime.UtcNow.ToString("dd");
      var year = DateTime.UtcNow.ToString("yyyy");

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Home\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").FillAsync($"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var caseref = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}");
      await Task.Run(() => Assert.That(caseref.InnerTextAsync().Result, Does.Contain($"{stringCase}")));
      await caseref.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Schedule Date: {datee}/{month}/{year}").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit\\ Recording\"]").ClickAsync();

      var dropdown = Page.Frame("fullscreen-app-host").Locator("li[role='option']");

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\:\\ {wit1}\"]").ClickAsync();

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court\\ Name\\.\\ Selected\\:\\ {def1}\"]").ClickAsync();

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      }
      else if (UpdateBookedRecordings.use == "WD")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\:\\ {wit1}\"]").ClickAsync();

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(66) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-label").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court\\ Name\\.\\ Selected\\:\\ {def1}\"]").ClickAsync();

        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      }
    }
    public async Task removeWitDefScheduled()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      for (int i = 0; i < 4; i++)
      {
        inputBoxes = null;
        if (Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p input").Nth(i).CountAsync().Result > 0)
        {
          inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.canvasContentDiv.container_1vt1y2p input").Nth(i);
        }

        if (UpdateBookedRecordings.use == "W" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
        }
        else if (UpdateBookedRecordings.use == "D" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
        }
        else if (UpdateBookedRecordings.use == "WD" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

            flag = true;
            UpdateBookedRecordings.use = "W";
            await checkErrorMessagescheduledwitdef();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

            flag = true;
            UpdateBookedRecordings.use = "D";
            await checkErrorMessagescheduledwitdef();
            await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
          }
        }
        else if (UpdateBookedRecordings.use == "T")
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {
            await inputBoxes.FillAsync("");
            var saveicon = Page.Frame("fullscreen-app-host").Locator("div:nth-child(52)   div:nth-child(1)  div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").Nth(i);
            System.Console.WriteLine(saveicon);
            System.Console.WriteLine(stringCase);
            await Task.Run(() => Assert.IsTrue(saveicon.IsHiddenAsync().Result));
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {
            await inputBoxes.FillAsync("");
            var saveicon = Page.Frame("fullscreen-app-host").Locator("div:nth-child(52)   div:nth-child(1)  div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").Nth(i + 1);
            System.Console.WriteLine(saveicon);
            System.Console.WriteLine(stringCase);
            await Task.Run(() => Assert.IsTrue(saveicon.IsHiddenAsync().Result));
          }
        }
      }
    }

    public async Task checkErrorMessagescheduledwitdef()
    {
      if (UpdateBookedRecordings.use == "W")
      {
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=You are deleting {wit1} from the system");
        var errorMessage2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={wit1} is associated with: 1 scheduled recordings.");
        var errorMessage3 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=If you choose to delete {wit1}, all associated recordings will also be deleted.");

        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain($"You are deleting {wit1} from the system")));
        await Task.Run(() => Assert.That(errorMessage2.TextContentAsync().Result, Does.Contain($"{wit1} is associated with: 1 scheduled recordings.")));
        await Task.Run(() => Assert.That(errorMessage3.TextContentAsync().Result, Does.Contain($"If you choose to delete {wit1}, all associated recordings will also be deleted.")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }

      else if (UpdateBookedRecordings.use == "D")
      {
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=You are deleting {def1} from the system");
        var errorMessage2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={def1} is associated with: 1 scheduled recordings.");
        var errorMessage3 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=If you choose to delete {def1}, all associated recordings will also be deleted.");

        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain($"You are deleting {def1} from the system")));
        await Task.Run(() => Assert.That(errorMessage2.TextContentAsync().Result, Does.Contain($"{def1} is associated with: 1 scheduled recordings.")));
        await Task.Run(() => Assert.That(errorMessage3.TextContentAsync().Result, Does.Contain($"If you choose to delete {def1}, all associated recordings will also be deleted.")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
      }
      if (flag)
      {
        UpdateBookedRecordings.use = "WD"; flag = false;
      }
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task addMoreParticipants()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(50) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Full Name\"]").ClickAsync();
      if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Full Name\"]").FillAsync($"{newDef}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Defendant\")").Nth(2).ClickAsync();
        HooksInitializer.contactCount = HooksInitializer.contactCount++;
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Full Name\"]").FillAsync($"{newWit}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Witness\")").Nth(2).ClickAsync();
        HooksInitializer.contactCount++;
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Full Name\"]").FillAsync($"{newDef}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Defendant\")").Nth(2).ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(50) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Full Name\"]").FillAsync($"{newWit}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Witness\")").Nth(2).ClickAsync();
        HooksInitializer.contactCount = HooksInitializer.contactCount + 2;
      }

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Refresh Recordings\"]").ClickAsync();
    }

    public async Task checkBookAdd()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={stringCourt}").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").FillAsync($"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();

      if (UpdateBookedRecordings.use == "D")
      {
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
        await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Contain($"{newDef}")));
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
        await Task.Run(() => Assert.That(witBox.InputValueAsync().Result, Does.Contain($"{newWit}")));
      }
      else
      {
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
        await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Contain($"{newDef}")));
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
        await Task.Run(() => Assert.That(witBox.InputValueAsync().Result, Does.Contain($"{newWit}")));
      }
    }

    public async Task checkScheduleAdd()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{HooksUpdateBookedRecording.day}\\ {HooksUpdateBookedRecording.month}\\ {HooksUpdateBookedRecording.datee}\\ {HooksUpdateBookedRecording.year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newDef}\")");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newWit}\")");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newDef}\")");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newWit}\")");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
      }
    }

    public async Task checkManageAdd()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").ClickAsync();

      if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {def1}\"]").ClickAsync();
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"] >> text={newDef}");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {wit1}\"]").ClickAsync();
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"] >> text={newWit}");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {def1}\"]").ClickAsync();
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"] >> text={newDef}");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {wit1}\"]").ClickAsync();
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"] >> text={newWit}");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
      }
    }

    public async Task checkAdminAdd()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").FillAsync($"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(6) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Recording\"]").ClickAsync();

      if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {def1}\"]").ClickAsync();
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={newDef}");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {wit1}\"]").ClickAsync();
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={newWit}");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {def1}\"]").ClickAsync();
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={newDef}");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {wit1}\"]").ClickAsync();
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={newWit}");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
      }
    }

    public async Task checkAdmin()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").FillAsync($"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}").ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(6) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Recording\"]").ClickAsync();

      if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {Udef1}\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef1}");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
        var defBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef2}");
        await Task.Run(() => Assert.IsTrue(defBox2.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {Uwit1}\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit1}");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
        var witBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit2}");
        await Task.Run(() => Assert.IsTrue(witBox2.IsVisibleAsync().Result));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {Udef1}\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef1}");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
        var defBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef2}");
        await Task.Run(() => Assert.IsTrue(defBox2.IsVisibleAsync().Result));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {Uwit1}\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit1}");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
        var witBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit2}");
        await Task.Run(() => Assert.IsTrue(witBox2.IsVisibleAsync().Result));
      }
    }
    public async Task checkSchedule()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();

      if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newDef}\")");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newWit}\")");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newDef}\")");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"span:has-text(\"{newWit}\")");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
      }
    }
    public async Task checkScheduleRemovedscheduledWitDef()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{HooksUpdateBookedRecording.day}\\ {HooksUpdateBookedRecording.month}\\ {HooksUpdateBookedRecording.datee}\\ {HooksUpdateBookedRecording.year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Witness\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var witdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {wit1}").First;
        await Task.Run(() => Assert.IsFalse(witdropdown.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Defendants\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var defdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {def1}");
        await Task.Run(() => Assert.IsFalse(defdropdown.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "WD")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Witness\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var witdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {wit1}").First;
        if (witdropdown.IsVisibleAsync().Result == true)
        {
          await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        }
        await Task.Run(() => Assert.IsFalse(witdropdown.IsVisibleAsync().Result));

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Defendants\"]").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        var defdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {def1}");
        await Task.Run(() => Assert.IsFalse(defdropdown.IsVisibleAsync().Result));
      }
    }

    public async Task checkRemovedscheduledWitDef()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase.Trim()}");
      var result = Page.Frame("fullscreen-app-host").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");

      if (result.IsVisibleAsync().Result == false)
      {
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }

      await result.ClickAsync();

      if (UpdateBookedRecordings.use == "W")
      {
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");

        await Task.Run(() => Assert.That(witBox.InputValueAsync().Result, Does.Not.Contain($"{wit1}")));
      }

      else if (UpdateBookedRecordings.use == "D")
      {
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");

        await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Not.Contain($"{def1}")));
      }

      else if (UpdateBookedRecordings.use == "WD")
      {
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");

        await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Not.Contain($"{def1}")));

        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");

        await Task.Run(() => Assert.That(witBox.InputValueAsync().Result, Does.Not.Contain($"{wit1}")));
      }
    }
    public async Task checkManageRemovedscheduledWitDef()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{stringCase}");
      var searchmessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=There are no recordings matching your search criteria. Consider changing or remo");
      await Task.Run(() => Assert.That(searchmessage.InnerTextAsync().Result, Does.Contain("There are no recordings matching your search criteria.")));
    }

    public async Task checkAdminRemovedScheduledWitDef()
    {
      var day = DateTime.UtcNow.ToString("ddd");
      var month = DateTime.UtcNow.ToString("MM");
      var datee = DateTime.UtcNow.ToString("dd");
      var year = DateTime.UtcNow.ToString("yyyy");

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Home\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").FillAsync($"{stringCase}");

      var caseref = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}").First;
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await caseref.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Schedule Date: {datee}/{month}/{year}").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var recStatus = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Recording Status: Deleted");
      await Task.Run(() => Assert.That(recStatus.InnerTextAsync().Result, Does.Contain("Deleted")));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit\\ Recording\"]").ClickAsync();

      //bug S28-535
      // var dropdown = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("li[role='option']");

      // if (UpdateBookedRecordings.use == "W")
      // {
      //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\:\\ {wit1}\"]").ClickAsync();

      //   await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));
      // }
      // if (UpdateBookedRecordings.use == "D")
      // {
      //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court\\ Name\\.\\ Selected\\:\\ {def1}\"]").ClickAsync();

      //   await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      // }

    }
    public async Task removeAllWitDef()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      for (int i = 0; i < 4; i++)
      {
        await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").First.ClickAsync();
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(31)");
        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain("deleting")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task saveButtonDisabledCheck()
    {
      var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2);
      await Task.Run(() => Assert.IsTrue(saveButton.IsDisabledAsync().Result));
    }

    public async Task allBookRecordingCheck()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase}");
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");

      await Task.Run(() => Assert.That(witBox.InputValueAsync().Result, Does.Not.Contain($"{wit1}")));
      await Task.Run(() => Assert.That(witBox.InputValueAsync().Result, Does.Not.Contain($"{wit2}")));

      var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");

      await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Not.Contain($"{def1}")));
      await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Not.Contain($"{def2}")));
    }
    public async Task checkSaveIconDisabled()
    {

      for (int i = 0; i < 4; i++)
      {
        if (Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i).IsVisibleAsync().Result == true)
        {
          inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i);
        }
        if ((inputBoxes.InputValueAsync().Result).Contains("") && inputBoxes != null)
        {
          var saveicon = Page.Frame("fullscreen-app-host").Locator("div:nth-child(52)   div:nth-child(1)  div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").Nth(i + 1);
          System.Console.WriteLine(saveicon);
          System.Console.WriteLine(stringCase);
          await Task.Run(() => Assert.IsFalse(saveicon.IsEditableAsync().Result));
        }
        if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}") && inputBoxes != null)
        {
          var saveicon = Page.Frame("fullscreen-app-host").Locator("div:nth-child(52)   div:nth-child(1)  div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").Nth(i + 1);
          await Task.Run(() => Assert.IsTrue(saveicon.IsEnabledAsync().Result));
        }
        inputBoxes = null;
      }
    }

    public async Task checkBook()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={stringCourt}").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").FillAsync($"{stringCase}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
        var savedWitness = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={Uwit1}").First;
        await Task.Run(() => Assert.IsTrue(savedWitness.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
        var savedDefendant = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={Udef1}").First;
        await Task.Run(() => Assert.IsTrue(savedDefendant.IsVisibleAsync().Result));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Witness\"]").ClickAsync();
        var savedWitness = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={Uwit1}").First;
        await Task.Run(() => Assert.IsTrue(savedWitness.IsVisibleAsync().Result));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select your Defendants\"]").ClickAsync();
        var savedDefendant = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={Udef1}").First;
        await Task.Run(() => Assert.IsTrue(savedDefendant.IsVisibleAsync().Result));
      }
    }

    // public async Task checkManageAdd()
    // {
    //   while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").IsEnabledAsync().Result == false) { }
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
    //   while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsEnabledAsync().Result == false) { }
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{stringCase}");
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").ClickAsync();

    //   if (UpdateBookedRecordings.use == "D")
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {def1}\"]").ClickAsync();
    //     var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"] >> text={newDef}");
    //     await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
    //   }
    //   else if (UpdateBookedRecordings.use == "W")
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {wit1}\"]").ClickAsync();
    //     var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"] >> text={newWit}");
    //     await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
    //   }
    //   else
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {def1}\"]").ClickAsync();
    //     var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"] >> text={newDef}");
    //     await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {wit1}\"]").ClickAsync();
    //     var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"li[role=\"option\"] >> text={newWit}");
    //     await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
    //   }
    // }


    // public async Task checkAdminAdd()
    // {
    //   while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").IsEnabledAsync().Result == false) { }
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
    //   while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsEnabledAsync().Result == false) { }
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").FillAsync($"{stringCase}");
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(6) .react-knockout-control .appmagic-svg").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Recording\"]").ClickAsync();

    //   if (UpdateBookedRecordings.use == "D")
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {def1}\"]").ClickAsync();
    //     var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={newDef}");
    //     await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
    //   }
    //   else if (UpdateBookedRecordings.use == "W")
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {wit1}\"]").ClickAsync();
    //     var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={newWit}");
    //     await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
    //   }
    //   else
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {def1}\"]").ClickAsync();
    //     var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={newDef}");
    //     await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {wit1}\"]").ClickAsync();
    //     var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={newWit}");
    //     await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
    //   }
    // }

    // public async Task checkAdmin()
    // {
    //   while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").IsEnabledAsync().Result == false) { }
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
    //   while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsEnabledAsync().Result == false) { }
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").FillAsync($"{stringCase}");
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(6) .react-knockout-control .appmagic-svg").ClickAsync();
    //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Recording\"]").ClickAsync();

    //   if (UpdateBookedRecordings.use == "D")
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {Udef1}\"]").ClickAsync();
    //     var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef1}");
    //     await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
    //     var defBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef2}");
    //     await Task.Run(() => Assert.IsTrue(defBox2.IsVisibleAsync().Result));
    //   }
    //   else if (UpdateBookedRecordings.use == "W")
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {Uwit1}\"]").ClickAsync();
    //     var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit1}");
    //     await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
    //     var witBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit2}");
    //     await Task.Run(() => Assert.IsTrue(witBox2.IsVisibleAsync().Result));
    //   }
    //   else
    //   {
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {Udef1}\"]").ClickAsync();
    //     var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef1}");
    //     await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
    //     var defBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef2}");
    //     await Task.Run(() => Assert.IsTrue(defBox2.IsVisibleAsync().Result));
    //     await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {Uwit1}\"]").ClickAsync();
    //     var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit1}");
    //     await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
    //     var witBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit2}");
    //     await Task.Run(() => Assert.IsTrue(witBox2.IsVisibleAsync().Result));
    //   }
    // }
  }
}
