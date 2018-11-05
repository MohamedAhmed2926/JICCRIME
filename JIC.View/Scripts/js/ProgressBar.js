var interval = null;
var progressFinished = true;
function StartProgressBar() {

    UpdateProgressBar();
    interval = setInterval(UpdateProgressBar, 500);
}

function UpdateProgressBar() {
    var RequestId = $('.progress').attr("RequestID");
    if (RequestId == null) {
        return false;
    }
    else {
        $.ajax({
            type: 'GET',
            url: '/Handlers/ProgressBarHandler.ashx',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            responseType: 'json',
            data: { 'RequestID': $('.progress').attr("RequestID") },
            success: function (data) {
                if (data == null)
                    alert('JSON Object is null');
                else {
                    $('.progress').show();
                    var me = $('.progress .progress-bar');
                    if (data.RequestStatus == 1 || data.RequestStatus == 2) {
                        me.css('width', (data.ProgressPercentage) + '%');
                        me.text(data.ProgressPercentage + ' %');

                    }
                    if (data.RequestStatus == 3 && progressFinished) {
                        progressFinished = false;
                        ClearProgressBarInterval();
                        me.css('width', 100 + '%');
                        me.text(100 + ' %');
                        $('.progress').hide();
                        $('.FinishProgress').click();

                    }
                }

            },
            error: function () {
                alert('Ajax failed');
            }
        });
        return false;
    }
}

function ClearProgressBarInterval() {

    if (interval != null) {
        clearInterval(interval);
        interval = null;
        $('.progress').hide();
    }
}

