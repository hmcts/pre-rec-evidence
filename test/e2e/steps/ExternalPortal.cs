using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;
using Microsoft.Extensions.Configuration;

namespace pre.test
{
  [Binding]
  public class ExternalPortals
  {
    public static string use = "";
    public static ExternalPortal _externalPortal;

    public static PageSetters _pagesetters;

    public ExternalPortals(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _externalPortal = new ExternalPortal(_pagesetters.Page);
    }

    [Given(@"I can view the shared recording and I click on it")]
    public async Task ViewSharedRecording()
    {
      await _externalPortal.ViewRecording();
    }


    [When(@"I go to the portal")]
    public async Task NavigateToPortal()
    {
      await _pagesetters.Page.GotoAsync($"{HooksInitializer.testPortalUrl}");
       var checkLogin = HooksInitializer._context.Page.Locator("text=Signin");
      var flag = await Task.Run(() => (checkLogin.IsVisibleAsync().Result));
      if (flag == true) { await HooksExternalPortal.PortalLogin(); }
      while(HooksInitializer._context.Page.Locator("text=Please note: Playback is preferred on non-mobile devices. If possible, please us").IsVisibleAsync().Result==false){}
    }

    [Given(@"there have been no recordings shared with me")]
    public async Task CheckSharedRecordings()
    {
      await _pagesetters.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await _externalPortal.CheckSharedRecordings();
    }

    [Then(@"a message should be displayed stating No recordings found")]
    public async Task NoRecordingsMessage()
    {
      await _pagesetters.Page.GotoAsync($"{HooksInitializer.testPortalUrl}");
      var checkLogin = _pagesetters.Page.Locator("text=Welcome to the Pre-recorded Evidence Portal‌‌...");
      var flag = await Task.Run(() => (checkLogin.IsVisibleAsync().Result));
      if (flag == false){await HooksExternalPortal.PortalLogin();}
      while(HooksInitializer._context.Page.Locator("text=Please note: Playback is preferred on non-mobile devices. If possible, please us").IsVisibleAsync().Result==false){}
      //await _externalPortal.NoRecordingsMessage(); Write method :)
    }

    [Given(@"I can see the witness names")]
    public async Task checkWit()
    {
      await _externalPortal.checkWitnesses();
    }

    [Then(@"I can see the updated witness name")]
    public async Task checkUpdatedWit()
    {
      await _externalPortal.checkUpdatedWitnesses();
    }

    [Then(@"I update these witness names")]
    public async Task updateWit()
    {
      await _pagesetters.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await _externalPortal.updateWitnesses();
    }


    [Given(@"I go to Manage cases")]
    public async Task WhenIgotoManagecases()
    {
      await _pagesetters.Page.GotoAsync($"{HooksInitializer.testUrl}");
      await _externalPortal.goToManageCases();
    }


    [Then(@"I can find the recording id")]
    public async Task ThenIcanfindtherecordingid()
    {
      await _externalPortal.findRecordingId();
    }


    [Given(@"I remove access for a participant to view a recording")]
    public async Task GivenIremoveaccessforaparticipanttoviewarecording()
    {
      await _externalPortal.removeAccess();
    }


    [Then(@"the participant is removed when I confirm")]
    public async Task ThentheparticipantisremovedwhenIconfirm()
    {
      await _externalPortal.checkRemoved();
    }


    [Then(@"I can view the recording uid")]
    public async Task ThenIcanviewtherecordinguid()
    {
      await _externalPortal.checkRecordingUID();
    }
  }
}
