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

passport.use(User.createStrategy());

passport.serializeUser(User.serializeUser());
passport.deserializeUser(User.deserializeUser());

mongoose.connect('mongodb://localhost/test');

var server = tls.createServer(options, function(cleartextStream) {
    //cleartextStream.pipe(cleartextStream);
    cleartextStream.write("sup", function()
    {
        console.log("sup written");
    });
  //  if(cleartextStream.authorized)
  //  {
  //      console.log("authorized");
  //      cleartextStream.write("welcome!\n");
  //      cleartextStream.setEncoding('utf8');
  //  }
  //  else if (cleartextStream.authorized == false)
  //  {
  //      console.log("unauthorized");
  //      cleartextStream.write("welcome!\n");
  //      //console.log(cleartextStream);
  //      console.log(cleartextStream.authorizationError);
  //  }
  //  else
  //  {
  //      console.log("cleartextStream.authorized: " + cleartextSTream.authorized);
  //  }
    cleartextStream.on("data", function(data){
        var dstring = data.toString();
        if(dstring.indexOf("register") === 0)
        {
           var usern = dstring.split(' ')[1]; 
           var passwd = dstring.split(' ')[2]; 
           User.register(new User({username: usern}), passwd, function(err, user) {
               console.log(err);
               console.log("jebou");
           });
        }
        else if(dstring.indexOf("login") === 0)
        {
            console.log("got to login");
            var usern = dstring.split(' ')[1];
            var passwd = dstring.split(' ')[2];
            var bdy = {username: usern, password: passwd};
            var req = {body: bdy};
            var next = function (req, res, next)
            {
                console.log("derpa herp");
                console.log(req);
                console.log(res);
                console.log(next);
            };
            var res = next;
            var derp = passport.authenticate('local', function(err, user, info){
                console.log("failed?");
                console.log(err);
                console.log(info);
                console.log("authenticated user: " + user);
            })(req, res, next);
            console.log("derp: " + derp);

            
        }
        else
        {
            console.log("got data: " + data.toString())
        }
        //console.log("got data");
        //console.log(data.toString());
        //cleartextStream.write("welcome!\n");
    });
    //cleartextStream.pipe(cleartextStream);
});

server.listen(8666, function() {
    console.log('listening');
});
