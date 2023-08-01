(function() {
    window.LovebirdsApplication = angular.module('LovebirdsApplication', ['ui.bootstrap', 'ngAnimate', 'ngSanitize', 'ngFileUpload', 'toastr', 'SignalR', 'ui.select', 'akoenig.deckgrid', 'ngDialog', 'infinite-scroll'])
        .run(function($rootScope, utils) {
            $rootScope.utils = utils;
        });

    LovebirdsApplication.filter('propsFilter', function () {
        return function (items, props) {
            var out = [];

            if (angular.isArray(items)) {
                var keys = Object.keys(props);

                items.forEach(function (item) {
                    var itemMatches = false;

                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }

            return out;
        };
    });

    LovebirdsApplication.controller('AppController', AppController);

    AppController.$inject = ['$http', '$scope', 'Hub','$window', 'toastr','utils'];

    function AppController($http, $scope, hub, $window, toastr, utils) {
        var vm = this;
        vm.pendingFriendRequests = [];
        vm.redirectToSearch = function () {
            $window.location.href = '/Friends?search=' + vm.searchTerm;
        }
        vm.updateFriendRequest = function (request, status) {
            request.IsApproved = status;
            takeActionOnFriendRequest(request);
        }
        activate();

        function activate() {
            //Logic here
            console.log("App controller init");
            $scope.welcomeMessage = 'Hello to Lovebirds';
            var chatHub = new hub('chatHub', {

                //client side methods
                listeners: {
                    'hello': function (name, message) {
                        $scope.$apply();
                    }
                },
                useSharedConnection: false,
                //server side methods
                //methods: ['send'],

                //query params sent on initial connection
                queryParams: {
                },

                //handle connection error
                errorHandler: function (error) {
                    console.error(error);
                },

                //specify a non default root
                //rootPath: '/api

                stateChanged: function (state) {
                    switch (state.newState) {
                        case $.signalR.connectionState.connecting:
                            console.log('connecting to chat hub...');
                            break;
                        case $.signalR.connectionState.connected:
                            console.log('chat hub connected');
                            break;
                        case $.signalR.connectionState.reconnecting:
                            console.log('re-connecting to chat hub...');
                            //your code here
                            break;
                        case $.signalR.connectionState.disconnected:
                            console.log('connection closed. Disconnected');
                            //your code here
                            break;
                    }
                }
            });
            getPendingFriendRequests();
        }
        
        function getPendingFriendRequests() {
            utils.getCurrentUser().then(function (response) {
                var currentUser = response.data;
                $http.get('/api/FriendRequest/GetPendingRequests', { params: { "userId": currentUser.Id } })
                .then(function (response) {
                    response.data.forEach(function (item) {
                        vm.pendingFriendRequests.push(item);
                    });
                }, function (response) {
                    toastr.error('error while loading friend requests.');
                });
            });
        }
        function takeActionOnFriendRequest(request) {
            $http.post('/api/FriendRequest/UpdateStatus', request)
                .then(function(response) {
                    var index = vm.pendingFriendRequests.indexOf(request);
                    if (index > -1) {
                        vm.pendingFriendRequests.splice(index, 1);
                    }
                }, function(response) {
                    toastr.error('error occured while updating freind request.');
                });
        }
    }
})();