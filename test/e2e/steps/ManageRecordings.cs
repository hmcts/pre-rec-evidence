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

    [Then(@"the save button should be disabled")]
    public async Task Thenthesavebuttonshouldbedisabled()
    {
      await _manageRecording.checkSaveButtonDisabled();
    }
  }

}
