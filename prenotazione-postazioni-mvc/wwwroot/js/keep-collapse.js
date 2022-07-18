$(".collapse").on('show.bs.collapse', function () {
    var active = $(this).attr('id');
    $.cookie(active, "1");
    alert("show");
});

$(".collapse").on('hide.bs.collapse', function () {
    var active = $(this).attr('id');
    alert("hide");
    $.removeCookie(active);
});

$(document.ready(function () {
    var panels = $.cookie(); //get all cookies
    for (var panel in panels) { //<-- panel is the name of the cookie
        console.log(panel);
    }
}));
