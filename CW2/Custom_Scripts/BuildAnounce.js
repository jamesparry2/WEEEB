$(document).ready(function () {
    $.ajax({
        url: '/Anouncements/BuildTable',
        success: function (result) {
            $('#tableDiv').Ajax.JavaScriptStringEncode(result);
        }
    });
})
