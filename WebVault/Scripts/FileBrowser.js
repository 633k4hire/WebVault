$(document).ready(function () {

    // run on page load  

    function calcFileAppHeight() {
        var footer = 50;
        var toolbar = 50;
        var newHeight = jQuery(window).height() - jQuery("#MainMenu").height() - jQuery("#footer").height() - jQuery("#toolbar").height();
        //$("#DirTree").height(newHeight);
        // $("#L0XBOX").height(fullHeightMinusHeader);
        $("#browserbox").height(newHeight);
        //$("#directorytree").height(newHeight);

        //$("#DEBUGBOX").height(newHeight);
    }

    calcFileAppHeight();
    // run on window resize event
    $(window).resize(function () {
        calcFileAppHeight();
    });
    


});

function treeClick(evt) {
    // this gets the element clicked so you can do what you like with it
    var src = window.event != window.undefined ? window.event.srcElement : evt.target;
}
function fileNodeClick(id)
{
    alert("File " + id + " selected");
}
function dirNodeClick(id) {
    alert("Dir "+id+" selected");
}


