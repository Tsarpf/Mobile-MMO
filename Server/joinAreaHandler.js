var resObject = require('./genericResponseObject.js');
var joinAreaHandler = function(eventData, callback) {
    var user = eventData.user;
    var areas = eventData.areas;

    var areaId = eventData["areaId"];
    var areaPassword = null;
    if("password" in eventData)
    {
        if(eventData["password"] != "")
        {
            areaPassword = eventData["password"];
        }   
    }

    var area = areas[areaId]; 

    var error = area.join(user, areaPassword);
    if(error)
    {
        return callback(resObject("info", "Cannot join room, reason: " + error));
    }


    var areaDesc = area.getDescriptionObject();
    //for(var i = 0; i < areaDesc.playersData.length; i++)
    //{
    //    if(areaDesc.playersData[i].username === user.getName())
    //    {
    //        areaDesc.playersData.splice(i,1);
    //        break;
    //    }
    //}
    

     

    return callback(resObject("joinAreaEvent", areaDesc));
}

module.exports = joinAreaHandler;
