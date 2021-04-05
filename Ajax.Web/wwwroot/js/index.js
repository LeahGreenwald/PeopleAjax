$(() => {
    function fillTable() {
        $("tbody").empty();
        $.get('/people/getall', function (ppl) {
            ppl.forEach(p => {
                $("tbody").append(`
<tr>
    <td>${p.firstName}</td>
    <td>${p.lastName}</td>
    <td>${p.age}</td>
    <td><button class="btn btn-danger" id="delete" data-id=${p.id}>Delete</button></td>
    <td><button class="btn btn-warning" id="edit" data-id=${p.id} data-first-name=${p.firstName} data-last-name=${p.lastName} data-age=${p.age}>Edit</button></td>
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

    $("tbody").on('click', '#delete', function () {
        let Id = $(this).data('id');
        $.post("/people/delete", { Id }, function () {
            fillTable();
        })
    });

    $("tbody").on('click', "#edit", function () {
        alert("edit");
        let id = $(this).data('id');
        let firstName = $(this).data('first-name');
        let lastName = $(this).data('last-name');
        let age = $(this).data('age');

        $("#edit-first-name").val(firstName);
        $("#edit-last-name").val(lastName);
        $("#edit-age").val(age);
        $(".modal").data('id', id);

        $(".modal").modal();
    });

    $("#update").on('click', function () {
        let id = $(".modal").data('id');
        let firstName = $("#edit-first-name").val();
        let lastName = $("#edit-last-name").val();
        let age = $("#edit-age").val();
        $.post('/people/edit', { id, firstName, lastName, age }, function () {
            fillTable();
        })
        $(".modal").modal('hide');
    });
});