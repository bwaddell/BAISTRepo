<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FacilitatorAccount.aspx.cs" Inherits="FacilitatorAccount" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
    <div class="row">
        <div class="text-centre">
            <asp:Label ID="Namelbl" runat="server" Text=""></asp:Label>
        </div>       
    </div>
    <div class="row">
        <div class="col-md-6 text-right">
            First Name:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="FNametxt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 text-right">
            Last Name:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="LNametxt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 text-right">
           Title:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Titletxt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 text-right">
            Organization:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Orgtxt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 text-right">
            Location:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Loctxt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 text-right">
            Email:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Emailtxt" runat="server"></asp:TextBox>
        </div>

        <div class="col-md-6 text-right">
            <asp:Button ID="UpdateBtn" runat="server" Text="Update Information" OnClick="UpdateBtn_Click" CausesValidation="false" />
        </div>
        <div class="col-md-6">
            <asp:Label ID="Msglbl" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 text-centre">
            <asp:Label ID="Label1" runat="server" Text="Update Password"></asp:Label>
        </div>
        <div class="col-md-6 text-right">
            Old Password:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="oldPasswordtxt" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldPassword" runat="server" ErrorMessage="Current Password is Required" ControlToValidate="oldPasswordtxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 text-right">
            New Password:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Passwordtxt" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPassword" runat="server" ErrorMessage="New Password is Required" ControlToValidate="Passwordtxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 text-right">
            Verify Password:
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="Passwordtxt2" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidatorpswd" runat="server" ErrorMessage="Password Fields must be Identical" ControlToCompare="Passwordtxt" ControlToValidate="Passwordtxt2" Operator="Equal" Display="Dynamic"></asp:CompareValidator>
        </div>
        <div class="col-md-6 text-right">
            <asp:Button ID="UpdatePasswordBtn" runat="server" Text="Change Password" OnClick="UpdatePasswordBtn_Click" />
        </div>
        <div class="col-md-6">
            <asp:Label ID="Pswdlbl" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="row text-center">
        <div class="col-md-12">
            <asp:ListBox ID="EventListBox" runat="server"></asp:ListBox>
        </div>
        <div class="">
            <asp:Button ID="ViewEventbtn" runat="server" Text="View Event" OnClick="ViewEventbtn_Click" CausesValidation="false" />
        </div>
    </div>

    <div class="row">
        <div>
            <asp:Button ID="ButtonLogout" runat="server" Text="Log Out" OnClick="ButtonLogout_Click" CausesValidation="false" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

