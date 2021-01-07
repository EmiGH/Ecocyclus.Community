<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true" CodeBehind="MeterWasteAdd.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.WasteMeterAdd" %>

<asp:Content ID="cntMeterWasteAdd" ContentPlaceHolderID="cphContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            InitDescriptionTranslations();
        });

        var arrTranslations;

        function ToggleDescriptionTranslations() {

            var _divTranslations = $get('divDescriptionTranslations');

            if (_divTranslations.style.display == 'none')
                _divTranslations.style.display = 'block';
            else
                _divTranslations.style.display = 'none';
        }

        function InitDescriptionTranslations() {

            var _ddlLanguages = $get('<%=ddlDescriptionTranslations.ClientID%>');

            arrTranslations = new Array(_ddlLanguages.options.length);

            for (var x = 0; x < _ddlLanguages.options.length; x++) {
                arrTranslations[x] = new Array(2);
                arrTranslations[x][0] = _ddlLanguages.options[x].value;
                arrTranslations[x][1] = '';
            }
        }

        function SaveDescriptionTranslations() {

            var _ddlLanguages = $get('<%=ddlDescriptionTranslations.ClientID%>');
            var _txtDescriptionTranslation = $get('<%=txtDescriptionTranslations.ClientID%>');

            for (var x = 0; x < arrTranslations.length; x++) {
                if (_ddlLanguages.options[_ddlLanguages.selectedIndex].value == arrTranslations[x][0]) {
                    arrTranslations[x][1] = _txtDescriptionTranslation.value;
                    break;
                }
            }
        }

        function ReadDescriptionTranslations() {

            var _ddlLanguages = $get('<%=ddlDescriptionTranslations.ClientID%>');
            var _txtDescriptionTranslation = $get('<%=txtDescriptionTranslations.ClientID%>');

            for (var x = 0; x < arrTranslations.length; x++) {
                if (_ddlLanguages.options[_ddlLanguages.selectedIndex].value == arrTranslations[x][0]) {
                    _txtDescriptionTranslation.value = arrTranslations[x][1];
                    break;
                }
            }
        }

        function SerializeDescriptionTranslations() {

            var _serialize = "";
            var _hdnDescriptionTranslation = $get('<%=hdnDescriptionTranslations.ClientID%>');

            for (var x = 0; x < arrTranslations.length; x++) {
                if (_serialize != '') {
                    _serialize += '°';
                }
                _serialize += arrTranslations[x][0] + '|' + arrTranslations[x][1];
            }
            _hdnDescriptionTranslation.value = _serialize;
            alert(_hdnDescriptionTranslation.value);
        }

    </script>
  
    <div class="divLineSeparatorHeader Meter"></div>
     <div class="divForm AddMeter">
        <!--*******************************************************-->
        <!--**************** Meter Properties *********************-->
        <!--*******************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
        <asp:Label style="color:Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
        <br /><br />

        <div class="divColumn column3">
            <!-- Identification -->
            <asp:Label ID="lblIdentification" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvIdentification" runat="server" ValidationGroup="Add"
                ControlToValidate="txtIdentification" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtIdentification" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <!-- Description -->
        <div class="divColumn column1 multiLine">
            <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Columns="40"
                Rows="5" CssClass="lblValue"></asp:TextBox>
            <asp:Button ID="btnDescriptionTranslations" runat="server" OnClientClick="ToggleDescriptionTranslations();return false;" CssClass="btnActions" />
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
        <div class="divColumn column3">
            <!-- Units -->
            <asp:Label ID="lblUnits" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvUnits" runat="server" ControlToValidate="ddlUnits" Operator="NotEqual"
                ValueToCompare="0" ValidationGroup="Add" Display="Dynamic" CssClass="rfvRequested"></asp:CompareValidator>
            <asp:DropDownList ID="ddlUnits" runat="server" AutoPostBack="false">
            </asp:DropDownList>
        </div>
        <div class="clear">
        </div>
        
        <!--**************************************************************-->
        <!--********************* Save  **********************************-->
        <!--**************************************************************-->
        <div class="divContentSave">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="Add"  CssClass="btnActions btnSave" />
        </div>
    </div>

</asp:Content>
