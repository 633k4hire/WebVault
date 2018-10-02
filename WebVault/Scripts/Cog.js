$(document).ready(function () {
    

    // run on page load    
    cogHeight();

    // run on window resize event
    $(window).resize(function () {
        cogHeight();
     });

});
function cogHeight(adjust) {
    var windowHeight = jQuery(window).height();
    if (adjust === null)
    {
        var newtop = (windowHeight - $("#MainMenu").height()-83);
        $("#CogBox").css('top', newtop + 'px');
    } else {
        var newtop = (windowHeight - $("#MainMenu").height() - adjust);
        $("#CogBox").css('top', newtop + 'px');
    }       

}