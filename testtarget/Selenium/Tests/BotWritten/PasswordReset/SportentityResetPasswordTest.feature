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
@PasswordReset @BotWritten
Feature: Reset Sportentity account password

Scenario: Reset Sportentity account password
Given I navigate to the request reset password page
	And I complete the form requesting to reset my Sportentity account password
Then I expect to recieve an email with a link to reset my password
When I follow the link and complete the reset password form
Then I expect that my old password will not log me in
	And I expect my new password will log me in