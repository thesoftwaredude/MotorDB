var fakeDataResponse = [{
    Identifier: 1,
    PolicyholderName: "Policyholder 1",
    PolicyNumber: "1001",
    PolicyPeriods: [{
        StartDate: "2011-02-01",
        EndDate: "2012-01-31"
    },
        {
            StartDate: "2012-02-01",
            EndDate: "2013-01-31"
        },
        {
            StartDate: "2013-02-01",
            EndDate: "2014-01-31"
        }]
}];

describe('test populating Policy data using sinon.useFakeHttpRequest', function () {

    var requests;
    beforeEach(function () {
        this.xhr = sinon.useFakeXMLHttpRequest();
        requests = this.requests = [];

        this.xhr.onCreate = function (xhr) {
            requests.push(xhr);
        };
    });

    afterEach(function () {
        this.xhr.restore();
    });

    it('Get Policy Data ', function () {
        var db = new MotorDB.DataService();
        var sut = new MotorDB.PolicyViewModel(db);
        sut.Load();
        requests[0].respond(200, { "Content-Type": "application/json" }, JSON.stringify(fakeDataResponse));
        var policyData = sut.Policies();
        expect(policyData.length).to.equal(1);
    });
});

describe('Populate Policy Data using sinon.fakeServer', function() {
    var server;

    beforeEach(function () {
        server = sinon.fakeServer.create();
        server.respondWith([200, { "Content-Type": "application/json" }, JSON.stringify(fakeDataResponse)]);
    });

    afterEach(function () {
        server.restore();
    });

    it('Get Policy Data', function() {
        var db = new MotorDB.DataService();
        var sut = new MotorDB.PolicyViewModel(db);
        sut.Load();
        server.respond();
        var policyData = sut.Policies();
        expect(policyData.length).to.equal(1);
    });
});