using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class ManageRecordings
  {
    public static ManageRecording _manageRecording;
    public static PageSetters _pagesetters;

    public ManageRecordings(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _manageRecording = new ManageRecording(_pagesetters.Page);
    }


    [Given(@"there's no active stream")]
    public async Task Giventheresnoactivestream()
    {
      await _manageRecording.checkStream();
    }


    [Then(@"I cannot see a check stream button")]
    public async Task ThenIcannotseeacheckstreambutton()
    {
      await _manageRecording.checkStreamButton();
    }


    [Given(@"I update the schedule date to a past date")]
    public async Task GivenIupdatethescheduledatetoapastdate()
    {
      await _manageRecording.updateToPastDate();
    }


    [Then(@"an error message should come up to say the date cannot be in the past")]
    public async Task Thenanerrormessageshouldcomeuptosaythedatecannotbeinthepast()
    {
      await _manageRecording.pastDateErrorMessage();
    }

    [Given(@"I have removed the court")]
    public async Task GivenIhaveremovedthecourt()
    {
      await _manageRecording.RemoveCourt();
    }

    [Then(@"an error message should appear")]
    public async Task Thenthesavebuttonshouldbedisabled()
    {
      await _manageRecording.checkErrorMessageSaveButton();
    }

    [Given(@"I update a recording")]
    public async Task GivenIupdatearecording()
    {
      await _manageRecording.UpdateRecording();
    }

    [Then(@"the success message says recording updated")]
    public async Task Thenthesuccessmessagesaysrecordingupdated()
    {
      await _manageRecording.updaterecordingConfirmationcheck();
    }

    [Given(@"I've created a case")]
    public async Task GivenIvecreatedacase()
    {
      await _manageRecording.createdCase();
    }

    [Then(@"I can see the version number for the recording")]
    public async Task ThenIcanseetheversionnumberfortherecording()
    {
      await _manageRecording.checkVersionNumber();
    }

    [Given(@"I delete a recording")]
    public async Task GivenIdeletearecording()
    {
      await _manageRecording.DeleteRecording();
    }

    [Then(@"the success message says scheduled recording deleted")]
    public async Task Thenthesuccessmessagesaysscheduledrecordingdeleted()
    {
      await _manageRecording.DeleterecordingConfirmationcheck();
    }

    [Given(@"there's no recording")]
    public async Task Giventheresnorecording()
    {
      await HooksInitializer._context.Page.FrameLocator("iframe[name=\"fullscreen-app-host\"]").Locator("[aria-label=\"Close Amend Recording\"]").First.ClickAsync();
    }

    [Then(@"the amend, manage and record buttons should be visible with a no recording status")]
    public async Task Thentheamendmanageandrecordbuttonsshouldbevisiblewithanorecordingstatus()
    {
      await _manageRecording.noRecordingCheck();
    }

    [When(@"I start the recording")]
    public async Task WhenIstarttherecording()
    {
      await _manageRecording.startRecording();
    }

    [Then(@"the manage, check, view and finish buttons should be visible with a in recording mode status")]
    public async Task Thenthemanagecheckviewandfinishbuttonsshouldbevisiblewithainrecordingmodestatus()
    {
      await _manageRecording.recordingStartedCheck();
    }
  }
}
