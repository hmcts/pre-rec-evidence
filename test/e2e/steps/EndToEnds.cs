using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;
using System;

namespace pre.test
{
  [Binding]
  public class EndToEnds
  {
    public static EndToEnd _endToEnd;
    public static string use = "";
    public static PageSetters _pagesetters;

    public EndToEnds(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _endToEnd = new EndToEnd(_pagesetters.Page);
    }

    [Given(@"I have created a case and schedule")]
    public async Task GivenIhavecreatedacaseandschedule()
    {
      await _endToEnd.createCaseSched();
    }

    [Then(@"I can get a rtmps link from manage recordings")]
    public async Task ThenIcangetartmpslinkfrommanagerecordings()
    {
      await _endToEnd.getRtmps();
    }

    [Given(@"I copy this into cvp and start a recording")]
    public async Task GivenIcopythisintocvpandstartarecording()
    {
      await _endToEnd.startRecording();
    }

    [Then(@"I can livestream the recording")]
    public async Task ThenIcanlivestreamtherecording()
    {
      await _endToEnd.livestreamCheck();
    }

    [Given(@"i end the recording in cvp and finish in pre")]
    public async Task Giveniendtherecordingincvpandfinishinpre()
    {
      await _endToEnd.finishRecording();
    }

    [Then(@"the recording is moved into view recordings")]
    public async Task Thentherecordingismovedintoviewrecordings()
    {
      await _endToEnd.checkView();
    }

    [Then(@"I can view the recording in view recordings")]
    public async Task ThenIcanviewtherecordinginviewrecordings()
    {
      await _endToEnd.viewRecording();
    }

    [Given(@"I share this recording with an external user")]
    public async Task GivenIsharethisrecordingwithanexternaluser()
    {
      await _endToEnd.shareRecording();
    }

    [Then(@"the user can view the recording in the portal")]
    public async Task Thentheusercanviewtherecordingintheportal()
    {
      await _endToEnd.viewInPortal();
    }

    [Given(@"I un-share this recording with the external user")]
    public async Task GivenIunsharethisrecordingwiththeexternaluser()
    {
      await _endToEnd.unshareRecording();
    }

    [Then(@"the user can no longer view the recording in the portal")]
    public async Task Thentheusercannolongerviewtherecordingintheportal()
    {
      await _endToEnd.noViewPortal();
    }
  }
}