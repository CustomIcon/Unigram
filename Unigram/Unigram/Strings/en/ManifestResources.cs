//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// --------------------------------------------------------------------------------------------------
// <auto-generatedInfo>
// 	This code was generated by ResW File Code Generator (http://bit.ly/reswcodegen)
// 	ResW File Code Generator was written by Christian Resma Helle
// 	and is under GNU General Public License version 2 (GPLv2)
// 
// 	This code contains a helper class exposing property representations
// 	of the string resources defined in the specified .ResW file
// 
// 	Generated: 11/11/2017 16:00:03
// </auto-generatedInfo>
// --------------------------------------------------------------------------------------------------
namespace Unigram.Strings
{
    using Windows.ApplicationModel.Resources;
    
    
    public sealed partial class ManifestResources
    {
        
        private static ResourceLoader resourceLoader;
        
        static ManifestResources()
        {
            string executingAssemblyName;
            executingAssemblyName = Windows.UI.Xaml.Application.Current.GetType().AssemblyQualifiedName;
            string[] executingAssemblySplit;
            executingAssemblySplit = executingAssemblyName.Split(',');
            executingAssemblyName = executingAssemblySplit[1];
            string currentAssemblyName;
            currentAssemblyName = typeof(ManifestResources).AssemblyQualifiedName;
            string[] currentAssemblySplit;
            currentAssemblySplit = currentAssemblyName.Split(',');
            currentAssemblyName = currentAssemblySplit[1];
            if (executingAssemblyName.Equals(currentAssemblyName))
            {
                resourceLoader = ResourceLoader.GetForCurrentView("ManifestResources");
            }
            else
            {
                resourceLoader = ResourceLoader.GetForCurrentView(currentAssemblyName + "/ManifestResources");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Unigram"
        /// </summary>
        public static string ApplicationDescription
        {
            get
            {
                return resourceLoader.GetString("ApplicationDescription");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Unigram (Alpha)"
        /// </summary>
        public static string ApplicationDisplayName
        {
            get
            {
                return resourceLoader.GetString("ApplicationDisplayName");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Unigram"
        /// </summary>
        public static string PackageDisplayName
        {
            get
            {
                return resourceLoader.GetString("PackageDisplayName");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Unigram, Inc."
        /// </summary>
        public static string PublisherDisplayName
        {
            get
            {
                return resourceLoader.GetString("PublisherDisplayName");
            }
        }
    }
}
