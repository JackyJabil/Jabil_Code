<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowmachineOEE.aspx.cs" Inherits="OEE_DASHBOARD.ShowmachineOEE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="jquery.js"></script>
    <script type="text/javascript" >
        $(document).ready(function () {
            $.ajax({
                type: "GET",
                timeout: 5000,
                url: "GetmachineOEE.ashx?id="+<%=id%>,
                dataType: "html",
                cache: false,
                async: false,
                error: function () {
                },
                success: function (data) {
                    $('#content').html(data);
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1">
    <div >
        <div id="content"></div>
    
    </div>
    </form>
     
</body>
</html>
