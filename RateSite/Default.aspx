<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--added this script to let SingalR work ??--%>
    <script src="Scripts/jquery.signalR-2.2.2.min.js" type="text/javascript"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">

    <div class="">
        <p>Hello World from Default.aspx</p><br />

        <p>name: <label id="name" runat="server">na</label></p><br />
        <p>message:  <label id="message" runat="server">na</label></p> <br />
        <asp:Button ID="btnBroadcast" runat="server" Text="Hit"/>
        <ul id="messages">

        </ul>

    </div>  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
    <script>
        $(function () {
            var myRateHub = $.connection.rateHub;
            


            //myRateHub.hub.start()
            $.connection.hub.start()
                .done(function () { console.log('Now connected, connnection ID: ' + $.connection.hub.id); })
                .fail(function () { console.log('Could not Connect!'); });

            $('#MainBody_btnBroadcast').click(function () {
                console.log('Button pressed');
                myRateHub.server.newMessageToPage('myName', 'myMessage');
            });


            myRateHub.client.addNewMessageToPage = function (name, message) {
                //add message to page
                console.log('client.send: ' + message + ' ' + name);
                $('#messages').append('<li>' + message + ' ' + name + '</li>');
            };

        });
    </script>
</asp:Content>

