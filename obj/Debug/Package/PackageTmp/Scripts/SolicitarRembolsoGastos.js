$(document).ready(function () {
    $('.js-example-basic-single').select2({
        theme: 'bootstrap4',
        maximumResultsForSearch: 10
    })
});

alertify.set('notifier', 'position', 'top-left');
'use strict';
(function (document, window, index) {
    var inputs = document.querySelectorAll('.inputfile');
    Array.prototype.forEach.call(inputs, function (input) {
        var label = input.nextElementSibling,
            labelVal = label.innerHTML;

        input.addEventListener('change', function (e) {
            var fileName = '';
            if (this.files && this.files.length > 1)
                fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
            else
                fileName = e.target.value.split('\\').pop();

            if (fileName)
                label.querySelector('span').innerHTML = fileName;
            else
                label.innerHTML = labelVal;
        });
    });
}(document, window, 0));

//validar

$(document).ready(function () {

    $("#formV").validate({

        errorElement: "em_error",
        rules: {
            codigoempleado: {
                required: true
            },
            tipoconsumo: {
                required: true,
                min: 1


            },
            slMoneda: {
                required: true
            },

            txtMonto: {
                required: true

            },
            tipoproveedor: {
                required: true
            },

            txtTcambio: {
                required: true
            },
            slDestino: {
                required: true
            },
            fechafactura: {
                required: true
            },
            slctTipo: {
                required: true
            },
            motivogasto: {
                required: true
            },
            tipofactura: {
                required: true
            }, fechadesde: {
                required: true
            },
            fechahasta: {
                required: true
            },



            messages: {
                codigoempleado: {
                    required: "Campo requerido"
                },
                tipoconsumo: {
                    required: "Campo requerido",
                    min: "seleccione 1"
                },
                slMoneda: {
                    required: "Campo requerido"


                },
                txtMonto: {
                    required: "Campo requerido"

                },
                tipoproveedor: {
                    required: "Campo requerido"
                },

                txtTcambio: {
                    required: "Campo requerido"
                },
                slDestino: {
                    required: "Campo requerido"
                },
                fechafactura: {
                    required: "Campo requerido"
                },
                slctTipo: {
                    required: "Campo requerido"
                },
                motivogasto: {
                    required: "Campo requerido"
                },
                tipofactura: {
                    required: "Campo requerido"
                }, fechadesde: {
                    required: "Campo requerido"
                }, fechahasta: {
                    required: "Campo requerido"
                }
            }


        }

    });
});


function Detalle(id) {
    var obj = { ids: id };

    var request = $.ajax({
        url: "TcJustificar",
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "html",
        contentType: "application/json; charset=utf-8"

    });

    request.done(function (response) {



    });

    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);

    });

}


$(document).ready(function () {

    $("#formVFAE").validate({

        errorElement: "em_error",
        rules: {
            codigoempleado: {
                required: true
            },
            tipoconsumo: {
                required: true,
                min: 1


            },
            motanticefect: {
                required: true,
                min: 1
            },

            txtMonto: {
                required: true

            },
            tipoproveedor: {
                required: true
            },

            slTC: {
                required: true,
                min: 1
            },
            slDestino: {
                required: true,
                min: 1

            },
            pais: {
                required: true,
                min: 1
            },
            ciudad: {
                required: true,
                min: 1
            },
            total: {
                required: true
            },
            valor: {
                required: true
            }, fechadesde: {
                required: true
            },
            fechahasta: {
                required: true
            },



            messages: {
                codigoempleado: {
                    required: "Campo requerido"
                },
                tipoconsumo: {
                    required: "Campo requerido",
                    min: "seleccione 1"
                },
                motanticefect: {
                    required: "Campo requerido",
                    min: "-seleccione-"
                },
                txtMonto: {
                    required: "Campo requerido"

                },
                tipoproveedor: {
                    required: "Campo requerido"
                },

                slTC: {
                    required: "Campo requerido",
                    min: "-seleccione-"
                },
                slDestino: {
                    required: "Campo requerido",
                    min: "seleccione 1"
                },
                pais: {
                    required: "Campo requerido",
                    min: "seleccione 1"
                },
                ciudad: {
                    required: "Campo requerido",
                    min: "seleccione 1"
                },
                total: {
                    required: "Campo requerido"
                },
                valor: {
                    required: "Campo requerido"
                }, fechadesde: {
                    required: "Campo requerido"
                }, fechahasta: {
                    required: "Campo requerido"
                }
            }


        }

    });
});




function readURL(input) {

    if (input.files && input.files[0]) { //Revisamos que el input tenga contenido



        var reader = new FileReader(); //Leemos el contenido

        reader.onload = function (e) { //Al cargar el contenido lo pasamos como atributo de la imagen de arriba
            $('#blah').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

function checkPDF1() {

    if (document.getElementById("checkJPG").checked == false && document.getElementById("checkPDF").checked == false) {
        document.getElementById("filePDF-" + contInput).style.display = "none";
        document.getElementById("fileJPG-" + contInput).style.display = "none";
    } else {
        document.getElementById("checkJPG").checked = false;
        document.getElementById("filePDF-" + contInput).style.display = "block";
        document.getElementById("fileJPG-" + contInput).style.display = "none";
    }

}
function checkJPG1() {

    if (document.getElementById("checkJPG").checked == false && document.getElementById("checkPDF").checked == false) {
        document.getElementById("filePDF-" + contInput).style.display = "none";
        document.getElementById("fileJPG-" + contInput).style.display = "none";
    } else {

        document.getElementById("checkPDF").checked = false;
        document.getElementById("filePDF-" + contInput).style.display = "none";
        document.getElementById("fileJPG-" + contInput).style.display = "block";

    }
}


$("#btnAdd").mouseover(function () {
    document.getElementById("btnAdd").style.color = "#6DE157";;



});
$("#btnAdd").mouseout(function () {
    document.getElementById("btnAdd").style.color = "#52B63F";

});

function Enviar() {

    DetConsumo.codigoEmpleado = $("#codigoempleado").val();
    DetConsumo.slctTipo = $("#slctTipo").text;
    DetConsumo.motivogasto = $("#motivogasto").val();

}

function Rechazar() {


    $('#sectionAddComentario').empty();

    var request = $.ajax({
        url: "/PIC/_AddComentario",
        type: "POST",
        data: JSON.stringify(),
        dataType: "Html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {

        document.getElementById("btnaddComentario").style.display = "initial";
        $('#sectionAddComentario').append(response);



    });


    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);

    });



}

function VisorImagenes(formulario) {
    $('#sectionVisorImg').empty();

    var id = {
        idC: IdCabecera,
        form: formulario

    }
    var request = $.ajax({
        url: "/PIC/_VisorImagenes",
        type: "POST",
        data: JSON.stringify(id),
        dataType: "Html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {


        $('#sectionVisorImg').append(response);



    });


    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);

    });



}
//function GuardarPdfs() {

//    var data = new FormData();
//    $(".Pdfs").each(function (index, obj) {
//        var file = $(this).get(0);
//        var filess = file.files;



//        if (file.files && file.files[0]) {

//            data.append('name', 6969);
//            data.append('Type', filess[0].type);
//            data.append('File', filess[0]);


//        }



//    });


//    $.ajax({
//        type: "POST",
//        url: "/PIC/UploadFilesDet",
//        contentType: false,
//        processData: false,
//        data: data,
//        success: function (message) {
//            console.log(message);
//        },
//        error: function () {
//            alert("There was error uploading files!");
//        }
//    });
//}

//function GuardarImagenes() {

//    var data = new FormData();
//    $(".Imagenes").each(function (index, obj) {
//        var file = $(this).get(0);
//        var filess = file.files;



//        if (file.files && file.files[0]) {

//            data.append('name', 6969);
//            data.append('Type', filess[0].type);
//            data.append('File', filess[0]);


//        }



//    });


//    $.ajax({
//        type: "POST",
//        url: "/PIC/UploadFilesDet",
//        contentType: false,
//        processData: false,
//        data: data,
//        success: function (message) {
//            console.log(message);
//        },
//        error: function () {
//            alert("There was error uploading files!");
//        }
//    });
//}

