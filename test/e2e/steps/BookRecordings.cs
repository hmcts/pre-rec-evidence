using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;
using System;



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


    [Given(@"all fields entered and click save")]
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

    [Given(@"i fill required data for creating recording")]
    public async Task Whenifillrequireddataforcreatingrecording()
    {
      use = "Schedule";
      await _bookrecording.EnterCaseDetails();
      await _bookrecording.ScheduleRecording();
    }

    [Then(@"schedules will be created")]
    public async Task Thenscheduleswillbecreated()
    {
      await _bookrecording.CheckCaseScheduled();
    }

    [Given(@"I select a court name")]
    public async Task WhenIselectacourtname()
    {
      await _bookrecording.SelectCourt();
    }

    [Then(@"I am presented only with MVP court names")]
    public async Task ThenIampresentedonlywithMVPcourtnames()
    {
      await _bookrecording.CheckCourt();
    }


    [Given(@"I select a date in the past")]
    public async Task WhenIselectadateinthepast()
    {
      use = "PastDate";
      await _bookrecording.EnterCaseDetails();
      await _bookrecording.selectPastDate();
    }


    [Then(@"an error message is displayed")]
    public async Task Thenanerrormessageisdisplayed()
    {
      await _bookrecording.pastDateErrorMessage();
    }


    [Then(@"the recordings box is filled")]
    public async Task Thentherecordingsboxisfilled()
    {
      await _bookrecording.checkRecordingBox();
    }


    [Given(@"i fill required data for creating ten recordings")]
    public async Task Givenifillrequireddataforcreatingtenrecordings()
    {
      use = "ScheduleTen";
      var orginalMonth = DateTime.UtcNow.ToString("MMM");
      await _bookrecording.EnterCaseDetails();
      for (int i = 0; i < _bookrecording.quotaNum; i++)
      {
        _bookrecording.day = (DateTime.UtcNow.AddDays(+i)).ToString("ddd");

        if (orginalMonth != (DateTime.UtcNow.AddDays(+i)).ToString("MMM") && (DateTime.UtcNow.AddDays(+i)).ToString("dd") == "01")
        {
          _bookrecording.changeMonthCount = _bookrecording.changeMonthCount + 1;
        }
        _bookrecording.month = (DateTime.UtcNow.AddDays(+i)).ToString("MMM");
        _bookrecording.dateNum = (DateTime.UtcNow.AddDays(+i)).ToString("dd");
        _bookrecording.year = (DateTime.UtcNow.AddDays(+i)).ToString("yyyy");

        await _bookrecording.ScheduleRecording();
      }
    }


    [Then(@"i can start recordings for the ten schedules")]
    public async Task Thenicanstartrecordingsforalltenschedules()
    {
      await _bookrecording.startTenRecordings();
    }


    [Given(@"I create a case with blank values")]
    public async Task GivenIcreateacasewithblankvalues()
    {
      await _bookrecording.BlankValues();
    }



    // [Then(@"an error message is displayed about the blank values")]
    // public async Task Thenanerrormessageisdisplayedblank()
    // {
    // 	await _bookrecording.blankErrorMessage();
    // }


    [Given(@"I create a case with blank values in court")]
    public async Task GivenIcreateacasewithblankvaluesincourt()
    {
      _bookrecording.use = "blankCourt";
      await _bookrecording.BlankValues();
    }


    [Given(@"I create a case with blank values in case ref")]
    public async Task GivenIcreateacasewithblankvaluesincaseref()
    {
      _bookrecording.use = "blankCaseRef";
      await _bookrecording.BlankValues();
    }


    [Given(@"I create a case with blank values in witnesses")]
    public async Task GivenIcreateacasewithblankvaluesinwitnesses()
    {
      _bookrecording.use = "blankWitnesses";
      await _bookrecording.BlankValues();
    }


    [Given(@"I create a case with blank values in defendants")]
    public async Task GivenIcreateacasewithblankvaluesindefendants()
    {
      _bookrecording.use = "blankDefendants";
      await _bookrecording.BlankValues();
    }


    [When(@"I update the case with blank values")]
    public async Task WhenIupdatethecasewithblankvalues()
    {
      await _bookrecording.UpdateBlank();
    }


    [Given(@"I create a case with blank values in a list")]
    public async Task GivenIcreateacasewithblankvaluesinalist()
    {
      await _bookrecording.listBlankValues();
    }


    [Then(@"the case is created but the blank values are ignored")]
    public async Task Thenthecaseiscreatedbuttheblankvaluesareignored()
    {
      await _bookrecording.checkBlanksIgnored();
    }

    [When(@"I update a case with blank values they cannot be saved")]
    public async Task GivenIupdateacasewithblankvaluesinalist()
    {
      _bookrecording.use = "updateToBlankWitDef";
      await _bookrecording.UpdateBlank();
    }


    [Given(@"I create a case with a duplicate case ref")]
    public async Task GivenIcreateacasewithaduplicatecaseref()
    {
      await _bookrecording.EnterCaseDetails();
      use= "D";
      await _bookrecording.EnterCaseDetails();
    }

    [Then(@"an error message is displayed stating the case exists")]
    public async Task Thenanerrormessageisdisplayedstatingthecaseexists()
    {
      await _bookrecording.checkDuplicateErrorMessage();
    }
  }
}
