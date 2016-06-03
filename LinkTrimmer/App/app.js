
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

    $scope.trim = function ()
    {
        $http.post('/api/link/trim', { SourceUrl: $scope.url }).success(function (data)
        {
            $scope.url = data;
            $scope.trimmed = true;
        });
    }

    $scope.urlChanged = function ()
    {
        $scope.trimmed = false;
    }
})
.controller('HistoryController', function ($scope, $state, $http, $rootScope)
{
    $scope.page = $scope.pages = 1;
    $scope.pagination = [{ Page: $scope.page }];

    $scope.items = [];

    $scope.selectPage = function (page)
    {
        $scope.page = page;
        $http.post('/api/link/history', { Page: $scope.page }).success(function (data)
        {
            $scope.items = data.Items;
            $scope.pages = data.Pages;
            $scope.pagination = data.Pagination;
        });

        return false;
    }

    $scope.selectPage($scope.page);
});