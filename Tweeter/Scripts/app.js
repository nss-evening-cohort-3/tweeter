var app = angular.module("Tweeter", []);

app.controller('TweetCtrl', function ($scope, $http) {
    $scope.tweets = [
       /* {
            username: "JakeFromStateFarm",
            message: "Hello",
            image: null,
            date: "Nov, 21 2016"
        },
        {
            username: "FloFroProgressive",
            message: "Progressive!",
            image: "http://placehold.it/350x150",
            date: "Nov, 20 2016"
        },
        {
            username: "JakeFromStateFarm",
            message: "Hello Again",
            image: null,
            date: "Nov, 21 2016"
        }*/
    ];
    

    $scope.running = false;

    $scope.getTweets = function () {
        if (!$scope.running) {
            $scope.running = true;
            $http.get("api/Tweet")
                   .success(function (response) {
                       console.log(response);
                       $scope.tweets = response;
                       /*for (var i = 0; i < response.length; i++) {
                           $scope.tweets.push(response.data[i]);
                       }
                       */
                       
                   })
                    .error(function (error) {
                        console.log("failed!");
                    });
        }

        return $scope.tweets;
    }
});

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