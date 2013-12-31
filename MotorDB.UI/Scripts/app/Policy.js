/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="DataService.ts" />
var MotorDB;
(function (MotorDB) {
    var Policy = (function () {
        function Policy(Identifier, PolicyNumber, Policyholder) {
            this.Identifier = Identifier;
            this.PolicyNumber = PolicyNumber;
            this.Policyholder = Policyholder;
        }
        return Policy;
    })();
    MotorDB.Policy = Policy;

    var PolicyViewModel = (function () {
        function PolicyViewModel() {
            var self = this;
            self.dataService = new MotorDB.DataService();
            self.Policies = ko.observableArray();
        }
        PolicyViewModel.prototype.Load = function () {
            var self = this;
            self.dataService.GetAllPolicys().done(function (data) {
                $.each(data, function (index, element) {
                    var policy = new Policy(element.Identifier, element.PolicyNumber, element.PolicyholderName);
                    self.Policies.push(policy);
                });
            });
        };
        return PolicyViewModel;
    })();
    MotorDB.PolicyViewModel = PolicyViewModel;
})(MotorDB || (MotorDB = {}));
//# sourceMappingURL=Policy.js.map
