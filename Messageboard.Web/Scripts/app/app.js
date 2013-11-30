'use strict';

var messageboard = angular.module("messageboard", [ "ngRoute" ]);

messageboard.config(["$routeProvider", "$locationProvider", function($routeProvider, $locationProvider) {
    $routeProvider.when('/topics', {
        templateUrl: 'Templates/topics.html',
        controller: 'topicsController'
    });

    $routeProvider.when('/new-topic', {
        templateUrl: '/Templates/new-topic.html',
        controller: 'newTopicController'
    });
    
    $routeProvider.when('/topic/:id', {
        templateUrl: '/Templates/topic.html',
        controller: 'topicDetailController'
    })

    $routeProvider.otherwise({ redirectTo: '/topics' });
    $locationProvider.html5Mode(true);
}]);