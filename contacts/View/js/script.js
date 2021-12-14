

Page_load();

function Page_load() {
    $.ajax({
        url: "/api/Contacts",
        type: "GET",
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == 'invalid') {
                console.log('error');
            }
            else {
                Fill_data(JSON.parse(JSON.parse(data)));
                $('#loading').hide();
            }
        },
        error: function (e) {
            console.log('error ' + e);
        }
    });

}

function Fill_data(data) {
    data.forEach((item, index) => {
        Add_tr(item["FirstName"],
            item["LastName"],
            item["Tel"],
            item["CityName"],
            item["StreetName"],
            item["HouseNumber"],
            item["ApartmentNumber"],
            item["ContactID"])
    })
}


function Add_tr(First_name, Last_name, Phone_number, City, Street, House_number, Apartment_number, ContactID) {
    let html = `
    <tr>
        <td>${First_name}</td>
        <td>${Last_name}</td>
        <td>${Phone_number}</td>
        <td>${City}</td>
        <td>${Street}</td>
        <td>${House_number}</td>
        <td>${Apartment_number}</td>
        <td><button onclick="Delete_contact(${ContactID})" type="button" class="btn btn-default btn-sm">
          <span class="glyphicon glyphicon-trash"></span> Delete 
        </button>
        <button onclick="Get_contact(${ContactID})" type="button" class="btn btn-default btn-sm">
          <span class="glyphicon glyphicon-pencil"></span> Edit 
        </button>
        </td>
        
     </tr >`;
    $('table').find('tbody').append(html);
}





function Get_contact(ContactID) {
    $.ajax({
        url: `/api/Contacts/${ContactID}`,
        type: "GET",
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == 'invalid') {
                console.log('error');
            }
            else {
                Fill_form((JSON.parse(JSON.parse(data)))[0], ContactID);
            }
        },
        error: function (e) {
            console.log('error ' + e);
        }
    });
}

function Fill_form(data, ContactID) {
    $('#inputFirstname').val(data["FirstName"]);
    $('#inputLastName').val(data["LastName"]);
    $('#inputtel').val(data["Tel"]);
    $('#inputCity').val(data["CityName"]);
    $('#inputStreetName').val(data["StreetName"]);
    $('#inputHouseNumber').val(data["HouseNumber"]);
    $('#inputApartmentNumber').val(data["ApartmentNumber"]);
    $('#ContactID').val(ContactID);
    $('#open_edit_model').click();
}

function Delete_contact(ContactID) {
    $.ajax({
        url: `/api/Contacts/${ContactID}`,
        type: 'DELETE',
        success: function (result) {
            $("table > tbody").html("");
            Page_load();
        }
    });
}

function UpdateData() {
    Edit_contact(
        $('#inputFirstname').val(),
        $('#inputLastName').val(),
        $('#inputtel').val(),
        $('#inputCity').val(),
        $('#inputStreetName').val(),
        $('#inputHouseNumber').val(),
        $('#inputApartmentNumber').val(),
        $('#ContactID').val()
    );
    document.getElementById("edit_contact_form").reset();
}

function Edit_contact(First_name, Last_name, Phone_number, City, Street, House_number, Apartment_number, ContactID) {
    let data = ` {'Id': ${ContactID},'FirstName': '${First_name}','LastName': '${Last_name}','Tel': '${Phone_number}',adress:{'Id':-1,'CityName': '${City}','StreetName': '${Street}','HouseNumber':${House_number},'ApartmentNumber':${Apartment_number}}}`;
    $.ajax({
        url: `/api/Contacts/${ContactID}`,
        type: "PUT",
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        data: data,
        success: function (data) {
            if (data == 'invalid') {
                console.log('error');
            }
            else {
                $("table > tbody").html("");
                Page_load();
            }
        },
        error: function (e) {
            console.log('error ' + e);
        }
    });

}


function SetData() {
    Add_contact(
        $('#EinputFirstname').val(),
        $('#EinputLastName').val(),
        $('#Einputtel').val(),
        $('#EinputCity').val(),
        $('#EinputStreetName').val(),
        $('#EinputHouseNumber').val(),
        $('#EinputApartmentNumber').val()
    );
    document.getElementById("add_contact_form").reset();

}

function Add_contact(First_name, Last_name, Phone_number, City, Street, House_number, Apartment_number) {
    let data = ` {'Id': -1,'FirstName': '${First_name}','LastName': '${Last_name}','Tel': '${Phone_number}',adress:{'Id':-1,'CityName': '${City}','StreetName': '${Street}','HouseNumber':${House_number},'ApartmentNumber':${Apartment_number}}}`;
    console.log(data);

    $.ajax({
        url: "/api/Contacts",
        type: "POST",
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        data: data,
        success: function (data) {
            if (data == 'invalid') {
                console.log('error');
            }
            else {
                $("table > tbody").html("");
                Page_load();
            }
        },
        error: function (e) {
            console.log('error ' + e);
        }
    });

}


