using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;

namespace pre.test
{
  [Binding]
  public class Editings
  {
    public static Editing _editings;
    public static string use = "";
    public static PageSetters _pagesetters;

    public Editings(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _editings = new Editing(_pagesetters.Page);
    }

    [Given(@"I have a recorded video")]
    public async Task GivenIhavearecordedvideo()
    {
      await _editings.findRecordedVideo();
    }

    [When(@"I click on the edit icon")]
    public async Task WhenIclickontheediticon()
    {
      await _editings.clickEdit();
    }

    [Then(@"I can view the editing details")]
    public async Task ThenIcanviewtheeditingdetails()
    {
      await _editings.viewEditDetails();
    }

  }
}