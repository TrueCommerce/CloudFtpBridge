#define AppCompany "TrueCommerce PSG Engineering"
#define AppCopyright "Copyright (c) 2017, TrueCommerce PSG Engineering"
#define AppName "Cloud FTP Bridge"
#define AppServiceName "TcCloudFtpBridge"
#define AppVersion "2.1.0"
#define AppVersionStrict "2.1.0.0"

[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
AppCopyright={#AppCopyright}
AppId={{C571D33E-BB2C-4453-8544-35E93924D568}
DefaultDirName={pf}\True Commerce\PSG Engineering\Cloud FTP Bridge
DisableDirPage=yes
AppPublisher={#AppCompany}
AppPublisherURL=https://www.truecommerce.com
VersionInfoVersion={#AppVersionStrict}
VersionInfoTextVersion={#AppVersion}
VersionInfoProductName={#AppName}
VersionInfoProductVersion={#AppVersionStrict}
VersionInfoProductTextVersion={#AppVersion}
AppVerName={#AppName}
AppSupportURL=https://github.com/TrueCommerce/CloudFtpBridge
VersionInfoCompany={#AppCompany}
VersionInfoCopyright={#AppCopyright}
OutputDir=bin
OutputBaseFilename=CloudFtpBridge_{#AppVersion}
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
DefaultGroupName={#AppCompany}
LicenseFile=userdocs:GitHub\CloudFtpBridge\LICENSE

[Files]
Source: "..\src\Tc.Psg.CloudFtpBridge.Service\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.Service\bin\Release\*.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.Service\bin\Release\*.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.UI\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.UI\bin\Release\*.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.UI\bin\Release\*.json"; DestDir: "{app}"; Flags: ignoreversion

[Run]
Filename: "{app}\CloudFtpBridgeService.exe"; Parameters: "uninstall"; Flags: waituntilterminated; StatusMsg: "Uninstalling polling service...";
Filename: "{app}\CloudFtpBridgeService.exe"; Parameters: "install"; Flags: waituntilterminated; StatusMsg: "Installing polling service...";
Filename: "{app}\CloudFtpBridgeService.exe"; Parameters: "start"; Flags: waituntilterminated; StatusMsg: "Starting polling service...";

[UninstallRun]
Filename: "{app}\CloudFtpBridgeService.exe"; Parameters: "stop"; Flags: waituntilterminated;
Filename: "{app}\CloudFtpBridgeService.exe"; Parameters: "uninstall"; Flags: waituntilterminated;

[Icons]
Name: "{group}\Cloud FTP Bridge"; Filename: "{app}\CloudFtpBridgeUI.exe"