function AddComentario() {

    $('#sectionAddComentario').empty();
    var request = $.ajax({
        url: "/PIC/_AddComentario",
        type: "POST",
        data: JSON.stringify(),
        dataType: "Html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {

        document.getElementById("btnaddComentario").style.display = "initial";
        $('#sectionAddComentario').append(response);
        $.unblockUI();
 
    });


    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);
        $.unblockUI();

    });
}
function EnviarFlujo(accion) {

                var comentario;
                var motivoRechazoElement = document.getElementById("txtMotivoRechazo");

                if (motivoRechazoElement) {
     
                    comentario = motivoRechazoElement.value || 'Aprobado';
                } else {
    
                    comentario = 'Aprobado';
                }

    $.blockUI({ message: messageElementSol });

    var obj2 = {
        secuencial: secuencialRPG,
        accion:accion,
        Form: "RPG",
        comentario: comentario
    };
   
        var request = $.ajax({
            url: "/PIC/EnvioCorreo",
            type: "POST",
            data: JSON.stringify(obj2),
            dataType: "Html",
            contentType: "application/json; charset=utf-8"
        });

        request.done(function (response) {

            $.unblockUI();
            window.location.href = "/PIC/Flujos";


        });


        request.fail(function (jqXHR, textStatus) {
            alert("Proceso no realizado: " + textStatus);
            $.unblockUI();

        });
    





}


$(document).ready(function () {
    $(document).on('change', '.RucVal', function () {

        var ob = {
            ruc: $("#rucproveedor").val()
        }


        if (ob.ruc.length < 13 || ob.ruc.length > 13) {
            toastr.warning("El ruc debe tener 13 digitos.")
            $("#rucproveedor").val("");
        } else {


            var request = $.ajax({
                url: "/PIC/CargarRuc",
                type: "POST",
                data: JSON.stringify(ob),
                dataType: "html",
                contentType: "application/json; charset=utf-8"
            });
            request.done(function (tpm) {
                var objeto = JSON.parse(tpm);
                if (objeto.razonsocialModal !== null) {

                    $("#razonsocial").val(objeto.razonsocialModal);
                    $("#tipoproveedor").val(objeto.TipoProveedor1).outerText;
                    document.getElementById("btnaddProveedor").style.display = "none";

                } else {
                    $("#razonsocial").val("");
                    document.getElementById("btnaddProveedor").style.display = "block";

                }





            });


            request.fail(function (jqXHR, textStatus) {
                alert("Proceso no realizado: " + textStatus);

            });

        }

    })

});



function AddProveedor() {

    var obj1 = {

        numero: $("#rucproveedor").val()
    };

    $('#sectionAddProveedor').empty();

    var request = $.ajax({
        url: "_CreateProveedor",
        type: "POST",
        data: JSON.stringify(obj1),
        dataType: "Html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {

        document.getElementById("btnaddProveedor").style.display = "initial";
        $('#sectionAddProveedor').append(response);


    });


    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);

    });


};



function GrabarProveedor() {

    var obj1 = {

        rucModal: $("#rucModal").val(),
        razonsocialModal: $("#razonsocialModal").val(),
        TipoProveedor1: $("#TipoProveedor1 option:selected").val()

    }
    if (obj1.rucModal !== "" && obj1.razonsocialModal !== "" && obj1.TipoProveedor1 !== "") {

        var request = $.ajax({
            url: "CreateProveedorGrabar",
            type: "POST",
            data: JSON.stringify(obj1),
            dataType: "Html",
            contentType: "application/json; charset=utf-8"
        });

        request.done(function (response) {
            var objeto = JSON.parse(response);
            if (objeto == "ok") {

                $('#sectionAddProveedor').append(response);
                $("#btnaddProoveedor").modal('hide');//ocultamos el modal
                $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
                $('.modal-backdrop').remove();//eliminamos el backdrop del modal
                $("#razonsocial").val(obj1.razonsocialModal)

            }




        });


        request.fail(function (jqXHR, textStatus) {
            alert("Proceso no realizado: " + textStatus);

        });
    } else {
        toastr.warning("Llene todos los campos")
    }




}



//function EnviarFlujoA(formulario) {

//    $.blockUI({ message: messageElementSol });
//    var obj2 = {
//        id: IdCabecera,
//        Secuencial: secuen,
//        Estado: "A",
//        Formulario: formulario,
//        Aprobador: "",
//        Nivel: "",
//        Comentario: $("#txtMotivoRechazo").val()
//    }



//    var request = $.ajax({
//        url: "/PIC/_EnviarFlujoA",
//        type: "POST",
//        data: JSON.stringify(obj2),
//        dataType: "Html",
//        contentType: "application/json; charset=utf-8"
//    });

//    request.done(function (response) {

//        var tpm = JSON.parse(response);
//        if (tpm.Mensaje == "ok") {
//            var objeto = { IdCab: tpm.Data.Id, Form: tpm.Data.Form };

//            var request = $.ajax({
//                url: "/PIC/EnvioCorreo",
//                type: "POST",
//                data: JSON.stringify(objeto),
//                dataType: "Html",
//                contentType: "application/json; charset=utf-8"
//            });

//            request.done(function (response) {

//                $.unblockUI();
//                window.location.href = "/PIC/Flujos";


//            });


//            request.fail(function (jqXHR, textStatus) {
//                alert("Proceso no realizado: " + textStatus);
//                $.unblockUI();

//            });
//        }

//    });


//    request.fail(function (jqXHR, textStatus) {
//        alert("Proceso no realizado: " + textStatus);
//        $.unblockUI();
//    });



//}

