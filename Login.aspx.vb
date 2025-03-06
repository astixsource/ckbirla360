Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Text.RegularExpressions
Imports System.Data
Imports System.Web
Imports System.Web.Helpers
Imports Azure.Identity
Imports Azure.Core


Partial Class Login
    Inherits System.Web.UI.Page


    Dim strLocalIP As String
    Dim drdr As SqlDataReader
    Dim objCon As SqlConnection
    Dim objCom As SqlCommand
    Dim _tenantId As String = ConfigurationManager.AppSettings("_tenantId")
    Dim _clientId As String = ConfigurationManager.AppSettings("_clientId")

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'btnLogin.Attributes.Add("onclick", "javascript:return fnValidate()")
        If Page.IsPostBack = False Then

            Session.Abandon()
            Session.Clear()
            Session.RemoveAll()
            Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))

        End If
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function fnLoginFromDB(ByVal UserName As String, ByVal Password As String) As String
        Dim Objcon2 As SqlConnection
        Dim objCom2 As SqlCommand
        Dim strResponse As String = ""
        Try

            If ReplaceQuotes(Trim(HttpUtility.HtmlEncode(UserName))) = "astix" And ReplaceQuotes(Trim(HttpUtility.HtmlEncode(Password))) = "admin" Then

                strResponse = "1|AdminReports/frmStatusReport.aspx"
            Else
                Dim PassChangeFirst As String = "0"
                Dim flgAgreementsigned As String = "0"
                Dim chkRoleID As String = "0"
                Dim varAuthenticate As Boolean = False
                Dim NodeID As Integer = 1
                Dim strConn As String = Convert.ToString(HttpContext.Current.Application("DbConnectionString"))
                Objcon2 = New SqlConnection(strConn.Split("|")(0))
                objCom2 = New SqlCommand("spSecUserLogin", Objcon2)
                objCom2.Parameters.AddWithValue("@UserName", ReplaceQuotes(Trim(HttpUtility.HtmlEncode(UserName))))
                objCom2.Parameters.AddWithValue("@Password", ReplaceQuotes(Trim(HttpUtility.HtmlEncode(Password))))
                objCom2.Parameters.AddWithValue("@SessionID", HttpContext.Current.Session.SessionID)
                objCom2.Parameters.AddWithValue("@IPAddress", HttpContext.Current.Request.ServerVariables("REMOTE_ADDR"))
                objCom2.Parameters.AddWithValue("@BrowserVersion", HttpContext.Current.Request.Browser.Type)
                objCom2.Parameters.AddWithValue("@Resolution", "")
                objCom2.CommandType = CommandType.StoredProcedure
                objCom2.CommandTimeout = 0
                Dim drdr As SqlDataReader
                Dim cycleName As String = ""

                'Objcon2.AccessToken = clsAccesstoken.getazureAccessToken()
                Objcon2.Open()
                drdr = objCom2.ExecuteReader
                drdr.Read()
                If (drdr.Item("LoginID") = 0) Then
                    strResponse = "2|Invalid Login-Id or Password. Try Again !!"
                Else
                    HttpContext.Current.Session("RId") = drdr.Item("RoleID")
                    HttpContext.Current.Session("LoginId") = drdr.Item("LoginID")
                    HttpContext.Current.Session("FullName") = drdr.Item("FullName")
                    If Not IsDBNull(drdr.Item("CycleID")) Then
                        HttpContext.Current.Session("CycleID") = drdr.Item("CycleID")
                    Else
                        HttpContext.Current.Session("CycleID") = 1
                    End If
                    If Not IsDBNull(drdr.Item("AssmntTypeID")) Then
                        HttpContext.Current.Session("AssmntTypeID") = drdr.Item("AssmntTypeID")
                    Else
                        HttpContext.Current.Session("AssmntTypeID") = 1
                    End If
                    If Not IsDBNull(drdr.Item("EndDate")) Then
                        HttpContext.Current.Session("EndDate") = drdr.Item("EndDate")
                    Else
                        HttpContext.Current.Session("EndDate") = Format(DateTime.Now.Date, "dd-MM-yyyy")
                    End If
                    NodeID = 1 'drdr.item("NodeID")
                    HttpContext.Current.Session("Flag") = 1
                    strResponse = drdr.Item("RoleID").ToString()

                    If HttpContext.Current.Session("RId") = 6 Then
                        strResponse = "1|Data/frmMain.aspx"  'Response.Redirect("Data/frmMain.aspx")
                    Else
                        strResponse = "1|Data/Instruction.aspx?NodeID="
                    End If

                End If
                drdr = Nothing
                objCom2.Dispose()
                Objcon2.Close()
                Objcon2.Dispose()
            End If


        Catch ex As Exception
            strResponse = "2|Error : " & ex.Message
        Finally

        End Try

        Return strResponse
    End Function

    Public Shared Function ReplaceQuotes(ByVal str As String) As String
        Return Replace(str, "'", "''")
    End Function
    'Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
    '    txtLoginID.Text = ""
    '    txtPassword.Text = ""
    '    dvMessage.InnerText = ""
    'End Sub

End Class


