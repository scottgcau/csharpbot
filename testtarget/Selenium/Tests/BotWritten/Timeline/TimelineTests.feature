###
# @bot-written
# 
# WARNING AND NOTICE
# Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
# Full Software Licence as accepted by you before being granted access to this source code and other materials,
# the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
# commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
# licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
# including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
# access, download, storage, and/or use of this source code.
# 
# BOT WARNING
# This file is bot-written.
# Any changes out side of "protected regions" will be lost next time the bot makes any changes.
###
@timelines @BotWritten
Feature: Timeline Test

Scenario: Visit the admin timeline page
	Given I login to the site as a user
	And I click on the Topbar Link
	And A sidebar option for timeline is visible
	And I click on Timelines Nav link on the Admin Nav section
	And I am on the timeline graph view
	Then I am able to toggle from graph to list view

Scenario: Visit admin timelines page from RosterEntity admin crud
	Given I login to the site as a user
	And I navigate to the RosterEntity backend page
	Then A RosterEntity has a view in timeline option
	And when I click the view in timeline option
	And I am on the timeline graph view
	Then I am able to toggle from graph to list view
 