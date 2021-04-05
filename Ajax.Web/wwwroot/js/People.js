$(() => {
    alert("java script");
    function fillTable() {
        $("tbody").empty();
        $.get('/people/getall', function (ppl) {
            ppl.forEach(p => {
                $("tbody").append(`
<tr>
    <td>${p.firstName}</td>
    <td>${p.lastName}</td>
    <td>${p.age}</td>
    <td><button class="btn btn-primary btn-block" id="delete" data-id=${p.id}>Delete</button></td>
    <td><button class="btn btn-primary btn-block" id="edit" data-id=${p.id} data-first-name=${p.firstName} data-last-name=${p.lastName} data-age=${p.age}>Edit</button></td>
</tr>`);
            });
        });
    };

    fillTable();

    $("#add").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();

        $("#first-name").val('');
        $("#last-name").val('');
        $("#age").val('');

        $.post('/people/add', { firstName, lastName, age }, function (p) {
            fillTable();
        });

    });

    $("tbody").on('click', ("#delete"), function () {
        var id = (this).data('id');
        $.post('/people/delete', { id }, function () {
            fillTable();
        })
    });

    $("tbody").on('click', ("#edit"), function () {
        var id = (this).data('id');
        var firstName = (this).data('first-name');
        var lastName = (this).data('last-name');
        var age = (this).data('age');

        $("#edit-first-name").val(firstName);
        $("#edit-last-name").val(lastName);
        $("#edit-age").val(age);
        $(".modal").data('id', id);

        $(".modal").modal();
    });

    $("#save").on('click', function () {
        var id = $(".modal").data('id');
        var firstName = $("#edit-first-name").val();
        var lastName = $("#edit-last-name").val();
        var age = $("#edit-age").val();
        $(".modal").hide();
        $.post('/people/edit', { id, firstName, lastName, age }, function () {
            fillTable();
        })
    });
});