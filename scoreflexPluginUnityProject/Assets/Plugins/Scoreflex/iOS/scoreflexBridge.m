#import <Scoreflex/Scoreflex.h>

void scoreflexInitialize(char *_id, char *_secret, int _sandbox)
{
	NSString *id = [NSString stringWithCString:_id encoding:NSASCIIStringEncoding];
	NSString *secret = [NSString stringWithCString:_secret encoding:NSASCIIStringEncoding];
	BOOL sandbox = _sandbox == 1 ? YES : NO;

	[Scoreflex setClientId:id secret:secret sandboxMode:sandbox];
}

void scoreflexShowPlayerProfile()
{
	[Scoreflex showPlayerProfile:nil params:nil];
}
