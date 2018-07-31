$(document).ready(function () {
    $("#EndDate").on("change", OnChange);
    $("#StartDate").on("change", OnChange);
});
function OnChange(e) {
    var startDate = document.getElementById("StartDate").value;
    var endDate = document.getElementById("EndDate").value;
    if (startDate < endDate) {
        $("#DateError").addClass("hidden");
    }
    else {
        $("#DateError").removeClass("hidden");
    }


}