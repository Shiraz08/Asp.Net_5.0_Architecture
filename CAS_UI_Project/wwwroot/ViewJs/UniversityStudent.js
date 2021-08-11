$(function () {
    GetStuList();
    GetDepartmentList();
    GetCourseList();
});
function GetStuList()
{
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44340/api/UniversityStudent/GetUniversityStudentList',
        success: function (response) {
            debugger
            console.log(response);
            debugger
            var op = '';
            $.each(response, function (key, item) {
                debugger;
                op += '<tr><td>' + item.uniStudentName + '</td><td>' + item.uniStudentEmail + '</td><td>' + item.uniStudentPhoneNo + '</td><td>' + item.departmentName + '</td><td>' + item.courseName + '</td>';
                op += '<td><a href="javascript:;"   class="btn btn-success mt-2" onclick="editstu(\'' + item.id + '\',\'' + item.uniStudentName + '\',\'' + item.uniStudentEmail + '\',\'' + item.uniStudentPhoneNo + '\',\'' + item.departmentName + '\',\'' + item.courseName + '\')">Edit</a><a href="javascript:;" onclick="delstu(\'' + item.id + '\')" class="btn btn-danger mt-2">Delete</a></td></tr>';

            });
            $('#stutbl').empty().append(op);
            $("#students").DataTable({
                "responsive": true, "lengthChange": false, "autoWidth": false,
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}
function GetDepartmentList() {
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44340/api/DropDown/GetDepartmentList',
        success: function (response) {
            debugger
            console.log(response);
            debugger
            var s = '<option value="-1">Please Select a Department</option>';
            for (var i = 0; i < response.length; i++) {
                s += '<option value="' + response[i].value + '">' + response[i].text + '</option>';
            }
            $("#departmentsDropdown").html(s);  
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}
function GetCourseList() {
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44340/api/DropDown/GetCourseList',
        success: function (response) {
            debugger
            console.log(response);
            debugger
            var s = '<option value="-1">Please Select a Course</option>';
            for (var i = 0; i < response.length; i++) {
                s += '<option value="' + response[i].value + '">' + response[i].text + '</option>';
            }
            $("#CourseDropdown").html(s);  
            Datatables();
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}
var id = '';
function AddStudent() {
    // Student Object
    var Student = {};
    Student.UniStudentName = $('#name').val();
    Student.UniStudentPhoneNo = $('#phone').val();
    Student.UniStudentEmail = $('#email').val();
    Student.DepartmentId = $('#departmentsDropdown').val();
    Student.CourseId = $('#CourseDropdown').val();

    debugger
    var actnam = 'AddUniversityStudent';
    if (id != '') {
        actnam = 'UpdateUniversityStudent';
        Student.Id = id;
    }
    var data1 = JSON.stringify(Student);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: 'https://localhost:44340/api/UniversityStudent/' + actnam + '',
        data: data1,
        success: function (response) {
            debugger
            $('#msg').show();
            GetStuList();
            GetDepartmentList();
            GetCourseList();
            $('#addupbtn').empty().text('Add');
            setTimeout(function () { $('#msg').hide(); }, 2000);
            id = '';
            $('#name').val('');
            $('#email').val('');
            $('#phone').val('');
            $('#departmentsDropdown').val('');
            $('#CourseDropdown').val('');
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}
function editstu(Id, uniStudentName, uniStudentEmail, uniStudentPhoneNo, departmentName, courseName) {
    debugger
    $('#name').val(uniStudentName);
    $('#phone').val(uniStudentPhoneNo);
    $('#email').val(uniStudentEmail);
    $("#departmentsDropdown option:contains(" + departmentName + ")").attr('selected', 'selected');
    $("#CourseDropdown option:contains(" + courseName + ")").attr('selected', 'selected');
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
        url: 'https://localhost:44340/api/UniversityStudent/DeleteUniversityStudent',
        data: data1,
        success: function (response) {
            $('#msg').show();
            GetStuList();
            GetDepartmentList();
            GetCourseList();
            setTimeout(function () { $('#msg').hide(); }, 2000);
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

function AddStudentCourse()
{
    debugger
    var course_val = $("#CourseName").val();
    var Student = {};
    Student.CourseName = course_val;
    var data1 = JSON.stringify(Student);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: 'https://localhost:44340/api/DropDown/AddNewCourse',
        data: data1,
        success: function (response)
        {
            $('#AddCoursepopup').modal('toggle');
            $("#CourseName").val('');
            GetCourseList();
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });

}
function AddStudentDepartment() {
    debugger
    var dep_val = $("#DepartmentName").val();
    var Student = {};
    Student.DepartmentName = dep_val;
    var data1 = JSON.stringify(Student);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: 'https://localhost:44340/api/DropDown/AddNewDepartment',
        data: data1,
        success: function (response) {
            $('#AddDepartmentpopup').modal('toggle');
            $("#DepartmentName").val('');
            GetDepartmentList();
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });

}