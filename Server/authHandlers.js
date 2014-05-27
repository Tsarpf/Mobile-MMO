var path = require('path'),
    passport = require('passport'),
    LocalStrategy = require('passport-local').Strategy,
    mongoose = require('mongoose');
    userModel = require('./models/user');
    userClass = require('./UserClass.js');
    resObject = require('./genericResponseObject.js');

mongoose.connect('mongodb://localhost/test');

passport.use(userModel.createStrategy());
passport.serializeUser(userModel.serializeUser());
passport.deserializeUser(userModel.deserializeUser());

var loginHandler = function(eventData, callback) {
    var username = eventData["username"];
    var password = eventData["password"];

    //Using passport.js which is usually used with express so we encapsulate the stuff into request body (a bit ugly)
    var req = {body: {username: username, password: password}};
    passport.authenticate('local', function(err, user, info) {
        if(!user) {
            console.log("login failed");
            var msg = resObject("login", "rejected");
            callback(msg);
            return;
        }

        //Saves user to logged in users and update server session user info thingy
        var userClassed = userClass(user);
        eventData.user = userClassed;
        eventData.users[userClassed.getUsername()] = userClassed;

        var msg = resObject("login", "accepted");
        if(callback) {callback(msg);};
        
    })(req, null, null);
}

var registerHandler = function(eventData, callback) {

    var username = eventData["username"];
    var password = eventData["password"];
    var email = eventData["email"];
    userModel.register(new userModel({username: username}), password, email, function(err, user) {
        if(err){
            console.log("Error in registration: " + err);
            var msg = resObject("register", "rejected"); 
            callback(msg);
            return;
        }
        console.log(username + " registered succesfully");
        var msg = resObject("register", "accepted");
        if(callback) {callback(msg);};
    });
}


module.exports = {
    login: loginHandler,
    register: registerHandler
}
