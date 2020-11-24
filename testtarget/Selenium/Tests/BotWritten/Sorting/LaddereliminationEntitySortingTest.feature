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
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort LaddereliminationEntity
	
	@LaddereliminationEntity
	Scenario: Sort LaddereliminationEntity
	Given I login to the site as a user
	And I navigate to the LaddereliminationEntity backend page
	When I sort LaddereliminationEntity by PointsFor
	Then I assert that PointsFor in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by PointsFor
	Then I assert that PointsFor in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by AwatWon
	Then I assert that AwatWon in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by AwatWon
	Then I assert that AwatWon in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by AwayLost
	Then I assert that AwayLost in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by AwayLost
	Then I assert that AwayLost in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by AwayFor
	Then I assert that AwayFor in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by AwayFor
	Then I assert that AwayFor in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by AwayAgainst
	Then I assert that AwayAgainst in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by AwayAgainst
	Then I assert that AwayAgainst in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by HomeAgainst
	Then I assert that HomeAgainst in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by HomeAgainst
	Then I assert that HomeAgainst in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by HomeFor
	Then I assert that HomeFor in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by HomeFor
	Then I assert that HomeFor in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by HomeLost
	Then I assert that HomeLost in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by HomeLost
	Then I assert that HomeLost in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by HomeWon
	Then I assert that HomeWon in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by HomeWon
	Then I assert that HomeWon in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by PointsAgainst
	Then I assert that PointsAgainst in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by PointsAgainst
	Then I assert that PointsAgainst in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by Played
	Then I assert that Played in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by Played
	Then I assert that Played in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by Won
	Then I assert that Won in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by Won
	Then I assert that Won in LaddereliminationEntity of type int is properly sorted in ascending
	When I sort LaddereliminationEntity by Lost
	Then I assert that Lost in LaddereliminationEntity of type int is properly sorted in descending
	When I sort LaddereliminationEntity by Lost
	Then I assert that Lost in LaddereliminationEntity of type int is properly sorted in ascending

