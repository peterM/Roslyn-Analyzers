﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MalikP.Analyzers.AsyncMethodAnalyzer {
    using System;
    using System.Reflection;
    
    
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MalikP.Analyzers.AsyncMethodAnalyzer.Resources", typeof(Resources).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to Method returning &apos;Task&apos; or &apos;Task&lt;T&gt;&apos; should contain &apos;CancellationToken&apos; parameter. Here is possibility to add &apos;CancellationToken&apos; from scope..
        /// </summary>
        internal static string CancellationTokenParameterReusePossibility_Task_Invocation_Analyzer_Description {
            get {
                return ResourceManager.GetString("CancellationTokenParameterReusePossibility_Task_Invocation_Analyzer_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method &apos;{0}&apos; missing &apos;CancellationToken&apos; parameter. Add and use from scope.
        /// </summary>
        internal static string CancellationTokenParameterReusePossibility_Task_Invocation_Analyzer_MessageFormat {
            get {
                return ResourceManager.GetString("CancellationTokenParameterReusePossibility_Task_Invocation_Analyzer_MessageFormat" +
                        "", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add and use scoped &apos;CancellationToken&apos; in &apos;Task&apos;, &apos;Task&lt;T&gt;&apos; methods (Invocation).
        /// </summary>
        internal static string CancellationTokenParameterReusePossibility_Task_Invocation_Analyzer_Title {
            get {
                return ResourceManager.GetString("CancellationTokenParameterReusePossibility_Task_Invocation_Analyzer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method &apos;{0}&apos; missing &apos;CancellationToken&apos; parameter. Here is possibility to add &apos;CancellationToken&apos; from scope..
        /// </summary>
        internal static string CancellationTokenParameterReusePossibility_Void_Invocation_Analyzer_Description {
            get {
                return ResourceManager.GetString("CancellationTokenParameterReusePossibility_Void_Invocation_Analyzer_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method &apos;{0}&apos; missing &apos;CancellationToken&apos; parameter. Add and use from scope.
        /// </summary>
        internal static string CancellationTokenParameterReusePossibility_Void_Invocation_Analyzer_MessageFormat {
            get {
                return ResourceManager.GetString("CancellationTokenParameterReusePossibility_Void_Invocation_Analyzer_MessageFormat" +
                        "", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add and use scoped &apos;CancellationToken&apos; in &apos;async void&apos; methods (Invocation).
        /// </summary>
        internal static string CancellationTokenParameterReusePossibility_Void_Invocation_Analyzer_Title {
            get {
                return ResourceManager.GetString("CancellationTokenParameterReusePossibility_Void_Invocation_Analyzer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method returning &apos;Task&apos; or &apos;Task&lt;T&gt;&apos; should have &apos;Async&apos; suffix..
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Task_Declaration_Rule_Description {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Task_Declaration_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method &apos;{0}&apos; missing &apos;Async&apos; suffix.
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Task_Declaration_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Task_Declaration_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method returning &apos;Task&apos;, &apos;Task&lt;T&gt;&apos; missing &apos;Async&apos; suffix.
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Task_Declaration_Rule_Title {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Task_Declaration_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method returning &apos;Task&apos; or &apos;Task&lt;T&gt;&apos; should have &apos;Async&apos; suffix..
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Task_Invocation_Rule_Description {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Task_Invocation_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method &apos;{0}&apos; missing &apos;Async&apos; suffix.
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Task_Invocation_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Task_Invocation_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method returning &apos;Task&apos;, &apos;Task&lt;T&gt;&apos; missing &apos;Async&apos; suffix (Invocation).
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Task_Invocation_Rule_Title {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Task_Invocation_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method name should end with &apos;Async&apos; suffix..
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Void_Declaration_Rule_Description {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Void_Declaration_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method &apos;{0}&apos; missing &apos;Async&apos; suffix.
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Void_Declaration_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Void_Declaration_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method missing &apos;Async&apos; suffix.
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Void_Declaration_Rule_Title {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Void_Declaration_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method name should end with &apos;Async&apos; suffix..
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Void_Invocation_Rule_Description {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Void_Invocation_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method &apos;{0}&apos; missing &apos;Async&apos; suffix.
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Void_Invocation_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Void_Invocation_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method missing &apos;Async&apos; suffix (Invocation).
        /// </summary>
        internal static string MethodMissingAsyncSuffix_Void_Invocation_Rule_Title {
            get {
                return ResourceManager.GetString("MethodMissingAsyncSuffix_Void_Invocation_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method returning &apos;Task&apos; or &apos;Task&lt;T&gt;&apos; should contain &apos;CancellationToken&apos; parameter..
        /// </summary>
        internal static string MissingCancellationTokenParameter_Task_Declaration_Rule_Description {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Task_Declaration_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method &apos;{0}&apos; missing &apos;CancellationToken&apos; parameter.
        /// </summary>
        internal static string MissingCancellationTokenParameter_Task_Declaration_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Task_Declaration_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method returning &apos;Task&apos; or &apos;Task&lt;T&gt;&apos; missing &apos;CancellationToken&apos; parameter.
        /// </summary>
        internal static string MissingCancellationTokenParameter_Task_Declaration_Rule_Title {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Task_Declaration_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method returning &apos;Task&apos; or &apos;Task&lt;T&gt;&apos; should contain &apos;CancellationToken&apos; parameter..
        /// </summary>
        internal static string MissingCancellationTokenParameter_Task_Invocation_Rule_Description {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Task_Invocation_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method &apos;{0}&apos; missing &apos;CancellationToken&apos; parameter.
        /// </summary>
        internal static string MissingCancellationTokenParameter_Task_Invocation_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Task_Invocation_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method returning &apos;Task&apos; or &apos;Task&lt;T&gt;&apos; missing &apos;CancellationToken&apos; parameter (Invocation).
        /// </summary>
        internal static string MissingCancellationTokenParameter_Task_Invocation_Rule_Title {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Task_Invocation_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method should contain &apos;CancellationToken&apos; parameter..
        /// </summary>
        internal static string MissingCancellationTokenParameter_Void_Declaration_Rule_Description {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Void_Declaration_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method &apos;{0}&apos; missing &apos;CancellationToken&apos; parameter.
        /// </summary>
        internal static string MissingCancellationTokenParameter_Void_Declaration_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Void_Declaration_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method missing &apos;CancellationToken&apos; parameter.
        /// </summary>
        internal static string MissingCancellationTokenParameter_Void_Declaration_Rule_Title {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Void_Declaration_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method should contain &apos;CancellationToken&apos; parameter..
        /// </summary>
        internal static string MissingCancellationTokenParameter_Void_Invocation_Rule_Description {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Void_Invocation_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method &apos;{0}&apos; missing &apos;CancellationToken&apos; parameter.
        /// </summary>
        internal static string MissingCancellationTokenParameter_Void_Invocation_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Void_Invocation_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Asynchronous &apos;void&apos; method missing &apos;CancellationToken&apos; parameter (Invocation).
        /// </summary>
        internal static string MissingCancellationTokenParameter_Void_Invocation_Rule_Title {
            get {
                return ResourceManager.GetString("MissingCancellationTokenParameter_Void_Invocation_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;CancellationToken&apos; should be named &apos;cancellationToken&apos;..
        /// </summary>
        internal static string WrongCancellationTokenMethodParameterName_Declaration_Rule_Description {
            get {
                return ResourceManager.GetString("WrongCancellationTokenMethodParameterName_Declaration_Rule_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CancellationToken parameter &apos;{0}&apos; name differs from &apos;cancellationToken&apos;.
        /// </summary>
        internal static string WrongCancellationTokenMethodParameterName_Declaration_Rule_MessageFormat {
            get {
                return ResourceManager.GetString("WrongCancellationTokenMethodParameterName_Declaration_Rule_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;CancellationToken&apos; has wrong name.
        /// </summary>
        internal static string WrongCancellationTokenMethodParameterName_Declaration_Rule_Title {
            get {
                return ResourceManager.GetString("WrongCancellationTokenMethodParameterName_Declaration_Rule_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Position of &apos;CancellationToken&apos; parameter is wrong. Parameter &apos;CancellationToken&apos; should be last method parameter..
        /// </summary>
        internal static string WrongCancellationTokenMethodParameterPositionRule_Declaration_Description {
            get {
                return ResourceManager.GetString("WrongCancellationTokenMethodParameterPositionRule_Declaration_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;{0}&apos; position is wrong.
        /// </summary>
        internal static string WrongCancellationTokenMethodParameterPositionRule_Declaration_MessageFormat {
            get {
                return ResourceManager.GetString("WrongCancellationTokenMethodParameterPositionRule_Declaration_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reorder &apos;CancellationToken&apos; as last.
        /// </summary>
        internal static string WrongCancellationTokenMethodParameterPositionRule_Declaration_Title {
            get {
                return ResourceManager.GetString("WrongCancellationTokenMethodParameterPositionRule_Declaration_Title", resourceCulture);
            }
        }
    }
}
