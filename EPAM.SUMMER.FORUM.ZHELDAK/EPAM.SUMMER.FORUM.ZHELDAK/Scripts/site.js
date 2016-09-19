$(function () {

    // We can attach the `fileselect` event to all file inputs on the page
    $(document).on('change', ':file', function () {
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);
    });

    // We can watch for our custom `fileselect` event like this
    $(document).ready(function () {
        $(':file').on('fileselect', function (event, numFiles, label) {

            var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + 'files selected' : label;

            if (input.length) {
                input.val(log);
            } else {
                if (log) alert(log);
            }

        });
    });

});

$(document).ready(function () {
    $("#role").multiselect();

    $("a.check-comment").each(function () {
        if (check_children(this))
            $(this).css("pointer-events", "none");//
    });
    $("input.btn-comment").click(function() {
        $("#comment-area").empty();
    });

    $("#search").click(function (e) {
        e.preventDefault();
        var value = $("#auto-field").val();
        if (value === "")
            return;

        var newUrl = "/Question/ShowQuestion?categoryName=" + value;
        window.location.href = newUrl;
    });

    $(window).on('scroll', function () {
        $(this).scrollTop() > 200 ? $(".to-top-button").fadeIn('slow') : $(".to-top-button").fadeOut('slow');

    });

    $(".to-top-button").on("click", function (e) {
        $('body').animate({ scrollTop: 0 }, 500);
    });
    
});


function check_children(parent) {
    var childs = $(parent).children();
    var ch = $(childs[0]).text();
    if (ch === "0")
        return true;
    return false;

}

function cleartext() {
    $("#comment").val("");
}
