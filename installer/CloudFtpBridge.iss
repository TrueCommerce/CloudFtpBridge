#define AppCompany "TrueCommerce PSG Engineering"
#define AppCopyright "Copyright (c) 2017, TrueCommerce PSG Engineering"
#define AppName "Cloud FTP Bridge"
#define AppServiceName "TcCloudFtpBridge"
#define AppVersion "1.0.1"
#define AppVersionStrict "1.0.1.0"

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
DisableProgramGroupPage=auto
DefaultGroupName={#AppCompany}
LicenseFile=userdocs:GitHub\CloudFtpBridge\LICENSE

[Files]
Source: "..\src\Tc.Psg.CloudFtpBridge\bin\Release\FluentFTP.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge\bin\Release\Serilog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge\bin\Release\Tc.Psg.CloudFtpBridge.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.ConfigManager\bin\Release\ConfigManager.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.Service\bin\Release\Serilog.Sinks.File.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.Service\bin\Release\Serilog.Sinks.RollingFile.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\Tc.Psg.CloudFtpBridge.Service\bin\Release\Tc.Psg.CloudFtpBridge.Service.exe"; DestDir: "{app}"; Flags: ignoreversion

[Run]
Filename: "{app}\ConfigManager.exe"; Flags: waituntilterminated; StatusMsg: "Waiting for Config Manager..."; AfterInstall: RegisterAndStartCloudFtpBridgeService

[UninstallRun]
Filename: "{sys}\sc.exe"; Parameters: "delete {#AppServiceName}"

[Dirs]
Name: "{app}\Log"; Flags: uninsalwaysuninstall; Permissions: everyone-full

[Icons]
Name: "{group}\Configure Cloud FTP Bridge"; Filename: "{app}\ConfigManager.exe"

[Code]
(* Global Variables *)
var
  CustomProgressPage: TOutputProgressWizardPage;

(* Service Management *)
procedure RegisterCloudFtpBridgeService();
var
  ExitCode: Integer;
begin
  CustomProgressPage.SetText('Registering Cloud FTP Bridge service...', '');
  CustomProgressPage.Show();

  Exec(ExpandConstant('{sys}\sc.exe'), ExpandConstant('create {#AppServiceName} binPath="{app}\Tc.Psg.CloudFtpBridge.Service.exe" displayName="{#AppName}" start="auto"'), '', SW_HIDE, ewWaitUntilTerminated, ExitCode);
  
  CustomProgressPage.Hide();
end;

procedure StartCloudFtpBridgeService();
var
  ExitCode: Integer;
begin
  CustomProgressPage.SetText('Starting Cloud FTP Bridge service...', '');
  CustomProgressPage.Show();

  Exec(ExpandConstant('{sys}\sc.exe'), 'start {#AppServiceName}', '', SW_HIDE, ewWaitUntilTerminated, ExitCode);
  
  CustomProgressPage.Hide();
end;

procedure StopCloudFtpBridgeService();
var
  ExitCode: Integer;
begin
  CustomProgressPage.SetText('Stopping Cloud FTP Bridge service...', '');
  CustomProgressPage.Show();

  Exec(ExpandConstant('{sys}\sc.exe'), 'stop {#AppServiceName}', '', SW_HIDE, ewWaitUntilTerminated, ExitCode);
  
  CustomProgressPage.Hide();
end;

procedure RegisterAndStartCloudFtpBridgeService();
begin
  RegisterCloudFtpBridgeService();
  StartCloudFtpBridgeService();
end;

(* Custom Page Initializers *)
procedure InitializeCustomProgressPage();
var
  Page: TOutputProgressWizardPage;
begin
  Page := CreateOutputProgressPage('Please Wait...', '');
  CustomProgressPage := Page;
end;

(* InnoSetup Event Functions *)
procedure InitializeWizard();
begin
  InitializeCustomProgressPage();
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
begin
  StopCloudFtpBridgeService();
end;
