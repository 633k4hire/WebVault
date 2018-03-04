<%@ Page Title="Browser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Browser.aspx.cs" Inherits="WebVault.Browser" %>
<asp:Content ID="BrowserPage" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/browser.css" rel="stylesheet" />
    <link href="Content/jquery-ui.css" rel="stylesheet" />
    
    <script src="Scripts/jquery-ui.js"></script>
    <script src="Scripts/jquery-resizable.js"></script>
    <script src="Scripts/browser.js"></script>
        <div id="appcontainer">
        <div id="toolbar"></div>
        <div id="browserbox">
                  <div class="wrap">
                    <div class="resizable resizable1">
                      <div class="inner">
                         <asp:UpdatePanel ID="DirectoryTreeUpdatePanel" ClientIDMode="Static" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">
                            <ContentTemplate>
                              <asp:TreeView OnTreeNodeCheckChanged="DirTree_TreeNodeCheckChanged" 
                                  OnSelectedNodeChanged="DirTree_SelectedNodeChanged" 
                                  ShowLines="true" 
                                  ExpandDepth="1" 
                                  runat="server"
                                  ID ="DirTree" 
                                  ClientIDMode="Static" 
                                  NodeWrap="false"
                                  ShowCheckBoxes="All"  >
                              </asp:TreeView>    
                            </ContentTemplate>
          
                        </asp:UpdatePanel>

                      </div>  
                    </div>
                    <div class="resizable resizable2">
                      <div class="inner">
                        <asp:UpdatePanel ID="FileBoxUpdatePanel" ClientIDMode="Static" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">
                            <ContentTemplate>
                                <asp:MultiView ID="BrowserMulti" ActiveViewIndex="0" runat="server">
                                    <asp:View ID="DirViewer" ClientIDMode="Static" runat="server">
                                        <asp:Repeater  OnItemCreated="FileRepeater_ItemCreated" ID="FileRepeater" runat="server" EnableViewState="false">
                                            <ItemTemplate>
                                             
                                            </ItemTemplate>
                                        </asp:Repeater>    
                                    </asp:View>
                                    <asp:View ID ="FileViewer" ClientIDMode="Static" runat="server">
                                      File View
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DirClick" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="FileClick" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                      </div>  
                    </div>
                </div>

         </div>
        <div id="footer"></div>
    </div>
    <asp:TextBox runat="server" ID ="avSelectedView" ClientIDMode="Static" style="display:none"></asp:TextBox>
    <asp:TextBox runat="server" ID ="DirID" ClientIDMode="Static" style="display:none"></asp:TextBox>
    <asp:TextBox runat="server" ID ="FileID" ClientIDMode="Static" style="display:none"></asp:TextBox>
<asp:Button ID="avChangeView" runat="server" Text="CLICK ME" OnClick="avChangeView_Click" style="display:none" ClientIDMode="Static"/>
<asp:Button ID="DirClick" runat="server" Text="CLICK ME" OnClick="DirClick_Click" style="display:none" ClientIDMode="Static"/>
<asp:Button ID="FileClick" runat="server" Text="CLICK ME" OnClick="FileClick_Click" style="display:none" ClientIDMode="Static"/>

</asp:Content>
