// Site.js
/**
* Run setup functions once DOM has loaded
**/
$("document").ready(function () {


    //test to see if main is empty meaning it needs content
    if ($("#main").text() === "")
    { 
        //first time loading the page then load homepage
        loadContent("Home");
        setupBtnsSensorSelect();
    }
    //load the slideshow if there is a div for it.  Call other needed functions to make it work.
    var slideShow = $("#slideShow");
    if (slideShow.text() === "")
    {
        $.get("/Main/LoadSlideShow", function (data) {
            slideShow.html(data);
            setupSlideShow();
        }, 'html')
            .fail(function () {
                slideShow.html('<h3>Slide Show Unavailble</h3> <p>Please check with administrator</p>');
            });
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
                    chartType: "line",
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
        else { //loading the sensor home
            startUserTimer();
        }


    }, "html");
    //start the timer for user input
    
}

//setup slideShow events
function setupSlideShow() {
    //default attribute for current slot to slider
    $("#ss-full-slider").attr("data-current-slot", 0);

    //click event for the left scroll button on slide show
    $("#ss-scroll-left button").click(function () {
        moveToNextSlide(-1);
        startUserTimer();
    });

    //click event for the right scroll button on slide show
    $("#ss-scroll-right button").click(function () {
        moveToNextSlide(1);
        startUserTimer();
    });

    //click event for hiding the slideshow (slideshow image)
    $("#ss-full").on("click", "img", function () {
        toggleSlideShow();
        startUserTimer();
    });

    //user clicked the a non current slideshow nav image
    $("#ss-nav-slider").on("click", "div",function () {
        $("#ss-full, #ss-scroll div").removeClass("ss-scroll-down");
        $("#ss-full-slider").attr("data-current-slot", $(this).index());
        startUserTimer();
        updateSlideshow();
    });

    //clicked on the current slot yellow div.  hide and show the slide show
    $("#ss-nav-slot-select").click(function () {
        toggleSlideShow();
        startUserTimer();
    });

    $("#ss-nav, #ss-full, #ss-nav-slot-select").

}

//go to the next slide.  -1 for left, +1 for right
function moveToNextSlide(direction) {
    var slider = $("#ss-full-slider");
    var slotNum = parseInt(slider.attr("data-current-slot")) + direction;
    var numSlots = $("#ss-full-slider div").length;

    //alert(slotNum);
    if (slotNum < 0){
        slider.attr("data-current-slot", numSlots - 1);
    }
    else if (slotNum >= numSlots){
        slider.attr("data-current-slot", 0);
    }
    else {
        slider.attr("data-current-slot", slotNum);
    }
    updateSlideshow();
}

//start a slideshow timer and
function startSlideShowTimer()
{
    //clear out the user timer and set to null
    if (timers.user !== null){
        clearTimeout(timers.user);
        timers.user = null;
    }

    //activate the slideshow
    activateSlideShow();

    //set a timer for the slideshow
    timers.slideShow = setTimeout(function () {
        //go to next slide
        moveToNextSlide(1);
        startSlideShowTimer();
    }, 5000);    
}

function startUserTimer(timeInSeconds) {

    //clear out slide show timer
    if (timers.slideShow !== null) {
        clearTimeout(timers.slideShow);
        timers.slideShow = null;
    }

    //clear out old user timer, user click on something
    if (timers.user !== null) {
        clearTimeout(timers.user);
    }

    timers.user = setTimeout(function () {
        startSlideShowTimer();
    }, 10000);

}

//take what ever is in the data-current-slot and apply offsets for both sliders
function updateSlideshow()
{
    var slider = $("#ss-full-slider");
    slider.css("margin-left", slider.attr("data-current-slot") * -1600);

    $("#ss-nav-slider").css("margin-left", slider.attr("data-current-slot") * -200 + 700);
}

//turns the slide show on
function activateSlideShow() {
    $("#ss-full, #ss-scroll div").removeClass("ss-scroll-down");
}

//turns slideshow off
function deactivateSlideShow() {
    $("#ss-full, #ss-scroll div").addClass("ss-scroll-down");
}

//toggle slideShow on/off... moves the slide show up for on and down for off.
function toggleSlideShow() {
    $("#ss-full, #ss-scroll div").toggleClass("ss-scroll-down");
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
    
    //select the sensor data buttons
    $("#chartScaleBtns button").click(function () {

        var btn = $(this);

        //check for current class on button
        if (!btn.hasClass("current")) //if not then
        {
            $("#chartScaleBtns button.current").removeClass("current"); //find and remove current of buttons
            btn.addClass("current"); //add current to this one
        }
        changeChartData("data-graph-scale", btn.attr("data-graph-scale")); //change the chart data and refresh the chart.
    });
}

