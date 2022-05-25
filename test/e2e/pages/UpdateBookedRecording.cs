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
    public static string newWit = "NewWitnessWow";
    public static string newDef = "WowNewDef";


    public async Task FindCaseToView()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Manage Recordings\")");
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
      .Locator("div:nth-child(19) div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)");
      stringCase = caseLocation.TextContentAsync().Result.ToString().Trim();
      stringCase = stringCase.Substring(stringCase.LastIndexOf(':') + 1);

      var courtLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
      .Locator("div:nth-child(19)  div.virtualized-gallery > div > div > div:nth-child(1)  div:nth-child(4)");
      stringCourt = courtLocation.TextContentAsync().Result.ToString().Trim();
      stringCourt = stringCourt.Substring(stringCourt.LastIndexOf(':') + 1);
    }


    public async Task SearchCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase.Trim()}");
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

      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");

      await Page.Frame("fullscreen-app-host").ClickAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text", $"{stringCase}");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
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
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").IsEnabledAsync().Result == false) { }
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

    public async Task addMoreParticipants()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Modify\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(50) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Case Number \\\\ URN\"]").ClickAsync();

      if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Case Number \\\\ URN\"]").FillAsync($"{newDef}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Defendant\")").Nth(2).ClickAsync();
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Case Number \\\\ URN\"]").FillAsync($"{newWit}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Witness\")").Nth(2).ClickAsync();
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Case Number \\\\ URN\"]").FillAsync($"{newDef}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Defendant\")").Nth(2).ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Full Name: Role: RoleOpen popup to select items. >> [placeholder=\"Case Number \\\\ URN\"]").FillAsync($"{newWit}");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Select Court\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("span:has-text(\"Witness\")").Nth(2).ClickAsync();
      }

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(3) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .powerapps-icon").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save\")").Nth(2).ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Refresh Recordings\"]").ClickAsync();
    }

    public async Task checkBookAdd()
    {
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(42) .appmagic-borderfill-container .appmagic-border-inner .react-knockout-control .appmagic-textbox .appmagic-text").FillAsync($"{stringCase}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div[role=\"button\"]:has-text(\"Court Name\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={stringCourt}").ClickAsync();
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

    public async Task checkManageAdd()
    {
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Manage Recordings\")").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search Case Ref\"]").FillAsync($"{stringCase}");
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
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").FillAsync($"{stringCase}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(6) .react-knockout-control .appmagic-svg").ClickAsync();
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
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Book a Recording\")").IsEnabledAsync().Result == false) { }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Cases\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Case Ref \\\\ URN \\\\ ID \\\\ Court\"]").FillAsync($"{stringCase}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {stringCase}").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator(".container_1f0sgyp div:nth-child(6) .react-knockout-control .appmagic-svg").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Edit Recording\"]").ClickAsync();

      if (UpdateBookedRecordings.use == "D")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {Udef1}\"]").ClickAsync();
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef1}");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
        var defBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef2}");
        await Task.Run(() => Assert.IsTrue(defBox2.IsVisibleAsync().Result));
      }
      else if (UpdateBookedRecordings.use == "W")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {Uwit1}\"]").ClickAsync();
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit1}");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
        var witBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit2}");
        await Task.Run(() => Assert.IsTrue(witBox2.IsVisibleAsync().Result));
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Court Name\\. Selected\\: {Udef1}\"]").ClickAsync();
        var defBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef1}");
        await Task.Run(() => Assert.IsTrue(defBox.IsVisibleAsync().Result));
        var defBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Udef2}");
        await Task.Run(() => Assert.IsTrue(defBox2.IsVisibleAsync().Result));
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"[aria-label=\"Selected\\: {Uwit1}\"]").ClickAsync();
        var witBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit1}");
        await Task.Run(() => Assert.IsTrue(witBox.IsVisibleAsync().Result));
        var witBox2 = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"ul[role=\"listbox\"] >> text={Uwit2}");
        await Task.Run(() => Assert.IsTrue(witBox2.IsVisibleAsync().Result));
      }
    }

  }
}
