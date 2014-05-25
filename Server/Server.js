var tls = require('tls'),
    fs = require('fs'),
    path = require('path'),
    passport = require('passport'),
    LocalStrategy = require('passport-local').Strategy,
    mongoose = require('mongoose');
    userModel = require('./models/user');
    userClass = require('./UserClass.js');

passport.use(userModel.createStrategy());

passport.serializeUser(userModel.serializeUser());
passport.deserializeUser(userModel.deserializeUser());

mongoose.connect('mongodb://localhost/test');


var loggedInUsers = {};
var areas = {}; //Todo implement loading from db
var TLSServer = tls.createServer(options, clearTextServer);
var clearTextServer = function(cleartextStream) {
    var currenUser = {};
    var loginClosure = function(currentUser) {
        var authHandlers = require('authHandlers.js');
        return authHandlers["Login"];
    }

    var authClosure = {
        "Register": function(currentUser){
            return require('authHandlers.js')["Register"];
        },
        "Login": function(currentUser){
            return require('authHandlers.js')["Login"];
        }
    }
    var serverHandlers = {
        "Register": authClosure["Register"](currentUser),
        "Login": authClosure["Login"](currentUser),
        "Something": other
    }
    cleartextStream.on("error", function(err){
        //something
    }
    cleartextStream.on("data", function(data) {
        //jotain
    }
}
