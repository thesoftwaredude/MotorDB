/// <reference path="../typings/jquery/jquery.d.ts" />

module MotorDB {

    export interface IDataService {
        GetAllPolicys();
    }

    export class DataService implements IDataService {

        public GetAllPolicys() {
            return $.ajax({
                url: 'api/policy',
                type: 'GET'
            });            
        }
    }
}