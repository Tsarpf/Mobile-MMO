module.exports = function(eventType, properties) {
    var obj = {
        type: eventType,
        properties: properties 
    }

    return obj;
}
