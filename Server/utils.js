var replaceProperties = function(oldObject, newObject) {
    for(var key in oldObject)
    {
        if(oldObject.hasOwnProperty(key)){
            delete oldObject[key];
        }
    }

    for(var key in newObject)
    {
        if(newObject.hasOwnProperty(key)) {
            oldObject[key] = newObject[key];
        }
    }
}

exports.replaceProperties = replaceProperties;
