using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class AdminManageCases
  {
    public static AdminManageCase _manageCase;
    public static PageSetters _pagesetters;
    public static string use = "";
    public static string usePage = "";

    public AdminManageCases(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _manageCase = new AdminManageCase(_pagesetters.Page);
    }


    [Given(@"I update the court on a case")]
    public async Task WhenIupdatethecourtonacase()
    {
      await _manageCase.updateCourt();
    }


    [Then(@"the court will be updated in manage recordings")]
    public async Task Thenthecourtwillbeupdatedinmanagerecordings()
    {
      await _manageCase.checkCourtManageRecordings();
    }


    [Then(@"the court will be updated in view recordings")]
    public async Task Thenthecourtwillbeupdatedinviewrecordings()
    {
      await _manageCase.checkCourtViewRecordings();
    }


    [Then(@"the court will be updated in book recordings")]
    public async Task Thenthecourtwillbeupdatedinbookrecordings()
    {
      await _manageCase.checkCourtBookRecordings();
    }


    [Given(@"I update the scheduled date on a recording")]
    public async Task WhenIupdatethescheduleddateonarecording()
    {
      await _manageCase.updateDate();
    }


    [Then(@"the scheduled date will be updated in book recordings")]
    public async Task Thenthescheduleddatewillbeupdatedinbookrecordings()
    {
      await _manageCase.checkDateBookRecordings();
    }


    [Then(@"the scheduled date will be updated in manage recordings")]
    public async Task Thenthescheduleddatewillbeupdatedinmanagerecordings()
    {
      await _manageCase.checkDateManageRecordings();
    }


    [Then(@"the scheduled date will be updated in view recordings")]
    public async Task Thenthescheduleddatewillbeupdatedinviewrecordings()
    {
      await _manageCase.checkDateViewRecordings();
    }


    [Given(@"I have created a case to search for")]
    public async Task GivenIhavecreatedacase()
    {
      await _manageCase.checkCaseCreated();
    }


    [Then(@"I can search for it by case ref in manage cases")]
    public async Task ThenIcansearchforitbycaserefinmanagecases()
    {
      use = "caseRef";
      await _manageCase.search();
    }


    [Then(@"I can search for it by case id in manage cases")]
    public async Task ThenIcansearchforitbycaseidinmanagecases()
    {
      use = "caseId";
      await _manageCase.search();
    }

    [Then(@"I can search for it by the case id in manage cases")]
    public async Task ThenIcansearchforitbythecaseidinmanagecases()
    {
      usePage = "super";
      use = "caseId";
      await _manageCase.search();
    }


    [Then(@"I can search for it by court in manage cases")]
    public async Task ThenIcansearchforitbycourtinmanagecases()
    {
      use = "court";
      await _manageCase.search();
    }

    [When(@"I delete the case")]
    public async Task WhenIdeletethecase()
    {
      await _manageCase.deleteCase();
    }


    [Then(@"the case is no longer visible in book recordings")]
    public async Task Thenthecaseisnolongervisibleinbookrecordings()
    {
      await _manageCase.checkCaseDelete();
    }


    [When(@"I restore the case")]
    public async Task WhenIrestorethecase()
    {
      await _manageCase.restoreCase();
    }


    [Then(@"the case is visible in book recordings but not in schedule recordings")]
    public async Task Thenthecaseisvisibleinbookrecordingsbutnotinschedulerecordings()
    {
      await _manageCase.checkBookNotSchedule();
    }


    [When(@"I delete the schedule")]
    public async Task WhenIdeletetheschedule()
    {
      await _manageCase.deleteSchedule();
    }


    [Then(@"the schedule is no longer visible in manage recordings")]
    public async Task Thenthescheduleisnolongervisibleinmanagerecordings()
    {
      await _manageCase.ScheduleDeleteCheckManage();
    }


    [Then(@"I cannot remove the case reference in case details")]
    public async Task ThenIcannotremovethecasereferenceincasedetails()
    {
      await _manageCase.removeCaseRefCase();
    }


    [Then(@"I cannot remove the case reference in schedule details")]
    public async Task ThenIcannotremovethecasereferenceinscheduledetails()
    {
      use = "schedule";
      await _manageCase.removeCaseRefCase();
    }


    [When(@"I delete a case")]
    public async Task WhenIdeleteacase()
    {
      usePage = "super";
      await _manageCase.deleteCase();
    }


    [When(@"I restore a case")]
    public async Task WhenIrestoreacase()
    {
      usePage = "super";
      await _manageCase.restoreCase();
    }


    [Given(@"I update the case ref on a case")]
    public async Task GivenIupdatethecaserefonacase()
    {
      await _manageCase.updateWithDuplicateCaseRef();
    }

    [Then(@"an error message stating the case already exists will be displayed")]
    public async Task Thenanerrormessagestatingthecasealreadyexistswillbedisplayed()
    {
      await _manageCase.checkDuplicateErrorMessage();
    }


    [Given(@"I have a case with a recoring")]
    public async Task GivenIhaveacasewitharecoring()
    {
      await _pagesetters.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await _manageCase.goToAdmin();
    }


    [When(@"I delete the recording")]
    public async Task WhenIdeletetherecording()
    {
      await _manageCase.deleterecording();
    }


    [Then(@"the case is no longer visible in view recordings")]
    public async Task Thenthecaseisnolongervisibleinviewrecordings()
    {
      use = "deleted";
      await _manageCase.checkViewRecording();
    }

    [When(@"I restore the recording")]
    public async Task WhenIrestoretherecording()
    {
      use="recording";
      await _manageCase.goToAdmin();
    }


    [Then(@"the recording is visible in view recordings")]
    public async Task Thentherecordingisvisibleinviewrecordings()
    {
      await _manageCase.checkViewRecording();
    }
  }
}
