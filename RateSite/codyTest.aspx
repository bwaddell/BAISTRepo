<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codyTest.aspx.cs" Inherits="codyTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManagerForGraph" runat="server"></asp:ScriptManager>

            <%--<asp:Timer ID="TimerForNumRefresh" runat="server" Interval="1000" OnTick="TimerForGraphRefresh_Tick"></asp:Timer>--%>

            <asp:Button ID="btnUp" runat="server" Text="UP"  OnClick="btnUp_Click"/>

            <div class="border">
                START of panel


                <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
                    <%--INSIDE PANEL--%>
                    <%--this is where the graph and stuff goes--%>
                    <ContentTemplate>
                        <asp:Label ID="LabelRating" runat="server" Text="1"></asp:Label>
                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnUp" />
                        <asp:AsyncPostBackTrigger ControlID="btnDown" />
                        <%--<asp:AsyncPostBackTrigger ControlID="TimerForNumRefresh" EventName="Tick" />--%>
                    </Triggers>
                </asp:UpdatePanel>
                            END of panel

            <asp:Button ID="btnDown" runat="server" Text="Down" OnClick="btnDown_Click"/>

            </div>

            <%--so this button can sit outside of the panel but its trigger is inside the panel^^--%>
            <asp:Button ID="Button1" runat="server" Text="Other Button" />






        </div>
    </form>
</body>
</html>
