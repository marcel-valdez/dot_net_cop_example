<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Game._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        <asp:Label ID="lbTitle" runat="server" Text=""></asp:Label>
    </h2>
    <p>
        <asp:Label ID="lbUser" runat="server" Text=""></asp:Label>
        <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lbPass" runat="server" Text=""></asp:Label>
        <asp:TextBox ID="tbPass" runat="server" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnLogin" runat="server" Text="" />
    </p>
    <p>
        <asp:Label ID="lbMsg" runat="server" Visible="false" Text=""></asp:Label>
    </p>
    <p>
        <asp:LinkButton ID="lkBCreate" runat="server">
            <asp:Label ID="lbCreate" runat="server" Text=""></asp:Label>
        </asp:LinkButton>
    </p>
    <p>
        <asp:Label ID="message" runat="server" Text=""></asp:Label>
    </p>
</asp:Content>
