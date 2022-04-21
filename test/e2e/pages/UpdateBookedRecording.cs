using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class UpdateBookedRecording : BasePage
  {
    
    public UpdateBookedRecording(IPage page) : base(page)
    {
    }
    private String stringCourt = "";
    private String stringCase = "";
   public async Task NavigateToBooking()
    {
       await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Manage Recordings\")");
    }

    public async Task FindCaseToUpdate() {
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator( "div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)");
      stringCase = caseLocation.TextContentAsync().Result.ToString().Trim();
      stringCase = stringCase.Substring(stringCase.LastIndexOf(':') + 1);

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder='Search case ref']");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder='Search case ref']", $"{stringCase.Trim()}");
    }
    public async Task SearchCase()
    {
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)");
      stringCase = caseLocation.TextContentAsync().Result.ToString().Trim();
      stringCase = stringCase.Substring(stringCase.LastIndexOf(':') + 1);
      


      var courtLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(4)");
      stringCourt = courtLocation.TextContentAsync().Result.ToString().Trim();
      stringCourt = stringCourt.Substring(stringCourt.LastIndexOf(':') + 1);



      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      System.Console.WriteLine("This is "+stringCourt);
      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase.Trim()}");
    }
    public async Task FindCase()
    {


      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
      await Task.Run(() => Assert.That(caseLocation.TextContentAsync().Result, Does.Contain($"{stringCourt.Trim()}")));

      var caseName = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "#publishedCanvas div.canvasContentDiv.container_1vt1y2p div div:nth-child(1) div div div div div");
      await Task.Run(() => Assert.That(caseName.TextContentAsync().Result, Does.Contain($"{stringCase.Trim()}")));
    }
    public async Task UpdateCase()
    {
        await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");


      
      // await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      // await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "Updatedef 1,\nUpdatedef 2");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]","UpdateWitness 1,\nUpdateWitness 2");
      var enabled = Page.Frame("fullscreen-app-host").Locator(":nth-match(button:has-text(\"Save\"), 2)");
      //await Task.Run(() => Assert.IsTrue(enabled.IsEnabledAsync().Result));
     // await Page.Frame("fullscreen-app-host").IsEnabledAsync(":nth-match(button:has-text(\"Save\"), 2)");
      //await Page.Frame("fullscreen-app-host").ClickAsync("#publishedCanvas");
      await Task.Run(() => Assert.That(enabled.TextContentAsync().Result, Does.Contain("Save")));
      await UpdateBookedRecordings._pagesetters.Page.WaitForFunctionAsync(":nth-match(button:has-text(\"Save\"), 2)");
      
      //await Task.Run(() => Page.Frame("fullscreen-app-host").FocusAsync(":nth-match(button:has-text(\"Save\"), 2)"));
      //await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
      //await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");;;;;
    }
   }
}
