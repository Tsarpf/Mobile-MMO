

var user = function(cleartextStream) {
    var mStream = cleartextStream;
    var mSessionHandlers = {};
    //var mUser = properties["User"]; 
    var pub = {};
    pub.GetHandlers = function()
    {
        var list = [];
        list.push();
        return list;
    }
    pub.Teardown = function()
    {
        mTeardown();
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
        console.log("error: " + err["code"]);
        mTeardown();
    }
    var mSetUp = function()
    {
        mSessionHandlers = {
            "Login": mLoginHandler,
            "Register": mRegisterHandler
        }
        mStream.on("error", mStreamErrorHandler);
    }

    mSetUp();
    return pub;
}

module.exports = user 
