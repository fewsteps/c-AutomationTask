@Converter
Feature: Converter

Scenario: Convert currency
    Given I open converter page
	When I convert <Amount> amount from <FromCurrency> to <ToCurrency>
	Then Conversion rates should be visible
	When I insert negative amount <NegativeAmount>
	Then Error message "<ErrorMessageForNegativeAmount>" should be displayed
	When I insert non numeric amount <NonNumericValue>
	Then Error message "<ErrorMessageForNonNumericAmount>" should be displayed

	Examples:
	| Amount | FromCurrency  | ToCurrency | NegativeAmount | ErrorMessageForNegativeAmount              | ErrorMessageForNonNumericAmount |  NonNumericValue |
	| 10     | CAD           | GBP        |		-10		   |   Please enter an amount greater than 0    |  Please enter a valid amount    |         val      |
	| 10.25  | CAD           | GBP        |		-10.25	   |   Please enter an amount greater than 0    |  Please enter a valid amount    |        !.+       |
