// Site.js

/**
* Run setup functions once DOM has loaded
*
**/
$("document").ready(function()
{

    //first time loading the page then load homepage
    loadContent("Home");


    //setupBtnsSensorSelect();
    //setupBtnsSensorData();
    //getGraphPoints($("#chartArea div canvas"));
});


//load the sensor home content
function loadContent(content) {

    $.get("/Main/LoadContent", { name: content },function (data) {
        $("#main").html(data);
    }, "html");
}

//change the canvas to desired attribute. Will only change if the attribute is different.
function changeCanvas(canvas, attribute, value) {
    $(canvas).each(function () {
        var canvasToChange = $(this); //cache the wrapped jquery varable
        if (canvasToChange.attr(attribute) !== value) { //checkt to see if it empty
            canvasToChange.attr(attribute, value); //set the attribute in the canvas
            reskinChartInterface(attribute); //change all the buttons to match the graph
            getGraphPoints(canvasToChange);
        }
    });
}

//setup the sensor select buttons (Electrical, Water, Naturalgas, OutsideTemperature))
function setupBtnsSensorSelect() {

    //add a click action listener to the select buttons
    $("#sidebar button.sensorNavBtn").click(function () {
        
    });
}


//setup the sensor data change buttons (Amount, Change) 
function setupBtnsSensorData(){
    //select the sensor data buttons
    $("#chartArea div.chartDataSelectbtn button").click(function () {
        changeCanvas($("#chartArea div canvas"), "data-graph-data", $(this).attr("data-graph-data"));
    });
}


//changes the buttons for units, 
function reskinChartInterface(sensorType)
{
   // alert(sensorType);
}


////returns the lable for each dataset in the graph 
//function getDatasetLable(canvasTag) {
//    var canvas = $(canvasTag);
//    var sensor = canvas.attr("data-graph-sensor"); 
//    var dataType = canvas.attr("data-graph-data");
//    var scale = canvas.attr("data-graph-scale");
 
    
//    //return a string like Electrical Demand kilowatt hours
//}

//setup a global chart object to handel the chart properties
var chartProperties = {
    datasetLable: "Electrical Usage",
    backgroundColor: "rgba(255,255,0,.5)",
    borderColor: "rgba(255,255,0,1)",
    labelString: "kiloWatt Hours",
    xAxis: [],
    yAxis: [],
    globalChart: null
}; 


//returns the color for the fill area under the line
function getBackgroundColor(canvasTag) { }

function getBorderColor(canvasTag) { }

function getYAxisLable(canvasTag) { }


//Ajax request to Graph controller for the points corrosponding to the graph.  
//returns an array of graph points
function getGraphPoints(canvasTag) {

    var $canvasTag = $(canvasTag);
    var testTable = $("#outputTable");

    $.getJSON('/Graph/GetGraphPoints',
        {

            start: $canvasTag.attr('data-graph-start'),
            end: $canvasTag.attr("data-graph-end"),
            sensor: $canvasTag.attr("data-graph-sensor"),
            dataType: $canvasTag.attr("data-graph-data"),
            scale: $canvasTag.attr("data-graph-scale")
        },
        function (data) {

            chartProperties.xAxis = data.xAxis;
            chartProperties.yAxis = data.yAxis;
            drawGraphForCanvas(canvasTag);
        });
}

//draw a graph for given canvas tag
function drawGraphForCanvas(canvasTag) {

    //alert(chartProperties.xAxis.length);

 

    if (chartProperties.globalChart !== undefined && chartProperties.globalChart !== null) {
        chartProperties.globalChart.destroy();
    }

    chartProperties.globalChart = new Chart(canvasTag, {
        type: 'line',
        data:
        {
            labels: chartProperties.xAxis,

            datasets:
            [
                {
                    label: chartProperties.datasetLable,
                    data: chartProperties.yAxis,
                    lineTension: 0,
                    backgroundColor: chartProperties.backgroundColor,
                    borderColor: chartProperties.borderColor

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
                        labelString: chartProperties.labelString
                    }
                }],
                xAxes:
                [{
                    ticks:
                    {
                        maxTicksLimit: 10,
                        callback: function (value) {
                            return new Date(value).toLocaleDateString('de-DE', { month: 'short', year: 'numeric' });
                        }
                    }
                    //type: 'time',
                    //unit: 'day',
                    ////unitStepSize: 1,
                    //time: {
                    //    displayFormats: {
                    //        day: 'MMM DD'
                    //    }
                    //}
                }]
            }
        }
    });
    chartProperties.globalChart.resize(1000, 500);
    chartProperties.globalChart.resize();

}


