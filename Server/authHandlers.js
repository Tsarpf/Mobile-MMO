var path = require('path'),
    passport = require('passport'),
    LocalStrategy = require('passport-local').Strategy,
    mongoose = require('mongoose');
    userModel = require('./models/user');
    userClass = require('./UserClass.js');

mongoose.connect('mongodb://localhost/test');

passport.serializeUser(userModel.serializeUser());
passport.deserializeUser(userModel.deserializeUser());

var loginHandler = function(eventData) {
    var username = eventData["Username"];
    var password = eventData["Password"];

    //Using passport.js which is usually used with express so we encapsulate the stuff into request body

    var req = {body: {username: username, password: password}};
    passport.authenticate('local', function(err, user, info) {
        if(!user) {
            console.log("login failed");
            return;
        }

        //currentUser accessable through closure
        currentUser = userClass(user); //Pass all data about user from db to userclass
        
    })(req, null, null);
}

var registerHandler = function(eventData) {
    var username = eventData["Username"];
    var password = eventData["Password"];
    var email = eventData["Email"];
    User.register(new User({username: usern}), passwd, function(err, user) {
       console.log(err);
       console.log("jebou");
    });
}

module.exports = {
    Login: loginHandler,
    Register: registerHandler
}
