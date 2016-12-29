$(function () {

    $(window).on('scroll', function () {
        $(this).scrollTop() > 200 ? $(".to-top-button").fadeIn('slow') : $(".to-top-button").fadeOut('slow');
    });

    $(".to-top-button").on("click", function (e) {
        $('body').animate({ scrollTop: 0 }, 500);
    });

});