using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using pre.test.Hooks;

namespace pre.test.pages
{
  public class ManageUser : BasePage
  {
    protected static Microsoft.Extensions.Configuration.IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("secrets.json")
    .Build();
    public static string existingEmail = config["portalEmail"];
    public static string existingEmailFirstName = "test";
    public static string existingEmailLastName = "existing email";
    public static string createUserFirstName = "Automated testUser";
    public static string createUserLastName = "";
    public static string use = "";
    public static string newUserEmail = "";
    public static bool first = true;
    public static bool last = false;

    public ManageUser(IPage page) : base(page) { }

    public async Task CheckEmailErrorMessage()
    {
      var EmailError = ManageUsers._pagesetters.Page.Locator("text=Email already exists in system");
      await Task.Run(() => Assert.That(EmailError.InnerTextAsync().Result, Does.Contain("Email already exists in system")));
    }
    public async Task CheckRecordIsntCreated()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search User\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{existingEmailFirstName} {existingEmailLastName}");
      
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      var UserBox = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={existingEmailFirstName} {existingEmailLastName}");
      await Task.Run(() => Assert.IsFalse(UserBox.IsVisibleAsync().Result));
    }
    public async Task DisabledSave()
    {
      var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Add New User\")");
      await Task.Run(() => Assert.IsTrue(saveButton.IsDisabledAsync().Result));
    }

    public async Task enabledSave()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Add New User\")");
      await Task.Run(() => Assert.IsFalse(saveButton.IsDisabledAsync().Result));

      await saveButton.ClickAsync();
       
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task UpdateFirstName()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ First\\ Name\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ First\\ Name\"]").First.FillAsync($"{createUserFirstName}Update");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      HooksInitializer.contacts.RemoveAt(HooksInitializer.contacts.Count -1);
      HooksInitializer.contacts.Add($"{ManageUser.createUserFirstName}Update {ManageUser.createUserLastName}");
    }
    public async Task UpdateFirstNameCheck()
    {
      await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      var firstName = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").First;
      await firstName.WaitForAsync();
      await Task.Run(() => Assert.That(firstName.InputValueAsync().Result, Does.Contain($"{createUserFirstName}Update")));
    }

    public async Task UpdateLastName()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Last\\ Name\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Last\\ Name\"]").First.FillAsync($"{createUserLastName}Update");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      HooksInitializer.contacts.RemoveAt(HooksInitializer.contacts.Count -1);
      HooksInitializer.contacts.Add($"{ManageUser.createUserFirstName} {ManageUser.createUserLastName}Update");
    }

    
    public async Task UpdateLastNameCheck()
    {
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      var lastName = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").First;
      await lastName.WaitForAsync();
      await Task.Run(() => Assert.That(lastName.InputValueAsync().Result, Does.Contain($"{createUserLastName}Update")));
    }
    public async Task UpdateEmail()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{createUserLastName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]").FillAsync($"{newUserEmail}Update");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task UpdateEmailCheck()
    {
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      var email = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]");
      await email.WaitForAsync();
      await Task.Run(() => Assert.That(email.InputValueAsync().Result, Does.Contain($"{newUserEmail}Update")));
    }

    public async Task UpdatePhoneNo()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Phone\\ Number\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Phone\\ Number\"]").First.FillAsync("n/aUpdate");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }

    public async Task UpdatePhoneNoCheck()
    {
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      var phoneNumber = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").First;
      await phoneNumber.WaitForAsync();
      await Task.Run(() => Assert.That(phoneNumber.InputValueAsync().Result, Does.Contain("n/aUpdate")));
    }

    public async Task UpdateOrganisation()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Organisation\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Organisation\"]").First.FillAsync("n/aUpdate");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

    }

    public async Task UpdateOrganisationCheck()
    {
      var organisation = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").First;
      await Task.Run(() => Assert.That(organisation.InputValueAsync().Result, Does.Contain("n/aUpdate")));
    }
    public async Task UpdateRole()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Roles\\.\\ Selected\\:\\ Level\\ 3\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Clerks, Court Associates, Ushers, Legal Advisors, JIT").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

    }

    public async Task UpdateRoleCheck()
    {
      await HooksInitializer._context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      var role = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#react-combobox-view-4");
      await Task.Run(() => Assert.IsTrue(role.IsVisibleAsync().Result));
      await role.ClickAsync();

      var superuser = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Super User functions");
      await Task.Run(() => Assert.IsFalse(superuser.IsVisibleAsync().Result));
    }

    public async Task UpdateAll()
    {

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ First\\ Name\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ First\\ Name\"]").First.FillAsync($"{createUserFirstName}Update");

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Last\\ Name\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Last\\ Name\"]").First.FillAsync($"{createUserLastName}Update");

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]").FillAsync($"{newUserEmail}Update");

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Phone\\ Number\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Phone\\ Number\"]").First.FillAsync("n/aUpdate");

      if (ManageUser.use == "super")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Organisation\\ Name\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Organisation\\ Name\"]").FillAsync("n/aUpdate");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Role\\.\\ Selected\\:\\ Super\\ User\"]").ClickAsync();
      }

      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Organisation\"]").First.ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Organisation\"]").First.FillAsync("n/aUpdate");
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Roles\\.\\ Selected\\:\\ Level\\ 3\"]").First.ClickAsync();
      }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Clerks, Court Associates, Ushers, Legal Advisors, JIT").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      HooksInitializer.contacts.RemoveAt(HooksInitializer.contacts.Count -1);
      HooksInitializer.contacts.Add($"{ManageUser.createUserFirstName}Update {ManageUser.createUserLastName}Update");

    }

    public async Task UpdateAllCheck()
    {
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
  
      var firstName = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").First;
      await Task.Run(() => Assert.That(firstName.InputValueAsync().Result, Does.Contain($"{createUserFirstName}Update")));

      var lastName = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").First;
      await Task.Run(() => Assert.That(lastName.InputValueAsync().Result, Does.Contain($"{createUserLastName}Update")));

      var email = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]");
      await Task.Run(() => Assert.That(email.InputValueAsync().Result, Does.Contain($"{newUserEmail}Update")));

      var phoneNumber = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").First;
      await Task.Run(() => Assert.That(phoneNumber.InputValueAsync().Result, Does.Contain("n/aUpdate")));

      if (ManageUser.use == "super")
      {
        var organisation = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Organisation\\ Name\"]").First;
        await Task.Run(() => Assert.That(organisation.InputValueAsync().Result, Does.Contain("n/aUpdate")));

        var role = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Role\\.\\ Selected\\:\\ Level\\ 2\"]");
        await Task.Run(() => Assert.IsTrue(role.IsVisibleAsync().Result));

        await role.ClickAsync();
        var superUser = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Super User functions");
        await Task.Run(() => Assert.IsTrue(superUser.IsVisibleAsync().Result));
      }
      else
      {
        var organisation = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").First;
        await Task.Run(() => Assert.That(organisation.InputValueAsync().Result, Does.Contain("n/aUpdate")));

        var role = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("#react-combobox-view-4");
        await Task.Run(() => Assert.IsTrue(role.IsVisibleAsync().Result));
      }
    }
    public async Task UpdateExistingEmail()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]").FillAsync($"{existingEmail}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task searchNoRecords()
    {
      if (ManageUser.use == "super")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search Users\"]").ClickAsync();
      }
      else
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search User\"]").ClickAsync();
      }
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync("Invalid Value");

    }
    public async Task searchNoRecordsMessage()
    {
      var noRecordMessage = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=There are no records matching your search criteria. Consider changing your searc");
     await noRecordMessage.WaitForAsync();
      await Task.Run(() => Assert.That(noRecordMessage.TextContentAsync().Result, Does.Contain("no records matching")));
    }
    public async Task adminCheck()
    { 
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      //await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Users\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search User\"]").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").WaitForAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{createUserLastName}");
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task checkValidation()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).FillAsync($"{ManageUsers.firstName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).FillAsync($"{ManageUsers.lastName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").FillAsync($"{ManageUsers.email}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).FillAsync($"{ManageUsers.phoneNumber}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).FillAsync($"{ManageUsers.organisation}");

      if (first)
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_1lj5p80").Nth(1).ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=External Access via Portal").ClickAsync();
      }
    }

    public async Task checkCharacterLimit()
    {
      if (use == "FN")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).FillAsync($"{ManageUsers.firstName}extra");

        var textFN = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1);
        await Task.Run(() => Assert.AreEqual(textFN.InputValueAsync().Result, $"{ManageUsers.firstName}"));
      }
      else if (use == "LN")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).FillAsync($"{ManageUsers.lastName}extra");

        var textLN = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1);
        await Task.Run(() => Assert.AreEqual(textLN.InputValueAsync().Result, $"{ManageUsers.lastName}"));
      }
      else if (use == "E")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").FillAsync($"{ManageUsers.email}extra");

        var textE = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]");
        await Task.Run(() => Assert.AreEqual(textE.InputValueAsync().Result, $"{ManageUsers.email}"));
      }
      else if (use == "P")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).FillAsync($"{ManageUsers.phoneNumber}extra");

        var textP = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1);
        await Task.Run(() => Assert.AreEqual(textP.InputValueAsync().Result, $"{ManageUsers.phoneNumber}"));
      }
      else if (use == "O")
      {
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).ClickAsync();
        await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).FillAsync($"{ManageUsers.organisation}extra");

        var textO = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1);
        await Task.Run(() => Assert.AreEqual(textO.InputValueAsync().Result, $"{ManageUsers.organisation}"));
      }
    }

    public async Task checkUserCreated()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search User\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{ManageUsers.firstName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
       
      var UserBox = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={ManageUsers.firstName} {ManageUsers.lastName}");
      await UserBox.WaitForAsync();
      await Task.Run(() => Assert.IsTrue(UserBox.IsVisibleAsync().Result));
      await Task.Run(() => UserBox.ClickAsync());

      var firstName = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ First\\ Name\"]").First;
      await Task.Run(() => Assert.That(firstName.InputValueAsync().Result, Does.Contain($"{ManageUsers.firstName}")));

      var lastName = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").First;
      await Task.Run(() => Assert.That(lastName.InputValueAsync().Result, Does.Contain($"{ManageUsers.lastName}")));

      var email = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Email\"]");
      await Task.Run(() => Assert.That(email.InputValueAsync().Result, Does.Contain($"{ManageUsers.email}")));

      var phoneNumber = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").First;
     await Task.Run(() => Assert.That(phoneNumber.InputValueAsync().Result, Does.Contain($"{ManageUsers.phoneNumber}")));

      var organisation = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").First;
      await Task.Run(() => Assert.That(organisation.InputValueAsync().Result, Does.Contain($"{ManageUsers.organisation}")));

      var role = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Roles\\. Selected\\: Level 3\"]").First;
       await Task.Run(() => Assert.IsTrue(role.IsVisibleAsync().Result));
    }
    public async Task gotoSuperUser()
    {
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Super User\")").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Users\"]").First.ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search Users\"]").ClickAsync();
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();

      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{ManageUser.createUserLastName}");
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      var UserBox = HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={ManageUser.createUserFirstName} {ManageUser.createUserLastName}");
      await Task.Run(() => Assert.IsTrue(UserBox.IsVisibleAsync().Result));
      await Task.Run(() => UserBox.ClickAsync());
    }
  }
}
