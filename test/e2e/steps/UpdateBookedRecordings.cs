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

    [Given(@"I add an extra defendant")]
    public async Task GivenIaddanextradefendant()
    {
      use = "D";
      await _updatebookedrecording.addMoreParticipants();
    }


    [Then(@"the defendant will be visible in book recordings")]
    public async Task Thenthedefendantwillbevisibleinbookrecordings()
    {
      await _updatebookedrecording.checkBookAdd();
    }


    [Then(@"the defendant will be visible in schedule recordings")]
    public async Task Thenthedefendantwillbevisibleinschedulerecordings()
    {
      await _updatebookedrecording.checkScheduleAdd();
    }

    [Then(@"the defendant will be visible in manage recordings")]
    public async Task Thenthedefendantwillbevisibleinmanagerecordings()
    {
      await _updatebookedrecording.checkManageAdd();
    }

    [Then(@"the defendant will be visible in admin, manage cases, recordings")]
    public async Task Thenthedefendantwillbevisibleinadminmanagecasesrecordings()
    {
      await _updatebookedrecording.checkAdminAdd();
    }

    [Given(@"I add an extra witness")]
    public async Task WhenIaddanextrawitness()
    {
      use = "W";
      await _updatebookedrecording.addMoreParticipants();
    }

    [Then(@"the witness will be visible in book recordings")]
    public async Task Thenthewitnesswillbevisibleinbookrecordings()
    {
      await _updatebookedrecording.checkBookAdd();
    }


    [Then(@"the witness will be visible in schedule recordings")]
    public async Task Thenthewitnesswillbevisibleinschedulerecordings()
    {
      await _updatebookedrecording.checkScheduleAdd();
    }

    [Then(@"the witness will be visible in manage recordings")]
    public async Task Thenthewitnesswillbevisibleinmanagerecordings()
    {
      await _updatebookedrecording.checkManageAdd();
    }

    [Then(@"the witness will be visible in admin, manage cases, recordings")]
    public async Task Thenthewitnesswillbevisibleinadminmanagecasesrecordings()
    {
      await _updatebookedrecording.checkAdminAdd();
    }

    [Given(@"I add an extra witness and defendant")]
    public async Task WhenIaddanextrawitnessanddefendant()
    {
      await _updatebookedrecording.addMoreParticipants();
    }

    [Then(@"the witness and defendant will be visible in book recordings")]
    public async Task Thenthewitnessanddefendantwillbevisibleinbookrecordings()
    {
      await _updatebookedrecording.checkBookAdd();
    }

    [Then(@"the witness and defendant will be visible in schedule recordings")]
    public async Task Thenthewitnessanddefendantwillbevisibleinschedulerecordings()
    {
      await _updatebookedrecording.checkScheduleAdd();
    }

    [Then(@"the witness and defendant will be visible in manage recordings")]
    public async Task Thenthewitnessanddefendantwillbevisibleinmanagerecordings()
    {
      await _updatebookedrecording.checkManageAdd();
    }

    [Then(@"the witness and defendant will be visible in admin, manage cases, recordings")]
    public async Task Thenthewitnessanddefendantwillbevisibleinadminmanagecasesrecordings()
    {
      await _updatebookedrecording.checkAdminAdd();
    }


    [Then(@"the case will be updated in admin, manage cases, recordings")]
    public async Task Thenthecasewillbeupdatedinadminmanagecasesrecordings()
    {
      await _updatebookedrecording.checkAdmin();
    }

  }
}