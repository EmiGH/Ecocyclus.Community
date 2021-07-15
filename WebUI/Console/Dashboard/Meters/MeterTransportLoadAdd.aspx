<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterTransportLoadAdd.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterTransportLoadAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="cphContent" runat="server">
    
    <script type="text/javascript" src="../../../Scripts/map.js"></script>
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblGrid').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
        });
    
    </script>
    <script type="text/javascript">

        //Init values for fields with hidden values on postbacks
        $(document).ready(function () {
            if ($("#<%=hdnLoadDate.ClientID%>").val() != "") {
                $('#<%=txtLoadDate.ClientID%>').val($("#<%=hdnLoadDate.ClientID%>").val());
            }
        });

        function switchLoadType() {
            if ($('#<%=rdDistance.ClientID%>').is(':checked')) {
                $('#cphContent_divLoadLocation').hide();
                $('#cphContent_divLoadDistance').show();
                $('#divMapPickup').css('height', '0');
            }
            else {
                $('#cphContent_divLoadDistance').hide();
                $('#cphContent_divLoadLocation').show();
                $('#divMapPickup').css('height', '300');
            }
        }

        function validateValue(source, args) {
            if ($('#<%=rdDistance.ClientID%>').is(':checked')) {
                
                if ($('#<%=txtLoadDistanceValue.ClientID%>').val() != '' && $('#<%=ddlLoadDistanceUnits.ClientID%>').val() != '') {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            else {
                if ($('#<%=hdnLoadLocationAddress.ClientID%>').val() != '' && $('#<%=hdnLoadLocationPoint.ClientID%>').val() != '') {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }

        }

        function validateGrid(source, args) {

            if ($('#tblGrid tr').length > 1) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function validateDate(source, args) {

            var _txt = $get('<%=txtLoadDate.ClientID%>').value;
            if (_txt == '') {
                args.IsValid = false;
            }
            else {
                $get('<%=hdnLoadDate.ClientID%>').value = _txt;
                args.IsValid = true;
            }
        }

        function validateLocation(source, args) {

            if ($('#<%=rdLocation.ClientID%>').is(':checked')) {

                var _hdn = $get('<%=hdnLoadLocationPoint.ClientID%>');
                if (_hdn.value == '') {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }
        }

    </script>
    <script type="text/javascript">

        //*********************************************************//
        //***************** Map  Functions  ***********************//
        //*********************************************************//
        $(document).ready(function () {

            //Init values for fields with hidden values on postbacks
            if ($("#<%=hdnLoadLocationPoint.ClientID%>").val() != "") {
                $('#txtMapLocator').val($("#<%=hdnLoadLocationAddress.ClientID%>").val());
                $('#hdnMapPoint').val($("#<%=hdnLoadLocationPoint.ClientID%>").val());
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
            var _drivingDistance = $('#hdnMapDistance').val();

            var _hdnAddress = $get('<%=hdnLoadLocationAddress.ClientID%>');
            var _hdnCoords = $get('<%=hdnLoadLocationPoint.ClientID%>');
            var _hdnDistance = $get('<%=hdnLoadLocationDistance.ClientID%>');
            
            if (_coordenates != '') {
                _hdnCoords.value = _coordenates;
                _hdnAddress.value = _address;
                _hdnDistance.value = _drivingDistance;
            }
            else {
                _hdnCoords.value = "";
                _hdnAddress.value = "";
                _hdnDistance.value = "";
            }
        }
        
    </script>
    <div class="divLineSeparatorHeader Meter">
    </div>
    <div class="divTitle">
        <asp:Label ID="lblProperties" runat="server"></asp:Label>
    </div>
    <div class="divDetail">
        <!--*******************************************************-->
        <!--**************** Meter Properties *********************-->
        <!--*******************************************************-->
        <div id="divProperties">
            <div class="divColumn column3">
                <asp:Label ID="lblIdentification" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblIdentificationValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column1 margin">
                <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>
        <div class="clear">
        </div>
        <!--*******************************************************-->
        <!--****************** Load Values ************************-->
        <!--*******************************************************-->
        <div id="divLoad">
            <div class="divTitle">
                <asp:Label ID="lblLoad" runat="server"></asp:Label>
            </div>
            <asp:Label Style="color: Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
            <br />
            <br />
            <div>
                <asp:Label ID="lblValidLoadRange" runat="server"></asp:Label>
            </div>
            <br />
            <br />
            <div id="divLoadGeneral" class="divForm AddSerie">
                <div class="divColumn column4">
                    <asp:Label ID="lblLoadHeaderDate" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:CustomValidator ID="cvLoadDate" runat="server" ValidationGroup="Load" EnableClientScript="true"
                        ClientValidationFunction="validateDate" Display="Dynamic" CssClass="rfvRequested"></asp:CustomValidator>
                    <asp:HiddenField runat="server" ID="hdnLoadDate" />
                    <asp:TextBox ID="txtLoadDate" runat="server" ReadOnly="true" CssClass="lblValue"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calLoadDate" runat="server" DefaultView="Days"
                        TargetControlID="txtLoadDate" PopupButtonID="btnLoadDate">
                    </ajaxToolkit:CalendarExtender>
                </div>
                <div class="divColumn column4">
                    <asp:Label ID="lblLoadHeaderPlateNumber" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:TextBox ID="txtLoadPlateNumber" runat="server" CssClass="lblValue"></asp:TextBox>
                </div>
                <div class="divColumn column2 last">
                    <asp:Label ID="lblLoadHeaderTypes" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:CompareValidator ID="cvLoadTypes" runat="server" ControlToValidate="ddlLoadTypes"
                        Operator="NotEqual" ValueToCompare="0" ValidationGroup="Load" Display="Dynamic"
                        CssClass="rfvRequested"></asp:CompareValidator>
                    <asp:DropDownList ID="ddlLoadTypes" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="divColumn column4" style="height: 30px !important; display: none; visibility: hidden;">
                <asp:Label ID="lblLoadInputType" runat="server" CssClass="lblTitle"></asp:Label>
                <div id="divLoadType">
                    <asp:CustomValidator ID="cvLocation" runat="server" ValidationGroup="Load" EnableClientScript="true"
                        ClientValidationFunction="validateLocation" Display="Dynamic" CssClass="rfvRequested"></asp:CustomValidator>
                    <asp:RadioButton ID="rdDistance" runat="server" GroupName="LoadType" Checked="true" />
                    <asp:RadioButton ID="rdLocation" runat="server" GroupName="LoadType" />
                </div>
            </div>
            <div class="divColumn column4" style="height: 30px !important; display: none; visibility: hidden;">
                <asp:Label ID="lblLoadHeaderIsRoundtrip" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:CheckBox ID="chkIsRoundtrip" runat="server" />
            </div>
            <div class="clear">
            </div>
            <div id="divLoadDistance" runat="server" class="divForm AddSerie">
                <div class="divColumn column2">
                    <asp:Label ID="lblLoadDistanceHeaderValue" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:TextBox ID="txtLoadDistanceValue" runat="server" CssClass="lblValue"></asp:TextBox>
                </div>
                <div class="divColumn column2 last">
                    <asp:Label ID="lblLoadDistanceHeaderUnit" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:DropDownList ID="ddlLoadDistanceUnits" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <!-- ***************************************-->
        <!-- ************* Map Pickup **************-->
        <!-- ***************************************-->
        <asp:HiddenField ID="hdnLoadLocationAddress" runat="server" />
        <asp:HiddenField ID="hdnLoadLocationPoint" runat="server" />
        <asp:HiddenField ID="hdnLoadLocationDistance" runat="server" />
        <div id="divMapPickup" style="height: 0px; width: 732px; overflow: hidden;" class="divForm AddSerie">
            <div id="divMapLocator" class="divColumn column1 margin">
                <asp:Label ID="lblMapLocator" runat="server" CssClass="lblTitle"></asp:Label>
                <input type="text" id="txtMapLocator" value="" class="lblValue" />
            </div>
            <div class="clear">
            </div>
            <div id="divMapCanvas" class="divMapCanvas divLarge">
            </div>
            <div id="divMapGeocoding">
            </div>
            <div>
                <input id="hdnMapPoint" type="hidden" />
                <input id="hdnMapAddress" type="hidden" />
                <input id="hdnMapSite" type="hidden" value="<%=SiteLocation%>" />
                <input id="hdnMapDistance" type="hidden" />
            </div>
        </div>
        <div class="clear">
        </div>
        <asp:LinkButton ID="lnkLoadAdd" runat="server" ValidationGroup="Load" CssClass="btnMeterAdd"></asp:LinkButton>
        <!--*******************************************************-->
        <!--****************** Loaded Values **********************-->
        <!--*******************************************************-->
        <div id="divGrid">
            <div class="divTitle">
                <asp:Label ID="lblGrid" runat="server"></asp:Label>
            </div>
            <asp:CustomValidator runat="server" ID="cvGrid" ValidationGroup="Add" ClientValidationFunction="validateGrid"></asp:CustomValidator>
            <asp:UpdatePanel ID="upGrid" UpdateMode="Always" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkLoadAdd" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="rptGrid" EventName="ItemDataBound" />
                </Triggers>
                <ContentTemplate>
                    <table id="tblGrid" class="tbl">
                        <thead>
                            <tr>
                                <th>
                                </th>
                                <th>
                                    <asp:Label ID="lblGridHeaderDate" runat="server"></asp:Label>
                                </th>
                                <th>
                                    <asp:Label ID="lblGridHeaderPlateNumber" runat="server"></asp:Label>
                                </th>
                                <!--<th>
                                    <asp:Label ID="lblGridHeaderIsRoundtrip" runat="server"></asp:Label>
                                </th>-->
                                <th class="thlast">
                                    <asp:Label ID="lblGridHeaderTransportType" runat="server"></asp:Label>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptGrid" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnGridDelete" CommandName="Delete" runat="server" CssClass="btnSitesDelete" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblGridDate" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblGridPlateNumber" runat="server"></asp:Label>
                                        </td>
                                        <!--<td>
                                            <asp:Label ID="lblGridIsRoundtrip" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnGridIsRoundtrip" runat="server" />
                                        </td>-->
                                        <td class="tdlast">
                                            <asp:Label ID="lblGridTransportType" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnGridIdTransportType" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="tdlast line">
                                            <asp:Label ID="lblGridValue" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnGridDistance" runat="server" />
                                            <asp:HiddenField ID="hdnGridIdUnit" runat="server" />
                                            <asp:HiddenField ID="hdnGridAddress" runat="server" />
                                            <asp:HiddenField ID="hdnGridDrivingDistance" runat="server" />
                                            <asp:HiddenField ID="hdnGridLocation" runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="trAlternating">
                                        <td>
                                            <asp:Button ID="btnGridDelete" CommandName="Delete" runat="server" CssClass="btnSitesDelete" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblGridDate" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblGridPlateNumber" runat="server"></asp:Label>
                                        </td>
                                        <!--<td>
                                            <asp:Label ID="lblGridIsRoundtrip" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnGridIsRoundtrip" runat="server" />
                                        </td>-->
                                        <td class="tdlast">
                                            <asp:Label ID="lblGridTransportType" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnGridIdTransportType" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="trAlternating">
                                        <td colspan="5" class="tdlast line-b">
                                            <asp:Label ID="lblGridValue" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnGridDistance" runat="server" />
                                            <asp:HiddenField ID="hdnGridIdUnit" runat="server" />
                                            <asp:HiddenField ID="hdnGridAddress" runat="server" />
                                            <asp:HiddenField ID="hdnGridDrivingDistance" runat="server" />
                                            <asp:HiddenField ID="hdnGridLocation" runat="server" />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <!--**************************************************************-->
        <!--********************* Save  **********************************-->
        <!--**************************************************************-->
        <div class="divContentSave">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="Add" CssClass="btnSave btnActions" />
        </div>
    </div>
</asp:Content>
