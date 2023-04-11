@ConverterSwap
Feature: ConverterSwap

Scenario: Convert currency
    Given I open converter page
	When I convert <Amount> amount from <FromCurrency> to <ToCurrency>
	Then Conversion rates should be visible
	And Check browser url contains amount <Amount>, from curency <FromCurrency>, to currency <ToCurrency>
	When I swap currency
	Then Currencies <FromCurrency>, <ToCurrency> are swapped
	And Check browser url contains amount <Amount>, from curency <ToCurrency>, to currency <FromCurrency>
	When I open converter by url with amount <Amount>, from curency <UrlFromCurrency>, to currency <UrlToCurrency>
	Then Conversion rates should be visible
	And Check browser url contains amount <Amount>, from curency <UrlFromCurrency>, to currency <UrlToCurrency>
	Then I verify that conversion is correct for <Amount> amount 

	Examples:
	| Amount | FromCurrency  | ToCurrency |  UrlFromCurrency |  UrlToCurrency |
	| 10     | CAD           | GBP        |       AUD        |       USD      |
