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

@BotWritten @Filter
Feature: User filtered by future's created and modifed and search Feature

@User
Scenario: User filtered by future's created and modifed and search
Given I login to the site as a user
Given I have 10 valid User entities
And I have 1 valid User entities with fixed string values BlaBla_StringToSearch_BlaBla
And I navigate to the User backend page
When I click the filter Button on the User page
Then The filter panel shows up with correct information
When I enter the string StringToSearch to search and click filter button
Then The string StringToSearch is in each row of the the collection content
When I enter the current date plus 7 days to filter created date and click apply filters
Then No row is within the appllied current date range filters