/// <reference path="jquery-2.1.1.js" />

$(function () {

    var correlativo = 0;

    $('#btn-agregar').click(function (e) {
        e.preventDefault();

        var $fila = $('<tr/>');
        var $celdaA = $('<td />');
        var $celdaB = $('<td />');
        var $celdaC = $('<td />');

        var prefijo = 'archivos[' + correlativo + ']';

        var $hidden = $('<input />', { type: 'hidden', value: correlativo, name: 'archivos.index' });
        var $dropDown = $('<select />', { name: prefijo + '.tipo' });
        var $inputFile = $('<input />', { name: prefijo + '.archivo', type: 'file' });
        var $delete = $('<button />', { type: 'button', class: 'pure-button' }).text('Eliminar');


        $dropDown.append($('<option />', { value: 'A', text: 'Tipo "A"' }));
        $dropDown.append($('<option />', { value: 'B', text: 'Tipo "B"' }));
        $dropDown.append($('<option />', { value: 'C', text: 'Tipo "C"' }));

        $celdaC.append($delete);
        $celdaB.append($inputFile);
        $celdaA.append($dropDown);

        $fila.append([$celdaA, $celdaB, $celdaC]);

        $('#tbl-archivos').append($fila);

        $delete.click(function (_e) {
            _e.preventDefault();
            $fila.remove();
        })

        correlativo++;

        $inputFile.click();

    });

})