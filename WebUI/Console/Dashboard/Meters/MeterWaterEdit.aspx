<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterWaterEdit.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterWaterEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="cntMeterWaterEdit" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            initDescriptionTranslations();
            initEFDescriptionTranslations();
        });

        //******************************************
        //      Description Translations
        //******************************************
        var arrTranslations;
        function initDescriptionTranslations() {

            var _arrExistingValues = $get('<%=hdnDescriptionTranslations.ClientID%>').value.split('°');
            for (x = 0; x < _arrExistingValues.length; x++) {
                _arrExistingValues[x] = _arrExistingValues[x].split('|');
            }

            var _ddlLanguages = $get('<%=ddlDescriptionTranslations.ClientID%>');
            arrTranslations = new Array(_ddlLanguages.options.length);

            for (var x = 0; x < _ddlLanguages.options.length; x++) {

                arrTranslations[x] = new Array(2);
                arrTranslations[x][0] = _ddlLanguages.options[x].value;

                var _found = false;
                for (var i = 0; i < _arrExistingValues.length; i++) {

                    if (_arrExistingValues[i][0] == arrTranslations[x][0]) {
                        arrTranslations[x][1] = _arrExistingValues[i][1];
                        _found = true;

                        if (_ddlLanguages.options[_ddlLanguages.selectedIndex].value == arrTranslations[x][0])
                            $get('<%=txtDescriptionTranslations.ClientID%>').value = arrTranslations[x][1];
                        break;
                    }
                }
                if (!_found)
                    arrTranslations[x][1] = '';
            }
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

        //******************************************
        //      EF Description Translations
        //******************************************
        var arrEFTranslations;
        function initEFDescriptionTranslations() {

            var _arrExistingValues = $get('<%=hdnEFDescriptionTranslations.ClientID%>').value.split('°');
            for (x = 0; x < _arrExistingValues.length; x++) {
                _arrExistingValues[x] = _arrExistingValues[x].split('|');
            }

            var _ddlLanguages = $get('<%=ddlEFDescriptionTranslations.ClientID%>');
            arrEFTranslations = new Array(_ddlLanguages.options.length);

            for (var x = 0; x < _ddlLanguages.options.length; x++) {

                arrEFTranslations[x] = new Array(2);
                arrEFTranslations[x][0] = _ddlLanguages.options[x].value;

                var _found = false;
                for (var i = 0; i < _arrExistingValues.length; i++) {

                    if (_arrExistingValues[i][0] == arrEFTranslations[x][0]) {
                        arrEFTranslations[x][1] = _arrExistingValues[i][1];
                        _found = true;

                        if (_ddlLanguages.options[_ddlLanguages.selectedIndex].value == arrEFTranslations[x][0])
                            $get('<%=txtEFDescriptionTranslations.ClientID%>').value = arrEFTranslations[x][1];
                        break;
                    }
                }
                if (!_found)
                    arrEFTranslations[x][1] = '';
            }
        }

        //Check translation save
        $(function () {
            $('#<%=ddlEFDescriptionTranslations.ClientID%>').focus(function () {
                prev_val = $(this).val();
            }).change(function () {
                $(this).blur() // Firefox fix as suggested by AgDude

                if (checkEFUnsavedTranslation(prev_val)) {
                    readEFDescriptionTranslations();
                }
                else {
                    $(this).val(prev_val);
                    return false;
                }
            });
        });

        function checkEFUnsavedTranslation(language) {

            var _txtEFDescriptionTranslation = $get('<%=txtEFDescriptionTranslations.ClientID%>');

            for (var x = 0; x < arrEFTranslations.length; x++) {
                if (language == arrEFTranslations[x][0]) {
                    if (_txtEFDescriptionTranslation.value != arrEFTranslations[x][1]) {
                        return confirm('<%=Resources.Messages.TranslationNotSaved%>');
                        break;
                    }
                }
            }
            return true;
        }

        function toggleEFDescriptionTranslations() {

            var _divEFTranslations = $get('divEFDescriptionTranslations');

            if (_divEFTranslations.style.display == 'none')
                _divEFTranslations.style.display = 'block';
            else
                _divEFTranslations.style.display = 'none';
        }

        function saveEFDescriptionTranslations() {

            var _ddlLanguages = $get('<%=ddlEFDescriptionTranslations.ClientID%>');
            var _txtEFDescriptionTranslation = $get('<%=txtEFDescriptionTranslations.ClientID%>');

            for (var x = 0; x < arrEFTranslations.length; x++) {


                if (_ddlLanguages.options[_ddlLanguages.selectedIndex].value == arrEFTranslations[x][0]) {
                    arrEFTranslations[x][1] = _txtEFDescriptionTranslation.value;
                    break;
                }
            }
        }

        function readEFDescriptionTranslations() {

            var _ddlLanguages = $get('<%=ddlEFDescriptionTranslations.ClientID%>');
            var _txtEFDescriptionTranslation = $get('<%=txtEFDescriptionTranslations.ClientID%>');

            _txtEFDescriptionTranslation.Value = '';
            for (var x = 0; x < arrEFTranslations.length; x++) {
                if (_ddlLanguages.options[_ddlLanguages.selectedIndex].value == arrEFTranslations[x][0]) {
                    _txtEFDescriptionTranslation.value = arrEFTranslations[x][1];
                    break;
                }
            }
        }

        function serializeEFDescriptionTranslations() {

            var _lastLanguage = $('#<%=ddlDescriptionTranslations.ClientID%>').val();

            if (checkEFUnsavedTranslation(_lastLanguage)) {
                var _serialize = "";
                var _hdnEFDescriptionTranslation = $get('<%=hdnEFDescriptionTranslations.ClientID%>');

                for (var x = 0; x < arrEFTranslations.length; x++) {
                    if (_serialize != '') {
                        _serialize += '°';
                    }
                    _serialize += arrEFTranslations[x][0] + '|' + arrEFTranslations[x][1];
                }
                _hdnEFDescriptionTranslation.value = _serialize;
            }
            else {
                return false;
            }
        }

        //To hidden values
        function serializeTranslations() {
            if (serializeDescriptionTranslations())
                return serializeEFDescriptionTranslations();
            return false;
        }
    </script>
    <script type="text/javascript">

        //******************************************
        //      EF Selector Functions
        //******************************************

        function toggleEFSelector() {

            var _divEFSelector = $get('divEFSelector');

            if (_divEFSelector.style.display == 'none')
                _divEFSelector.style.display = 'block';
            else
                _divEFSelector.style.display = 'none';
        }

        function selectEF() {
            $get('<%=txtEF.ClientID%>').value = $('#<%=lstEFSelector.ClientID%>').val();
            $get('<%=txtEFDescription.ClientID%>').value = $('#<%=lstEFSelector.ClientID%>').text().trim();
        }

        function validateInitialReading(source, arguments) {
            if ($.isNumeric($('#<%=txtInitialReading%>').val())) {
                arguments.IsValid = true;
            } else {
                arguments.IsValid = false;
            }
        }

        function validateInitialDate(source, args) {

            var _txt = $get('<%=txtInitialDate.ClientID%>').value;
            if (_txt == '') {
                args.IsValid = false;
            }
            else {
                $get('<%=hdnInitialDate.ClientID%>').value = _txt;
                args.IsValid = true;
            }
        }

    </script>
    <div class="divLineSeparatorHeader Meter">
    </div>
    <div class="divForm AddMeter">
        <!--*******************************************************-->
        <!--**************** Meter Properties *********************-->
        <!--*******************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
        <asp:Label Style="color: Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
        <br />
        <br />
        <div class="divColumn column3">
            <!-- Identification -->
            <asp:Label ID="lblIdentification" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvIdentification" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtIdentification" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtIdentification" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <!-- Description -->
        <div class="divColumn column1 multiLine">
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
        <div class="clear">
        </div>
        <div class="divColumn column3">
            <!-- Units -->
            <asp:Label ID="lblUnits" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvUnits" runat="server" ControlToValidate="ddlUnits" Operator="NotEqual"
                ValueToCompare="0" ValidationGroup="Edit" Display="Dynamic" CssClass="rfvRequested"></asp:CompareValidator>
            <asp:DropDownList ID="ddlUnits" runat="server" AutoPostBack="false">
            </asp:DropDownList>
        </div>
        <div class="divColumn column3">
            <!-- Frequency Quantity -->
            <asp:Label ID="lblFrequencyQuantity" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvFrequencyQuantity" runat="server" ControlToValidate="txtFrequencyQuantity"
                Type="Integer" Operator="DataTypeCheck" ValidationGroup="Edit" CssClass="rfvRequested"
                Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvFrequencyQuantity" runat="server" ControlToValidate="txtFrequencyQuantity"
                ValidationGroup="Edit" CssClass="rfvRequested" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFrequencyQuantity" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <!-- Frequency Units -->
            <asp:Label ID="lblFrequencyUnits" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvFrequencyUnits" runat="server" ControlToValidate="ddlFrequencyUnits"
                Operator="NotEqual" ValueToCompare="0" ValidationGroup="Edit" CssClass="rfvRequested"
                Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvFrequencyUnits" runat="server" ControlToValidate="ddlFrequencyUnits"
                ValidationGroup="Edit" CssClass="rfvRequested" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlFrequencyUnits" runat="server" AutoPostBack="false">
            </asp:DropDownList>
        </div>
        <div class="divColumn column3">
            <!-- Alert Before -->
            <asp:Label ID="lblAlertBefore" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RangeValidator ID="rvAlertBefore" runat="server" ValidationGroup="Edit" Type="Integer"
                ControlToValidate="txtAlertBefore" MinimumValue="0" MaximumValue="255" CssClass="rfvRequested"
                Display="Dynamic"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="rfvAlertBefore" runat="server" ControlToValidate="txtAlertBefore"
                ValidationGroup="Edit" CssClass="rfvRequested" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtAlertBefore" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3">
            <!-- Alert After -->
            <asp:Label ID="lblAlertAfter" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RangeValidator ID="rvAlertAfter" runat="server" ValidationGroup="Edit" Type="Integer"
                ControlToValidate="txtAlertAfter" MinimumValue="0" MaximumValue="255" CssClass="rfvRequested"
                Display="Dynamic"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="rfvAlertAfter" runat="server" ControlToValidate="txtAlertAfter"
                ValidationGroup="Edit" CssClass="rfvRequested" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtAlertAfter" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <!-- Alert On Start -->
            <asp:Label ID="lblAlertOnStart" runat="server" CssClass="lblTitle"></asp:Label><br />
            <asp:CheckBox ID="chkAlertOnStart" runat="server" />
        </div>
        <div class="clear">
        </div>
        <!-- Initial Reading -->
        <div id="divInitialReading" style="display: none" runat="server">
            <div class="divColumn column3 margin">
                <asp:Label ID="lblInitialDate" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnInitialDate" />
                <asp:TextBox ID="txtInitialDate" runat="server" ReadOnly="true" CssClass="lblValue"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calInitialDate" runat="server" DefaultView="Days"
                    TargetControlID="txtInitialDate" PopupButtonID="btnInitialDate">
                </ajaxToolkit:CalendarExtender>
                <asp:CustomValidator ID="cvInitialDate" runat="server" ValidationGroup="Edit" EnableClientScript="true"
                    ClientValidationFunction="validateInitialDate"></asp:CustomValidator><br />
            </div>
            <div class="divColumn column3 last margin">
                <asp:Label ID="lblInitialReading" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:TextBox ID="txtInitialReading" runat="server" CssClass="lblValue"></asp:TextBox>
                <asp:CustomValidator ID="cvInitialReading" runat="server" ControlToValidate="txtInitialReading"
                    ClientValidationFunction="validateInitialReading" ValidationGroup="Edit"></asp:CustomValidator>
            </div>
        </div>
        <div class="divColumn column3" style="display: none;">
            <!-- EF Value -->
            <div id="divEF" style="display: none;">
                <asp:Label ID="lblEF" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtEF" runat="server" CssClass="lblValue"></asp:TextBox>
                <asp:ImageButton ID="btnEFSelector" runat="server" OnClientClick="toggleEFSelector(); return false;" />
                <asp:CompareValidator ID="cvEF" runat="server" ControlToValidate="txtEF" Type="Double"
                    Operator="DataTypeCheck" ValidationGroup="Edit"></asp:CompareValidator>
            </div>
        </div>
        <div class="divColumn column3" style="display: none;">
            <!-- EF Selector -->
            <input type="hidden" id="hdnIdEF" runat="server" value="0" />
            <div id="divEFSelector" style="display: none; float: inherit">
                <asp:Button ID="btnEFSelectorClose" runat="server" OnClientClick="toggleEFSelector();return false" />
                <asp:Label ID="lblEFSelector" runat="server"></asp:Label><br />
                <asp:DropDownList ID="ddlEFSelectorCountries" runat="server" AutoPostBack="true">
                </asp:DropDownList>
                <br />
                <asp:UpdatePanel ID="upEFSelector" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEFSelectorCountries" EventName="selectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:ListBox ID="lstEFSelector" runat="server" Rows="5"></asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- EF Description -->
        <div id="divEFDescription" style="display: none;">
            <asp:Label ID="lblEFDescription" runat="server"></asp:Label><br />
            <asp:TextBox ID="txtEFDescription" runat="server" TextMode="MultiLine" Columns="40"
                Rows="5"></asp:TextBox>
            <asp:ImageButton ID="btnEFDescriptionTranslations" runat="server" OnClientClick="toggleEFDescriptionTranslations();return false;" />
        </div>
        <!-- EF Description Translations -->
        <asp:HiddenField ID="hdnEFDescriptionTranslations" runat="server" />
        <div id="divEFDescriptionTranslations" style="display: none; float: inherit">
            <asp:Button ID="btnEFDescriptionTranslationsClose" runat="server" OnClientClick="toggleEFDescriptionTranslations();return false" />
            <asp:Label ID="lblEFDescriptionTranslations" runat="server"></asp:Label><br />
            <asp:DropDownList ID="ddlEFDescriptionTranslations" runat="server" AutoPostBack="false"
                onchange="readEFDescriptionTranslations()">
            </asp:DropDownList>
            <br />
            <asp:TextBox ID="txtEFDescriptionTranslations" TextMode="MultiLine" Columns="40"
                Rows="5" runat="server"></asp:TextBox><br />
            <asp:Button ID="btnEFDescriptionTranslationsSave" runat="server" OnClientClick="saveEFDescriptionTranslations();return false" />
        </div>
        <div class="clear">
        </div>
        <!--**************************************************************-->
        <!--********************* Save  **********************************-->
        <!--**************************************************************-->
        <div class="divContentSave">
            <asp:Button ID="btnSave" runat="server" CausesValidation="true" ValidationGroup="Edit"
                OnClientClick="serializeTranslations()" CssClass="btnActions btnSave" />
        </div>
    </div>
</asp:Content>
