/// <reference path="../typings/jquery/jquery.d.ts" />

module MotorDB {
    export class DataService {

        public GetAllPolicys() {
            return $.ajax({
                url: 'api/policy',
                type: 'GET'
            });            
        }

    }
}