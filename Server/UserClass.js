

var user = function(userModel) {
    var mUser = userModel;
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
    pub.enterArea = function(area)
    {
        mCurrentArea = area;
        mCurrentPosition = area.getStartingPosition();
        
    }
    pub.getCurrentPosition = function()
    {
        //Todo: if moving, calculate approximate progress to destination and give that as the current position
        return mCurrentArea.getPlayerPosition(mUser);
    }
    pub.getUsername = function()
    {
        return mName;
    }
    pub.getName = function()
    {
        return mName;
    }
    pub.getCurrentArea = function()
    {
        return mCurrentArea;
    }

    var mTeardown = function()
    {

    }
    
    var mLoginHandler = function()
    {
    }
    var mRegisterHandler = function()
    {
    }
    var mStreamErrorHandler = function(err)
    {
    }
    var mSetUp = function()
    {
    }

    mSetUp();
    return pub;
}

module.exports = user;
