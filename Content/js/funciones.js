/*Modals*/
$("#btnAgregar").click(function () {
    alert("hola");
    $('#modalAgregar').modal('show');
});
$("#btnEditar").click(function (ev) {
    $('#modalEditar').modal("show");
});
    
/*Removiendo del carrito*/
$(function () {
    $(".EliminarDelCarrito").click(function () {
        var recordToDelete = $(this).attr("data-id");
        if (recordToDelete != '') {
            $.post("/CarritoCompras/EliminarDelCarrito", { "id": recordToDelete },
            function (data) {
                if (data.ItemCount == 1) {
                    $('#row-' + data.DeleteId).fadeOut('slow');
                } else {
                    $('#item-count-' + data.DeleteId).val(data.ItemCount);
                }
                $('#cart-total').text("S/. " + data.TotalCarrito);
                $('#update-message').text(data.Mensaje);
                $('#cart-status').text('Cart (' + data.CartCount + ')');
            });
        }
    });

});

/*Datepicker jqueryui*/
$(".fecha").datepicker({ dateFormat: "dd/mm/yy" }).mask("99/99/9999");
$.validator.addMethod('date',
function (value, element, params) {
    if (this.optional(element)) {
        return true;
    }
    var ok = true;
    try {
        $.datepicker.parseDate('dd/mm/yy', value);
    }
    catch (err) {
        ok = false;
    }
    return ok;
});