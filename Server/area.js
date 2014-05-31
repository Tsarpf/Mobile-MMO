var resObject = require('./genericResponseObject.js');
var area = function() {
    //var playersInArea = {};
    var players = {};
    var movingPlayers = {};
    var pub = {};
    var playerSpeed = 1;
    var inaccessableCells = [];
    var mPassword = null;

    var mStartingPosition = {x: 0, y: 0};
    var mDimensions = {x: 30, y: 20};

    pub.getSetPlayerRoute = function(player, targetPos)
    {
        var startPos = players[player.getName()].position;
        var route = mGetRoute(startPos, targetPos);         

        //TODO: set player on route
        console.log("ssetting player on route unfinished. please finish me");
        players[player.getName()].position = targetPos;
        return route;
    }
    pub.isReachable = function(startPos, targetPos)
    {
        console.log("reachability stuff unfinished. please finish me");
        return true;
    }
    pub.getStartingPosition = function()
    {
        return mStartingPosition;
    }
    pub.setStartingPosition = function(pos)
    {
        mStartingPosition = pos;
    }
    pub.isPasswordProtected = function()
    {
        return false;
    }
    pub.getDescriptionObject = function()
    {
        var robject = {};
        robject.startingPosition = mStartingPosition;
        robject.dimensions = mDimensions;
        var playersData = [];
        for(var playerName in players)
        {
            if(playerName in movingPlayers)
            {
                //handle in some different way
            }
            else
            {
                var playerData = players[playerName].instance.getDescriptionObject();
                playersData.push(playerData);
            }
        }

        robject.playersData = playersData;
        
        return robject;
    }
    pub.sendAll = function(msg)
    {
        for(var player in players)
        {
            players[player].instance.send(msg);
        }
    }
    pub.join = function(player, password)
    {
        //placeholder. ugly. any better ideas?
        var cannotJoinForSomeReason = !mPlayerCanJoin(player);
        if(cannotJoinForSomeReason) return "reason is:";

        if(!mPassword)
        {
            password = null;
        }

        if(password !== mPassword)
        {
            return "invalid password";
        }

        var msg = {startingPosition: mStartingPosition, username: player.getName()};
        var msgObj = resObject("remotePlayerJoinEvent", msg);
        for(var plr in players)
        {
            if(plr != player.getName())
            {
                //Tad ugly for this to be here
                players[plr].send(msgObj);
            }
        }

        player.setCurrentArea(this);
        players[player.getName()] = {
            position: mStartingPosition,
            instance: player
        };
    }
    pub.getPlayerPosition = function(user)
    {
        if(user.getName() in movingPlayers)
        {
            //TODO: time stuff. stuff like start time won't be set here, but is here now for .. saving the thought?
            //var routeLength = mGetRouteLength(route);
            //var timeEstimate = routeLength / playerSpeed;
            //var startTime = datetime.now();
            //var endtime = startTime + timeEstimate;
            //var timeSpent = currentTime - startTime
            //var progress = timeSpent / timeEstimate;
        }
        else
        {
            return players[user.getName()].position;
        }
    }
    var mPlayerCanJoin = function(player)
    {
        return true;
    }
    var mGetRoute = function(startPos, targetPos)
    {
        console.log("finding player route unfinished. please finish me");
        return [targetPos];
    }
    var mGetRouteLength = function(route)
    {
        var prev = route[0];
        var estimate = 0;
        for(var i = 1; i < route.length; i++)
        {
            var vector = {};
            vector.x = prev.x - route[i].x;
            vector.y = prev.y - route[i].y;
            estimate += mGetVectorMagnitude(vector);
        }

        return estimate;
    }

    var mGetVectorMagnitude = function(vector)
    {
        return Math.sqrt(Math.pow(vector.x,2) + Math.pow(vector.y,2));
    }
    var mConstruct = function()
    {
        
    }
    mConstruct();
    return pub;
}

module.exports = area;
