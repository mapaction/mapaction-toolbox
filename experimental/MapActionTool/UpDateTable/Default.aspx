<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<br />
        <br />
        <br />
        &nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Style="z-index: 101; left: 13px;
            position: absolute; top: 40px">
            <asp:ListItem>GeoExtent</asp:ListItem>
            <asp:ListItem Value="DataCategories">Data Categories</asp:ListItem>
            <asp:ListItem>ThemeCodes</asp:ListItem>
            <asp:ListItem Value="DataTypeCodes">Data type codes</asp:ListItem>
            <asp:ListItem>ScaleCodes</asp:ListItem>
            <asp:ListItem>SourceCodes</asp:ListItem>
            <asp:ListItem>Permission</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="lblTitle" runat="server" Font-Names="Arial" Style="z-index: 102; left: 13px;
            position: absolute; top: 14px" Text="PLEASE USE THIS FORM TO UPDATE THE MAPACTION TABLE"
            Width="614px"></asp:Label>
    
    </div>
    </form>
</body>
</html>
