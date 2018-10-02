<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="WebVault.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
  
    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
                <div class="col-md-12">
            <div class="awp_box rounded shadow">      
                <div class="awp_box_title bg-grayed">
                    <span class="fg-white shadow-metro-black">Account Management</span>
                </div>
                <div  class="awp_box_content bg-grayDark fg-white shadow-metro-black" style="height:auto;">
                                     <div class="form-horizontal text-left">
                <dl class="dl-horizontal">
                    <dt>Password:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Change]" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Create]" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>
                   
                </dl>
            </div>
     
                </div>
            </div>
        </div>  

        
    </div>

</asp:Content>
