<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="fast_claims_v1._2.Views.Admin.dummy_views.WebForm1" %>

<%@ Register assembly="Infragistics4.WebUI.WebResizingExtender.v19.1, Version=19.1.20191.115, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI" tagprefix="igui" %>

<!DOCTYPE html>
<link href="../../../Content/bootstrap.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <div>
            <textarea>.</textarea>
            <asp:Image ID="Image1" runat="server" Height="437px" Width="394px" />
            <igui:WebResizingExtender ID="Image1_WebResizingExtender" runat="server" TargetControlID="Image1">
            </igui:WebResizingExtender>
            <asp:Table ID="Table1" runat="server" Height="717px" Width="736px">
            </asp:Table>
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
            </asp:CheckBoxList>
        </div>
    </form>
</body>
</html>
