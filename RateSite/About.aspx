<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="About.aspx.cs" Inherits="About" Theme="ContinUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">




    <h2>About</h2>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>This website has been designed and implemented by Benjamin Waddell, Cody Jacob, and Martin Sawicki as part of the 
                   <a href="http://www.nait.ca/program_home_78547.htm">Bachelor of Applied Information Systems Technology</a> capstone 
                   program at the <a href="http://www.nait.ca/">Northern Alberta Institute of Technology</a>, on behalf of the 
                   <a href="http://performancescience.ac.uk/">Center for Performance Science</a>. Its purpose is to allow an audience 
                   to provide live feedback during an event, which can range from dramatic acting to product demonstrations.</p>
                <p>Below you will find guides demonstrating the operation of the various services this website provides. If you would 
                   like to provide feedback or report a issue, please send an email to: -insert CPS IT department email here-</p>
            </div>
        </div>
    </div>

    <h2>Joining an Event</h2>

     <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>To join an event, obtain the event key from the event facilitator and input it into the text box on the homepage. 
                   Click the join event button, and if the event key is correct, you will enter the event room. If you entered the
                   event key incorrectly, an error message will appear.  
                </p>
            </div>
        </div>
    </div>

    <h2>Evaluating an Event</h2>

     <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>In the event room you will see two large arrows, one blue, the other red, as well as a number in the center of the
                   screen, your current rating. Tapping the blue arrow will increase your rating by one, and tapping the red arrow will
                   decrease it by one. If the event has not begun or has ended, tapping one of the arrows will display a message to
                   inform you of the events status.</p>
                <p>Currently, if you close the event page you will be unable to continue where you left off, and rejoining will create 
                   a new audience member with a new set of ratings. Be sure to only close the page if you are done evaluating the event.
                </p>
            </div>
        </div>
    </div>

    <h2>Creating an Account</h2>

     <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>If you wish to host your own events and gain live feedback for them, you must create a facilitator account. This can 
                   be done by clicking Create Account in the top right corner of the page, beside the Login button. You will be redirected
                   to the Create Account page, where you will need to fill in the required fields, such as your email and a password. If
                   your account was made successfully, you will be redirected to the homepage, with two new options in the top left of
                   the page, View Account and Events.
                </p>
                <p>If there were any errors during account creation, the page will notify you.
                </p>
            </div>
        </div>
    </div>

    <h2>Viewing your Account</h2>

     <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>If you wish to update your account information or your password, click on View Account in the top left corner of the page.
                   Here you can modify the contents of your account, or password, as you please. If any errors or empty fields exist when
                   you attempt to change your information, the page will notify you and not proceed with the update.
                </p>
            </div>
        </div>
    </div>

    <h2>Creating an Event</h2>

     <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p>To create an event for evaluation, click the Create New Event button on the home page.</p>
            </div>
        </div>
    </div>

    <h2>Viewing an Event</h2>

     <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <p></p>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>

