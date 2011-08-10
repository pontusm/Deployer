; Script generated by the HM NIS Edit Script Wizard.

SetCompressor lzma

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "Deployer"
!define PRODUCT_VERSION "2.8.0.0"
!define PRODUCT_VERSION_NAME "2.8"
!define PRODUCT_PUBLISHER "Stendahls"
!define PRODUCT_WEB_SITE "http://www.stendahls.se"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\Deployer.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "license_full.txt"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\${PRODUCT_VERSION}\Deployer.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

XPStyle on

RequestExecutionLevel admin

;Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
Name "${PRODUCT_NAME} ${PRODUCT_VERSION_NAME}"
OutFile "DeployerSetup.exe"
InstallDir "$PROGRAMFILES\Deployer"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Function .onInit
;UAC_Elevate:
;	UAC::RunElevated
;	StrCmp 1223 $0 UAC_ElevationAborted ; UAC dialog aborted by user?
;	StrCmp 0 $0 0 UAC_Err ; Error?
;	StrCmp 1 $1 0 UAC_Success ;Are we the real deal or just the wrapper?
;	Quit

;UAC_Err:
;	MessageBox mb_iconstop "Unable to elevate, error $0"
;	Abort

;UAC_ElevationAborted:
	# elevation was aborted, run as normal?
;	MessageBox mb_iconstop "This installer requires admin access, aborting!"
;	Abort

;UAC_Success:
;	StrCmp 1 $3 +4 ;Admin?
;	StrCmp 3 $1 0 UAC_ElevationAborted ;Try again?
;	MessageBox mb_iconstop "This installer requires admin access, try again"
;	goto UAC_Elevate

	; Check if program already installed
	;ReadRegStr $R0 HKLM "${PRODUCT_UNINST_KEY}" "UninstallString"
	;StrCmp $R0 "" done

	;MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION \
	;	"The groovy ${PRODUCT_NAME} is already installed. It must be uninstalled$\n \
	;	before the new version can be installed.$\n$\nClick `OK` to uninstall \
	;	or `Cancel` to abort the installation." \
	;IDOK uninst
	;Abort
 
;Run the uninstaller
;uninst:
	;ClearErrors

	;ExecWait '$R0 _?=$INSTDIR' ;Do not copy the uninstaller to a temp file

	;IfErrors no_remove_uninstaller
		;You can either use Delete /REBOOTOK in the uninstaller or add some code
		;here to remove the uninstaller. Use a registry key to check
		;whether the user has chosen to uninstall. If you are using an uninstaller
		;components page, make sure all sections are uninstalled.
	;no_remove_uninstaller:
 
;done:

FunctionEnd

Function .onInstFailed
;	UAC::Unload		; UAC must be unloaded
FunctionEnd

Function .onInstSuccess
;	UAC::Unload		; UAC must be unloaded
FunctionEnd

