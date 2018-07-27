
$(document).ready(function () {

  $("select").on("change", OnChange);

});

//function OnChange(e) {
  
//    $.ajax(
//        {
//            url: "/Modules/SatrtDateValidate",
//            type: "post",
//            dataType: "json",
//            data:
//                {

//                    CourseId: $("#CourseId").val()
               
                  
//                },
//            context: this,
//            success: function (data) {


//                alert(data);
                
                
            
//                $("#StartDate").val(data)

//            },
//            error: function (data) {
//                alert("Error");
//            }
//        });
//}
function OnChange(e) {

    $.ajax(
        {
            url: "/Modules/SatrtDateValidate",
            type: "post",
            dataType: "json",
            data:
                {

                    CourseId: $("#CourseId").val(), 
                  sDate: "sDate"


                },
            context: this,
            success: function (data) {


                alert(data);



                $("#StartDate").val(data);

            },
            error: function (data) {
                alert("Error");
            }
        });
}
