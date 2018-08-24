$(document).ready(function () {
    $("#Name").on("focusout", OnFocusOut);
    $("input[type='submit']").on("click", OnClick);
});

function OnFocusOut() {
 
    $.ajax(
        {
            url: "/Courses/UniqueCourseName",
            type: "post", 
            data:
                {
                    DataName: this.id, 
                    text: this.value
                }, 
            conttext: this,
            success: function (data) {
                if (data !== null && data.length > 0) {
                  
                    $("#warningText").removeClass("hidden");
                    $("#Name").addClass("has-error");
                    
                }
                else {
                  
                    $("#warningText").addClass("hidden");
                    $("#Name").removeClass("has-error");
                }
            },
            error: function (data) {
               
                $("#warningText").removeClass("hidden");
                $("#Name").addClass("has-error");
            }

        }
    );
}

function OnClick(e) {
    if ($("#Name").hasClass("has-error")) {
        e.preventDefault();
        
     
    }
 
}
