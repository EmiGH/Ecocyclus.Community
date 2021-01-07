<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebCorporative.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- Marcado de microdatos añadido por el Asistente para el marcado de datos estructurados de Google. -->
<html xmlns="http://www.w3.org/1999/xhtml">
<html lang="en">
<head id="Head1" runat="server">
    <title>CSI :: Construction Site Impacts | Environmental Management Platform</title>      
    <meta name="description" content="Construction Site Impacts - Environmental Management Platform, web application to monitor environmental site impacts from construction activities."/>
    <meta name="keywords" content="construction site impacts, site impacts, software, web application, CSI, CSH, BREEAM, WRAP, DEFRA, MAN 03"/>
    <meta name="robots" content="index, follow" />    
    <meta name="language" content="EN"/>
    <meta name="google-site-verification" content="CrnlRYxNHW-fGHZ3g4xxbAy82Epwn4ihN7SqVHwHlWE" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:200,300,400,600,700,900,200italic,300italic,400italic,600italic,700italic,900italic'
        rel='stylesheet' type='text/css'/>
    <link rel='stylesheet' type='text/css' href='css/style.css' />
    <link rel="Stylesheet" href="css/jquery-ui.css" />    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/jquery-ui.min.js"></script>
    <script type='text/javascript' src='js/app.js'></script>
    <script type="text/javascript">

        function Submit() {

            var _email = $('#<%=txtEmail.ClientID%>').val();
            var _name = $('#<%=txtName.ClientID%>').val();
            var _organization = $('#<%=txtOrganization.ClientID%>').val();
            var _phone = $('#<%=txtPhone.ClientID%>').val();
            var _message = $('#<%=txtMessage.ClientID%>').val();

            $('#divAjaxGif').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Default.aspx/Contact",
                data: "{'from':'" + _email + "', 'name':'" + _name + "','organisation':'" + _organization + "','telephone':'" + _phone + "','message':'" + _message + "'}",
                dataType: "json",
                success: function (data) {
                    $('#divAjaxGif').hide();
                    showMessage(data.d);
                },
                error: function (req, status, error) {
                    $('#divAjaxGif').hide();
                    alert(error);
                }
            });

        }


        function showMessage(content) {
            $("#divMessage").html(content).dialog({
                autoOpen: true,
                modal: true,
                title: 'asas',
                draggable: true,
                resizable: false,
                close: function (event, ui) { $(this).dialog("destroy"); },
                buttons: {
                    'Ok': function () {
                        $(this).dialog("destroy");
                    }
                },
                overlay: {
                    opacity: 0.45,
                    background: "black"
                }
            });
        }

    </script>
