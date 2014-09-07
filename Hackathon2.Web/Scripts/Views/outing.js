"use strict";

function log(message) {
    $("<div/>").text(message).appendTo("#log");
    $("#log").attr("scrollTop", 0);
}

///(function(){


var stopIndex = 0;

$(document).ready(function () {

    $("#location").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.getJSON("/Account/AvailableLocations", {
                term: $("#location").val()
            }, response);
        },
        select: function (event, ui) {
            if (ui.item)
            {
                log("Stop: " + ui.item.value);
                
                var stop = $("<input>").attr({
                    type: "hidden",
                    name: "Stops[" + stopIndex++ + "]",
                    value: ui.item.value
                });


                $("#puppy").append(stop);

                $("#location").val("");


        

                 
                //$("input[name^='Stops']").each(function () {

                //    log("Stop: " + $(this).val() + "yoyo");

                //});
            }

           
        }


    });
});
