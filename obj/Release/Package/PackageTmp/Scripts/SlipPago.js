let messageElementSol;

$(document).ready(function () {
    messageElementSol = $('#divMessageSol');
});

function ConsultarRol() {
    //obtener el year
    let yearPer = $("#YearPer").val().split('-');
    let year = yearPer[0];
    let per = 0;

    if (yearPer[1] === "BONO")
        per = 13;
    else
        per = parseInt(yearPer[1]);
   

    let filter = {
        year: year,
        periodo:per
    };

    $('#consultaSection').empty(); 

    $.blockUI({ message: messageElementSol });
    var request = $.ajax({
        url: "/ZonaPersonal/SlipPago",
        type: "POST",
        data: JSON.stringify(filter),
        dataType: "html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {
        $.unblockUI();
        $('#consultaSection').append(response);  
        
    });

    request.fail(function (jqXHR, textStatus) {
        $.unblockUI();
        alert("Proceso no realizado: " + textStatus);
       
    });      

}