using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using NUnit.Framework;
using System;
using Microsoft.Playwright;

namespace pre.test.Hooks
{

  [Binding]
  public class HooksManageUsers
  {

    [BeforeScenario("ManageUsers", Order = 1)]
    public async Task goToManageUsers()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.sboxUrl}");
      ManageUser.createUserLastName = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
      ManageUser.newUserEmail = $"autotest{ManageUser.createUserLastName}";
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Users\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add Users\"]").ClickAsync();
    }

    [BeforeScenario("SuperUserManageUsers", Order = 1)]
    public async Task goToManageUsersSuperUser()
    {
      await HooksInitializer._context.Page.GotoAsync($"{HooksInitializer.sboxUrl}");

      ManageUser.createUserLastName = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
      ManageUser.newUserEmail = $"autotest{ManageUser.createUserLastName}";
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Super User\")").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Users\"]").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User\"]").ClickAsync();
    }
    [BeforeScenario("SuperUserCreateAUser", Order = 2)]
    public async Task superUserCreateAUser()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add\\ User\\ First\\ Name\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add\\ User\\ First\\ Name\"]").FillAsync($"{ManageUser.createUserFirstName}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Last\\ Name\"]").Nth(1).ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Last\\ Name\"]").Nth(1).FillAsync($"{ManageUser.createUserLastName}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add\\ User\\ Email\"]").FillAsync($"{ManageUser.newUserEmail}");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add\\ User\\ Phone\\ Number\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add\\ User\\ Phone\\ Number\"]").FillAsync("n/a");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add\\ User\\ Organisation\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add\\ User\\ Organisation\"]").FillAsync("n/a");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add\\ User\\ Role\"]").ClickAsync();
      var superUser = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Super User functions");
      await Task.Run(() => Assert.IsTrue(superUser.IsVisibleAsync().Result));
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Super User functions").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Add New User\")").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      HooksInitializer.contacts.Add($"{ManageUser.createUserFirstName} {ManageUser.createUserLastName}");
    
    }

    [BeforeScenario("CreateAUser", Order = 2)]
    public async Task CreateAUser()
    {
      if (ManageUser.use == "duplicateEmail")
      {
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).FillAsync($"{ManageUser.existingEmailFirstName}");
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).FillAsync($"{ManageUser.existingEmailLastName}");
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").FillAsync($"{ManageUser.existingEmail}");
      }
      else if (ManageUser.use == "blankEmail")
      {
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).FillAsync($"{ManageUser.createUserFirstName}");
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).FillAsync($"{ManageUser.createUserLastName}");
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").FillAsync($"");
      }
      else
      {
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).FillAsync($"{ManageUser.createUserFirstName}");
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).ClickAsync();
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).FillAsync($"{ManageUser.createUserLastName}");
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").FillAsync($"{ManageUser.newUserEmail}");

      }
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).FillAsync("n/a");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).FillAsync("n/a");
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_1lj5p80").Nth(1).ClickAsync();
      var superUser = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Super User functions");
      await Task.Run(() => Assert.IsFalse(superUser.IsVisibleAsync().Result));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=External Access via Portal").ClickAsync();
      if (ManageUser.use != "blankEmail")
      {
        await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Add New User\")").ClickAsync();
        await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
        if( ManageUser.use != "duplicateEmail")
        {
        HooksInitializer.contacts.Add($"{ManageUser.createUserFirstName} {ManageUser.createUserLastName}");
        }
      }
    }

    [BeforeScenario("SuperscreencheckUser", Order = 3)]
    public async Task superscreenCheckRecordIsCreated()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search Users\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{ManageUser.createUserLastName}");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      

      var UserBox = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={ManageUser.createUserFirstName} {ManageUser.createUserLastName}");
      await Task.Run(() => Assert.IsTrue(UserBox.IsVisibleAsync().Result));
      await Task.Run(() => UserBox.ClickAsync());

      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var firstName = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").First;
      await Task.Run(() => Assert.That(firstName.InputValueAsync().Result, Does.Contain($"{ManageUser.createUserFirstName}")));

      var lastName = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").First;
      await Task.Run(() => Assert.That(lastName.InputValueAsync().Result, Does.Contain($"{ManageUser.createUserLastName}")));

      var email = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Email\"]");
      await Task.Run(() => Assert.That(email.InputValueAsync().Result, Does.Contain($"{ManageUser.newUserEmail}")));

      var phoneNumber = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").First;
      await Task.Run(() => Assert.That(phoneNumber.InputValueAsync().Result, Does.Contain($"n/a")));

      var organisation = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Organisation\\ Name\"]");
      await Task.Run(() => Assert.That(organisation.InputValueAsync().Result, Does.Contain($"n/a")));

      var role = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Role\\.\\ Selected\\:\\ Super\\ User\"]");
      await Task.Run(() => Assert.IsTrue(role.IsVisibleAsync().Result));

      // Bug S28-526 
      // var status = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Status\"]").Nth(1);
      // await Task.Run(() => Assert.That(status.InputValueAsync().Result, Does.Contain($"Active")));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Lock\\ Account\"]").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    [BeforeScenario("checkUser", Order = 3)]

    public async Task CheckRecordIsCreated()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search User\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{ManageUser.createUserLastName}");
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var UserBox = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={ManageUser.createUserFirstName} {ManageUser.createUserLastName}");
      await Task.Run(() => Assert.IsTrue(UserBox.IsVisibleAsync().Result));
      await Task.Run(() => UserBox.ClickAsync());

      var firstName = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ First\\ Name\"]").First;
      await Task.Run(() => Assert.That(firstName.InputValueAsync().Result, Does.Contain($"{ManageUser.createUserFirstName}")));

      var lastName = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").First;
      await Task.Run(() => Assert.That(lastName.InputValueAsync().Result, Does.Contain($"{ManageUser.createUserLastName}")));

      var email = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Email\"]");
      await Task.Run(() => Assert.That(email.InputValueAsync().Result, Does.Contain($"{ManageUser.newUserEmail}")));

      var phoneNumber = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").First;
      await Task.Run(() => Assert.That(phoneNumber.InputValueAsync().Result, Does.Contain($"n/a")));

      var organisation = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").First;
      await Task.Run(() => Assert.That(organisation.InputValueAsync().Result, Does.Contain($"n/a")));

      var role = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Roles\\. Selected\\: Level 3\"]").First;
      await Task.Run(() => Assert.IsTrue(role.IsVisibleAsync().Result));

      // Bug S28-526 
      // var status = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Status\"]").Nth(1);
      // await Task.Run(() => Assert.That(status.InputValueAsync().Result, Does.Contain($"Active")));

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Lock\"]").ClickAsync();
      await HooksInitializer._context.Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }


  }

}


