// Site.js

/**
* Run setup functions once DOM has loaded
*
**/
$("document").ready(function()
{
   
    setupButtonEvents();      

});


//setup the buttons on the page
function setupButtonEvents() {
    var clickCount = 0;
    

    $("#testBtnOne").click(function () {
        
        //$("#ajaxStatus").html("Changed Status");


        //add rows to the output table
        //$("#outputTable").append("<tr><td>Apr 23</td><td>50.0</td><tr>");

       // $("#ajaxStatus").load('/Sensor/AjaxTest');

        var outputTable = $("#outputTable");

        $.getJSON('/Graph/GetAllGraphPoints', function (data) {

            $.each(data, function (i) {
                outputTable.append("<tr><td>" + data[i].x_Value + "</td><td>" + data[i].y_Value + "</td><tr>");
            });

        });


    });

}


