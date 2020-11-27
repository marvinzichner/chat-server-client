<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="csharp_webserver.Views.Chat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--
    Wie man dich erreicht: <span id="yourConnection" runat="server">UNKNOWN</span>
    <hr />
    Empfänger: <asp:TextBox ID="recipientField" runat="server"></asp:TextBox><br />
    
    <span id="sendingStatus" runat="server"></span>
    <hr />
    -->
    
    <table style="width: 100%">
        <tr>

            <td class="topleft chats">
                 <div class="preRow">
                    <asp:TextBox ID="newRequestUrl" autocomplete="off" onchange="urlChanged()" onkeypress="urlChanged()" style="width: 800px;" placeholder="Starte Chat mit Adresse (http://abc:1234)" runat="server"></asp:TextBox> 
                 </div>

                 <table style="width: 100%;" id="chatList" class="entry-chat" runat="server"></table>
            </td>

            <td class="topleft bordered">
                <div class="preRow">
                    <asp:TextBox ID="messageField" onkeypress="messageWritten(event);" autocomplete="off" style="width: 800px;" placeholder="Nachricht eingeben" runat="server"></asp:TextBox> 
                    <asp:Button ID="sendButton" OnClientClick="clearMessageField()" Text="Senden" runat="server" OnClick="sendMessage" />
                </div>
                
                <table id="historyTable" class="entry-bubble" runat="server"></table>
            </td>

        </tr>
    </table>



    <style>
        .preRow {
            padding:        8px;
            width:          100%;
            border-bottom:  2px solid #e6e6e6;
            text-align:     right;
        }
        .topleft {
            text-align:     left;
            vertical-align: top;
        }
        .bordered {
            border:         2px solid #e6e6e6;
        }
        .chats {
            width:          300px;
            height:         600px;
            border:         2px solid #e6e6e6;
            background-color: #f2f2f2;
        }
        .entry-chat td {
            padding:        10px;
            border-bottom:  1px solid #e6e6e6;
            background-color: white;
            font-size:      10pt;
        }
        .entry-bubble {
            width:          100%;
        }
        .entry-bubble td {
            padding:        3px;
            padding-left:   5px;
            padding-right:  5px;
        }
        .name {
            font-size:     10pt;
            color:         #3399ff;
        }
        .hideButton {
            outline:        none;
            border:         none;
            background-color: white;
        }
        .field_ok {
            border:             1px solid green;
            color:              green;
            background-color:   #b3ffcc;
        }
        .field_fail {
            border:             1px solid red;
            color:              red;
            background-color:   #ffcccc;
        }
    </style>


    <script>

        document.addEventListener("DOMContentLoaded", function (event) {
            setInterval(checkUpdate, 2000);
        });


        function checkUpdate() {

            const origin = window.location.origin;

            fetch(`${origin}/api/Refresh`)
                .then(response => response.json())
                .then(data => {

                    console.log(`checked new messages on server: action=${data.event}`);
                    if (data.event == "CHANGED") {

                        location.reload();
                    }
                });
        }

        function messageWritten(event) {

            sessionStorage.setItem("MSG_FIELD_TXT", event.target.value);
        }

        function clearMessageField() {

            sessionStorage.setItem("MSG_FIELD_TXT", "");
        }

        function urlChanged() {

            console.log("searching user");
            const field = document.getElementById("MainContent_newRequestUrl").value;

            fetch(`${field}/api/Chat`)
                .then(response => response.json())
                .then(data => {

                    console.log(`found user on server: name=${data.name}`);
                    if (data.name != null || data.name != undefined || data.name != "") {

                        document.getElementById("MainContent_newRequestUrl").classList.add("field_ok");
                        document.getElementById("MainContent_newRequestUrl").classList.remove("field_fail");
                    }
                })
                .catch(function () {

                    document.getElementById("MainContent_newRequestUrl").classList.add("field_fail");
                    document.getElementById("MainContent_newRequestUrl").classList.remove("field_ok");
                    console.error(`illegal adress; user not found`);
                });
        }

        function focus() {

            if (document.getElementById("MainContent_newRequestUrl").value != "") {
                document.getElementById("MainContent_messageField").focus();
            } else {
                document.getElementById("MainContent_newRequestUrl").focus();
            }
        }

        function fillMessageField() {

            if (sessionStorage.getItem("MSG_FIELD_TXT") == null) {
                sessionStorage.setItem("MSG_FIELD_TXT", "");
            }

            const value = sessionStorage.getItem("MSG_FIELD_TXT");
            if (value != null || value != undefined || value != "") {

                document.getElementById("MainContent_messageField").value = value;
            }
        }

        fillMessageField();
        focus();

    </script>

</asp:Content>
