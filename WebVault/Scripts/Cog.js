$(document).ready(function () {
    

    // run on page load    
    cogHeight(50);

    // run on window resize event
    $(window).resize(function () {
        cogHeight(50);
     });

});
function cogHeight(adjust) {
    var windowHeight = jQuery(window).height();
    if (adjust === null)
    {
        var newtop = (windowHeight - 50);
        $("#CogBox").css('top', newtop + 'px');
    } else {
        var newtop = (windowHeight - 50 - adjust);
        $("#CogBox").css('top', newtop + 'px');
    }       

}