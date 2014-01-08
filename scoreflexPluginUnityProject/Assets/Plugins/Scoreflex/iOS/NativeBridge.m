#import <Scoreflex/Scoreflex.h>

NSString * fromUnichar(const unichar *source)
{
	size_t length = 0; while(source[length] != 0) length++;
	NSString *string = [NSString stringWithCharacters:source length:length];
	return string;
}

NSString * stringOrNil(const unichar *source)
{
	NSString *result = source == NULL ? nil : fromUnichar(source);
	return result;
}

NSString * scoreflexUnityObjectName = @"Scoreflex";

void scoreflexSetUnityObjectName(const unichar *_unityObjectName)
{
	scoreflexUnityObjectName = fromUnichar(_unityObjectName);
}

void scoreflexInitialize(const unichar *_id, const unichar *_secret, int _sandbox)
{
	NSString *identification = fromUnichar(_id);
	NSString *secret = fromUnichar(_secret);
	BOOL sandbox = _sandbox == 1 ? YES : NO;

	NSLog(@"Initializing:\nid = %@\nsecret = %@\n_sandbox = %d", identification, secret, _sandbox);

	[Scoreflex setClientId:identification secret:secret sandboxMode:sandbox];
}

void scoreflexGetPlayerId(void *buffer, int bufferLength)
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

void scoreflexSetDeviceToken(const unichar *_deviceToken)
{
	NSString *deviceToken = fromUnichar(_deviceToken);
	[Scoreflex setDeviceToken:deviceToken];
}

void scoreflexShowDeveloperGames(const unichar *_developerId)
{
	NSString *developerId = fromUnichar(_developerId);
	
	[Scoreflex showDeveloperGames:developerId params:nil];
}

void scoreflexShowDeveloperProfile(const unichar *_developerId)
{
	NSString *developerId = fromUnichar(_developerId);
	
	[Scoreflex showDeveloperProfile:developerId params:nil];
}

// showFullScreenView ?

void scoreflexShowGameDetails(const unichar *_gameId)
{
	NSString *gameId = fromUnichar(_gameId);
	
	[Scoreflex showGameDetails:gameId params:nil];
}

void scoreflexShowGamePlayers(const unichar *_gameId)
{
	NSString *gameId = fromUnichar(_gameId);
	
	[Scoreflex showGamePlayers:gameId params:nil];
}

void scoreflexShowLeaderboard(const unichar *_leaderboardId)
{
	NSString *leaderboardId = fromUnichar(_leaderboardId);
	[Scoreflex showLeaderboard:leaderboardId params:nil];
}


void scoreflexShowLeaderboardOverview(const unichar *_leaderboardId)
{
	NSString *leaderboardId = fromUnichar(_leaderboardId);
	[Scoreflex showLeaderboardOverview:leaderboardId params:nil];
}

//showPanelView ?

void scoreflexShowPlayerChallenges()
{
	[Scoreflex showPlayerChallenges:nil];
}

void scoreflexShowPlayerFriends(const unichar *_playerId)
{
	NSString *playerId = stringOrNil(_playerId);
	[Scoreflex showPlayerFriends:playerId params:nil];
}

void scoreflexShowPlayerNewsFeed()
{
	[Scoreflex showPlayerNewsFeed:nil];
}

void scoreflexShowPlayerProfile(const unichar *_playerId)
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

void scoreflexShowRanksPanel(const unichar *_leaderboardId, int _score)
{
	NSString *leaderboardId = fromUnichar(_leaderboardId);
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

void scoreflexSubmitTurn(const unichar *_challengeInstanceId)
{
	NSString *challengeInstanceId = fromUnichar(_challengeInstanceId);
	[Scoreflex submitTurn:challengeInstanceId params:nil
		handler:^(SXResponse *response , NSError *error) {
			NSString *message = error == nil ? [NSString stringWithFormat:@"failure: %@",[error localizedDescription]] : @"success";
			UnitySendMessage([scoreflexUnityObjectName UTF8String], "HandleSubmitTurn", [message UTF8String]);
			NSLog(@"SubmitTurn Returned");
		}
	];
}

void scoreflexSubmitScore(const unichar *_leaderboardId, int _score)
{
	NSString *leaderboardId = fromUnichar(_leaderboardId);
	NSString *score = [NSString stringWithFormat:@"%d", _score];
	NSDictionary *params = [NSDictionary dictionaryWithObject:score forKey:@"score"];
	[Scoreflex submitScore:leaderboardId params:params
		handler:^(SXResponse *response , NSError *error) {
			NSString *message = error == nil ? [NSString stringWithFormat:@"failure: %@",[error localizedDescription]] : @"success";
			UnitySendMessage([scoreflexUnityObjectName UTF8String], "HandleSubmitScore", [message UTF8String]);
			NSLog(@"SubmitScore Returned");
		}
	];
}

void scoreflexSubmitScoreAndShowRanksPanel(const unichar *_leaderboardId, int _score)
{
	NSString *leaderboardId = fromUnichar(_leaderboardId);
	NSString *score = [NSString stringWithFormat:@"%d", _score];
	NSDictionary *params = [NSDictionary dictionaryWithObject:score forKey:@"score"];
	[Scoreflex submitScoreAndShowRanksPanel:leaderboardId params:params gravity:SXGravityTop];
}

void scoreflexSubmitTurnAndShowChallengeDetail(const unichar *_challengeInstanceId)
{
	NSString *challengeInstanceId = fromUnichar(_challengeInstanceId);
	
	[Scoreflex submitTurnAndShowChallengeDetail:challengeInstanceId params:nil];
}
