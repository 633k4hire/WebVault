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
                        <div id="_files" runat="server">
                            <asp:TreeView ID="_tree" CollapseImageToolTip="Close Folder" ExpandImageToolTip="Open Folder" 
                                ParentNodeStyle-VerticalPadding="10" RootNodeStyle-NodeSpacing="10" NodeStyle-HorizontalPadding="10"
                                ParentNodeStyle-HorizontalPadding="10" LeafNodeStyle-HorizontalPadding="10" LineImagesFolder="~/images/treeview"
                                ShowLines="true" NodeWrap="true" ExpandDepth="1"  runat="server"></asp:TreeView>
                        </div>
                        <div id="_noFiles" visible="false" runat="server">
                            <p>No files!</p>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="filebox">
                <asp:UpdatePanel ID="FileBoxUpdatePanel" ClientIDMode="Static" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">
                    <ContentTemplate>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
         </div>
        <div id="footer"></div>
    </div>
</asp:Content>
