<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--added this script to let SingalR work ??--%>
    <script src="Scripts/jquery.signalR-2.2.2.min.js" type="text/javascript"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" Runat="Server">

    <div class="">
        <p>Hello World from Default.aspx</p><br />

        <p>name: 
            <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
        </p><br />
        <p>message:  
            <asp:TextBox ID="tbMessage" runat="server"></asp:TextBox>
        </p> <br />
        <asp:Button ID="btnBroadcast" runat="server" Text="Broadcast" OnClick="btnBroadcast_Click"/>
        <asp:Button ID="btnJoinGroup" runat="server" Text="Join Group" OnClick="btnJoinGroup_Click"/>
        <ul id="messages">

        </ul>

    </div>  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
    <script>
        $(function () {
            var myRateHub = $.connection.rateHub;

            //myRateHub.connection.rateHub.start()
            $.connection.hub.start()
                .done(function () {
                    console.log('Now connected, connnection ID: ' + $.connection.hub.id);
                    console.log('Group name: ' + $.connection.hub.group);
                })
                .fail(function () { console.log('Could not Connect!'); });

            //this is the trigger event called when: btnBroadcast is clicked 
            //the method in the hub is called
            $('#MainBody_btnBroadcast').click(function () {
                console.log('Broadcast Button pressed');
                //var formName = document.getElementById('MainBody_tbName').value;
                //var formMessage = document.getElementById('MainBody_tbMessage').value;
                //myRateHub.server.newMessageToPage(formName, formMessage);
            });
            //the above fucntion has been moved to the broadcast_click function in codebehind

            $('#MainBody_btnJoinGroup').click(function () {
                console.log('Join Group Button pressed');
                //var formName = document.getElementById('MainBody_tbName').value;
                //var formMessage = document.getElementById('MainBody_tbMessage').value;
                var group = "mygroup";
                myRateHub.server.joinGroup('mygroup');
                console.log('Group name: ' + $.connection.hub.group);
            });

            //this function displays the data to the client page
            //this event is called when the hub tells the clients to call it.
            myRateHub.client.addNewMessageToPage = function (name, message) {
                //add message to page
                console.log('client.send: ' + name + ' ' + message);
                console.log('Group name: ' + $.connection.hub.groupID);
                $('#messages').append('<li><b>' + name + ':</b> ' + message + '</li>');
            };

        });
    </script>
</asp:Content>

