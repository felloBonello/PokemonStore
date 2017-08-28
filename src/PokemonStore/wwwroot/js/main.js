$(function () {
    // display message if modal still loaded i
    if ($("#detailsId").val() > 0) {
        var Id = $("#detailsId").val();
        CopyToModal(Id);
        $('#details_popup').modal('show');
    } //details
    // details anchor click - to load popup on catalogue
    $("a.btn-default").on("click", function (e) {
        var Id = $(this).attr("data-id");
        $("#results").text("");
        CopyToModal(Id);
    });

    if ($("#register_popup") != undefined) {
        $('#register_popup').modal('show');
    }

    if ($("#login_popup") != undefined) {
        $('#login_popup').modal('show');
    }

});

function CopyToModal(id) {
    $("#qty").val("0");
    $("#hp").text($("#hp" + id).val());
    $("#attack").text($("#attack" + id).val());
    $("#defence").text($("#defence" + id).val());
    $("#specialAttack").text($("#specialAttack" + id).val());
    $("#specialDefence").text($("#specialDefence" + id).val());
    $("#speed").text($("#speed" + id).val());
    $("#ProductName").text($("#ProductName" + id).val());
    $("#detailsGraphic").attr("src", $("#detailsGraphic" + id).val());
    $("#detailsGraphic").attr("width", "280");
    $("#detailsGraphic").attr("height", "280");
    $("#detailsId").val(id);
    
    $("#Description").text($("#Description" + id).val());
    $("#CostPrice").text("$" + parseFloat($("#CostPrice" + id).val()).toFixed(2));
    $("#MSRP").text($("#MSRP" + id).val());
    $("#QtyOnHand").text($("#QtyOnHand" + id).val());
    $("#QtyOnBackOrder").text($("#QtyOnBackOrder" + id).val());

}