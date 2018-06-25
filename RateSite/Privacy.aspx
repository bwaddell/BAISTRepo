<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Privacy.aspx.cs" Inherits="Privacy" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
   
    <h2>Privacy Notice</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    Page under construction
                </p>
            </div>
        </div>
    </div>

    <%-- 

    Copy the block between the dashes to create a new paragraph
    --------------------------------
    <h2>insert title</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    insert text
                </p>
            </div>
        </div>
    </div> 
    ----------------------------
    --%>

    <h2>Delete Site Cookies</h2>
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    Use this if you wish to revoke consent for the use of cookies by this website.
                    All cookies will be deleted and you will be logged off of your account.
                </p>
            </div>
        </div>
    </div>
    <asp:Button ID="delCook" runat="server" Text="Delete Cookies" OnClick="delCook_Click" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>

