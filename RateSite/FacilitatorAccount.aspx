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
        <label>First Name:</label>
        <asp:TextBox ID="FNametxt" runat="server" CssClass="form-control"></asp:TextBox>
        <label>Last Name:</label>
        <asp:TextBox ID="LNametxt" runat="server" CssClass="form-control"></asp:TextBox>
        <label>Title:</label>
        <asp:TextBox ID="Titletxt" runat="server" CssClass="form-control"></asp:TextBox>
        <label>Organization:</label>
        <asp:TextBox ID="Orgtxt" runat="server" CssClass="form-control"></asp:TextBox>
        <label>Location:</label>
        <asp:TextBox ID="Loctxt" runat="server" CssClass="form-control"></asp:TextBox>
        <label>Email:</label>
        <asp:TextBox ID="Emailtxt" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
    </div>
    <br />
    <div class="row">
        <asp:Button ID="UpdateBtn" runat="server" Text="Update Information" OnClick="UpdateBtn_Click" CausesValidation="false" />
        <asp:Label ID="Msglbl" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12 text-centre">
            <asp:Label ID="Label1" runat="server" Text="Update Password" Font-Size="Large" Font-Bold="true"></asp:Label>
        </div>
        <label>Old Password:</label>
        <asp:TextBox ID="oldPasswordtxt" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldPassword" runat="server" ErrorMessage="Current Password is Required" ControlToValidate="oldPasswordtxt" Display="Dynamic"></asp:RequiredFieldValidator>

        <label>New Password:</label>
        <asp:TextBox ID="Passwordtxt" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPassword" runat="server" ErrorMessage="New Password is Required" ControlToValidate="Passwordtxt" Display="Dynamic"></asp:RequiredFieldValidator>

        <label>Verify Password:</label>
        <asp:TextBox ID="Passwordtxt2" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidatorpswd" runat="server" ErrorMessage="Password Fields must be Identical" ControlToCompare="Passwordtxt" ControlToValidate="Passwordtxt2" Operator="Equal" Display="Dynamic"></asp:CompareValidator>
    </div>
    <br />
    <div class="row">
        <asp:Button ID="UpdatePasswordBtn" runat="server" Text="Change Password" OnClick="UpdatePasswordBtn_Click" />
        <asp:Label ID="Pswdlbl" runat="server" Text=""></asp:Label>
    </div>

    <div class="row text-center">
        <div class="col-md-12">
            <asp:ListBox ID="EventListBox" runat="server"></asp:ListBox>
        </div>
        <div class="">
            <asp:Button ID="ViewEventbtn" runat="server" Text="View Event" OnClick="ViewEventbtn_Click" CausesValidation="false" />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

