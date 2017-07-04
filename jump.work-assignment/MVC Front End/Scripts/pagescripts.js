$(document).ready(function () {

    $("#skillText").tagsinput({
        //itemValue: "value", 
        //itemText: "text", 
        typeaheadjs: {
            source: ["winter-sports", "eSports"]
        }
    });
});