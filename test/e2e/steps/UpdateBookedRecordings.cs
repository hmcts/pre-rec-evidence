using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
{
  [Binding]
  public class UpdateBookedRecordings
  {

    public static BookRecording _bookrecording;
    public static UpdateBookedRecording _updatebookedrecording;
    public static PageSetters _pagesetters;
    public UpdateBookedRecordings(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _updatebookedrecording = new UpdateBookedRecording(_pagesetters.Page);
    }

    public static string use = "";

    [Given(@"I am on the book recordings page and I want to find an existing case")]
    public async Task GivenIamonthebookrecordingspage()
    {
      await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9");
      await _updatebookedrecording.FindCaseToView();
    }


    [Given(@"I want to find an existing Cases and enter the case reference")]
    public async Task GivenIwanttofindanexistingCase()
    {
      await _updatebookedrecording.SearchCase();
    }

    [Then(@"the application will check if the case reference already exists")]
    public async Task Thentheapplicationwillcheckif()
    {
      await _updatebookedrecording.FindCase();
      //await _updatebookedrecording.UpdateCase();
    }

    [Given(@"I enter and save additional witnesses")]
    public async Task GivenIenterandsaveadditionalwitnesses()
    {
      use = "W";
      await _updatebookedrecording.UpdateCase();
    }

    [Given(@"I enter and save additional defendants")]
    public async Task GivenIenterandsaveadditionaldefendants()
    {
      use = "D";
      await _updatebookedrecording.UpdateCase();

    }

    [Given(@"I enter and save additional defendants and witnesses")]
    public async Task GivenIenterandsaveadditionaldefendantsandwitnesses()
    {
      await _updatebookedrecording.UpdateCase();

    }


    [Then(@"the case will be updated in manage recordings")]
    public async Task Thenthiswillbereflectedinmanagerecordings()
    {
      await _updatebookedrecording.checkManage();
    }

    [Then(@"the case will be updated in schedule recordings")]
    public async Task Thenthiswillbereflectedinschedulerecordings()
    {
      await _updatebookedrecording.CheckUpdatedCase();
    }

  }
}