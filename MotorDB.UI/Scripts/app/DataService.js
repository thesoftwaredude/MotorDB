/// <reference path="../typings/jquery/jquery.d.ts" />
var MotorDB;
(function (MotorDB) {
    var DataService = (function () {
        function DataService() {
        }
        DataService.prototype.GetAllPolicys = function () {
            return $.ajax({
                url: 'api/policy',
                type: 'GET'
            });
        };
        return DataService;
    })();
    MotorDB.DataService = DataService;
})(MotorDB || (MotorDB = {}));
//# sourceMappingURL=DataService.js.map
