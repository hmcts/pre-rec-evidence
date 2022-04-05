using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class ViewRecordings
  {
    public static ViewRecording _viewrecording;
    public static PageSetters _pagesetters;

    public ViewRecordings(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _viewrecording = new ViewRecording(_pagesetters.Page);
    }

    [Given(@"I am watching a recording")]
    [Given(@"I am on the view recording page")]
    public async Task NavigateToViewRecordingScreen()
    {
      await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");

      await _viewrecording.NavigateToViewRecording();
    }

    [When(@"I search for a case reference")]
    public async Task FindCaseAndSearch()
    {
      await _viewrecording.FindCaseToView();
    }

    [Then(@"the recordings for that case reference will show")]
    public async Task VerifySearchResults()
    {
      await _viewrecording.CheckSearch();
    }

    [When(@"I turn off the timestamp")]
    public async Task SwitchOffTimestamp()
    {
      await _viewrecording.SwitchTimestamp();
      await _viewrecording.SwitchTimestamp();
    }

    [When(@"I turn on the timestamp")]
    public async Task SwitchOnTimestamp()
    {
      await _viewrecording.SwitchTimestamp();
    }

    [Then(@"the video will no longer show a timestamp")]
    public async Task VerifyTimestampOff()
    {
      await _viewrecording.CheckTimeStampOff();
    }

    [Then(@"the video will show a timestamp with hours, mins and seconds")]
    public async Task VerifyTimestampOn()
    {
      await _viewrecording.CheckTimeStampOn();
    }

  }
}
