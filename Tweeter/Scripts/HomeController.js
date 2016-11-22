app.controller("HomeCtrl", function ($scope, $http) {

    $scope.tweets = [];
    

    //$scope.PostNewTweet = function () {
    //    $http({
    //        method: 'POST',
    //        url:'api/Tweet'
    //    }).then(function(response){
    //        console.log("POST worked");
    //        console.log(response);
    //    }, function(err){
    //        console.log("POST failed");
    //        console.log(err);
    //    })
    //    };

    //$scope.GetAllTweets = function () {
        $http({
            method: 'GET',
            url:'/api/Tweet'
        }).then(function (response) {
            
            $scope.tweets = response.data;
            
            
            

        }, function (err) {
            console.log("Get call failed");
            console.log(err);
        })
    //}
    
   

})