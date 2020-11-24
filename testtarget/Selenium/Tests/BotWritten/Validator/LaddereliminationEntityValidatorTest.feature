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
@BotWritten @validator
Feature: LaddereliminationEntity Validator

Scenario: Violate LaddereliminationEntity validators
Given I login to the site as a user
And I navigate to the LaddereliminationEntity create page
Then I verify the Required Validator for the PointsFor attribute for on-submit validation
Then I verify the Required Validator for the HomeAgainst attribute for on-submit validation
Then I verify the Required Validator for the HomeFor attribute for on-submit validation
Then I verify the Required Validator for the HomeLost attribute for on-submit validation
Then I verify the Required Validator for the HomeWon attribute for on-submit validation
Then I verify the Required Validator for the PointsAgainst attribute for on-submit validation
Then I verify the Required Validator for the Played attribute for on-submit validation
Then I verify the Required Validator for the Won attribute for on-submit validation
Then I verify the Required Validator for the Lost attribute for on-submit validation
