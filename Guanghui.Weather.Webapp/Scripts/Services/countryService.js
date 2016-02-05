app.service('countryService', function ($http) {

    //Create new record
    this.post = function (country) {
        var request = $http({
            method: "post",
            url: "/api/Countries",
            data: country
        });
        return request;
    };

    //Get Single Records
    this.get = function (id) {
        return $http.get("/api/Countries/" + id);
    };

    //Get All Countries
    this.getAll = function () {
        return $http.get("/api/Countries");
    };


    //Update the Record
    this.put = function (id, country) {
        var request = $http({
            method: "put",
            url: "/api/Countries/" + id,
            data: country
        });
        return request;
    };

    //Delete the Record
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "/api/Countries/" + id
        });
        return request;
    };

});
