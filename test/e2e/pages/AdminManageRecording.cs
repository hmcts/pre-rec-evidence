using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using pre.test.pages;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace pre.test.pages
{
  public class AdminManageRecording : BasePage
  {
    public AdminManageRecording(IPage page) : base(page)
    {
    }
    protected string date = DateTime.UtcNow.ToString("dd/MM/yyyy");
    protected string pastDate = (DateTime.UtcNow.AddDays(-1)).ToString("dd/MM/yyyy");
    public static string oldDate = "";
    public static int n = 1;
    public string finalCount;
    public static List<string> Recordings = new List<string>();
    public static bool isDeleted = false;


    private String stringID = "";

    public async Task changeDate()
    {
      while (Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Video Link\"]").Nth(n).InputValueAsync().Result.Contains("http"))
      {
        n++;
      }

      oldDate = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n).InputValueAsync().Result.Trim();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n).ClickAsync();
      if (AdminManageRecordings.use == "normal")
      {
        if (date == oldDate)
        {
          date = (DateTime.UtcNow.AddDays(1)).ToString("dd/MM/yyyy");
        }
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n).FillAsync($"{date}");
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n).ClickAsync();
        await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      }
      else if (AdminManageRecordings.use == "past")
      {
        var dateLocation = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n);
        if (pastDate == dateLocation.InputValueAsync().Result)
        {
          pastDate = (DateTime.UtcNow.AddDays(-2)).ToString("dd/MM/yyyy");
        }
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n).FillAsync($"{pastDate}");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      }
    }
    public async Task checkDateChange()
    {
      var dateLocator = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording Start\"]").Nth(n);
      await Task.Run(() => Assert.That(dateLocator.InputValueAsync().Result, Does.Contain($"{date}")));

    }
    public async Task pastDateError()
    {
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var error = Page.Locator("text=Date cannot be in the past.");
      await error.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(error.IsVisibleAsync().Result));
    }
    public async Task PageCheck()
    {
      var Header = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Recording Start");
      await Task.Run(() => Assert.That(Header.TextContentAsync().Result, Does.Contain("Recording Start")));
    }
    public async Task CheckSaveButtonDisabled()
    {
      for (int i = 2; i < 7; i++)
      {
        var Button = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n);
        await Task.Run(() => Assert.IsFalse(Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Save\"]").Nth(n).IsVisibleAsync().Result));
      }
    }
    public async Task findRecording()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[placeholder=\"Case\\ Ref\\ \\\\\\ URN\\ \\\\\\ ID\\ \\\\\\ Court\"]", $"{ExternalPortal.caseName}");
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text=Case Ref: {ExternalPortal.caseName}").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      //      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      //await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(48) div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p div:nth-child(2)").First.WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(48) div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p div:nth-child(2)").First.ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      var RceordingLocation = Page.Frame("fullscreen-app-host").Locator("div:nth-child(49) div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div >div:nth-child(1)").First;
      stringID = RceordingLocation.TextContentAsync().Result.ToString().Trim();

      stringID = stringID.Substring(stringID.LastIndexOf(':') + 1);
      if (AdminManageRecordings.use == "F")
      {
        
        
        var Date = DateTime.Now.AddSeconds(7);
         while (DateTime.Now < Date)
         {
              await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings\"]").PressAsync("ArrowDown");
         }
         var gallery = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#publishedCanvas > div > div:nth-child(3) > div > div > div:nth-child(49) > div > div > div > div > div.virtualized-gallery > div > div > div").Last.InnerHTMLAsync().Result;
        // System.Console.WriteLine(something +"something");
         var removinghtml1 = gallery.Substring(gallery.IndexOf("Item") + 5);
        var removinghtml2 = Regex.Replace(removinghtml1.Split()[0], @"[^0-9a-zA-Z\ ]+", "");
         finalCount = removinghtml2.Substring(0,(removinghtml2.IndexOf("divdiv"))); //USEEE THIS VARIABLE!!!!
        // System.Console.WriteLine(finalCount + "item");
        
        var finalResult = Int32.Parse(finalCount);  
         var Date2 = DateTime.Now.AddSeconds(7);
         while (DateTime.Now < Date2)
         {
              await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings\"]").PressAsync("ArrowUp");
         }
        for(int i = 0; i < finalResult; i ++)
        {
          var RecordingLocation = Page.Frame("fullscreen-app-host").Locator("div:nth-child(49) div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div >div:nth-child(1)").First;
          Recordings.Add(RecordingLocation.InnerTextAsync().Result);
          // if(i% 2 == 0)
          // {
             var Date3 = DateTime.Now.AddMinutes(3);
            while(RecordingLocation.InnerTextAsync().Result == Page.Frame("fullscreen-app-host").Locator("div:nth-child(49) div.virtualized-gallery div.canvasContentDiv.container_1vt1y2p > div >div:nth-child(1)").First.InnerTextAsync().Result)
            {
                 await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings\"]").PressAsync("ArrowDown");
                 if(Date3 > DateTime.Now)
                 {
                   break;
                 }
            }
            
            
           System.Threading.Thread.Sleep(2000);


          // }
          System.Console.WriteLine(Recordings[i]);
        }
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordsdvbings\"]").ClickAsync();
        
         
        
        
        
      
        //var n = 1;

        // for (int i = 1; i <= n; i++)
        // {
        //   System.Console.WriteLine(n + "here");
        //   Recording = Page.Frame("fullscreen-app-host").Locator($"div:nth-child(49) div:nth-child({i}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)");
        //   var temp = Recording.TextContentAsync().Result.Trim();

        //   var temp2 = temp.Substring(temp.LastIndexOf(':') + 1);
        //   //System.Console.WriteLine(temp2 + "thankyou");
        //   Recordings.Add(temp2);
        //   if (!(Page.Frame("fullscreen-app-host").Locator($"div:nth-child(49) div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)").IsVisibleAsync().Result))
        //   {
        //     // for (int j = 0; j < 46; j++)
        //     // {
        //       var Date = DateTime.Now.AddSeconds(1);
        //       while (DateTime.Now < Date)
        //       {
        //         await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recordings\"]").PressAsync("ArrowDown");
        //         //await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

        //       }
        //     //}
        //   }

        //   if (Page.Frame("fullscreen-app-host").Locator($"div:nth-child(49) div:nth-child({i + 1}) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(1)").IsVisibleAsync().Result)
        //   {
        //     n++;
        //     if (n == 6)
        //     {
        //       n = 1;
        //     }


        //   }



       // }
        
      }
      //await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      // await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder='Search case ref']");
      // await Page.Frame("fullscreen-app-host").FillAsync("[placeholder='Search case ref']", $"{stringID.Trim()}");
      // await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }
    public async Task search()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Recordings\"]").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      //      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\.\\.\\.\"]").ClickAsync();
      if (AdminManageRecordings.use == "caseref")
      {

        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\.\\.\\.\"]").FillAsync($"{ExternalPortal.caseName}");
        var UID = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]");
        //System.Console.WriteLine(UID.CountAsync().Result + "hello");
        //System.Console.WriteLine(finalCount + "goodbye");
        //await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Searcgfctdyh\\.\\.\\.\"]").ClickAsync();
        var Date = DateTime.Now.AddSeconds(7);
         while (DateTime.Now < Date)
         {
            await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]").Last.ClickAsync();
              await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]").Last.PressAsync("ArrowDown");
         }
         var gallery = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(3) > div > div > div:nth-child(71) > div > div > div > div > div.virtualized-gallery > div > div > div").Last.InnerHTMLAsync().Result;
       
         var removinghtml1 = gallery.Substring(gallery.IndexOf(">Item") + 5);
          
       // var removinghtml2 = Regex.Replace(removinghtml1.Split()[3], @"[^a-zA-Z\ ]+", "");
       var result =(removinghtml1.Remove(removinghtml1.Length-(removinghtml1.Length-3))).Trim();
       var finalResult = Int32.Parse(result);
       // System.Console.WriteLine(result +"something");
        //  var finalCount2 = removinghtml2.Substring(0,(removinghtml2.IndexOf("divdiv"))); //USEEE THIS VARIABLE!!!!
        //  System.Console.WriteLine(finalCount2 + "hello");
        // System.Console.WriteLine(finalCount + "goodbye");
         //await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Searcgfctdyh\\.\\.\\.\"]").ClickAsync();
       await Task.Run(() => Assert.AreEqual(result, finalCount)); 
        for (int i = 0; i < finalResult; i++)
        {
          var temp = UID.Nth(i).InputValueAsync().Result.Trim();
          await Task.Run(() => Assert.AreEqual(temp, Recordings[i]));
        }

      // }
      // else
      // {
      //   await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\.\\.\\.\"]").FillAsync($"{stringID.Trim()}");
      // }
      // await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      // await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      // var ID = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ ID\"]").Nth(1);
      // //await ID.WaitForAsync();
      // await Task.Run(() => Assert.That(ID.InputValueAsync().Result, Does.Contain(stringID.Trim())));



    }
    }

    public async Task delete()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Delete\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Yes\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      isDeleted = true;
      var message = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=There are no recordings matching your search criteria. Consider changing or remo").Nth(1);
      await Task.Run(() => Assert.That(message.TextContentAsync().Result, Does.Contain("no recordings")));
    }
    public async Task checkView()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"View Recordings\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search\\ case\\ ref\"]").FillAsync($"{stringID.Trim()}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      var message = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div:nth-child(29)");
      await message.WaitForAsync();
      await Task.Run(() => Assert.That(message.TextContentAsync().Result, Does.Contain("no recordings")));

    }
    public async Task checkDelete()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("label rect").ClickAsync();
      await Task.Run(() => Assert.That(Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Item 1. Selected. Off >> [aria-label=\"Recording\\ ID\"]").InputValueAsync().Result, Does.Contain(stringID.Trim())));
      var deleted = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Recording\\ Status\"]");
      await Task.Run(() => Assert.That(deleted.InputValueAsync().Result, Does.Contain("Deleted")));
    }
    public async Task gotoManageCases()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Home\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Cases\"]").First.ClickAsync();


    }
    public async Task checkManageCases()
    {

      var restoreButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Restore\\ Recording\"]");
      await restoreButton.WaitForAsync();
      await Task.Run(() => Assert.IsFalse(restoreButton.IsVisibleAsync().Result));
      await restoreButton.ClickAsync();
    }

  }


}



