$(document).ready(function () {

    function cHeight() {
        var fullHeightMinusHeader = jQuery(window).height() - $("#MainMenu").height()-84;
        $("#CogBox").css({ top: fullHeightMinusHeader });
    }

    // run on page load    
    cHeight();

    // run on window resize event
    $(window).resize(function () {
        cHeight();
    });

});
