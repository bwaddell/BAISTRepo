<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Privacy.aspx.cs" Inherits="Privacy" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">
   
    <h2>Privacy Notice</h2>

    <h2>Cookies</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    A Cookie is a small collection of data sent from a website and stored in your browser.  
                    ContinUI uses Cookies to perform functions such as keeping you logged in for later visits and for facilitating the connection of evaluators to open events.  
                    Most browsers will allow you to decline the use of these Cookies, however some functionality of this website may not work properly.
                </p>
            </div>
        </div>
    </div>

    <h2>Analytics</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    This site uses Google Analytics to analyze activity and trends for how ContinUI is used.  
                    You can read more on Google's practices and their use of Cookies <a href="https://policies.google.com/technologies/partner-sites">here</a>.
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

