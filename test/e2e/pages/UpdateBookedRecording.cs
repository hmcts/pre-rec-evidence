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

    public async Task FindCaseToView()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Manage Recordings\")");
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(19) div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)");
      stringCase = caseLocation.TextContentAsync().Result.ToString().Trim();
      stringCase = stringCase.Substring(stringCase.LastIndexOf(':') + 1);

      var courtLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(19)  div.virtualized-gallery > div > div > div:nth-child(1)  div:nth-child(4)");
      stringCourt = courtLocation.TextContentAsync().Result.ToString().Trim();
      stringCourt = stringCourt.Substring(stringCourt.LastIndexOf(':') + 1);
    }
    public async Task SearchCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase.Trim()}");
    }
    public async Task FindCase()
    {
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
      await Task.Run(() => Assert.That(caseLocation.AllInnerTextsAsync().Result, Does.Contain($"{stringCourt.Trim()}")));

      var caseName = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "div:nth-child(45) div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1) ");
      await Task.Run(() => Assert.That(caseName.InnerTextAsync().Result, Does.Contain($"{stringCase.Trim()}")));
    }
    public async Task UpdateCase()
    {

      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");

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
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit2}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Uwit2}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();
          }
        }
        else if (UpdateBookedRecordings.use == "D")
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Udef1}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();

          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def2}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Udef2}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();

          }
        }
        else
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Uwit1}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();

          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit2}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Uwit2}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();

          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Udef1}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();

          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def2}"))
          {
            await inputBoxes.ClickAsync();
            await inputBoxes.FillAsync($"{Udef2}");
            await Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52)  div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)").ClickAsync();

          }
        }
      }

      var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2);
      await Task.Run(() => Assert.IsTrue(saveButton.IsEnabledAsync().Result));
      await Task.Run(() => saveButton.ClickAsync());
    }

    public async Task CheckUpdatedCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
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
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{stringCase}");

      var updatecaseScheduled = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator($"div.virtualized-gallery:has-text(\"{stringCourt}\")");
      await Task.Run(() =>
        Assert.That(updatecaseScheduled.InnerTextAsync().Result, Does.Contain($"{stringCase}")));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Amend\")").First.ClickAsync();

      var dropdown = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("li[role='option']");

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {Uwit1}\"]").ClickAsync();
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Uwit1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Uwit2}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {Udef1}\"]").ClickAsync();
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Udef1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Udef2}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {Uwit1}\"]").ClickAsync();
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Uwit1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Uwit2}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{wit2}")));
        await Page.Frame("fullscreen-app-host").ClickAsync("text=HMCTS Logo Dev Home Manage Recordings Court Court NameOpen popup to select items");

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Defendants\\. Selected\\: {Udef1}\"]").ClickAsync();
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Udef1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Contain($"{Udef2}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def1}")));
        await Task.Run(() => Assert.That(dropdown.AllInnerTextsAsync().Result, Does.Not.Contain($"{def2}")));
      }
    }
    public async Task removeWitDefNotScheduled()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");

      for (int i = 0; i < 4; i++)
      {
        if(Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i).IsVisibleAsync().Result == true){
        inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i);
        }
        if (UpdateBookedRecordings.use == "W" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit2}"))
          {

            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
          }
        }
        else if (UpdateBookedRecordings.use == "D" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def2}"))
          {
            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
          }

        }
        else if (UpdateBookedRecordings.use == "WD" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit2}"))
          {

            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            flag = true;
            UpdateBookedRecordings.use = "W";
            await checkErrorMessage();
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def2}"))
          {

            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            flag = true;
            UpdateBookedRecordings.use = "D";
            await checkErrorMessage();

          }
          inputBoxes = null;
}
      }
    }

    public async Task checkErrorMessage()
    {
      if (UpdateBookedRecordings.use == "W")
      {
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=You are deleting {wit2} from the system");
        var errorMessage2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={wit2} is associated with: 0 Recordings.");
        var errorMessage3 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=If you choose to delete {wit2}, all associated recordings will also be deleted.");

        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain($"You are deleting {wit2} from the system")));
        await Task.Run(() => Assert.That(errorMessage2.TextContentAsync().Result, Does.Contain($"{wit2} is associated with: 0 Recordings.")));
        await Task.Run(() => Assert.That(errorMessage3.TextContentAsync().Result, Does.Contain($"If you choose to delete {wit2}, all associated recordings will also be deleted.")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
      }

      if (UpdateBookedRecordings.use == "D")
      {
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=You are deleting {def2} from the system");
        var errorMessage2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={def2} is associated with: 0 Recordings.");
        var errorMessage3 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=If you choose to delete {def2}, all associated recordings will also be deleted.");

        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain($"You are deleting {def2} from the system")));
        await Task.Run(() => Assert.That(errorMessage2.TextContentAsync().Result, Does.Contain($"{def2} is associated with: 0 Recordings.")));
        await Task.Run(() => Assert.That(errorMessage3.TextContentAsync().Result, Does.Contain($"If you choose to delete {def2}, all associated recordings will also be deleted.")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
      }
      if (flag) { UpdateBookedRecordings.use = "WD"; flag = false; }
    }
    public async Task checkRemovedWitDef()
    {

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase}");
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

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Witness\"]").ClickAsync();
        var witdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {wit2}");
        while (witdropdown.IsVisibleAsync().Result == true) { }
        await Task.Run(() => Assert.IsFalse(witdropdown.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Defendants\"]").ClickAsync();
        var defdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {def2}");
        while (defdropdown.IsVisibleAsync().Result == true) { }
        await Task.Run(() => Assert.IsFalse(defdropdown.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "WD")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Defendants\"]").ClickAsync();
        var defdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {def2}");
        while (defdropdown.IsVisibleAsync().Result == true) { }
        await Task.Run(() => Assert.IsFalse(defdropdown.IsVisibleAsync().Result));

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Witness\"]").ClickAsync();
        var witdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {wit2}");
        while (witdropdown.IsVisibleAsync().Result == true) { }
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
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").FillAsync($"{stringCase}");
      var caseref = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}");
      await Task.Run(() => Assert.That(caseref.InnerTextAsync().Result, Does.Contain($"{stringCase}")));
      await caseref.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Schedule Date: {datee}/{month}/{year}").ClickAsync();
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
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");

      for (int i = 0; i < 4; i++)
      
      {
       if(Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i).IsVisibleAsync().Result == true){
        inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i);
        }
        if (UpdateBookedRecordings.use == "W" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {

            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
          }
        }
        else if (UpdateBookedRecordings.use == "D" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {

            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
          }
        }
        else if (UpdateBookedRecordings.use == "WD" && inputBoxes != null)
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {

            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            flag = true;
            UpdateBookedRecordings.use = "W";
            await checkErrorMessagescheduledwitdef();
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {

            await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").Nth(i).ClickAsync();
            flag = true;
            UpdateBookedRecordings.use = "D";
            await checkErrorMessagescheduledwitdef();

          }
          inputBoxes = null;
        }
        else if (UpdateBookedRecordings.use == "T" )
        {
          if ((inputBoxes.InputValueAsync().Result).Contains($"{wit1}"))
          {
            
            await inputBoxes.FillAsync("");
            System.Console.WriteLine (inputBoxes);
            var saveicon= Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52) div:nth-child({i+1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
            await saveicon.ClickAsync();
            System.Console.WriteLine (saveicon);
           await Task.Run(() => Assert.IsFalse(saveicon.IsCheckedAsync().Result));
           
          }
          if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}"))
          {

            await inputBoxes.FillAsync("");
            var saveicon= Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52) div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
            await saveicon.ClickAsync();
           await Task.Run(() => Assert.IsFalse(saveicon.IsDisabledAsync().Result));

          }
        }
      }
    }

    public async Task checkErrorMessagescheduledwitdef()
    {
      if (UpdateBookedRecordings.use == "W")
      {
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=You are deleting {wit1} from the system");
        var errorMessage2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={wit1} is associated with: 1 Recordings.");
        var errorMessage3 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=If you choose to delete {wit1}, all associated recordings will also be deleted.");

        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain($"You are deleting {wit1} from the system")));
        await Task.Run(() => Assert.That(errorMessage2.TextContentAsync().Result, Does.Contain($"{wit1} is associated with: 1 Recordings.")));
        await Task.Run(() => Assert.That(errorMessage3.TextContentAsync().Result, Does.Contain($"If you choose to delete {wit1}, all associated recordings will also be deleted.")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();

      }

      else if (UpdateBookedRecordings.use == "D")
      {
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=You are deleting {def1} from the system");
        var errorMessage2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={def1} is associated with: 1 Recordings.");
        var errorMessage3 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=If you choose to delete {def1}, all associated recordings will also be deleted.");

        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain($"You are deleting {def1} from the system")));
        await Task.Run(() => Assert.That(errorMessage2.TextContentAsync().Result, Does.Contain($"{def1} is associated with: 1 Recordings.")));
        await Task.Run(() => Assert.That(errorMessage3.TextContentAsync().Result, Does.Contain($"If you choose to delete {def1}, all associated recordings will also be deleted.")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();

      }
      if (flag) { UpdateBookedRecordings.use = "WD"; flag = false; }
  }
     public async Task checkScheduleRemovedscheduledWitDef()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();

      if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Witness\"]").ClickAsync();
        var witdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {wit1}");
        while (witdropdown.IsVisibleAsync().Result == true) { }
        await Task.Run(() => Assert.IsFalse(witdropdown.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Defendants\"]").ClickAsync();
        var defdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {def1}");
        while (defdropdown.IsVisibleAsync().Result == true) { }
        await Task.Run(() => Assert.IsFalse(defdropdown.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "WD")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Witness\"]").ClickAsync();
        var witdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {wit1}");
        while (witdropdown.IsVisibleAsync().Result == true) { }
        await Task.Run(() => Assert.IsFalse(witdropdown.IsVisibleAsync().Result));

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select\\ your\\ Defendants\"]").ClickAsync();
        var defdropdown = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text= {def1}");
        while (defdropdown.IsVisibleAsync().Result == true) { }
        await Task.Run(() => Assert.IsFalse(defdropdown.IsVisibleAsync().Result));
      }
    }

    public async Task checkRemovedscheduledWitDef()
    {

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase}");
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");

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
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]").FillAsync($"{stringCase}");

      var caseref = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}").First;
      while (caseref.IsVisibleAsync().Result == false){}
      await caseref.ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Schedule Date: {datee}/{month}/{year}").ClickAsync();

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
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");

      for (int i = 0; i < 4; i++)
      {

        await Page.Frame("fullscreen-app-host").Locator("div:nth-child(6) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").First.ClickAsync();
        var errorMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(31)");
        await Task.Run(() => Assert.That(errorMessage.TextContentAsync().Result, Does.Contain("deleting")));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Delete\")").ClickAsync();
      }
    }
    public async Task saveButtonDisabledCheck()
    {
      var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2);
      await Task.Run(() => Assert.IsTrue(saveButton.IsDisabledAsync().Result));
    }

    public async Task allBookRecordingCheck()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = Page.Frame("fullscreen-app-host").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host").FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase}");
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");

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
        if(Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i).IsVisibleAsync().Result == true){
        inputBoxes = Page.Frame("fullscreen-app-host").Locator("div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p input").Nth(i);
        }
        if ((inputBoxes.InputValueAsync().Result).Contains("")  && inputBoxes != null)
          {

           var saveicon= Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52) div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
           await Task.Run(() => Assert.IsTrue(saveicon.IsEnabledAsync().Result));
          }
        if ((inputBoxes.InputValueAsync().Result).Contains($"{def1}") && inputBoxes != null)
          {

            var saveicon= Page.Frame("fullscreen-app-host").Locator($"div:nth-child(52) div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
            await Task.Run(() => Assert.IsTrue(saveicon.IsEnabledAsync().Result));

          }
        inputBoxes = null;
        }
      }
    }
  }
