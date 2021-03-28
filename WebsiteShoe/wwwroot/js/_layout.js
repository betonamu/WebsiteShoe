var Search = function () {
    var term = document.getElementById("search").value;
    var url = "/Product/Search/?term=" + term;
    $.get(url, SearchCallBack);
}

var SearchCallBack = function (data, status) {
    if (status == "success") {
        var option = "";
        data.forEach(function (item) {
            var b = JSON.parse(JSON.stringify(item));
            var image = b.images.slice(2, 500);
            option +=
                '<ul>' +
                '<li>' +
                '<img src="' + image + '" class="col-sm-4" height=30px width=30px>' +
                '<a class="col-sm-8" href = "/Product/ProductDetail/?id=' + b.shoeId + '" >' + b.shoeName + '</a >' +
                '<li>'
            '</ul>';
            console.log(b.shoeId);
        });
        $("#search1").empty();
        $("#search1").append(option);
    }
}