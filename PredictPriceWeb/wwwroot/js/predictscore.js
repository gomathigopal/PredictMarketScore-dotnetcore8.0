"use strict"

$(document).ready(function () {

    $("#sourceFiles").on("click", function () {

    });

});

function PredictScore() {
    debugger;
    
    var formData = new FormData();
    var files = $('#Files')[0].files;
    if (files.length > 0) {
        if (files.length > 0) {
            for (var x = 0; x < files.length; x++) {
                // the name has to be 'files' so that .NET could properly bind it
                formData.append('files', files.item(x));
                return new Promise(function (resolve, reject) {
                    $.ajax({
                        url: "https://localhost:7077/api/PredictScore/GetDataPoints",
                        type: 'POST',
                        data: formData,
                        dataType: "json",
                        success: function (response) {
                            debugger;

                            var responseobj = JSON.stringify(response);
                            console.log(responseobj);

                            resolve(callPredictScore(responseobj));

                        },
                        cache: false,
                        contentType: false,
                        processData: false
                    });

                });
            }
        }


    }
    else {
        alert("Please select file!");
        return false;
    }
}

function callAPIPredictScore(response) {
    return new Promise(function (resolve, reject) {
        debugger;
        $.ajax({
            url: "https://localhost:7077/api/PredictScore/GetPredictionScore?json=" + response,
            type: 'POST',
            data: response,
            success: function (prediction) {
                debugger;
                console.log(prediction);
                var jsonObject = JSON.stringify(prediction);
                resolve(jsonObject);


                // var responseobj = JSON.parse(response1);
                /*  var data = convertToCSV(jsonObject);
                  var filename = prediction[0].stockId;
                  save(filename + '.csv', data);*/


            },
            cache: false,
            //  contentType: false,
            //  processData: false
        });
    });
}

async function callPredictScore(response) {
    debugger;
   await callAPIPredictScore(response).then(function(prediction) {
        debugger
        return (getJsonToCSV(prediction));
    })
        .then(function () {
            resolve("Success!");
        })
        .catch(function (error) {
            console.log("Error:", error);
            reject("error");
        });
}

function save(filename, data) {
    debugger;
    const blob = new Blob([data], { type: 'text/csv' });
    if (window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveBlob(blob, filename);
    }
    else {
        const elem = window.document.createElement('a');
        elem.href = window.URL.createObjectURL(blob);
        elem.download = filename;
        document.body.appendChild(elem);
        elem.click();
        document.body.removeChild(elem);
    }
}


function convertToCSV(objArray) {
    return new Promise(function (resolve, reject) {
        var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;
        var str = '';

        for (var i = 0; i < array.length; i++) {
            var line = '';
            for (var index in array[i]) {
                if (line != '') line += ','

                line += array[i][index];
            }

            str += line + '\r\n';
        }

        resolve(str);
    });
}

async function getJsonToCSV(prediction) {
    debugger;

    var objArray = JSON.parse(prediction);
    var filename = objArray[0].stockId + ".csv";
    await convertToCSV(objArray).then(function (str) {
            debugger
            return save(filename, str);
        })
        .then(function () {
            debugger
            $('#Files').val('');
            $('#Files').removeClass('validation-error');
            resolve("Success!");
        })
        .catch(function (error) {
            console.log("Error:", error);
            reject("error");
        });
}