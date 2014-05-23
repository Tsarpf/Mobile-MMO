// requires the model with Passport-Local Mongoose plugged in
var path = require('path'),
    passport = require('passport'),
    LocalStrategy = require('passport-local').Strategy,
    http = require('http'),
    mongoose = require('mongoose'),
    express = require('express'),
    bodyParser = require('body-parser'),
    session = require('express-session'),
    //connect = require('connect'),
    MongoStore = require('connect-mongo')(session),
    cookieParser = require('cookie-parser'),
    io = require('socket.io'),
    cookie = require('cookie'),
    passportSocketIo = require('passport.socketio');

//var mongoStore = new MongoStore({db: "mongodb://localhost/test"});
var mongoStore = new MongoStore({db: "test"});

var app = express();

//app.configure(function() {
  app.use(express.static('public'));
  app.use(cookieParser());
  app.use(bodyParser());

  app.use(session({ store: mongoStore, secret: 'essi on ihana :3' }));

  app.use(passport.initialize());
  app.use(passport.session());
  //app.use(app.router);
  console.log("configured t. mä");
//});


var User = require('./models/user');

//passport.use(new LocalStrategy(User.authenticate()));
passport.use(User.createStrategy());

// use static serialize and deserialize of model for passport session support
passport.serializeUser(User.serializeUser());
passport.deserializeUser(User.deserializeUser());


mongoose.connect('mongodb://localhost/test');

var server = http.createServer(app)
server.listen(3002, 'datisbox.net', function() {
    console.log("server listening");
});


io = io.listen(server);
io.sockets.on('connection', function(socket){
    console.log("ses");
});

io.set('authorization', passportSocketIo.authorize({
    passport: passport,
    cookieParser: cookieParser,
    key: 'express.sid',
    secret: 'essi on ihana :3',
    store: mongoStore,
    success: ses,
    fail: notses
}));

io.set('register', function(req, res){
    console.log("got register");
    console.log("req: " + req);
    console.log("res: " + res);
});
function ses(data, accept){
    console.log('succesful connection to socket.io');
    accept(null, true);
}
function notses(data, message, error, accept){
    if(error)
        throw new Error(message);

    console.log(data);
    console.log('failed connection to socket.io:', message);
    accept(null,true);
    //accept(null,false);
};
/*
passport.use(new LocalStrategy(
  function(username, password, done) {
    User.findOne({ username: username }, function (err, user) {
      if (err) { return done(err); }
      if (!user) {
        return done(null, false, { message: 'Incorrect username.' });
      }
      if (!user.validPassword(password)) {
        return done(null, false, { message: 'Incorrect password.' });
      }
      return done(null, user);
    });
  }
));
*/

