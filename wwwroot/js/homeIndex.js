$(document).ready(function () {
    $("#btnSearch").click(function (e) {
        e.preventDefault();

        var govId = $("#govarnateId").val();
        var regId = $("#regionId").val();

        var origin = window.origin;
        window.open(origin + "/home/index?regId=" + regId + "&govId=" + govId + "","_self");
    })
})