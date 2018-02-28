<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codyTest.aspx.cs" Inherits="codyTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

    <asp:ScriptManager ID="ScriptManagerForGraph" runat="server" ></asp:ScriptManager>
        
    <asp:Timer ID="TimerForGraphRefresh" runat="server" Interval="10000" OnTick="TimerForGraphRefresh_Tick"></asp:Timer>
        
        
        START of panel
    <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
        <%--INSIDE PANEL--%>
        <%--this is where the graph and stuff goes--%>
        <ContentTemplate>
        <asp:Label ID="lbStartTime" runat="server" Text="Label"></asp:Label>

            <asp:Label ID="lbCurrentTime" runat="server" Text="Time "></asp:Label>
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        </ContentTemplate>





        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" />
            <asp:AsyncPostBackTrigger ControlID="TimerForGraphRefresh" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    END of panel


    <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
    <%--so this button can sit outside of the panel but its trigger is inside the panel^^--%>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />







    </div>
    </form>
</body>
</html>
