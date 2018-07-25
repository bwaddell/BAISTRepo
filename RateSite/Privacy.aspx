<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Privacy.aspx.cs" Inherits="Privacy" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">

    <h2>Privacy Notice</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    ContinUI is committed to protecting your privacy. On this page we set out why we collect information, what we do with information, and what your rights are. Personal information or data is any information that can be used to identify you as an individual.
                </p>
            </div>
        </div>
    </div>

    <h2>Your Rights and Preferences</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    The legal basis for processing your personal data is your consent, where you have provided this, and otherwise our legitimate interests. In the case of our legitimate interests to process your personal data, we are required to ensure that our interests are balanced against any detriment you may face from our processing.
You have a right to know what information ContinUI holds about you and to ask us to stop processing your data, provided that the ContinUI has no legitimate or legal reason to continue using it.
Under GDPR, you have the right to:<br />
                    &nbsp;&nbsp;•	opt out of specified or all communication with ContinUI<br />
                    &nbsp;&nbsp;•	ask for access to, or correction/deletion, of your data<br />
                    &nbsp;&nbsp;•	have data processing restricted to a particular use<br />
                    Requests to obtain confirmation that your data are being processed in accordance with the regulations, or to request access to your personal data, must be made in writing (via email) and should include:
                    <br />
                    &nbsp;&nbsp;•	your full name, address and contact telephone number<br />
                    &nbsp;&nbsp;•	details of the specific information you require and any relevant dates.<br />
                    The CPS will respond within 30 calendar days of receipt of the request. You can submit requests via email to info@ContinUI.uk.
                </p>
            </div>
        </div>
    </div>

    <h2>Visitors to and Users of our Website</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    The CPS website collects data from website visitors for specific purposes that relate to legitimate interests concerning ContinUI core functions.
                </p>
                <h4>Creators (those creating and inviting others to respond to events)
                </h4>
                <p>
                    ContinUI collects user your account information to deliver its services. Optional demographic information is used to tailor our services to the markets using it. When creating events, you as the the Creator have sole responsibility over what questions you ask of and what data are collected from your Respondents. This includes how long you choose to store the data (see Data Retention, below), with whom you share them, and ensuring your respondents’ rights to their data following. 

                </p>
                <h4>Respondents (those joining and responding to events)
                </h4>


                <p>
                    Responses to ContinUI events are controlled and managed by the Creator (the person or group who invited you to join the event). In those instances, ContinUI is only storing and processing those responses on behalf of the Creator. The Creator is responsible for how the data are stored, exported, and shared.
                </p>
                <h4>Data Retention</h4>
                <p>
                    If you hold an account with ContinUI we do not delete the data in your account. You are responsible for how long the data are stored. There are controls in your account where you can delete data at the account level (all data in your account), the event level (all of the data in an event) and at the response level (data from individual respondents). If you are a Respondent, you will need to ask the Creator how long your responses will be stored with ContinUI.
                </p>
            </div>
        </div>
    </div>


    <h2>Cookies</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    A Cookie is a small collection of data sent from a website and stored in your browser.  
                    ContinUI uses Cookies to perform functions such as keeping you logged in for later visits and for facilitating the connection of evaluators to open events.  
                    Most browsers will allow you to decline the use of these Cookies, however some functionality of this website may not work properly.
                </p>
            </div>
        </div>
    </div>

    <h4>Delete Site Cookies</h4>
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    Use this if you wish to revoke consent for the use of cookies by this website.
                    All cookies will be deleted and you will be logged off of your account.
                </p>
            </div>
        </div>
    </div>
    <asp:Button ID="delCook" runat="server" Text="Delete Cookies" OnClick="delCook_Click" />

    <h2>Analytics</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    This site uses Google Analytics to analyze activity and trends for how ContinUI is used.  
                    You can read more on Google's practices and their use of Cookies <a href="https://policies.google.com/technologies/partner-sites">here</a>.
                </p>
            </div>
        </div>
    </div>

    <h2>Links to other websites</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    The ContinUI website contains links to other websites. We are not responsible for the privacy practices of other sites. We encourage our visitors to be aware when they leave our website and to read the privacy arrangements of other sites that collect or use personal data.
                </p>
            </div>
        </div>
    </div>

    <h2>Contact</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    For more information concerning the way in which your data are managed, please contact info@ContinUI.uk.
                </p>
            </div>
        </div>
    </div>

    <%-- 

    Copy the block between the dashes to create a new paragraph
    --------------------------------
    <h2>insert title</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>
                    insert text
                </p>
            </div>
        </div>
    </div> 
    ----------------------------
    --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