; This is used to cleanup before installing. It's used to cleanup VirtualStore on Vista.
;Function CleanupOldFiles
;	Delete "$INSTDIR\AppStart.config"
;FunctionEnd

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite try
  
  ; Execute with user privileges
  ;GetFunctionAddress $0 CleanupOldFiles
  ;UAC::ExecCodeSegment $0

  ;File "AppStart.exe"
  ;File "AppStart.config"
  
  SetOutPath "$INSTDIR\${PRODUCT_VERSION}"
  File "..\Deployer\bin\Release\Deployer.exe"
  File "..\Deployer\bin\Release\DeployerEngine.dll"
  File "..\Deployer\bin\Release\DeployerEngine.XmlSerializers.dll"
  File "..\Deployer\bin\Release\DeployerPluginInterfaces.dll"
  File "..\Deployer\bin\Release\GlacialControls.dll"
  File "..\Deployer\bin\Release\Controls.dll"

  SetOutPath "$INSTDIR\${PRODUCT_VERSION}\Plugins"
  File "..\DeployerPlugins\bin\Release\DeployerPlugins.dll"
  File "..\DeployerPlugins\bin\Release\edtftpnet-1.1.9.dll"
  
  CreateDirectory "$SMPROGRAMS\Deployer"
  ;CreateShortCut "$SMPROGRAMS\Deployer\Deployer.lnk" "$INSTDIR\AppStart.exe"
  ;CreateShortCut "$DESKTOP\Deployer.lnk" "$INSTDIR\AppStart.exe"
  CreateShortCut "$SMPROGRAMS\Deployer\Deployer.lnk" "$INSTDIR\${PRODUCT_VERSION}\Deployer.exe"
  CreateShortCut "$DESKTOP\Deployer.lnk" "$INSTDIR\${PRODUCT_VERSION}\Deployer.exe"
  
  ; back up old file extension
  !define Index "Line${__LINE__}"
	  ReadRegStr $1 HKCR ".deploy" ""
	  StrCmp $1 "" "${Index}-NoBackup"
	    StrCmp $1 "Deployer.DeployFile" "${Index}-NoBackup"
	    WriteRegStr HKCR ".deploy" "backup_val" $1
  "${Index}-NoBackup:"
	  WriteRegStr HKCR ".deploy" "" "Deployer.DeployFile"
	  ReadRegStr $0 HKCR "Deployer.DeployFile" ""
	  StrCmp $0 "" 0 "${Index}-Skip"
		WriteRegStr HKCR "Deployer.DeployFile" "" "Deployer Settings File"
		WriteRegStr HKCR "Deployer.DeployFile\shell" "" "open"
		WriteRegStr HKCR "Deployer.DeployFile\DefaultIcon" "" "$INSTDIR\${PRODUCT_VERSION}\Deployer.exe,0"
  "${Index}-Skip:"
	  WriteRegStr HKCR "Deployer.DeployFile\shell\open\command" "" \
	    '$INSTDIR\${PRODUCT_VERSION}\Deployer.exe "%1"'
	  ;WriteRegStr HKCR "Deployer.DeployFile\shell\edit" "" "Edit Deployer File"
	  ;WriteRegStr HKCR "Deployer.DeployFile\shell\edit\command" "" '$INSTDIR\Appstart.exe "%1"'
	 
	  System::Call 'Shell32::SHChangeNotify(i 0x8000000, i 0, i 0, i 0)'
!undef Index
 
SectionEnd

Section -AdditionalIcons
  CreateShortCut "$SMPROGRAMS\Deployer\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\Deployer.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\Deployer.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
;Restore file extension
!define Index "Line${__LINE__}"
  ReadRegStr $1 HKCR ".deploy" ""
  StrCmp $1 "Deployer.DeployFile" 0 "${Index}-NoOwn" ; only do this if we own it
    ReadRegStr $1 HKCR ".deploy" "backup_val"
    StrCmp $1 "" 0 "${Index}-Restore" ; if backup="" then delete the whole key
      DeleteRegKey HKCR ".deploy"
    Goto "${Index}-NoOwn"
"${Index}-Restore:"
      WriteRegStr HKCR ".deploy" "" $1
      DeleteRegValue HKCR ".deploy" "backup_val"
   
    DeleteRegKey HKCR "Deployer.DeployFile" ;Delete key with association settings
 
    System::Call 'Shell32::SHChangeNotify(i 0x8000000, i 0, i 0, i 0)'
"${Index}-NoOwn:"
!undef Index
  ;rest of script 
 

  Delete /REBOOTOK "$SMPROGRAMS\Deployer\Deployer.lnk"
  Delete /REBOOTOK "$DESKTOP\Deployer.lnk"
  Delete /REBOOTOK "$SMPROGRAMS\Deployer\Uninstall.lnk"

  ; Remove entire program dir
  RMDir "$SMPROGRAMS\Deployer"
  RMDir /r /REBOOTOK "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
  
  IfRebootFlag 0 noreboot
    MessageBox MB_YESNO "Some files could not be automatically removed. They will be removed when you reboot. Do you wish to reboot now?" IDNO noreboot
    Reboot
  noreboot:
SectionEnd