</head>
<body>
    <!--******************************************************-->
    <!--***************** Nav Section ************************-->
    <!--******************************************************-->
    <div id="divNav">
        <div class="wrap">
            <div class="logo-nav">
            </div>
            <h1 style="color:#3B323A;">Construction Site Impacts</h1>
            <ul>                             
                <li>
                    <div id="aWhatWeDo">
                        What We Do</div>
                </li>
                <li>
                    <div id="aOurPrices">
                        Our Prices</div>
                </li>
                <li>
                    <div id="aContact">
                        Contact</div>
                </li>
                <li><a href="support.htm" target="_blank">Support</a> </li>
            </ul>
        </div>
    </div>
    <!--******************************************************-->
    <!--***************** Header Section *********************-->
    <!--******************************************************-->
    <span itemscope itemtype="http://schema.org/SoftwareApplication"><div id="div1"> <div id="divHeader">
        <div class="sombra-banner">
            <div>
            </div>
        </div>
        <div class="wrap">
            <a href="http://www.siteimpacts.com" class="logo"></a>
            <meta itemprop="url" content="http://www.siteimpacts.com"><div id="divAccess">
                <a href="http://app.siteimpacts.com/" target="new" class="singnup">FREE SIGN UP</a> 
                <a href="http://app.siteimpacts.com/" target="new" class="login">LOG IN</a> 
                <a href="screenshots.htm" id="aDemo" class="demo" target="_blank">TAKE A TOUR</a>
            </div>
            <div class="banner">
            <meta itemprop="screenshot" content="http://www.siteimpacts.com/img/img_header.png">
            </div>
        </div>
    </div>
    <!--******************************************************-->
    <!--***************** Home Section ***********************-->
    <!--******************************************************-->
    <div id="divHome">
        <div class="wrap">
            <div class="center">
                <div class="ArrowTop oscurra home">
                </div>
            </div>
            <div class="TextHome">
                Take control of the environmental impacts of your site.<br />
                Monitor, record and manage electricity, water, fuel and commercial transport.<br />
                Take a step in the right direction with ease.
            </div>
            <div class="conteiner">
                <div class="box">
                    <img src="img/img_collect.png" alt="site impacts", "data collection"/>
                    <div>
                        COLLECT</div>
                    <div align="justify">
                        <span>Collect site activities data regularly to provide input for calculating emissions.
                            With the user-friendly interface this exercise will soon become second nature.</span></div>
                </div>
                <div class="box">
                    <img src="img/img_analice.png" alt= "site impacts", "data analysis", "data conversion", "data processing" />
                    <div>
                        ANALYSE</div>
                    <div align="justify">
                        <span class="box2">Analyse and convert data into KPIs to describe the site's environmental
                            performance. Take control of the impacts, keep ahead of legislation and reduce costs.
                        </span>
                    </div>
                </div>
                <div class="box">
                    <img src="img/img_assess.png" alt= "site impacts", "set targets", "environmental performance" />
                    <div>
                        SET TARGETS</div>
                    <div align="justify">
                        <span>Set realistic targets and track the site performance with ease. Having all the
                            information in the same place will greatly facilitate the management process.
                        </span>
                    </div>
                </div>
                <div class="box">
                    <img src="img/img_report.png" alt= "site impacts", "report CO2 emissions" />
                    <div>
                        REPORT</div>
                    <div align="justify">
                        <span>Report emissions data to stakeholders and any interested parties. Keep them informed
                            about company performance and comply with CSR requirements. </span>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--******************************************************-->
    <!--*********** What We Do Section ***********************-->
    <!--******************************************************-->
    <div itemscope itemtype="http://data-vocabulary.org/Product"> <div id="divWhatWeDo">
        <div class="wrap">
            <div class="center">
                <div href="#aWhatWeDo" class="ArrowTop oscurra WhatWeDo">
                </div>
            </div itemscope> 
            <div class="title">
                What we do</div>
            <div class="text">
                <div style="float: left; width: 475px; margin-right: 34px;">
                    <p align="justify">
                        <span itemprop="name">CSI Environmental Management Platform</span> is a <span itemprop="applicationCategory">web 
                        application</span> to measure the environmental impacts arising from construction activities.
                        <br />
                        <br />
                        With an intuitive and user-friendly interface, the relevant data can be easily uploaded
                        into the tool where the powerful engine will efficiently manage the data and return
                        the results in a range of environmental metrics.
                    </p>
                    <p align="justify">
                        With all information in the same place, the management of site environmental impacts
                        can be evaluated with greater confidence and reduced risk.
                        <br />
                        <br />
                    </p>
                </div>
                <div style="float: left; width: 475px;">
                    <div align="justify">
                        Primarily built with the construction industry in mind, CSI will monitor, help set
                        targets and report on site emissions arising from energy consumption including gas
                        and liquid fuel alongside emissions generated by delivery and subcontractor travel.
                        All data is then compared with DTI's Environmental KPI benchmarks to help evaluate
                        performance against industry leaders.
                        <br />
                        <br />
                        The reporting capability of the CSI engine follows all relevant UK and international
                        protocols such as the DEFRA emissions factors, ISO 14064-1:2006 and GHG Protocol
                        initiative. This ensures a robust and enhanced auditable evidence trail to be communicated
                        to stakeholders and interested parties.
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <img src="img/pasos.png" alt= "construction site impacts web application", "site impacts"/>
            <div class="box">
                <div class="left">
                    <div class="title">
                        Benefits</div>
                    <ul>
                        <li>Easy to sign up and use.</li>
                        <li>Web based tool. No software download, no viruses risk.</li>
                        <li>Intuitive user interface.</li>
                        <li>Geo – referenced sites. </li>
                        <li>Easily monitor target and reduce emissions.</li>
                            <li>Comply with <a href="http://www.breeam.org/" target="_blank">BREEAM</a>, <a href="http://www.wrap.org.uk/"
                                target="_blank">WRAP</a> and the <a href="https://www.gov.uk/government/policies/improving-the-energy-efficiency-of-buildings-and-using-planning-to-protect-the-environment/supporting-pages/code-for-sustainable-homes" target="_blank">Code for Sustainable
                                    Homes</a>.</li>
                            <li>Compare results with industry leaders.</li>
                                <li>Encrypted security, authorised access only.</li>
                                <li>Clear subscription structure.</li>
                    </ul>
                </div>
                <div class="right">
                    <div class="title">
                        Key features</div>
                    <ul>
                        <li>A nominated individual can monitor, collect and record meter readings and deliveries
                            data, in a simple and easy way.</li>
                        <li>Management team can select relevant KPIs and set realistic targets for continuous
                            improvement.</li>
                        <li>System reporting capabilities can display graphical analysis showing site perfomance
                            against targets for any given time over the project duration.</li>
                        <li>All the information is available online and on site, to the senior project and site
                            management staff/suppliers and dynamically updated while the System is fed with
                            data.</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!--******************************************************-->
    <!--***************** Our Prices Section *****************-->
    <!--******************************************************-->
    <div id="divOurPrices">
        <div class="wrap">
            <div class="center">
                <div href="#OurPrices" class="ArrowTop oscurra OurPrices">
                </div>
            </div>
            <div class="Text">
                We like to keep our prices simple, we do not dress up our prices
            </div>
            <div class="Subtext">
                We charge a flat fee for your subscription based on the project construction cost.
                That's it - No hidden extras!
            </div>
            <!-- 1 -->
            <div class="box">
                <div class="category category1">
                    ALL CATEGORIES</div>
                <div class="price price1">
                    <div>
                        <p>
                            £100.00<span>(+VAT) PER SITE</span></p>
                    </div>
                </div>
                <div class="number">
                    <div>
                        <p>
                            Set-up fee per Site.<br />
                            One-off payment.</p>
                    </div>
                </div>
            </div>
            <!-- 2 -->
            <div class="box">
                <div class="category category2">
                    CATEGORY 1</div>
                <div class="price price2">
                    <div>
                        <p>
                            £2.00<span>(+VAT) PER MONTH</span></p>
                    </div>
                </div>
                <div class="number">
                    <div>
                        <p>
                            Up to 500,000.00
                        </p>
                    </div>
                </div>
            </div>
            <!-- 3 -->
            <div class="box">
                <div class="category category3">
                    CATEGORY 2</div>
                <div class="price price3">
                    <div>
                        <p>
                            £4.00<span>(+VAT) PER MONTH</span></p>
                    </div>
                </div>
                <div class="number">
                    <div>
                        <p>
                            From 500,000.00 to 4,999,999.99</p>
                    </div>
                </div>
            </div>
            <!-- 4 -->
            <div class="box">
                <div class="category category4">
                    CATEGORY 3</div>
                <div class="price price4">
                    <div>
                        <p>
                            £8.00<span>(+VAT) PER MONTH</span></p>
                    </div>
                </div>
                <div class="number">
                    <div>
                        <p>
                            From 5,000,000.00 to 10,000,000.00</p>
                    </div>
                </div>
            </div>
            <!-- 5 -->
            <div class="box">
                <div class="category category5">
                    CATEGORY 4</div>
                <div class="price price5">
                    <div>
                        <p>
                            £10.00<span>(+VAT) PER MONTH</span></p>
                    </div>
                </div>
                <div class="number">
                    <div>
                        <p>
                            Above 10,000,000.00</p>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="detail">
                <span></span>
                <div class="box2">
                    <div class="left">
                        <div class="title">
                            All categories include:</div>
                        <ul>
                            <li>30-day free trial. </li>
                            <li>Project use of platform. </li>
                            <li>Remote support and help desk. </li>
                        </ul>
                    </div>
                    <div class="right">
                        <div class="title">
                            Ask us for additional services:</div>
                        <ul>
                            <li>Onsite training. </li>
                            <li>Multiple site discount. </li>
                            <li>Quarterly analytical report.</li>
                            <li>Monthly data collection and loading.</li>
                        </ul>
                    </div>
                    <div class="payment">
                        <!-- Payment Logo --><table border="0" cellpadding="10" cellspacing="0" align="left"><tr>
                        <br />
			            <img src="img/PoweredByStripe.png" /><td align="center"></td></tr><tr><td align="center">
                        </a></div></td></tr></table><!-- Payment Logo -->


                    </div>

                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--******************************************************-->
    <!--***************** Who We Are Section *****************-->
    <!--******************************************************-->
    <div id="divWhoWeAre">
        <div class="wrap">
            <div class="center">
                <div class="ArrowTop blanca contact">
                </div>
            </div>
            <div class="left">
                <div class="title">
                    Contact us
                </div>
                <div class="text">
                    <!--  We Do Care - Ecological Building Solutions<br /><br />-->
                    LONDON OFFICE<br />
                    <div itemprop="address" itemscope itemtype="http://schema.org/PostalAddress">
                    <span itemprop="streetAddress">45 Rotherfield Street</span><br />
                    <span itemprop="addressLocality">London</span><br />
                    <span itemprop="postalCode">N1 3BU</span><br />  
                    <span itemprop="addressCountry">UNITED KINGDOM</span><br />
                </div>
                     <br />
                    <a href="mailto:info@siteimpacts.com">info@siteimpacts.com</a>
                    <div class="social-red">
                        <a href="https://www.facebook.com/siteimpacts" target="_blank" class="facebook">
                            <div class="bottom">
                            </div>
                            <div class="top">
                            </div>
                        </a><a href="https://twitter.com/SiteImpacts" target="_blank" class="twitter">
                            <div class="bottom">
                            </div>
                            <div class="top">
                            </div>
                        </a><a href="http://www.linkedin.com/company/siteimpacts?trk=top_nav_home" target="_blank"
                            class="linkedin">
                            <div class="bottom">
                            </div>
                            <div class="top">
                            </div>
                        </a>
                    </div>
                    <div class="titleD">
                        Data Protection
                    </div>
                    <div class="textD">
                        Data is kept in strict accordance with our <a href="http://app.siteimpacts.com/Registration/DataPolicy.aspx"
                            target="_blank">Data Policy</a>.
                    </div>
                </div>
            </div>
            <div class="right">
                <div class="title">
                    Why not evaluate our CSI web app using your own data?
                </div>
                <div class="text">
                    Ask us about an online demo and we will show you how easy it is to set up and start
                    using.
                    <br />
                    <br />
                    If you would like one of our expert to contact you with more information please
                    include your telephone number.
                    <br />
                    <br />
                    Fields marked with * are required
                </div>
                <form id="frmDefault" runat="server">
                <asp:ScriptManager ID="smManager" runat="server" EnablePageMethods="true" />
                <div style="position: relative;">
                    <asp:TextBox ID="txtName" runat="server" CssClass="nombre"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="wmName" runat="server" TargetControlID="txtName"
                        WatermarkCssClass="watermarked" />
                    <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" runat="server"
                        ValidationGroup="Submit" Text="*" EnableClientScript="true" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="email"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="wmEmail" runat="server" TargetControlID="txtEmail"
                        WatermarkCssClass="watermarked" />
                    <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="txtEmail" runat="server"
                        ValidationGroup="Submit" Text="*" EnableClientScript="true" SetFocusOnError="true"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revRegisterEmail" runat="server" ValidationGroup="Submit"
                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        SetFocusOnError="true" EnableClientScript="true" Text="*" Display="Dynamic">
                    </asp:RegularExpressionValidator><asp:TextBox ID="txtOrganization" runat="server"
                        CssClass="organisation"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="wmOrganization" runat="server" TargetControlID="txtOrganization"
                        WatermarkCssClass="watermarked" />
                    <asp:RequiredFieldValidator ID="rfvOrganization" ControlToValidate="txtOrganization"
                        ValidationGroup="Submit" Text="*" EnableClientScript="true" SetFocusOnError="true"
                        runat="server"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="telephone"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="wmPhone" runat="server" TargetControlID="txtPhone"
                        WatermarkCssClass="watermarked" />
                    <span style="visibility: hidden;">*</span>
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="message" TextMode="MultiLine"
                        Rows="4"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="wmMessage" runat="server" TargetControlID="txtMessage"
                        WatermarkCssClass="watermarked" />
                    <div id="divAjaxGif" class="ajaxgif" style="display: none;">
                    </div>
                    <br />
                    <asp:Button UseSubmitBehavior="false" runat="server" ID="btnSubmit" CausesValidation="true"
                        ValidationGroup="Submit" OnClientClick="if(Page_ClientValidate()) Submit();return false;"
                        class="send" />
                </div>
                <div id="divMessage" style="display: none">
                </div>
                </form>
            </div>
            <div class="clear">
            </div>
            <div id="divFooter">
                <img itemprop="image" src="img/logo_footer.png" alt="site impacts", "construction site impacts", "MAN 3", "CSH", "CSI", "BREEAM"/>
                <div class="info">
                © 2013<div itemscope itemtype="http://schema.org/Organization"> <span itemprop="name">Construction Site Impacts</span>.
                All rights reserved. CSI is part of the We Do Care ltd group of companies. Registered in England and Wales Company 
                number 04332702. VAT registration number 888 5399 41. No part of this site may be reproduced without
                    our written permission.</div>
            </div>
        </div>
    </div></span>
   
    <script type="text/javascript">
        (function (d, s, id) {
            if (d.getElementById(id)) return;
            var js = d.createElement(s), hbjs = d.getElementsByTagName(s)[0];
            js.id = id; js.src = window.location.protocol +
            "//www.heybubble.com/vchat/frame/2EBE35DF5726C455863D1A98C0E8A366";
            hbjs.parentNode.insertBefore(js, hbjs);
        } (document, 'script', 'heybubble-jssdk'));
    </script>
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
			m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-43658356-1', 'siteimpacts.com');
        ga('send', 'pageview');
    </script>
    <a href="https://plus.google.com/103088355964846252006?rel=author" style="display:none">SiteImpacts/>  
</body>
</html>
