// CS4417 Assignment 1, Brandon Luu

// New collection which contains each hashtag used in tweets, with # of times hashtag was used

function myMapper() {
    //The mapper function is called with each document, which has the special name 'this'

    //Emit a key-value pair:

    //(the mapper can emit many key-value pairs if needed)

    // For each tweet, loop through the hashtags and emit them
    for (var hashtag in this.entities.hashtags) {
        emit(this.entities.hashtags[hashtag].text, 1);
    }
}

function myReducer(key, values) {
    //The reducer is called once for each key, and is passed an array

    //containing all values corresponding to that key.

    //Produce the desired result

    return Array.sum( values )
}


// map + reduce!
db.tweets.mapReduce(myMapper, myReducer, { query: {}, out: "mroutput" })

// sort and list them
var output = db.mroutput.aggregate({$sort: {value: -1}})

// for some reason load() only displays 'true' when running, so display this msg
print("Done! Run this to display hashtags: db.mroutput.aggregate({$sort: {value: -1}})")