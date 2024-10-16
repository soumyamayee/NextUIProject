Feature: Verify Women's Skincare Section

@Testcase1
Scenario: Verify Women's Skincare Products from Clinique Brand
	Given User on Next Home Page
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "brand" filter
	Then User Click on "Clinique" brand
	Then User verify only the "Clinique" brand products displayed

@Testcase2
Scenario: Verify Women's Skincare Products from Clarins Brand
	Given User on Next Home Page
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "brand" filter
	Then User Click on "Clarins" brand
	Then User verify only the "Clarins" brand products displayed
@Testcase3
Scenario: Verify Women's Skincare Products Sort by Price: High - Low filter
	Given User on Next Home Page
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "brand" filter
	Then User Click on "Clarins" brand
	And User select Sort "Price: High - Low" filter
	Then User verify the products are displayed from "High to low price" 
@Testcase4
Scenario: Verify Women's Skincare Products Sort by Price: Low - High filter
	Given User on Next Home Page
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "brand" filter
	Then User Click on "Clarins" brand
	And User select Sort "Price: Low - High" filter
	Then User verify the products are displayed from "Low to high price" 
	@Testcase5
Scenario: Verify Women's Skincare Products by selecting Price range
	Given User on Next Home Page
	Then User login to Next shopping site
	When User Navigated to the "Women" section
	Then User select "Skincare" category
	Then User Click on "More" filter
	Then User Click on "price" filter
	Then User Select the Price range from "£40-£100"
	Then User verify the products displayed are between the price range "£40-£100"
