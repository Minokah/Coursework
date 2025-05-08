// CS4417 Assignment 1, Brandon Luu

// List of users with total # of tweets posted, in decreasing order
db.tweets.aggregate([
    {
        $group: {_id: '$user.screen_name', count: { $count: {}}}
    },
    {
        $sort: {'count': -1}
    }
])

// List of place names with total # of tweets from that place in decreasing order
db.tweets.aggregate([
    {
        $group: {_id: '$place.name', count: { $count: {}}}
    },
    {
        $sort: {'count': -1}
    }
])

// List of users with total # of replies to that user in decreasing order
db.tweets.aggregate([
    {
        $group: {_id: '$in_reply_to_screen_name', count: { $count: {} }}
    },
    {
        $sort: {'count': -1}
    }
])

// List of users with total # of hashtags used by that user in decreasing order
db.tweets.aggregate([
    {
        $unwind: '$entities.hashtags'
    },
    {
        $group: {_id: '$user.screen_name', count: { $count: {} }}
    },
    {
        $sort: {'count': -1}
    }
])