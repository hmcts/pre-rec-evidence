
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

    [Then(@"the date is updated")]
    public async Task Thenthedateisupdated()
    {
      await _adminManageRecordings.checkDateChange();
    }


    [Given(@"I have a recording")]
    public async Task GivenIhavearecording()
    {
      await _adminManageRecordings.findRecording();
    }


    [Then(@"I can search for it in Manage Recording")]
    public async Task ThenIcansearchforitinManageRecording()
    {
      await _adminManageRecordings.search();
    }



    [Given(@"I delete a recording in manage recordings")]
    public async Task GivenIdeletearecordinginmanagerecordings()
    {
      await _adminManageRecordings.findRecording();
      await _adminManageRecordings.search();
      await _adminManageRecordings.delete();
    }

    [Then(@"I can see it in show deleted items")]
    public async Task ThenIcanseeitinshowdeleteditems()
    {
      await _adminManageRecordings.checkDelete();
    }

   [Then(@"I cannot see it in View recordings")]
    public async Task ThenIcannotseeitinViewrecordings()
    {
      await _adminManageRecordings.checkView();
    }

    [Then(@"I can search for it in Manage Recording using case ref")]
    public async Task ThenIcansearchforitinManageRecordingusingcaseref()
    {
      use = "caseref";
      await _adminManageRecordings.search();
    }

    [Given(@"I have a recording to search for using case ref")]
    public async Task GivenIhavearecordingtosearchforusingcaseref()
    {
      use = "F";
      await _adminManageRecordings.findRecording();
    }


  }
}
