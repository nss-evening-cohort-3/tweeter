$(
    displayTweets()
);

function displayTweets() {
    $.get("api/Tweet", function (data) {
        if (data.length > 0) {
            let tweetBoxes = "";
            for (var i = 0; i < data.length; i++)
            {
                tweetBoxes += `<div id="${data[i].TweetId}" class="row text-center mainTweetDiv">`;
                tweetBoxes += `  <div class="col-xs-6 col-sm-offset-3">`;
                tweetBoxes += `    <img class="tweeterimg" src="${data[i].ImageURL}">`;
                tweetBoxes += `    <p>Message: ${data[i].Message}</p>`;
                tweetBoxes += `    <div class="btn-group" role="group" aria-label="...">`;
                tweetBoxes += `      <button type="button" class="btn btn-default">Retweet</button>`;
                tweetBoxes += `      <button type="button" class="btn btn-default delete">Delete</button>`;
                tweetBoxes += `    </div>`;
                tweetBoxes += `  </div>`;
                tweetBoxes += `</div>`;
            }
            $("div.tweetbox").html(tweetBoxes);
        } else {
        $("div.tweetbox").html("<p>No Tweets today!</p>");
        }
    })
};

$('.tweetbox').click(function(event) {
    if ( $(event.target).hasClass("delete")) {
        let deleteTweetId = $(event.target).parents(".mainTweetDiv").attr("id");
        $.ajax({
            type: "DELETE",
            url: `api/Tweet/${deleteTweetId}`,
            success: console.log("Delete Successful")
        });
        $(`div[id=${deleteTweetId}]`).remove();
    }
});

$("#submit-tweet").click(function () {
    let newTweet = {
        Message: $('input.composer').val(),
        ImageURL: "http://placehold.it/350x175"
    }
    $.ajax({
        type: "POST",
        url: `api/Tweet`,
        data: newTweet
    }).then(function () {
        displayTweets();
    });
});