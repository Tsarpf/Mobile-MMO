module.exports = function(eventType, state) {
    var obj = {
        type: eventType,
        state: state 
    }

    return obj;
}
