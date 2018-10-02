<%@ Page Title="One Time Pad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OTP.aspx.cs" Inherits="WebVault.OTP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/jquery.dm-uploader.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.dm-uploader.js"></script>
    <script src="Scripts/demo-ui-otp.js"></script>
    <script src="Scripts/demo-config-otp.js"></script>
    <link href="Content/browser.css" rel="stylesheet" />
    <script src="Scripts/webapp.js"></script>
    <script src="Scripts/Resizer-otp.js"></script>
            <div id="toolbar" style="margin:0px !important;">
<div class="l0x-toolbar" style="height:auto !important;">
                        <div class="l0x-toolbar-btn"><asp:LinkButton CssClass="fg-white  flat-link font-2-5x"  runat="server" ID="DownloadKeyFileBtn" OnClick="DownloadKeyFileBtn_Click" >  <span title="Download KeyFile" class="fa font-1x fa-key v-align-middle"></span></asp:LinkButton></div>

                         <asp:UpdatePanel style ="display:inline-flex !important; vertical-align:top !important"  ID="CogUpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                             <ContentTemplate>
                                 

                                 <div class="l0x-toolbar-btn hidden" style="vertical-align:bottom" ><b>Mode:</b></div>
                                 <div class="l0x-toolbar-switch hidden" style="top:-15px !important;">                             
                                     <span style="padding-top:8px !important">            
                                    <label class="awp-switch">
                                        <input title="Mode" id="OtpModeSwitch"  type="checkbox"  onchange="ToggleOtpMode()">
                                        <span class="check"></span>
                                    </label>
                                    </span>
                                 </div>


                                 <div class="l0x-toolbar-Seperator bg-white hidden"></div>   

                                 <div class="l0x-toolbar-btn" style=" vertical-align:top !important" >
                                     <asp:TextBox  id="KeyField" Width="400px" runat="server" class="form-control bau-bold" ToolTip="One Time Key" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                                 <div class="l0x-toolbar-btn" style=" vertical-align:top !important" >
                                     <asp:Button runat="server" ID="SubmitPasswordBtn" ClientIDMode="Static" CssClass="form-control bau-bold" Text="Update Key" OnClick="SubmitPasswordBtn_Click" />
                                 </div>
                                 <div class="l0x-toolbar-Seperator bg-white hidden"></div>   

                                 <div class="l0x-toolbar-btn hidden " ><asp:LinkButton CssClass="fg-white flat-link font-2-5x"  runat="server" ID="EncryptModeBtn" OnClick= "EncryptModeBtn_Click" ><span title="Back" class="fa font-1x fa-angle-left v-align-middle"></span></asp:LinkButton></div>
                                 <div class="l0x-toolbar-btn hidden "><asp:LinkButton CssClass="fg-white  flat-link font-2-5x"  runat="server" ID="DecryptModeBtn" OnClick="DecryptModeBtn_Click" >  <span title="Home" class="fa font-1x fa-home v-align-middle"></span></asp:LinkButton></div>


                                    
                             </ContentTemplate> 
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="SubmitPasswordBtn" EventName="Click" />
                            </Triggers>
                         </asp:UpdatePanel>
</div>   
                    </div>
                
    <asp:UpdatePanel ID="OTP_UpdatePanel" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">
        <ContentTemplate>        
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="awp_box rounded shadow">      
                                        <div class="awp_box_title bg-grayDark">
                                            <span class="fg-white shadow-metro-black">OTP</span>
                                        </div>
                                        <div class="awp_box_content bg-grayDark">
                                            <div class="row">
                                               <!-- <div class ="col-md-4" style="height:180px">
                                                    <div id="keyfile-drag-and-drop-zone" class="dm-uploader p-5" style="height:100%;">
                                                        <h3 class="mb-5 mt-5 fg-white shadow-metro-black ">Drag &amp; drop KeyFile here</h3>
                                                        <div class="btn btn-primary btn-block mb-5">
                                                            <span>Browse</span>
                                                            <input type="file" title='Click to add KeyFile'/>
                                                        </div>
                                                    </div>
                                                </div>    /uploader -->
                                                <div class="col-md-12" style="height:180px">
                                                    <div id="drag-and-drop-zone" class="dm-uploader p-5" style="height:100%;">
                                                        <h3 class="mb-5 mt-5 fg-white shadow-metro-black ">Drag &amp; drop a File or KeyFile here</h3>

                                                        <div class="btn btn-primary btn-block mb-5">
                                                            <span>Browse</span>
                                                            <input type="file" title='Click to add Files'/>
                                                        </div>
                                                    </div><!-- /uploader -->
                                                </div>
                                            </div>                                        
                                        </div>
                                    </div>
                                </div>    
                            </div>
       

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="EncryptModeBtn" EventName="click" />
             <asp:AsyncPostBackTrigger ControlID="DecryptModeBtn" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
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
    <!--cog Charm
        <div id="CogBox"  class=" transition-bottom">
       <span class="modal-closer" onclick="ToggleCog()"><span id="cog-chevron"  class=" glyphicon glyphicon-chevron-up fg-white"></span></span>
            <div id="cog-box-label" onclick="ToggleCog()"><span style="font-size:1em;" class=" glyphicon glyphicon-cog fg-white shadow-metro-black"></span>
            <asp:Label Text="Settings" ID="CogLabel" ClientIDMode="Static" CssClass="fg-white shadow-metro-black" runat="server" />

            </div>          
            <div id="cog-box-content" class="fg-white">
                    
            </div>
        </div>
End cog Charm-->  
    <!--Dailogs-->
    <asp:Button ID="DownloadButton" runat="server" Text="" OnClick="DownloadButton_Click" style="display:none" ClientIDMode="Static"/>

    <asp:Button ID="SuperButton" runat="server" Text="" OnClick="SuperButton_Click" style="display:none" ClientIDMode="Static"/>
    <asp:TextBox runat="server" ID ="SuperButtonArg" ClientIDMode="Static" style="display:none"></asp:TextBox>

</asp:Content>
