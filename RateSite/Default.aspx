<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--added this script to let SingalR work ??--%>
    <script src="Scripts/jquery.signalR-2.2.2.min.js" type="text/javascript"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">

    <div class="">
        <p>Henlo World from Default.aspx</p><br />

        <p>name: <label id="name" runat="server">na</label></p><br />
        <p>message:  <label id="message" runat="server">na</label></p> <br />
        <asp:Button ID="btnBroadcast" runat="server" Text="Button" />
        <ul id="messages">

        </ul>

    </div>  


    <script>
        $(function () {
            var mychat = $.connection.rateHub;

            $('.btnBroadcast').click(function () {
                
            });

            $.connection.hub.start()
                .done(function () { console.log('Now connected, connnection ID=' + $.connection.hub.id); })
                .fail(function () { console.log('Could not Connect!'); });

            //$('.btnBroadcast').click(function () {
            //    console.log('button pressed' + message);
            //    mychat.send($('#message').text());
            //    $('#messages').append('<li>' + message + '</li>');
            //});

            mychat.client.addNewMessageToPage = function (name, message) {
                //add message to page
                console.log('client.send: ' + message + ' ' + name);
                //mychat.send($('#message').text());
                $('#messages').append('<li>' + message + '</li>');

            };
        });
    </script>

</asp:Content>

