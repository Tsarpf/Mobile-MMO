var resObject = require('./genericResponseObject.js');

var user = function(userModel, stream) {
    var mUser = userModel;
    var mStream = stream;
    var mName = mUser.username;
    var mCurrentArea = null;
    var mMoving = false;
    var pub = {};
    pub.getHandlers = function()
    {
        var list = [];
        list.push();
        return list;
    }
    pub.teardown = function()
    {
        mTeardown();
    }
    pub.setCurrentArea = function(area)
    {
        mCurrentArea = area;
    }
    pub.getCurrentArea = function()
    {
        return mCurrentArea;
    }
    pub.getCurrentPosition = function()
    {
        return mCurrentArea.getPlayerPosition(this);
    }
    pub.getCurrentArea = function()
    {
        return mCurrentArea;        
    }
    pub.getUsername = function()
    {
        return mName;
    }
    pub.getName = function()
    {
        return mName;
    }
    pub.getDescriptionObject = function()
    {
        var robject = {};
        robject.username = mName;
        robject.position = pub.getCurrentPosition();
        //add more when needed.
        return robject;
    }
    pub.send = function(msg)
    {
        var message = JSON.stringify(msg);
        mStream.write(message, function() {
            console.log("wrote to " + mName + ": ");
            console.log(message);
        });
    }
    pub.closeStream = function()
    {
        mStream.destroy();
    }

    var mTeardown = function()
    {

    }
    
    var mConstruct = function()
    {
    }

    mConstruct();
    return pub;
}

module.exports = user;
