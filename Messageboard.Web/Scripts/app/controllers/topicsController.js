'use strict';

angular.module('messageboard').controller('topicsController', [
    "$scope", "$location", "topicsService",
    function($scope, $location, topicsService) {
        $scope.isBusy = true;
        $scope.data = topicsService;
        $scope.go = function(route) {
            $location.path(route);
        };

        if (topicsService.areTopicsLoaded()) {
            $scope.isBusy = false;
        } else {
            topicsService.loadTopics()
                .then(function() {
                    // success
                    alertify.success("Topics ingeladen!", 1000);

                }, function() {
                    // error
                    alertify.error("Topics konden niet opgehaald worden!", 1000);
                })
                .then(function() {
                    // always
                    $scope.isBusy = false;
                });
        }
    }
]);