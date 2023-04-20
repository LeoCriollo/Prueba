
        //#region DataTableCriollao

        var nametabla = "#tabla-invitados";
        var obj = {
            nombre: "",
            empresa: "",
            cargo: ""
        }

        var miTabla;

        $(document).ready(function () {
            miTabla = $(nametabla).DataTable({
                columns: [
                  
                    { title: "Nombre" },
                    { title: "Empresa" },
                    { title: "Cargo" }
                ]
            });


        });

        // Maneja el evento click en un tr de la tabla
        $('#tabla-invitados tbody').on('click', 'tr', function () {

            // Deselecciona todas las filas de la tabla
            miTabla.$('tr.selected').removeClass('selected');

            // Selecciona la fila actual
            $(this).addClass('selected');
       
            var data = miTabla.row(this).data();
         
            obj.id = $(this).data('id');
            obj.nombre = data[0];
            obj.empresa = data[1];
            obj.cargo = data[2];


            $('#editar').prop('disabled', false);
            $('#eliminar').prop('disabled', false);
        });

        function EditarDataTable() {
  
            var nombre = document.getElementById("nombre").value;
            var empresa = document.getElementById("empresa").value;
            var cargo = document.getElementById("cargo").value;

            if (!miTabla.row('.selected').any()) {
                alert('Por favor seleccione una fila para editar.');
                return;
            }


            var filaSeleccionada = miTabla.row('.selected');
        

            filaSeleccionada.data([ nombre, empresa, cargo]).draw();
           


}

        let messageElementSol;
        $(document).ready(function () {
            messageElementSol = $('#divMessageSol');


        });

        function InicializarDataTable() {

            $(nametabla).DataTable().destroy();

            var miTabla = $(nametabla).DataTable({
                columns: [

                    { title: "Nombre" },
                    { title: "Empresa" },
                    { title: "Cargo" }
                ]
            });
            miTabla.$('tr.selected').removeClass('selected');
            $('#editar').prop('disabled', true);
            $('#eliminar').prop('disabled', true);
        }


        function Nuevo() {

            var nombre = document.getElementById("nombre").value;
            var empresa = document.getElementById("empresa").value;
            var cargo = document.getElementById("cargo").value;

            miTabla.$('tr.selected').removeClass('selected');
            var data = [nombre, empresa, cargo];
            miTabla.row.add(data).draw();
      
        }
        function EliminarDataTable() {

            var filaSeleccionada = miTabla.row('.selected');
            if (!filaSeleccionada.any()) {
                alert('Por favor seleccione una fila para eliminar.');
                return;
            }
            filaSeleccionada.remove().draw();
            $('#editar').prop('disabled', true);
            $('#eliminar').prop('disabled', true);
}




            function loadDetalleDatatable(editMode, dataObj) {
                var data = {};

                if (editMode) {
                    if (obj.id) {
                        data = {
                            id: obj.id
                        };
                    } else {
                        data = dataObj;
                    }
                }

                $('#sectionDetInvitado').empty();

                var request = $.ajax({
                    url: "/PIC/_DetalleDatatable",
                    type: "POST",
                    data: JSON.stringify(data),
                    dataType: "html",
                    contentType: "application/json; charset=utf-8"
                });

                request.done(function (response) {
                    $('#sectionDetInvitado').append(response);
                });

                request.fail(function (jqXHR, textStatus) {
                    alert("Proceso no realizado: " + textStatus);
                });
            }



            function DetalleConsumo() {
                document.getElementById("btnNuevo").style.display = "initial";
                document.getElementById("btnEditar").style.display = "none";
                $('#smallModalLabel').html("Create New");
                loadDetalleDatatable(false);
            }

            function Editar() {
                document.getElementById("btnNuevo").style.display = "none";
                document.getElementById("btnEditar").style.display = "initial";
                $('#smallModalLabel').html("Edit");
                //$('#nombre').val(obj.nombre);
                //$('#empresa').val(obj.empresa);
                //$('#cargo').val(obj.cargo);
           
                var data = {
                    nombre: obj.nombre,
                    empresa: obj.empresa,
                    cargo: obj.cargo
                };
                loadDetalleDatatable(true, data);
            }


        //#endregion
