﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmuCPU {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class HelpData {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal HelpData() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EmuCPU.HelpData", typeof(HelpData).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt; 
        ///
        ///&lt;!-- Главная страница --&gt;	
        ///&lt;Page Title=&quot;Главная&quot; Address=&quot;&quot;&gt;
        ///	&lt;p Size=&quot;16&quot; Color=&quot;DarkGreen&quot; Style=&quot;Bold&quot;&gt;EmuCPU&lt;/p&gt;
        ///	&lt;p&gt;Данная программа является симулятором вымышленного процессора и была разработана для изучения основ программирования на языке ассемблера.&lt;/p&gt;
        ///	
        ///	&lt;p Size=&quot;12&quot; Color=&quot;DimGray&quot; Style=&quot;Bold&quot;&gt;Содержание&lt;/p&gt;
        ///	&lt;Links&gt;
        ///		&lt;a To=&quot;/syntax&quot;&gt;Основы синтаксиса&lt;/a&gt;
        ///		&lt;a To=&quot;/calls&quot;&gt;Инструкции&lt;/a&gt;
        ///		&lt;a To=&quot;/regs&quot;&gt;Регистры&lt;/a&gt;
        ///		&lt;a To=&quot;/memory&quot;&gt;Память&lt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Content {
            get {
                return ResourceManager.GetString("Content", resourceCulture);
            }
        }
    }
}