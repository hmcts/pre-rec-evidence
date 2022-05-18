using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;
using Microsoft.Extensions.Configuration;


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

    [Given(@"I search for a case reference")]
    public async Task FindCaseAndSearch()
    {
      await _viewrecording.FindCaseToView();
    }

    [Then(@"the recordings for that case reference will show")]
    public async Task VerifySearchResults()
    {
      await _viewrecording.CheckSearch();
    }

    [Given(@"I turn off the timestamp")]
    public async Task SwitchOffTimestamp()
    {
      await _viewrecording.SwitchTimestamp();
      await _viewrecording.SwitchTimestamp();
    }

    [Given(@"I turn on the timestamp")]
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

    
[Then(@"I can see the version for the recording")]
public async Task ThenIcanseetheversionfortherecording()
{
	await _viewrecording.checkVersion();
}


  }
}
