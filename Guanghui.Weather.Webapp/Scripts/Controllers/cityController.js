app.controller('cityController', function ($scope, cityService) {

    $scope.IsNewRecord = 1; //The flag for the new record
    getAll();

    $scope.getCitiesByTemp = function (t1, t2) {
        $scope.Message = '';

        cityService.getCitiesByTemp(t1, t2).then(function (res) {
            $scope.Cities = res.data[0].Cities;
            $scope.Countries = res.data[0].Countries;

            ////method 1
            //var lookup = {};//{Brazil: 3, China: 7, France: 4, India: 3, USA: 5}
            //var items = res.data;
            //var result = []; //result = ["Brazil", "China", "France", "India", "USA"]
            //for (var item, i = 0; item = items[i++];) {
            //    var name = item.CountryName;
            //    var countr = {};
            //    if (!(name in lookup)) {
            //        lookup[name] = 1;

            //        countr["countryName"] = name;
            //        countr["count"] = 1;
            //        result.push(countr);
            //    } else {
            //        lookup[name]++;
            //    }
            //}

            ////method 2
            //var countries = {}, singleOj; //{Brazil: 3, China: 7, France: 4, India: 3, USA: 5}, singleOj = Object {CityId: 322, CityName: "Fernley", StateId: 0, CountryId: 5, Latitude: 0…}
            //for (var i = 0, l = res.data.length; i < l; i++) {
            //    singleOj = res.data[i];
            //    countries[singleOj.CountryName] = (countries[singleOj.CountryName] || 0) + 1;
            //}
            ////want to get this: [{country:"brazil",count:3},{country:"china",count:7}]
            //$scope.Countries = countries;

        },
            function (err) {
                console.log('failure loading Cities', err);
            });
    };

    function getAll() {
        cityService.getAll().then(function (res) {
            $scope.Cities = res.data;
        },
            function (err) {
                $log.error('failure loading City', err);
            });
    }

    $scope.save = function () {
        var City = {
            CityId: $scope.CityId,
            CityName: $scope.CityName,
            CountryId: $scope.CountryId,
            CountryName: $scope.CountryName,
            Temperature: $scope.Temperature
        };
        //If the flag is 1 add new record
        if ($scope.IsNewRecord === 1) {
            cityService.post(City).then(function (res) {
                $scope.CityId = res.data.CityId;
                getAll();
                $scope.Message = 'Created Successfuly';
            }, function (err) {
                console.log('Err' + err);
            });
        } else { //Edit
            cityService.put($scope.CityId, City).then(function (res) {
                $scope.Message = 'Updated Successfuly';
                getAll();
            }, function (err) {
                console.log('Err' + err);
            });
        }
    };


    $scope.delete = function (cityId) {
        cityService.delete(cityId).then(function (res) {
            $scope.Message = 'Deleted Successfuly';
            $scope.CityId = 'NA';
            $scope.CityName = '';
            $scope.CountryId = 'NA';
            $scope.CountryName = '';
            $scope.Temperature = -1;
            getAll();
        }, function (err) {
            console.log('Err' + err);
        });
    }


    $scope.get = function (city) {
        $scope.Message = '';

        cityService.get(city.CityId).then(function (res) {
            $scope.CityId = res.data.CityId;
            $scope.CityName = res.data.CityName;
            $scope.Temperature = res.data.Temperature;
            $scope.CountryId = res.data.CountryId;
            $scope.CountryName = res.data.CountryName;
            $scope.IsNewRecord = 0;
        },
        function (err) {
            console.log('failure loading City', err);
        });
    }

    $scope.getById = function (id) {
        $scope.Message = '';

        cityService.get(id).then(function (res) {
            $scope.CityId = res.data.CityId;
            $scope.CityName = res.data.CityName;
            $scope.Temperature = res.data.Temperature;
            $scope.IsNewRecord = 0;
        },
        function (err) {
            console.log('failure loading City', err);
        });
    }

    $scope.clear = function () {
        $scope.IsNewRecord = 1;
        $scope.Message = '';

        $scope.CityId = 0;
        $scope.CityName = '';
        $scope.Temperature = 0;
        $scope.CountryName = '';
        $scope.CountryId = 0;
    }
});