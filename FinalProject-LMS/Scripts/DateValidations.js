$(document).ready(function () {
    window.courseStartDate = document.getElementById("StartDate").value;
   
  
    $("#EndDate").on("change", OnChange);
    $("#StartDate").on("change", OnChange);
    $("#CreateButton").on("change", OnClick);
    $("input[type='submit']").on("click", OnClick);
});
function OnChange(e) {
   
    var startDate = document.getElementById("StartDate").value;
    var endDate = document.getElementById("EndDate").value;
    if (startDate < courseStartDate) {
        $("#warningTextForCourseStartDate").removeClass("hidden");

    }
    else {
        $("#warningTextForCourseStartDate").addClass("hidden");
    }
   if (startDate < endDate) {
        $("#warningText").addClass("hidden");
     
    
    }
    else{
        $("#warningText").removeClass("hidden");
    
    }


}

function OnClick(e) {
    if (!$("#warningText").hasClass("hidden") || !$("#warningTextForCourseStartDate").hasClass("hidden")) {
        e.preventDefault();
    }

   


}