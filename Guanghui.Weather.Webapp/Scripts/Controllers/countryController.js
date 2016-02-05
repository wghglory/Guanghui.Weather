
app.controller('countryController', function ($scope, countryService) {


    $scope.selectedCountryIds = [];
    

    $scope.IsNewRecord = 1; //The flag for the new record
    getAll();

    function getAll() {
        countryService.getAll().then(function (res) {
            $scope.Countries = res.data;
        },
            function (err) {
                $log.error('failure loading Country', err);
            });
    }

    $scope.save = function () {
        var Country = {
            CountryId: $scope.CountryId,
            CountryName: $scope.CountryName,
            Temperature: $scope.Temperature
        };
        //If the flag is 1 add new record
        if ($scope.IsNewRecord === 1) {
            countryService.post(Country).then(function (res) {
                $scope.CountryId = res.data.CountryId;
                getAll();
                $scope.Message = 'Created Successfuly';
            }, function (err) {
                console.log('Err' + err);
            });
        } else { //Edit
            countryService.put($scope.CountryId, Country).then(function (res) {
                $scope.Message = 'Updated Successfuly';
                getAll();
            }, function (err) {
                console.log('Err' + err);
            });
        }
    };


    $scope.delete = function (countryId) {
        countryService.delete(countryId).then(function (res) {
            $scope.Message = 'Deleted Successfuly';
            $scope.CountryId = 'NA';
            $scope.CountryName = '';
            $scope.Temperature = -1;
            getAll();
        }, function (err) {
            console.log('Err' + err);
        });
    }


    $scope.get = function (country) {
        $scope.Message = '';

        countryService.get(country.CountryId).then(function (res) {
            $scope.CountryId = res.data.CountryId;
            $scope.CountryName = res.data.CountryName;
            $scope.Temperature = res.data.Temperature;
            $scope.IsNewRecord = 0;
        },
        function (err) {
            console.log('failure loading Country', err);
        });
    }

    $scope.getById = function (id) {
        $scope.Message = '';

        countryService.get(id).then(function (res) {
            $scope.CountryId = res.data.CountryId;
            $scope.CountryName = res.data.CountryName;
            $scope.Temperature = res.data.Temperature;
            $scope.IsNewRecord = 0;
        },
        function (err) {
            console.log('failure loading Country', err);
        });
    }

    $scope.clear = function () {
        $scope.IsNewRecord = 1;
        $scope.Message = '';

        $scope.CountryId = 0;
        $scope.CountryName = '';
        $scope.Temperature = 0;
    }
});