
$(function () {
    $("#resultImagediv").css('background', 'lightgray');
    $.ajax({
        url: "/api/ImageDetection/GetPublishedModels",
        type: "GET",
        success: function (result) {
            var models = result.result;
            $("#modelname").empty();
            //Load Options
            for (var i = 0; i < models.length; i++) {
                $("#modelname").append(new Option(models[i], models[i]));
            }
        },
        error: function (e) {
            var x = e;
        }
    });
});

var form = document.querySelector('form');

form.addEventListener('submit', e => {
    e.preventDefault();

    const files = document.querySelector('[type=file]').files;
    var modelName = document.getElementById('modelname').value;

    const formData = new FormData();

    if (files.length == 0 || !modelName) { alert("Select an image & model name - they are required to predict"); }
    else {
        ///Create Formdata - image and model name
        formData.append('imageFile', files[0]);
        formData.append('modelName', modelName);

        // Sending the image data to Server
        $.ajax({
            type: 'POST',
            url: '/api/ImageDetection/IdentifyObjects',
            data: formData,
            contentType: false,
            processData: false,
            success: function (result) {
                var data = result.imageString;
                $("#resultImagediv").css('background', 'none');
                $("#result").attr("src", 'data:image/jpeg;base64,' + data);
            }
        });
    }

});
