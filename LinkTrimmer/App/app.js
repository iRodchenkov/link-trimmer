
var theApp = angular.module('LinkTrimmer', ['ui.router', 'ngclipboard']);

theApp.run(
    ['$rootScope', '$state', '$stateParams',
        function ($rootScope, $state, $stateParams)
        {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ]
);

// ---- ROUTING ----
theApp.config(function ($stateProvider, $urlRouterProvider)
{
    $urlRouterProvider.when("", "/");

    $stateProvider.state("trimmer", {
        url: "/",
        templateUrl: '/App/html/trimmer.html',
        controller: 'TrimmerController'
    });
        
    $stateProvider.state("history", {
        url: "/history",
        templateUrl: '/App/html/history.html',
        controller: 'HistoryController'
    });
})
.controller('TrimmerController', function ($scope, $state, $http, $rootScope)
{
    $scope.trimmed = false;
    $scope.processing = false;

    $scope.trim = function ()
    {
        $scope.processing = true;
        $http.post('/api/link/trim', { SourceUrl: $scope.url }).success(function (data)
        {
            if (data.R.HasErrors)
            {
                var displayError = function (error)
                {
                    var alert = '<div class="alert alert-danger validation-error fade in" role="alert"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>';
                    alert += error;
                    alert += '</div>';

                    $('#workspace').append(alert);
                    setTimeout(function ()
                    {
                        $('.validation-error').alert('close');
                    }, 2500);
                };

                displayError(data.R.AllErrors);
            }
            else
            {
                $scope.url = data.Link.TrimmedUrl;
                $scope.trimmed = true;
            }

            $scope.processing = false;
        });
    }

    $scope.urlChanged = function ()
    {
        $scope.trimmed = false;
    }
})
.controller('HistoryController', function ($scope, $state, $http, $rootScope)
{
    $scope.loading = true;

    $scope.page = $scope.pages = 1;
    $scope.pagination = [{ Page: $scope.page }];

    $scope.items = [];

    $scope.selectPage = function (page)
    {
        $scope.loading = true;
        $scope.page = page;
        $http.post('/api/link/history', { Page: $scope.page }).success(function (data)
        {
            $scope.items = data.Items;
            $scope.pages = data.Pages;
            $scope.pagination = data.Pagination;
            $scope.loading = false;
        });

        return false;
    }

    $scope.selectPage($scope.page);
});