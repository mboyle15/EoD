// Site.js
/**
* Run setup functions once DOM has loaded
**/
$("document").ready(function () {


    if ($("#main").text() === "")
    { 
        //first time loading the page then load homepage
        loadContent("Home");
    }

    setupBtnsSensorSelect();
    //setupBtnsSensorData();
    //getGraphPoints($("#chartArea div canvas"));
});


//load the sensor home content
function loadContent(content) {

    $.get("/Main/LoadContent", { name: content },function (data) {
        $("#main").html(data);
    }, "html");
}

//setup the sensor select buttons (Electrical, Water, Naturalgas, OutsideTemperature))
function setupBtnsSensorSelect() {

    //add a click action listener to the select buttons
    $("#sidebar button.sensorNavBtn").click(function () {
        var button = $(this);

        //check if this is already the current page loaded 
        if (!button.hasClass("current")) { //if not then
            loadContent(button.attr("data-graph-sensor"));  //load new page
            $("#sidebar button.current").removeClass("current"); //remove current from the previous button
            button.addClass("current"); //add current to the button pressed
        }
    });
}

//change the the div with chartData iff 
function changeChartData(attribute, value) {
    var chartData = $("#chartData");

    if (chartData.attr(attribute) !== value) {
        chartData.attr(attribute, value);
        getGraphPoints(); //
    }
}

//setup the sensor data change buttons (Amount, Change) 
function setupBtnsSensorData(){
    //select the sensor data buttons
    $("#sensorDataBtns button").click(function () {
        changeChartData("data-graph-data", $(this).attr("data-graph-data"));
    });
}


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


//Ajax request to Graph controller for the points corrosponding to the graph.  
//returns an array of graph points
function getGraphPoints() {

    var chartDataDiv = $("#chartData");

    $.getJSON('/Graph/GetGraphPoints',
        {
            start: chartDataDiv.attr('data-graph-start'),
            end: chartDataDiv.attr("data-graph-end"),
            sensor: chartDataDiv.attr("data-graph-sensor"),
            dataType: chartDataDiv.attr("data-graph-data"),
            scale: chartDataDiv.attr("data-graph-scale")
        },
        function (data) {
            chartProperties.xAxis = data.xAxis;
            chartProperties.yAxis = data.yAxis;
            drawGraphForCanvas();
        });
}

//draw a graph for given canvas tag
function drawGraphForCanvas() {


    chartProperties.globalChart = new Chart($("#sensorChartHere"), {
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
                }]
            }
        }
    });
}


