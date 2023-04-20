let messageElementSol;

$(document).ready(function () {
    messageElementSol = $('#divMessageSol');
});


function ConsultarEmpleados() {
    
    //obtener datos
    let tBusqueda = $("#tipoBusqueda").val();
    let busqueda = $("#busqueda").val();
    
   

    let filter = {
        tipoBusqueda: tBusqueda,
        busqueda: busqueda        
    };

    $('#consultaSlipSection').empty(); 



    $.blockUI({ message: messageElementSol });
    var request = $.ajax({
        url: "/ZonaPersonal/ConsultaSlipPagoIndex",
        type: "POST",
        data: JSON.stringify(filter),
        dataType: "html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {
        $.unblockUI();
        $('#consultaSlipSection').append(response);

        $('#tbl_consultSplip').DataTable({
            "language": {
                "url": "../Scripts/Datatables/Spanish_Datatable.json"
            }

        });

    });

    request.fail(function (jqXHR, textStatus) {
        $.unblockUI();
        alert("Proceso no realizado: " + textStatus);

    });      








}


function ConsultarRolC() {
    //obtener el year
    let yearPer = $("#YearPer").val().split('-');
    let year = yearPer[0];
    let per = 0;
    let codEmpleado = $("#codEmpleado").val();

    if (yearPer[1] === "BONO")
        per = 13;
    else
        per = parseInt(yearPer[1]);

    let filter = {
        year: year,
        periodo: per,
        codEmpleado: codEmpleado
    };

    $('#consultaSection').empty(); 

    $.blockUI({ message: messageElementSol });
    var request = $.ajax({
        url: "/ZonaPersonal/ConsultaSlip2",
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