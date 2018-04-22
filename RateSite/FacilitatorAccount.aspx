<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeFile="FacilitatorAccount.aspx.cs" 
    Inherits="FacilitatorAccount" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <h2>Facilitator Account</h2>


    <div class="row">
        <div class="col-md-6">
            <div class="form-group-lg h3">
                <asp:Label ID="Label2" runat="server" Text="Update Account Information"></asp:Label>
            </div>
            <div class="form-group">
                <label>First Name:</label>
                <asp:TextBox ID="FNametxt" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorfirst" runat="server" ValidationGroup="accountInfo"
                    ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="FNametxt"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorfirstLength" ValidationGroup="accountInfo"
                    runat="server" ErrorMessage="Maximum 20 characters allowed." Display="Dynamic"
                    ValidationExpression="^[\s\S]{0,20}$" ControlToValidate="FNametxt"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Last Name:</label>
                <asp:TextBox ID="LNametxt" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorlast" runat="server" ValidationGroup="accountInfo"
                    ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="LNametxt"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorlastlength" runat="server"
                    ErrorMessage="Maximum 20 characters allowed." Display="Dynamic" ValidationGroup="accountInfo"
                    ValidationExpression="^[\s\S]{0,20}$" ControlToValidate="LNametxt"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Title:</label>
                <asp:TextBox ID="Titletxt" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" ValidationGroup="accountInfo"
                    ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="Titletxt"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorTitleLength" runat="server"
                    ErrorMessage="Maximum 20 characters allowed." Display="Dynamic" ValidationGroup="accountInfo"
                    ValidationExpression="^[\s\S]{0,20}$" ControlToValidate="Titletxt"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Organization:</label>
                <asp:TextBox ID="Orgtxt" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1Org" runat="server" ValidationGroup="accountInfo"
                    ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="Orgtxt"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorOrgLength" runat="server"
                    ErrorMessage="Maximum 40 characters allowed." Display="Dynamic" ValidationGroup="accountInfo"
                    ValidationExpression="^[\s\S]{0,40}$" ControlToValidate="Orgtxt"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Location:</label>
                <asp:TextBox ID="Loctxt" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoc" runat="server" ValidationGroup="accountInfo"
                    ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="Loctxt"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorLocLength" runat="server"
                    ErrorMessage="Maximum 40 characters allowed." Display="Dynamic" ValidationGroup="accountInfo"
                    ValidationExpression="^[\s\S]{0,40}$" ControlToValidate="Loctxt"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Email:</label>
                <asp:TextBox ID="Emailtxt" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatoremail" runat="server" ValidationGroup="accountInfo"
                    ErrorMessage="Field Required" Display="Dynamic" ControlToValidate="Emailtxt"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatoremailLength" runat="server"
                    ErrorMessage="Maximum 40 characters allowed." Display="Dynamic" ValidationGroup="accountInfo"
                    ValidationExpression="^[\s\S]{0,40}$" ControlToValidate="Emailtxt"></asp:RegularExpressionValidator>
            </div>

            <div class="form-group">
                <asp:Button ID="UpdateBtn" runat="server" Text="Update Information"
                    OnClick="UpdateBtn_Click" CssClass="btn" ValidationGroup="accountInfo" />
                <asp:Label ID="Msglbl" runat="server" Text=""></asp:Label>
            </div>

        </div>


        <div class="col-md-6">
            <div class="form-group-lg h3">
                <asp:Label ID="Label1" runat="server" Text="Update Password"></asp:Label>
            </div>
            <div class="form-group">
                <label>Old Password:</label>
                <asp:TextBox ID="oldPasswordtxt" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldPassword" runat="server" ValidationGroup="updatePassword"
                    ErrorMessage="Current Password is Required" ControlToValidate="oldPasswordtxt" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label>New Password:</label>
                <asp:TextBox ID="Passwordtxt" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPassword" runat="server" ErrorMessage="New Password is Required"
                    ControlToValidate="Passwordtxt" Display="Dynamic" ValidationGroup="updatePassword"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label>Verify Password:</label>
                <asp:TextBox ID="Passwordtxt2" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidatorpswd" runat="server" ErrorMessage="Password Fields must be Identical"
                    ControlToCompare="Passwordtxt" ControlToValidate="Passwordtxt2" Operator="Equal" Display="Dynamic"
                    ValidationGroup="updatePassword"></asp:CompareValidator>
            </div>
            <div class="form-group">
                <asp:Button ID="UpdatePasswordBtn" runat="server" Text="Change Password" OnClick="UpdatePasswordBtn_Click"
                    ValidationGroup="updatePassword" CssClass="btn" />
                <asp:Label ID="Pswdlbl" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

    <asp:Panel ID="PEventList" runat="server">
        <div class="row text-center">
            <div class="col-md-12">
                <div class="form-group">
                    <h3 class="text-center">Event List</h3>
                    <asp:Table ID="tblEventList" runat="server" CssClass=""></asp:Table>

                    <asp:ListBox ID="EventListBox" runat="server" Width="100%" 
                        SelectionMode="Single" CssClass="form-control" Font-Bold="True"></asp:ListBox>

                </div>
                <div class="form-group">
                    <asp:Button ID="ViewEventbtn" runat="server" Text="View Event" OnClick="ViewEventbtn_Click"
                        CausesValidation="false" CssClass="btn" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

