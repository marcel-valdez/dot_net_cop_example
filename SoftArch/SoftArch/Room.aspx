<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Room.aspx.cs" Inherits="SoftArch.Room" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="lbTitle" runat="server" Text=""></asp:Label>
    </h2>
    <p>
        <asp:ListBox ID="ltbUsers" runat="server"></asp:ListBox>
    </p>
    <p>
        <input id="challenge" type="button" value="Retar" />
        <input id="findBattle" type="button" value="Buscar batalla" disabled="disabled" />
        <asp:Button ID="btExit" runat="server" Text="" />
        <asp:Button ID="btLogout" runat="server" Text="" />
    </p>
    <p id="complete"></p>
    <p id="username"></p>
    <p id="played"></p>
    <p id="won"></p>
    <p id="lost"></p>
    <p id="colorR" style="background-color:Red"></p><div id="colorG" style="background-color:Green"></div><div id="colorW" style="background-color:White"></div>
    <div id="dialog" title="title"></div>
    <script type="text/javascript">

        var t;

        function enblList() {
            $("#<%= ltbUsers.ClientID %>").change(function () {
                var str = "";
                $("#challenge").removeAttr("disabled");
                $("#<%= ltbUsers.ClientID %> option:selected").each(function () {
                    str = "select," + $(this).val();
                    if ($(this).css("background-color") == $("#colorR").css("background-color")) {
                        $("#challenge").attr("disabled", "disabled");
                    }
                });
                CallServer(str, "");
            });
        }

        $("#challenge").click(function () {
            CallServer("initChallenge", "");
        });

        $("#findBattle").click(function () {
            CallServer("findBattle", "");
        });

        /*function updatePlayers(info) {
            var l = $("option").length;
            var nl = info.lbActual.length();
            if (l == nl) {
                var flag = true;
                for (i = 0; i < l; i++) {
                    var equals = true;
                    if ($('option[value="' + String(i) + '"]').attr("text") == info.lbActual[i].text) {
                        equals = false;
                    }
                    flag = flag && equals;
                }
                if (!flag) {
                }
            }
            if (l > nl) { } else { }
        }*/

        function PlayersViewHandler(arg, context) {
            var info;
            eval(arg);
            if (info.response == "select") {
                $("#username").text(info.Username);
                $("#played").text(info.lbPlayed + " " + info.lbPCount);
                $("#won").text(info.lbWon + " " + info.lbWCount);
                $("#lost").text(info.lbLost + " " + info.lbLCount);
            }
            if (info.response == "waitFirst") {
                createDialog(info);
                t = setTimeout(listen("waiting"), 500);
            }
            if (info.response == "findFirst") {
                createDialog(info);
                t = setTimeout(listen("finding"), 500);
            }
            if (info.response == "challenge") {
                createDialog(info);
                clearTimeout(t);
            }
            if (info.response == "cancel") {
                $("#dialog").dialog("close");
                $("#dialog").dialog("destroy");
                t = setTimeout(listen("listen"), 500);
            }
            if (info.response == "listen") {
                //updatePlayers(info);
                t = setTimeout(listen("listen"), 500);
            }
        }

        function btObject(text,index){
            this.text = text;
            this.click = function () {
                $("#dialog").dialog("close");
                $("#dialog").dialog("destroy");
                var str = "pressed," + String(index);
                CallServer(str, "");
            };
        }

        function createDialog(info) {
            $("#dialog").attr("title", info.title);
            $("#dialog").text(info.dialogMessage);
            var bt = new Array();
            for (i = 0; i < info.buttons.length; i++) {
                bt[i] = new btObject(info.buttons[i], i);
            }
            $("#dialog").dialog({
                closeOnEscape: false,
                closeText: "",
                autoOpen: false,
                resizable: false,
                modal: true,
                buttons: bt
            });
            $("#dialog").dialog("open");
        }

        function listen(arg) {
            CallServer(arg, "");
        }

        enblList();
        listen("listen");

    </script>
</asp:Content>
