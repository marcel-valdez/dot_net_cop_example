<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Battle.aspx.cs" Inherits="Game.Battle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Label ID="lbOpponent" runat="server" Text=""></asp:Label>
    </p>
    <p id="hpo">
        <asp:Label ID="hpTxtO" runat="server" Text=""></asp:Label><asp:Label ID="hpPointsO"
            runat="server" Text=""></asp:Label>
    </p>
    <div style="height: 150px; width: 520px; padding: 10px">
        <div id="c1O" style="float: left; height: 110px; width: 90px; background-color: Blue; padding: 10px"><asp:Image ID="card1O" runat="server" /></div>
        <div id="c2O" style="float: left; height: 110px; width: 90px; background-color: Blue; padding: 10px"><asp:Image ID="card2O" runat="server" /></div>
        <div id="c3O" style="float: left; height: 110px; width: 90px; background-color: Blue; padding: 10px"><asp:Image ID="card3O" runat="server" /></div>
        <div id="c4O" style="float: left; height: 110px; width: 90px; background-color: Blue; padding: 10px"><asp:Image ID="card4O" runat="server" /></div>
    </div>
    <p id="message">
        <asp:Label ID="msg" runat="server" Text=""></asp:Label>
    </p>
    <p id="result"></p>
    <p id="subMsg"></p>
    <div style="height: 150px; width: 520px; padding: 10px">
        <div id="1" class="img" style="float: left; height: 110px; width: 90px; background-color: Blue; padding: 10px"><asp:Image ID="card1P" runat="server" /></div>
        <div id="2" class="img" style="float: left; height: 110px; width: 90px; background-color: Blue; padding: 10px"><asp:Image ID="card2P" runat="server" /></div>
        <div id="3" class="img" style="float: left; height: 110px; width: 90px; background-color: Blue; padding: 10px"><asp:Image ID="card3P" runat="server" /></div>
        <div id="4" class="img" style="float: left; height: 110px; width: 90px; background-color: Blue; padding: 10px"><asp:Image ID="card4P" runat="server" /></div>
    </div>
    <p>
        <asp:Label ID="lbPlayer" runat="server" Text=""></asp:Label>
    </p>
    <p id="hpp">
        <asp:Label ID="hpTxtP" runat="server" Text=""></asp:Label><asp:Label ID="hpPointsP"
            runat="server" Text=""></asp:Label>
    </p>
    <p>
        <input id="confirm" type="button" value="Confirmar" /><asp:Button ID="btReturn" runat="server"
            Text="" /><asp:Button ID="btLogout" runat="server" Text="" />
    </p>
    <script type="text/javascript">
        var selected = 0;
        var selecteds = new Array(0, 0, 0, 0);
        var t;

        //Funciones para la primer etapa

        function initMoveBtn() {
            var str;
            $("#confirm").click(function () {
                $(this).attr("disabled", "disabled");
                str = "moving" + "," + String(selected);
                CallServer(str, "");
                unblMove("waiting");
            });
        }

        function enblMove(st) {
            $("#confirm").attr("disabled", "disabled");
            $(".img").css("background-color", "Blue");
            $(".img").bind("click", function () {
                $(this).css("background-color", "Green");
                selected = Number($(this).attr("id"));
                unblMove("moving");
            });
        }

        function unblMove(st) {
            if (st == "moving") {
                $("#confirm").removeAttr("disabled");
                $(".img").bind("click", function () {
                    $(".img").css("background-color", "Blue");
                    selected = 0;
                    enblMove("moving");
                });
            } else {
                $(".img").unbind("click");
            }
        }

        //Funciones para la segunda etapa

        function enblDispose() {
            selecteds[selected - 1] = 1;
            $(".img").bind("click", function () {
                if ($(this).attr("id") == String(selected)) {
                    $(this).unbind("click");
                } else {
                    if (selecteds[Number($(this).attr("id")) - 1] == 0) {
                        $(this).css("background-color", "Green");
                        selecteds[Number($(this).attr("id")) - 1] = "1";
                    } else {
                        $(this).css("background-color", "Blue");
                        selecteds[Numvber($(this).attr("id")) - 1] = "0";
                    }
                }
            });
            $("#confirm").bind("click", function () {
                str = "dispose" + "," + String(selecteds[0]) + "," +
                                        String(selecteds[1]) + "," +
                                        String(selecteds[2]) + "," + 
                                        String(selecteds[3]);
                unblDispose();
                CallServer(str, "");
            });
        }

        function unblDispose() {
            $(".img").unbind("click");
        }

        //Funciones de control

        function PlayersViewHandler(arg, context) {
            var info;
            eval(arg);
            if (info.state == "waitingM") {
                $("#confirm").attr("disabled", "disabled");
                t = setTimeout(listen("waitingM"), 500);
            }
            if (info.state == "waitingD") {
                $("#confirm").attr("disabled", "disabled");
                t = setTimeout(listen("waitingD"), 500);
            }
            if (info.state == "dispose") {
                clearTimeout(t);
                $("#confirm").removeAttr("disabled");
                $("#message").text(info.MiddleMsgTitle);
                $("#result").text(info.HarmInflictedTxt + info.HarmInflictedPts + " | " + info.HarmReceivedTxt + info.HarmReceivedPts);
                $("#subMsg").text(info.MiddleMsgFooter);
                enblDispose();
            }
            if (info.state == "moving") {
                $("#<%= card1P.ClientID %>").attr("src", info.cardP1);
                $("#<%= card2P.ClientID %>").attr("src", info.cardP2);
                $("#<%= card3P.ClientID %>").attr("src", info.cardP3);
                $("#<%= card4P.ClientID %>").attr("src", info.cardP4);
                $("#<%= card1O.ClientID %>").attr("src", info.cardO1);
                $("#<%= card2O.ClientID %>").attr("src", info.cardO2);
                $("#<%= card3O.ClientID %>").attr("src", info.cardO3);
                $("#<%= card4O.ClientID %>").attr("src", info.cardO4);
                $("#message").text(info.MiddleMsgTitle);
                $("#result").text("");
                $("#subMsg").text("");
                initMoveBtn();
                enblMove();
            }
            if (info.state == "finish") {
                $("#message").text(info.MiddleMsgTitle);
                $("#subMsg").text(info.MiddleMsgFooter);
                unblMove();
                unblDispose();
                $("#confirmar").attr("disabled", "disabled");
                $("#<%= btReturn.ClientID %>").css("visibility","visible");
            }
        }

        function listen(str) {
            CallServer(str, "");
        }

        initMoveBtn();
        enblMove();

    </script>
</asp:Content>
