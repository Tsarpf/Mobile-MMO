var area = function() {
    //var playersInArea = {};
    var playerPositions = {};
    var movingPlayers = {};
    var pub = {};
    var playerSpeed = 1;
    var emptyCells = [];
    var mStartingPosition;

    pub.getSetPlayerRoute = function(player, targetPos)
    {
        var startPos = playerPositions[player.getName()];
        var route = mGetRoute(startPos, targetPos);         

        //TODO: set player on route
        console.log("ssetting player on route unfinished. please finish me");
        playerPositions[player.getName()] = targetPos;
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
    pub.playerCanJoin = function(player)
    {
        return true;
    }
    pub.playerJoin = function(player)
    {
        playerPositions[player.getName()] = mStartingPosition;
    }
    pub.getPlayerPosition = function(user)
    {
        console.log("hello from getplayerposition in area: ");
        console.log(user);
        console.log(playerPositions);
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
            return playerPositions[user.getName()];
        }
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
        mStartingPosition = {x: 0, y: 0};
    }
    mConstruct();
    return pub;
}

module.exports = area;
