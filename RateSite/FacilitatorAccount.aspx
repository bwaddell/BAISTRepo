<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FacilitatorAccount.aspx.cs" Inherits="FacilitatorAccount" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <h2>Facilitator Account</h2>


    <div class="row">
        <div class="form-group">
            <label>First Name:</label>
            <asp:TextBox ID="FNametxt" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorfirst" runat="server" ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="FNametxt"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorfirstLength" runat="server" ErrorMessage="Maximum 20 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,20}$" ControlToValidate="FNametxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>Last Name:</label>
            <asp:TextBox ID="LNametxt" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorlast" runat="server" ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="LNametxt"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorlastlength" runat="server" ErrorMessage="Maximum 20 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,20}$" ControlToValidate="LNametxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>Title:</label>
            <asp:TextBox ID="Titletxt" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="Titletxt"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTitleLength" runat="server" ErrorMessage="Maximum 20 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,20}$" ControlToValidate="Titletxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>Organization:</label>
            <asp:TextBox ID="Orgtxt" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1Org" runat="server" ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="Orgtxt"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorOrgLength" runat="server" ErrorMessage="Maximum 40 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,40}$" ControlToValidate="Orgtxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>Location:</label>
            <asp:TextBox ID="Loctxt" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoc" runat="server" ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="Loctxt"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorLocLength" runat="server" ErrorMessage="Maximum 40 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,40}$" ControlToValidate="Loctxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>Email:</label>
            <asp:TextBox ID="Emailtxt" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatoremail" runat="server" ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="Emailtxt"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatoremailLength" runat="server" ErrorMessage="Maximum 40 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,40}$" ControlToValidate="Emailtxt"></asp:RegularExpressionValidator>
        </div>
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
    <br />
    <div class="row text-center">
        <div class="col-md-12">
            <asp:ListBox ID="EventListBox" runat="server"></asp:ListBox>
        </div>
        <div class="">
            <asp:Button ID="ViewEventbtn" runat="server" Text="View Event" OnClick="ViewEventbtn_Click" CausesValidation="false" />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

