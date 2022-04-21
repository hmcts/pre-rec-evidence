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

[Given(@"I am on the book recordings page and I want to find an existing case")]
public async Task GivenIamonthebookrecordingspage()
{
	await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    //await _bookrecording.NavigateToBooking();
}


// [When(@"I want to find an existing Cases and enter the case reference")]
// public async Task WhenIwanttofindanexistingCase()
// {
// 	await _updatebookedrecording.SearchCase();
// }

// [Then(@"the application will check if the case reference already exists")]
// public async Task Thentheapplicationwillcheckif()
// {
// 	await _updatebookedrecording.FindCase();
//     //await _updatebookedrecording.UpdateCase();
// }


// [Given(@"I have found an existing case")]
// public async Task GivenIhavefoundanexistingcase()
// {
// 	await _bookrecording.NavigateToBooking();
//     await _updatebookedrecording.SearchCase();
//     await _updatebookedrecording.FindCase();
// }


// [When(@"I enter and save additional witnesses")]
// public async Task WhenIenterandsaveadditionalwitnesses()
// {
// 	await _updatebookedrecording.UpdateCase();
// }


// [Then(@"the case will be updated with additional witnesses")]
// public async Task Thenthecasewillbeupdatedwithadditionalwitnesses()
// {
	
// }

        
    }
}