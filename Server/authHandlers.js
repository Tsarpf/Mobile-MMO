var path = require('path'),
    passport = require('passport'),
    LocalStrategy = require('passport-local').Strategy,
    mongoose = require('mongoose'),
    userModel = require('./models/user'),
    userClass = require('./UserClass.js'),
    resObject = require('./genericResponseObject.js'),
    replaceProperties = require('./utils.js').replaceProperties

mongoose.connect('mongodb://localhost/test');

passport.use(userModel.createStrategy());
passport.serializeUser(userModel.serializeUser());
passport.deserializeUser(userModel.deserializeUser());

var loginHandler = function(eventData, serverHandlers, postLoginHandlers, cleartextStream, callback) {
    var username = eventData["username"];
    var password = eventData["password"];

    //Using passport.js which is usually used with express so we encapsulate the stuff into request body (a bit ugly)
    var req = {body: {username: username, password: password}};
    passport.authenticate('local', function(err, user, info) {
        if(!user) {
            var msg = resObject("loginEvent", "rejected");
            callback(msg);
            return;
        }

        replaceProperties(serverHandlers, postLoginHandlers);

        //Saves user to logged in users and update server session user info thingy
        var userClassed = userClass(user, cleartextStream);
        //eventData.user = userReference;
        replaceProperties(eventData.user, userClassed);
        if(userClassed.getUsername() in eventData.users)
        {
            eventData.users[userClassed.getUsername()].send(resObject("info", "Logged in from another session. Closing this one"));
            eventData.users[userClassed.getUsername()].closeStream();
            delete eventData.users[userClassed.getUsername()];
        }
        eventData.users[userClassed.getUsername()] = userClassed;
        //replaceProperties(eventData.users[

        var msg = resObject("loginEvent", "accepted");
        if(callback) {callback(msg);};
        
    })(req, null, null);
}

var registerHandler = function(eventData, callback) {

    var username = eventData["username"];
    var password = eventData["password"];
    var email = eventData["email"];
    userModel.register(new userModel({username: username, email: email}), password, email, function(err, user) {
        if(err){
            console.log("Error in registration: " + err);
            var msg = resObject("registerEvent", "rejected"); 
            callback(msg);
            return;
        }
        console.log(username + " registered succesfully");
        var msg = resObject("registerEvent", "accepted");
        if(callback) {callback(msg);};
    });
}


module.exports = {
    login: loginHandler,
    register: registerHandler
}
