
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class AdminManageRecordings
  {
    public static AdminManageRecording _adminManageRecordings;
    public static PageSetters _pagesetters;

    public AdminManageRecordings(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _adminManageRecordings = new AdminManageRecording(_pagesetters.Page);
    }

    [Given(@"I am in manage recordings in admin")]
    public async Task GivenIaminmanagerecordingsinadmin()
    {
      // using sandbox url until environments are aligned, update to test in future
      await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      await _adminManageRecordings.goToAdminManageRecordings();
    }


    [When(@"I change the date of a recording")]
    public async Task WhenIchangethedateofarecording()
    {
      await _adminManageRecordings.changeDate();
    }


    [Then(@"the date is changed")]
    public async Task Thenthedateischanged()
    {
      await _adminManageRecordings.checkDateChange();
    }


    [Then(@"I revert the date back")]
    public async Task ThenIrevertthedateback()
    {
      await _adminManageRecordings.revertDate();
    }



  }
}
