$("#register-username").keyup(function () {
    //$("form").submit(true);
    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response.exists);
        if (!response.exists) {
            $("#submit").attr("disabled", "disabled");
            $("#username-ans").addClass("glyphicon-remove");
            
        } else {
            $("#submit").removeAttr("disabled");
            $("#username-ans").addClass("glyphicon-ok");
        }
    }).fail(function (error) {
        console.log(error);
    });
});

var box_of_tweets = {};

var PutTweetsOnPage = function (array_of_tweets) {
    //clear out old tweets
    //$("#tweetTarget").html() = "";

    //populate with current tweets
    for (tweet in array_of_tweets) {
        console.log(array_of_tweets[tweet]);
        $("#tweetTarget").append(
        "<div class='panel panel-default tweet'>"+array_of_tweets[tweet].Author+"</br>"+array_of_tweets[tweet].Message+"<br><span class='icons'><div class='glyphicon-remove'></div></span></div>"
        )
    }
}

var PopulateTweets = function () {
    $.ajax({
        url: "/api/Tweet",
        method: 'GET'
    }).success(function (response) {
        console.log(response);
        PutTweetsOnPage(response);
    }).fail(function (error) {
        console.log(error);
    })
};

//on page load, get all tweets and put them on the page
$(document).ready(function () {
    PopulateTweets();
});

$("#submit_tweet").click(function () {
    $.ajax({
        url: "/api/Tweet",
        method: 'POST',
        data: {Message:$("#compose_tweet").val(), ImageURL:""}
    }).success(function(response){
        console.log("clicked", response);
    }).fail(function (error) {
        console.log(error);
    })
})



/*
$("#register-username").focusout(function () {
    //alert("defocused!!!");
    //console.log($(this).val());
    //$("#username-ans").addClass("hidden");
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response);
        if (response.exists) {
            $("#username-ans").addClass("glyphicon-ok");
        } else {
            $("#username-ans").addClass("glyphicon-remove");
        }
    }).fail(function (error) {
        console.log(error);
    });
});

$("#register-username").focusin(function () {
    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
});
*/