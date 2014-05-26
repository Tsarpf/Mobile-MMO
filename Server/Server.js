var tls = require('tls'),
    fs = require('fs');
   // path = require('path'),
   // passport = require('passport'),
   // LocalStrategy = require('passport-local').Strategy,
   // mongoose = require('mongoose');
   // userModel = require('./models/user');
   // userClass = require('./UserClass.js');

//passport.use(userModel.createStrategy());

//passport.serializeUser(userModel.serializeUser());
//passport.deserializeUser(userModel.deserializeUser());

//mongoose.connect('mongodb://localhost/test');


var loggedInUsers = {};
var areas = {}; //Todo implement loading from db
var options = {
    pfx: fs.readFileSync('tsarpf.pfx'),
};
var clearTextServer = function(cleartextStream) {
    var currentUser = {};
    var serverHandlers = {
        "register": require('./authHandlers.js')["register"],
        "login": require('./authHandlers.js')["login"]
    }
    cleartextStream.on("error", function(err){
        //something
    });
    cleartextStream.on("data", function(data) {
        console.log('data: ' + data);
        var receivedData;
        try {
            receivedData = JSON.parse(data);
        }
        catch(e) {
            var error = "Error parsing data: " + e;
            console.log(error);
            return;
        }

        console.log('received stuff: ' + receivedData);
        var eventType = receivedData["eventType"];
        if(eventType in serverHandlers)
        {
            //save the closure thingy
            var eventData = receivedData;
            eventData.user = currentUser;
            eventData.areas = areas;
            eventData.users = loggedInUsers;

            serverHandlers[eventType](receivedData, function(responseData){
                if(responseData) {
                    console.log('writing back to client:')
                    console.log(responseData);
                    cleartextStream.write(responseData);
                }
            });
        }
    });
}
var TLSServer = tls.createServer(options, clearTextServer);

TLSServer.listen(8666, function() {
    console.log('listening');
});

/*
var closureHelper = function(handler, data, eventData){
    handler(eventData);
}
*/
