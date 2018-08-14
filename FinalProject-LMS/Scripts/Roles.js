$(document).ready(function () {
    $("#Kind").on("change", OnChange);

});
function OnChange(e) {
   
    var role = document.getElementById("Kind").value;
    if (role === "1") {
        $("#Courses").addClass("hidden");
    }
    else if (role === "2"){
        $("#Courses").removeClass("hidden");
    }



}