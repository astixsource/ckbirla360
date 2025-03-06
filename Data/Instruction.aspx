<%@ Page Title="" Language="VB" MasterPageFile="~/Data/Site.master" AutoEventWireup="true" CodeFile="Instruction.aspx.vb" Inherits="_Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/instructionJS.js"></script>


</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-title">
        <%--<h3 class="text-center">360 Degree <span>Feedback Survey</span></h3>--%>
        <h3 class="text-center">CK BIRLA GROUP <span>360 ASSESSMENT SURVEY</span></h3>
        <div class="title-line-center"></div>
    </div>

    <div class="row g-0">
        <div class="col-md-5">
            <div class="grid-welcome-bg">
                <img src="../Images/Instruction2.jpg" class="info-bg" />
            </div>
        </div>
        <div class="col-md-7">
            <div class="grid bg_two">
                <p>Welcome to the Survey!</p>
                <p>This 360-degree survey is designed to support your professional development by providing you with comprehensive feedback on the CK Birla Group Leadership Competencies.</p>
                <p>The survey helps you become aware of your strengths and development areas, as perceived by yourself and by others – i.e., colleagues and stakeholders with whom you interact.</p>

                <p>How It Works:</p>
                <ul>
                    <li>Click on <strong>Your Name</strong> under the evaluations section to start the survey.</li>
                    <li>You will be presented with a series of leadership behaviours to rate.</li>
                    <li>Rate each behaviour on a 4-point scale, ranging from "1 = Is always like this" to "4 = Is rarely/never like this." If you haven't observed the behaviour, simply select the "0 = I can&rsquo;t assess this" option.</li>
                </ul>
                <p><strong>Your responses will remain confidential</strong>, and the results will be used to help you focus on improvement strategies for the future. Thank you for your participation in this important process!</p>
                
            </div>
        </div>
    </div>
    <h5 class="small-title"><strong>The Rating Scale used for the 360-feedback survey is as below:</strong></h5>
    <table class="table-style mt-3">
        <thead>
            <tr>
                <th>Rating</th>
                <th>Interpretation</th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            

            <tr>
                <td>
                    <strong>1</strong>
                </td>
                <td>
                    <strong>Is rarely/ never like this</strong>
                </td>
                <td>Individual rarely demonstrates the competency and there is a need for significant improvement on their part 
                </td>
            </tr>
            <tr>
                <td>
                    <strong>2</strong>
                </td>
                <td>
                    <strong>Is sometimes like this</strong>
                </td>
                <td>Individual demonstrates the competency in some instances but not in others, and there is room for improvement. 

                </td>
            </tr>
            <tr>
                <td>
                    <strong>3</strong>
                </td>
                <td>
                    <strong>Is often like this</strong>
                </td>
                <td>Individual demonstrates the competency most of the time, and it is a reliable aspect of their performance. 
                </td>
            </tr>
            <tr>
                <td>
                    <strong>4</strong>
                </td>
                <td>
                    <strong>Is always like this</strong>
                </td>
                <td>Individual demonstrates the competency consistently in all instances and are a role model for the behaviour
                </td>
            </tr>

            <tr>
                <td>
                    <strong>0</strong>
                </td>
                <td>
                    <strong>I can’t assess this</strong>
                </td>
                <td>I do not know to what degree the individual displays this competency
                </td>
            </tr>
        </tbody>
    </table>
     <p>For any queries, please contact  <a href="mailto:a.sharma@ckbirlagroup.com">a.sharma@ckbirlagroup.com</a> or <a href="mailto:apkirti@deloitte.com.">apkirti@deloitte.com</a> .</p>
               
    
    <div class="text-center">
        <%--<asp:Button ID="btnBack" runat="server" Text="Back"  CssClass="btns btn-submit" />--%>
        <asp:Button ID="btnContiue" runat="server" Text="Start Survey" CssClass="btns btn-submit" />
    </div>
</asp:Content>
