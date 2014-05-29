var tls = require('tls'),
    fs = require('fs'),
    resObject = require('genericResponseObject.js');
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
areas["test"] = require('./area.js');
var options = {
    pfx: fs.readFileSync('tsarpf.pfx'),
};
var clearTextServer = function(cleartextStream) {
    var currentUser = {};
    var serverHandlers = {
        "registerRequest": require('./authHandlers.js')["register"],
        "loginRequest": require('./authHandlers.js')["login"],
        "moveRequest": require('./moveHandler.js'),
        "joinAreaRequest": require(./joinAreaHandler.js')
    }
    cleartextStream.on("error", function(err){
        //something
        console.log(currentUser.getName() + " errored:");
        console.log(err);
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
        var eventType = receivedData["eventtype"];
        if(eventType in serverHandlers)
        {
            //save the closure thingy
            var eventData = receivedData;
            eventData.user = currentUser;
            eventData.areas = areas;
            eventData.users = loggedInUsers;

            console.log("dispatching on next tick: " + eventType);
            process.nextTick(
                serverHandlers[eventType](receivedData, function(responseData){
                    if(responseData) {
                        console.log(loggedInUsers);
                        console.log('writing back to client:')
                        console.log(responseData);
                        send(responseData);
                    }
                });
            );
        }
        else
        {
            send(resObject("info", "No such event"));
        }
    });
    var send = function(data, callback)
    {
        cleartextStream.write(data, function() {
            console.log("written");
            if(callback) {callback();};
        });
    }
}
var TLSServer = tls.createServer(options, clearTextServer);

TLSServer.listen(8666, function() {
    console.log('listening');
});
