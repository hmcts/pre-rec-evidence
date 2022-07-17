using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace pre.test.pages
{
    public class BookRecording : BasePage
    {
     public static string date = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
    public BookRecording(IPage page) : base(page) { }
    
    public async Task NavigateToBooking()
        {
        await Page.IsVisibleAsync("button:has-text(\"Book a Recording\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
        }
    public async Task EnterCaseDetails()
    {
        
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
       
        public async Task ScheduleRecording()
        {
        var day = DateTime.UtcNow.ToString("ddd");
        var month = DateTime.UtcNow.ToString("MMM");
        var date = DateTime.UtcNow.ToString("dd");
        var year = DateTime.UtcNow.ToString("yyyy");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"Witness surname\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
        await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"defendants 1\")");
        await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
       

        }
        
        public async Task CheckSchedule()
        {
        
            
            await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
            await Page.IsVisibleAsync("button:has-text(\"Manage Recordings\")");
            await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Manage Recordings\")");
            await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
            await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ",$"AutoTest{date}");
            await Page.Frame("fullscreen-app-host").ClickAsync($"div.virtualized-gallery:has-text(\"AutoTest{date}\")");

        }
        


         public async Task CheckCaseCreated()
         {
          System.Threading.Thread.Sleep(1000);
          await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
          System.Threading.Thread.Sleep(500);
          await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
          await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
          await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"Birmingham\")");
          await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
          await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
          await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"AutoTest{date}");
          await Page.Frame("fullscreen-app-host").ClickAsync("#publishedCanvas  div.canvasContentDiv.container_1vt1y2p  div:nth-child(2)");
          
        
        }

    

    }

     
}

