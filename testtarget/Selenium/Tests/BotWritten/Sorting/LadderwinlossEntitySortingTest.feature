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

Feature: Sort LadderwinlossEntity
	
	@LadderwinlossEntity
	Scenario: Sort LadderwinlossEntity
	Given I login to the site as a user
	And I navigate to the LadderwinlossEntity backend page
	When I sort LadderwinlossEntity by Played
	Then I assert that Played in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by Played
	Then I assert that Played in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by Won
	Then I assert that Won in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by Won
	Then I assert that Won in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by Lost
	Then I assert that Lost in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by Lost
	Then I assert that Lost in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by PointsFor
	Then I assert that PointsFor in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by PointsFor
	Then I assert that PointsFor in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by PointsAgainst
	Then I assert that PointsAgainst in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by PointsAgainst
	Then I assert that PointsAgainst in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by HomeWon
	Then I assert that HomeWon in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by HomeWon
	Then I assert that HomeWon in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by HomeLost
	Then I assert that HomeLost in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by HomeLost
	Then I assert that HomeLost in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by HomeFor
	Then I assert that HomeFor in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by HomeFor
	Then I assert that HomeFor in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by HomeAgainst
	Then I assert that HomeAgainst in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by HomeAgainst
	Then I assert that HomeAgainst in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by AwayWon
	Then I assert that AwayWon in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by AwayWon
	Then I assert that AwayWon in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by AwayLost
	Then I assert that AwayLost in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by AwayLost
	Then I assert that AwayLost in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by AwayFor
	Then I assert that AwayFor in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by AwayFor
	Then I assert that AwayFor in LadderwinlossEntity of type int is properly sorted in ascending
	When I sort LadderwinlossEntity by AwayAgainst
	Then I assert that AwayAgainst in LadderwinlossEntity of type int is properly sorted in descending
	When I sort LadderwinlossEntity by AwayAgainst
	Then I assert that AwayAgainst in LadderwinlossEntity of type int is properly sorted in ascending

