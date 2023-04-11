using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V107.Cast;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using System;

namespace SeadCehicAutomationQATask.PageObjects;

/// <summary>
/// Home Page Object
/// </summary>
public class ConverterPage
{
    //The URL of the application to be opened in the browser
    private const string ConverterUrl = "https://www.xe.com/currencyconverter/";

    //The Selenium web driver to automate the browser
    private readonly IWebDriver _webDriver;

    private WebDriverWait wait;

    public ConverterPage(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
    }

    //Finding elements by ID
    private IWebElement ConvertButton => _webDriver.FindElement(By.XPath("//button[text()='Convert']"));
    private IWebElement AcceptButton => _webDriver.FindElement(By.XPath("//button[text()='Accept']"));
    private IWebElement AmountInput => _webDriver.FindElement(By.Id("amount"));
    private IWebElement FromCurrency => _webDriver.FindElement(By.Id("midmarketFromCurrency"));
    private IWebElement ToCurrency => _webDriver.FindElement(By.Id("midmarketToCurrency"));
    private IWebElement SwapButton => _webDriver.FindElement(By.XPath("//button[@aria-label='Swap currencies']"));
    private IWebElement OneUnitValue => _webDriver.FindElement(By.XPath("(//div[contains(@class, 'unit-rates___StyledDiv')]//p)[1]"));
    private IWebElement ResultOfConversion => _webDriver.FindElement(By.XPath("//p[contains(@class, 'result__BigRate')]"));

    private IWebElement CurrencyDropDownValue(string value) => _webDriver.FindElement(By.XPath($"//div[contains(text(), '{value}')]"));


    public void ConvertCurrency(string amount, string fromCurrency, string toCurrency)
    {
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[text()='Accept']")));
        AcceptButton.Click();

        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//button[text()='Accept']")));
        AmountInput.Clear();
        AmountInput.SendKeys(amount);
        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.v(By.Id("amount")));

        FromCurrency.Click();
        FromCurrency.SendKeys(fromCurrency);
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(CurrencyDropDownValue(fromCurrency)));
        CurrencyDropDownValue(fromCurrency).Click();

        ToCurrency.Click();
        ToCurrency.SendKeys(toCurrency);
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(CurrencyDropDownValue(toCurrency)));
        CurrencyDropDownValue(toCurrency).Click();

        ConvertButton.Click();
    }

    public void InsertValue(string amount)
    {
        AmountInput.Click();
        AmountInput.SendKeys(Keys.Control + "a" + Keys.Delete);
        AmountInput.SendKeys(amount);
    }

    public void ErrorMessageShouldBeDisplayed(string errorMessage)
    {
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//div[contains(text(), '{errorMessage}')]")));
    }
    public void ConversionRatedVisible()
    {
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(@class, 'result__ConvertedText')]")));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(@class, 'result__BigRate')]")));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'unit-rates___StyledDiv')]")));
    }

    public void EnsureHomePageIsOpen()
    {
        _webDriver.Navigate().GoToUrl(ConverterUrl);
        _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
    }

    public void SwapCurrency()
    {
        SwapButton.Click();
    }

    public void CurrenciesAreReplaced(string fromCurrency, string toCurrency)
    {
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//div[@id='midmarketFromCurrency-descriptiveText']//span[text()='{toCurrency}']")));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//div[@id='midmarketToCurrency-descriptiveText']//span[text()='{fromCurrency}']")));
    }

    public void CheckBroserUrl(string amount, string fromCurrency, string toCurrency)
    {
        string currentURL = _webDriver.Url;
        Assert.That(currentURL, Does.Contain("Amount=" + amount));
        Assert.That(currentURL, Does.Contain("From=" + fromCurrency));
        Assert.That(currentURL, Does.Contain("To=" + toCurrency));
    }

    public void CheckIfConversionIsCorrect(string amount)
    {
        var unitValues = OneUnitValue.Text.Split("= ");
        var unitValue = unitValues[1].Split(" ");
        var valueForCompare = decimal.Parse(unitValue[0]) * decimal.Parse(amount);
        valueForCompare = decimal.Round(valueForCompare, 5);
        var conversionResults = ResultOfConversion.Text.Split(" ");
        var conversionResult = decimal.Round(decimal.Parse(conversionResults[0]), 5);
        Assert.AreEqual(valueForCompare, conversionResult);
    }

    public void OpenConverterByUrl(string amount, string fromCurrency, string toCurrency)
    {
        var url = ConverterUrl + $"convert/?Amount={amount}&From={fromCurrency}&To={toCurrency}";
        _webDriver.Navigate().GoToUrl(url);
    }
}
