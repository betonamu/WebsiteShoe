var AddToCard = function () {
    var quantity = document.getElementById("quantity").value;
    var shoeId = document.getElementById("shoe-id").value;
    $.ajax({
        type: "POST",
        url: "/Cart/AddToCart/",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ shoeId: parseInt(shoeId), quantity: parseInt(quantity) }),
        dataType: "json",
        success: function (res) {
            location.href = "/Cart/";
        }
    });
}
