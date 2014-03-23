describe('dsc2', function() {
    var fake1 = [{
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

    var myStub;

    beforeEach(function() {
    });

    afterEach(function() {
    });

    it('tst2', function () {

        //var myStub2 = sinon.stub(MotorDB.DataService, "GetAllPolicys");
        myStub = sinon.stub(MotorDB.DataService, 'GetAllPolicys', function () {
            // Creating a deffered object 
            var dfd = $.Deferred();

            // assigns success callback to done.
            //if (options.success)
            dfd.done(options.success({ status_code: 200, data: fake1 }));

            // assigns error callback to fail.
            //if (options.error) 
            dfd.fail(options.error);
            dfd.success = dfd.done;
            dfd.error = dfd.fail;

            // returning the deferred object so that we can chain it.
            return dfd.promise();

        });

        var sut = new MotorDB.PolicyViewModel(myStub);
        sut.Load();
        var data = sut.Policies();
    });
});

//describe('dsc99', function () {

//    var fake1 = [{
//        Identifier: 1,
//        PolicyholderName: "Policyholder 1",
//        PolicyNumber: "1001",
//        PolicyPeriods: [{
//            StartDate: "2011-02-01",
//            EndDate: "2012-01-31"
//        },
//            {
//                StartDate: "2012-02-01",
//                EndDate: "2013-01-31"
//            },
//            {
//                StartDate: "2013-02-01",
//                EndDate: "2014-01-31"
//            }]
//    }];

//    it('tst1', function () {
//        var db = new MotorDB.DataService();
//        sinon.stub(db, "GetAllPolicys").returns(fake1);
//        var aa = db.GetAllPolicys();
//        var sut = new MotorDB.PolicyViewModel(db);
//        sut.Load();
//        var data = sut.Policies();
//        //expect(sut.Policies().length).to.be(3);
//        console.log(aa);
//    });

//});

//describe('test policy', function () {

//    var server;
//    var fakeData = [{
//        Identifier: 1,
//        PolicyholderName: "Policyholder 1",
//        PolicyNumber: "1001",
//        PolicyPeriods: [{
//            StartDate: "2011-02-01",
//            EndDate: "2012-01-31"
//        },
//            {
//                StartDate: "2012-02-01",
//                EndDate: "2013-01-31"
//            },
//            {
//                StartDate: "2013-02-01",
//                EndDate: "2014-01-31"
//            }]
//    }];

//    before(function() {
//        //sinon.stub($, "ajax").yields(fakeData);
//        //mock = sinon.mock(jQuery).expects("ajax").yieldsTo("success", fakeData);
//        server = sinon.fakeServer.create();
//        server.respondWith("GET", "api/policy",
//            [200, { "Content-Type": "application/json" }, JSON.stringify(fakeData)]);
//    });

//    after(function() {
//        //$.ajax.restore();
//        server.restore();
//    });


//    beforeEach(function() {

//    });

//    afterEach(function() {

//    });

//    it('load policy', function() {
//        expect(1).to.equal(1);
//    });

//    it('load policy data', function () {


//        //var dbstub = sinon.stub(MotorDB.DataService, null, "GetAllPolicys"). returns(fakeData);
//        //dbstub.GetAllPolicys.done(fakeData);
//        //sinon.stub($, "ajax").yieldsTo("done", fakeData);
//        var dbstub = new MotorDB.DataService();
//        var sut = new MotorDB.PolicyViewModel(dbstub);
//        sut.Load();
//        server.respond();
//        var data = sut.Policies();
//        //expect(sut.Policies().length).to.be(3);
//    });

//    //it('load 3', function() {
//    //    spyOn($, 'ajax').andCallFake(function (req) {
//    //        var d = $.Deferred();
//    //        d.resolve(fakeData);
//    //        return d.promise();
//    //    });
//    //    var dbstub = new MotorDB.DataService();
//    //    dbstub.GetAllPolicys();
//    //});
//})