using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class UpdateBookedRecordings
  {

    public static BookRecording _bookrecording;
    public static UpdateBookedRecording _updatebookedrecording;
    public static PageSetters _pagesetters;
    public UpdateBookedRecordings(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _updatebookedrecording = new UpdateBookedRecording(_pagesetters.Page);
    }

    public static string use = "";

    [Given(@"I am on the book recordings page and I want to find an existing case")]
    public async Task GivenIamonthebookrecordingspage()
    {
      await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9");
      await _updatebookedrecording.FindCaseToView();
    }


    [Given(@"I want to find an existing Cases and enter the case reference")]
    public async Task GivenIwanttofindanexistingCase()
    {
      await _updatebookedrecording.SearchCase();
    }

    [Then(@"the application will check if the case reference already exists")]
    public async Task Thentheapplicationwillcheckif()
    {
      await _updatebookedrecording.FindCase();
      //await _updatebookedrecording.UpdateCase();
    }

    [Given(@"I enter and save additional witnesses")]
    public async Task GivenIenterandsaveadditionalwitnesses()
    {
      use = "W";
      await _updatebookedrecording.UpdateCase();
    }

    [Given(@"I enter and save additional defendants")]
    public async Task GivenIenterandsaveadditionaldefendants()
    {
      use = "D";
      await _updatebookedrecording.UpdateCase();

    }

    [Given(@"I enter and save additional defendants and witnesses")]
    public async Task GivenIenterandsaveadditionaldefendantsandwitnesses()
    {
      await _updatebookedrecording.UpdateCase();

    }


    [Then(@"the case will be updated in manage recordings")]
    public async Task Thenthiswillbereflectedinmanagerecordings()
    {
      await _updatebookedrecording.checkManage();
    }

    [Then(@"the case will be updated in schedule recordings")]
    public async Task Thenthiswillbereflectedinschedulerecordings()
    {
      await _updatebookedrecording.CheckUpdatedCase();
    }


    [Given(@"I attempt to remove the witnesses from the case")]
    public async Task GivenIattempttoremovethewitnessesfromthecase()
    {
      use = "W";
      await _updatebookedrecording.removeWitDefNotScheduled();
    }

    [Then(@"an error message will appear Or the delete button is disabled")]
    public async Task ThenanerrormessagewillappearOrthedeletebuttonisdisabled()
    {
        await _updatebookedrecording.checkErrorMessage();
    }

    [Then(@"the case will be updated in book recordings")]
    public async Task Thenthecasewillbeupdatedinbookrecordings()
    {
      await _updatebookedrecording.checkRemovedWitDef();
    }


    [Then(@"the case will be updated in schedule recording")]
    public async Task Thenthecasewillbeupdatedinschedulerecording()
    {
      await _updatebookedrecording.checkScheduleRemovedWitDef();
    }


    [Then(@"the case will be updated in manage recording")]
    public async Task Thenthecasewillbeupdatedinmanagerecording()
    {
      await _updatebookedrecording.checkManageRemovedWitDef();
    }


    [Then(@"the case will be updated in manage cases")]
    public async Task Thenthecasewillbeupdatedinmanagecases()
    {
      await _updatebookedrecording.checkAdminRemovedWitDef();
    }
    

    
[Given(@"I attempt to remove the defendant from the case")]
public async Task GivenIattempttoremovethedefendantfromthecase()
{
	use = "D";
      await _updatebookedrecording.removeWitDefNotScheduled();
}

[Then(@"an error message will pop up Or the delete button is disabled")]
public async Task ThenanerrormessagewillpopupOrthedeletebuttonisdisabled()
{
	await _updatebookedrecording.checkErrorMessage();



  }

  
[Then(@"the case will be updated inside schedule recording")]
public async Task Thenthecasewillbeupdatedinsideschedulerecording()
{
	await _updatebookedrecording.checkScheduleRemovedWitDef();
}

[Then(@"the case will be updated inside book recordings")]
public async Task Thenthecasewillbeupdatedinsidebookrecordings()
{
	await _updatebookedrecording.checkRemovedWitDef();
}


[Then(@"the case will be updated inside manage recording")]
public async Task Thenthecasewillbeupdatedinsidemanagerecording()
{
	await _updatebookedrecording.checkManageRemovedWitDef();
}


[Then(@"the case will be updated inside manage cases")]
public async Task Thenthecasewillbeupdatedinsidemanagecases()
{
	await _updatebookedrecording.checkAdminRemovedWitDef();
}


[Given(@"I try to remove the witnesses from the case")]
public async Task GivenItrytoremovethewitnessesfromthecase()
{
  use ="W";
	await _updatebookedrecording.removeWitDefScheduled();

}

[Then(@"an error message will show Or the delete button is disabled")]
public async Task ThenanerrormessagewillshowOrthedeletebuttonisdisabled()
{
	await _updatebookedrecording.checkErrorMessagescheduledwitdef();
}


[Then(@"the case in schedule recording will be updated")]
public async Task Thenthecaseinschedulerecordingwillbeupdated()
{
	await _updatebookedrecording.checkScheduleRemovedscheduledWitDef();
}


[Then(@"the case in book recordings will be updated")]
public async Task Thenthecaseinbookrecordingswillbeupdated()
{
		await _updatebookedrecording.checkRemovedscheduledWitDef();
}


[Then(@"the case in manage recording will be updated")]
public async Task Thenthecaseinmanagerecordingwillbeupdated()
{
	await _updatebookedrecording.checkManageRemovedscheduledWitDef();
}


[Then(@"the case in manage cases will be updated")]
public async Task Thenthecaseinmanagecaseswillbeupdated()
{
	await _updatebookedrecording.checkAdminRemovedScheduledWitDef();
}


[Given(@"I try to remove the defendant from the case")]
public async Task GivenItrytoremovethedefendantfromthecase()
{
	use ="D";
	await _updatebookedrecording.removeWitDefScheduled();
}


[Then(@"an error message will show up Or the delete button is disabled")]
public async Task ThenanerrormessagewillshowupOrthedeletebuttonisdisabled()
{
   await _updatebookedrecording.checkErrorMessagescheduledwitdef();
}


[Then(@"the case will be updated within schedule recording")]
public async Task Thenthecasewillbeupdatedwithinschedulerecording()
{
	await _updatebookedrecording.checkScheduleRemovedscheduledWitDef();
}


[Then(@"the case will be updated within book recordings")]
public async Task Thenthecasewillbeupdatedwithinbookrecordings()
{
	await _updatebookedrecording.checkRemovedscheduledWitDef();
}


[Then(@"the case will be updated within manage recording")]
public async Task Thenthecasewillbeupdatedwithinmanagerecording()
{
	await _updatebookedrecording.checkManageRemovedscheduledWitDef();
}


[Then(@"the case will be updated within manage cases")]
public async Task Thenthecasewillbeupdatedwithinmanagecases()
{
	await _updatebookedrecording.checkAdminRemovedScheduledWitDef();
}


[Given(@"I try to remove the defendants and witnesses from the case")]
public async Task GivenItrytoremovethedefendantsandwitnessesfromthecase()
{
	await _updatebookedrecording.removeAllWitDef();
}


[Then(@"an error message will show up Or the Save button is disabled")]
public async Task ThenanerrormessagewillshowupOrtheSavebuttonisdisabled()
{
	await _updatebookedrecording.saveButtonDisabledCheck();
}

[Then(@"the case within book recordings will be updated")]
public async Task Thenthecasewithinbookrecordingswillbeupdated()
{
	await _updatebookedrecording.allBookRecordingCheck();
}


[Then(@"the case within manage recording will be updated")]
public async Task Thenthecasewithinmanagerecordingwillbeupdated()
{
	await _updatebookedrecording.checkManageRemovedscheduledWitDef();
}


[Then(@"the case within manage cases will be updated")]
public async Task Thenthecasewithinmanagecaseswillbeupdated()
{
	await _updatebookedrecording.checkAdminRemovedScheduledWitDef();
}


[Given(@"I try to remove the witness and defendant from the case")]
public async Task GivenItrytoremovethewitnessanddefendantfromthecase()
{
  use = "WD";
	await _updatebookedrecording.removeWitDefScheduled();
}


[Then(@"an error message will be presented Or the delete button is disabled")]
public async Task ThenanerrormessagewillbepresentedOrthedeletebuttonisdisabled()
{
	await _updatebookedrecording.checkErrorMessagescheduledwitdef();
}


[Then(@"the case inside schedule recording will be updated")]
public async Task Thenthecaseinsideschedulerecordingwillbeupdated()
{
		await _updatebookedrecording.checkScheduleRemovedscheduledWitDef();
}


[Then(@"the case inside book recordings will be updated")]
public async Task Thenthecaseinsidebookrecordingswillbeupdated()
{
		await _updatebookedrecording.checkRemovedscheduledWitDef();
}


[Then(@"the case inside manage recording will be updated")]
public async Task Thenthecaseinsidemanagerecordingwillbeupdated()
{
		await _updatebookedrecording.checkManageRemovedscheduledWitDef();
}


[Then(@"the case inside manage cases will be updated")]
public async Task Thenthecaseinsidemanagecaseswillbeupdated()
{
		await _updatebookedrecording.checkAdminRemovedScheduledWitDef();
}



[Given(@"I try to remove the non scheduled witness and defendant from the case")]
public async Task GivenItrytoremovethenonscheduledwitnessanddefendantfromthecase()
{
  use ="WD";
	await _updatebookedrecording.removeWitDefNotScheduled();
}


[Then(@"an error message appears Or the delete button is disabled")]
public async Task ThenanerrormessageappearsOrthedeletebuttonisdisabled()
{
	await _updatebookedrecording.checkErrorMessage();
}


[Then(@"the case on schedule recording will be updated")]
public async Task Thenthecaseonschedulerecordingwillbeupdated()
{
	await _updatebookedrecording.checkScheduleRemovedWitDef();
}


[Then(@"the case on book recordings will be updated")]
public async Task Thenthecaseonbookrecordingswillbeupdated()
{
	await _updatebookedrecording.checkRemovedWitDef();
}


[Then(@"the case on manage recording will be updated")]
public async Task Thenthecaseonmanagerecordingwillbeupdated()
{
	await _updatebookedrecording.checkManageRemovedWitDef();
}


[Then(@"the case on manage cases will be updated")]
public async Task Thenthecaseonmanagecaseswillbeupdated()
{
	await _updatebookedrecording.checkAdminRemovedWitDef();
}


[Given(@"I remove the text form witness and defendant fields and attempt to click save")]
public async Task GivenIremovethetextformwitnessanddefendantfieldsandattempttoclicksave()
{
  use = "T";
	await _updatebookedrecording.removeWitDefScheduled();
}


[Then(@"the save icon is disabled")]
public async Task Thenthesaveiconisdisabled()
{
	await _updatebookedrecording.checkSaveIconDisabled();
}


}
}