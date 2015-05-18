<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DateTimeOffsetApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//code.jquery.com/jquery-1.11.3.min.js"></script>
</head>
<body>
    <form id="DateForm" runat="server">
    <div>
        <span><%= DateTime.Now.ToLocalTime() %></span>
        <br />
        <span><%= DateTimeOffset.Now.ToLocalTime() %></span>
        <br />
        <span class="clientOffset"></span>
    </div>
    <div style="width:1px;height:20px">
    </div>
    <div>
    <center>
       <asp:Label ID="Label1" runat="server" Text="Enter a Date and Time:" Width="248px"></asp:Label>
       <asp:TextBox ID="DateString" runat="server" Width="176px"></asp:TextBox><br />
    </center>       
    <br />
    <center>
    <asp:Button ID="OKButton" runat="server" Text="Button"  
            OnClientClick="AddDateInformation()" onclick="OKButton_Click" />
    <asp:HiddenField ID="DateInfo"  Value=""  runat="server" />
    </center>
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
