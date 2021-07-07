#define AppCompany "TrueCommerce PSG Engineering"
#define AppCopyright "Copyright (c) 2017, TrueCommerce PSG Engineering"
#define AppName "Cloud FTP Bridge"
#define AppServiceName "TcCloudFtpBridge"
#define AppVersion "3.1.2"
#define AppVersionStrict "3.1.2.0"

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
LicenseFile=..\LICENSE

[InstallDelete]
Type: filesandordirs; Name: "{pf}\True Commerce\PSG Engineering\Cloud FTP Bridge\*"

[Files]
Source: "..\src\CloudFtpBridge.BlazorApp\bin\Release\net5.0\publish\*.*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs;

[Run]
Filename: "{sys}\sc.exe"; Parameters: "delete TcCloudFtpBridge"; Flags: waituntilterminated; StatusMsg: "Uninstalling existing service...";
Filename: "{sys}\sc.exe"; Parameters: "create TcCloudFtpBridge start= auto binpath= ""{app}\CloudFtpBridge.BlazorApp.exe"" displayname= ""Cloud FTP Bridge"" "; Flags: waituntilterminated; StatusMsg: "Installing service...";
Filename: "{sys}\sc.exe"; Parameters: "description TcCloudFtpBridge ""Provides file transfer automation intended to be used with TrueCommerce Transaction Manager's Cloud FTP service."" "; Flags: waituntilterminated; StatusMsg: "Setting service description...";
Filename: "{sys}\net.exe"; Parameters: "start TcCloudFtpBridge"; Flags: waituntilterminated; StatusMsg: "Starting service...";

[UninstallDelete]
Type: filesandordirs; Name: "{pf}\True Commerce\PSG Engineering\Cloud FTP Bridge\*"

[UninstallRun]
Filename: "{sys}\net.exe"; Parameters: "stop TcCloudFtpBridge"; Flags: waituntilterminated;
Filename: "{sys}\sc.exe"; Parameters: "delete TcCloudFtpBridge"; Flags: waituntilterminated;

[Icons]
Name: "{group}\Cloud FTP Bridge"; Filename: "http://localhost:5000/"
