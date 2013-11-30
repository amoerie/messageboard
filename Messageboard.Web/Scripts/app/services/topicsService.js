'use strict';

angular.module('messageboard').factory('topicsService', [
    "$http", "$q",
    function ($http, $q) {

        var topics = [];
        var areTopicsLoaded = false;

        var loadTopics = function (options) {
            options = options || {};
            var includeReplies = options.includeReplies || false;
            var deferred = $q.defer();

            $http.get('api/v1/topics', { params: { includeReplies: includeReplies } })
                .then(function(result) {
                    // success
                    angular.copy(result.data, topics);
                    deferred.resolve();
                    areTopicsLoaded = true;
                }, function() {
                    // error
                    deferred.reject();
                });

            return deferred.promise;
        };

        var addTopic = function(topic) {

            var deferred = $q.defer();

            $http.post('/api/v1/topics', topic)
                .then(function(result) {
                    // success
                    topics.push(result.data);
                    deferred.resolve();
                }, function() {
                    // error
                    deferred.reject();
                });

            return deferred.promise;
        };

        var getTopicById = function(id, options) {

            var deferred = $q.defer();

            options = options || {};
            var includeReplies = options.includeReplies || false;

            $http.get('/api/v1/topics/' + id, { params: { includeReplies: includeReplies } })
                .then(function(result) {
                    // success
                    deferred.resolve(result.data);
                }, function() {
                    // error
                    deferred.reject();
                });

            return deferred.promise;
        };

        var addReply = function(topic, reply) {

            var deferred = $q.defer();

            $http.post('/api/v1/topics/' + topic.id + '/replies', reply)
                .then(function(result) {
                    // success
                    topic.replies = topic.replies || [];
                    var newReply = result.data;
                    topic.replies.push(newReply);
                    deferred.resolve(newReply);
                }, function() {
                    // error
                    deferred.reject();
                });

            return deferred.promise;
        };

        return {
            loadTopics: loadTopics,
            topics: topics,
            areTopicsLoaded: function () { return areTopicsLoaded; },
            addTopic: addTopic,
            getTopicById: getTopicById,
            addReply: addReply
        };
    }
]);