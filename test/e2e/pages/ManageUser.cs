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
    protected string existingEmail = config["portalEmail"];
    protected string existingEmailFirstName = "test";
    protected string existingEmailLastName = "existing email";
    protected string createUserFirstName = "Auto testUser";
    protected static string createUserLastName = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

    protected string newUserEmail = $"autotest{createUserLastName}";

    public ManageUser(IPage page) : base(page) { }

    public async Task GoToManageUsers()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Admin\")").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Users\"]").First.ClickAsync();
    }

    public async Task AddExistingEmail()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Users\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).FillAsync($"{existingEmailFirstName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).FillAsync($"{existingEmailLastName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").FillAsync($"{existingEmail}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).FillAsync("n/a");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).FillAsync("n/a");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_1lj5p80").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=External Access via Portal").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Add New User\")").ClickAsync();
    }

    public async Task CheckEmailErrorMessage()
    {
      var EmailError = ManageUsers._pagesetters.Page.Locator("text=Email already exists in system");
      while (await Task.Run(() => EmailError.IsVisibleAsync().Result == false))
      {
        await Task.Run(() => Assert.That(EmailError.InnerTextAsync().Result, Does.Contain("Email already exists in system")));
      }
    }

    public async Task CheckRecordIsntCreated()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{existingEmailFirstName} {existingEmailLastName}");
      var UserBox = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={existingEmailFirstName} {existingEmailLastName}");
      await Task.Run(() => Assert.IsFalse(UserBox.IsVisibleAsync().Result));
    }

    public async Task CreateUser()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Manage Users\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").Nth(1).FillAsync($"{createUserFirstName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").Nth(1).FillAsync($"{createUserLastName}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User Email\"]").FillAsync($"{newUserEmail}");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").Nth(1).FillAsync("n/a");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").Nth(1).FillAsync("n/a");
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("div.combobox-view-chevron.arrowContainer_1kmq8gc-o_O-container_r2h174-o_O-containerColors_1lj5p80").Nth(1).ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("text=Clerks, Court Associates, Ushers, Legal Advisors, JIT").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("button:has-text(\"Add New User\")").ClickAsync();
    }

    public async Task CheckRecordIsCreated()
    {
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Add User\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").ClickAsync();
      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[placeholder=\"Search - Name \\/ Email\"]").FillAsync($"{createUserLastName}");
      var UserBox = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator($"text={createUserFirstName} {createUserLastName}");
      await Task.Run(() => Assert.IsTrue(UserBox.IsVisibleAsync().Result));
      await Task.Run(() => UserBox.ClickAsync());

      var firstName = ManageUsers._pagesetters.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User First Name\"]").First;
      await Task.Run(() => Assert.That(firstName.InputValueAsync().Result, Does.Contain($"{createUserFirstName}")));

      var lastName = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Last Name\"]").First;
      await Task.Run(() => Assert.That(lastName.InputValueAsync().Result, Does.Contain($"{createUserLastName}")));

      var email = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Email\"]");
      await Task.Run(() => Assert.That(email.InputValueAsync().Result, Does.Contain($"{newUserEmail}")));

      var phoneNumber = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Phone Number\"]").First;
      await Task.Run(() => Assert.That(phoneNumber.InputValueAsync().Result, Does.Contain($"n/a")));

      var organisation = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Organisation\"]").First;
      await Task.Run(() => Assert.That(organisation.InputValueAsync().Result, Does.Contain($"n/a")));

      var role = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Roles\\. Selected\\: Level 2\"]");
      await Task.Run(() => Assert.IsTrue(role.IsVisibleAsync().Result));

      var status = Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"User Status\"]").Nth(1);
      await Task.Run(() => Assert.That(status.InputValueAsync().Result, Does.Contain($"Active")));

      await Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Lock\"]").ClickAsync();
    }
  }
}
