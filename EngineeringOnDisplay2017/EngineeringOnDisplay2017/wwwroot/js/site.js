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


        //add a canvas tag to DOM.  Attach a 





        
        //$("#ajaxStatus").html("Changed Status");


        //add rows to the output table
        //$("#outputTable").append("<tr><td>Apr 23</td><td>50.0</td><tr>");

       // $("#ajaxStatus").load('/Sensor/AjaxTest');

        //var graphs = $("canvas.graph");

        //alert(graphs.attr("data-graph"));

        var outputTable = $("#outputTable");

        $.getJSON('/Graph/GetAllGraphPoints', function (data) {

            $.each(data, function (i) {
                outputTable.append("<tr><td>" + data[i].x_Value + "</td><td>" + data[i].y_Value + "</td><tr>");
            });

        });



    });
}

//returns the lable for each dataset in the graph 
function getDatasetLable(canvasTag) { }

//returns the color for the fill area under the line
function getBackgroundColor(canvasTag) { }

function getBorderColor(canvasTag) { }

function getYAxisLable(canvasTag) { }


//Ajax request to Graph controller for the points corrosponding to the graph.  
//returns an array of graph points
function getGraphPoints(canvasTag) {


}

//draw a graph for given canvas tag
function drawGraphForCanvas(canvasTag) {


    var myChart = new Chart(ctx, {
        type: 'line',
        data:
        {
            labels: getGraphPoints(),

            datasets:
            [
                {
                    label: "Electrical Demand",
                    data: [],
                    lineTension: 0,
                    backgroundColor: "rgba(255,255,0,1)",
                    borderColor: "#ffc000"

                }
            ]
        },
        options:
        {
            scales:
            {
                yAxes:
                [{
                    scaleLabel:
                    {
                        display: true,
                        labelString: 'kiloWatts'
                    }
                }],
                xAxes:
                [{
                    ticks:
                    {
                        maxTicksLimit: 10
                    }
                }]
            }
        }
    });
}