function AlcanceDetalle(estado) {
    $.blockUI({ message: messageElementSol });

    var f = document.getElementById("fileAprobacion").files;
    DetGastos.estado = estado;
    DetGastos.codigoEmpleado = $("#codigoempleado").val();
    DetGastos.destino = $("#slDestino option:selected").val();
    DetGastos.motivoAnticipo = $("#motanticefect option:selected").val();
    DetGastos.pais = $("#SlPais option:selected").val();
    DetGastos.AutoFinanciera = $("#SlctGerentes option:selected").val();
    DetGastos.ciudad = $("#Ciudades option:selected").val();
    DetGastos.usaTc = $("#slTC option:selected").val();
    DetGastos.Comentario = $("#Comentarios").val();
    if (f.length > 0) {
        DetGastos.namefile = f[0].name;

    }

    DetGastos.fechadesde = $("#fechadesde").val();
    DetGastos.fechahasta = $("#fechahasta").val();
    DetGastos.nivel1 = $("#txtareanivel2").val();
    DetGastos.nivel2 = $("#txtareanivel3").val();
    DetGastos.cantInvitados = conteoAsist;

    var request = $.ajax({
        url: "/PIC/_AlcanceDetalle",
        type: "POST",
        data: JSON.stringify(DetGastos),
        dataType: "Html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {
        GuardarPdfs()
        toastr.success("Se genero Correctamente");
        var tpm = JSON.parse(response);
        $.unblockUI();


        if (estado == "G") {
            window.location.href = "/PIC/MisReportes";
        } else {
            var request = $.ajax({
                url: "/PIC/EnvioNivel1",
                type: "POST",
                data: JSON.stringify(),
                dataType: "Html",
                contentType: "application/json; charset=utf-8"
            });

            request.done(function (response) {


                var objeto = JSON.parse(response);

                var request = $.ajax({
                    url: "/PIC/EnvioCorreo",
                    type: "POST",
                    data: JSON.stringify(objeto),
                    dataType: "Html",
                    contentType: "application/json; charset=utf-8"
                });

                request.done(function (response) {
                    console.log("se envio")
                    $.unblockUI();
                    window.location.href = "/PIC/Flujos";


                });
                request.fail(function (jqXHR, textStatus) {
                    alert("Proceso no realizado: " + textStatus);
                    $.unblockUI();
                });

            });


            request.fail(function (jqXHR, textStatus) {
                alert("Proceso no realizado: " + textStatus);
                $.unblockUI();

            });

        }
    });


    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);
        $.unblockUI();
    });



}
function GuardarFae(estado) {
    $.blockUI({ message: messageElementSol });

    var f = document.getElementById("fileAprobacion").files;
    DetGastos.estado = estado;
    DetGastos.codigoEmpleado = $("#codigoempleado").val();
    DetGastos.destino = $("#slDestino option:selected").val();
    DetGastos.motivoAnticipo = $("#motanticefect option:selected").val();
    DetGastos.pais = $("#SlPais option:selected").val();
    DetGastos.AutoFinanciera = $("#SlctGerentes option:selected").val();
    DetGastos.ciudad = $("#Ciudades option:selected").val();
    DetGastos.usaTc = $("#slTC option:selected").val();
    DetGastos.Comentario = $("#Comentarios").val();
    if (f.length > 0) {
        DetGastos.namefile = f[0].name;

    }

    DetGastos.fechadesde = $("#fechadesde").val();
    DetGastos.fechahasta = $("#fechahasta").val();
    DetGastos.nivel1 = $("#txtareanivel2").val();
    DetGastos.nivel2 = $("#txtareanivel3").val();
    DetGastos.cantInvitados = conteoAsist;

    var request = $.ajax({
        url: "/PIC/_CreateTransactionFae",
        type: "POST",
        data: JSON.stringify(DetGastos),
        dataType: "Html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {
        GuardarPdfs()
        toastr.success("Se genero Correctamente");
        var tpm = JSON.parse(response);



        if (estado == "G") {
            window.location.href = "/PIC/MisReportes";
        } else {
            var request = $.ajax({
                url: "/PIC/EnvioNivel1",
                type: "POST",
                data: JSON.stringify(),
                dataType: "Html",
                contentType: "application/json; charset=utf-8"
            });

            request.done(function (response) {


                var objeto = JSON.parse(response);

                var request = $.ajax({
                    url: "/PIC/EnvioCorreo",
                    type: "POST",
                    data: JSON.stringify(objeto),
                    dataType: "Html",
                    contentType: "application/json; charset=utf-8"
                });

                request.done(function (response) {
                    console.log("se envio")
                    $.unblockUI();
                    window.location.href = "/PIC/Flujos";


                });
                request.fail(function (jqXHR, textStatus) {
                    alert("Proceso no realizado: " + textStatus);
                    $.unblockUI();
                });

            });


            request.fail(function (jqXHR, textStatus) {
                alert("Proceso no realizado: " + textStatus);
                $.unblockUI();

            });

        }
    });


    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);
        $.unblockUI();
    });



}


function Guardar(estado) {

    var tableData = miTablaRPG.rows().data();
    tableData.each(function (data, index) {

        DetConsumo.DetallConsumo[index] = {};
        DetConsumo.DetallConsumo[index].idtipoconsumo = data[15];
        DetConsumo.DetallConsumo[index].idmoneda = data[16];
        DetConsumo.DetallConsumo[index].tcambio = data[2];
        DetConsumo.DetallConsumo[index].monto = data[3];
        DetConsumo.DetallConsumo[index].valor = data[5];
        DetConsumo.DetallConsumo[index].rucproveedor = data[7];
        DetConsumo.DetallConsumo[index].razonsocial = data[8];
        DetConsumo.DetallConsumo[index].numerofactura = data[10];
        DetConsumo.DetallConsumo[index].fechafactura = data[11];
        DetConsumo.DetallConsumo[index].descripcionGastos = data[12];
        DetConsumo.DetallConsumo[index].Cantinvitados = data[14];
        DetConsumo.DetallConsumo[index].idtipoproveedor = data[20];
        DetConsumo.DetallConsumo[index].idtipofactura = data[21];
        DetConsumo.DetallConsumo[index].ArrayInvitados = data[19];
        //DetConsumo.DetallConsumo[index].fileURL = data[18];
        DetConsumo.DetallConsumo[index].idFile = data[22];
        DetConsumo.DetallConsumo[index].archivoprueba = data[23];

    });
    if ($("#nombre").val() != null && DetConsumo.DetallConsumo.length > 0) {
        $.blockUI({ message: messageElementSol });
        DetConsumo.codigoEmpleado = $("#codigoempleado").val();
        DetConsumo.motivogasto = $("#motivogasto").val();
        DetConsumo.slctTipo = $("#slctTipo option:selected").val();
        DetConsumo.fechadesde = $("#fechadesde").val();
        DetConsumo.fechahasta = $("#fechahasta").val();
        DetConsumo.IdAutoridadFinanciera = $("#SlctGerentes option:selected").val();
        DetConsumo.IdDirector = $("#SlctDirectores option:selected").val(); 
        DetConsumo.CodCentroCostoA = $("#SlctCentroCostoa option:selected").val();
        DetConsumo.CentroCosto = $("#centrocosto").val();
        DetConsumo.Nivel1 = $("#txtareanivel1").val();
        DetConsumo.Nivel2 = $("#txtareanivel2").val();
        DetConsumo.Nivel3 = $("#txtareanivel3").val();
        DetConsumo.estado = estado;
        DetConsumo.secuencial = secuencialRPG;

  
        var formData = new FormData();

        // Agregar propiedades al objeto FormData
        formData.append("codigoEmpleado", DetConsumo.codigoEmpleado);
        formData.append("motivogasto", DetConsumo.motivogasto);
        formData.append("slctTipo", DetConsumo.slctTipo);
        formData.append("fechadesde", DetConsumo.fechadesde);
        formData.append("fechahasta", DetConsumo.fechahasta);
        formData.append("IdAutoridadFinanciera", DetConsumo.IdAutoridadFinanciera);
        formData.append("IdDirector", DetConsumo.IdDirector);
        formData.append("CodCentroCostoA", DetConsumo.CodCentroCostoA);
        formData.append("CentroCosto", DetConsumo.CentroCosto);
        formData.append("Nivel1", DetConsumo.Nivel1);
        formData.append("Nivel2", DetConsumo.Nivel2);
        formData.append("Nivel3", DetConsumo.Nivel3);
        formData.append("estado", DetConsumo.estado); 
        formData.append("secuencial", DetConsumo.secuencial); 
        formData.append("accion", estado);
        formData.append("comentario", "");


        for (var i = 0; i < DetConsumo.DetallConsumo.length; i++) {
            var detalle = DetConsumo.DetallConsumo[i];
         
            
                formData.append('DetallConsumo[' + i + '].idtipoproveedor', detalle.idtipoproveedor);
                formData.append('DetallConsumo[' + i + '].archivoprueba', detalle.archivoprueba);
                formData.append('DetallConsumo[' + i + '].idFile', detalle.idFile);
                formData.append('DetallConsumo[' + i + '].idmoneda', detalle.idmoneda);
                formData.append('DetallConsumo[' + i + '].tcambio', detalle.tcambio);
                formData.append('DetallConsumo[' + i + '].monto', detalle.monto);
                formData.append('DetallConsumo[' + i + '].idtipoproveedor', detalle.idtipoproveedor);
                formData.append('DetallConsumo[' + i + '].rucproveedor', detalle.rucproveedor);
                formData.append('DetallConsumo[' + i + '].numerofactura', detalle.numerofactura);
                formData.append('DetallConsumo[' + i + '].idtipofactura', detalle.idtipofactura);
                formData.append('DetallConsumo[' + i + '].fechafactura', detalle.fechafactura);
            formData.append('DetallConsumo[' + i + '].idtipoconsumo', detalle.idtipoconsumo);
            formData.append('DetallConsumo[' + i + '].valor', detalle.valor);
 
                formData.append('DetallConsumo[' + i + '].descripcionGastos', detalle.descripcionGastos);
            if (detalle.ArrayInvitados && detalle.ArrayInvitados.length > 0) {
                if (detalle.ArrayInvitados.length > 0) {


                    for (var x = 0; x < detalle.ArrayInvitados.length; x++) {

                        console.log(detalle.ArrayInvitados)
                        formData.append('DetallConsumo[' + i + '].ArrayInvitados[' + x + '].Nombre', detalle.ArrayInvitados[x].nombre);
                        formData.append('DetallConsumo[' + i + '].ArrayInvitados[' + x + '].Cargo', detalle.ArrayInvitados[x].cargo);
                        formData.append('DetallConsumo[' + i + '].ArrayInvitados[' + x + '].Empresa', detalle.ArrayInvitados[x].empresa);
                    }

                }




            }
        

           
            
        }

        request = $.ajax({
            url: "/PIC/_CreateTransaction",
            type: "POST",
            data: formData, // Usar formData en lugar de JSON
            dataType: "Html",
            processData: false, // Establecer en false para que jQuery no procese los datos
            contentType: false // Establecer en false para que jQuery no establezca el contentType
        });

        request.done(function (response) {
            var tpm = JSON.parse(response);
            
            //GUARDA
            if (estado == "G") {

              
                if (tpm.Mensaje == "ok") {
                    $.unblockUI();  
                    toastr.success("Se genero Correctamente");
                    window.location.href = "/PIC/MisReportes";

                } else {
                    $.unblockUI();
                    toastr.error("Error. " + tpm.Mensaje);
                }

            } else {
            //GUARDA Y ENVIA

                if (tpm.Mensaje == "ok") {

           
                    var objeto = { secuencial: tpm.Data.secuencial, Form: tpm.Data.Form, accion: 'P' };
                    var request = $.ajax({
                        url: "/PIC/EnvioCorreo",
                        type: "POST",
                        data: JSON.stringify(objeto),
                        dataType: "Html",
                        contentType: "application/json; charset=utf-8"
                    });

                    request.done(function (response) {
                
                        $.unblockUI();
                        window.location.href = "/PIC/Flujos";


                    });
                    request.fail(function (jqXHR, textStatus) {
                        alert("Proceso no realizado: " + textStatus);
                        $.unblockUI();
                    });

                } else {

                    $.unblockUI();
                    toastr.error("Error. " + tpm.Mensaje);
                }
               

            }
            
           
        });

        request.fail(function (jqXHR, textStatus) {
            alert("Proceso no realizado: " + textStatus);
            $.unblockUI();
        });


    } else {

        toastr.warning("Asegurese de completar todos los campos!")
    }


}











