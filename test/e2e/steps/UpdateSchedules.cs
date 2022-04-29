using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;


namespace pre.test
    {
    [Binding]
    public class UpdateSchedules
    {

        
        public static UpdateSchedule _updateschedule;
        public static PageSetters _pagesetters;
        public UpdateSchedules(PageSetters pageSetters)
        {
            _pagesetters = pageSetters;
            _updateschedule = new UpdateSchedule(_pagesetters.Page);
        }
        public static string use = "";

[Given(@"I have selected a particular scheduled recording")]
public async Task GivenIhaveselectedaparticularscheduledrecording()
{
    use = "C";
    await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updateschedule.NavigateToBookings();
	await _updateschedule.Bookschedule();
    await _updateschedule.Schedule();
    await _updateschedule.FindSchedule();
}

[When(@"I change the Child Witness Indicator and save")]
public async Task WhenIchangetheChildWitnessIndicatorandsave()
{
	await _updateschedule.Updateaschedule();
    await _updateschedule.Updatechildwitness();
}

[Then(@"the schedule will show the updated Child Witness Indicator")]
public async Task ThentheschedulewillshowtheupdatedChildWitnessIndicator()
{
	await _updateschedule.CheckUpdatedschedule();
    await _updateschedule.CheckUpdatechildwitness();
}

[Given(@"I have a particular scheduled recording")]
public async Task GivenIhaveaparticularscheduledrecording()
{
    use = "D";
	await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updateschedule.NavigateToBookings();
	await _updateschedule.Bookschedule();
    await _updateschedule.Schedule();
    await _updateschedule.FindSchedule();
}

[When(@"I make a change to the Date and save")]
public async Task WhenImakeachangetotheDateandsave()
{
    
	await _updateschedule.Updateaschedule();
}

[Then(@"the scheduled recording will show the updated Date")]
public async Task ThenthescheduledrecordingwillshowtheupdatedDate()
{
	await _updateschedule.CheckUpdatedschedule();
    await _updateschedule.CheckUpdateinbookandviewrecordings();
    
}


[Given(@"I have selected a scheduled recording")]
public async Task GivenIhaveselectedascheduledrecording()
{
    use = "E";
	await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updateschedule.NavigateToBookings();
	await _updateschedule.Bookschedule();
    await _updateschedule.Schedule();
    await _updateschedule.FindSchedule();
}


[When(@"I add an additional defendant and save")]
public async Task WhenIaddanadditionaldefendantandsave()
{
	await _updateschedule.Updateaschedule();
}


[Then(@"the scheduled recording will show the additional defendant")]
public async Task Thenthescheduledrecordingwillshowtheadditionaldefendant()
{
	await _updateschedule.CheckUpdatedschedule();
    await _updateschedule.CheckUpdateinbookandviewrecordings();
}


[Given(@"I have a scheduled recording")]
public async Task GivenIhaveascheduledrecording()
{
	use = "W";
	await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updateschedule.NavigateToBookings();
	await _updateschedule.Bookschedule();
    await _updateschedule.Schedule();
    await _updateschedule.FindSchedule();
}


[When(@"I change the witness and save the updated record")]
public async Task WhenIchangethewitnessandsavetheupdatedrecord()
{
	await _updateschedule.Updateaschedule();
}

[Then(@"the scheduled recording will show the new witness")]
public async Task Thenthescheduledrecordingwillshowthenewwitness()
{
	await _updateschedule.CheckUpdatedschedule();
    await _updateschedule.CheckUpdateinbookandviewrecordings();
}


[Given(@"I have chosen a particular scheduled recording")]
public async Task GivenIhaveschosenaparticularscheduledrecording()
{
	use = "DE";
	await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updateschedule.NavigateToBookings();
	await _updateschedule.Bookschedule();
    await _updateschedule.Schedule();
    await _updateschedule.FindSchedule();


    }

    
[When(@"I remove a defendant and save the updated record")]
public async Task WhenIremoveadefendantandsavetheupdatedrecord()
{
    
	await _updateschedule.Updateaschedule();
}

[Then(@"the scheduled recording will not show that defendant")]
public async Task Thenthescheduledrecordingwillnotshowthatdefendant()
{
	await _updateschedule.CheckUpdatedschedule();
    await _updateschedule.CheckUpdateinbookandviewrecordings();
}

[Given(@"I have a particular schedule")]
public async Task GivenIhaveaparticularschedule()
{
	use = "O";
	await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updateschedule.NavigateToBookings();
	await _updateschedule.Bookschedule();
    await _updateschedule.Schedule();
    await _updateschedule.FindSchedule();
}

[When(@"I change the court and save")]
public async Task WhenIchangethecourtandsave()
{
	await _updateschedule.Updateaschedule();
}


[Then(@"the schedule will show the updated court")]
public async Task Thentheschedulewillshowtheupdatedcourt()
{
	await _updateschedule.CheckUpdatedschedule();
    await _updateschedule.CheckUpdateinbookandviewrecordings();
}


[Given(@"I have a recording scheduled")]
public async Task GivenIhavearecordingscheduled()
{
	use = "V";
	await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updateschedule.NavigateToBookings();
	await _updateschedule.Bookschedule();
    await _updateschedule.Schedule();
    await _updateschedule.FindSchedule();
}

[When(@"I click Amend and then click the cross button")]
public async Task WhenIclickAmendandthenclickthecrossbutton()
{
	await _updateschedule.Updateaschedule();
}

[Then(@"the view is closed")]
public async Task Thentheviewisclosed()
{
	await _updateschedule.CheckUpdatedschedule();
}


[Given(@"I have scheduled a recording")]
public async Task GivenIhavescheduledarecording()
{
	
	await _pagesetters.Page.GotoAsync(
        "https://apps.powerapps.com/play/abb08c46-bf74-4873-af2f-0871eed97ee9?tenantId=531ff96d-0ae9-462a-8d2d-bec7c0b42082");
    await _updateschedule.NavigateToBookings();
	await _updateschedule.Bookschedule();
    await _updateschedule.Schedule();
    await _updateschedule.FindSchedule();
}


[When(@"I change every field and save the updated record")]
public async Task WhenIchangeeveryfieldandsavetheupdatedrecord()
{
	await _updateschedule.Updateaschedule();
}

[Then(@"the scheduled recording will show the updated fields")]
public async Task Thenthescheduledrecordingwillshowtheupdatedfields()
{
    use = "A";
	await _updateschedule.CheckUpdatedschedule();
     await _updateschedule.CheckUpdateinbookandviewrecordings();
}

}





}

