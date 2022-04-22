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
	await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updatebookedrecording.FindCaseToView();
}


[When(@"I want to find an existing Cases and enter the case reference")]
public async Task WhenIwanttofindanexistingCase()
{
	await _updatebookedrecording.SearchCase();
}

[Then(@"the application will check if the case reference already exists")]
public async Task Thentheapplicationwillcheckif()
{
	await _updatebookedrecording.FindCase();
    //await _updatebookedrecording.UpdateCase();
}


[Given(@"I have created a case")]
public async Task GivenIhavecreatedacase()
{
    use = "W";
    await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
	await _updatebookedrecording.NavigateToBooking();
    await _updatebookedrecording.BookCase();
    await _updatebookedrecording.ScheduleUpdateRecording();
    await _updatebookedrecording.SearchCase();
    await _updatebookedrecording.FindCase();
}


[When(@"I enter and save additional witnesses")]
public async Task WhenIenterandsaveadditionalwitnesses()
{
	await _updatebookedrecording.UpdateCase();
}


[Then(@"the case will be updated with additional witnesses")]
public async Task Thenthecasewillbeupdatedwithadditionalwitnesses()
{
	await _updatebookedrecording.CheckUpdatedCase();
}

[Given(@"I have booked a case")]
public async Task GivenIhavebookedacase()
{
    use = "D";
	await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
	await _updatebookedrecording.NavigateToBooking();
    await _updatebookedrecording.BookCase();
    await _updatebookedrecording.ScheduleUpdateRecording();
    await _updatebookedrecording.SearchCase();
    await _updatebookedrecording.FindCase();
}

[When(@"I enter and save additional defendants")]
public async Task WhenIenterandsaveadditionaldefendants()
{
	await _updatebookedrecording.UpdateCase();

}

[Then(@"the case will be updated with additional defendants")]
public async Task Thenthecasewillbeupdatedwithadditionaldefendants()
{
	await _updatebookedrecording.CheckUpdatedCase();
}


[Given(@"I have added a case")]
public async Task GivenIhaveaddedacase()
{
    use = "WD";
	await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
	await _updatebookedrecording.NavigateToBooking();
    await _updatebookedrecording.BookCase();
    await _updatebookedrecording.ScheduleUpdateRecording();
    await _updatebookedrecording.SearchCase();
    await _updatebookedrecording.FindCase();
}


[When(@"I enter and save additional defendants and witnesses")]
public async Task WhenIenterandsaveadditionaldefendantsandwitnesses()
{
	await _updatebookedrecording.UpdateCase();

}

  
[Then(@"the case will be updated with additional defendants and witnesses")]
public async Task Thenthecasewillbeupdatedwithadditionaldefendantsandwitnesses()
{
	await _updatebookedrecording.CheckUpdatedCase();
}
      
    }
}