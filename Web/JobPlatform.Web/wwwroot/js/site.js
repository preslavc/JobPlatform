﻿$(function () {
    $("time").each(function (i, e) {
        const dateTimeValue = $(e).attr("datetime");
        if (!dateTimeValue) {
            return;
        }

        const time = moment.utc(dateTimeValue).local();
        $(e).html(time.format("DD-MM-YYYY HH:mm:ss"));
        $(e).attr("title", $(e).attr("datetime"));
    });
});