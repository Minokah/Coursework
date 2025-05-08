// CS4417 Assignment 1, Brandon Luu

// Find all tweets replying to globeandmail
db.tweets.find({in_reply_to_screen_name: 'globeandmail'}).pretty()

// Find all tweets from MLHealthUnit
db.tweets.find({'user.screen_name': 'MLHealthUnit'}).pretty()