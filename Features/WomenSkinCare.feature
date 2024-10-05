Feature: Verify Women's Skincare Section

@mytag
Scenario: Verify Women's Skincare Category on Next Website
	Given User on Next Home Page
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "brand" filter
	Then User Click on "Next" brand
	Then User verify only the "next" brand products displayed


	