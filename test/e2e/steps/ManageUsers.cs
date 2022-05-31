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
      ManageUser.use ="";
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
      ManageUser.use ="";
    }


[Given(@"I need to update a users First Name")]
//[When(@"I make this change in PRE for the user")]
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
  //await _manageuser.CheckRecordIsCreated();
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
  
	await _manageuser.UpdateAll();
}


[Then(@"the PRE user record will be updated")]
public async Task ThenthePREuserrecordwillbeupdated()
{
	await _manageuser.UpdateAllCheck();
  ManageUser.use ="";
}


[Given(@"I update all fields")]
public async Task GivenIupdateallfields()
{
  ManageUser.use ="super";
	await _manageuser.UpdateAll();
}






}




  }
