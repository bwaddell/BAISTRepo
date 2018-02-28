<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codyTest.aspx.cs" 
    Inherits="codyTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManagerForGraph" runat="server"></asp:ScriptManager>

            <asp:Timer ID="TimerForNumRefresh" runat="server" Interval="5000" OnTick="TimerForNumRefresh_Tick"></asp:Timer>


            <div class="border">
                START of panel


                <asp:UpdatePanel ID="UpdatePanelGraph" runat="server" UpdateMode="Conditional">
                    <%--INSIDE PANEL--%>
                    <%--this is where the graph and stuff goes--%>
                    <ContentTemplate>
                        <asp:Label ID="LabelTime" runat="server" Text="time"></asp:Label>
                        <asp:Label ID="LabelRating" runat="server" Text="LabelRating"></asp:Label>
                        <%--CHART--%>
                        <asp:Chart ID="Chart1" runat="server" Height="500px" Width="500px">
                            <Titles>
                                <asp:Title ShadowOffset="3" name="Things" />
                            </Titles>
                            <Legends>
                                <asp:Legend Alignment ="Center" Docking="Bottom" 
                                    IsTextAutoFit="false" Name="Default" LegendStyle="Row" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Series1"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" ></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>


                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TimerForNumRefresh" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
                            END of panel


            </div>

            <%--so this button can sit outside of the panel but its trigger is inside the panel^^--%>
            <asp:Button ID="Button1" runat="server" Text="Other Button" />






        </div>
    </form>
</body>
</html>
