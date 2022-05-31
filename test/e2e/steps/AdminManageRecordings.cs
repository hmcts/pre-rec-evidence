
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]

  public class AdminManageRecordings
  {
    public static string use = "";
    public static AdminManageRecording _adminManageRecordings;
    public static PageSetters _pagesetters;

    public AdminManageRecordings(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _adminManageRecordings = new AdminManageRecording(_pagesetters.Page);
    }


    [Given(@"I change the date of a recording")]
    public async Task WhenIchangethedateofarecording()
    {
      use = "normal";
      await _adminManageRecordings.changeDate();
    }


    [Then(@"the date is changed")]
    public async Task Thenthedateischanged()
    {
      await _adminManageRecordings.checkDateChange();
    }

    [Given(@"I change the date of a recording to the past")]
    public async Task GivenIchangethedateofarecordingtothepast()
    {
      use = "past";
      await _adminManageRecordings.changeDate();
    }

    [Then(@"an error message is displayedd")]
    public async Task Thenanerrormessageisdisplayed()
    {
      await _adminManageRecordings.pastDateError();
    }

    
    [Given(@"I do not make a change")]
    public async Task GivenIdonotmakeachange()
    {
	    await _adminManageRecordings.PageCheck();
    }
  
    [Then(@"the save button will be disabled")]
    public async Task Thenthesavebuttonwillbedisabled()
    {
      await _adminManageRecordings.CheckSaveButtonDisabled();
    }


    
[Given(@"I change the date")]
public async Task GivenIchangethedate()
{
  use = "supernormal";
	await _adminManageRecordings.superUserDateChange();
}


    
[Then(@"the date is updated")]
public async Task Thenthedateisupdated()
{
	await _adminManageRecordings.checkDateChange();
}




  }
}
