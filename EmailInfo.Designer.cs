﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScrumProject {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class EmailInfo {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EmailInfo() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ScrumProject.EmailInfo", typeof(EmailInfo).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [Email norification].
        /// </summary>
        public static string EMAIL_SUBJECT_DEFAULT {
            get {
                return ResourceManager.GetString("EMAIL_SUBJECT_DEFAULT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ScrumstersOru@gmail.com.
        /// </summary>
        public static string FROM_EMAIL_ACCOUNT {
            get {
                return ResourceManager.GetString("FROM_EMAIL_ACCOUNT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to scrumstersoru123.
        /// </summary>
        public static string FROM_EMAIL_PASSWORD {
            get {
                return ResourceManager.GetString("FROM_EMAIL_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to smtp.gmail.com.
        /// </summary>
        public static string SMTP_HOST_GMAIL {
            get {
                return ResourceManager.GetString("SMTP_HOST_GMAIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 587.
        /// </summary>
        public static string SMTP_PORT_GMAIL {
            get {
                return ResourceManager.GetString("SMTP_PORT_GMAIL", resourceCulture);
            }
        }
    }
}
