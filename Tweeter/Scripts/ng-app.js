var app = angular.module("myApp", []);

app.controller("tweetController", function ($scope, $http) {

    $scope.tweetsToDisplay = [];

    $scope.getAllTweets = function () {
        $scope.tweetsToDisplay.length = 0;
        $http({
            method: 'GET',
            url: '/api/Tweet/'
        }).then(function successCallback(response) {
            for (var i = 0; i < response.data.length; i++){
                $scope.tweetsToDisplay.push(response.data[i]);
                console.log($scope.tweetsToDisplay);
            };
        }, function errorCallback(response) {
            console.log(response);
        });
    }


});