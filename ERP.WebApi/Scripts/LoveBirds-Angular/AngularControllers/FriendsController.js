(function () {
    LovebirdsApplication.controller('FriendsController', FriendsController);

    FriendsController.$inject = ['$http', '$scope', '$rootScope', 'toastr', 'ngDialog', 'utils'];

    function FriendsController($http, $scope, $rootScope, toastr, ngDialog, utils) {
        var vm = this;
        vm.users = [];
        vm.loading = false;
        vm.loadMore = getSearchResults;
        vm.sendFriendRequest = sendFriendRequest;
        
        var currentUser,
        pageSize = 10,
        pageNo = 1;

        loadCurrentUser();

        function getSearchResults() {
            if (vm.loading) return;
            vm.loading = true;
            if (utils.getParameterByName('search')) {
                $http.get('/api/FriendRequest/FriendSearch', { params: { "SearchString": utils.getParameterByName('search'), 'PageSize': pageSize, 'PageNo': pageNo } })
                .then(function (response) {
                    response.data.List.forEach(function (item) {
                        vm.users.push(item);
                    });
                    pageNo += 1;
                    vm.loading = false;
                }, function (response) {
                    vm.loading = false;
                    toastr.error('error while loading users.');
                });
            }
        }

        function loadCurrentUser() {
            utils.getCurrentUser().then(function (response) {
                currentUser = response.data;
            });
        };

        function sendFriendRequest(user) {
            ngDialog.openConfirm({
                template: 'friendRequestlDialog',
                className: 'ngdialog-theme-default'
            }).then(function(notes) {
                user.inProcess = true;
                if (currentUser) {
                    $http.post('/api/FriendRequest/FriendRequest', { "SenderId": currentUser.Id, 'TargetId': user.Id, 'Notes': notes })
                        .then(function(response) {
                            user.inProcess = false;
                            user.isCompleted = true;
                        }, function(response) {
                            user.inProcess = false;
                            toastr.error('error occured while sending friend request, please try again later.');
                        });
                }
            });
        }
    }
})();