var resObject = require('./genericResponseObject.js');
var areaChatHandler = function(eventData, callback) {
    var user = eventData.user;
    var area = user.getCurrentArea();
    if(!area) {
        callback(resObject("info", "User is not in any area, so cannot send a message to one"));
        return;
    }


    var message = eventData.message;
    area.sendAll(resObject("areaChatEvent", message));
}

module.exports.areaChat = areaChatHandler;

