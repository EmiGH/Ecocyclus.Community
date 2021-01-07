<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="Sites.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Sites" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="cntHead" runat="server">
    <script type="text/javascript" src="../../Scripts/mapReferences.js"></script>
</asp:Content>

<asp:Content ID="cntSites" ContentPlaceHolderID="cphContent" runat="server">
    
    <script type="text/javascript">

        //*********************************************************//
        //***************** Mps  Functions  ***********************//
        //*********************************************************//
        $(document).ready(loadMap());

        function loadMap() {
            var _script = document.createElement("script");
            _script.type = "text/javascript";
            _script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyDzj5Mp1wUYuTzITA9F_Vw0Yjaid30o6dg&sensor=false&callback=mapsLoaded&libraries=places";
            document.body.appendChild(_script);

        }

    </script>
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblSites').dataTable({
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

    <!--*********************************************** -->
    <!--*************** Sites Lists ******************* -->
    <!--*********************************************** -->
    <div class="divLineSeparatorHeader Sites">
    </div>
    <div id="divSites">
        <div class="divTitle">
            <asp:Label ID="lblSites" runat="server"></asp:Label>
        </div>
        <table class="tbl" id="tblSites">
            <thead>
                <tr>
                    <th>
                    </th>
                    <th>
                        <asp:Label ID="lblSitesHeaderStatus" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblSitesHeaderTitle" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblSiteHeaderType" runat="server"></asp:Label>
                    </th>
                    <th class="thlast">
                        <asp:Label ID="lblSitesHeaderAddress" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptSites" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="tdIcons">
                                <asp:Button ID="btnSitesAlert" CssClass="btnSitesAlert" CommandName="Alert" runat="server" />
                                <asp:Button ID="btnSitesView" CssClass="btnSitesView" CommandName="View" runat="server" />
                                <asp:Button ID="btnSitesEdit" CssClass="btnSitesEdit" CommandName="Edit" runat="server" />
                                <asp:Button ID="btnSitesDelete" CssClass="btnSitesDelete" CommandName="Delete" runat="server" />
                            </td>
                            <td>
                                <asp:Image ID="imgSitesLoadStatus" style="width:15px;height:15px" runat="server" />
                                <asp:Image ID="imgSitesLiveStatus" style="width:15px;height:15px" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblSitesTitle" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSitesType" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblSitesAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="trAlternating">
                            <td class="tdIcons">
                                <asp:Button ID="btnSitesAlert" CssClass="btnSitesAlert" CommandName="Alert" runat="server" />
                                <asp:Button ID="btnSitesView" CssClass="btnSitesView" CommandName="View" runat="server" />
                                <asp:Button ID="btnSitesEdit" CssClass="btnSitesEdit" CommandName="Edit" runat="server" />
                                <asp:Button ID="btnSitesDelete" CssClass="btnSitesDelete" CommandName="Delete" runat="server" />
                            </td>
                            <td>
                                <asp:Image ID="imgSitesLoadStatus" style="width:15px;height:15px" runat="server" />
                                <asp:Image ID="imgSitesLiveStatus" style="width:15px;height:15px" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblSitesTitle" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSitesType" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblSitesAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <!--*********************************************** -->
    <!--*************** Map *************************** -->
    <!--*********************************************** -->
    <div class="divTitle">
        <asp:Label ID="lblMap" runat="server"></asp:Label>
    </div>
    <div id="divMap">
        <input type="hidden" id="hdnMapPoint" value="<%=Locations%>" />
        <div id="divMapCanvas" class="divMapCanvas divLarge">
        </div>
    </div>
    
        
</asp:Content>
