angular.module('messageboard').controller('topicDetailController', [
    "$scope", "$routeParams", "topicsService",
    function($scope, $routeParams, topicsService) {
        $scope.isBusy = true;
        $scope.isSendingReply = false;
        $scope.topic = {};
        $scope.newReply = {};

        $scope.addReply = function () {
            $scope.isSendingReply = true;
            topicsService.addReply($scope.topic, $scope.newReply)
                .then(function() {
                    // success
                    alertify.success('Reactie toegevoegd!');
                    $scope.newReply = {};
                }, function() {
                    // error
                    alertify.error('Reactie niet toegevoegd');
                })
                .then(function() {
                    // always
                    $scope.isSendingReply = false;
                });
        };

        topicsService.getTopicById($routeParams.id, { includeReplies: true })
            .then(function(topic) {
                // success
                angular.copy(topic, $scope.topic);
                alertify.success('Topic met id = ' + topic.id + ' opgehaald!');
            }, function() {
                // error
                alertify.error('Er is iets misgelopen bij het ophalen van de topic');
            })
            .then(function() {
                // always
                $scope.isBusy = false;
            });
        
        
    }
]);