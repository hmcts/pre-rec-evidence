using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;
using System;


namespace pre.test
{
  [Binding]
  public class ManageUsers
  {
    public static ManageUser _manageuser;
    public static PageSetters _pagesetters;
    public static string date = DateTime.UtcNow.ToString("MMddss");
    public static string firstName = $"FAutomated-{date}";
    public static string lastName = $"LAutomated-{date}";
    public static string specialChars = "!#$%&'*+-/=?^_`{|}";
    public static string email = $"EAutomated-{date}-{specialChars}~@pretest.com";
    public static string phoneNumber = "+447000000001";
    public static string organisation = $"OAutomated-{date}";

    public ManageUsers(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _manageuser = new ManageUser(_pagesetters.Page);
    }
    public HooksManageUsers Hook = new HooksManageUsers();

    [Given(@"I try to add a new user with an existing email address")]
    public async Task WhenItrytoaddanewuserwithanexistingemailaddress()
    {
      ManageUser.use = "duplicateEmail";
      await Hook.CreateAUser();
    }

    [Then(@"an error message is displayed stating the email address already exists in PRE")]
    public async Task ThenanerrormessageisdisplayedstatingtheemailaddressalreadyexistsinPRE()
    {

      await _manageuser.CheckEmailErrorMessage();
      ManageUser.use = "";
    }

    [Then(@"the record is not saved")]
    public async Task Thentherecordisnotsaved()
    {
      await _manageuser.CheckRecordIsntCreated();
    }

    [Then(@"the record is saved")]
    public async Task Thentherecordissaved()
    {
      await Hook.CheckRecordIsCreated();
    }

    [Given(@"I try to add a new user with a blank email address")]
    public async Task GivenItrytoaddanewuserwithablankemailaddress()
    {
      ManageUser.use = "blankEmail";
      await Hook.CreateAUser();
    }

    [Then(@"the save button is disabled")]
    public async Task Thenthesavebuttonisdisabled()
    {
      await _manageuser.DisabledSave();
      ManageUser.use = "";
    }

    [Given(@"I need to update a users First Name")]
    public async Task WhenImakethischangeinPREfortheuser()
    {

      await _manageuser.UpdateFirstName();
    }

    [Then(@"the PRE user record will be updated with the new First Name")]
    public async Task ThenthePREuserrecordwillbeupdatedwiththenewFirstName()
    {
      await _manageuser.UpdateFirstNameCheck();
    }

    [Given(@"I update the users Last Name")]

    public async Task WhenIupdatetheusersLastName()
    {
      await _manageuser.UpdateLastName();
    }

    [Then(@"the PRE user record will be updated with the new Last Name")]
    public async Task ThenthePREuserrecordwillbeupdatedwiththenewLastName()
    {
      await _manageuser.UpdateLastNameCheck();
    }

    [When(@"I update the users Email")]
    public async Task WhenIupdatetheusersEmail()
    {
      await _manageuser.UpdateEmail();
    }

    [Then(@"the PRE user record will be updated with the new Email")]
    public async Task ThenthePREuserrecordwillbeupdatedwiththenewEmail()
    {
      await _manageuser.UpdateEmailCheck();
    }

    [Given(@"I update the users Phone No")]
    public async Task WhenIupdatetheusersPhoneNo()
    {

      await _manageuser.UpdatePhoneNo();
    }

    [Then(@"the PRE user record will be updated with the new Phone No")]
    public async Task ThenthePREuserrecordwillbeupdatedwiththenewPhoneNo()
    {
      await _manageuser.UpdatePhoneNoCheck();
    }

    [Given(@"I update the Oraganisation")]
    public async Task WhenIupdatetheOraganisation()
    {

      await _manageuser.UpdateOrganisation();
    }

    [Then(@"the PRE user record will be updated with the new Oraganisation")]
    public async Task ThenthePREuserrecordwillbeupdatedwiththenewOraganisation()
    {
      await _manageuser.UpdateOrganisationCheck();
    }

    [Given(@"I update the users Role")]
    public async Task WhenIupdatetheusersRole()
    {
      await _manageuser.UpdateRole();
    }

    [Then(@"the PRE user record will be updated with the new Role")]
    public async Task ThenthePREuserrecordwillbeupdatedwiththenewRole()
    {
      await _manageuser.UpdateRoleCheck();
    }

    [Given(@"I update every field")]
    public async Task WhenIupdateeveryfield()
    {
      ManageUser.use = "";
      await _manageuser.UpdateAll();
    }

    [Given(@"I want to search for a user and no records are returned")]
    public async Task GivenIwanttosearchforauserandnorecordsarereturned()
    {
      await _manageuser.searchNoRecords();
    }

    [Then(@"a message should be displayed stating that no records were found")]
    public async Task Thenamessageshouldbedisplayedstatingthatnorecordswerefound()
    {
      await _manageuser.searchNoRecordsMessage();
    }

