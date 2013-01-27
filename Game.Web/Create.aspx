<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Game.Create" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="lbTitle" runat="server" Text=""></asp:Label>
    </h2>
    <p>
        <asp:Label ID="lbUser" runat="server" Text=""></asp:Label><asp:TextBox ID="tbUser"
            runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lbPass" runat="server" Text=""></asp:Label><asp:TextBox ID="tbPass"
            runat="server" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lbPassCon" runat="server" Text=""></asp:Label><asp:TextBox ID="tbPassCon"
            runat="server" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btCreate" runat="server" Text="" /><asp:Button ID="btRed" runat="server" Text="" />
    </p>
    <p>
        <asp:Label ID="message" runat="server" Text=""></asp:Label>
    </p>
</asp:Content>
