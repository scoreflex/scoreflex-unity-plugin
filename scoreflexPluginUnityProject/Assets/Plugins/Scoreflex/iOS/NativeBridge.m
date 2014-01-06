#import <Scoreflex/Scoreflex.h>

NSString * stringOrNil(const char *source)
{
	NSString *result = source == NULL ? nil : [NSString stringWithCString:source encoding:NSUnicodeStringEncoding];
	return result;
}

const char * scoreflexUnityObjectName = "Scoreflex";

void scoreflexSetUnityObjectName(const char *_unityObjectName)
{
	NSString *unityObjectName = [NSString stringWithCString:_unityObjectName encoding:NSUnicodeStringEncoding];
	scoreflexUnityObjectName = [unityObjectName UTF8String];
}

void scoreflexInitialize(const char *_id, const char *_secret, int _sandbox)
{
	NSString *id = [NSString stringWithCString:_id encoding:NSUnicodeStringEncoding];
	NSString *secret = [NSString stringWithCString:_secret encoding:NSUnicodeStringEncoding];
	BOOL sandbox = _sandbox == 1 ? YES : NO;

	[Scoreflex setClientId:id secret:secret sandboxMode:sandbox];
}

void scoreflexGetPlayerId(char *buffer, int bufferLength)
{
	NSString *playerId = [Scoreflex getPlayerId];
	[playerId getCString:buffer maxLength:bufferLength encoding:NSUnicodeStringEncoding];
}

float scoreflexGetPlayingTime()
{
	NSNumber *playTime = [Scoreflex getPlayingTime];
	return [playTime floatValue];
}

// handleNotification?

// handleURL?

// isReachable?

// languageCode?

// location?

void scoreflexSetDeviceToken(const char *_deviceToken)
{
	NSString *deviceToken = [NSString stringWithCString:_deviceToken encoding:NSUnicodeStringEncoding];
	[Scoreflex setDeviceToken:deviceToken];
}

void scoreflexShowDeveloperGames(const char *_developerId)
{
	NSString *developerId = [NSString stringWithCString:_developerId encoding:NSUnicodeStringEncoding];
	
	[Scoreflex showDeveloperGames:developerId params:nil];
}

void scoreflexShowDeveloperProfile(const char *_developerId)
{
	NSString *developerId = [NSString stringWithCString:_developerId encoding:NSUnicodeStringEncoding];
	
	[Scoreflex showDeveloperProfile:developerId params:nil];
}

// showFullScreenView ?

void scoreflexShowGameDetails(const char *_gameId)
{
	NSString *gameId = [NSString stringWithCString:_gameId encoding:NSUnicodeStringEncoding];
	
	[Scoreflex showGameDetails:gameId params:nil];
}

void scoreflexShowGamePlayers(const char *_gameId)
{
	NSString *gameId = [NSString stringWithCString:_gameId encoding:NSUnicodeStringEncoding];
	
	[Scoreflex showGamePlayers:gameId params:nil];
}

void scoreflexShowLeaderboard(const char *_leaderboardId)
{
	NSString *leaderboardId = [NSString stringWithCString:_leaderboardId encoding:NSUnicodeStringEncoding];
	[Scoreflex showLeaderboard:leaderboardId params:nil];
}


void scoreflexShowLeaderboardOverview(const char *_leaderboardId)
{
	NSString *leaderboardId = [NSString stringWithCString:_leaderboardId encoding:NSUnicodeStringEncoding];
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

void scoreflexShowPlayerProfileEdit()
{
	[Scoreflex showPlayerProfileEdit:nil];
}

void scoreflexShowPlayerRating()
{
	[Scoreflex showPlayerRating:nil];
}

void scoreflexShowPlayerSettings()
{
	[Scoreflex showPlayerSettings:nil];
}

void scoreflexShowRanksPanel(const char *_leaderboardId, int _score)
{
	NSString *leaderboardId = [NSString stringWithCString:_leaderboardId encoding:NSUnicodeStringEncoding];
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
	NSString *challengeInstanceId = [NSString stringWithCString:_challengeInstanceId encoding:NSUnicodeStringEncoding];
	[Scoreflex submitTurn:challengeInstanceId params:nil
		handler:^(SXResponse *response , NSError *error) {
			UnitySendMessage(scoreflexUnityObjectName, "HandleSubmitTurn", "success");
			NSLog(@"SubmitTurn Returned");
		}
	];
}

void scoreflexSubmitScore(const char *_leaderboardId, int _score)
{
	NSString *leaderboardId = [NSString stringWithCString:_leaderboardId encoding:NSUnicodeStringEncoding];
	NSString *score = [NSString stringWithFormat:@"%d", _score];
	NSDictionary *params = [NSDictionary dictionaryWithObject:score forKey:@"score"];
	[Scoreflex submitScore:leaderboardId params:params
		handler:^(SXResponse *response , NSError *error) {
			UnitySendMessage(scoreflexUnityObjectName, "HandleSubmitScore", "success");
			NSLog(@"SubmitScore Returned");
		}
	];
}

void scoreflexSubmitScoreAndShowRanksPanel(const char *_leaderboardId, int _score)
{
	NSString *leaderboardId = [NSString stringWithCString:_leaderboardId encoding:NSUnicodeStringEncoding];
	NSString *score = [NSString stringWithFormat:@"%d", _score];
	NSDictionary *params = [NSDictionary dictionaryWithObject:score forKey:@"score"];
	[Scoreflex submitScoreAndShowRanksPanel:leaderboardId params:params gravity:SXGravityTop];
}

void scoreflexSubmitTurnAndShowChallengeDetail(const char *_challengeInstanceId)
{
	NSString *challengeInstanceId = [NSString stringWithCString:_challengeInstanceId encoding:NSUnicodeStringEncoding];
	
	[Scoreflex submitTurnAndShowChallengeDetail:challengeInstanceId params:nil];
}
