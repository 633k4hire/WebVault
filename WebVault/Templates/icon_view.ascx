<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="icon_view.ascx.cs" Inherits="WebVault.Templates.icon_view" %>
<!--Icon Template-->
<div  class="file" title='<%# Eval("ToolTip") %>' >
    <asp:LinkButton ID="IconLink"  CommandName='<%# Eval("Id") %>' runat="server" CommandArgument="" >
    <div class="i-icon icon-folder">   
        <img src='<%# Eval("Image") %>' />
    </div>
    <div class="i-title"><%# Eval("Title") %></div>

    </asp:LinkButton>
</div>