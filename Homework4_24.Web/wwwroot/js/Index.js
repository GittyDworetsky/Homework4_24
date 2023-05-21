$(() => {


   
    const modal = new bootstrap.Modal($('.modal')[0]);

    function refreshTable(cb) {
        $(".table tr:gt(1)").remove();
        $.get('/home/getpeople', function (people) {
            
            people.forEach(function (person) {
                $("tbody").append(`<tr>
            <td>${person.firstName}</td>
            <td>${person.lastName}</td>
            <td>${person.age}</td>
            <td>
                <button class='btn btn-warning edit' data-person-id='${person.id}'>Edit</button>
                <button class='btn btn-danger delete' data-person-id='${person.id}'>Delete</button>
            </td>
</tr>`)
            });

            if (cb) {
                cb();
            }
        });
    }

    refreshTable();

    $("#add-person").on('click', function () {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        $("#save-person").show();
        $("#update-person").hide();
        $(".modal-title").text('Add Person');
        modal.show();
    });

    $("#save-person").on('click', function () {
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = $("#age").val();


        $.post('/home/addperson', { firstName, lastName, age }, function () {
            refreshTable(() => {
                modal.hide();
            });
        });
    })

    $('table').on('click', '.delete', function () {
        const personId = $(this).data('person-id');
        $.post('/home/deleteperson', { personId }, function () {
            refreshTable();
        })
    })

    $('table').on('click', '.edit', function () {
        const personId = $(this).data('person-id');
        $.get('/home/showeditperson', { personId }, function (person) {
            $("#firstName").val(person.firstName);
            $("#lastName").val(person.lastName);
            $("#age").val(person.age);
            $("#save-person").hide();
            $("#update-person").show();
            $(".modal-title").text('Edit Person');
            modal.show();          
        })

        $(".modal").data('person-id', personId);

    })

    $("#update-person").on('click', function () {
       
        const id = $('.modal').data('person-id');
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = $("#age").val();

        $.post('/home/updateperson', { firstName, lastName, age, id }, function () {
            refreshTable(() => {
                modal.hide();
            });
        });
    })
})