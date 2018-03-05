<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateEvent.aspx.cs" Inherits="CreateEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">



    <div class="form-group border">

    
    Event ID:
    <asp:TextBox ID="tbEventID" runat="server">1</asp:TextBox>
    <br />
    Facilitator ID:
    <asp:TextBox ID="tbEvaluatorID" runat="server">1</asp:TextBox>
    <br />
    Location:
    <asp:TextBox ID="TextBox1" runat="server">1</asp:TextBox>
    <br />
    Performer:
    <asp:TextBox ID="TextBox2" runat="server">1</asp:TextBox>
    <br />
    Nature Of Performance:
    <asp:TextBox ID="TextBox3" runat="server">1</asp:TextBox>
    <br />
    Event Date:
    <asp:TextBox ID="TextBox4" runat="server">1</asp:TextBox>
    <br />
    Event Begin:
    <asp:TextBox ID="TextBox5" runat="server">1</asp:TextBox>
    <br />
    Event End:
    <asp:TextBox ID="TextBox6" runat="server">1</asp:TextBox>

    </div>




    <asp:Button ID="btnCreateEvent" runat="server" Text="Create Event" OnClick="btnCreateEvent_Click" />






</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

