<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs" Inherits="WindowsAuthSample.SessionExpired" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Windows Auth Test Site</title>
    <link rel="stylesheet" type="text/css" media="screen" href="styles/main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
		    </div>
		    <div id="main">
                <div id="content">
                    <p>Session expired.</p>
                    <p>Click <a href="<%= Referrer %>">here</a> to return to where you were.</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
