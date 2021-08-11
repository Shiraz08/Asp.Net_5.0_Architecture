$(function () {
    GetStuList();
});
function GetStuList() {
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44340/api/Student/GetStudentList',
        success: function (response) {
            debugger
            console.log(response);
            debugger
            var op = '';
            $.each(response, function (key, item) {
                debugger;
                op += '<tr><td>' + item.name + '</td><td>' + item.email + '</td><td>' + item.phoneNo + '</td>';
                op += '<td><a href="javascript:;"  class="btn btn-success mt-2" onclick="editstu(\'' + item.id + '\',\'' + item.name + '\',\'' + item.email + '\',\'' + item.phoneNo + '\')">Edit</a><a href="javascript:;" onclick="delstu(\'' + item.id + '\')" class="btn btn-danger mt-2">Delete</a></td></tr>';

            });
            $('#stutbl').empty().append(op);
            Datatables();
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}
var id = '';
function AddStudent() {
    var Student = {};
    Student.Name = $('#name').val();
    Student.PhoneNo = $('#phone').val();
    Student.Email = $('#email').val();
    
    debugger
    var actnam = 'AddStudent';
    if (id != '') {
        actnam = 'UpdateStudent';
        Student.Id = id;
    }
    var data1 = JSON.stringify(Student);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: 'https://localhost:44340/api/Student/' + actnam + '',
        data: data1,
        success: function (response) {
            $('#msg').show();
            GetStuList();
            $('#addupbtn').empty().text('Add');
            setTimeout(function () { $('#msg').hide(); }, 2000);
            id = '';
            $('#name').val('');
            $('#email').val('');
            $('#phone').val('');
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}
function editstu(Id,Name,Email,PhoneNo) {
    $('#name').val(Name);
    $('#phone').val(PhoneNo);
    $('#email').val(Email);
    id = Id;
    $('#addupbtn').empty().text('Update');
}
function delstu(Id) {
    var Student = {};
    Student.Id = Id;
    var data1 = JSON.stringify(Student);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: 'https://localhost:44340/api/Student/DeleteStudent',
        data: data1,
        success: function (response) {
            $('#msg').show();
            GetStuList();
            setTimeout(function () { $('#msg').hide(); }, 2000);
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}