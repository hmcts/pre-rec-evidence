using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
	{
	[Binding]
	public class BookRecordings
	{
		public static BookRecording _bookrecording;

		public static string use = "";


		public static PageSetters _pagesetters;
		public BookRecordings(PageSetters pageSetters)
		{
			_pagesetters = pageSetters;
			_bookrecording = new BookRecording(_pagesetters.Page);
		}


[Given(@"user on Book recording screen")]
public async Task NavigateToBookingScreen()
{
	await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
	await _bookrecording.NavigateToBooking();
}


[When(@"all fields entered and click save")]
public async Task Whenallfieldsenteredandclicksave()
{
	use = "Case";
	await _bookrecording.EnterCaseDetails();
}

[Then(@"case will be created")]
public async Task Thencasewillbecreated()
{
	await _bookrecording.CheckCaseCreated();
}



[Given(@"user on Schedule page")]
public async Task GivenuseronSchedulepage()

{
	await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
	await _bookrecording.NavigateToBooking();
}

[When(@"i fill required data for creating recording")]
public async Task Whenifillrequireddataforcreatingrecording()
{
  use = "Schedule";
  await _bookrecording.EnterCaseDetails();
  await _bookrecording.ScheduleRecording();
}

[Then(@"schedules will be created")]
public async Task Thenscheduleswillbecreated()
{
[Then(@"schedules will be created")]

public async Task Thenscheduleswillbecreated()

{

await _bookrecording.CheckCaseScheduled();

}
[Given(@"I need to enter a court name")]
public async Task GivenIneedtoenteracourtname()
{
    await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/ee7bf58e-99c9-4a34-b57d-7137307231af?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _bookrecording.NavigateToBooking();
    
}

[When(@"I select a court name")]
public async Task WhenIselectacourtname()
{
    await _bookrecording.SelectCourt();
}

[Then(@"I am presented only with MVP court names")]
public async Task ThenIampresentedonlywithMVPcourtnames()
{
    await _bookrecording.CheckCourt();
}




}

}
}
