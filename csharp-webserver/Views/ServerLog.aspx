<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServerLog.aspx.cs" Inherits="csharp_webserver.Views.ServerLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table id="logTable" runat="server" style="width: 100%;"></table>

    <style>
        table {
            width:          100%;
        }

        td {
            padding:        6px;
        }
        .info {
            background-color:   #e6f9ff;
        }
        .warn {
            background-color:   #fff2e6;
        }
        .error {
            background-color:   #ffb3d1;
        }
    </style>

</asp:Content>
