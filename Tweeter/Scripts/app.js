﻿$("#register-username").keyup(function () {
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
var app = angular.module("TweeterApp", []);


app.controller('TweetController', function ($scope, userService, $resource, $http) {
    $scope.getTweets = function () {
        $http({
            url: '/api/tweet/',
            method: "GET"
        })
        .then(function (result) {
            $scope.Tweet.Message = angular.copy(result.data);

        }, function (error) {
        //put error syntax here
        });
    }
    getTweets();
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


});

$("#register-username").focusin(function () {
    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
});
*/

