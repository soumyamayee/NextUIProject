Feature: Verify Women's Skincare Section

@Testcase1
Scenario: Verify Women's Skincare Products from Next Brand
	Given User on Next Home Page
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "brand" filter
	Then User Click on "Next" brand
	Then User verify only the "next" brand products displayed

@Testcase2
Scenario: Verify Women's Skincare Products from Clarins Brand
	Given User on Next Home Page
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "brand" filter
	Then User Click on "Clarins" brand
	Then User verify only the "Clarins" brand products displayed
@Testcase3
Scenario: Verify Women's Skincare Products Sort by Price Low to High filter
	Given User on Next Home Page
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "brand" filter
	Then User Click on "Clarins" brand
	And User select Sort "Price: High - Low" filter
	Then User verify only the "Clarins" brand products displayed
	