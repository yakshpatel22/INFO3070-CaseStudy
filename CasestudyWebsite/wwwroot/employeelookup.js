﻿$(function () {
    $("#getbutton").click(async (e) => {
        try {
            let Email = $("#TextBoxLastname").val();
            $("#status").text("please wait...");
            let response = await fetch(`/api/student/${Email}`);
            if (response.ok) {
                let data = await response.json();
                if (data.Lastname !== "not found") {
                    $("#lastname").text(data.email);
                    $("#title").text(data.title);
                    $("#firstname").text(data.firstname);
                    $("#phone").text(data.phoneno);
                    $("#status").text("student found");
                }
                else {
                    $("#firstname").text("not found");
                    $("#lastname").text("");
                    $("#title").text("");
                    $("#phone").text("");
                    $("#status").text("no such student");
                }
            }
            else if (response.status !== 404) {
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            }
            else {
                $("#status").text("no such path on server");
            }
        }
        catch (error) {
            $("#status").text(error.message);
        }
    });
});
//server was reached but server had a problem with the call
const errorRtn = (problemJson, status) => {
    if (status > 499) {
        $("#status").text("Problem server side, see debug console");
    }
    else {
        let keys = Object.keys(problemJson.errors)
        problem = {
            status: status,
            statusText: problemJson.errors[keys[0]][0], //first error
        };
        $("#status").text("Problem client side, see browser console");
        console.log(problem);
    } //else
}