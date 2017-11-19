// Write your JavaScript code.

var wheelHash = "";
//var lastSpinRestingSlice = -1;

//Called to get the balance from anywhere.
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

function getServerWheelHash() {
    $.ajax({
        url: urls.spinWheelHashUrl,
        type: 'GET',
        data: { wheelH: '_wheelHash' }

    }).done(function (data) {
        wheelHash = data._wheelHash;

    }).fail(function () {
        alert('Failed to retrieve wheel Hash.');
    });
}

//Listener when the user presses the spin Btn in STW tab
//function requestServerSpin() {
//    $.ajax({
//        url: urls.spinUrl,
//        type: 'POST',
//        data: {
//            _spinWheelHash: wheelHash,
//            _spinBetAmount: document.getElementById('js-userSpinAmountInput').value,
//            spinStatusResponse: 'spinStatus',
//            spinMeesage: 'spinStatusMsg',
//            userPlacedAmountResponse: 'userPlacedAmount',
//            userReceivedAmountResponse: 'userReceivedAmount ',
//            spinRestingSliceResponse: 'wheelStoppingSlice'

//        }

//    }).done(function (data) {
//        if (data.spinStatusResponse == "false")
//            alert(spinMeesage)
//        lastSpinRestingSlice = data.wheelStoppingSlice;
//        getBalance();

//    }).fail(function () {
//        alert('fail');
//    })
//}

//$('#js-spinWheel-Btn').click(function () {
//    $.ajax({
//        url: urls.spinUrl,
//        type: 'POST',
//        data: {
//            spinBetAmount: document.getElementById('js-userSpinAmountInput').value,
//            spinStatusResponce: 'spinStatus',
//            userPlacedAmountResponce: 'userPlacedAmount'
//        }

//    }).done(function (data) {
//        if (data.spinStatus == "Not a valid spin")//TODO This is quick and dirty Fix it properly.
//        { alert("Not a valid spin came from server. Fix this properly.") }
//        getBalance();
//        //alert(data.spinStatus + data.userPlacedAmount);
//        //document.getElementById('userBalance').innerText = "The spin was: " + data.spinStatus;

//    }).fail(function () {
//        alert('fail');
//    })
//})


