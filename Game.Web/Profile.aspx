<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Profile.aspx.cs" Inherits="Game.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="lbTitle" runat="server" Text="" />
    </h2>
    <p>
        <asp:Label ID="lbHello" runat="server" Text="" />
        <asp:Label ID="lbUser" runat="server" Text="" />
    </p>
    <p>
        <asp:ListBox ID="lbxRooms" runat="server" />
    </p>
    <p>
        <asp:Button ID="btnEnter" runat="server" Text="" disabled="disabled" UseSubmitBehavior="true" />
        <asp:Button ID="btnLogout" runat="server" Text="" />
    </p>
    <p>
        <asp:Label ID="lbxGames" runat="server" Text="" />
        <asp:Label ID="lblGCount" runat="server" Text=""/>
    </p>
    <p>
        <asp:Label ID="lblWon" runat="server" Text="" />
        <asp:Label ID="lblWCount" runat="server" Text=""/>
    </p>
    <p>
        <asp:Label ID="lblLost" runat="server" Text="" />
        <asp:Label ID="lblLCount" runat="server" Text="" />
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server" Text="" />
    </p>

    <script type="text/javascript">
        $("#<%= lbxRooms.ClientID %>").change(function () {
            $("#<%= btnEnter.ClientID %>").removeAttr("disabled");
            // alert($("#<%= lbxRooms.ClientID %>").attr("selectedIndex"));
            return true;
        });
    </script>
</asp:Content>
