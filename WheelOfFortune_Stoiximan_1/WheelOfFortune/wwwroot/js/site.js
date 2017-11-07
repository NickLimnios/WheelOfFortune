// Write your JavaScript code.

//Get User balance from server

function getBalance() {

    $.ajax({
        url: urls.balanceUrl,
        type: 'GET',
        data: { amount: 'balance' }

    }).done(function (data) {
        document.getElementById('userBalance').innerText = "My Balance is: " + data.balance;

    }).fail(function () {
        alert('fail');
    });
}

$('#js-spinWheel-Btn').click(function () {
    $.ajax({
        url: urls.spinUrl,
        type: 'GET',
        data: { theSpinResult: 'spinResult' }

    }).done(function (data) {
        document.getElementById('userBalance').innerText = "The spin was: " + data.spinResult;

    }).fail(function () {
        alert('fail');
    })
})

//    function userSpinWheel() {

//    }))
//}