function EliminarImagen(IdFile) {
    var as = {
        Id: IdFile
    }


    var request = $.ajax({
        url: "/PIC/EliminarImagen",
        type: "POST",
        data: JSON.stringify(as),
        dataType: "Html",
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (response) {
        document.getElementById(IdFile).style.display = "none";

    });
    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);

    });
}
function GenerarInvitados(cantEmpleado) {

    document.getElementById("Generate1").style.display = "Initial";

    var html1 = "";
    html1 += '<div id="Invitadooo-' + contInv + '" style="background-color:#F2F2F2;padding:20px; box-sizing:border-box"><div>  <label>Nombre <abbr style="color:red">*</abbr></label> <input class="form-control" type="text" id="nombreI-' + contInv + '" name="nombreI-' + contInv + '" value="" /></div>';
    html1 += '<div> <label>Cargo <abbr style="color:red">*</abbr></label>  <input class="form-control" onkeyup="validar(' + contInv + ')"  type="text" id="cargoI-' + contInv + '" name="cargoI-' + contInv + '" value="" />   </div>';
    html1 += '<div><label>Empresa <abbr style="color:red">*</abbr></label>  <input class="form-control" type="text" id="empresaI-' + contInv + '"  name="empresaI-' + contInv + '" value="" /> </div>';
    html1 += '<div style="padding:20px; box-sizing:border-box"><button onclick="EliminarInvitado(' + contInv + ');" title="Eliminar Invitado" type="button"  style="border: none">  <svg xmlns="http://www.w3.org/2000/svg" width="30" style="color:red" height="30" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16"><path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/></svg></button></div></div>';
    $("#Generate").append(html1);
    contInv = contInv + 1;


}




//function GenerateInputFile() {

//    /*  document.getElementById("Generate1").style.display = "Initial";*/


//    var html1 = "";
//    html1 += '<input type="file" class="form-control-file Imagenes" name="fileJPG-' + contInput + '" id="fileJPG-' + contInput + '" value="" accept="image/jpeg ,image/png" style="display:none" />';
//    html1 += '<input type="file" class="form-control-file Pdfs" name="filePDF-' + contInput + '" id="filePDF-' + contInput + '" value="" style="display:block" accept="application/pdf" />';

//    $("#GenerateInputFile").append(html1);


//}


function EliminarInvitado(a) {

    if (a !== 4555) {
        if (document.getElementById("Invitadooo-" + a) !== null) {
            $("#Invitadooo-" + a).hide(800)

            /* document.getElementById("Invitadooo-" + a).style.display = "none";*/
            document.getElementById('Invitadooo-' + a).id = 'Eliminado';
            document.getElementById('cargoI-' + a).id = 'Eliminado';
            document.getElementById('empresaI-' + a).id = 'Eliminado';
            document.getElementById('nombreI-' + a).id = 'Eliminado';
            document.getElementsByName('Invitadooo-' + a).name = 'Eliminado';
            document.getElementsByName('cargoI-' + a).name = 'Eliminado';
            document.getElementsByName('empresaI-' + a).name = 'Eliminado';
            document.getElementsByName('nombreI-' + a).name = 'Eliminado';
        }
    } else {
        for (var a = 0; a <= 100; a++) {


            if (document.getElementById("Invitadooo-" + a) !== null) {
                $("#Invitadooo-" + a).hide(800)
                $("#Generate1").hide(800)
                document.getElementById('Invitadooo-' + a).id = 'Eliminado';
                document.getElementById('cargoI-' + a).id = 'Eliminado';
                document.getElementById('empresaI-' + a).id = 'Eliminado';
                document.getElementById('nombreI-' + a).id = 'Eliminado';
                document.getElementsByName('Invitadooo-' + a).name = 'Eliminado';
                document.getElementsByName('cargoI-' + a).name = 'Eliminado';
                document.getElementsByName('empresaI-' + a).name = 'Eliminado';
                document.getElementsByName('nombreI-' + a).name = 'Eliminado';
            }
        }
    }



}

 

function LimpiarVariable() {
    document.ready = document.getElementById("tipoconsumo").value = '0';
  /*  document.ready = document.getElementById("slMoneda").value = '0';*/
    document.ready = document.getElementById("tipoproveedor").value = '0';

    document.ready = document.getElementById("tipofactura").value = '0';
    /*    document.ready = document.getElementById("slDestino").value = '0';*/


    $("#txtTcambio").val("");
    $("#txtMonto").val("");
    $("#txtInvitados").val("");
    $("#valor").val("");

    $("#rucproveedor").val("");
    $("#razonsocial").val("");

    $("#numerofactura").val("");
    $("#fechafactura").val("");
    $("#descripcionGastos").val("");
    document.getElementById("fileRPG").value = "";
}
//function RenderTable(producto) {

//    $("#tbl_list").empty();

//    var html = '<table id="tbl_detalleConsumo" class="table"><thead><tr>';

//    html += '<th>Tipo de consumo</th>';
//    html += '<th>Valor</th>';
//    html += '<th>Razon Social</th>';
//    html += '<th>Destino</th>';
//    html += '<th>N.º Invitados</th>';
//    html += '<th>- Opciones - </th>';
//    html += '</tr></thead>';



//    $.each(DetConsumo.DetallConsumo, function (index, item) {

//        if (item !== undefined) {
//            var montoInt = parseInt(item.monto);
//            var idtipoconsumoInt = parseInt(item.idtipoconsumo);
//            if (montoInt > 15 && item.namefile == "" && (idtipoconsumoInt !== 10 && idtipoconsumoInt !== 12)) {

//                html += '<tbody> <tr style="background-color:#F4B084">';




