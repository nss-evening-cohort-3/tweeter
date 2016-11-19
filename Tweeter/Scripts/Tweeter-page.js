$(
    $.get("api/Tweet", function (data) {
        console.log(data);
        if (data.length > 0) {

            let tweetBoxes = "";
            for (var i = 0; i < data.length; i++) {
                
                tweetBoxes += `<div class="row text-center">`;
                tweetBoxes += `  <div class="col-xs-6 col-sm-offset-3">`;
                tweetBoxes += `    <img class="tweeterimg" src="${data[i].ImageURL}">`;
                tweetBoxes += `    <p>Message: ${data[i].Message}</p>`;
                tweetBoxes += `    <div class="btn-group" role="group" aria-label="...">`;
                tweetBoxes += `      <button type="button" class="btn btn-default">Retweet</button>`;
                tweetBoxes += `      <button type="button" class="btn btn-default">Delete</button>`;
                tweetBoxes += `    </div>`;
                tweetBoxes += `  </div>`;
                tweetBoxes += `</div>`;
            }
            $("div.tweetbox").html(tweetBoxes);
        } else {
            $("div.tweetbox").html("<p>No Tweets today!</p>");
        }
    })
);

$("#submit-tweet").click(function() {
    $.post("/api/Tweet", { message: "Here is a message", ImageURL: "Blank" });
});