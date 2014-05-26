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
        "Register": require('./authHandlers.js')["Register"],
        "Login": require('./authHandlers.js')["Login"]
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
        var eventType = receivedData["EventType"];
        if(eventType in serverHandlers)
        {
            //'tis a bit ugly workaround
            var logInClosure = function(currentUser, users) {
                return function(user) {
                    currentUser = user;
                    users[currentUser["username"]] = currentUser;
                }
            }
            //save the closure thingy
            receivedData["User"] = logInClosure(currentUser, loggedInUsers);

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
