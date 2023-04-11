using SeadCehicAutomationQATask.Drivers;
using SeadCehicAutomationQATask.PageObjects;
using TechTalk.SpecFlow;
using BindingAttribute = TechTalk.SpecFlow.BindingAttribute;

namespace SeadCehicAutomationQATask.Steps;

[Binding]
public sealed class ConverterStepDefinitions
{
    //Page Object for Application
    private readonly ConverterPage _converterPageObject;
    private readonly ListPage _listPage;
    private readonly ItemDetailsPage _itemDetailsPage;


    public ConverterStepDefinitions(BrowserDriver browserDriver)
    {
        _converterPageObject = new ConverterPage(browserDriver.Current);
        _listPage = new ListPage(browserDriver.Current);
        _itemDetailsPage = new ItemDetailsPage(browserDriver.Current);
    }

    [Given("I open converter page")]
    public void OpenBrowser()
    {
        _converterPageObject.EnsureHomePageIsOpen();
    }
    [When(@"I convert (.*) amount from (.*) to (.*)")]
    public void SearchItemById(string amount, string fromCurrency, string toCurrency)
    {
        //delegate to Page Object
        _converterPageObject.ConvertCurrency(amount, fromCurrency, toCurrency);
    }

    [Then("Conversion rates should be visible")]
    public void ConversionRatesVisible()
    {
        //delegate to Page Object
        _converterPageObject.ConversionRatedVisible();
    }

    [When("I insert (negative|non numeric) amount (.*)")]
    public void InsertNegativeValue(string text,string amount)
    {
        //delegate to Page Object
        _converterPageObject.InsertValue(amount);
    }

    [Then(@"Error message ""(.*)"" should be displayed")]
    public void ErrorMessageShouldBeDisplayed(string errorMessage)
    {
        //delegate to Page Object
        _converterPageObject.ErrorMessageShouldBeDisplayed(errorMessage);
    }

    [When("I swap currency")]
    public void SwapCurrency()
    {
        //delegate to Page Object
        _converterPageObject.SwapCurrency();

    }

    [Then("Currencies (.*), (.*) are swapped")]
    public void CurrenciesAreReplaced(string fromCurrency, string toCurrency)
    {
        //delegate to Page Object
        _converterPageObject.CurrenciesAreReplaced(fromCurrency, toCurrency);
    }

    [Then("Check browser url contains amount (.*), from curency (.*), to currency (.*)")]
    public void CheckBroserUrl(string amount, string fromCurrency, string toCurrency)
    {
        //delegate to Page Object
        _converterPageObject.CheckBroserUrl(amount, fromCurrency, toCurrency);
    }

    [When("I open converter by url with amount (.*), from curency (.*), to currency (.*)")]
    public void OpenConverterByUrl(string amount, string fromCurrency, string toCurrency)
    {
        //delegate to Page Object
        _converterPageObject.OpenConverterByUrl(amount, fromCurrency, toCurrency);

    }

    [Then("I verify that conversion is correct for (.*) amount")]
    public void VerifyConversion(string amount)
    {
        //delegate to Page Object
        _converterPageObject.CheckIfConversionIsCorrect(amount);
    }
}
