using NUnit.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.Helpers;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
	{
	[Binding]
	public class BookRecordings
	{
		readonly BookRecording _bookrecording;
		readonly PageSetters _pagesetters;
		public BookRecordings(PageSetters pageSetters)
		{
			_pagesetters = pageSetters;
			_bookrecording = new BookRecording(_pagesetters.Page);
		}

[Given(@"user on Book recording screen")]
public async Task NavigateToBookingScreen()
{
	await _pagesetters.Page.GotoAsync("https://apps.powerapps.com/play/35134b2c-3b8d-4d32-b1eb-d288c0206b6a?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
	await _bookrecording.NavigateToBooking();
}


[When(@"all fields entered and click save")]
public async Task Whenallfieldsenteredandclicksave()
{
	await _bookrecording.EnterCaseDetails();
}

[Then(@"case will be created")]
public async Task Thencasewillbecreated()
{
	//Assert.NotNull(await _bookrecording.CheckCaseCreated());
}


[Given(@"user on Schedule page")]
public void GivenuseronSchedulepage()
{
}

[When(@"i fill required data for creating recording")]
public void Whenifillrequireddataforcreatingrecording()
{

}

[Then(@"schedules will be created")]
public void Thenscheduleswillbecreated()
{
}



}

}
