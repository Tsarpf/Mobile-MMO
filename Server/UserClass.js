

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
        if(area.playerCanJoin(this))
        {
            area.playerJoin(this);
            mCurrentArea = area;
        }
    }
    pub.getCurrentPosition = function()
    {
        //Todo: if moving, calculate approximate progress to destination and give that as the current position
        //return mCurrentArea.getPlayerPosition(mUser);
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
    var mConstruct = function()
    {
        pub.enterArea(require("./area.js")());
        console.log("joining default area");
        console.log(pub.getCurrentPosition());
    }

    mConstruct();
    return pub;
}

module.exports = user;
