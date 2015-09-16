using Microsoft.Tools.WindowsInstallerXml;
using Microsoft.Tools.WindowsInstallerXml.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessMsi.Msi {
    public class WixDecompiler {
        public string msiPackage;
        private string[] allTableNames = new string[]
            {
                #region Hard Coded Table Names
                "ActionText",
                "AdminExecuteSequence ",
                "AdminUISequence",
                "AdvtExecuteSequence",
                "AdvtUISequence",
                "AppId",
                "AppSearch",
                "BBControl",
                "Billboard",
                "Binary",
                "BindImage",
                "CCPSearch",
                "CheckBox",
                "Class",
                "ComboBox",
                "CompLocator",
                "Complus",
                "Component",
                "Condition",
                "Control",
                "ControlCondition",
                "ControlEvent",
                "CreateFolder",
                "CustomAction",
                "Dialog",
                "Directory",
                "DrLocator",
                "DuplicateFile",
                "Environment",
                "Error",
                "EventMapping",
                "Extension",
                "Feature",
                "FeatureComponents",
                "File",
                "FileSFPCatalog",
                "Font",
                "Icon",
                "IniFile",
                "IniLocator",
                "InstallExecuteSequence",
                "InstallUISequence",
                "IsolatedComponent",
                "LaunchCondition",
                "ListBox",
                "ListView",
                "LockPermissions",
                "Media",
                "MIME",
                "MoveFile",
                "MsiAssembly",
                "MsiAssemblyName",
                "MsiDigitalCertificate",
                "MsiDigitalSignature",
                "MsiEmbeddedChainer",
                "MsiEmbeddedUI",
                "MsiFileHash",
                "MsiLockPermissionsEx Table",
                "MsiPackageCertificate",
                "MsiPatchCertificate",
                "MsiPatchHeaders",
                "MsiPatchMetadata",
                "MsiPatchOldAssemblyName",
                "MsiPatchOldAssemblyFile",
                "MsiPatchSequence",
                "MsiServiceConfig",
                "MsiServiceConfigFailureActions",
                "MsiSFCBypass",
                "ODBCAttribute",
                "ODBCDataSource",
                "ODBCDriver",
                "ODBCSourceAttribute",
                "ODBCTranslator",
                "Patch",
                "PatchPackage",
                "ProgId",
                "Property",
                "PublishComponent",
                "RadioButton",
                "Registry",
                "RegLocator",
                "RemoveFile",
                "RemoveIniFile",
                "RemoveRegistry",
                "ReserveCost",
                "SelfReg",
                "ServiceControl",
                "ServiceInstall",
                "SFPCatalog",
                "Shortcut",
                "Signature",
                "TextStyle",
                "TypeLib",
                "UIText",
                "Verb",
                "_Validation",
                "_Columns",
                "_Streams",
                "_Storages",
                "_Tables",
                "_TransformView Table",
                "Upgrade"
                #endregion
            };
        public WixDecompiler(string msiPackage) {
            this.msiPackage = msiPackage;
        }

        public void Decompile(string decompilePath) {
            try {

                var allTableNames = new string[]
            {
                #region Hard Coded Table Names
                //FYI: This list is from http://msdn.microsoft.com/en-us/library/2k3te2cs%28VS.100%29.aspx
                "ActionText",
                "AdminExecuteSequence ",
                "AdminUISequence",
                "AdvtExecuteSequence",
                "AdvtUISequence",
                "AppId",
                "AppSearch",
                "BBControl",
                "Billboard",
                "Binary",
                "BindImage",
                "CCPSearch",
                "CheckBox",
                "Class",
                "ComboBox",
                "CompLocator",
                "Complus",
                "Component",
                "Condition",
                "Control",
                "ControlCondition",
                "ControlEvent",
                "CreateFolder",
                "CustomAction",
                "Dialog",
                "Directory",
                "DrLocator",
                "DuplicateFile",
                "Environment",
                "Error",
                "EventMapping",
                "Extension",
                "Feature",
                "FeatureComponents",
                "File",
                "FileSFPCatalog",
                "Font",
                "Icon",
                "IniFile",
                "IniLocator",
                "InstallExecuteSequence",
                "InstallUISequence",
                "IsolatedComponent",
                "LaunchCondition",
                "ListBox",
                "ListView",
                "LockPermissions",
                "Media",
                "MIME",
                "MoveFile",
                "MsiAssembly",
                "MsiAssemblyName",
                "MsiDigitalCertificate",
                "MsiDigitalSignature",
                "MsiEmbeddedChainer",
                "MsiEmbeddedUI",
                "MsiFileHash",
                "MsiLockPermissionsEx Table",
                "MsiPackageCertificate",
                "MsiPatchCertificate",
                "MsiPatchHeaders",
                "MsiPatchMetadata",
                "MsiPatchOldAssemblyName",
                "MsiPatchOldAssemblyFile",
                "MsiPatchSequence",
                "MsiServiceConfig",
                "MsiServiceConfigFailureActions",
                "MsiSFCBypass",
                "ODBCAttribute",
                "ODBCDataSource",
                "ODBCDriver",
                "ODBCSourceAttribute",
                "ODBCTranslator",
                "Patch",
                "PatchPackage",
                "ProgId",
                "Property",
                "PublishComponent",
                "RadioButton",
                "Registry",
                "RegLocator",
                "RemoveFile",
                "RemoveIniFile",
                "RemoveRegistry",
                "ReserveCost",
                "SelfReg",
                "ServiceControl",
                "ServiceInstall",
                "SFPCatalog",
                "Shortcut",
                "Signature",
                "TextStyle",
                "TypeLib",
                "UIText",
                "Verb",
                "_Validation",
                "_Columns",
                "_Streams",
                "_Storages",
                "_Tables",
                "_TransformView Table",
                "Upgrade"
                #endregion
            };

                var systemTables = new string[]
			{
				"_Validation",
				"_Columns",
				"_Streams",
				"_Storages",
				"_Tables",
				"_TransformView Table"
			};

                IEnumerable<string> msiTableNames = allTableNames;

                using (var msidb = new Database(msiPackage, OpenDatabase.ReadOnly)) {
                    var query = "SELECT * FROM `_Columns`";
                    using (var msiTable = new ViewWrapper(msidb.OpenExecuteView(query))) {
                        
                        var tableNames = from record in msiTable.Records
                                         select record[0] as string;
                        //NOTE: system tables are not usually in the _Tables table.
                        var tempList = tableNames.ToList();
                        //tempList.AddRange(systemTables);
                        msiTableNames = tempList.ToArray();
                    }

                }
            }

                //var decompiler = new Microsoft.Tools.WindowsInstallerXml.Decompiler(d);
            //decompiler.Decompile(msiPackage, decompilePath);

            catch (Exception ex) {

                var stEx = ex.Message;
            }
        }

    }
}