    [Given(@"I search for a user and no records are returned")]
    public async Task GivenIsearchforauserandnorecordsarereturned()
    {
      ManageUser.use = "super";
      await _manageuser.searchNoRecords();
    }

    [Then(@"the PRE user record will be updated")]
    public async Task ThenthePREuserrecordwillbeupdated()
    {
      await _manageuser.UpdateAllCheck();
      ManageUser.use = "";
    }

    [Given(@"I update all fields")]
    public async Task GivenIupdateallfields()
    {
      ManageUser.use = "super";
      await _manageuser.UpdateAll();
    }

    [Then(@"the user will not be visible in Admin")]
    public async Task ThentheuserwillnotbevisibleinAdmin()
    {
      await _manageuser.adminCheck();
      await _manageuser.searchNoRecordsMessage();
    }

    [Given(@"I try to add a new user with an empty first name")]
    public async Task GivenItrytoaddanewuserwithanemptyfirstname()
    {
      ManageUser.first = true;
      firstName = "";
      await _manageuser.checkValidation();
      ManageUser.first = false;
    }

    [Given(@"I try to add a new user with an empty last name")]
    public async Task GivenItrytoaddanewuserwithanemptylastname()
    {
      ManageUser.first = true;
      lastName = "";
      await _manageuser.checkValidation();
      ManageUser.first = false;
    }

    [Given(@"I try to add a new user with an empty phone number")]
    public async Task GivenItrytoaddanewuserwithanemptyphonenumber()
    {
      ManageUser.first = true;
      phoneNumber = "";
      await _manageuser.checkValidation();
      ManageUser.first = false;
    }

    [Given(@"I try to add a new user with an empty email address")]
    public async Task GivenItrytoaddanewuserwithanemptyemailaddress()
    {
      ManageUser.first = true;
      email = "";
      await _manageuser.checkValidation();
      ManageUser.first = false;
    }

    [When(@"I change the first name to have (.*) characters")]
    public async Task WhenIchangethefirstnametohavemorethancharacters(int args1)
    {
      ManageUser.use = "FN";
      firstName = "firstnamefirstnamefirstna";
      await _manageuser.checkValidation();
    }

    [When(@"I change the last name to have (.*) characters")]
    public async Task WhenIchangethelastnametohavecharacters(int args1)
    {
      ManageUser.use = "LN";
      lastName = "lastnamelastnamelastnamel";
      await _manageuser.checkValidation();
    }

    [When(@"I change the email to have (.*) characters")]
    public async Task WhenIchangetheemailtohavecharacters(int args1)
    {
      ManageUser.use = "E";
      email = "EmailEmailEmailEmailEmailEmailEmailEmailEmailEmail";
      await _manageuser.checkValidation();
    }

    [When(@"I change the phone number to have (.*) characters")]
    public async Task WhenIchangethephonenumbertohavecharacters(int args1)
    {
      ManageUser.use = "P";
      phoneNumber = "Phonenumberph";
      await _manageuser.checkValidation();
    }

    [Given(@"I try to add a new user with an organisation of (.*) characters")]
    public async Task GivenItrytoaddanewuserwithanorganisationofcharacters(int args1)
    {
      ManageUser.first = true;
      ManageUser.use = "O";
      organisation = "OrganisationOrganisationOrganisationOrganisationOr";
      await _manageuser.checkValidation();
      ManageUser.first = false;
    }

    [Then(@"I cannot add any more characters")]
    public async Task ThenIcannotaddanymorecharacters()
    {
      await _manageuser.checkCharacterLimit();
    }

    [When(@"I change the first name to be alphanumeric, with a dash")]
    public async Task WhenIchangethefirstnametobealphanumericwithadash()
    {
      firstName = $"FAutomated-{date}";
      await _manageuser.checkValidation();
    }

    [When(@"I change the last name to be alphanumeric, with a dash")]
    public async Task WhenIchangethelastnametobealphanumericwithadash()
    {
      lastName = $"LAutomated-{date}";
      await _manageuser.checkValidation();
    }

    [When(@"I change the email to meet all validation criteria")]
    public async Task WhenIchangetheemailtomeetallvalidationcriteria()
    {
      email = $"EAutomated-{date}-{specialChars}~@pretest.com";
      await _manageuser.checkValidation();
    }

    [When(@"I change the phone number to be numeric with a plus sign")]
    public async Task WhenIchangethephonenumbertobenumericwithaplussign()
    {
      phoneNumber = "+447000000001";
      await _manageuser.checkValidation();
    }

    [When(@"I change the organisation to be alphanumeric")]
    public async Task WhenIchangetheorganisationtobealphanumeric()
    {
      organisation = $"OAutomated-{date}";
      await _manageuser.checkValidation();
    }

    [Then(@"the save button is enabled")]
    public async Task Thenthesavebuttonisenabled()
    {
      await _manageuser.enabledSave();
    }

    [Then(@"the user is created")]
    public async Task Thentheuseriscreated()
    {
      await _manageuser.checkUserCreated();
    }
  }
}
