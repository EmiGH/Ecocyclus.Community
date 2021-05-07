//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile el proyecto de Visual Studio.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "16.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Messages", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Invalida la propiedad CurrentUICulture del subproceso actual para todas las
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
        ///   Busca una cadena traducida similar a Your credential does not hold permissions to access this information..
        /// </summary>
        internal static string AccessDenied {
            get {
                return ResourceManager.GetString("AccessDenied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your account activation process has not been completed due to the following error: [error]&lt;br/&gt;&lt;br/&gt;Please contact our system administrators at: &lt;a href=&quot;mailto:[mail]&quot;&gt;[mail]&lt;/a&gt; for assistance..
        /// </summary>
        internal static string AccountActivationError {
            get {
                return ResourceManager.GetString("AccountActivationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your account registration process has been successfully completed.  Your EcoCyclus account is now active. &lt;br/&gt;&lt;br/&gt; You can proceed to the EcoCyclus &lt;a href=&quot;../Default.aspx&quot;&gt;site&lt;/a&gt; and log in..
        /// </summary>
        internal static string AccountActivationSuccessful {
            get {
                return ResourceManager.GetString("AccountActivationSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your password recovery process has not been completed due to the following error: [error]&lt;br/&gt;&lt;br/&gt;Please contact our system administrators at: &lt;a href=&quot;mailto:[mail]&quot;&gt;[mail]&lt;/a&gt; for assistance..
        /// </summary>
        internal static string AccountRecoveryError {
            get {
                return ResourceManager.GetString("AccountRecoveryError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please enter your EcoCyclus account email address in order to recover your password..
        /// </summary>
        internal static string AccountRecoveryMessage {
            get {
                return ResourceManager.GetString("AccountRecoveryMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The recovery process has been successfully started.  Please check your email to reset your password..
        /// </summary>
        internal static string AccountRecoverySuccessful {
            get {
                return ResourceManager.GetString("AccountRecoverySuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The email addresses do not match.
        /// </summary>
        internal static string AccountRegistrationEmailNotMatch {
            get {
                return ResourceManager.GetString("AccountRegistrationEmailNotMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a An error has occurred: [error] &lt;br/&gt; &lt;br/&gt;&lt;a href=&quot;mailto:[mail]&quot;&gt;[mail]&lt;/a&gt; for assistance..
        /// </summary>
        internal static string AccountRegistrationError {
            get {
                return ResourceManager.GetString("AccountRegistrationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please enter a valid email address.
        /// </summary>
        internal static string AccountRegistrationInvalidEmail {
            get {
                return ResourceManager.GetString("AccountRegistrationInvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The passwords do not match.
        /// </summary>
        internal static string AccountRegistrationPasswordNotMatch {
            get {
                return ResourceManager.GetString("AccountRegistrationPasswordNotMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Plase enter your organization name.
        /// </summary>
        internal static string AccountRegistrationRequiredCompanyName {
            get {
                return ResourceManager.GetString("AccountRegistrationRequiredCompanyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please enter a valid email address.
        /// </summary>
        internal static string AccountRegistrationRequiredEmail {
            get {
                return ResourceManager.GetString("AccountRegistrationRequiredEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please retype the email address.
        /// </summary>
        internal static string AccountRegistrationRequiredEmailRetype {
            get {
                return ResourceManager.GetString("AccountRegistrationRequiredEmailRetype", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please enter your firstname.
        /// </summary>
        internal static string AccountRegistrationRequiredFirstname {
            get {
                return ResourceManager.GetString("AccountRegistrationRequiredFirstname", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please enter your lastname.
        /// </summary>
        internal static string AccountRegistrationRequiredLastname {
            get {
                return ResourceManager.GetString("AccountRegistrationRequiredLastname", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please select your organization location.
        /// </summary>
        internal static string AccountRegistrationRequiredLocation {
            get {
                return ResourceManager.GetString("AccountRegistrationRequiredLocation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please select a password.
        /// </summary>
        internal static string AccountRegistrationRequiredPassword {
            get {
                return ResourceManager.GetString("AccountRegistrationRequiredPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please retype the password.
        /// </summary>
        internal static string AccountRegistrationRequiredPasswordRetype {
            get {
                return ResourceManager.GetString("AccountRegistrationRequiredPasswordRetype", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your account registration process has been successfully started.  Please check your email to activate your EcoCyclus account..
        /// </summary>
        internal static string AccountRegistrationSuccessful {
            get {
                return ResourceManager.GetString("AccountRegistrationSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your password reset process has not been completed due to the following error: [error]&lt;br/&gt;&lt;br/&gt;Please contact our system administrators at: &lt;a href=&quot;mailto:[mail]&quot;&gt;[mail]&lt;/a&gt; for assistance..
        /// </summary>
        internal static string AccountResetError {
            get {
                return ResourceManager.GetString("AccountResetError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please enter and retype your new password to reset your account..
        /// </summary>
        internal static string AccountResetMessage {
            get {
                return ResourceManager.GetString("AccountResetMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your password reset process has been successfully completed.&lt;br/&gt;&lt;br/&gt; You can proceed to the EcoCyclus &lt;a href=&quot;../Default.aspx&quot;&gt;site&lt;/a&gt; and log in..
        /// </summary>
        internal static string AccountResetSuccessful {
            get {
                return ResourceManager.GetString("AccountResetSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Warning! Removing data will delete all information from this period to the end of the serie..
        /// </summary>
        internal static string AlertDeleteLoad {
            get {
                return ResourceManager.GetString("AlertDeleteLoad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Are you sure you want to delete this information?  &lt;br&gt;&lt;br&gt;You should be aware that removing data will also delete all related information and you won\&apos;t be able to undo this action..
        /// </summary>
        internal static string ConfirmDeleteMessage {
            get {
                return ResourceManager.GetString("ConfirmDeleteMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Warning!.
        /// </summary>
        internal static string ConfirmDeleteTitle {
            get {
                return ResourceManager.GetString("ConfirmDeleteTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Are you sure yo want to modify this information?.
        /// </summary>
        internal static string ConfirmModifyMessage {
            get {
                return ResourceManager.GetString("ConfirmModifyMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Atention!.
        /// </summary>
        internal static string ConfirmModifyTitle {
            get {
                return ResourceManager.GetString("ConfirmModifyTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a This web application uses cookies to improve user experience. By using our web application you consent to all cookies in accordance with our Cookie Policy..
        /// </summary>
        internal static string CookieUseWarning {
            get {
                return ResourceManager.GetString("CookieUseWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The information about the country from the selected address is not recognized..
        /// </summary>
        internal static string ErrorCountryNotRecognized {
            get {
                return ResourceManager.GetString("ErrorCountryNotRecognized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a An unexpected error has occurred! &lt;br/&gt;&lt;br/&gt;Please contact our system administrators at: &lt;a href=&quot;mailto:[mail]&quot;&gt;[mail]&lt;/a&gt; for assistance..
        /// </summary>
        internal static string ErrorDetails {
            get {
                return ResourceManager.GetString("ErrorDetails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The uploaded image is too big.
        /// </summary>
        internal static string ErrorImageTooBig {
            get {
                return ResourceManager.GetString("ErrorImageTooBig", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The range of dates is invalid.
        /// </summary>
        internal static string ErrorInvalidDateRange {
            get {
                return ResourceManager.GetString("ErrorInvalidDateRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Error Occurs.
        /// </summary>
        internal static string ErrorTitle {
            get {
                return ResourceManager.GetString("ErrorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a It seems to be your first time at EcoCyclus.&lt;br/&gt;Please follow the &quot;Add Site&quot; link to register your sites..
        /// </summary>
        internal static string FirstTimeGuideApp {
            get {
                return ResourceManager.GetString("FirstTimeGuideApp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a If this is your first meter please follow the &quot;Load Data&quot; link to add values to your meters series..
        /// </summary>
        internal static string FirstTimeGuideMeter {
            get {
                return ResourceManager.GetString("FirstTimeGuideMeter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a If this is your first site please follow the &quot;Add Meter&quot; link to register your meters..
        /// </summary>
        internal static string FirstTimeGuideSite {
            get {
                return ResourceManager.GetString("FirstTimeGuideSite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Need help?.
        /// </summary>
        internal static string FirstTimeNeedHelp {
            get {
                return ResourceManager.GetString("FirstTimeNeedHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Welcome [user]! First time?.
        /// </summary>
        internal static string FirstTimeTitle {
            get {
                return ResourceManager.GetString("FirstTimeTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a There was an authentication error in the login process.
        /// </summary>
        internal static string LoginAuthenticationError {
            get {
                return ResourceManager.GetString("LoginAuthenticationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The authentication process has failed.
        /// </summary>
        internal static string LoginAuthenticationFail {
            get {
                return ResourceManager.GetString("LoginAuthenticationFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Address Unrecognized.
        /// </summary>
        internal static string MapAddressUnknown {
            get {
                return ResourceManager.GetString("MapAddressUnknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Geocode was not successful for the following reason:.
        /// </summary>
        internal static string MapGeocoderError {
            get {
                return ResourceManager.GetString("MapGeocoderError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Coordenates Selector.
        /// </summary>
        internal static string MapPickupTitle {
            get {
                return ResourceManager.GetString("MapPickupTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Your payment has been successfully processed..
        /// </summary>
        internal static string PaymentSuccessful {
            get {
                return ResourceManager.GetString("PaymentSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The payment could not be resolved.  Please review your information or contact us for further assisstance..
        /// </summary>
        internal static string PaypalError {
            get {
                return ResourceManager.GetString("PaypalError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Information has not been saved due to the following error:&lt;br/&gt;&lt;br/&gt;[error]&lt;br/&gt;&lt;br/&gt;Please contact our system administrators at: &lt;a href=&quot;mailto:[mail]&quot;&gt;[mail]&lt;/a&gt; for assistance..
        /// </summary>
        internal static string StandardError {
            get {
                return ResourceManager.GetString("StandardError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The information has been successfully saved..
        /// </summary>
        internal static string StandardSuccessful {
            get {
                return ResourceManager.GetString("StandardSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please complete all mandatory fields marked with an asterix (*) and verify the type of each data. .
        /// </summary>
        internal static string SummaryCheck {
            get {
                return ResourceManager.GetString("SummaryCheck", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Warning! An error has ocurred.
        /// </summary>
        internal static string SummaryError {
            get {
                return ResourceManager.GetString("SummaryError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a *.
        /// </summary>
        internal static string SummaryErrorCharacter {
            get {
                return ResourceManager.GetString("SummaryErrorCharacter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The information is unsaved!  Are you sure you want to continue?.
        /// </summary>
        internal static string TranslationNotSaved {
            get {
                return ResourceManager.GetString("TranslationNotSaved", resourceCulture);
            }
        }
    }
}
