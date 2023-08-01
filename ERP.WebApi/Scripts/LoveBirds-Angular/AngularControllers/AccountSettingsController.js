(function() {
    LovebirdsApplication.controller('AccountSettingsController', AccountSettingsController);

    AccountSettingsController.$inject = ['$http', '$q', '$timeout', '$scope', '$rootScope', 'utils', 'Upload', 'toastr'];

    function AccountSettingsController($http, $q, $timeout, $scope, $rootScope, utils, Upload, toastr) {
        var vm = this;
        vm.utils = utils;
        vm.selectedTab = 'Profile';
        vm.user = {};
        vm.loading = {};
        activate();

        function activate() {
            console.log("AccountSettingsController init");

            var loadProfile = function () {
                vm.loading.profile = true;
                $http.get('/api/UserProfile').then(function (response) {
                    vm.user = response.data;
                    vm.file = vm.ProfileImage = vm.user.ProfileImage;
                    vm.loading.profile = false;
                }, function (response) {
                    vm.loading.profile = false;
                    toastr.error('error while loading profile info.');
                });
            };

            vm.selectTab = function (tabName) {
                vm.selectedTab = tabName;
            }

            vm.selectFile = function (file) {
                console.log(file);
                if (file) {
                    vm.ProfileImage = file;
                } else {
                    vm.file = vm.ProfileImage;
                }
            }

            vm.removeImage = function() {
                vm.file = null;
                vm.user.ProfileImage = null;
            }

            vm.changeImage = function() {
                $('.drop-box').click();
            }

            vm.updateBasicInfo = function () {
                var formData = new FormData();
                formData.append('file', vm.file);
                formData.append('user', JSON.stringify(vm.user));
                vm.loading.profile = true; 
                $http({
                    url: '/api/UserProfile',
                    method: 'POST',
                    data: formData,
                    //assign content-type as undefined, the browser
                    //will assign the correct boundary for us
                    headers: { 'Content-Type': undefined },
                    //prevents serializing payload.  don't do it.
                    transformRequest: angular.identity
                }).then(function (response) {
                    //vm.user = response.data;
                    toastr.success('Your Profile has been updated.');
                    vm.loading.profile = false;
                }, function () {
                    toastr.error('something went wrong while updating your profile.');
                    vm.loading.profile = false;
                });

                //Upload.upload({
                //    url: '/api/UserProfile',
                //    data: { file: vm.file, 'user': JSON.stringify(vm.user) }
                //}).then(function (resp) {
                //    console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
                //}, function (resp) {
                //    console.log('Error status: ' + resp.status);
                //}, function (evt) {
                //    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                //    console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
                //});
            };

            vm.changePassword = function () {

                $http.post('/Manage/ChangePassword', vm.userPassword).then(function (response) {
                    toastr.success('Password has been changed');

                }, function(error) {
                    toastr.error('Error while updating password.');
                });
            }

            loadProfile();
        }
    }
})();