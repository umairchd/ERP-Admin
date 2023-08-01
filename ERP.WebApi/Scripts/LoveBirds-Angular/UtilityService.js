(function () {
    LovebirdsApplication.service('utils', utilityService);

    utilityService.$inject = ['$http', '$q', '$timeout', '$rootScope'];

    function utilityService($http, $q, $timeout, $rootScope) {
        var validateInput = function (property, types, submitted) {
            if (!property || !types || !types.length) {
                return false;
            }
            var t = types.split(",");
            var hasError = false;
            for (var i in t) {
                if (types.hasOwnProperty(i)) {
                    if ((property.$dirty || submitted) && property.$error[t[i]]) {
                        hasError = true;
                        break;
                    }

                }
            }
            return hasError;
        };

        var toLocalTime = function (date) {
            var local = moment.utc(date).local().toDate();
            return new Date(local);
        }

        var getParameterByName = function (name, url) {
            if (!url) {
                url = window.location.href;
            }
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        };

        var getCurrentUser = function() {
            var deferred = $q.defer();
            $http.get('/api/UserProfile').then(function(res) {
                return deferred.resolve(res);
            }, function(err) {
                deferred.reject(err);
            });

            return deferred.promise;
        }

        var confirmDelete = function (callback) {
            swal({
                    title: "Are you sure?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, delete it!",
                    cancelButtonText: "No, cancel!",
                    closeOnConfirm: true,
                    closeOnCancel: false
                }, callback);
        }

        return {
            toLocalTime: toLocalTime,
            validateInput: validateInput,
            getParameterByName: getParameterByName,
            getCurrentUser: getCurrentUser,
            confirmDelete: confirmDelete
        }
    }
})();


