<%@ Page Title="Upload" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebVault.Upload.Upload" %>

<asp:Content ID="UploadPage" ContentPlaceHolderID="MainContent" runat="server">     
    <script src="Scripts/jquery-3.3.1.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnUpload").click(function () {
                var uploader = $("#FileUpload1");
                var files = $("#FileUpload1")[0].files;
                if (files.length > 0)
                {
                    var formData = new FormData();
                    for (var i = 0; i < files.length; i++)
                    {
                        formData.append(files[i].name, files[i]);
                    }
                    var progressbarDiv = $("#progressBar");
                    var progressbarLabel = $("#progressBar-label");
                    $.ajax({
                        url: 'FileHandler.ashx',
                        method: 'post',
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function () {
                            progressbarLabel.text('Complete');
                            progressbarDiv.fadeOut(2000);
                        },
                        error: function (err) {
                            alert(err.statusText);
                        }
                    });
                    progressbarLabel.text('Uploading...');
                    progressbarDiv.progressbar({
                        value: false
                    }).fadeIn(500);

                }
            });
        });

    </script>
     <div class="row">
     
     <div class="col-lg-12">

        <div class="awp_box rounded bg-metro-dark shadow">
      
            <div class="awp_box_title bg-metro-dark">
               <span class="fg-white shadow-metro-black">Upload</span>
            </div>
            <div class="awp_box_content bg-metro-light" style="text-align:left !important;">
                <div id="MapDiv">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="awp_box rounded bg-metro-dark shadow">      
                                <div class="awp_box_title bg-metro-dark">
                                   <span class="fg-white shadow-metro-black">Files</span>
                                </div>
                                <div class="awp_box_content bg-metro-light" style="text-align:left !important;">
                                  <div id="bx" class="fg-white shadow-metro-black" style="border:5px dashed rgba(0, 0, 0, 0.50); height:100px;width:100%; display:table; vertical-align:middle; text-align:left">
                                      <b>Select Files:</b>
                                      <asp:FileUpload ID="FileUpload1" ClientIDMode="Static" runat="server" AllowMultiple="true" />
                                      <br /><br />
                                      <input type="button" id="btnUpload" value="Upload Files" />
                                      <br /><br />
                                      <div style="width:300px">
                                          <div id="progressBar" style="position:relative;display:none">
                                              <span id="progressBar-label" style="position:absolute; left:35%";top:20%;>Uploading...</span>
                                          </div>
                                      </div>
                                  </div>
                                    <div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="awp_box rounded bg-metro-dark shadow">      
                                <div class="awp_box_title bg-metro-dark">
                                   <span class="fg-white shadow-metro-black">Status</span>
                                </div>
                                <div class="awp_box_content bg-metro-light" style="text-align:left !important; height:100%">
                                    <div id="Dropper" class="fg-white shadow-metro-black" style="border:5px dashed rgba(0, 0, 0, 0.50); height:100px;width:100%; display:table; vertical-align:middle; text-align:left">
                                    Drop Here
                                    </div>  
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
 

        </div>
    <div id="UploadFile" class="awp-outer-dialog" style="display:none; z-index:99998" >
        <div class=" awp-inner-dialog" style="opacity:initial !important">
                    <span class="awp-dialog-close-btn bg-red fg-white shadow " onclick="HideDiv('UploadImage')"><i title="Close"  style="vertical-align:top" class="mif-cross av-hand-cursor fg-white shadow-metro-black"></i></span>
                    <div class="awp_box rounded bg-metro-dark shadow" style="left:50% !important; top:30%">
                        <div class="awp_box_title bg-metro-dark">
                           <span class="fg-white shadow-metro-black"><span class="mif-file-upload mif-2x"></span>Upload File</span>
                        </div>
                        <div class="awp_box_content bg-metro-light">  
                            <asp:FileUpload ID="Uploader" runat="server" />                           
                            <asp:Button CausesValidation="false"  runat="server" ID="UploadrBtn" ClientIDMode="Static" OnClick="UploadrBtn_Click" Text="Upload" />
                       </div>
                    </div>      
        </div>
    </div>

</asp:Content>
