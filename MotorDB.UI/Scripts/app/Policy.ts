/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="DataService.ts" />

module MotorDB {

    export class Policy {
        constructor(
            public Identifier: number,
            public PolicyNumber: string,
            public Policyholder: string) {
        }
    }

    export class PolicyViewModel {
        private dataService: IDataService;
        public Policies: KnockoutObservableArray<Policy>;
        
        constructor(dataService: IDataService) {
            var self = this;
            self.dataService = dataService;
            self.Policies = ko.observableArray<Policy>();
        }

        public Load() {
            var self = this;
            self.dataService.GetAllPolicys()
                .done(function (data) {
                    $.each(data, function (index, element) {
                        var policy = new Policy(element.Identifier, element.PolicyNumber, element.PolicyholderName);
                        self.Policies.push(policy);
                    });
                })
        }
    }
}