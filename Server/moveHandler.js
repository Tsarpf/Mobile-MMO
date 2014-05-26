var moveRequestHandler = function(eventData, callback) {
    var user = eventData.user;
    var area = user.getCurrentArea();
    if(!area) {
        callback(resObject("info", "User is not in any area, so cannot move"));
        return;
    }


    var startPos = user.getCurrentPosition();
    var targetPos = eventData.to; 
    if(!area.isReachable(startPos, targetPos))
    {
        callback(resObject("info", "Target position not reachable from start position"));
        return;
    }

    //Todo: other checks? f.ex if two players are trying to move to the same empty cell, should handle in some way
    
    var route = area.getSetPlayerRoute(user, targetPos);

    var moveEvent = {};

    moveEvent.user = user.getName();
    moveEvent.from = startPos;
    moveEvent.to = route;

    callback(moveEvent);    
}

module.exports = moveRequestHandler;
