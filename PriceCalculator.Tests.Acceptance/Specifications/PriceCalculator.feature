Feature: Price Calculator

Scenario: When I buy 1 bread, 1 butter and 1 milk and there is no discount
	Given I have added '1' 'Bread' to the basket
	And I have added '1' 'Butter' to the basket
	And I have added '1' 'Milk' to the basket
	When I calculate the total
	Then the total should be '2.95'

Scenario: When I buy 2 breads and 2 butters, I get 1 bread for half price
	Given I have added '2' 'Bread' to the basket
	And I have added '2' 'Butter' to the basket
	When I calculate the total
	Then the total should be '3.10'

Scenario: When I buy 4 milks, one of them is free
	Given I have added '4' 'Milk' to the basket
	When I calculate the total
	Then the total should be '3.45'

Scenario: When I buy 1 bread, 2 butters and 8 milks, the bread is half price and 2 milks are free
	Given I have added '1' 'Bread' to the basket
	And I have added '2' 'Butter' to the basket
	And I have added '8' 'Milk' to the basket
	When I calculate the total
	Then the total should be '9.00'