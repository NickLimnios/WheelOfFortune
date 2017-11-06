// Write your JavaScript code.

//Get User balance from server

function getBalance() {

    $.ajax({
        url: urls.balanceUrls,
        type: 'GET',
        data: { amount: 'balance' }

    }).done(function (data) {
        document.getElementById('userBalance').innerText = "My Balance is: " + data.balance;

    }).fail(function () {
        alert('fail');
    });
}
