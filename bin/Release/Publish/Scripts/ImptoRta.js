function ConsultarImptoRta() {
    //obtener el year
    let yearPer = $("#YearPer").val().split('-');
    let year = yearPer[0];
    let per = parseInt(yearPer[1]);

    let filter = {
        year: year,
        periodo:per
    };

    $('#consultaSectionImpto').empty(); 

    var request = $.ajax({
        url: "/ZonaPersonal/ImptoRta",
        type: "POST",
        data: JSON.stringify(filter),
        dataType: "html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {
        $('#consultaSectionImpto').append(response);  
        
    });

    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);
       
    });      

}