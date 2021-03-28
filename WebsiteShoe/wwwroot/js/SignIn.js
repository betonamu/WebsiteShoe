var checkConfirmPassword = function () {
    if (document.getElementById('password').value == document.getElementById('confirm_password').value) {
        document.getElementById('message').style.color = 'green';
        document.getElementById('message').innerHTML = 'Mật khẩu nhập lại chính xác';
        document.getElementById('submit').disabled = false;
    } else {
        document.getElementById('message').style.color = "red";
        document.getElementById('message').innerHTML = 'Mật khẩu nhập lại chưa chính xác';
        document.getElementById('submit').disabled = true;
    }
};

var checkPassword = function () {
    var password = document.getElementById("password").value;
    document.getElementById("message_password").style.color = "red";
    var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    var lowerCaseLetters = /(?=.*[a-z])/g;
    var upperCaseLetters = /(?=.*[A - Z])/g;
    var numbers = /(?=.*\d)/g;
    var specialCaseLetter = /(?=.*[@$!%*?&])/g;

    var messagePassword = document.getElementById("message_password");
    if (!password.match(lowerCaseLetters)) {
        messagePassword.innerHTML = "Mật khẩu phải có ít nhất 1 chữ thường";
    } else if (!password.match(upperCaseLetters)) {
        messagePassword.innerHTML = "Mật khẩu phải có ít nhất 1 chữ hoa";
    } else if (!password.match(numbers)) {
        messagePassword.innerHTML = "Mật khẩu phải có ít nhất 1 chữ số";
    } else if (!password.match(specialCaseLetter)) {
        messagePassword.innerHTML = "Mật khẩu phải có ít nhất 1 ký tự đặc biệt (chỉ bao gồm: @$!%*?&)";
    } else if (password.length <= 8) {
        messagePassword.innerHTML = "Mật khẩu phải có ít nhất 8 ký tự";
    } else {
        messagePassword.style.color = "green";
        messagePassword.innerHTML = "";
    }
}


var checkUsernameExists = function () {
    var username = $("#username").val();
    $.ajax({
        type: "POST",
        url: "/Auth/CheckUserNameExists/",
        data: JSON.stringify(username),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response) {
                document.getElementById("username_message").classList.remove("text-danger");
                document.getElementById("username_message").style.color = "green";
                document.getElementById("username_message").innerHTML = "Username có thể sử dụng";
                document.getElementById('submit').disabled = true;
            } else {
                document.getElementById("username_message").classList.add("text-danger");
                document.getElementById("username_message").innerHTML = "Username đã tồn tại! Vui lòng chọn Username khác.";
                document.getElementById('submit').disabled = false;
            }
        }
    });
};

var inputEmail = document.getElementById("email");
inputEmail.addEventListener('keyup', function () {
    var email = document.getElementById("email").value;
    $.ajax({
        type: "POST",
        url: "/Auth/CheckEmailExists/",
        data: JSON.stringify(email),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response) {
                var email = document.getElementById("email").value;
                var regex = /^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/;
                if (email.match(regex)) {
                    document.getElementById("email_message").classList.remove("text-danger");
                    document.getElementById("email_message").style.color = "green";
                    document.getElementById("email_message").innerHTML = "Email hợp lệ có thể sử dụng";
                    document.getElementById('submit').disabled = false;
                } else if (!email.match(regex)) {
                    document.getElementById("email_message").classList.add("text-danger");
                    document.getElementById("email_message").innerHTML = "Vui lòng nhập email hợp lệ";
                    document.getElementById('submit').disabled = true;
                }
            } else {
                document.getElementById("email_message").classList.add("text-danger");
                document.getElementById("email_message").innerHTML = "Email đã tồn tại! Vui lòng nhập Email khác.";
                document.getElementById('submit').disabled = true;
            }
        }
    });
});