//                html += '<td>' + item.tipoconsumo + '</td>';
//                html += '<td> $ ' + parseFloat(item.valor).toFixed(2) + '</td>';
//                html += '<td>' + (item.razonsocial == "" ? "N/D" : item.razonsocial) + '</td>';
//                html += '<td>' + (item.destinoD == "1" ? "Nacional" : "Exterior") + '</td>';
//                /*html += '<td> <a href="javascript:invitados( ' + index + ')"   data-toggle="modal"   data-target="#detinvitados" > ' + item.ArrayInvitados.length + '  </a></td>';*/
//                html += '<td> <a href="#"  onclick="Invitados( ' + index + ')""  data-toggle="modal"   data-target="#detInvitados" > ' + item.ArrayInvitados.length + '  </a></td>';
//                /*           html += '<td  style = "display:none" >' + index + '<class="hide"/td>';*/

//                html += '<td><a style="  background-color: #52B63F;"  data-toggle="modal" id="btnView" class="btn btn-3d btn-quaternary" data-target="#detConsumo" onclick="DetalleConsumo(' + index + ');" ><i class="fas fa-search"></i></a>|';
//                html += ' <button id="btnDelete" type="button"  style="  background-color: #CF152D;" class="btn btn-3d btn-quaternary" onClick="DeleteFactura(\'' + item.id + '\')"><i class="fas fa-trash-alt"></i></button>|';

//                html += '<a id="btnEdit" style="  background-color: blue;" class="btn btn-3d btn-quaternary" onClick="EditConsumo(\'' + item.id + '\')"><i class="fa fa-edit"></i></a> </td>';
//                html += '</tr> ';
//                toastr.warning("No adjunto soporte!");

//            } else {
//                html += '<tbody> <tr>';

//                html += '<td>' + item.tipoconsumo + '</td>';
//                html += '<td> $ ' + parseFloat(item.valor).toFixed(2) + '</td>';
//                html += '<td>' + (item.razonsocial == "" ? "N/D" : item.razonsocial) + '</td>';
//                html += '<td>' + (item.destinoD == "1" ? "Nacional" : "Exterior") + '</td>';
//                /*html += '<td> <a href="javascript:invitados( ' + index + ')"   data-toggle="modal"   data-target="#detinvitados" > ' + item.ArrayInvitados.length + '  </a></td>';*/
//                html += '<td> <a href="#"  onclick="Invitados( ' + index + ')""  data-toggle="modal"   data-target="#detInvitados" > ' + item.ArrayInvitados.length + '  </a></td>';
//                /*           html += '<td  style = "display:none" >' + index + '<class="hide"/td>';*/

//                html += '<td><a style="  background-color: #52B63F;"  data-toggle="modal" id="btnView" class="btn btn-3d btn-quaternary" data-target="#detConsumo" onclick="DetalleConsumo(' + index + ');" ><i class="fas fa-search"></i></a>|';
//                html += ' <button id="btnDelete" type="button"  style="  background-color: #CF152D;" class="btn btn-3d btn-quaternary" onClick="DeleteFactura(\'' + item.id + '\')"><i class="fas fa-trash-alt"></i></button>|';

//                html += '<a id="btnEdit" style="  background-color: blue;" class="btn btn-3d btn-quaternary" onClick="EditConsumo(\'' + item.id + '\')"><i class="fa fa-edit"></i></a> </td>';
//                html += '</tr> ';

//            }

//        }
//    });
//    html += '</tbody></table>';







//    $("#tbl_list").append(html);
//}

//function RenderTableRevisor(producto) {

//    $("#tbl_list").empty();

//    var html = '<table id="tbl_detalleConsumo" class="table"><thead><tr>';

//    html += '<th>Tipo de consumo</th>';
//    html += '<th>Valor</th>';
//    html += '<th>Razon Social</th>';
//    html += '<th>Destino</th>';
//    html += '<th>N.º Invitados</th>';
//    html += '<th>- Opciones - </th>';
//    html += '</tr></thead>';



//    $.each(DetConsumo.DetallConsumo, function (index, item) {

//        if (item !== undefined) {


//            var montoInt = parseInt(item.monto);
//            var idtipoconsumoInt = parseInt(item.idtipoconsumo);
//            if (montoInt > 15 && item.namefile == "" && (idtipoconsumoInt !== 10 && idtipoconsumoInt !== 12)) {

//                html += '<tbody> <tr style="background-color:   ">';
//                html += '<td>' + item.tipoconsumo + '</td>';
//                html += '<td> $ ' + parseFloat(item.valor).toFixed(2) + '</td>';
//                html += '<td>' + (item.razonsocial == "" ? "N/D" : item.razonsocial) + '</td>';
//                html += '<td>' + (item.destino == "1" ? "Nacional" : "Exterior") + '</td>';
//                /*html += '<td> <a href="javascript:invitados( ' + index + ')"   data-toggle="modal"   data-target="#detinvitados" > ' + item.ArrayInvitados.length + '  </a></td>';*/
//                html += '<td> <a href="#"  onclick="Invitados( ' + index + ')""  data-toggle="modal"   data-target="#detInvitados" > ' + item.ArrayInvitados.length + '  </a></td>';
//                /*           html += '<td  style = "display:none" >' + index + '<class="hide"/td>';*/

//                html += '<td><a style="  background-color: #52B63F;"  data-toggle="modal" id="btnView" class="btn btn-3d btn-quaternary" data-target="#detConsumo" onclick="DetalleConsumo(' + index + ');" ><i class="fas fa-search"></i></a>';
//                html += '</tr> ';
//            } else {
//                html += '<tbody> <tr>';

//                html += '<td>' + item.tipoconsumo + '</td>';
//                html += '<td> $ ' + parseFloat(item.valor).toFixed(2) + '</td>';
//                html += '<td>' + (item.razonsocial == "" ? "N/D" : item.razonsocial) + '</td>';
//                html += '<td>' + (item.destino == "1" ? "Nacional" : "Exterior") + '</td>';
//                /*html += '<td> <a href="javascript:invitados( ' + index + ')"   data-toggle="modal"   data-target="#detinvitados" > ' + item.ArrayInvitados.length + '  </a></td>';*/
//                html += '<td> <a href="#"  onclick="Invitados( ' + index + ')""  data-toggle="modal"   data-target="#detInvitados" > ' + item.ArrayInvitados.length + '  </a></td>';
//                /*           html += '<td  style = "display:none" >' + index + '<class="hide"/td>';*/

//                html += '<td><a style="  background-color: #52B63F;"  data-toggle="modal" id="btnView" class="btn btn-3d btn-quaternary" data-target="#detConsumo" onclick="DetalleConsumo(' + index + ');" ><i class="fas fa-search"></i></a>';
//                html += '</tr> ';

//            }
//        }
//    });
//    html += '</tbody></table>';







//    $("#tbl_list").append(html);
//}

