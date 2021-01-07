using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using System.Web;

namespace CSI.WebUI
{
    public class Common
    {
        #region Permissions

        public enum PermissionType
        { CompanyManager, SiteManager, SiteOperator, SiteReader, NoAccess }
        public static PermissionType GetPermissionFromContext(Library.Objects.Users.User currentUser, Library.Objects.Sites.SiteMine currentSite)
        {
            if (currentUser is Library.Objects.Users.UserOperatorMeManager)
                return PermissionType.CompanyManager;

            if ((currentSite.CurrentPermission() == Library.Security.Authority.PermissionTypes.SiteManager))
                return PermissionType.SiteManager;
            if ((currentSite.CurrentPermission() == Library.Security.Authority.PermissionTypes.SiteOperator))
                return PermissionType.SiteOperator;
            if ((currentSite.CurrentPermission() == Library.Security.Authority.PermissionTypes.SiteReader))
                return PermissionType.SiteReader;

            return PermissionType.NoAccess;
        }

        #endregion

        #region Paths and Folders

        public enum eFolders
        { Root, Images, Scripts, Style, Console, Directory, Dashboard, Permissions, EmissionFactors, Meters, Payments, Reports, Targets, Common }
        public static String GetPath(eFolders folder, HttpRequest request)
        {
            String _uri = new UriBuilder(request.Url.Scheme, request.Url.Host, request.Url.Port, !string.IsNullOrEmpty(request.ApplicationPath) ? request.ApplicationPath +"/" : "").ToString();

            switch (folder)
            {
                case eFolders.Root:
                    return _uri;
                case eFolders.Images:
                    return _uri + "Images/";
                case eFolders.Scripts:
                    return  _uri + "Scripts/";
                case eFolders.Style:
                    return  _uri + "Style/";
                case eFolders.Console:
                    return  _uri + "Console/";
                case eFolders.Directory:
                    return  _uri + "Console/Directory/";
                case eFolders.Dashboard:
                    return  _uri + "Console/Dashboard/";
                case eFolders.Permissions:
                    return _uri + "Console/Dashboard/Permissions/";
                case eFolders.EmissionFactors:
                    return _uri + "Console/Dashboard/EmissionFactors/";
                case eFolders.Meters:
                    return _uri + "Console/Dashboard/Meters/";
                case eFolders.Payments:
                    return _uri + "Console/Dashboard/Payments/";
                case eFolders.Reports:
                    return _uri + "Console/Dashboard/Reports/";
                case eFolders.Targets:
                    return _uri + "Console/Dashboard/Targets/";
                case eFolders.Common:
                    return  _uri + "Console/Common/";
                default:
                    return _uri;
            }
        }

        #endregion

        #region Image Manipulation

        public static Pair GetImageSize(byte[] image, int limit)
        {            
            System.IO.MemoryStream ms = new System.IO.MemoryStream(image);
            return GetImageSize((System.Drawing.Bitmap)System.Drawing.Image.FromStream(ms), limit);
        }
        public static Pair GetDefaultImageSize(int limit)
        {
            return GetImageSize((System.Drawing.Bitmap)System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "/Images/NoImagesAvailable.gif"), limit);            
        }
        private static Pair GetImageSize(System.Drawing.Bitmap bmp, int limit)
        {
            Double _width, _height;
            _width = bmp.Width;
            _height = bmp.Height;

            if (limit < _width || limit < _height)
            {
                if (_width > _height)
                {
                    _height = _height * (limit / _width);
                    _width = limit;
                }
                else
                {
                    _width = _width * (limit / _height);
                    _height = limit;
                }
            }
            return new Pair(Convert.ToInt64(_width), Convert.ToInt64(_height));
        }
        public static Int64 GetImageMaxSizeInBytes()
        {
            return 512000;
        }

        #endregion
                
        #region Charts and Protocols

        public enum Protocols 
        { Electricity=1, Fuels=2, Transport=3, Water=4, Waste=5 }

        public enum ChartProtocols
        { Electricity = 1, Fuels = 2, Transport = 3, Water = 4, Waste = 5, CO2 = 6 }

