<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
    <div class="row">
        <div class="col-md-6">
            Email:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="EmailTxt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="Email is Required" ControlToValidate="EmailTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <p>Password:</p>
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="PasswordTxt" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Password is Required" ControlToValidate="PasswordTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:Button ID="ButtonLogin" runat="server" Text="Log In" onclick="ButtonLogin_Click"/>
        </div>
        <div class="col-md-6">
            <asp:Label ID="MsgLbl" runat="server" Text=""></asp:Label>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

