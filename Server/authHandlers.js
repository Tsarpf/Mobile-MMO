var path = require('path'),
    passport = require('passport'),
    LocalStrategy = require('passport-local').Strategy,
    mongoose = require('mongoose');
    userModel = require('./models/user');
    userClass = require('./UserClass.js');
    resObject = require('./genericResponseObject.js');

mongoose.connect('mongodb://localhost/test');

passport.serializeUser(userModel.serializeUser());
passport.deserializeUser(userModel.deserializeUser());

var loginHandler = function(eventData, callback) {
    var username = eventData["Username"];
    var password = eventData["Password"];

    //Using passport.js which is usually used with express so we encapsulate the stuff into request body (a bit ugly)
    var req = {body: {username: username, password: password}};
    passport.authenticate('local', function(err, user, info) {
        if(!user) {
            console.log("login failed");
            var msg = resObject("Login", "Rejected");
            callback(msg);
            return;
        }

        //Saves user to logged in users and update server session user info thingy
        var userClassed = userClass(user);
        eventData["User"](userClassed);

        var msg = resObject("Login", "Accepted");
        if(callback) {callback(msg);};
        
    })(req, null, null);
}

var registerHandler = function(eventData, callback) {

    var username = eventData["Username"];
    var password = eventData["Password"];
    var email = eventData["Email"];
    userModel.register(new userModel({username: username}), password, email, function(err, user) {
        if(err){
            console.log("Error in registration: " + err);
            var msg = resObject("Register", "Rejected"); 
            callback(msg);
            return;
        }
        console.log(username + " registered succesfully");
        var msg = resObject("Register", "Accepted");
        if(callback) {callback(msg);};
    });
}


module.exports = {
    Login: loginHandler,
    Register: registerHandler
}