//setup the scroll buttons for the graph
function setupBtnsSensorScroll() {
    $("#chartScrollLeftBtn").click(function () {

        var chartDataDiv = $("#chartData"); //cache a wrapped copy of the chart data div
        var newEnd = 0;

        switch (chartDataDiv.attr("data-graph-Scale"))
        {
            case "All":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).subtract(1, 'hour').format("YYYY-MM-DDThh:mm");
                break;
            case "Hour":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).subtract(1, 'hour').format("YYYY-MM-DDThh:mm");
                break;
            case "Day":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).subtract(1, 'day').format("YYYY-MM-DDThh:mm");
                break;
            case "Month":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).subtract(1, 'month').format("YYYY-MM-DDThh:mm");
                break;
            case "Year":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).subtract(1, 'year').format("YYYY-MM-DDThh:mm");
                break;
        }

        changeChartData("data-graph-end", newEnd);
    });

    $("#chartScrollRightBtn").click(function () {
        var chartDataDiv = $("#chartData"); //cache a wrapped copy of the chart data div
        var newEnd = 0;

        switch (chartDataDiv.attr("data-graph-Scale")) {
            case "All":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).add(1, 'hour').format("YYYY-MM-DDThh:mm");
                break;
            case "Hour":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).add(1, 'hour').format("YYYY-MM-DDThh:mm");
                break;
            case "Day":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).add(1, 'day').format("YYYY-MM-DDThh:mm");
                break;
            case "Month":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).add(1, 'month').format("YYYY-MM-DDThh:mm");
                break;
            case "Year":
                newEnd = moment(chartDataDiv.attr("data-graph-end")).add(1, 'year').format("YYYY-MM-DDThh:mm");
                break;
        }

        changeChartData("data-graph-end", newEnd);

    });


    //to do
}

//search for current class on buttons and build a graph title
function updateTitleFromBtns() {
    //to do
}

//changes the chart title to Loading Chart...
function updateTitleToLoadingChart() {
    //to do 




    //still need to do




}

//change the chart data if changed and redraw chart with title and Data Div updates
function changeChartData(attribute, value) {
    var chartDataDiv = $("#chartData"); //cache a wrapped copy of the chart data div

    if (chartDataDiv.attr(attribute) !== value) {
        updateTitleToLoadingChart(); //Change the title to reflect a chart is being loaded
        chartDataDiv.attr(attribute, value); //change the data div
        //setupChartPropertiesFromDataDiv(); //this changes the chart properties to reflect button press
        updateChart(); //update the chart with an Ajax call
        updateTitleFromBtns(); //change the title for the new chart
    }
}

//wrapper function that chooses which type of chart points to get (single or multiple)
function updateChart() {
    getChartPoints();
    startUserTimer();
    deactivateSlideShow();
}

//Ajax request to Graph controller for the points corrosponding to the graph.  
//returns an array of graph points
function getChartPoints() {
    
    var chartDataDiv = $("#chartData");

    $.getJSON('/Main/GetGraphPoints',
        {
            end: chartDataDiv.attr("data-graph-end"),
            numTicks: chartDataDiv.attr("data-graph-ticks"),
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
    if (chartProperties.globalChart !== undefined && chartProperties.globalChart !== null)
    {
        //create an new canvas
        $("#graphContainer").html('<canvas id="sensorChartHere" ></canvas>');

        //chartProperties.globalChart.destroy();
    }

    if (chartProperties.xAxis.length < 15)
    {
        chartProperties.chartType = 'bar';
    }
    else {
        chartProperties.chartType = 'line';
    }

    drawChartSingleData(); //hard coded for now
}

//draw a graph for given canvas tag
function drawChartSingleData() {

    var myChart = $("#sensorChartHere");

    chartProperties.globalChart = new Chart(myChart, {
        type: chartProperties.chartType,
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
                        labelString: chartProperties.labelString,
                        fontStyle: "bold",
                        fontSize: 20
                    },
                    ticks: {
                        fontStyle:"bold"
                    }
                }],
                xAxes:
                [{  
                    ticks:
                    {
                        autoSkip: true,
                        maxTicksLimit: 10,
                       // maxRotation: 0,
                        //minRotation: 0,
                        fontSize: 16,
                        fontStyle:"bold"
                    },

                    type: "time",
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Date'
                    },
                    time: {
              
                        displayFormats: {
                            'minute': 'h:mmA',
                            'hour': 'h:mmA',
                            'day': 'MMM D',
                            'week': 'MMM YYYY',
                            'month': 'MMM YYYY',
                            'quarter': "YYYY",
                            'year': 'YYYY'
                        }
                    }
                    
                }]
            }
        }
    });

}

//setup a global chart object to handel the chart properties
var chartProperties = {};

//global varables for times
var timers = {
    slideShow: null,
    user: null
};
