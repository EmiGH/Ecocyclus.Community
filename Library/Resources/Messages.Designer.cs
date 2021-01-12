﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSI.Library.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CSI.Library.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The token for your account activation is invalid..
        /// </summary>
        internal static string AccountActivationBadTokenError {
            get {
                return ResourceManager.GetString("AccountActivationBadTokenError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The company name is already registered.
        /// </summary>
        internal static string AccountMessageCompanyExists {
            get {
                return ResourceManager.GetString("AccountMessageCompanyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The email address is already registered.
        /// </summary>
        internal static string AccountMessageEmailExists {
            get {
                return ResourceManager.GetString("AccountMessageEmailExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a An e-Greening account has been generated for you.  This is your login information:[n][n]User: [user][n]Password: [password].[n][n]You can proceed to e-Greening and enter the platform at: [&lt;a]http://gsi.siteimpacts.com[a&gt;]http://gsi.siteimpacts.com[/a]..
        /// </summary>
        internal static string AccountNewMailBody {
            get {
                return ResourceManager.GetString("AccountNewMailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a e-Greening:: New Account.
        /// </summary>
        internal static string AccountNewMailSubject {
            get {
                return ResourceManager.GetString("AccountNewMailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your recovery process has been started.[n][n]Please reset your  account password by following this link: [&lt;a]http://ecocyclus.com/Communities/Registration/PasswordReset.aspx?token=[token]&amp;language=[language][a&gt;]&gt;reset password[/a][n][n].
        /// </summary>
        internal static string AccountRecoveryMailBody {
            get {
                return ResourceManager.GetString("AccountRecoveryMailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a e-Greening :: Recovery Process.
        /// </summary>
        internal static string AccountRecoveryMailSubject {
            get {
                return ResourceManager.GetString("AccountRecoveryMailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The company [company] registration process has been started.[n][n].
        /// </summary>
        internal static string AccountRegistrationAdministrationMailBody {
            get {
                return ResourceManager.GetString("AccountRegistrationAdministrationMailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your registration process has been started.[n][n]Please activate your account by following this link: [&lt;a]http://ecocyclus.com/Communities/Registration/RegistrationVerifier.aspx?token=[token]&amp;language=[language][a&gt;]activate account[/a][n][n].
        /// </summary>
        internal static string AccountRegistrationMailBody {
            get {
                return ResourceManager.GetString("AccountRegistrationMailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a e-Greening :: Registration Process.
        /// </summary>
        internal static string AccountRegistrationMailSubject {
            get {
                return ResourceManager.GetString("AccountRegistrationMailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The company [company] has verified the account information.[n][n].
        /// </summary>
        internal static string AccountVerificationAdministrationMailBody {
            get {
                return ResourceManager.GetString("AccountVerificationAdministrationMailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The authentication process has failed..
        /// </summary>
        internal static string AuthenticationError {
            get {
                return ResourceManager.GetString("AuthenticationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a e-Greening :: Contact Request.
        /// </summary>
        internal static string ContactTitle {
            get {
                return ResourceManager.GetString("ContactTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The distance are invalid.  They must be greater than 0..
        /// </summary>
        internal static string DataLoadInvalidDistance {
            get {
                return ResourceManager.GetString("DataLoadInvalidDistance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The dates are invalid.  Start date must be greater than end date..
        /// </summary>
        internal static string DataLoadInvalidPeriod {
            get {
                return ResourceManager.GetString("DataLoadInvalidPeriod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The dates ranges provided are invalid.
        /// </summary>
        internal static string DatesInvalid {
            get {
                return ResourceManager.GetString("DatesInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The meter cannot be saved because its identification already exists..
        /// </summary>
        internal static string DuplicatedMeter {
            get {
                return ResourceManager.GetString("DuplicatedMeter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The information cannot be saved because a unique value already exist..
        /// </summary>
        internal static string DuplicatedRecord {
            get {
                return ResourceManager.GetString("DuplicatedRecord", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The facility cannot be saved because its name or number already exists.  .
        /// </summary>
        internal static string DuplicatedSite {
            get {
                return ResourceManager.GetString("DuplicatedSite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The unit cannot be saved because its name already exists..
        /// </summary>
        internal static string DuplicatedUnit {
            get {
                return ResourceManager.GetString("DuplicatedUnit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a You cannot change the initial date because the meter already has values..
        /// </summary>
        internal static string ErrorCannotChangeInitialDate {
            get {
                return ResourceManager.GetString("ErrorCannotChangeInitialDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a You cannot change magnitude because the meter already has values with previous magnitude..
        /// </summary>
        internal static string ErrorCannotChangeMagnitude {
            get {
                return ResourceManager.GetString("ErrorCannotChangeMagnitude", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Related data exist which prevent removal of this information.
        /// </summary>
        internal static string ErrorCannotDeleteExistingRelationship {
            get {
                return ResourceManager.GetString("ErrorCannotDeleteExistingRelationship", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The last user cannot be removed..
        /// </summary>
        internal static string ErrorCannotDeleteLastUser {
            get {
                return ResourceManager.GetString("ErrorCannotDeleteLastUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Different magnitudes cannot be used for the same data type in the serie..
        /// </summary>
        internal static string ErrorCannotUseDifferentMagnitudes {
            get {
                return ResourceManager.GetString("ErrorCannotUseDifferentMagnitudes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The dates for loading data have to be lesser or equal than today..
        /// </summary>
        internal static string LoadDateViolation {
            get {
                return ResourceManager.GetString("LoadDateViolation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a [n][n]This is an e-Greening platform notification message.  Please do not respond this email..
        /// </summary>
        internal static string MailFooter {
            get {
                return ResourceManager.GetString("MailFooter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The meter cannot modify the measurement unit because it already has values..
        /// </summary>
        internal static string MeterHasValue {
            get {
                return ResourceManager.GetString("MeterHasValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The first date provided does not match the next available date for data load..
        /// </summary>
        internal static string NextDateInvalid {
            get {
                return ResourceManager.GetString("NextDateInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The meter [meter] of site [site] needs to be loaded..
        /// </summary>
        internal static string NotificationSiteMeterNoticeMessage {
            get {
                return ResourceManager.GetString("NotificationSiteMeterNoticeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Meter Load Notice.
        /// </summary>
        internal static string NotificationSiteMeterNoticeTitle {
            get {
                return ResourceManager.GetString("NotificationSiteMeterNoticeTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The load frequency of meter [meter] of facility [site] is overdue..
        /// </summary>
        internal static string NotificationSiteMeterOverdueMessage {
            get {
                return ResourceManager.GetString("NotificationSiteMeterOverdueMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Meter Load Overdue.
        /// </summary>
        internal static string NotificationSiteMeterOverdueTitle {
            get {
                return ResourceManager.GetString("NotificationSiteMeterOverdueTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The targets for [protocol] of the facility [site] on [month] of [year] have been surpassed by [amount] [units]..
        /// </summary>
        internal static string NotificationSiteTargetSurpassedMessage {
            get {
                return ResourceManager.GetString("NotificationSiteTargetSurpassedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Facility Targets Surpassed.
        /// </summary>
        internal static string NotificationSiteTargetSurpassedTitle {
            get {
                return ResourceManager.GetString("NotificationSiteTargetSurpassedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The company [company] has made a payment of [amount] for the period [period] for the facility [site].[n] The transaction id is: [transaction].[n][n].
        /// </summary>
        internal static string PaymentAdministrationMailBody {
            get {
                return ResourceManager.GetString("PaymentAdministrationMailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a e-Greening :: New Payment.
        /// </summary>
        internal static string PaymentAdministrationMailSubject {
            get {
                return ResourceManager.GetString("PaymentAdministrationMailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The company [company] has confirmed the payment for transaction id [transaction].[n][n].
        /// </summary>
        internal static string PaymentConfirmedAdministrationMailBody {
            get {
                return ResourceManager.GetString("PaymentConfirmedAdministrationMailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a e-Greening :: Payment Confirmed.
        /// </summary>
        internal static string PaymentConfirmedAdministrationMailSubject {
            get {
                return ResourceManager.GetString("PaymentConfirmedAdministrationMailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The total amount informed for the payment does not match with the correct payment scale..
        /// </summary>
        internal static string PaymentErrorDifferentAmount {
            get {
                return ResourceManager.GetString("PaymentErrorDifferentAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The dates for the new load period don´t have correct values!.
        ///The dates must be a valid time range and must match with the beginning or the end of the current load period..
        /// </summary>
        internal static string PaymentErrorWrongDates {
            get {
                return ResourceManager.GetString("PaymentErrorWrongDates", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a You do not have permissions to perform this action.  Please contact your platform administrators..
        /// </summary>
        internal static string PermissionDenied {
            get {
                return ResourceManager.GetString("PermissionDenied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a You cannot change access permissions for a company manager.
        /// </summary>
        internal static string PermissionsCannotChangeManager {
            get {
                return ResourceManager.GetString("PermissionsCannotChangeManager", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Reading values of a physical meter have to be equal or greater than the previous one..
        /// </summary>
        internal static string PhysicalMeterInvalidValue {
            get {
                return ResourceManager.GetString("PhysicalMeterInvalidValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a You cannot perform this action because the facility is closed..
        /// </summary>
        internal static string SiteClosed {
            get {
                return ResourceManager.GetString("SiteClosed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The dates for loading data must be valid for the time range enabled for the facility..
        /// </summary>
        internal static string SiteLoadDateViolation {
            get {
                return ResourceManager.GetString("SiteLoadDateViolation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The dates for loading data have to be greater or equal than the facility start date..
        /// </summary>
        internal static string SiteStartDateViolation {
            get {
                return ResourceManager.GetString("SiteStartDateViolation", resourceCulture);
            }
        }
    }
}
