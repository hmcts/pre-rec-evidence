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

    [Given(@"I have created a new case and scheduled a recording")]
    public async Task GivenIhavecreatedanewcaseandscheduledarecording()
    {
      // using sandbox url until environments are aligned, update to test in future
      await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      await _manageRecording.createAndScheduleCase();
    }


    [When(@"I go to manage recordings and search for the case")]
    public async Task WhenIgotomanagerecordingsandsearchforthecase()
    {
      await _manageRecording.goToManageRecordings();
    }


    [Then(@"I cannot see a check stream button")]
    public async Task ThenIcannotseeacheckstreambutton()
    {
      await _manageRecording.checkStreamButton();
    }


  }

}
