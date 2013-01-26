<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SoftArch.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="lbTitle" runat="server" Text=""></asp:Label>
    </h2>
    <p>
        <asp:Label ID="lbHello" runat="server" Text=""></asp:Label><asp:Label ID="lbUser"
            runat="server" Text=""></asp:Label>
    </p>
    <p>
        <asp:ListBox ID="ltbRooms" runat="server"></asp:ListBox>
    </p>
    <p>
        <asp:Button ID="btEnter" runat="server" Text="" disabled="disabled"/><asp:Button ID="btLogout" runat="server" Text=""/>
    </p>
    <p>
        <asp:Label ID="lbGames" runat="server" Text=""></asp:Label><asp:Label ID="lbGCount"
            runat="server" Text=""></asp:Label>
    </p>
    <p>
        <asp:Label ID="lbWon" runat="server" Text=""></asp:Label><asp:Label ID="lbWCount"
            runat="server" Text=""></asp:Label>
    </p>
    <p>
        <asp:Label ID="lbLost" runat="server" Text=""></asp:Label><asp:Label ID="lbLCount"
            runat="server" Text=""></asp:Label>
    </p>
    <p>
        <asp:Label ID="lbMessage" runat="server" Text="">
    </p>
    <script type="text/javascript">
        $("#<%= ltbRooms.ClientID %>").change(function () {
            $("#<%= btEnter.ClientID %>").removeAttr("disabled");
        });
    </script>
</asp:Content>