function loadImageInContainer(imageURL) {

    var imageContainer = document.getElementById("imageContainer");

    if (imageURL) {
        var linkElement = document.createElement("a");
        linkElement.href = imageURL;
        linkElement.target = "_blank";
        linkElement.innerHTML = "Ver imagen.."; // Cambia este texto por el que desees mostrar

        var imgElement = document.createElement("img");
        imgElement.src = imageURL;
        imgElement.style.width = "100%";
        imgElement.style.height = "auto";

        linkElement.appendChild(imgElement);

        imageContainer.appendChild(linkElement);
    } else {
        imageContainer.innerHTML = "No hay archivo";
    }
}
function createTempURL(base64String) {
    // Eliminar el prefijo "data:image/png;base64," si está presente
/*    var base64Data = base64String.split(',')[1];*/
    var base64Data = base64String;

    // Convertir la cadena base64 en un objeto Blob
    var contentType = 'image/png'; // Ajusta esto según el tipo de archivo (por ejemplo, 'image/jpeg' para JPEG)
    var byteCharacters = atob(base64Data);
    var byteArrays = [];

    for (var offset = 0; offset < byteCharacters.length; offset += 512) {
        var slice = byteCharacters.slice(offset, offset + 512);
        var byteNumbers = new Array(slice.length);

        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    var blob = new Blob(byteArrays, { type: contentType });

    // Crear una URL temporal utilizando URL.createObjectURL()
    var tempURL = URL.createObjectURL(blob);

    // Devuelve la URL temporal
    return tempURL;
}
function recopilarInvitados() {

    var invitados = [];


    for (var i = 0; i < contInv; i++) {
        var invitadoElement = document.getElementById("Invitadooo-" + i);

        if (invitadoElement !== null) {
            var nombre = $('#nombreI-' + i).val().trim();
            var cargo = $('#cargoI-' + i).val().trim();
            var empresa = $('#empresaI-' + i).val().trim();

            if (nombre.length > 4 && cargo.length > 4 && empresa.length > 3) {
                invitados.push({
                    id: i,
                    nombre: nombre,
                    cargo: cargo,
                    empresa: empresa
                });
            } else {
                todosLosCamposCompletos = false;
            }
        }
    }
 
    return invitados;

}
function clearImageContainer() {
    var imageContainer = document.getElementById("imageContainer");
    imageContainer.innerHTML = "";
}

    function EditConsumo() {
   
    
        var filaSeleccionada = miTablaRPG.row('.selected');

        document.getElementById("tipoReembolsoGastos").style.display = "block";
        document.getElementById("IdDesocultarD").style.display = "block";
        document.getElementById("IdocultarD").style.display = "none";


        if (DetConsumo.DetallConsumo.ArrayInvitados && DetConsumo.DetallConsumo.ArrayInvitados.length > 0) {

            for (var a = 0; a < DetConsumo.DetallConsumo.ArrayInvitados.length; a++) {
                GenerarInvitados();
            }

            for (var e = 0; e < DetConsumo.DetallConsumo.ArrayInvitados.length; e++) {



            
                $('#nombreI-' + e).val(DetConsumo.DetallConsumo.ArrayInvitados[e].nombre);
                $('#cargoI-' + e).val(DetConsumo.DetallConsumo.ArrayInvitados[e].cargo);
                $('#empresaI-' + e).val(DetConsumo.DetallConsumo.ArrayInvitados[e].empresa);


            }
        }

        document.ready = document.getElementById("tipoconsumo").value = DetConsumo.DetallConsumo.idtipoconsumo;
        document.ready = document.getElementById("slMoneda").value = DetConsumo.DetallConsumo.idmoneda;
        document.ready = document.getElementById("tipoproveedor").value = DetConsumo.DetallConsumo.idtipoproveedor;
        document.ready = document.getElementById("tipofactura").value = DetConsumo.DetallConsumo.idtipofactura;
        $("#txtTcambio").val(DetConsumo.DetallConsumo.tcambio);
        $("#txtMonto").val(DetConsumo.DetallConsumo.monto);
        $("#txtInvitados").val(DetConsumo.DetallConsumo.invitados);
        $("#valor").val(DetConsumo.DetallConsumo.valor);
        $("#rucproveedor").val(DetConsumo.DetallConsumo.rucproveedor);
        $("#razonsocial").val(DetConsumo.DetallConsumo.razonsocial);
        $("#numerofactura").val(DetConsumo.DetallConsumo.numerofactura);
        $("#fechafactura").val(DetConsumo.DetallConsumo.fechafactura);
        $("#descripcionGastos").val(DetConsumo.DetallConsumo.descripcionGastos);
        idArchivo = DetConsumo.DetallConsumo.idFile;
        loadImageInContainer(DetConsumo.DetallConsumo.fileURL);
        if (DetConsumo.DetallConsumo.AtencionNegocios == "1") {
            document.getElementById("checkAtencionNegocios").checked = true;
        }

        /* DeleteFacturaSP(idtipoconsumo);*/
        var tableData = miTablaRPG.rows().data();
        calcularValores(tableData);

        filaSeleccionada.remove().draw();
        $('#editar').prop('disabled', true);
        $('#eliminar').prop('disabled', true);


    }

function DeleteFacturaSP(tc) {



    DetConsumo.DetallConsumo = $.grep(DetConsumo.DetallConsumo, function (element, index) {
        if (element !== undefined) {
            return element.id !== parseInt(tc);
        }

    });

    RenderTable(DetConsumo);
    calcularValores(DetConsumo.DetallConsumo);






}
function DeleteFactura(tc) {

    alertify.confirm('<b style="color:black;font-size:20px">CONFIRMAR </b>', 'Seguro desea <b style="color:red"> ELIMINAR</b> el detalle de consumo.',


        function () {
            DetConsumo.DetallConsumo = $.grep(DetConsumo.DetallConsumo, function (element, index) {

                if (element !== undefined) {
                    return element.id !== parseInt(tc);
                }

            });

            RenderTable(DetConsumo);
            calcularValores(DetConsumo.DetallConsumo);


            toastr.success('Ha sido eliminado con éxito.')
        }
        , function () {
            toastr.error('Cancel')
        });


}
function BuscarRepetidos(numfact) {



    if (DetConsumo.DetallConsumo.length > 0) {

        for (var i = 0; i < DetConsumo.DetallConsumo.length; i++) {


            if (DetConsumo.DetallConsumo[i].numerofactura == numfact) {


                return false;

            }
        }

        return true;
    } else {
        return true;
    }

}


$(document).on('change', '.destino', function () {

    $('#tipoproveedor').val($("#slDestino").val()).outerText;
});

$(document).on('change', '.Moneda', function () {

    let opc = $("#slMoneda option:selected").val();

    if (opc == "") {
        document.getElementById("txtTcambio").value = ""
        document.getElementById("MDolar").style.display = "none";
        document.getElementById("MEuro").style.display = "none";
        document.getElementById("MGeneric").style.display = "none";
        document.getElementById("MSol").style.display = "none";
        alertify.warning("ESCOJA UNA MONEDA VALIDA");
    }
    if (opc == "316") /*es dolar*/ {
        document.getElementById("txtTcambio").value = "1"
        document.getElementById("MDolar").style.display = "Initial";
        document.getElementById("MEuro").style.display = "none";
        document.getElementById("MGeneric").style.display = "none";
        document.getElementById("MSol").style.display = "none";

    }
    if (opc == "276") {

        document.getElementById("txtTcambio").value = ""
        document.getElementById("MEuro").style.display = "Initial";
        document.getElementById("MDolar").style.display = "none";
        document.getElementById("MGeneric").style.display = "none";
        document.getElementById("MSol").style.display = "none";
    }
    if (opc == "376") {
        document.getElementById("txtTcambio").value = ""
        document.getElementById("MSol").style.display = "Initial";
        document.getElementById("MEuro").style.display = "none";
        document.getElementById("MDolar").style.display = "none";
        document.getElementById("MGeneric").style.display = "none";
    }
    if (opc != "276" && opc != "376" && opc != "316") {
        document.getElementById("txtTcambio").value = ""
        document.getElementById("MGeneric").style.display = "Initial";
        document.getElementById("MEuro").style.display = "none";
        document.getElementById("MDolar").style.display = "none";
        document.getElementById("MSol").style.display = "none";
    }

});

let calcular = document.getElementsByName("calcularValor");


$(document).on('change', '.calcularValor', function () {

    //var tcambio = document.getElementById("txtTcambio").value;

    var monto = document.getElementById("txtMonto").value;
    var tcambio = document.getElementById("txtTcambio").value;

    if (monto != "" && tcambio != "") {
        var valor = monto / tcambio;
        $("#valor").val(valor);


    }



});
    
function calcularValores(tableData) {
    var totalConsumosCombustibles = 0;
    var totalServiciosFinancieros = 0;
    var totalNacional = 0;
    var totalExterior = 0;
    var entro = 0;
    var entro2 = 0;
    var totalFae = DetConsumo.totalfae;
    var valorTotal = 0;

    // Función para actualizar el correo del textarea
    function actualizarCorreo(selector, correo) {
        $(selector).val(correo);
    }

    if (tableData.length > 0) {
        tableData.each(function (rowData, index) {
            var tipoProveedor = rowData[20];
            var valor = rowData[5] ? parseFloat(rowData[5]) : 0;
            valorTotal += valor;
            var archivo = rowData[18];
            var tipoConsumo = rowData[15];

            // Verificar si se debe adjuntar un archivo
            if (valor > 15 && archivo == null) {
                toastr.warning("Adjunte un archivo.");
                if (entro === 0) {
                    entro = 1;
                    actualizarCorreo('#txtareanivel1', "Rocio.Moran@dole.com");
                }
            }

            // Verificar si el proveedor es exterior
            if (tipoProveedor === "1") {
                if (entro2 === 0) {
                    entro2 = 1;
                    actualizarCorreo('#txtareanivel3', "Alegria.Molina@dole.com");
                }
            }

            // Calcular totales según el tipo de consumo y proveedor
            if (tipoConsumo === 5 || tipoConsumo === 11) {
                if (tipoConsumo === 5) {
                    totalConsumosCombustibles += parseFloat(valor);
                } else {
                    totalServiciosFinancieros += parseFloat(valor);
                }
            } else {
                if (tipoProveedor == 0) {
                    totalNacional += parseFloat(valor);
                } else {
                    totalExterior += parseFloat(valor);
                }
            }
        });
    }

    // Actualizar valores en la interfaz
    function actualizarValor(id, valor) {
        var valorNumerico = (typeof valor === "number" && !isNaN(valor)) ? valor : 0;
       
        document.getElementById(id).innerHTML = "$ " + valorNumerico.toFixed(2);
    }

    actualizarValor('valorReportado', valorTotal);
    actualizarValor('valorCombustible', totalConsumosCombustibles);
    actualizarValor('valorServicios', totalServiciosFinancieros);
    actualizarValor('valorNacional', totalNacional);
    actualizarValor('valorExterior', totalExterior);

    totalFae -= totalFae;

    actualizarValor('valor2', totalFae);
}


//function CalcularValores(tableData) {


//    var ConsumosCombustibles = 0;
//    var ServiciosFinancieros = 0;
//    var Nacional = 0;
//    var Exterior = 0;
//    var entro = 0;
//    var entro2 = 0
//    var totalFae = DetConsumo.totalfae;

//    var TipoProveedor = rowData[20];
//    var valor = rowData[5];
//    var valorReportado = 0;
//    var archivo = rowData[18];
//    var tipoconsumo = rowData[15];
//    $('#txtareanivel3').val("");
//    $('#txtareanivel1').val("maria.mawyin@dole.com");

//    if (tableData.length > 0) {


//        tableData.each(function (rowData, index) {
        
            
//            if (valor > 15 && archivo == null) {

//                toastr.warning("Adjunte un archivo.")
//                if (entro == 0) {
//                    entro = 1;

//                    $('#txtareanivel1').val("");
//                    $('#txtareanivel1').val("Rocio.Moran@dole.com");
//                }

//            }

//            //destino - tipoproveedor == 1 ? exterior : nacional
//            if (TipoProveedor == "1") {
//                if (entro2 == 0) {
//                    entro2 = 1;

//                    $('#txtareanivel3').val("");
//                    $('#txtareanivel3').val("Alegria.Molina@dole.com");

//                }


//            }

//            valorReportado = valorReportado + parseFloat(valor);
//            //rowData[15] = idTipoconsumo
//            if (tipoconsumo == 5 || tipoconsumo == 11) {


//                if (tipoconsumo == 5) {
//                    ConsumosCombustibles = ConsumosCombustibles + parseFloat(valor);
//                } else {
//                    ServiciosFinancieros = ServiciosFinancieros + parseFloat(valor);
//                }


//            } else {
//                if (TipoProveedor == 0) {
//                    Nacional = Nacional + parseFloat(valor);
//                } else {
//                    Exterior = Exterior + parseFloat(valor);
//                }


//            }
//        });
//    }
 

//    document.getElementById('valorReportado').innerHTML = "$ " + valorReportado.toFixed(2);
//    document.getElementById('valorCombustible').innerHTML = "$ " + ConsumosCombustibles.toFixed(2);
//    document.getElementById('valorServicios').innerHTML = "$ " + ServiciosFinancieros.toFixed(2);
//    document.getElementById('valorNacional').innerHTML = "$ " + Nacional.toFixed(2);
//    document.getElementById('valorExterior').innerHTML = "$ " + Exterior.toFixed(2);

//    totalFae = totalFae - valorReportado;

//    document.getElementById('valor2').innerHTML = "$ " + totalFae;



//}



//function CalcularValoresRev(detalle) {


//    var ConsumosCombustibles = 0;
//    var ServiciosFinancieros = 0;
//    var Nacional = 0;
//    var Exterior = 0;
//    var valorReportado = 0;
//    for (var i = 0; i < detalle.length; i++) {

//        if (detalle[i] !== undefined) {





//            valorReportado = valorReportado + parseFloat(detalle[i].valor);
//            // 5  consumos combustibles
//            if (detalle[i].idtipoconsumo == 5 || detalle[i].idtipoconsumo == 11) {


//                if (detalle[i].idtipoconsumo == 5) {
//                    ConsumosCombustibles = ConsumosCombustibles + parseFloat(detalle[i].valor);
//                } else {
//                    ServiciosFinancieros = ServiciosFinancieros + parseFloat(detalle[i].valor);
//                }


//            } else {
//                if (detalle[i].destinoD == 1) {
//                    Nacional = Nacional + parseFloat(detalle[i].valor);
//                } else {
//                    Exterior = Exterior + parseFloat(detalle[i].valor);
//                }


//            }


//        }

//    }



//    document.getElementById('valorReportado').innerHTML = "$ " + valorReportado.toFixed(2);
//    document.getElementById('valorCombustible').innerHTML = "$ " + ConsumosCombustibles.toFixed(2);
//    document.getElementById('valorServicios').innerHTML = "$ " + ServiciosFinancieros.toFixed(2);
//    document.getElementById('valorNacional').innerHTML = "$ " + Nacional.toFixed(2);
//    document.getElementById('valorExterior').innerHTML = "$ " + Exterior.toFixed(2);



//}


function CalcularFechaConsumo(fechaIngresada) {
    let fechaActual = Date.now();
    console.log(fechaActual);

}

// La siguiente funcion valida el elemento input
function validar(a) {
    // Variable que usaremos para determinar si el input es valido
    let isValid = false;

    // El input que queremos validar
    const input = document.forms['formV']["cargoI-" + a];

    //El div con el mensaje de advertencia:
    const message = document.getElementById('message');

    input.willValidate = false;

    // El tamaño maximo para nuestro input
    const maximo = 35;

    // El pattern que vamos a comprobar
    const pattern = new RegExp('^[A-Z]+$', 'i');

    // Primera validacion, si input esta vacio entonces no es valido
    if (!input.value) {
        isValid = false;
    } else {
        // Segunda validacion, si input es mayor que 35
        if (input.value.length > maximo) {
            isValid = false;
        } else {
            // Tercera validacion, si input contiene caracteres diferentes a los permitidos
            if (!pattern.test(input.value)) {
                // Si queremos agregar letras acentuadas y/o letra ñ debemos usar
                // codigos de Unicode (ejemplo: Ñ: \u00D1  ñ: \u00F1)
                isValid = false;
            } else {
                // Si pasamos todas la validaciones anteriores, entonces el input es valido
                isValid = true;
            }
        }
    }

    //Ahora coloreamos el borde de nuestro input
    if (!isValid) {
        // rojo: no es valido
        input.style.borderColor = 'salmon'; // me parece que 'salmon' es un poco menos agresivo que 'red'
        // mostramos mensaje


    } else {
        // verde: si es valido
        input.style.borderColor = 'palegreen'; // 'palegreen' se ve mejor que 'green' en mi opinion
        // ocultamos mensaje;

    }

    // devolvemos el valor de isValid
    return isValid;
}



$(document).on('change', '.fechadh', function () {

    var fechah = $("#fechahasta").val();
    var fechad = $("#fechadesde").val();
    if (fechah != "" && fechad != "") {
        if (fechad > fechah) {
            toastr.error("Fecha desde no puede ser mayor a fecha hasta");
            $("#fechadesde").val("");
            $("#fechahasta").val("");
        }

    }


});

function Refresh() {

    //url: "@Url.Action("_CreateProveedor", "PIC")",
    //    url: "/PIC/CargaEmpleado",
    fetch("/PIC/Refresh" + "?Id=" + "1")
        .then(function (result) {
            if (result.ok) {
                return result.json();
            }
        })
        .then(function (data) {

            cbo = document.getElementById("SlctDirectores1");
            cbo.innerHTML = "";
            data.forEach(function (element) {

                let opt = document.createElement("option");
                opt.appendChild(document.createTextNode(element.Text));
                opt.value = element.Value;


                cbo.appendChild(opt);
            })
        })
}


function CodigoEmpleado() {



    empleado =
    {
        codigo: $("#codigoempleado").val(),
        centroCosto: ""
    }

    var request = $.ajax({
        url: "/PIC/CargaEmpleado",
        type: "POST",
        data: JSON.stringify(empleado),
        dataType: "html",
        contentType: "application/json; charset=utf-8"
    });
    request.done(function (tpm) {

        var tpm = JSON.parse(tpm);
        console.log(tpm.CorreoAutoridadF)
        $('#txtareanivel2').val(tpm.CorreoAutoridadF);
        if (tpm.Bandera != "V") {


            if (tpm.Bandera == "G" || tpm.Bandera == "D") {


                document.getElementById("SlctGerentes").style.display = "none";
                document.getElementById("afff").style.display = "none";
                document.getElementById("af").style.display = "none";
                document.getElementById("afi").style.display = "none";
                document.getElementById("SlctDirectores").style.display = "block";
                document.getElementById("dir").style.display = "block";
                if (tpm.Bandera == "D") {

                    $("select#SlctDirectores1 option[value='" + tpm.Codigo + "']").remove();

                } else {

                    $('#SlctDirectores1').val(tpm.CodAreaResponsable).outerText;
                    /*  $("#SlctDirectores1 option:selected").(tpm.CodigoDirector)*/
                }

            }
            else {

                document.getElementById("SlctGerentes").style.display = "";
                document.getElementById("afff").style.display = "none";
                document.getElementById("af").style.display = "";
                document.getElementById("afi").style.display = "";
                document.getElementById("SlctDirectores").style.display = "none";
                document.getElementById("dir").style.display = "none";
                document.getElementById("SlctGerentes").disabled = true;
            }
            $("#asistentes").val(tpm.Nombre)
            $("#nombre").val(tpm.Nombre)
            $("#departamento").val(tpm.Departamento)
            $("#compania").val(tpm.Compania)
            $("#centrocosto").val(tpm.CentroCosto)
            $("#area").val(tpm.Area)
            empleado.centroCosto = tpm.CentroCosto
            $('#SlctGerentes').val(tpm.CodAreaResponsable).outerText;


        } else {
            $("#nombre").val("")
            $("#departamento").val("")
            $("#compania").val("")
            $("#centrocosto").val("")
            $("#area").val("")
            toastr.error("Ingrese un código valido")
        }

        LimpiarCentroCostoa()

    });


    request.fail(function (jqXHR, textStatus) {
        alert("Proceso no realizado: " + textStatus);

    });

}
function LimpiarCentroCostoa() {
    document.getElementById("checkIn").checked = false
    document.getElementById("SlctCentroCostoa").disabled = true;
    document.getElementById("SlctCentroCostoa").value = 0;
    $("#centrocosto").val(empleado.centroCosto)
}

function checkIn1() {
    if (document.getElementById("checkIn").checked == true) {
        $("#centrocosto").val("")
        document.getElementById("SlctCentroCostoa").disabled = false;
        /*     document.getElementById("checkEx").checked = false;*/
    } else {
        LimpiarCentroCostoa()
    }

}


function CheckDirector() {
    if (document.getElementById("CheckDirect").checked == true) {
        document.getElementById("SlctDirectores").style.display = "block";
        /*            document.getElementById("SlctGerentes").style.display = "none";*/
        document.getElementById("SlctGerentes").value = "";
        document.getElementById("dir").style.display = "block";

        document.getElementById("SlctGerentes").disabled = true;
        $("#SlctAutoridadFinan").val("");
    } else {
        document.getElementById("dir").style.display = "none";
        document.getElementById("SlctDirectores").style.display = "none";
        /*        document.getElementById("SlctGerentes").style.display = "block";*/
        document.getElementById("SlctGerentes").disabled = false;

    }


}



function GenerarAsitente() {

    conteoAsist = conteoAsist + 1;
    CalcularTotal();
    $("#conteoAsist").val(conteoAsist);
    document.getElementById("GenerateAsistente").style.display = "Initial";

    var html1 = "";
    html1 += '  <div  style="display:initial" id="EliminarAsistent-' + conteoAsist + '" > <div class="row"><div class="col-md-6"></div> <div class="col-md-1" style="text-align:right; border:0;padding:0"> <button type="button" title="Eliminar asistente" onclick="EliminarAsistente(' + conteoAsist + ');" class="btn btn-success" id="btnEliminarAsistent-' + conteoAsist + '" style="border: none; background-color: #FFFFFF"><svg xmlns="http://www.w3.org/2000/svg" width="28" height="28" style="color:red" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">';
    html1 += '<path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z" />';
    html1 += ' </svg>  </button></div ><div class="col-md-3"> <input type="text" name="asistentes-' + conteoAsist + '" id="asistentes-' + conteoAsist + '" class="form-control" value="" /> </div> <div class="col-md-1"></div> </div > <br> </div >';
    $("#GenerateAsistente").append(html1);
    /*    contInv = contInv + 1;*/

}


function EliminarAsistente(a) {


    if (a !== 559) {
        document.getElementById("EliminarAsistent-" + a).style.display = "none";
        document.getElementById('EliminarAsistent-' + a).id = 'Eliminado';
        document.getElementById('asistentes-' + a).id = 'Eliminado';
        conteoAsist = conteoAsist - 1;
        CalcularTotal();
        $("#conteoAsist").val(conteoAsist);
    } else {
        for (var a = 0; a <= 50; a++) {

            if (document.getElementById("EliminarAsistent-" + a) !== null) {

                document.getElementById("EliminarAsistent-" + a).style.display = "none";
                document.getElementById('EliminarAsistent-' + a).id = 'Eliminado';
                document.getElementById('asistentes-' + a).id = 'Eliminado';


            }

        }
        conteoAsist = 1;

        $("#conteoAsist").val(conteoAsist);

    }


    //if (document.getElementById("EliminarAsistent-" + a) !== null) {
    //        $("#Invitadooo-" + a).hide(800)

    //        /* document.getElementById("Invitadooo-" + a).style.display = "none";*/
    //        document.getElementById('Invitadooo-' + a).id = 'Eliminado';
    //        document.getElementById('cargoI-' + a).id = 'Eliminado';
    //        document.getElementById('empresaI-' + a).id = 'Eliminado';
    //        document.getElementById('nombreI-' + a).id = 'Eliminado';
    //        document.getElementsByName('Invitadooo-' + a).name = 'Eliminado';
    //        document.getElementsByName('cargoI-' + a).name = 'Eliminado';
    //        document.getElementsByName('empresaI-' + a).name = 'Eliminado';
    //        document.getElementsByName('nombreI-' + a).name = 'Eliminado';
    //    }
    //} else {
    //    for (var a = 0; a <= 100; a++) {


    //        if (document.getElementById("Invitadooo-" + a) !== null) {
    //            $("#Invitadooo-" + a).hide(800)
    //            $("#Generate1").hide(800)
    //            document.getElementById('Invitadooo-' + a).id = 'Eliminado';
    //            document.getElementById('cargoI-' + a).id = 'Eliminado';
    //            document.getElementById('empresaI-' + a).id = 'Eliminado';
    //            document.getElementById('nombreI-' + a).id = 'Eliminado';
    //            document.getElementsByName('Invitadooo-' + a).name = 'Eliminado';
    //            document.getElementsByName('cargoI-' + a).name = 'Eliminado';
    //            document.getElementsByName('empresaI-' + a).name = 'Eliminado';
    //            document.getElementsByName('nombreI-' + a).name = 'Eliminado';
    //        }
    //    }


}