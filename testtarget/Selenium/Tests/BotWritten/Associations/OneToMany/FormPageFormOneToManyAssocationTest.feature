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
@BotWritten @associations
Feature: Reference from SportentityFormTile using Form Page to Sportentity using Form
	Scenario: Reference from SportentityFormTile using Form Page to Sportentity using Form
	Given I login to the site as a user
	And I navigate to the SportentityFormTile backend page
	And I create 3 SportentityFormTile's each associated with 1 Form using Form
	Then I validate each Sportentity has 3 SportentityFormTile associations using Form Page
	Then I validate each SportentityFormTile has 1 Sportentity associations using Form