var visionApp = (function ($) {

    var handleAjax = function (url, success, postData) {
        if (postData !== undefined) {
            $.ajax({
                type: "POST",
                url: url,
                data: postData,
                success: success,
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "json"
            });
        }
        else {
            $.ajax({
                type: "GET",
                url: url,
                cache: false,
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "json",
                success: success,
                error: function (data) {
                    console.log(data);
                }
            });

        }
    };

    var _visionApp = {
        init: function () {
            $("#resultImagediv").css('background', 'lightgray');
            //simple js to register js based on page
            if ($('#predict').length)
                _visionApp.predictTask();

            if ($('#train').length)
                _visionApp.trainTask();

        },
        predictTask: function () {
            handleAjax("/api/ImageDetection/GetPublishedModels", function (data) {
                var models = data.result;
                $("#modelname").empty();
                //Load Options
                for (var i = 0; i < models.length; i++) {
                    $("#modelname").append(new Option(models[i], models[i]));
                }
            });
            _visionApp.registerForm();
        },
        trainTask: function () {
            //Disable Button
            $('#train-btn').prop('disabled', true);
            $('#model-info').hide();
            handleAjax("/api/ImageDetection/GetCurrentTraining", function (data) {
                var modelName = data.result;
                if (modelName) {
                    $('#train-btn').prop('disabled', true);
                    $('#model-info').text('A model named --> ' + modelName + ' is currently being trained! Please try after sometime');
                    $('#model-info').show();
                }
                else {
                    $('#model-info').text('');
                    $('#train-btn').prop('disabled', false);
                    _visionApp.trainModel();
                }

            });
        },
        registerForm: function () {
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
        },
        trainModel: function () {
            $(document).on('click', '#train-btn', function (e) {
                console.log('Btn clciked')
                var modelName = $('#modelname').val();
                if (modelName.length < 3) { // Not sure how to validate models. tobe done in future
                    alert('Please provide a valid name');
                }
                else {
                    const formData = new FormData();
                    formData.append('publishModelName', modelName);
                    $.ajax({
                        type: 'POST',
                        url: '/api/ImageDetection/TrainProject',
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            $('#train-btn').prop('disabled', true);
                            $('#model-info').text('A model named --> ' + modelName + ' is currently being trained! Please try after sometime');
                            $('#model-info').show();
                            alert(result);
                        }
                    });
                }
            });
        }
    }

    _visionApp.init();
    return _visionApp;

})(jQuery);



