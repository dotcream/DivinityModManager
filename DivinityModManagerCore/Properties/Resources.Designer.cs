﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DivinityModManager.Properties {
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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DivinityModManager.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap AddItem {
            get {
                object obj = ResourceManager.GetObject("AddItem", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to                         &lt;node id=&quot;Module&quot;&gt;
        ///                            &lt;attribute id=&quot;UUID&quot; value=&quot;{0}&quot; type=&quot;22&quot; /&gt;
        ///                        &lt;/node&gt;.
        /// </summary>
        public static string ModSettingsModOrderModuleNode {
            get {
                return ResourceManager.GetString("ModSettingsModOrderModuleNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 						&lt;node id=&quot;ModuleShortDesc&quot;&gt;
        ///							&lt;attribute id=&quot;Folder&quot; value=&quot;{0}&quot; type=&quot;30&quot; /&gt;
        ///							&lt;attribute id=&quot;MD5&quot; value=&quot;{1}&quot; type=&quot;23&quot; /&gt;
        ///							&lt;attribute id=&quot;Name&quot; value=&quot;{2}&quot; type=&quot;22&quot; /&gt;
        ///							&lt;attribute id=&quot;UUID&quot; value=&quot;{3}&quot; type=&quot;22&quot; /&gt;
        ///							&lt;attribute id=&quot;Version&quot; value=&quot;{4}&quot; type=&quot;4&quot; /&gt;
        ///						&lt;/node&gt;.
        /// </summary>
        public static string ModSettingsModuleShortDescNode {
            get {
                return ResourceManager.GetString("ModSettingsModuleShortDescNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot; ?&gt;
        ///&lt;save&gt;
        ///    &lt;header version=&quot;2&quot; /&gt;
        ///    &lt;version major=&quot;3&quot; minor=&quot;6&quot; revision=&quot;4&quot; build=&quot;0&quot; /&gt;
        ///    &lt;region id=&quot;ModuleSettings&quot;&gt;
        ///        &lt;node id=&quot;root&quot;&gt;
        ///            &lt;children&gt;
        ///                &lt;node id=&quot;ModOrder&quot;&gt;
        ///                    &lt;children&gt;
        ///{0}					&lt;/children&gt;
        ///                &lt;/node&gt;
        ///                &lt;node id=&quot;Mods&quot;&gt;
        ///                    &lt;children&gt;
        ///{1}					&lt;/children&gt;
        ///                &lt;/node&gt;
        ///            &lt;/children&gt;
        ///        &lt;/node&gt;
        ///    &lt;/region&gt;
        ///&lt;/save [rest of string was truncated]&quot;;.
        /// </summary>
        public static string ModSettingsTemplate {
            get {
                return ResourceManager.GetString("ModSettingsTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap Save {
            get {
                object obj = ResourceManager.GetObject("Save", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap SaveDisabled {
            get {
                object obj = ResourceManager.GetObject("SaveDisabled", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
