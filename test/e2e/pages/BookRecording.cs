using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace pre.test.pages
{
    public class BookRecording : BasePage
    {
    public BookRecording(IPage page) : base(page) { }
    public async Task NavigateToBooking()
        {
        await Page.IsVisibleAsync("button:has-text(\"Book a Recording\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
        }
    public async Task EnterCaseDetails()
    {
        var date = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
        await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"AutoTest{date}");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Birmingham\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
        await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "defendants 1,\ndefendants 2");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
        await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]", "Witness surname,\nWitness surname");
        await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
        }

        public async Task<string> CheckCaseCreated()
        {
          await Page.IsVisibleAsync("#publishedCanvas > div > div:nth-child(5) > div > div > div:nth-child(14) > div > div > div > div > div");
          var casecreated =  await Page.InnerTextAsync("");
          return casecreated;
        }
    }
}
