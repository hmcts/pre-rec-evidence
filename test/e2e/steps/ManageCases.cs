using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class ManageCases
  {
    public static ManageCase _manageCase;
    public static PageSetters _pagesetters;

    public ManageCases(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _manageCase = new ManageCase(_pagesetters.Page);
    }


    [Given(@"I am on manage cases")]
    public async Task GivenIamonmanagecases()
    {
      // using sandbox url until environments are aligned, update to test in future
      await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/97f0b518-0111-4c1e-9bbf-4bca71b82b84");
      await _manageCase.goToManageCase();
    }


    [When(@"I update the court on a case")]
    public async Task WhenIupdatethecourtonacase()
    {
      await _manageCase.updateCourt();
    }


    [Then(@"the court will be updated in manage recordings")]
    public async Task Thenthecourtwillbeupdatedinmanagerecordings()
    {
      await _manageCase.checkCourtManageRecordings();
    }


    [Then(@"the court will be updated in view recordings")]
    public async Task Thenthecourtwillbeupdatedinviewrecordings()
    {
      await _manageCase.checkCourtViewRecordings();
    }


    [Then(@"I will update the court back to the original court")]
    public async Task ThenIwillupdatethecourtbacktotheoriginalcourt()
    {
      await _manageCase.revertCourt();
    }




  }
}