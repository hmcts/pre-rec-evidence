using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

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
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

      var UserBox = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={existingEmailFirstName} {existingEmailLastName}");
      await Task.Run(() => Assert.IsFalse(UserBox.IsVisibleAsync().Result));
    }
    public async Task DisabledSave()
    {
      var saveButton = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Add New User\")");
      await Task.Run(() => Assert.IsTrue(saveButton.IsDisabledAsync().Result));
    }

    public async Task UpdateFirstName()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ First\\ Name\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ First\\ Name\"]").First.FillAsync($"{createUserFirstName}Update");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
    }
    public async Task UpdateFirstNameCheck()
    {
      var firstName = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").First;
      await Task.Run(() => Assert.That(firstName.InputValueAsync().Result, Does.Contain($"{createUserFirstName}Update")));
    }

    public async Task UpdateLastName()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Last\\ Name\"]").First.ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Last\\ Name\"]").First.FillAsync($"{createUserLastName}Update");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

    }
    public async Task UpdateLastNameCheck()
    {
      var lastName = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").First;
      await Task.Run(() => Assert.That(lastName.InputValueAsync().Result, Does.Contain($"{createUserLastName}Update")));
    }
    public async Task UpdateEmail()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{createUserLastName}");
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]").FillAsync($"{newUserEmail}Update");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Save User\")").ClickAsync();
      await Page.WaitForResponseAsync(resp => resp.Url.Contains("https://browser.pipe.aria.microsoft.com/Collector/3.0"));

    }

    public async Task UpdateEmailCheck()
    {
      var email = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User\\ Email\"]");
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
      var phoneNumber = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").First;
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

      if(ManageUser.use == "super")
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
      await Task.Run(() => Assert.That(noRecordMessage.TextContentAsync().Result, Does.Contain("no records matching")));
    }
    public async Task adminCheck()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=HMCTS Logo").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage\\ Users\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Search User\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{createUserLastName}");
    }
  }
}

