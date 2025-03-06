<%@ Page Language="VB" MasterPageFile="~/Data/Site.master" AutoEventWireup="false" CodeFile="frmQuestionMain.aspx.vb" Inherits="Questionaire_frmQuestionMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/Question_8.js"></script>

    <style type="text/css">
        input[type="radio"].styled-radio {
            position: relative;
            width: 20px;
            height: 20px;
            border-radius: 50%;
            cursor: pointer;
            -webkit-appearance: none; /* Remove default radio button appearance */
            -moz-appearance: none;
            appearance: none;
            margin: 0;
        }

            /* Create the styled appearance for radio button (using pseudo-element) */
            input[type="radio"].styled-radio::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                border-radius: 50%;
                background-color: #d1d1cf; /* Default gray color for unchecked state */
                border: 2px solid #ccc;
                transition: background-color 0.3s ease, border-color 0.3s ease;
            }

            /* Alternate color for unchecked radio buttons */
            input[type="radio"].styled-radio:nth-of-type(odd)::before {
                background-color: #d1d1cf; /* Light gray */
            }

            input[type="radio"].styled-radio:nth-of-type(even)::before {
                background-color: #e5e57d; /* Yellowish */
            }

            /* Styling for checked radio button */
            input[type="radio"].styled-radio:checked::before {
                background-color: #e5e57d; /* Yellow for checked state */
                border-color: #b3b300; /* Darker yellow for checked border */
            }

            /* Optional: Hover effect for better interaction */
            input[type="radio"].styled-radio:hover::before {
                background-color: #cccc00; /* Light yellow when hovered */
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <!--Ques Heading-->
    <div class="section-title">
        <h3 class="text-center">360 DEGREE <span>FEEDBACK SURVEY</span></h3>
        <div class="title-line-center"></div>
    </div>
    <p class="text-center mb-2" id="h6">
        Please respond to each statement using scale provided, ranging from "Is never/rarely like this" to “Is always like this".<br />
        You may click "Save & Continue" to save your responses and return later to complete the rest of the survey.
    </p>


    <!--Ques top header-->
    <div id="tblTopHeader">
        <!-- Proper Table Header -->
        <div id="h7">
            <table class="Questable" style="background: #8CC63F; color: #fff">
                <thead>
                    <tr>
                        <!-- Header for Statements -->
                        <th>Statements</th>
                        <!-- Header for Rating Scale -->
                        <th>Rating Scale</th>
                    </tr>
                </thead>
            </table>
        </div>

        <!-- Original Table Content -->
        <div class="row">
            <div class="col-md-6 align-bottom">
                <table class="Questable">
                    <tr>
                       <%-- <th id="tdPageNum1">
                            <br>
                        </th>--%>
                        <th id="dvName">
                            <br>
                        </th>
                        <th width="20%" id="dvRespColor">&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
                </table>
            </div>

            <div class="col-md-6" id="dvQuestions">
                <table class="Questable">
                    <thead>
                        <tr>
                            <!-- Rating Scale Values -->
                            
                            <th style="width: 19.28%; text-align: center; padding: 5px;"><span id="Label1"><strong>01</strong><br />Is never/rarely <br />like this</span></th>
                            <th style="width: 19.28%; text-align: center; padding: 5px;"><span id="Label2"><strong>02</strong><br />Is sometimes <br />like this</span></th>
                            <th style="width: 19.28%; text-align: center; padding: 5px;"><span id="Label3"><strong>03</strong><br />Is often <br />like this</span></th>
                            <th style="width: 19.28%; text-align: center; padding: 5px;"><span id="Label4"><strong>04</strong><br />Is always <br />like this</span></th>
                            <th style="width: 19.28%; text-align: center; padding: 5px;"><span id="Label0"><strong>NA</strong><br />I can’t assess <br />this behaviour</span></th>
                            
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
    <!--Ques top header end-->




    <!--Ques Section end-->
    <div id="dvSurvey" class="w-100"></div>

    <!--divComments Section end-->
    <div class="ques-row w-100 mt-3" id="divComments" style="display: none; overflow-x: hidden; overflow-y: hidden;">

        <div class="row mb-2">
            <div class="col-md-6 align-right-middile" id="tdCmmnt1" runat="server"><strong>What are this individual’s key strengths that you have observed at work?</strong></div>
            <div class="col-md-6">
                <asp:TextBox onkeypress="fnCheckLength(this);" ID="Textbox1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-md-6 align-right-middile" id="tdCmmnt2" runat="server"><strong>What specific areas of improvement would help this individual become more effective and successful in their role?</strong></div>
            <div class="col-md-6">
                <asp:TextBox onkeypress="fnCheckLength(this);" ID="Textbox2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-md-6 align-right-middile" id="tdCmmnt3" runat="server"><strong>Any other recommendation/comments for the individual.</strong></div>
            <div class="col-md-6">
                <asp:TextBox onkeypress="fnCheckLength(this);" ID="Textbox3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
        </div>
    </div>
       <div style="text-align: right" id="tdPageNum1">
    </div>
    <div class="row mt-3 mb-3">
        <div class="col-md-11">
            <div class="text-center">

                <div class="button-group">
                    <input type="button" class="btns btn-danger" id="btnSaveExit" onclick="fnSaveExit()" value="Exit">
                    <input type="button" class="btns btn-previous" id="btnPrevious" onclick="fnPrevious()" value="Previous" style="display: none;">
                    <input type="button" class="btns btn-submit" id="btnNext" onclick="fnNext()" value="Save &amp; Continue" style="display: inline-block;">
                    <input type="button" class="btns btn-submit" id="btnSubmit" onclick="FinalSubmit()" value="Submit" style="display: none;">
                </div>

            </div>
        </div>
    </div>

    <div id="dvinstruction" style="display: none;">
        <div class="section-title">
            <h3 class="text-center">WELCOME TO WMC 360 FEEDBACK SURVEY!</h3>
            <div class="title-line-center"></div>
        </div>
        <ol type="1">
            <li>You are required to respond on the given Statements using the 4-Point scale, where ‘1’ stands for ‘Strongly Disagree’ and ‘4’ indicates ‘Strongly Agree</li>
            <li>While responding, please recollect your experience of working with the individual during the recent few years. </li>
            <li>All statements are mandatory</li>
            <li>You may ‘Save’ your responses and ‘Exit’ to come back later and complete the rest of the Survey.</li>
        </ol>

        <p>The survey will remain open till <strong>Monday, 8<sup>th</sup> Nov 2021 5 P.M.</strong> We request you to adhere to the timelines to ensure that the process is completed successfully on time.</p>
        <p>Please click on the <strong>Provide Feedback</strong> tab on the <strong>Menu</strong> to begin the feedback process</p>
        <p>We look forward to your whole-hearted participation in this developmental initiative.</p>
        <p>
            For any query or clarification please reach out to –
            <br />
            <strong>Shubha Ranjan Nandi</strong> at <a href="mailto:shubho.nandi@gmail.com">shubho.nandi@gmail.com</a> +91 6289066933 OR </br> <strong>Arundhati Bagchi</strong> at <a href="mailto:arundhatibagchi2016@gmail.com">arundhatibagchi2016@gmail.com</a> +91 9051344191
        </p>
    </div>

    <input type="hidden" id="hdnRspID" runat="server" />
    <input type="hidden" id="hdnPGNmbr" runat="server" />
    <input type="hidden" id="hdnMaxPGNmbr" runat="server" />
    <input type="hidden" id="hdnName" runat="server" />
    <input type="hidden" id="hdnLevelID" runat="server" />
    <input type="hidden" id="hdnCmptncyCmntCntr" runat="server" name="hdnCmptncyCmntCntr">
    <input type="hidden" id="hdnCmptncyCmnt" runat="server" name="hdnCmptncyCmnt">
    <input type="hidden" id="hdnCmptncyCmntValue" runat="server" name="hdnCmptncyCmntValue">
    <input type="hidden" id="hdnCmptncyID" runat="server" name="hdnCmptncyID">
</asp:Content>
