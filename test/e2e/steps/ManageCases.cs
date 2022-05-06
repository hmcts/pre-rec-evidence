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


    [Given(@"I update the court on a case")]
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


    [Then(@"the court will be updated in book recordings")]
    public async Task Thenthecourtwillbeupdatedinbookrecordings()
    {
      await _manageCase.checkCourtBookRecordings();
    }


    [Given(@"I update the scheduled date on a recording")]
    public async Task WhenIupdatethescheduleddateonarecording()
    {
      await _manageCase.updateDate();
    }


    [Then(@"the scheduled date will be updated in book recordings")]
    public async Task Thenthescheduleddatewillbeupdatedinbookrecordings()
    {
      await _manageCase.checkDateBookRecordings();
    }


    [Then(@"the scheduled date will be updated in manage recordings")]
    public async Task Thenthescheduleddatewillbeupdatedinmanagerecordings()
    {
      await _manageCase.checkDateManageRecordings();
    }


    [Then(@"the scheduled date will be updated in view recordings")]
    public async Task Thenthescheduleddatewillbeupdatedinviewrecordings()
    {
      await _manageCase.checkDateViewRecordings();
    }

  }
}