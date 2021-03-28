var quantityId = "";
var shoeId = "";
var quantity = 0;

var minus = function (event) {
    let minusId = event.currentTarget.id;
    shoeId = minusId.substring(0, minusId.length - 1);
    quantityId = "quantity" + shoeId;
    quantity = parseInt(document.getElementById(quantityId).value);
    quantity = quantity - 1;
    if (quantity < 1) {
        alert("Số lượng không thể nhỏ hơn 1");
        quantity = 1;
    }
    else {
        updateQuantity(quantity, shoeId, quantityId);
    }
}

var plus = function (event) {
    let plusId = event.currentTarget.id;
    shoeId = plusId.substring(0, plusId.length - 1);
    quantityId = "quantity" + shoeId;
    quantity = parseInt(document.getElementById(quantityId).value);
    quantity = quantity + 1;
    updateQuantity(quantity, shoeId, quantityId);
}

var updateQuantity = function (number, shoeId, quantityId) {
    let totalPriceId = "TotalPrice" + shoeId;
    $.ajax({
        type: "POST",
        url: "/Cart/UpdateQuantity/",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ shoeId: parseInt(shoeId), quantity: number }),
        dataType: "json",
        success: function (res) {
            let quantity = res.quantity;
            let totalPrice = res.totalPrice;
            document.getElementById(quantityId).value = quantity;
            document.getElementById(totalPriceId).innerHTML = totalPrice;
        }
    });
}

