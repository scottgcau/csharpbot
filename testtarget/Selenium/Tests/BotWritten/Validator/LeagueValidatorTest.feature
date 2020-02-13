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
@BotWritten @validator
Feature: League Validator

Scenario: Violate League validators
Given I login to the site as a user
And I navigate to the League create page
Then I verify the Required Validator for the Id attribute for on-submit validation
Then I verify the Required Validator for the Name attribute for on-submit validation
Then I verify the Required Validator for the SportId attribute for on-submit validation
Then I verify the Required Validator for the SportId attribute for on-submit validation

Scenario: Violate League live validators
Given I login to the site as a user
And I navigate to the League create page
Then I verify the Required Validator for the SportId attribute for live validation
