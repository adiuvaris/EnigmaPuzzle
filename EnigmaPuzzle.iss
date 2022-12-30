[Setup]
AppId={{79a62362-10de-46f6-a40a-6981d214501d}
AppName=Enigma
AppVerName=Enigma 1.0.1
AppPublisher=Adiuvaris
AppPublisherURL=http://www.adiuvaris.ch/
AppSupportURL=http://www.adiuvaris.ch/
AppUpdatesURL=http://www.adiuvaris.ch/
DefaultDirName={pf}\Enigma
DefaultGroupName=Enigma
OutputDir=.\Setup
OutputBaseFilename=EnigmaSetup
SetupIconFile=.\EnigmaPuzzle\Enigma.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "de"; MessagesFile: "compiler:Languages\German.isl"; LicenseFile: ".\EnigmaPuzzle\de\Lizenz.txt"
Name: "en"; MessagesFile: "compiler:Default.isl"; LicenseFile: ".\EnigmaPuzzle\en\License.txt"


[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkedonce

[_ISTool]
EnableISX=true

[Files]
Source: "C:\Program Files (x86)\ISTool\isxdl.dll"; Flags: dontcopy
Source: ".\EnigmaPuzzle\bin\Release\EnigmaPuzzle.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\EnigmaPuzzle\bin\Release\en\EnigmaPuzzle.resources.dll"; DestDir: "{app}\en"; Flags: ignoreversion
Source: ".\EnigmaPuzzle\de\EnigmaPuzzle.pdf"; DestDir: "{app}\de"; Languages: de
Source: ".\EnigmaPuzzle\de\EnigmaPuzzleKurz.pdf"; DestDir: "{app}\de"; Languages: de
Source: ".\EnigmaPuzzle\en\EnigmaPuzzle.pdf"; DestDir: "{app}\en"; Languages: en
Source: ".\EnigmaPuzzle\en\EnigmaPuzzleShort.pdf"; DestDir: "{app}\en"; Languages: en

[Icons]
Name: "{group}\Enigma"; Filename: "{app}\EnigmaPuzzle.exe"; Workingdir: "{localappdata}\EnigmaPuzzle"
Name: "{group}\{cm:UninstallProgram,Enigma}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Enigma"; Filename: "{app}\EnigmaPuzzle.exe"; Workingdir: "{localappdata}\EnigmaPuzzle"; Tasks: desktopicon

[Code]
function GetUninstaller: string;
begin
  if not RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\???_is1', 'UninstallString', Result) then
    Result := '';
end;


function SplitParameters(FileName: string; var Parameters: string):
string;
var
i : integer;
InQuote : boolean;
begin
Result := '';
Parameters := '';
if FileName = '' then exit;
InQuote := false;
i := 1;
while i <= length(FileName) do
begin
if (FileName[i] = '"') then InQuote := not InQuote;
if (FileName[i] = ' ') and (not Inquote) then break;
i := i + 1;
end;
Result := RemoveQuotes(Copy(FileName, 1, i - 1));
Parameters := Copy(FileName, i + 1, length(FileName));
end;



function CopyFileToTemp(FileName: string): boolean;
begin
  Result := FileCopy(FileName, Format('%s\%s', [ExpandConstant('{tmp}'), ExtractFileName(FileName)]), false);
end;

function GetTempName(FileName: string): string;
begin
  Result := Format('%s\%s',[ExpandConstant('{tmp}'), ExtractFileName(FileName)]);
end;



function UninstallOldVersion(const ShowErrorMsg, SilentMode: boolean):
boolean;
var
  Uninstaller,
  UninstallerParams,
  UninstallerDatFile : string;
  ExecResult : integer;
begin
  Result := true;

  Uninstaller := SplitParameters(GetUninstaller, UninstallerParams);

  if SilentMode then
    UninstallerParams := Format('%s /SILENT', [UninstallerParams]);

  if (Uninstaller <> '') and (FileExists(Uninstaller)) then
  begin
    if CopyFileToTemp(Uninstaller) then
    begin

      Exec(
        GetTempName(Uninstaller),
        Format('/SECONDPHASE="%s" %s', [Uninstaller, UninstallerParams]),
        ExpandConstant('{tmp}'),
          SW_SHOWNORMAL,
        ewWaitUntilTerminated,
        ExecResult);

      Result := ExecResult = 0;

      if (not Result) and (ShowErrorMsg) then
        MsgBox(Format('%s (%d)', [SysErrorMessage(ExecResult), ExecResult]),mbError,MB_OK);
      end else
        Result := false;
    end;
  end;
end.



function IsDotNetDetected(version: string; service: cardinal): boolean;
// Indicates whether the specified version and service pack of the .NET Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework version:
//    'v1.1.4322'     .NET Framework 1.1
//    'v2.0.50727'    .NET Framework 2.0
//    'v3.0'          .NET Framework 3.0
//    'v3.5'          .NET Framework 3.5
//    'v4\Client'     .NET Framework 4.0 Client Profile
//    'v4\Full'       .NET Framework 4.0 Full Installation
//
// service -- Specify any non-negative integer for the required service pack level:
//    0               No service packs required
//    1, 2, etc.      Service pack 1, 2, etc. required
var
    key: string;
    install, serviceCount: cardinal;
    success: boolean;
begin
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;
    // .NET 3.0 uses value InstallSuccess in subkey Setup
    if Pos('v3.0', version) = 1 then begin
        success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
    end else begin
        success := RegQueryDWordValue(HKLM, key, 'Install', install);
    end;
    // .NET 4.0 uses value Servicing instead of SP
    if Pos('v4', version) = 1 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
    end else begin
        success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
    end;
    result := success and (install = 1) and (serviceCount >= service);
end;

function InitializeSetup(): Boolean;
begin
    UninstallOldVersion("Fehler", false);
  
    if not IsDotNetDetected('v4\Client', 0) then begin
        MsgBox('EnigmaPuzzle requires Microsoft .NET Framework 4.0 Client Profile.'#13#13
            'Please use Windows Update to install this version,'#13
            'and then re-run the setup program.', mbInformation, MB_OK);
        result := false;
    end else
        result := true;
        
end;

