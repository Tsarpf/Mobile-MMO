var tls = require('tls'),
    fs = require('fs'),
   // path = require('path'),
   // passport = require('passport'),
   // LocalStrategy = require('passport-local').Strategy,
   // mongoose = require('mongoose');
   // userModel = require('./models/user');
   // userClass = require('./UserClass.js');

passport.use(userModel.createStrategy());

passport.serializeUser(userModel.serializeUser());
passport.deserializeUser(userModel.deserializeUser());

mongoose.connect('mongodb://localhost/test');


var loggedInUsers = {};
var areas = {}; //Todo implement loading from db
var options = {
    pfx: fs.readFileSync('tsarpf.pfx'),
};
var TLSServer = tls.createServer(options, clearTextServer);
var clearTextServer = function(cleartextStream) {
    var currenUser = {};
    var serverHandlers = {
        "Register": require('authHandlers.js')["Register"],
        "Login": require('authHandlers.js')["Login"],
        "Something": other
    }
    cleartextStream.on("error", function(err){
        //something
    }
    cleartextStream.on("data", function(data) {
        var receivedData;
        try {
            receivedData = JSON.parse(data);
        }
        catch(e) {
            var error = "Error parsing data: " + e;
            console.log(error);
            return;
        }

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
                    cleartextStream.write(responseData);
                }
            };
        }
    }
}

TLSServer.listen(8666, function() {
    console.log('listening');
});

/*
var closureHelper = function(handler, data, eventData){
    handler(eventData);
}
*/
