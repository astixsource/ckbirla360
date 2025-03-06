<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">

    <title>CK Birla Group 360</title>
    <%--<link href="Images/favicon.ico" rel="shortcut icon" type="image/x-icon" />--%>
    <!-- CSS Files -->
    <link href="Content/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery -->
    <script src="Scripts/jqueryJS.js"></script>
    <script src="Scripts/loginJS.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="full-background">
            <img src="Images/login-bg.jpg" class="bg-img" />
        </div>

        <div class="wrapper">
            <div class="container-fluid">
                <div class="loginfrm cls-4">
                    <div class="login-box alt">
                        <div class="toggle"></div>
                    </div>
                    <div class="login-box">
                        <div class="login-logo">
                            <asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/CKBIRLA_LOGO.svg" title="logo" />
                            <asp:Image ID="ImgLogo2" runat="server" ImageUrl="~/Images/deloitte_black.svg" title="logo" />
                        </div>
                        <!-- Login Title -->
                        <div class="login-box-msg">
                            <h3 class="title">Login</h3>
                        </div>

                        <!-- Login Form -->
                        <div class="login-box-body clearfix">
                            <div class="frm-group">
                                <input type="text" id="txtLoginID" class="form-ctrl" name="user name" placeholder="Enter your User Name" autocomplete="off" />
                                <span class="form-focus"></span>
                                <span class="form-symbol"><i class="fa fa-envelope"></i></span>
                            </div>
                            <div class="frm-group">
                                <input type="password" id="txtPassword" class="form-ctrl" name="password" placeholder="Enter your Password" autocomplete="off" />
                                <span class="form-focus"></span>
                                <span class="form-symbol"><i class="fa fa-lock"></i></span>
                            </div>
                            <div class="text-center">
                                <div id="dvMessage" runat="server" class="text-danger font-weight-bold"></div>
                            </div>
                            <input type="button" id="btnLogin" value="Login" class="btns btn-submit w-100" />
                        </div>
                    </div>
                    <div class="login-box"></div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hdnaccesstoken" />

        <div class="loader_bg" id="dvFadeForProcessing">
            <div class="loader"></div>
        </div>
    </form>

</body>
</html>
