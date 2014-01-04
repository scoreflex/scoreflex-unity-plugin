#import <Scoreflex/Scoreflex.h>

NSString * stringOrNil(const char *source)
{
	NSString *result = source == NULL ? nil : [NSString stringWithCString:source encoding:NSUTF8StringEncoding];
	return result;
}

void scoreflexInitialize(const char *_id, const char *_secret, int _sandbox)
{
	NSString *id = [NSString stringWithCString:_id encoding:NSASCIIStringEncoding];
	NSString *secret = [NSString stringWithCString:_secret encoding:NSASCIIStringEncoding];
	BOOL sandbox = _sandbox == 1 ? YES : NO;

	[Scoreflex setClientId:id secret:secret sandboxMode:sandbox];
}

// getPlayerId — TODO

// getPlayingTime – TODO

// handleNotification?

// handleURL?

// isReachable?

// languageCode?

// location?

void scoreflexSetDeviceToken(const char *_deviceToken)
{
	NSString *deviceToken = [NSString stringWithCString:_deviceToken encoding:NSUTF8StringEncoding];
	
	[Scoreflex setDeviceToken:deviceToken];
}

void scoreflexShowDeveloperGames(const char *_developerId)
{
	NSString *developerId = [NSString stringWithCString:_developerId encoding:NSUTF8StringEncoding];
	
	[Scoreflex showDeveloperGames:developerId params:nil];
}

void scoreflexShowDeveloperProfile(const char *_developerId)
{
	NSString *developerId = [NSString stringWithCString:_developerId encoding:NSUTF8StringEncoding];
	
	[Scoreflex showDeveloperProfile:developerId params:nil];
}

// showFullScreenView ?

void scoreflexShowGameDetails(const char *_gameId)
{
	NSString *gameId = [NSString stringWithCString:_gameId encoding:NSUTF8StringEncoding];
	
	[Scoreflex showGameDetails:gameId params:nil];
}

void scoreflexShowGamePlayers(const char *_gameId)
{
	NSString *gameId = [NSString stringWithCString:_gameId encoding:NSUTF8StringEncoding];
	
	[Scoreflex showGamePlayers:gameId params:nil];
}

void scoreflexShowLeaderboard(const char *_leaderboardId)
{
	NSString *leaderboardId = [NSString stringWithCString:_leaderboardId encoding:NSUTF8StringEncoding];
	[Scoreflex showLeaderboard:leaderboardId params:nil];
}


void scoreflexShowLeaderboardOverview(const char *_leaderboardId)
{
	NSString *leaderboardId = [NSString stringWithCString:_leaderboardId encoding:NSUTF8StringEncoding];
	[Scoreflex showLeaderboardOverview:leaderboardId params:nil];
}

//showPanelView ?

void scoreflexShowPlayerChallenges()
{
	[Scoreflex showPlayerChallenges:nil];
}

void scoreflexShowPlayerFriends(const char *_playerId)
{
	NSString *playerId = stringOrNil(_playerId);
	[Scoreflex showPlayerFriends:playerId params:nil];
}

void scoreflexShowPlayerNewsFeed()
{
	[Scoreflex showPlayerNewsFeed:nil];
}

void scoreflexShowPlayerProfile(const char *_playerId)
{
	NSString *playerId = stringOrNil(_playerId);

	[Scoreflex showPlayerProfile:playerId params:nil];
}

void scoreflexPlayerProfileEdit()
{
	[Scoreflex showPlayerProfileEdit:nil];
}

void scoreflexPlayerRating()
{
	[Scoreflex showPlayerRating:nil];
}

void scoreflexShowPlayerSettings()
{
	[Scoreflex showPlayerSettings:nil];
}

void scoreflexShowRanksPanel(const char *_leaderboardId, int _score)
{
	NSString *leaderboardId = stringOrNil(_leaderboardId);
	NSString *score = [NSString stringWithFormat:@"%d", _score];
	NSDictionary *params = [NSDictionary dictionaryWithObject:score forKey:@"score"];
	[Scoreflex showRanksPanel:leaderboardId params:params gravity:SXGravityTop];
}

void scoreflexShowSearch()
{
	[Scoreflex showSearch:nil];
}

void scoreflexStartPlayingSession()
{
	[Scoreflex startPlayingSession];
}

void scoreflexStopPlayingSession()
{
	[Scoreflex stopPlayingSession];
}

void scoreflexSubmitTurn(const char *_challengeInstanceId)
{
	NSString *challengeInstanceId = [NSString stringWithCString:_challengeInstanceId encoding:NSUTF8StringEncoding];
	[Scoreflex submitTurn:challengeInstanceId params:nil
		handler:^(SXResponse *response , NSError *error) {
			UnitySendMessage("Scoreflex", "handleSubmitTurn", "");
			NSLog(@"SubmitTurn Returned");
		}
	];
}

void scoreflexSubmitScore(const char *_leaderboardId, int _score)
{
	NSString *leaderboardId = stringOrNil(_leaderboardId);
	NSString *score = [NSString stringWithFormat:@"%d", _score];
	NSDictionary *params = [NSDictionary dictionaryWithObject:score forKey:@"score"];
	[Scoreflex submitScore:leaderboardId params:params
		handler:^(SXResponse *response , NSError *error) {
			UnitySendMessage("Scoreflex", "handleSubmitScore", "");
			NSLog(@"SubmitScore Returned");
		}
	];
}

void scoreflexSubmitScoreAndShowRanksPanel(const char *_leaderboardId, int _score)
{
	NSString *leaderboardId = stringOrNil(_leaderboardId);
	NSString *score = [NSString stringWithFormat:@"%d", _score];
	NSDictionary *params = [NSDictionary dictionaryWithObject:score forKey:@"score"];
	[Scoreflex submitScoreAndShowRanksPanel:leaderboardId params:params gravity:SXGravityTop];
}

void scoreflexSubmitTurnAndShowChallengeDetail(const char *_challengeInstanceId)
{
	NSString *challengeInstanceId = [NSString stringWithCString:_challengeInstanceId encoding:NSUTF8StringEncoding];
	
	[Scoreflex submitTurnAndShowChallengeDetail:challengeInstanceId params:nil];
}
