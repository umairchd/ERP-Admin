(function () {
    LovebirdsApplication.controller('HomeController', HomeController)
        .directive('imageloaded', imageloaded)
        .directive('hoveredit', hoveredit); // required by demo;

    HomeController.$inject = ['$http', '$q', '$timeout', '$scope', '$rootScope', 'utils', 'Upload', 'toastr'];

    function HomeController($http, $q, $timeout, $scope, $rootScope, utils, Upload, toastr) {
        var vm = this;

        activate();

        function activate() {
            console.log("Home controller init");
            vm.tabs = { 'collection': 'collection', 'timeline': 'timeline', 'friends': 'friends' };
            var fileTypes = {
                'audio': 3,
                'video': 2,
                'image' : 1
            }
            vm.activeTab = 'timeline';
            vm.submitted = false;
            vm.mutations = [];
            vm.files = [];
            vm.Collections = {};
            vm.classifications = [];
            vm.friends = [];

            for (var i = 0; i < 10; i++) {
                vm.classifications.push({
                    Id: i,
                    ClassificationName: 'Classification ' + i
                });
            };

            var loadProfile = function () {
                vm.loading = true;
                utils.getCurrentUser().then(function (response) {
                    vm.user = response.data;
                    vm.loading = false;
                }, function (response) {
                    vm.loading = false;
                    toastr.error('error while loading profile info.');
                });
            };

            loadProfile();

            vm.openTab = function (tab) {
                vm.activeTab = tab;
                switch (tab) {
                    case vm.tabs.collection:
                        loadCollection();
                        break;

                    case vm.tabs.timeline:
                        loadTimeline();
                        break;
                    case vm.tabs.friends:
                        loadFriendList();
                        break;

                    default:
                }
            };

            var loadTimeline = function () {

            };
            var loadFriendList = function () {
                utils.getCurrentUser().then(function (response) {
                    var currentUser = response.data;
                    vm.friends = [];
                    $http.get('/api/FriendRequest/GetFriends', { params: { "userId": currentUser.Id } })
                    .then(function (response) {
                        response.data.forEach(function (item) {
                            vm.friends.push(item);
                        });
                    }, function (response) {
                        toastr.error('error while loading friend list.');
                    });
                });
            };

            var loadCollection = function () {
                //vm.loading = true;
                $http.get('/api/UserCollection').then(function (response) {
                    vm.Collections = response.data.Collections;
                    vm.mutations = response.data.Mutations;
                    vm.loading = false;
                }, function (response) {
                    vm.loading = false;
                    toastr.error('error while loading profile info.');
                });
            };

            vm.addCollection = function () {
                vm.addMode = true;
            };

            vm.back = function () {
                vm.addMode = false;
                loadCollection();
                vm.files = [];
                vm.errFiles = [];
            };

            vm.removeImage = function (index) {

                var file = vm.files[index];
                vm.files.splice(index, 1);
                //$http.delete('api/UserFile/' + file.result.Id).then(function (response) {
                //    vm.files.splice(index, 1);
                //}, function (error) {
                //    toastr.error('Error deleting file.');
                //});
            }

            vm.removeInvalid=  function(index) {
                vm.errFiles.splice(index, 1);
            }

            vm.selectFile = function (files, invalidFiles) {
                console.log('files selected');
                vm.errFiles = invalidFiles;

                angular.forEach(files.filter(function(x) { return !x.uploaded }), function (file) {
                    file.inProgress = true;
                    file.upload = Upload.upload({
                        url: '/api/UserCollection/UploadFile',
                        data: { file: file }
                    });

                    file.upload.then(function (response) {
                        $timeout(function () {
                            file.result = response.data;
                            file.failed = false;
                            file.inProgress = false;
                            file.uploaded = true;
                        });
                    }, function (response) {
                        file.failed = true;
                        file.inProgress = false;
                        file.uploaded = true;
                    }, function (evt) {
                        file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                    });
                });
            }

            vm.retry = function (file) {
                file.inProgress = true;
                file.upload = Upload.upload({
                    url: '/api/UserFile',
                    data: { file: file }
                });

                file.upload.then(function (response) {
                    $timeout(function () {
                        file.result = response.data;
                        file.failed = false;
                        file.inProgress = false;
                        file.uploaded = true;
                    });
                }, function (response) {
                    file.failed = true;
                    file.inProgress = false;
                    file.uploaded = true;
                }, function (evt) {
                    file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                });
            }

            vm.saveCollection = function() {
                if (!vm.collectionForm.$valid || vm.files.filter(function(x) { return x.uploaded && !x.failed }).length === 0) {
                    vm.collectionForm.$dirty = true;
                    toastr.error('Please fill all required fields, and upload at least 1 file');
                } else {
                    vm.Collection.saving = true;
                    vm.Collection.UserId = vm.user.Id;
                    vm.Collection.UserCollectionFiles = [];
                    vm.files.forEach(function(file) {
                        vm.Collection.UserCollectionFiles.push({
                            FilePath: file.result,
                            FileType: fileTypes[file.type.split('/')[0]]
                        });
                    });
                    $http.post('/api/UserCollection/Save', JSON.stringify(vm.Collection)).then(function(response) {
                        toastr.success('Collection saved.');
                        vm.Collection.saving = false;
                        vm.Collection = {};
                        vm.files = [];
                        vm.errFiles = [];
                        vm.back();
                    }, function (error) {
                        vm.Collection.saving = false;
                        toastr.error('Error occured while saving collection. Support Team has been notified');
                    });
                }
            }

        }
    }



    // Add class to img element when source is loaded
    function imageloaded() {
        // Copyright(c) 2013 André König <akoenig@posteo.de>
        // MIT Licensed
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            var cssClass = attrs.loadedclass;

            element.bind('load', function () {
                angular.element(element).addClass(cssClass);
            });
        }
    }

    function hoveredit() {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {

            var prev = element[0].firstElementChild;
            element.bind('mouseover', function () {
                prev.style.display = 'block';
                scope.$parent.card.showIndex = true;
                scope.$apply();
            });
            element.bind('mouseout', function () {
                prev.style.display = 'none';
                //$(prev).children('div[uib-dropdown]').click();
                scope.$parent.card.showIndex = false;
                scope.$apply();
            });
        }
    }
})();