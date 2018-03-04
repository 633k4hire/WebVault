<%@ Page Title="Upload" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="L0XX0R.aspx.cs" Inherits="WebVault.L0XX0R" %>
<asp:Content ID="UploadPage" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/jquery.dm-uploader.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.dm-uploader.js"></script>
    <script src="Scripts/Cog.js"></script>
    <script src="Scripts/demo-ui.js"></script>
    <script src="Scripts/demo-config.js"></script>

    <div style="margin-left:15px;margin-right:15px;">

    
    <!--<script src="Scripts/Resizer.js"></script>-->
    <div class="row">
        <div class="col-md-12">
            <div class="awp_box rounded shadow">      
                <div class="awp_box_title bg-grayed">
                    <span class="fg-white shadow-metro-black">Upload</span>
                </div>
                <div class="awp_box_content bg-grayDark">
                    <div id="L0XBOX" style="height:120px">
                        <div id="drag-and-drop-zone" class="dm-uploader p-5" style="height:100%;">
                            <h3 class="mb-5 mt-5 fg-white shadow-metro-black ">Drag &amp; drop files here</h3>

                            <div class="btn btn-primary btn-block mb-5">
                                <span>Open the file Browser</span>
                                <input type="file" title='Click to add Files' />
                            </div>
                        </div><!-- /uploader -->
                    </div>
                </div>
            </div>
        </div>    

    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="awp_box rounded shadow">      
                <div class="awp_box_title bg-grayed">
                    <span class="fg-white shadow-metro-black">Files</span>
                </div>
                <div  class="awp_box_content bg-grayDark" style="height:auto;">
                   <div id="FILEBOX" style="height:auto;">
                        <div class="card h-100">
                        <div class="card-header">
                          File List
                        </div>

                        <ul class="list-unstyled p-2 d-flex flex-column col" id="files">
                          <li class="text-muted text-center empty">No files uploaded.</li>
                        </ul>
                      </div>
                   </div>
                </div>
            </div>
        </div>                          

    </div>
    
    <div class="row" style="display:none">
        <div id="DEBUGBOX" >
            <div class="card h-100">
                <div class="card-header">
                  Debug Messages
                </div>

                <ul class="list-group list-group-flush" id="debug">
                  <li class="list-group-item text-muted empty">Loading plugin....</li>
                </ul>
            </div>
        </div>
    </div>
    </div>
   <!-- File item template -->
    <script type="text/html" id="files-template">
      <li class="media">
        <div class="media-body mb-1">
          <p class="mb-2 fg-white shadow-metro-black">
            <strong>%%filename%%</strong> - Status: <span class="text-muted">Waiting</span>
          </p>
          <div class="progress mb-2">
            <div class="progress-bar progress-bar-striped progress-bar-animated bg-amber" 
              role="progressbar"
              style="width: 0%" 
              aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">
            </div>
          </div>
          <hr class="mt-1 mb-1" />
        </div>
      </li>
    </script>

    <!-- Debug item template -->
    <script type="text/html" id="debug-template">
      <li class="list-group-item text-%%color%%"><strong>%%date%%</strong>: %%message%%</li>
    </script>
    <!--cog Charm-->
        <div id="CogBox" class=" transition-bottom">
       <span class="modal-closer" onclick="ToggleCog()"><span id="cog-chevron"  class=" glyphicon glyphicon-chevron-up fg-white"></span></span>
            <div id="cog-box-label"><span style="font-size:1em;" class=" glyphicon glyphicon-cog fg-white shadow-metro-black"></span>
            <asp:Label Text="Settings" ID="CogLabel" ClientIDMode="Static" CssClass="fg-white shadow-metro-black" runat="server" />
            </div>          
            <div id="cog-box-content" class="fg-white">
                <asp:UpdatePanel ID="CogUpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:TextBox id="KeyField" runat="server" class="form-control bau-bold" Text="Key" ClientIDMode="Static"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="KeyCheck" runat="server" CssClass="fg-white shadow-metro-black" 
                            ErrorMessage="Password length must be between 6 to 12 characters"
                            ControlToValidate="KeyField"    
                            ValidationExpression="^[a-zA-Z0-9'@&#.\s]{6,12}$" />
                        <asp:Button runat="server" ID="SubmitPasswordBtn" ClientIDMode="Static" CssClass="form-control bau-bold" Text="Update Password" OnClick="SubmitPasswordBtn_Click" />
                    </ContentTemplate> 
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="SubmitPasswordBtn" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </div>
        </div>
<!--End cog Charm-->  
</asp:Content>
