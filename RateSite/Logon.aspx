<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <h3 class="mt-5 text-center">Login</h3>
    <p class=" text-center">Please login below</p>


    <div class="row">
        <div class="col-md-6 col-md-offset-3">

            <div class="form-group row">
                <label class="col-md-6">
                    Email:
                </label>
                <div class="col-md-6">
                    <asp:TextBox ID="EmailTxt" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                        ErrorMessage="Email is Required" ControlToValidate="EmailTxt"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>


            <div class="form-group row">
                <label class="col-md-6">
                    Password:
                </label>
                <div class="col-md-6">
                    <asp:TextBox ID="PasswordTxt" runat="server" class="form-control"
                        TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword"
                        runat="server" ErrorMessage="Password is Required"
                        ControlToValidate="PasswordTxt" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group row">
                <label class="col-md-6">
                    Remember Me?:
                </label>
                <div class="col-md-6">
                    <label class="btn btn-default">
                        <asp:CheckBox ID="RememberChk" runat="server" Text="Yes / No" />
                    </label>
                </div>
            </div>

            <div class="for-group row">
                <div class="col-md-6">
                    <asp:Label ID="MsgLbl" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Button ID="ButtonLogin" runat="server" Text="Log In"
                        OnClick="ButtonLogin_Click" CssClass="btn btn-lg btn-primary" />
                </div>
            </div>

        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

