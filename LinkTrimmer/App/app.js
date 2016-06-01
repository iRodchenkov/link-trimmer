
var theApp = angular.module('LinkTrimmer', ['ui.router']);

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
        
    $stateProvider.state("stats", {
        url: "/stats",
        templateUrl: '/App/html/stats.html',
        controller: 'StatsController'
    });
})
.controller('TrimmerController', function ($scope, $state, $http, $rootScope)
{
    $scope.data = "Test text!32";

    $scope.trim = function ()
    {
        $http.post('/api/link/trim', { SourceUrl: $scope.data }).success(function (data)
        {
            $scope.data = data;
        });
    }
})
.controller('StatsController', function ($scope, $state, $http, $rootScope)
{
    $scope.data = "224";
});