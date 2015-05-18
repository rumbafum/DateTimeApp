﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DateTimeOffsetApp.Default" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//code.jquery.com/jquery-1.11.3.min.js"></script>
</head>
<body>
    <form id="DateForm" runat="server">
    <div>
        <span><%= new DateTime(2015, 5, 18, 16, 11, 0, DateTimeKind.Local) %></span>
        <br />
        <span><%= DateTimeOffset.Now.ToLocalTime() %></span>
        <br />
        <span><%= dt %></span>
        <br />
        <span class="clientOffset"></span>
    </div>
    <div style="width:1px;height:20px">
    </div>
    <div>
    <asp:Button ID="OKButton" runat="server" Text="Button"  
            OnClientClick="AddDateInformation()" onclick="OKButton_Click" />
    <asp:HiddenField ID="DateInfo"  Value=""  runat="server" />

    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" EditFormat="Custom" EditFormatString="dd-MM-yyyy hh:mm:ss tt"></dx:ASPxDateEdit>
    <dx:ASPxTimeEdit ID="timeEdit" runat="server"></dx:ASPxTimeEdit>
    <br />

    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            var offset = (new Date().getTimezoneOffset() / 60) * (-1);
            $.ajax({
                url: 'SetClientOffsetHandler.ashx',
                data: 'offset=' + offset,
                type: 'POST',
                success: function (resp) {
                    $('.clientOffset').text('<%= GetClientOffset() %>');
                }
            });

        });

    </script>
    </form>
</body>
</html>
