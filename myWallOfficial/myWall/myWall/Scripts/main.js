$(function () {
    "use strict";

    var prevX, prevY;
    var currX, currY;
    var flag;
    // Drawing stuff

    var hub = $.connection.drawHub;
    // Another ones won't work in IE
    $.connection.hub.start({ transport: 'longPolling' }).done();

    hub.client.drawFromHub = function (pX, pY, cX, cY) {

        prevX = pX;
        prevY = pY;
        currX = cX;
        currY = cY;

        draw();
    }


    var canvas = document.getElementById("canvas");
    var context = canvas.getContext("2d");

    var $parentDiv = $("#canvas").parent("div");

    canvas.width = $parentDiv.width();
    canvas.height = $parentDiv.height();

    canvas.addEventListener("mousemove", function (e) {
        findxy('move', e.clientX, e.clientY)
    }, false);

    canvas.addEventListener("mousedown", function (e) {
        findxy('down', e.clientX, e.clientY)
    }, false);

    canvas.addEventListener("mouseup", function (e) {
        findxy('up', e.clientX, e.clientY)
    }, false);

    canvas.addEventListener("mouseout", function (e) {
        findxy('out', e.clientX, e.clientY)
    }, false);

    function findxy(res, clientX, clientY) {

        if (res == 'down') {
            prevX = currX;
            prevY = currY;
            currX = clientX - canvas.offsetLeft;
            currY = clientY - canvas.offsetTop;

            flag = true;
        }

        if (res == 'up' || res == "out") {
            flag = false;
        }
        if (res == 'move') {
            if (flag) {
                prevX = currX;
                prevY = currY;
                currX = clientX - canvas.offsetLeft;
                currY = clientY - canvas.offsetTop;

                hub.server.move(prevX, prevY, currX, currY);

                draw();
            }
        }
    }

    function draw() {

        context.beginPath();
        context.moveTo(prevX, prevY);
        context.lineTo(currX, currY);
        context.lineWidth = 5;
        context.stroke();
        context.closePath();

        prevX = currX;
        prevY = currY;

    }
});