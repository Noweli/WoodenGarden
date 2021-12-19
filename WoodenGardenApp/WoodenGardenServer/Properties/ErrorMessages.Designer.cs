﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WoodenGardenServer.Properties {
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
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WoodenGardenServer.Properties.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to Could not add images to garden house. Database error..
        /// </summary>
        internal static string ApiError_GardenHouse_AddImagesFailed {
            get {
                return ResourceManager.GetString("ApiError_GardenHouse_AddImagesFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not add garden house to database..
        /// </summary>
        internal static string ApiError_GardenHouse_FailedToAddToDb {
            get {
                return ResourceManager.GetString("ApiError_GardenHouse_FailedToAddToDb", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not remove images from garden house. Database error..
        /// </summary>
        internal static string ApiError_GardenHouse_FailedToDeleteImagesFromDb {
            get {
                return ResourceManager.GetString("ApiError_GardenHouse_FailedToDeleteImagesFromDb", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not get all house models from api..
        /// </summary>
        internal static string ApiError_GardenHouse_FailedToGetHouses {
            get {
                return ResourceManager.GetString("ApiError_GardenHouse_FailedToGetHouses", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not remove garden house from database..
        /// </summary>
        internal static string ApiError_GardenHouse_FailedToRemoveFromDb {
            get {
                return ResourceManager.GetString("ApiError_GardenHouse_FailedToRemoveFromDb", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not update garden house in database..
        /// </summary>
        internal static string ApiError_GardenHouse_FailedToUpdateInDb {
            get {
                return ResourceManager.GetString("ApiError_GardenHouse_FailedToUpdateInDb", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find images with provided ids..
        /// </summary>
        internal static string ApiError_GardenHouse_ImagesNotFound {
            get {
                return ResourceManager.GetString("ApiError_GardenHouse_ImagesNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id has to be provided to remove garden house..
        /// </summary>
        internal static string ApiError_GardenHouseValidation_HouseIdNotProvided {
            get {
                return ResourceManager.GetString("ApiError_GardenHouseValidation_HouseIdNotProvided", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find garden house with provided id in database..
        /// </summary>
        internal static string ApiError_GardenHouseValidation_HouseWithIdNotFound {
            get {
                return ResourceManager.GetString("ApiError_GardenHouseValidation_HouseWithIdNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not add image to garden house. Garden house id has to be provided..
        /// </summary>
        internal static string ApiError_GardenHouseValidation_IdToAddImageNotProvided {
            get {
                return ResourceManager.GetString("ApiError_GardenHouseValidation_IdToAddImageNotProvided", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not update garden house. Id has to be provided..
        /// </summary>
        internal static string ApiError_GardenHouseValidation_IdToUpdateNotProvided {
            get {
                return ResourceManager.GetString("ApiError_GardenHouseValidation_IdToUpdateNotProvided", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not add image to garden house. Images not provided..
        /// </summary>
        internal static string ApiError_GardenHouseValidation_ImageBase64NotProvided {
            get {
                return ResourceManager.GetString("ApiError_GardenHouseValidation_ImageBase64NotProvided", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not delete images from garden house. Ids not provided..
        /// </summary>
        internal static string ApiError_GardenHouseValidation_ImageIdsToDeleteNotProvided {
            get {
                return ResourceManager.GetString("ApiError_GardenHouseValidation_ImageIdsToDeleteNotProvided", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not update garden house. Provide either new name or new description..
        /// </summary>
        internal static string ApiError_GardenHouseValidation_NameAndDescriptionToUpdateEmpty {
            get {
                return ResourceManager.GetString("ApiError_GardenHouseValidation_NameAndDescriptionToUpdateEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name for garden house has to be provided..
        /// </summary>
        internal static string ApiError_GardenHouseValidation_NameIsEmpty {
            get {
                return ResourceManager.GetString("ApiError_GardenHouseValidation_NameIsEmpty", resourceCulture);
            }
        }
    }
}
