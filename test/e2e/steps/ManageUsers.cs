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
      _manageuser.use = "duplicateEmail";
      await _manageuser.CreateUser();
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
      _manageuser.use = "newUser";
      await _manageuser.CreateUser();
    }


    [Then(@"the record is saved")]
    public async Task Thentherecordissaved()
    {
      await _manageuser.CheckRecordIsCreated();
    }


    [Given(@"I try to add a new user with a blank email address")]
    public async Task GivenItrytoaddanewuserwithablankemailaddress()
    {
      _manageuser.use = "blankEmail";
      await _manageuser.CreateUser();
    }


    [Then(@"the save button is disabled")]
    public async Task Thenthesavebuttonisdisabled()
    {
      await _manageuser.DisabledSave();
    }

    
[Given(@"I need to update a users First Name")]
public async Task GivenIneedtoupdateausersFirstName()
{
	await _manageuser.CheckRecordIsCreated();
}

[When(@"I make this change in PRE for the user")]
public async Task WhenImakethischangeinPREfortheuser()
{
  
	await _manageuser.UpdateFirstName();
}


[Then(@"the PRE user record will be updated with the new First Name")]
public async Task ThenthePREuserrecordwillbeupdatedwiththenewFirstName()
{
	await _manageuser.UpdateFirstNameCheck();
}


[When(@"I update the users Last Name")]
public async Task WhenIupdatetheusersLastName()
{
	await _manageuser.CheckRecordIsCreated();
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
  //await _manageuser.CheckRecordIsCreated();
	await _manageuser.UpdateEmail();
}

[Then(@"the PRE user record will be updated with the new Email")]
public async Task ThenthePREuserrecordwillbeupdatedwiththenewEmail()
{
	await _manageuser.UpdateEmailCheck();
}


[When(@"I update the users Phone No")]
public async Task WhenIupdatetheusersPhoneNo()
{
  await _manageuser.CheckRecordIsCreated();
	await _manageuser.UpdatePhoneNo();
}


[Then(@"the PRE user record will be updated with the new Phone No")]
public async Task ThenthePREuserrecordwillbeupdatedwiththenewPhoneNo()
{
	await _manageuser.UpdatePhoneNoCheck();
}


[When(@"I update the Oraganisation")]
public async Task WhenIupdatetheOraganisation()
{
  await _manageuser.CheckRecordIsCreated();
	await _manageuser.UpdateOrganisation();
}

[Then(@"the PRE user record will be updated with the new Oraganisation")]
public async Task ThenthePREuserrecordwillbeupdatedwiththenewOraganisation()
{
	await _manageuser.UpdateOrganisationCheck();
}


[When(@"I update the users Role")]
public async Task WhenIupdatetheusersRole()
{
  await _manageuser.CheckRecordIsCreated();
	await _manageuser.UpdateRole();
}

[Then(@"the PRE user record will be updated with the new Role")]
public async Task ThenthePREuserrecordwillbeupdatedwiththenewRole()
{
	await _manageuser.UpdateRoleCheck();
}


[When(@"I update every field")]
public async Task WhenIupdateeveryfield()
{
  await _manageuser.CheckRecordIsCreated();
	await _manageuser.UpdateAll();
}


[Then(@"the PRE user record will be updated")]
public async Task ThenthePREuserrecordwillbeupdated()
{
	await _manageuser.UpdateAllCheck();
}


[When(@"I update the email to an existng email")]
public async Task WhenIupdatetheemailtoanexistngemail()
{
  await _manageuser.CheckRecordIsCreated();
	await _manageuser.UpdateExistingEmail();
}


}




  }
