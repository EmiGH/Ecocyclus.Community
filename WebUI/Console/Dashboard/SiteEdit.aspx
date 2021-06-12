<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="SiteEdit.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.SiteEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="cntHead" runat="server">
    <script type="text/javascript" src="../../Scripts/map.js"></script>
</asp:Content>
<asp:Content ID="cntSiteEdit" ContentPlaceHolderID="cphContent" runat="server">

    <script language="javascript" type="text/javascript">

        //Init values for fields with hidden values on postbacks
        $(document).ready(function () {
            if ($("#<%=hdnStart.ClientID%>").val() != "") {
                $('#<%=txtStart.ClientID%>').val($("#<%=hdnStart.ClientID%>").val());
            }

        });

        function setHiddenValues() {
            serializeDescriptionTranslations();
            return true;
        }

        function validateLocation(source, args) {

            var _lbl = $get('<%=hdnLocationPoint.ClientID%>');

            if (_lbl.value == '') {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function validateStart(source, args) {

            var _txt = $get('<%=txtStart.ClientID%>').value;
            if (_txt == '') {
                args.IsValid = false;
            }
            else {
                $get('<%=hdnStart.ClientID%>').value = _txt;
                args.IsValid = true;
            }
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            searchClients();
            searchAgents();
            searchContractors();
            searchResponsible();
            searchManagers();
        });

        function searchClients() {
            $("#<%=txtClient.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SiteAdd.aspx/GetAutoCompleteClients",
                        data: "{'clientPattern':'" + $get('<%=txtClient.ClientID%>').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
            });
        }

        function searchAgents() {
            $("#<%=txtAgent.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SiteAdd.aspx/GetAutoCompleteAgents",
                        data: "{'agentPattern':'" + $get('<%=txtAgent.ClientID%>').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
            });
        }

        function searchContractors() {
            $("#<%=txtContractor.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SiteAdd.aspx/GetAutoCompleteContractors",
                        data: "{'contractorPattern':'" + $get('<%=txtContractor.ClientID%>').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
            });
        }

        function searchResponsible() {
            $("#<%=txtResponsible.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SiteAdd.aspx/GetAutoCompleteResponsible",
                        data: "{'responsiblePattern':'" + $get('<%=txtResponsible.ClientID%>').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
            });
        }

        function searchManagers() {
            $("#<%=txtManager.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SiteAdd.aspx/GetAutoCompleteManagers",
                        data: "{'managerPattern':'" + $get('<%=txtManager.ClientID%>').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
            });
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            initDescriptionTranslations();
        });

        var arrTranslations;
        function initDescriptionTranslations() {

            var _ddlLanguages = $get('<%=ddlDescriptionTranslations.ClientID%>');
            var _hdnTranslations = $get('<%=hdnDescriptionTranslations.ClientID%>');

            var _arrExisting = _hdnTranslations.value.split("°");
            var _exists;

            arrTranslations = new Array(_ddlLanguages.options.length);
            for (var x = 0; x < _ddlLanguages.options.length; x++) {

                arrTranslations[x] = new Array(2);
                arrTranslations[x][0] = _ddlLanguages.options[x].value;

                _exists = false;
                if (_arrExisting.length > 0) {
                    for (var y = 0; y < _arrExisting.length; y++) {

                        var _existing = _arrExisting[y].split('|');
                        if (_existing[0] == arrTranslations[x][0]) {
                            arrTranslations[x][1] = _existing[1];
                            _exists = true;
                            break;
                        }
                    }
                }

                if (!_exists) {
                    arrTranslations[x][1] = '';
                }

            }

            readDescriptionTranslations();
        }

        //Check translation save
        $(function () {
            $('#<%=ddlDescriptionTranslations.ClientID%>').focus(function () {
                prev_val = $(this).val();
            }).change(function () {
                $(this).blur() // Firefox fix as suggested by AgDude

                if (checkUnsavedTranslation(prev_val)) {
                    readDescriptionTranslations();
                }
                else {
                    $(this).val(prev_val);
                    return false;
                }
            });
        });

        function checkUnsavedTranslation(language) {

            var _txtDescriptionTranslation = $get('<%=txtDescriptionTranslations.ClientID%>');

            for (var x = 0; x < arrTranslations.length; x++) {
                if (language == arrTranslations[x][0]) {
                    if (_txtDescriptionTranslation.value != arrTranslations[x][1]) {
                        return confirm('<%=Resources.Messages.TranslationNotSaved%>');
                        break;
                    }
                }
            }
            return true;
        }

        function toggleDescriptionTranslations() {

            var _divTranslations = $get('divDescriptionTranslations');

            if (_divTranslations.style.display == 'none')
                _divTranslations.style.display = 'block';
            else
                _divTranslations.style.display = 'none';
        }

        function saveDescriptionTranslations() {

            var _ddlLanguages = $get('<%=ddlDescriptionTranslations.ClientID%>');
            var _txtDescriptionTranslation = $get('<%=txtDescriptionTranslations.ClientID%>');

            for (var x = 0; x < arrTranslations.length; x++) {
                if (_ddlLanguages.options[_ddlLanguages.selectedIndex].value == arrTranslations[x][0]) {
                    arrTranslations[x][1] = _txtDescriptionTranslation.value;
                    break;
                }
            }
        }

        function readDescriptionTranslations() {

            var _ddlLanguages = $get('<%=ddlDescriptionTranslations.ClientID%>');
            var _txtDescriptionTranslation = $get('<%=txtDescriptionTranslations.ClientID%>');

            _txtDescriptionTranslation.Value = '';
            for (var x = 0; x < arrTranslations.length; x++) {
                if (_ddlLanguages.options[_ddlLanguages.selectedIndex].value == arrTranslations[x][0]) {
                    _txtDescriptionTranslation.value = arrTranslations[x][1];
                    break;
                }
            }
        }

        function serializeDescriptionTranslations() {

            var _lastLanguage = $('#<%=ddlDescriptionTranslations.ClientID%>').val();

            if (checkUnsavedTranslation(_lastLanguage)) {
                var _serialize = "";
                var _hdnDescriptionTranslation = $get('<%=hdnDescriptionTranslations.ClientID%>');

                for (var x = 0; x < arrTranslations.length; x++) {
                    if (_serialize != '') {
                        _serialize += '°';
                    }
                    _serialize += arrTranslations[x][0] + '|' + arrTranslations[x][1];
                }
                _hdnDescriptionTranslation.value = _serialize;
            }
            else {
                return false;
            }
        }

    </script>
    <script type="text/javascript">

        //*********************************************************//
        //***************** Load Functions  ***********************//
        //*********************************************************//
        $(document).ready(function () {

            //Init values for fields with hidden values on postbacks
            if ($("#<%=hdnLocationPoint.ClientID%>").val() != "") {
                $('#txtMapLocator').val($("#<%=hdnLocationAddress.ClientID%>").val());
                $('#hdnMapPoint').val($("#<%=hdnLocationPoint.ClientID%>").val());
            }

            loadMap();
        });


        function loadMap() {
            var _script = document.createElement("script");
            _script.type = "text/javascript";
            _script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyDzj5Mp1wUYuTzITA9F_Vw0Yjaid30o6dg&sensor=false&callback=mapsLoaded&libraries=places";
            document.body.appendChild(_script);
        }


        function mapPickup() {

            var _coordenates = $('#hdnMapPoint').val();
            var _address = $('#hdnMapAddress').val();
            var _country = $('#hdnMapCountry').val();

            var _hdnAddress = $get('<%=hdnLocationAddress.ClientID%>');
            var _hdnCoords = $get('<%=hdnLocationPoint.ClientID%>');
            var _hdnCountry = $get('<%=hdnLocationCountry.ClientID%>');

            if (_coordenates != '') {
                _hdnCoords.value = _coordenates;
                _hdnAddress.value = _address;
                _hdnCountry.value = _country;

            }
            else {
                _hdnCoords.value = "";
                _hdnAddress.value = "";
                _hdnCountry.value = "";
            }
        }

    </script>
    
    <div class="divForm EditSite">
        <div class="divLineSeparatorHeader Sites"></div>

        <!--*********************************************** -->
        <!--*************** Site Properties *************** -->
        <!--*********************************************** -->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
        <asp:Label style="color:Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
        <br /><br />

        <!-- Image -->
        <div id="divImage" class="divColumn column3 img">
            <div class="divCenter">
                <p>
                    <span>
                        <asp:Image ID="imgImage" runat="server" />
                    </span>
                </p>
            </div>
            <span class="file-wrapper">
                <asp:FileUpload ID="fuImage" runat="server" />
                <asp:Label ID="lblChoosePicture" runat="server" CssClass="button"></asp:Label>
            </span>
            <div class="clear">
            </div>
        </div>
        <div class="divColumn column1p last">
            <!-- Title -->
            <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ValidationGroup="Edit" ControlToValidate="txtTitle"
                EnableClientScript="true" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column1p last">
            <!-- Type -->
            <asp:Label ID="lblType" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvTypes" runat="server" ControlToValidate="ddlTypes"
                        Operator="NotEqual" ValueToCompare="0" ValidationGroup="Edit" Display="Dynamic"
                        CssClass="rfvRequested"></asp:CompareValidator>
            <asp:DropDownList ID="ddlTypes" runat="server"></asp:DropDownList>
        </div>
            <div class="clear">
            </div>
        <div class="divColumn column1 text">
            <asp:Label ID="lblImageFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
            <asp:RegularExpressionValidator ID="revLogo" runat="server" ValidationExpression="^.*\.(jpg|JPG|gif|GIF|png|PNG|bmp|BMP)$"
                ControlToValidate="fuImage" ValidationGroup="Edit"></asp:RegularExpressionValidator>
        </div>
                <div class="clear">
            </div>
        <div class="divColumn column3">
            <!-- Date -->
            <asp:Label ID="lblStart" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CustomValidator ID="cvStart" runat="server" ValidationGroup="Edit" EnableClientScript="true"
                ClientValidationFunction="validateStart" Display="Dynamic" CssClass="rfvRequested"></asp:CustomValidator>
            <asp:HiddenField runat="server" ID="hdnStart" />
            <asp:TextBox ID="txtStart" runat="server" ReadOnly="true" CssClass="lblValue"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="calStart" runat="server" DefaultView="Days" TargetControlID="txtStart">
            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="divColumn column3 last">
            <!-- Number -->
            <asp:Label ID="lblNumber" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtNumber" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="clear">
        </div>

        <div class="divColumn column3 text margin   ">
            <!-- Value -->
            <asp:Label ID="lblValue" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvValue" ValidationGroup="Edit" EnableClientScript="true"
                ControlToValidate="txtValue" runat="server" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvValue" runat="server" ControlToValidate="txtValue" Type="Double"
                Operator="DataTypeCheck" ValidationGroup="Add" Display="Dynamic" CssClass="rfvRequested"></asp:CompareValidator>
            <asp:TextBox ID="txtValue" runat="server" CssClass="lblValue"></asp:TextBox>
            <asp:DropDownList ID="ddlCurrencies" runat="server" AutoPostBack="false">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCurrencies" runat="server" ControlToValidate="ddlCurrencies"
                ValidationGroup="Edit" EnableClientScript="true"></asp:RequiredFieldValidator>
        </div>
        <div class="divColumn column3">
            <!-- FloorSpace -->
            <asp:Label ID="lblFloorSpace" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvFloorSpace" ValidationGroup="Edit" EnableClientScript="true"
                ControlToValidate="txtFloorSpace" runat="server" CssClass="rfvRequested" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvFloorSpace" runat="server" ControlToValidate="txtFloorSpace"
                Type="Double" Operator="DataTypeCheck" ValidationGroup="Add" CssClass="rfvRequested"
                Display="Dynamic"> </asp:CompareValidator>
            <div class="clear">
            </div>
            <asp:TextBox ID="txtFloorSpace" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <!-- Units -->
            <asp:Label ID="lblUnits" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvUnits" runat="server" ControlToValidate="txtUnits" Type="Integer"
                Operator="DataTypeCheck" ValidationGroup="Add" Display="Dynamic" CssClass="rfvRequested"></asp:CompareValidator>
            <asp:TextBox ID="txtUnits" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>


        <!-- Details -->        
        <div style="display:none">
        <div class="clear">
        </div>
        <div class="divTitle">
            <asp:Label ID="lblSiteClient" runat="server"></asp:Label>
        </div>
        <div class="divColumn column2">
            <asp:Label ID="lblClient" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtClient" runat="server" class="autosuggest" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column2 last">
            <asp:Label ID="lblAgent" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtAgent" runat="server" class="autosuggest" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column2">
            <asp:Label ID="lblContractor" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtContractor" runat="server" class="autosuggest" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column2 last">
            <asp:Label ID="lblResponsible" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtResponsible" runat="server" class="autosuggest" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblManager" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtManager" runat="server" class="autosuggest" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3">
            <!-- Contact -->
            <asp:Label ID="lblTelephone" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtTelephone" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblEmail" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                Display="Dynamic" CssClass="rfvRequested"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="lblValue"></asp:TextBox>
            <asp:Label ID="lblEmailFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblUrl" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RegularExpressionValidator ID="revUrl" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtUrl" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"
                Display="Dynamic" CssClass="rfvRequested"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtUrl" runat="server" CssClass="lblValue"></asp:TextBox>
            <asp:Label ID="lblUrlFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblFacebook" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtFacebook" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblTwitter" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtTwitter" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        </div>

        <div class="clear">
        </div>
        <!-- Description -->
        <br />
        <br />
        <div class="divColumn column1 multiLine margin">
            <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Columns="40"
                Rows="5" CssClass="lblValue"></asp:TextBox>
            <asp:Button ID="btnDescriptionTranslations" runat="server" OnClientClick="toggleDescriptionTranslations();return false;"
                CssClass="btnActions" />
            <asp:HiddenField ID="hdnDescriptionTranslations" runat="server" />
            <br />
            <br />
            <div id="divDescriptionTranslations" style="display: none;">
                <asp:Label ID="lblDescriptionTranslations" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlDescriptionTranslations" runat="server" AutoPostBack="false"
                    onchange="readDescriptionTranslations()">
                </asp:DropDownList>
                <asp:TextBox ID="txtDescriptionTranslations" TextMode="MultiLine" Columns="40" Rows="5"
                    runat="server" CssClass="lblValue"></asp:TextBox>
                <asp:Button ID="btnDescriptionTranslationsSave" runat="server" OnClientClick="saveDescriptionTranslations();return false"
                    CssClass="btnActions" />
            </div>
        </div>
        <div class="divColumn column3 text margin">
            <!-- Public -->
            <asp:CheckBox ID="chkIsPublic" runat="server" />
            <asp:Label ID="lblIsPublic" runat="server" CssClass="lblTitle"></asp:Label>
        </div>
        <div class="clear">
        </div>
        <div class="divTitle">
            <asp:Label ID="lblMap" runat="server"></asp:Label>
        </div>

        <!-- ***************************************-->
        <!-- ************* Map Pickup **************-->
        <!-- ***************************************-->
        <!-- Location -->
        <input type="hidden" id="hdnLocationPoint" runat="server" />
        <input type="hidden" id="hdnLocationAddress" runat="server" />
        <input type="hidden" id="hdnLocationCountry" runat="server" />
        <div id="divMapPickup">
            <div id="divMapLocator" class="divColumn column1">
                <asp:Label ID="lblPreviousLocation" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:CustomValidator ID="cuvLocation" runat="server" ValidationGroup="Add" EnableClientScript="true"
                    ClientValidationFunction="validateLocation" ></asp:CustomValidator>
                <input type="text" class="lblValue" id="txtMapLocator" value="<%=Address%>" />
            </div>
            <div class="clear">
            </div>
            <div id="divMapCanvas" class="divMapCanvas divMapDetail divLarge">
            </div>
            <div id="divMapGeocoding">
            </div>
            <div>
                <input id="hdnMapPoint" type="hidden" value="<%=Location%>" />
                <input id="hdnMapAddress" type="hidden" />
                <input id="hdnMapCountry" type="hidden" />
            </div>
        </div>

        <!--**************************************************************-->
        <!--********************* Save  **********************************-->
        <!--**************************************************************-->
        <div class="divContentSave">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="Edit" OnClientClick="setHiddenValues();"
                CssClass="btnActions btnSave" UseSubmitBehavior="false" />
        </div>
    </div>
        
</asp:Content>
