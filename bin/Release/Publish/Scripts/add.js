let objectUsuario = {
    cod_trans: "",
    Ruc: "",
    Fecha: "",
    autOld: "",
    autNew:"",
    listaCias: []

};

let n = 1;

//Initialize variables
let listaCias = []; //Lista de cias.
//Button Add
$("#btnAdd").click(function () {

        objectUsuario.cod_trans = $("#Cod_Trans").val(),
        objectUsuario.Ruc = $("#Ruc").val(),
        objectUsuario.Fecha = $("#Fecha").val(),
        objectUsuario.autOld = $("#Cod_Viejo").val(),
        objectUsuario.autNew = $("#Cod_Nuevo").val()

    if ($("#formV").valid()) {

        
            let ciaLine = {

                Cod_Doc: $("#Cod_Doc").val(),
                Estab: $("#Estab").val(),
                Pto_Emi: $("#Pto_Emi").val(),
                Fin_Old: $("#Fin_Old").val(),
                Ini_New: $("#Ini_New").val(),
                pe: n


            };
            n = n + 1;

            if (objectUsuario.listaCias.length > 0) {

                let existData = $.grep(objectUsuario.listaCias, function (element, index) {
                    return element.Cod_Doc === ciaLine.Cod_Doc && element.Estab === ciaLine.Estab;
                });




                //revisamos que no exista en la lista
                if (existData.length == 0) {


                    $("#Cod_Trans").prop('disabled', true);
                    $("#Ruc").prop('disabled', true);
                    $("#Fecha").prop('disabled', true);
                    $("#Cod_Viejo").prop('disabled', true);
                    $("#Cod_Nuevo").prop('disabled', true);

                    $("#Cod_Doc").val("");
                    $("#Estab").val("");
                    $("#Pto_Emi").val("");
                    $("#Fin_Old").val("");
                    $("#Ini_New").val("");

                    objectUsuario.listaCias.push(ciaLine);
                    RenderTable(objectUsuario);




                } else {




                    toastr.warning('YA SE ENCUENTRA EN AGREGADO');
                }




            } else {
                toastr.success('AGREGADO CORRECTAMENTE');


                $("#Cod_Trans").prop('disabled', true);
                $("#Ruc").prop('disabled', true);
                $("#Fecha").prop('disabled', true);
                $("#Cod_Viejo").prop('disabled', true);
                $("#Cod_Nuevo").prop('disabled', true);


                $("#Cod_Doc").val("");
                $("#Estab").val("");
                $("#Pto_Emi").val("");
                $("#Fin_Old").val("");
                $("#Ini_New").val("");

                objectUsuario.listaCias.push(ciaLine);
                RenderTable(objectUsuario);


            }

     


    } else {
        toastr.error('LLENE TODOS LOS CAMPOS' );

    }
});


$(function () {
        
    $('.date-picker').datepicker(
        {
            format: "dd/mm/yyyy",
         
        }); //Initialise any date pickers
  

    });
       
$("#inputProcesar").click(function () {
    
    
    $.ajax({
        url: "/DoleEcIntranet/Home/GenerarXml/",
        type: "POST",
        data: JSON.stringify(objectUsuario),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
         success: function (data) {
            var response = data;
             window.location = '/DoleEcIntranet/Home/DownloadXml?fileGuid=' + response.FileGuid
                + '&filename=' + response.FileName;
        }
    })
    });








$(document).ready(function () {

    $("#formV").validate({

        errorElement: "em_error",
        rules: {
            Cod_Doc: {
                required: true
            },

            Cod_Trans: {
                required: true

            },

            Ruc: {
                
                required: true

            },
            Estab: {
                required: true

            },
            Date: {
                required: true

            },
            Cod_Nuevo: {
                required: true

            },
            Cod_Viejo: {
                required: true

            },
            Pto_Emi: {
                required: true
            },
            Fin_Old: {
                required: true
            },

            Ini_New: {
                required: true
            }
        },
        messages: {

            Cod_Doc: {
                required: "Campo requerido"
            },

            Cod_Trans: {
                required: "Campo requerido"
            },
            Fecha: {
                required: "campo requerido"

            },

            Cod_Viejo: {
                required: "campo requerido"

            },
            Cod_Nuevo: {
                required: "campo requerido"

            },
            Ruc: {
                required: "campo requerido"

            },
            Estab: {
                required: "Campo requerido"
            },
            Pto_Emi: {
                required: "Campo requerido"

            },
            Fin_Old: {
                required: "campo requerido"

            },
            Ini_New: {
                required: "campo requerido"

            }


        }



    });


});



    function RenderTable(listU) {
        $("#tbl_list").empty();

        var html = '<table id="tbl_cias" class="table"><thead><tr>';
       
        html += '<th>Codigo Document</th > ';
        html += '<th>Establecimiento</th>';
        html += '<th>Punto Emision</th>';
        html += '<th>FindOld</th>';
        html += '<th>IniNew</th>';
        html += '<th></th>';
        html += '</tr></thead><tbody>';

        $.each(listU.listaCias, function (index, item) {

            


            html += '<tr>';
          
            html += '<td>' + item.Cod_Doc + '</td>';
            html += '<td>' + item.Estab + '</td>';
            html += '<td>' + item.Pto_Emi + '</td>';
            html += '<td>' + item.Fin_Old + '</td>';
            html += '<td>' + item.Ini_New + '</td>';
            html += '<td><button type="button" id="btnDelete" onClick="deleteCia(\'' + item.pe + '\')"><i class="fas fa-minus"></i ></button></td>';
            html += '</tr>';


        });

        html += '</tbody></table>';




        $("#tbl_list").append(html);
        //Appy Datatable
        $("#tbl_cias").DataTable();
}

    function deleteCia(lol) {

        console.log(objectUsuario.listaCias);
        objectUsuario.listaCias = $.grep(objectUsuario.listaCias, function (element, index) {
            return element.pe != lol;



        });




        toastr.error('ELIMINADO CORRECTAMENTE ');
        RenderTable(objectUsuario);






    }


