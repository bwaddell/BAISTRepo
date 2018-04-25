<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
        <h2>Create Account</h2>


    <div class="row">
        <div class="form-group">
            <label>Email:</label>
            <asp:TextBox ID="EmailTxt" runat="server" TextMode="Email"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="Field Required" ControlToValidate="EmailTxt" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmailLength" runat="server" ErrorMessage="Maximum 40 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,40}$" ControlToValidate="EmailTxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>First Name:</label>
            <asp:TextBox ID="FirstNameTxt" runat="server"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirst" runat="server" ErrorMessage="Field Required" ControlToValidate="FirstNameTxt" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorfirstLength" runat="server" ErrorMessage="Maximum 20 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,20}$" ControlToValidate="FirstNameTxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>Last Name:</label>
            <asp:TextBox ID="LastNameTxt" runat="server"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Field Required" ControlToValidate="LastNameTxt" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorLastNamelength" runat="server" ErrorMessage="Maximum 20 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,20}$" ControlToValidate="LastNameTxt"></asp:RegularExpressionValidator>
        </div>
        
        <div class="form-group">
            <label>Password:</label>
            <asp:TextBox ID="PasswordTxt" runat="server"
                CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Field Required" ControlToValidate="PasswordTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label>Re-Enter Password:</label>
            <asp:TextBox ID="PasswordTxt2" runat="server"
                CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ErrorMessage="Your Passwords do not match" ControlToValidate="PasswordTxt2" ControlToCompare="PasswordTxt" Display="Dynamic" Operator="Equal"></asp:CompareValidator>
        </div>
        <div class="form-group">
            <label>Position:</label>
            <asp:TextBox ID="TitleTxt" runat="server"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" ErrorMessage="Field Required" ControlToValidate="TitleTxt" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTitlelength" runat="server" ErrorMessage="Maximum 20 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,20}$" ControlToValidate="TitleTxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>Organization:</label>
            <asp:TextBox ID="OrgTxt" runat="server"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorOrg" runat="server" ErrorMessage="Field Required" ControlToValidate="OrgTxt" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorOrgLength" runat="server" ErrorMessage="Maximum 40 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,40}$" ControlToValidate="OrgTxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>Location:</label>
            <asp:TextBox ID="LocTxt" runat="server"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoc" runat="server" ErrorMessage="Required Field" ControlToValidate="LocTxt" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorLocLength" runat="server" ErrorMessage="Maximum 40 characters allowed." Display="Dynamic"
                    ValidationExpression = "^[\s\S]{0,40}$" ControlToValidate="LocTxt"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" CssClass="btn btn-dark" />
            <asp:Label ID="MsgLbl" runat="server" Text=""></asp:Label>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

