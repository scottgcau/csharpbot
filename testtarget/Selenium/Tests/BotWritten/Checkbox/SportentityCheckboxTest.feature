###
# @bot-written
# 
# WARNING AND NOTICE
# Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
# Full Software Licence as accepted by you before being granted access to this source code and other materials,
# the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
# commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
# licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
# including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
# access, download, storage, and/or use of this source code.
# 
# BOT WARNING
# This file is bot-written.
# Any changes out side of "protected regions" will be lost next time the bot makes any changes.
###
@BotWritten @pagination @ignore
Feature: Sportentity Checkbox
# WARNING: These Tests have been flagged as needing web fixes and are currently ignored

@Sportentity
Scenario: Select all Sportentity entities across pages
Given I login to the site as a user
And I navigate to the Sportentity backend page
And I have 30 valid Sportentity entities
When I select all entities on all pages
Then 10 entities on current page should be selected
When I click on the next page button and validate page content
Then 10 entities on current page should be selected


@Sportentity
Scenario: Select all Sportentity entities on current page
Given I login to the site as a user
And I navigate to the Sportentity backend page
And I have 30 valid Sportentity entities
When I select all entities on current page
Then 10 entities on current page should be selected
When I click on the next page button and validate page content
Then 0 entities on current page should be selected


@Sportentity
Scenario: Verify Sportentity entity selection is persistant across pages
Given I login to the site as a user
And I navigate to the Sportentity backend page
And I have 30 valid Sportentity entities
When I select all entities on current page
Then 10 entities on current page should be selected
When I click on the next page button and validate page content
Then 0 entities on current page should be selected
When I select all entities on current page
Then 10 entities on current page should be selected
When I click on the previous page button and validate page content
Then 10 entities on current page should be selected


@Sportentity
Scenario: Verify Sportentity entity checkboxes are persistant across pages
Given I login to the site as a user
And I navigate to the Sportentity backend page
And I have 30 valid Sportentity entities
When I select all entities on all pages
Then 10 entities on current page should be selected
When I click on the next page button and validate page content
Then 10 entities on current page should be selected
When I unselect 3 entities
Then 7 entities on current page should be selected
When I click on the previous page button and validate page content
Then 10 entities on current page should be selected
When I click on the next page button and validate page content
Then 7 entities on current page should be selected



@Sportentity
Scenario: Sportentity checkbox single page deselect persistance test
Given I login to the site as a user
And I navigate to the Sportentity backend page
And I have 30 valid Sportentity entities
When I select all entities on current page
Then 10 entities on current page should be selected
When I unselect 5 entities
Then 5 entities on current page should be selected
When I click on the next page button and validate page content
Then 0 entities on current page should be selected
When I click on the previous page button and validate page content
Then 5 entities on current page should be selected

