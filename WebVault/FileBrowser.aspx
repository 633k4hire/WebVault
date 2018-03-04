<%@ Page Title="Browser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileBrowser.aspx.cs" Inherits="WebVault.FileBrowser" %>
<asp:Content ID="FileBrowserContent" ContentPlaceHolderID="MainContent" runat="server">   
    <link href="Content/FileBrowser.css" rel="stylesheet" />
    <script src="Scripts/FileBrowser.js"></script>
<!--Header,directory tree, file browser, footer for upload-->
    <div id="appcontainer">
        <div id="toolbar"></div>
        <div id="browserbox">
            <div id="directorytree">
                <asp:UpdatePanel ID="DirectoryTreeUpdatePanel" ClientIDMode="Static" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">
                    <ContentTemplate>
                      <asp:TreeView style="width:400px; height:200px;" OnTreeNodeCheckChanged="DirTree_TreeNodeCheckChanged" 
                          OnSelectedNodeChanged="DirTree_SelectedNodeChanged" 
                          ShowLines="true" 
                          ExpandDepth="1" s
                          runat="server"
                          ID ="DirTree" 
                          ClientIDMode="Static" 
                          NodeWrap="false"
                          ShowCheckBoxes="All"  >
                      </asp:TreeView>    
                    </ContentTemplate>
          
                </asp:UpdatePanel>
            </div>
            <div id="filebox">
                <asp:UpdatePanel ID="FileBoxUpdatePanel" ClientIDMode="Static" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">
                    <ContentTemplate>
                        <asp:MultiView ActiveViewIndex="0" runat="server">
                            <asp:View ID="FileViewer" ClientIDMode="Static" runat="server">
                                file
                            </asp:View>
                            <asp:View ID ="DirViewer" ClientIDMode="Static" runat="server">
                                directory
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
         </div>
        <div id="footer"></div>
    </div>
</asp:Content>
