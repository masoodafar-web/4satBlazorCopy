var firebaseConfig = {
    apiKey: "AIzaSyBgxIT63n3TJ2_bDzrORjsRI_eCFxWfKG8",
    authDomain: "foursat-242511.firebaseapp.com",
    databaseURL: "https://foursat-242511.firebaseio.com",
    projectId: "foursat-242511",
    storageBucket: "foursat-242511.appspot.com",
    messagingSenderId: "649446896190",
    appId: "1:649446896190:web:73e4ea885f008d7b16176f",
    measurementId: "G-LY74NPXPQH"
};

firebase.initializeApp(firebaseConfig);

const messaging = firebase.messaging();

messaging.usePublicVapidKey('BP-Pct1NudOiROsPBfIhKkYTlveE0wWDQIzuqFT4QXAhLBwVQXxZe_u4LkASjgO6QUZ_G_Xd-LtlDcsaxY2z0_g');

messaging.onTokenRefresh(() => {
    messaging.getToken().then((refreshedToken) => {
        console.log('Token refreshed.');

        setTokenSentToServer(false);

        sendTokenToServer(refreshedToken);

        resetUI();

    }).catch((err) => {
        console.log('Unable to retrieve refreshed token ', err);
    });
});

// [START receive_message]
// Handle incoming messages. Called when:
// - a message is received while the app has focus
// - the user clicks on an app notification created by a service worker
//   `messaging.setBackgroundMessageHandler` handler.
messaging.onMessage((payload) => {
    console.log('Message received. ', payload);

    appendMessage(payload);

});

function resetUI() {

    messaging.getToken().then((currentToken) => {
        if (currentToken) {
            sendTokenToServer(currentToken);

        } else {

            console.log('No Instance ID token available. Request permission to generate one.');
            setTokenSentToServer(false);
        }
    }).catch((err) => {
        console.log('An error occurred while retrieving token. ', err);
        setTokenSentToServer(false);
    });

}


function sendTokenToServer(currentToken) {
    //if (!isTokenSentToServer()) {
        //console.log('Sending token to server...');

        //alert(currentToken);

        $.ajax({
            url: "/Chats/AddPushToken",
            type: "post",
            global: false,
            data: {
                token: currentToken
            },
            success: function (data) {
                //alert(data);
            }
        });

        setTokenSentToServer(true);
    //} else {
    //    console.log('Token already sent to server so won\'t send it again ' +
    //        'unless it changes');
    //}

}

function RemoveTokenFromServer(currentToken) {


    $.ajax({
        url: "/Chats/RemovePushToken",
        type: "post",
        global: false,
        data: {
            token: currentToken
        },
        success: function (data) {
            //alert(data);
        }
    });

}

function isTokenSentToServer() {
    return window.localStorage.getItem('sentToServer') === '1';
}

function setTokenSentToServer(sent) {
    window.localStorage.setItem('sentToServer', sent ? '1' : '0');
}

function requestPermission() {
    console.log('Requesting permission...');

    Notification.requestPermission().then((permission) => {
        if (permission === 'granted') {
            console.log('Notification permission granted.');

            resetUI();

        } else {
            console.log('Unable to get permission to notify.');
        }
    });

}

function deleteToken() {

    messaging.getToken().then((currentToken) => {

        if (currentToken) {
            RemoveTokenFromServer(currentToken);
        }

        // [END delete_token]
    }).catch((err) => {
        console.log('Error retrieving Instance ID token. ', err);


    });

    document.getElementById('LogOffForm').submit();
}

//نمایش ناتیفی
function appendMessage(payload) {

    var popupNotification = $("#popupNotification").data("kendoNotification");

    if (payload.notification.image) {

        popupNotification.show({
            title: payload.notification.title,
            img: payload.notification.image 
        }, "pushContainImg");
    }
    else {
        popupNotification.show({
            title: payload.notification.title,
            message: payload.notification.body
        }, "push");
    }

}

resetUI();



$("#show-notifi-sidebar").click(function () {

    GetPushList();

});

function GetPushList() {

    $('#chatContactLoading').show();

    $.ajax({
        url: "/Chats/GetPushList",
        type: "post",
        data: {},
        success: function (data) {

            $('#chatContactLoading').hide();
            $('#feed-content').html(data);

        }
    });
}