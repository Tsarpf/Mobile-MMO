var tls = require('tls');
var fs = require('fs');

var options = {
    pfx: fs.readFileSync('tsarpf.pfx'),
};

 var path = require('path'),
     passport = require('passport'),
     LocalStrategy = require('passport-local').Strategy,
     http = require('http');
     mongoose = require('mongoose');
     //express = require('express'),
     //bodyParser = require('body-parser'),
     //session = require('express-session'),
     //connect = require('connect'),
     //MongoStore = require('connect-mongo')(session),
     //Schema = mongoose.Schema,
     //passportLocalMongoose = require('passport-local-mongoose');
     //cookieParser = require('cookie-parser'),
     //io = require('socket.io'),
     //cookie = require('cookie'),
     //passportSocketIo = require('passport.socketio');


var User = require('./models/user');
var UserClass = require('./UserClass.js');

passport.use(User.createStrategy());

passport.serializeUser(User.serializeUser());
passport.deserializeUser(User.deserializeUser());

mongoose.connect('mongodb://localhost/test');



var server = tls.createServer(options, function(cleartextStream) {
    var userSession;
    //Log in
    var loginHandler = function(eventData, sessionHandlers){ 
        console.log("got to login");

        var username = eventData["Username"];
        var password = eventData["Password"];
        var body = {username: username, password: password};
        var req = {body: body};
        var next = function (req, res, next){console.log("something called next or res?"); };
        var res = next;
        var derp = passport.authenticate('local', function(err, user, info){
            if(!user)
            {
                console.log("failed?");
                console.log(err);
                console.log(info);
                console.log("authenticated user: " + user);
            }
            else
            {
                console.log("success, user: " + user);
                var props = {User: user};
                userSession = UserClass(props);
            }
        })(req, res, next);
    }
    //Register
    var registerHandler = function(eventData, sessionHandlers){
        var username = eventData["Username"];
        var password = eventData["Password"];
        var email = eventData["Email"];
        User.register(new User({username: usern}), passwd, function(err, user) {
           console.log(err);
           console.log("jebou");
        });
    }
    //Initial list of handlers
    var sessionHandlers = {
        "Login": loginHandler,
        "Register": registerHandler
    }
    cleartextStream.on("error", function(err) {
        console.log("error");
        console.log(err["code"]);
        console.log(userSession);
        //if(err[
    });
    cleartextStream.on("data", function(data){
        try {
            var receivedData = JSON.parse(data);
        }
        catch (e)
        {
            var error = "Error parsing sent data: " + e;
            console.log(error);
            cleartextStream.write(error);
            return;
        }

        var eventType = receivedData["EventType"];
        if(eventType in sessionHandlers)
        {
            console.log("received data: " + receivedData);
            sessionHandlers[eventType](receivedData, sessionHandlers);
        }
        else
        {
            console.log("Alert: tried to use an illegal eventhandler");
        }













       // var dstring = data.toString();
       // if(dstring.indexOf("register") === 0)
       // {
       // }
       // else if(dstring.indexOf("login") === 0)
       // {
       //     console.log("got to login");
       //     var usern = dstring.split(' ')[1];
       //     var passwd = dstring.split(' ')[2];
       //     var bdy = {username: usern, password: passwd};
       //     var req = {body: bdy};
       //     var next = function (req, res, next)
       //     {
       //         console.log("something called next?");
       //     };
       //     var res = next;
       //     var derp = passport.authenticate('local', function(err, user, info){
       //         if(!user)
       //         {
       //             console.log("failed?");
       //             console.log(err);
       //             console.log(info);
       //             console.log("authenticated user: " + user);
       //         }
       //         else
       //         {
       //             var props = {User: user};
       //             var userSession = UserClass(props);
       //         }
       //     })(req, res, next);
       //     console.log("derp: " + derp);
       // }
       // else
       // {
       //     console.log("got data: " + data.toString())
       // }
    });
    //cleartextStream.pipe(cleartextStream);
});

server.listen(8666, function() {
    console.log('listening');
});
