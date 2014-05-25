module.exports = function(eventType, state) {
    var obj = {
        EventType: eventType,
        State: state 
    }

    return obj;
}
