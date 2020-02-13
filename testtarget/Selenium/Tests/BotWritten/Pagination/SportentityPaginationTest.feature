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
Feature: Sportentity Pagination Feature
# WARNING: These Tests have been flagged as needing web fixes and are currently ignored

@Sportentity
Scenario: Exceed upper pagination limit on Sportentity
Given I login to the site as a user
And I navigate to the Sportentity backend page
When I click on the last page button and validate page content
When I click on the next page button and validate page content

@Sportentity
Scenario: Exceed lower pagination limit on Sportentity
Given I login to the site as a user
And I navigate to the Sportentity backend page
When I click on the first page button and validate page content
When I click on the previous page button and validate page content

@Sportentity
Scenario: Sportentity next page
Given I login to the site as a user
And I navigate to the Sportentity backend page
When I click on the next page button and validate page content
When I click on the next page button and validate page content

@Sportentity
Scenario: Sportentity previous page
Given I login to the site as a user
And I navigate to the Sportentity backend page
When I click on the last page button and validate page content
When I click on the previous page button and validate page content
When I click on the previous page button and validate page content