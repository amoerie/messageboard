'use strict';

angular.module('messageboard').controller('newTopicController', [
    '$scope', '$location', 'topicsService', 
    function($scope, $location, topicsService) {

        $scope.newTopic = {};

        $scope.addTopic = function() {
            topicsService.addTopic($scope.newTopic)
                .then(function() {
                    // success
                    alertify.success('Topic toegevoegd!');

                    // redirect to topics
                    $location.path('/topics');
                }, function () {
                    
                    // error
                    alertify.error('Oei er is iets misgelopen');
                });
        };

    }
]);