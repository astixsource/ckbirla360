﻿Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class E360WebService
    Inherits System.Web.Services.WebService
    Dim strConn As String = Convert.ToString(HttpContext.Current.Application("DbConnectionString"))
    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function
    <System.Web.Services.WebMethod(EnableSession:=True)>
    Public Function fnGetCompletionStation() As String

        Dim Objcon As New SqlConnection(strConn.Split("|")(0))
        Dim objCom As New SqlCommand("spGetAppraisalStatusSummary", Objcon)
        Dim drdr As SqlDataReader
        Dim strReturn As String = ""
        objCom.Parameters.Add("@LoginID", Data.SqlDbType.Int).Value = Session("LoginId")
        objCom.Parameters.Add("@InfoReqd", Data.SqlDbType.Int).Value = 0
        objCom.Parameters.Add("@CycleID", Data.SqlDbType.Int).Value = Session("CycleId")
        objCom.CommandType = CommandType.StoredProcedure
        objCom.CommandTimeout = 0
        Try
            ' Objcon.AccessToken = strConn.Split("|")(1)
            Objcon.Open()
            drdr = objCom.ExecuteReader()
            drdr.Read()

            strReturn = drdr.Item(0) & "^" & drdr.Item(1) & "^" & drdr.Item(2)

            strReturn = "1@" & strReturn

        Catch ex As Exception
            strReturn &= "2@" & ex.Message
        Finally
            objCom.Dispose()
            Objcon.Close()
            Objcon.Dispose()
        End Try
        Return strReturn

    End Function


    <System.Web.Services.WebMethod(EnableSession:=True)>
    Public Function fnShowDispPerson() As String
        '**************************************For Feedback to others**********************************                

        Dim RSPId As Integer
        Dim RSPStatus As Integer
        Dim strReturn As String
        Dim drdr As SqlDataReader

        Dim LoginId As Integer
        LoginId = Session("LoginId")

        Dim CycleId As Integer
        CycleId = Session("CycleID")

        Dim Objcon As New SqlConnection(strConn.Split("|")(0))
        Dim objCom As New SqlCommand("spRSPNominatedApseList_Abhishek", Objcon)
        objCom.Parameters.Add("@LoginID", Data.SqlDbType.Int).Value = LoginId
        objCom.Parameters.Add("@CycleID", Data.SqlDbType.Int).Value = CycleId
        objCom.CommandType = CommandType.StoredProcedure
        objCom.CommandTimeout = 0
        Try
            ' Objcon.AccessToken = strConn.Split("|")(1)
            Objcon.Open()
            drdr = objCom.ExecuteReader()

            Dim iCol As Integer = 0

            Dim counter As Integer = 1
            Dim strTable As String = ""
            Dim flag As Integer = 0
            Dim str As String = ""
            Dim AssmntID As Integer = 0

            strTable = strTable & "<div class='section-title'><h3 class='text-center'>KINDLY PROVIDE FEEDBACK FOR PARTICIPANTS IN THE FOLLOWING LIST</h3><div class='title-line-center'></div></div>"
            ' strTable = strTable & "<p>You will be required to complete the survey(s) assuming one of the following roles:</p><ul><li>Self</li><li>Senior</li><li>Direct Report</li><li>Peer</li><li>Other</li></ul><p>To begin, click on the name of the participant you want to rate. Each survey consists of five screens and should take about 10-15 minutes to complete.If you can’t finish a survey in one sitting, you can save your progress and exit.</p>"

            strTable = strTable & "<div><p></p></div>"
            strTable &= "<table class='table-style' id='tblPreNominated'>"
            '  strTable &= "<thead><tr><th width='5%'>S.No.</th><th width='30%' align='left'>Name</th><th width='20%' align='left'>Provide Feedback As</th><th width='15%' align='left'>Status</th></thead><tbody>"
            strTable &= "<thead><tr><th width='5%'>S.No.</th><th width='30%' align='left'>Name</th><th width='15%' align='left'>Status</th></thead><tbody>"
            While (drdr.Read)
                RSPId = drdr.Item("RSPID")
                RSPStatus = drdr.Item("RspStatus")

                strTable &= "<tr>"

                strTable &= "<td>" & counter & "</td>"

                If RSPStatus = 0 Or RSPStatus = 1 Then

                    strTable = strTable & "<td align='left' class='paraTd'><input type='hidden' id='hdnRspId' value=" & drdr.Item("RspId") & "><input type='hidden' id='hdnRspStatus' value=" & drdr.Item("RspStatus") & "><input type='hidden' id='hdnCycApseAssmntTypeMapID' value=" & drdr.Item("CycleApseApsrMapID") & "><input type='hidden' id='hdnCycleID' value=" & drdr.Item("CycleID") & "><input type='hidden' id='hdnAssmntTypeID' value=" & drdr.Item("AssmntTypeID") & "><input type='hidden' id='hdnLevelID' value=" & drdr.Item("LevelID") & ">"
                    Dim NodeNameForJS As String = Replace(drdr.Item("NodeName"), " ", "_")

                    If RSPStatus = 0 Then

                        strTable = strTable & "<a href='###' onclick=fnShowQuestion(this," & drdr.Item("RspId") & "," & drdr.Item("RspStatus") & "," & drdr.Item("CycleApseApsrMapID") & "," & drdr.Item("CycleID") & "," & drdr.Item("AssmntTypeID") & "," & drdr.Item("LevelID") & ")  sname='" & NodeNameForJS & "' ><b><span class='glyphicon glyphicon-hand-right'></span> &nbsp;" & drdr.Item("NodeName")
                    ElseIf RSPStatus = 1 Then
                        strTable = strTable & "<a href='###' onclick=fnShowQuestion(this," & drdr.Item("RspId") & "," & drdr.Item("RspStatus") & "," & drdr.Item("CycleApseApsrMapID") & "," & drdr.Item("CycleID") & "," & drdr.Item("AssmntTypeID") & "," & drdr.Item("LevelID") & ") sname='" & NodeNameForJS & "' ><b><span class='glyphicon glyphicon-hand-right'></span> &nbsp;" & drdr.Item("NodeName")
                    End If
                    strTable = strTable & "</b></a></td>" '</label>

                ElseIf RSPStatus = 2 Then
                    strTable = strTable & "<td width='6%'>" '<label class='label label-success'>
                    strTable = strTable & drdr.Item("NodeName")
                    strTable = strTable & "</td>" '</label>

                End If

                'strTable &= "<td align='left'>"
                'str = drdr.Item("Relationship")
                'strTable &= str & "</td>"


                If RSPStatus = 0 Then
                    strTable &= "<td align='left'><label class='Not-Start'>"
                    str = "Pending"
                ElseIf RSPStatus = 1 Then
                    strTable &= "<td align='left'><label class='Inprogress'>"
                    str = "In Progress"
                ElseIf RSPStatus = 2 Then
                    strTable &= "<td align='left'><label class='Completed'>"
                    str = "Completed"
                End If
                strTable &= str & "</label></td>"

                strTable &= "</tr>"

                'End If

                If (iCol Mod 2 = 0) Then
                    If (iCol > 0) Then
                        strTable = strTable & "</tr>"
                    End If

                    strTable = strTable & "<tr>"
                Else
                End If
                iCol += 1
                counter += 1

            End While

            strTable = strTable & "</tbody></table>"

            strReturn = "1@" & strTable.ToString
        Catch ex As Exception
            strReturn = "2@" & ex.Message
        Finally
            objCom.Dispose()
            Objcon.Close()
            Objcon.Dispose()
        End Try
        Return strReturn
    End Function

    <System.Web.Services.WebMethod(EnableSession:=True)>
    Public Function fnUpdateResponses(ByVal strSave As String, ByVal FinalSave As Integer, ByVal strComment As String, ByVal RspID As Integer) As String
        Dim drdr As SqlDataReader
        Dim Objcon As New SqlConnection(strConn.Split("|")(0))

        Dim objCom As New SqlCommand("[spUpdateResponses]", Objcon)

        objCom.Parameters.Add("@RespStr", SqlDbType.VarChar).Value = strSave
        objCom.Parameters.Add("@FinalSave", SqlDbType.Int).Value = FinalSave
        objCom.Parameters.Add("@LoginIdUpd", SqlDbType.Int).Value = Session("LoginID")

        objCom.CommandType = CommandType.StoredProcedure
        objCom.CommandTimeout = 0


        Dim drdr1 As SqlDataReader
        Dim Objcon1 As New SqlConnection(strConn.Split("|")(0))

        Dim objCom1 As New SqlCommand("[SPRspManageCmptncyCmmnts]", Objcon1)


        objCom1.Parameters.Add("@RspId", SqlDbType.VarChar).Value = RspID
        objCom1.Parameters.Add("@CmptncyID", SqlDbType.Int).Value = strComment.Split("@")(1)
        objCom1.Parameters.Add("@Cmmnts", SqlDbType.NVarChar).Value = strComment.Split("@")(0)

        objCom1.CommandType = CommandType.StoredProcedure
        objCom1.CommandTimeout = 0



        Dim strReturn As String = ""
        Try
            ' Objcon.AccessToken = strConn.Split("|")(1)
            Objcon.Open()
            drdr = objCom.ExecuteReader()

            ' Objcon1.AccessToken = strConn.Split("|")(1)
            Objcon1.Open()
            drdr1 = objCom1.ExecuteReader()

            While (drdr.Read)
                '            If (FinalSave = 3) Then
                strReturn = IIf(IsDBNull(drdr.Item("Completed")), "", drdr.Item("Completed")) & "^" & IIf(IsDBNull(drdr.Item("Incompleted")), "", drdr.Item("Incompleted")) & "^" & IIf(IsDBNull(drdr.Item("pending")), "", drdr.Item("pending")) & "^" & drdr.Item("PageNmbr")
                '           End If
            End While
            strReturn = "1@" & strReturn
        Catch ex As Exception
            strReturn = "2@" & ex.Message


        Finally
            objCom.Dispose()
            Objcon.Close()
            Objcon.Dispose()

            objCom1.Dispose()
            Objcon1.Close()
            Objcon1.Dispose()
        End Try

        Return strReturn

    End Function


    'CHANGE
    <System.Web.Services.WebMethod()>
    Public Function subPopulateQuestions(ByVal RspID As Integer, ByVal PGNmbr As Integer, ByVal levelID As Integer) As String
        Dim Objcon As New SqlConnection(strConn.Split("|")(0))
        Dim objCom As New SqlCommand("[spRSPManageDet]", Objcon)
        Dim drdr As SqlDataReader
        objCom.Parameters.Add("@RSPID", SqlDbType.Int).Value = RspID
        objCom.Parameters.Add("@PgNmbr", SqlDbType.Int).Value = PGNmbr
        objCom.Parameters.Add("@levelID", SqlDbType.Int).Value = levelID

        objCom.CommandType = CommandType.StoredProcedure
        objCom.CommandTimeout = 0



        Dim Objcon1 As New SqlConnection(strConn.Split("|")(0))
        Dim objCom1 As New SqlCommand("[spRSPGetCmptncyCmnts]", Objcon1)
        Dim drdr1 As SqlDataReader
        objCom1.Parameters.Add("@RSPID", SqlDbType.Int).Value = RspID

        objCom1.CommandType = CommandType.StoredProcedure
        objCom1.CommandTimeout = 0

        Dim strReturn As String
        Try
            'Objcon.AccessToken = strConn.Split("|")(1)
            Objcon.Open()
            drdr = objCom.ExecuteReader()

            ' Objcon1.AccessToken = strConn.Split("|")(1)
            Objcon1.Open()
            drdr1 = objCom1.ExecuteReader()

            Dim name As String = ""
            Dim strTable As String = ""
            Dim str As String = ""
            Dim cmtIndex As Integer
            Dim txtcomment As String = ""
            Dim CmptncyCmntCntr As Integer
            Dim cmptncyCmmnts As String = ""
            Dim cmptncyString As String = ""
            Dim cmptncyID As Integer
            Dim strTxtLastValue1 As String = ""


            cmptncyString = ""
            While drdr1.Read
                If cmptncyString = "" Then
                    cmptncyString &= Replace(drdr1.Item(0) & "^" & IIf(IsDBNull(drdr1.Item(1)), "", drdr1.Item(1)), "&quot;", "'")
                Else
                    cmptncyString &= "|" & Replace(drdr1.Item(0) & "^" & IIf(IsDBNull(drdr1.Item(1)), "", drdr1.Item(1)), "&quot;", "'")
                End If
            End While
            'objADO.CloseConnection(Objcon1, objCom1, drdr1)


            Dim indx As Integer = 1
            '*************** previous class for strColorTable is clsVerySmall changed by Madan on 2-3-2005*******************
            Dim strColorTable As String = "<table align='center' width='100%' border='0' cellspacing='0' cellpadding='0'><tr>"
            Dim strOldValueOfName As String = ""
            Dim strTxtLastValue As String = ""
            Dim OldId As Integer = 0
            Dim OldIdForHeading As Integer = 0
            Dim CompValue As Integer
            Dim CompValue_SubCompentency As Integer
            Dim OldName As String = ""

            Dim strTxtLastValue_SubCompentency As String = ""
            Dim OldId_SubCompentency As Integer = 0
            Dim OldName_SubCompentency As String = ""

            While (drdr.Read)

                If (drdr.Item(7) <> OldId) Then
                    strTxtLastValue1 &= IIf(IsDBNull(drdr.Item(7)), "", drdr.Item(7))
                    OldId = drdr.Item(7)
                End If


                If (drdr.Item(6) <> OldName) Then

                    '''''''''Add CommnetBox After each Competency
                    ' If hdnPageNum.Value <> "6" Then

                    ''''''''''''''''''END''' 
                    CompValue = drdr.Item("CompNodeId")
                    strTable &= "<div style='background: #000000; color: #ffffff; font-size: 1rem ;font-weight: 700; text-transform: none; margin-left: 0; margin-right: -15px; margin-bottom: .25rem; padding: 0 25px ; width:100%'>"
                    strTable &= drdr.Item(6)
                    strTable &= "</div>"
                    OldName = drdr.Item(6)

                    'OldName = drdr.Item(7)
                    'chkValueForTxtBox = 1

                End If

                'strTable = strTable & "</tr>"
                'strTable = strTable & "<tr>"
                If (drdr.Item(8) <> OldId_SubCompentency) Then
                    strTxtLastValue_SubCompentency &= IIf(IsDBNull(drdr.Item(8)), "", drdr.Item(8))
                    OldId_SubCompentency = drdr.Item(8)
                End If

                If (drdr.Item(9) <> OldName_SubCompentency) Then

                    '''''''''Add CommnetBox After each Competency
                    ' If hdnPageNum.Value <> "6" Then

                    ''''''''''''''''''END'''

                    CompValue_SubCompentency = drdr.Item("SubCompID")
                    'strTable &= "<div style='color:#000;font-size: .75rem !important;font-weight: 700;text-transform: uppercase;'>"
                    'strTable &= drdr.Item(9)
                    'strTable &= "</div>"
                    OldName_SubCompentency = drdr.Item(9)

                End If

                strTable = strTable & "<div class='ques-row row g-0'>"
                '  strTable = strTable & "<div class='ques-row'>"
                strTable = strTable & "<div Class='col-md-6'>"
                strTable = strTable & "<table class='Questable'>"
                strTable = strTable & "<tr>"
                strTable = strTable & "<td width='6%'><b>"
                strTable = strTable & drdr.Item("SrlNmbr")
                strTable = strTable & "</b></td>"
                strTable = strTable & "<td id='tdResponse' & indx & '><input type='hidden' id=hdnQnsId' & indx & ' value='" & drdr.Item("RspDetId") & "'>"
                strTable = strTable & drdr.Item("Descr")
                strTable = strTable & "</td>"
                strTable = strTable & "</tr>"
                strTable = strTable & "</table></div>"
                strTable = strTable & "<div class='col-md-6 valign-middile'>"
                strTable = strTable & "<table class='Questable' iden='tblMain'>"
                strTable = strTable & "<tr>"
                Dim i As Integer = 1
                Dim rdovalue As String = ""


                For i = 1 To 5
                    'For i = 5 To 0 Step -1
                    rdovalue = drdr.Item("RspDetId") & "^" & i
                    strTable = strTable & " <td class='text-center' style='width:19.28%; text-align:center;'>"

                    If (drdr.Item("Answ") = i) Then
                        ' Checked radio button (ensure it has the correct checked style)
                        strTable = strTable & "<input type='radio' id='rdo" & i & "' name='rdoQns" & drdr.Item("RspDetId") & "' value='" & rdovalue & "' checked class='styled-radio'>"
                    Else
                        ' Unchecked radio button
                        strTable = strTable & "<input type='radio' id='rdo" & i & "' name='rdoQns" & drdr.Item("RspDetId") & "' value='" & rdovalue & "' class='styled-radio'>"
                    End If

                    strTable = strTable & "</td>"
                Next

                strTable = strTable & "</tr>"
                strTable = strTable & "</table>"


                strTable = strTable & "</div></div>"


                'strColorTable = strColorTable & "<td id='tdColor" & indx & "' ><img src='../Styles/Images/trans.gif' height='12px'></td>"

                indx = indx + 1
                strTxtLastValue = IIf(IsDBNull(drdr.Item(7)), "", drdr.Item(7))
            End While

            If PGNmbr <> 9 Then
                strTable = strTable & "<table class='w-100 mt-2'>"
                '''''''TExtBox Afet Each Competency
                strTable = strTable & "<tr>"
                strTable = strTable & "<td class='text-left'><b><i>" & "Provide comments based on the given competency:   " & OldName & "</i> (optional)</b></td>"
                strTable = strTable & "</tr>"

                Dim arr1() As String
                arr1 = cmptncyString.Split("|")
                Dim kk1 As Integer
                'strTxtLastValue1 &= IIf(IsDBNull(drdr.Item(7)), "", drdr.Item(7)) & "|"
                For kk1 = 0 To arr1.Length - 1
                    If (strTxtLastValue = arr1(kk1).Split("^")(0)) Then
                        txtcomment = arr1(kk1).Split("^")(1)
                    End If
                Next

                strTable = strTable & "<tr>"
                If (txtcomment = "") Then ' Eng
                    strTable = strTable & "<td class='text-center' id='tdCom" & cmtIndex & "'><textarea class='form-control wm-50' id='txtCompCmt1" & cmtIndex & "' rows='3'  onkeypress='return IsAlphaNumeric(event);' onkeyup='fnAdjustRows(this)' onmousedown=fnClearTextbox('txtCompCmt1" & cmtIndex & "') ></textarea><span id='error' style='color: Red; display: none'>* Some Special Characters not allowed.</span></td>" '<div style='width:50%; margin: 0 auto;'></div>
                Else
                    strTable = strTable & "<td class='text-center' id='tdCom" & cmtIndex & "'><textarea class='form-control wm-50' id='txtCompCmt1" & cmtIndex & "' rows='3' onkeypress='return IsAlphaNumeric(event);' onkeyup='fnAdjustRows(this)' onmousedown=fnClearTextbox('txtCompCmt1" & cmtIndex & "') >" & txtcomment & "</textarea><span id='error' style='color: Red; display: none'>* Some Special Characters  not allowed.</span></td> "
                End If
                'CmptncyCmntCntr = CmptncyCmntCntr + 1
                'cmtIndex = cmtIndex + 1
                strTable = strTable & "</tr>"


                strTable = strTable & "<tr>"
                If (txtcomment = "") Then ' Eng
                    strTable = strTable & "<td class='text-center' id='tdCom" & cmtIndex & "'><textarea class='form-control wm-50' id='txtCompCmt2" & cmtIndex & "' rows='3'  onkeypress='return IsAlphaNumeric(event);' onkeyup='fnAdjustRows(this)' onmousedown=fnClearTextbox('txtCompCmt1" & cmtIndex & "') ></textarea><span id='error' style='color: Red; display: none'>* Some Special Characters not allowed.</span></td>" '<div style='width:50%; margin: 0 auto;'></div>
                Else
                    strTable = strTable & "<td class='text-center' id='tdCom" & cmtIndex & "'><textarea class='form-control wm-50' id='txtCompCmt2" & cmtIndex & "' rows='3' onkeypress='return IsAlphaNumeric(event);' onkeyup='fnAdjustRows(this)' onmousedown=fnClearTextbox('txtCompCmt1" & cmtIndex & "') >" & txtcomment & "</textarea><span id='error' style='color: Red; display: none'>* Some Special Characters  not allowed.</span></td> "
                End If
                strTable = strTable & "</tr>"

                strTable = strTable & "<tr>"
                If (txtcomment = "") Then ' Eng
                    strTable = strTable & "<td class='text-center' id='tdCom" & cmtIndex & "'><textarea class='form-control wm-50' id='txtCompCmt3" & cmtIndex & "' rows='3'  onkeypress='return IsAlphaNumeric(event);' onkeyup='fnAdjustRows(this)' onmousedown=fnClearTextbox('txtCompCmt2" & cmtIndex & "') ></textarea><span id='error' style='color: Red; display: none'>* Some Special Characters not allowed.</span></td>" '<div style='width:50%; margin: 0 auto;'></div>
                Else
                    strTable = strTable & "<td class='text-center' id='tdCom" & cmtIndex & "'><textarea class='form-control wm-50' id='txtCompCmt3" & cmtIndex & "' rows='3' onkeypress='return IsAlphaNumeric(event);' onkeyup='fnAdjustRows(this)' onmousedown=fnClearTextbox('txtCompCmt2" & cmtIndex & "') >" & txtcomment & "</textarea><span id='error' style='color: Red; display: none'>* Some Special Characters  not allowed.</span></td> "
                End If
                strTable = strTable & "</tr>"


                CmptncyCmntCntr = CmptncyCmntCntr + 1
                cmtIndex = cmtIndex + 1

                strTable = strTable & "</table>"
                ''''''''''END
            End If

            strReturn = "1@" & strTable & "@" & strTxtLastValue1
        Catch ex As Exception
            strReturn = "2@" & ex.Message
        Finally
            objCom.Dispose()
            Objcon.Close()
            Objcon.Dispose()

            objCom1.Dispose()
            Objcon1.Close()
            Objcon1.Dispose()
        End Try
        Return strReturn
    End Function

    <WebMethod(EnableSession:=True)>
    Function fnRspInsertRspComments(ByVal RSPId As Integer, ByVal Comment1 As String, ByVal Comment2 As String, ByVal Comment3 As String, ByVal Comment4 As String, ByVal FinalSave As Integer) As String


        Dim Objcon As New SqlConnection(strConn.Split("|")(0))
        Dim objCom As New SqlCommand("[SPRspInsertRspComments]", Objcon)
        Dim drdr As SqlDataReader
        If Comment1 = "" Then
            Comment1 = "NA"
        End If
        If Comment2 = "" Then
            Comment2 = "NA"
        End If
        If Comment3 = "" Then
            Comment3 = "NA"
        End If

        If Comment4 = "" Then
            Comment4 = "NA"
        End If
        objCom.Parameters.Add("@RspId", SqlDbType.Int).Value = RSPId
        objCom.Parameters.Add("@Comment1", SqlDbType.VarChar).Value = ReplaceQuotes(Comment1)
        objCom.Parameters.Add("@Comment2", SqlDbType.VarChar).Value = ReplaceQuotes(Comment2)
        objCom.Parameters.Add("@Comment3", SqlDbType.VarChar).Value = ReplaceQuotes(Comment3)
        objCom.Parameters.Add("@Comment4", SqlDbType.VarChar).Value = ReplaceQuotes(Comment4)
        objCom.Parameters.Add("@FinalSave", SqlDbType.Int).Value = FinalSave
        objCom.Parameters.Add("@LoginIdUpd", SqlDbType.Int).Value = Session("LoginID")
        objCom.Parameters.Add("@CycleID", SqlDbType.Int).Value = Session("CycleID")

        objCom.CommandType = CommandType.StoredProcedure
        objCom.CommandTimeout = 0

        Dim strRet As String = ""
        Try
            ' Objcon.AccessToken = strConn.Split("|")(1)
            Objcon.Open()
            drdr = objCom.ExecuteReader()
            drdr.Read()
            strRet = drdr.Item("Completed") & "^" & drdr.Item("Incompleted") & "^" & drdr.Item("Pending") & "^" & drdr.Item("PageNmbr")

            strRet = "1@" & strRet
        Catch ex As Exception
            strRet = "2@" & strRet
        Finally
            objCom.Dispose()
            Objcon.Close()
            Objcon.Dispose()
        End Try
        Return strRet
    End Function

    <WebMethod()>
    Public Function fnGetRspComments(ByVal RSPId As Integer) As String
        Dim Objcon As New SqlConnection(strConn.Split("|")(0))
        Dim objCom As New SqlCommand("[SPGetRspComments]", Objcon)
        Dim drdr As SqlDataReader
        objCom.Parameters.Add("@RSPID", SqlDbType.Int).Value = RSPId
        objCom.CommandType = CommandType.StoredProcedure
        objCom.CommandTimeout = 0

        Dim strRet As String = ""


        Try
            'Objcon.AccessToken = strConn.Split("|")(1)
            Objcon.Open()
            drdr = objCom.ExecuteReader()
            drdr.Read()
            strRet = drdr.Item(0) & "^" & drdr.Item(1) & "^" & drdr.Item(2) & "^" & drdr.Item(3) & "^"
            strRet = "1@" & strRet

        Catch ex As Exception
            strRet = "2@" & strRet
        Finally
            objCom.Dispose()
            Objcon.Close()
            Objcon.Dispose()
        End Try
        Return strRet
    End Function

    <WebMethod(EnableSession:=True)>
    Function ReplaceQuotes(ByVal strtxt As String) As String
        Return Replace(strtxt, "'", "''")
    End Function
End Class