$(document).ready(function () {
    $("#role").multiselect();

    $("a.check-comment").each(function () {
        if (check_children(this))
            $(this).css("pointer-events", "none");//
    });

    $("input.btn-comment").click(function () {
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

});


