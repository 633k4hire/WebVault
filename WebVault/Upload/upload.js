$(document).ready(function () {
    $("#btnUpload").click(function () {
        var uploader = $("#FileUpload1");
        var files = $("#FileUpload1")[0].files;
        if (files.length > 0) {
            var formData = new FormData();
            for (var i = 0; i < files.length; i++) {
                formData.append(files[i].name, files[i]);
            }
            var progressbarDiv = $("#progressBar");
            var progressbarLabel = $("#progressBar-label");

            $.ajax({
                url: '/UploadController.aspx/UploadData',
                method: 'post',
                data: "{data:" + FormData + "}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function () {
                    progressbarLabel.text('Complete');
                    progressbarDiv.fadeOut(2000);
                },
                error: function (err) {
                    alert(err.statusText);
                },
                complete: function (err) {
                    alert('Comp');
                }
            });

            progressbarLabel.text('Uploading...');
            progressbarDiv.progressbar({
                value: false
            }).fadeIn(500);

        }
    });
});
