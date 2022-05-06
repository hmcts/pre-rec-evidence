
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


    [Given(@"I change the date of a recording")]
    public async Task WhenIchangethedateofarecording()
    {
      await _adminManageRecordings.changeDate();
    }


    [Then(@"the date is changed")]
    public async Task Thenthedateischanged()
    {
      await _adminManageRecordings.checkDateChange();
    }


  }
}
