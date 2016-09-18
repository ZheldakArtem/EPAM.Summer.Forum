$(document).ready(function () {
    $("#auto-field").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Category/Search",
                type: "POST",
                dataType: "json",
                data: { prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.Name };
                    }));
                }
            });
        },
        messages: {
            noResults: "", results: ""
        }
    });
});