using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class ManageUsers
  {
    public static ManageUser _manageuser;
    public static PageSetters _pagesetters;

    public ManageUsers(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _manageuser = new ManageUser(_pagesetters.Page);
    }

    [Given(@"I try to add a new user with an existing email address")]
    public async Task WhenItrytoaddanewuserwithanexistingemailaddress()
    {
      await _manageuser.AddExistingEmail();
    }


    [Then(@"an error message is displayed stating the email address already exists in PRE")]
    public async Task ThenanerrormessageisdisplayedstatingtheemailaddressalreadyexistsinPRE()
    {
      await _manageuser.CheckEmailErrorMessage();
    }


    [Then(@"the record is not saved")]
    public async Task Thentherecordisnotsaved()
    {
      await _manageuser.CheckRecordIsntCreated();
    }


    [Given(@"I try to add a new user")]
    public async Task WhenItrytoaddanewuser()
    {
      await _manageuser.CreateUser();
    }


    [Then(@"the record is saved")]
    public async Task Thentherecordissaved()
    {
      await _manageuser.CheckRecordIsCreated();
    }





  }
}