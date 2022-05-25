using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class UpdateSchedules
  {


    public static UpdateSchedule _updateschedule;
    public static PageSetters _pagesetters;
    public UpdateSchedules(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _updateschedule = new UpdateSchedule(_pagesetters.Page);
    }
    public static string use = "";

    [Given(@"I have selected a particular scheduled recording")]
    public async Task GivenIhaveselectedaparticularscheduledrecording()
    {
      use = "";
      await _updateschedule.Schedule();
      await _updateschedule.FindSchedule();
    }

    [When(@"I change the Child Witness Indicator and save")]
    public async Task WhenIchangetheChildWitnessIndicatorandsave()
    {

      await _updateschedule.Updatechildwitness();
    }

    [Then(@"the schedule will show the updated Child Witness Indicator")]
    public async Task ThentheschedulewillshowtheupdatedChildWitnessIndicator()
    {

      await _updateschedule.ManageRecordingsCheckUpdatechildwitness();
    }

    [Given(@"I have a particular scheduled recording")]
    public async Task GivenIhaveaparticularscheduledrecording()
    {
      use = "";
      await _updateschedule.Schedule();
      await _updateschedule.FindSchedule();
    }

    [When(@"I make a change to the Date and save")]
    public async Task WhenImakeachangetotheDateandsave()
    {

      await _updateschedule.Updatedate();
    }

    [Then(@"the scheduled recording will show the updated Date")]
    public async Task ThenthescheduledrecordingwillshowtheupdatedDate()
    {
      await _updateschedule.ManageRecordingsCheckUpdatedDate();
      await _updateschedule.FindupdatedCase();
      await _updateschedule.BookRecordingsCheckUpdatedDate();

    }


    [Given(@"I have selected a scheduled recording")]
    public async Task GivenIhaveselectedascheduledrecording()
    {
      use = "";
      await _updateschedule.Schedule();
      await _updateschedule.FindSchedule();
    }


    [When(@"I add an additional defendant and save")]
    public async Task WhenIaddanadditionaldefendantandsave()
    {
      await _updateschedule.UpdateDefendant();
    }


    [Then(@"the scheduled recording will show the additional defendant")]
    public async Task Thenthescheduledrecordingwillshowtheadditionaldefendant()
    {
      await _updateschedule.ManageRceordingsCheckUpdatedDefendant();
      await _updateschedule.FindupdatedCase();
      // await _updateschedule.BookRecordingsCheckUpdatedDefendant();
    }


    [Given(@"I have a scheduled recording")]
    public async Task GivenIhaveascheduledrecording()
    {
      use = "";
      await _updateschedule.Schedule();
      await _updateschedule.FindSchedule();
    }


    [When(@"I change the witness and save the updated record")]
    public async Task WhenIchangethewitnessandsavetheupdatedrecord()
    {
      await _updateschedule.UpdateWitness();
    }

    [Then(@"the scheduled recording will show the new witness")]
    public async Task Thenthescheduledrecordingwillshowthenewwitness()
    {
      await _updateschedule.ManageRecordingsCheckUpdatedWitness();
      await _updateschedule.FindupdatedCase();
      await _updateschedule.BookRecordingsCheckUpdatedWitness();
    }


    [Given(@"I have chosen a particular scheduled recording")]
    public async Task GivenIhaveschosenaparticularscheduledrecording()
    {
      use = "DE";
      await _updateschedule.Schedule();
      await _updateschedule.FindSchedule();


    }


    [When(@"I remove a defendant and save the updated record")]
    public async Task WhenIremoveadefendantandsavetheupdatedrecord()
    {

      await _updateschedule.RemoveDefendant();
    }

    [Then(@"the scheduled recording will not show that defendant")]
    public async Task Thenthescheduledrecordingwillnotshowthatdefendant()
    {
      await _updateschedule.ManageRecordingsCheckRemovedDefendant();
      await _updateschedule.FindupdatedCase();
      await _updateschedule.BookRecordingsCheckRemovedDefendant();
    }

    [Given(@"I have a particular schedule")]
    public async Task GivenIhaveaparticularschedule()
    {
      use = "O";

      await _updateschedule.Schedule();
      await _updateschedule.FindSchedule();
    }

    [When(@"I change the court and save")]
    public async Task WhenIchangethecourtandsave()
    {
      await _updateschedule.UpdateCourt();
    }


    [Then(@"the schedule will show the updated court")]
    public async Task Thentheschedulewillshowtheupdatedcourt()
    {
      await _updateschedule.ManageRecordingsCheckUpdatedCourt();
      await _updateschedule.FindupdatedCase();
      await _updateschedule.BookRecordingsCheckUpdatedCourt();
    }


    [Given(@"I have a recording scheduled")]
    public async Task GivenIhavearecordingscheduled()
    {
      use = "";
      await _updateschedule.Schedule();
      await _updateschedule.FindSchedule();
    }

    [When(@"I click Amend and then click the cross button")]
    public async Task WhenIclickAmendandthenclickthecrossbutton()
    {
      await _updateschedule.CloseAmendView();
    }

    [Then(@"the view is closed")]
    public async Task Thentheviewisclosed()
    {
      await _updateschedule.ManageRceordingsCheckCloseAmendView();
    }


    [Given(@"I have scheduled a recording")]
    public async Task GivenIhavescheduledarecording()
    {
      use = "A";

      await _updateschedule.Schedule();
      await _updateschedule.FindSchedule();
    }


    [When(@"I change every field and save the updated record")]
    public async Task WhenIchangeeveryfieldandsavetheupdatedrecord()
    {
      await _updateschedule.UpdateAllFields();
    }

    [Then(@"the scheduled recording will show the updated fields")]
    public async Task Thenthescheduledrecordingwillshowtheupdatedfields()
    {

      await _updateschedule.ManageRecordingsCheckAllUpdatedFields();
      await _updateschedule.FindupdatedCase();
      await _updateschedule.BookRecordingsCheckAllUpdatedFields();
    }


    [Then(@"a success message is shown")]
    public async Task Thenasuccessmessageisshown()
    {
      await _updateschedule.checkSuccessMessage();
    }


  }





}

