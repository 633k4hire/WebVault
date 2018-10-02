var IsKl0x = false;
$(function () {

  $('#drag-and-drop-zone').dmUploader({ //
    url: 'OTPHandler.ashx',
    maxFileSize: 2147483647, // 3 Megs 
    multiple:false,
    onDragEnter: function(){
      // Happens when dragging something over the DnD area
      this.addClass('active');
    },
    onDragLeave: function(){
      // Happens when dragging something OUT of the DnD area
      this.removeClass('active');
    },
    onInit: function(){
      // Plugin is ready to use
      ui_add_log('Encryption Ready...', 'info');
    },
    onComplete: function(){
      // All files in the queue are processed (success or error)
      ui_add_log('All pending tranfers finished');
    },
    onNewFile: function(id, file){
      // When a new file is added using the file selector or the DnD area
      ui_add_log('New file added ' + id);
      ui_multi_add_file(id, file);
      var split = file.name.split('.').pop();
      if (split == "kl0x")
      {
          IsKl0x = true;
      } else {
          IsKl0x = false;
      }
    },
    onBeforeUpload: function(id){
      // about tho start uploading a file
      ui_add_log('Starting the upload of ' + id);
      ui_multi_update_file_status(id, 'uploading', 'Uploading...');
      ui_multi_update_file_progress(id, 0, '', true);
    },
    onUploadCanceled: function(id) {
      // Happens when a file is directly canceled by the user.
      ui_multi_update_file_status(id, 'warning', 'Canceled by User');
      ui_multi_update_file_progress(id, 0, 'warning', false);
    },
    onUploadProgress: function(id, percent){
      // Updating file progress
      ui_multi_update_file_progress(id, percent);
    },
    onUploadSuccess: function(id, data){
      // A file was successfully uploaded
      //ui_add_log('Server Response for file ' + id + ': ' + JSON.stringify(data));
        //check encryption status
        // cogHeight(83);
        if (IsKl0x)
        {
            IsKl0x = false;
            ui_add_log('Upload of KeyFile ' + id + ' COMPLETED', 'success');
            ui_multi_update_file_status(id, 'success', 'Key Updated');
            ui_multi_update_file_progress(id, 100, 'success', false);
            updateKey();
        } else {
            ui_add_log('Upload of file ' + id + ' COMPLETED', 'success');
            ui_multi_update_file_status(id, 'success', 'Upload Complete');
            ui_multi_update_file_progress(id, 100, 'success', false);
            setTimeout(function () { $("#SuperButtonArg").val("download," + data); $("#DownloadButton").click(); }, 1000);
        }
      
     
      
    
    },
    onUploadError: function(id, xhr, status, message){
      ui_multi_update_file_status(id, 'danger', message);
      ui_multi_update_file_progress(id, 0, 'danger', false);  
    },
    onFallbackMode: function(){
      // When the browser doesn't support this plugin :(
      ui_add_log('Plugin cant be used here, running Fallback callback', 'danger');
    },
    onFileSizeError: function(file){
      ui_add_log('File \'' + file.name + '\' cannot be added: size excess limit', 'danger');
    }
    });

    /*
  $('#keyfile-drag-and-drop-zone').dmUploader({ //
      url: 'OTPHandler.ashx',
      maxFileSize: 300000, // 3 Megs 
      multiple: false,
      onDragEnter: function () {
          // Happens when dragging something over the DnD area
          this.addClass('active');
      },
      onDragLeave: function () {
          // Happens when dragging something OUT of the DnD area
          this.removeClass('active');
      },
      onInit: function () {
          // Plugin is ready to use
          ui_add_log('Encryption Ready...', 'info');
      },
      onComplete: function () {
          // All files in the queue are processed (success or error)
          ui_add_log('All pending tranfers finished');
      },
      onNewFile: function (id, file) {
          // When a new file is added using the file selector or the DnD area
          ui_add_log('New file added ' + id);
          ui_multi_add_file(id, file);
      },
      onBeforeUpload: function (id) {
          // about tho start uploading a file
          ui_add_log('Starting the upload of ' + id);
          ui_multi_update_file_status(id, 'uploading', 'Uploading...');
          ui_multi_update_file_progress(id, 0, '', true);
      },
      onUploadCanceled: function (id) {
          // Happens when a file is directly canceled by the user.
          ui_multi_update_file_status(id, 'warning', 'Canceled by User');
          ui_multi_update_file_progress(id, 0, 'warning', false);
      },
      onUploadProgress: function (id, percent) {
          // Updating file progress
          ui_multi_update_file_progress(id, percent);
      },
      onUploadSuccess: function (id, data) {
          // A file was successfully uploaded
          //ui_add_log('Server Response for file ' + id + ': ' + JSON.stringify(data));
          //check encryption status
          updateKey();
          ui_add_log('Upload of file ' + id + ' COMPLETED', 'success');
          ui_multi_update_file_status(id, 'success', 'KeyFile Uploaded');
          ui_multi_update_file_progress(id, 100, 'success', false);
          //setTimeout(function () { $("#SuperButtonArg").val("download," + data); $("#DownloadButton").click(); }, 1000);


      },
      onUploadError: function (id, xhr, status, message) {
          ui_multi_update_file_status(id, 'danger', message);
          ui_multi_update_file_progress(id, 0, 'danger', false);
      },
      onFallbackMode: function () {
          // When the browser doesn't support this plugin :(
          ui_add_log('Plugin cant be used here, running Fallback callback', 'danger');
      },
      onFileSizeError: function (file) {
          ui_add_log('File \'' + file.name + '\' cannot be added: size excess limit', 'danger');
      }
  });
    */


});