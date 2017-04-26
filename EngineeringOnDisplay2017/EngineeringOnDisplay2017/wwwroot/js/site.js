// Site.js
/**
* Run setup functions once DOM has loaded
**/
$("document").ready(function () {


    if ($("#main").text() === "")
    { 
        //first time loading the page then load homepage
        loadContent("Home");
        setupBtnsSensorSelect();

        
    }
});

//load the sensor home content
function loadContent(content) {

    $.get("/Main/LoadContent", { name: content },function (data) {
        $("#main").html(data);


        //check if it is the home page which does not need all the chart buttons
        if(content !== "Home")
        {
            
           
            try {
                chartProperties = JSON.parse($("#chartProperties").attr("data-chart-prop"));
            }
            catch (e) {
                //set a default chartProperties string
                chartProperties = {
                    datasetLabel: "Default Dataset - error with Parse",
                    backgroundColor: "rgba(255,0,0,1)",
                    borderColor: "rgba(255,0,0,1)",
                    labelString: "Default",
                    xAxis: [],
                    yAxis: [],
                    globalChart: null
                };
            }

            //must be a chart page.  set up charting buttons
            setupBtnsSensorData();  //setup the demand and usage buttons
            setupBtnsSensorScale(); //setup the scale buttons
            setupBtnsSensorScroll(); //setup the scroll buttons
            updateChart(); //draw the chart first time
        }


    }, "html");
    
}


//Load the full version of the slide show to the right
function loadSlideShowFull() {

}

function loadSlideShowNavBar() {

    //var navbar = $(#)

}

//setup the sensor select buttons (Electrical, Water, Naturalgas, OutsideTemperature)) , page refresh
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

//setup the sensor data change buttons (Amount, Change) 
function setupBtnsSensorData() {

    //select the sensor data buttons
    $("#sensorDataBtns button").click(function () {
     
        var btn = $(this);

        //check for current class on button
        if (!btn.hasClass("current")) //if not then
        {


            $("#sensorDataBtns button.current").removeClass("current"); //find and remove current of buttons
            btn.addClass("current"); //add current to this one
        }
        changeChartData("data-graph-data", btn.attr("data-graph-data")); //change the chart data and refresh the chart.
    });
}

//setup the scale feature at the bottom of the graph (All, Hour, Day, Month, Year)
function setupBtnsSensorScale() {
    //to do
}

//setup the scroll buttons for the graph
function setupBtnsSensorScroll() {
    //to do
}

//search for current class on buttons and build a graph title
function updateTitleFromBtns() {
    //to do
}

//changes the chart title to Loading Chart...
function updateTitleToLoadingChart() {
    //to do 
}

//change the chart data if changed and redraw chart with title and Data Div updates
function changeChartData(attribute, value) {
    var chartDataDiv = $("#chartData"); //cache a wrapped copy of the chart data div

    if (chartDataDiv.attr(attribute) !== value) {
        updateTitleToLoadingChart(); //Change the title to reflect a chart is being loaded
        chartDataDiv.attr(attribute, value); //change the data div
        setupChartPropertiesFromDataDiv(); //this changes the chart properties to reflect button press
        updateChart(); //update the chart with an Ajax call
        updateTitleFromBtns(); //change the title for the new chart
    }
}

//setup the chart properties from the div on page
function setupChartPropertiesFromDataDiv(){
    //to do
}

//wrapper function that chooses which type of chart points to get (single or multiple)
function updateChart() {
    getChartPoints();
}

//Ajax request to Graph controller for the points corrosponding to the graph.  
//returns an array of graph points
function getChartPoints() {
    
    var chartDataDiv = $("#chartData");

    $.getJSON('/Main/GetGraphPoints',
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
            drawChart();
        });
}

//gets the multiple data sets for the chart statisitics
function getGraphPointsWithStats() {
    //ToDo:  
}

//Wrapper function that chooses the correct chart to draw
function drawChart() {
    drawChartSingleData(); //hard coded for now
}


//setup a global chart object to handel the chart properties
var chartProperties = {}; 



//draw a graph for given canvas tag
function drawChartSingleData() {

    //alert("debug");
    //alert(moment("20170424T0432", "YYYYMMDDThhmm"));

    var myChart = $("#sensorChartHere");

    var foo = new Chart(myChart, {
        type: 'line',
        responsive: false,
        maintainAspectRatio: false,
        data:
        {
            labels: chartProperties.xAxis,

            datasets:
            [
                {
                    label: chartProperties.datasetLabel,
                    data: chartProperties.yAxis,
                    lineTension: 0,
                    backgroundColor: chartProperties.backgroundColor,
                    borderColor: chartProperties.borderColor,
                    pointBorderColor: "rgba(0,0,0,1)",
                    pointRadius: 5

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
                        //callback: function (value) {
                        //    return Date.parse(value).toLocaleString();
                        //}
                    },
                }]
            }
        }
    });

}
