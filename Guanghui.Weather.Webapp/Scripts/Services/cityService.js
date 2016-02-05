app.service('cityService', function ($http) {

    //Create new record
    this.post = function (city) {
        var request = $http({
            method: "post",
            url: "/api/Cities",
            data: city
        });
        return request;
    };

    //Get Single Records
    this.get = function (id) {
        return $http.get("/api/Cities/" + id);
    };

    //Get All Cities
    this.getAll = function () {
        return $http.get("/api/Cities");
    };

    //Get Cities within the temperature range
    this.getCitiesByTemp = function (t1, t2) {
        return $http.get("/api/Cities?t1=" + t1 + "&t2=" + t2);
    };

    //Update the Record
    this.put = function (id, city) {
        var request = $http({
            method: "put",
            url: "/api/Cities/" + id,
            data: city
        });
        return request;
    };

    //Delete the Record
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "/api/Cities/" + id
        });
        return request;
    };

});