        internal static void ChartBarSerieStyle(Telerik.Charting.ChartSeries serie)
        {
            serie.Appearance.FillStyle.MainColor = System.Drawing.ColorTranslator.FromHtml("#aec94c");
            serie.Appearance.FillStyle.SecondColor = System.Drawing.ColorTranslator.FromHtml("#aec94c");
            serie.Appearance.Border.Color = System.Drawing.ColorTranslator.FromHtml("#aec94c");
            serie.Appearance.LabelAppearance.LabelLocation = Telerik.Charting.Styles.StyleSeriesItemLabel.ItemLabelLocation.Auto;
            serie.Appearance.LabelAppearance.Position.AlignedPosition = Telerik.Charting.Styles.AlignedPositions.None;
        }
        internal static void ChartBarSerieItemStyle(Telerik.Charting.ChartSeriesItem point, Double target)
        {
            if(target <=0)
            {
                //point.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(238, 205, 109);
                //point.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(238, 205, 109);
                //point.Appearance.Border.Color = System.Drawing.Color.FromArgb(238, 205, 109);
                point.Label.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.FromArgb(143, 137, 129);
                
            }
            else if (point.YValue > target)
            {
                point.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(233, 131, 122);
                point.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(233, 131, 122);
                point.Appearance.Border.Color = System.Drawing.Color.FromArgb(233, 131, 122);
                point.Label.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.FromArgb(255,255,255);
            }
            else 
            {
                point.Appearance.FillStyle.MainColor = System.Drawing.ColorTranslator.FromHtml("#aec94c");
                point.Appearance.FillStyle.SecondColor = System.Drawing.ColorTranslator.FromHtml("#aec94c");
                point.Appearance.Border.Color = System.Drawing.ColorTranslator.FromHtml("#aec94c");
                point.Label.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.FromArgb(143, 137, 129);
            }
            
        }
        internal static void ChartPieSerieStyle(Telerik.Web.UI.RadChart pie, Telerik.Charting.ChartSeries serie)
        {
            Telerik.Charting.Palette _palette = new Telerik.Charting.Palette();
            
            _palette.Items.Add(new Telerik.Charting.PaletteItem(System.Drawing.ColorTranslator.FromHtml("#aec94c"), System.Drawing.ColorTranslator.FromHtml("#aec94c")));
            pie.CustomPalettes.Add(_palette);

            
            serie.Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Solid;
            serie.Appearance.Border.Color = pie.Appearance.FillStyle.MainColor;

            serie.Appearance.LabelAppearance.LabelLocation = Telerik.Charting.Styles.StyleSeriesItemLabel.ItemLabelLocation.Auto;
            serie.Appearance.LabelAppearance.Position.AlignedPosition = Telerik.Charting.Styles.AlignedPositions.None;
        }
        internal static void ChartPieSerieItemStyle(Telerik.Charting.ChartSeriesItem point)
        {
            point.Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Solid;
            

        }
        internal static void ChartZoneStyle(Telerik.Charting.ChartMarkedZone zone)
        {
            zone.Appearance.FillStyle.MainColor = System.Drawing.ColorTranslator.FromHtml("#8f8981");
        }

        #endregion

        #region Translations

        public static String GetBooleanTranslation(Boolean label)
        {
            if(label)
                return Resources.Data.Yes;
            return Resources.Data.No;

        }
        public static List<KeyValuePair<String, String>> GetTranslationStructure(String translations)
        {
            List<KeyValuePair<String, String>> _translationsList = new List<KeyValuePair<string, string>>();

            if (translations.Contains('°'))
            {
                String[] _translations = translations.Split('°');
                foreach (String _translation in _translations)
                {
                    String[] _pair = _translation.Split('|');
                    if (_pair[1] != "")
                        _translationsList.Add(new KeyValuePair<string, string>(_pair[0], _pair[1]));
                }
            }

            return _translationsList;
        }

        #endregion

        #region Utilities

        public static Nullable<T> Parse<T>(string input) where T : struct
        {
            //since we cant do a generic type constraint
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Generic Type 'T' must be an Enum");
            }
            if (!string.IsNullOrEmpty(input))
            {
                if (Enum.GetNames(typeof(T)).Any(
                        e => e.Trim().ToUpperInvariant() == input.Trim().ToUpperInvariant()))
                {
                    return (T)Enum.Parse(typeof(T), input, true);
                }
            }
            return null;
        }
        public static Double RoundAndFormat(Double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        #endregion

    }
}