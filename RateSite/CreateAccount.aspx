<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
    <div class="row">
        <div class="col-md-6 text-right">
            Email:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="EmailTxt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="Field Required" ControlToValidate="EmailTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 text-right">
            First Name:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="FirstNameTxt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirst" runat="server" ErrorMessage="Field Required" ControlToValidate="FirstNameTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 text-right">
            Last Name:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="LastNameTxt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLast" runat="server" ErrorMessage="Field Required" ControlToValidate="LastNameTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>   
        <div class="col-md-6 text-right">
            Title:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="TitleTxt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" ErrorMessage="Field Required" ControlToValidate="TitleTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>    
        <div class="col-md-6 text-right">
            Password:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="PasswordTxt" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Field Required" ControlToValidate="PasswordTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 text-right">
            Re-Enter Password:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="PasswordTxt2" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ErrorMessage="Your Passwords do not match" ControlToValidate="PasswordTxt2" ControlToCompare="PasswordTxt" Display="Dynamic" Operator="Equal"></asp:CompareValidator>
        </div>
        <div class="col-md-6 text-right">
            Organization:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="OrgTxt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorOrg" runat="server" ErrorMessage="Field Required" ControlToValidate="OrgTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 text-right">
            Location:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="LocTxt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoc" runat="server" ErrorMessage="Required Field" ControlToValidate="LocTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 text-right">
            <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />
        </div>
        <div class="col-md-6">
            <asp:Label ID="MsgLbl" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

