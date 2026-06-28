; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CProKeyDlg
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "lookfont.h"
LastPage=0

ClassCount=11
Class1=CLookFontApp
Class2=CAboutDlg
Class3=CLookFontDoc
Class4=CLookFontView
Class5=CMainFrame
Class6=CSettingDlg

ResourceCount=9
Resource1=IDD_DIALOG_TEXTCTRL
Resource2=IDR_TOOLBAR_FONTSELECT
Resource3=IDR_MENU_RCLICK
Resource4=IDD_DIALOG_SNKEY
Resource5=IDD_ABOUTBOX
Class7=CSettingUpdateDlg
Resource6=IDD_DIALOG_UNLOCK
Class8=CLicenceDlg
Resource7=IDR_MAINFRAME
Class9=CProKeyDlg
Class10=CUnlockDlg
Resource8=IDD_DIALOG_SETTING_UPDATE
Class11=CFindFontDlg
Resource9=IDD_DIALOG_FIND

[CLS:CLookFontApp]
Type=0
BaseClass=CWinApp
HeaderFile=LookFont.h
ImplementationFile=LookFont.cpp
LastObject=ID_MENU_SUPPORT
Filter=N
VirtualFilter=AC

[CLS:CAboutDlg]
Type=0
BaseClass=CDialog
HeaderFile=LookFont.cpp
ImplementationFile=LookFont.cpp
LastObject=CAboutDlg
Filter=D
VirtualFilter=dWC

[CLS:CLookFontDoc]
Type=0
BaseClass=CDocument
HeaderFile=LookFontDoc.h
ImplementationFile=LookFontDoc.cpp

[CLS:CLookFontView]
Type=0
BaseClass=CView
HeaderFile=LookFontView.h
ImplementationFile=LookFontView.cpp
LastObject=CLookFontView
Filter=C
VirtualFilter=VWC

[CLS:CMainFrame]
Type=0
BaseClass=CFrameWnd
HeaderFile=MainFrm.h
ImplementationFile=MainFrm.cpp
Filter=T
VirtualFilter=fWC
LastObject=CMainFrame

[CLS:CSettingDlg]
Type=0
BaseClass=CPropertySheet
HeaderFile=SettingDlg.h
ImplementationFile=SettingDlg.cpp
Filter=W
VirtualFilter=hWC
LastObject=CSettingDlg

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=11
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308992
Control3=IDC_STATIC,static,1342308864
Control4=IDOK,button,1342373889
Control5=ID_FILE_VERSION,static,1342308864
Control6=IDC_STATIC_URL,static,1342308864
Control7=IDC_STATIC,static,1342181381
Control8=IDC_STATIC,static,1342308864
Control9=IDC_STATIC_LICENCE_INFO,static,1342308864
Control10=IDC_STATIC,static,1342179331
Control11=IDC_STATIC_ACT,static,1342308865

[TB:IDR_MAINFRAME]
Type=1
Class=?
Command1=ID_EDIT_COPY
Command2=ID_EDIT_PASTE
Command3=ID_MENU_TMPFONT_INSTALL
Command4=ID_MENU_TMPFONT_UNINSTALL
CommandCount=4

[TB:IDR_TOOLBAR_FONTSELECT]
Type=1
Class=?
Command1=ID_MENU_SEL_LANG_EN
Command2=ID_MENU_SEL_LANG_JA
Command3=ID_MENU_SEL_LANG_ALL
Command4=ID_MENU_SEL_FONT_TMP
Command5=ID_MENU_SEL_FONT_ORG
CommandCount=5

[MNU:IDR_MAINFRAME]
Type=1
Class=CLookFontView
Command1=ID_MENU_SETTING
Command2=ID_MENU_UPDATE_CHECK
Command3=ID_APP_EXIT
Command4=ID_EDIT_COPY
Command5=ID_MENU_COPYFONTFACE
Command6=ID_EDIT_PASTE
Command7=ID_MENU_TMPFONT_INSTALL
Command8=ID_MENU_TMPFONT_FIND
Command9=ID_MENU_TMPFONT_UNINSTALL
Command10=ID_VIEW_TOOLBAR
Command11=ID_VIEW_FONTSELECTBAR
Command12=ID_VIEW_TEXTCTRLBAR
Command13=ID_HELP
Command14=ID_APP_ABOUT
Command15=ID_MENU_PLUSS
Command16=ID_MENU_SUPPORT
CommandCount=16

[MNU:IDR_MENU_RCLICK]
Type=1
Class=?
Command1=ID_EDIT_COPY
Command2=ID_MENU_COPYFONTFACE
CommandCount=2

[ACL:IDR_MAINFRAME]
Type=1
Class=?
Command1=ID_EDIT_COPY
Command2=ID_EDIT_PASTE
Command3=ID_NEXT_PANE
Command4=ID_PREV_PANE
CommandCount=4

[DLG:IDD_DIALOG_TEXTCTRL]
Type=1
Class=?
ControlCount=2
Control1=IDC_EDIT_DISPTEXT,edit,1350631552
Control2=IDC_COMBO_FONTSIZE,combobox,1344339970

[DLG:IDD_DIALOG_SETTING_UPDATE]
Type=1
Class=CSettingUpdateDlg
ControlCount=2
Control1=IDC_CHECK_AUTOUPDATE,button,1342242819
Control2=IDC_STATIC,static,1342308352

[CLS:CSettingUpdateDlg]
Type=0
HeaderFile=SettingUpdateDlg.h
ImplementationFile=SettingUpdateDlg.cpp
BaseClass=CPropertyPage
Filter=D
VirtualFilter=idWC
LastObject=IDC_CHECK_ENABLE_AUTOUPDATE

[CLS:CLicenceDlg]
Type=0
HeaderFile=LicenceDlg.h
ImplementationFile=LicenceDlg.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=CLicenceDlg

[DLG:IDD_DIALOG_SNKEY]
Type=1
Class=CProKeyDlg
ControlCount=23
Control1=IDC_STATIC,button,1342177287
Control2=IDC_EDIT_PROKEY1,edit,1350631552
Control3=IDC_EDIT_PROKEY2,edit,1350631552
Control4=IDC_EDIT_PROKEY3,edit,1350631552
Control5=IDC_EDIT_PROKEY4,edit,1350631552
Control6=IDC_EDIT_PROKEY5,edit,1350631552
Control7=IDC_BUTTON_ACT1,button,1342251008
Control8=IDC_BUTTON_ACT2,button,1342251008
Control9=IDC_BUTTON_ACT3,button,1342251008
Control10=IDCANCEL,button,1342242816
Control11=IDOK,button,1208025089
Control12=IDC_STATIC,static,1342179342
Control13=IDC_STATIC,static,1342308352
Control14=IDC_STATIC,static,1342308865
Control15=IDC_STATIC,static,1342308865
Control16=IDC_STATIC,static,1342308865
Control17=IDC_STATIC,static,1342308865
Control18=IDC_STATIC,static,1342308352
Control19=IDC_STATIC,static,1342177795
Control20=IDC_STATIC,static,1342181381
Control21=IDC_STATIC_LEAVE1,static,1342308864
Control22=IDC_STATIC_LIMITDAYS,static,1342308866
Control23=IDC_STATIC_LEAVE2,static,1342308864

[CLS:CProKeyDlg]
Type=0
HeaderFile=ProKeyDlg.h
ImplementationFile=ProKeyDlg.cpp
BaseClass=CDialog
Filter=D
LastObject=IDC_EDIT_PROKEY1
VirtualFilter=dWC

[DLG:IDD_DIALOG_UNLOCK]
Type=1
Class=CUnlockDlg
ControlCount=16
Control1=IDOK,button,1476460545
Control2=IDCANCEL,button,1342242816
Control3=IDC_STATIC,static,1342177294
Control4=IDC_STATIC,static,1342308352
Control5=IDC_STATIC,static,1342181381
Control6=IDC_STATIC,static,1342308864
Control7=IDC_EDIT_USER_CODE_1,edit,1350633600
Control8=IDC_STATIC,static,1342308864
Control9=IDC_EDIT_USER_CODE_2,edit,1350633600
Control10=IDC_STATIC,static,1342308864
Control11=IDC_STATIC,static,1342308864
Control12=IDC_EDIT_REG_KEY_1,edit,1350631552
Control13=IDC_EDIT_REG_KEY_2,edit,1350631552
Control14=IDC_BUTTON_GETWEB,button,1342251008
Control15=IDC_BUTTON_FAX,button,1342251008
Control16=IDC_BUTTON_EMAIL,button,1342251008

[CLS:CUnlockDlg]
Type=0
HeaderFile=UnlockDlg.h
ImplementationFile=UnlockDlg.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=ID_MENU_PLUSS

[DLG:IDD_DIALOG_FIND]
Type=1
Class=CFindFontDlg
ControlCount=13
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_BUTTON_SEARCHPATH,button,1342242816
Control4=IDC_EDIT_FINDPATH,edit,1350631552
Control5=IDC_STATIC,button,1342177287
Control6=IDC_CHECK_EXT1,button,1342242819
Control7=IDC_CHECK_EXT2,button,1342242819
Control8=IDC_CHECK_EXT3,button,1342242819
Control9=IDC_CHECK_EXT4,button,1342242819
Control10=IDC_CHECK_EXT5,button,1342242819
Control11=IDC_CHECK_EXT6,button,1342242819
Control12=IDC_EDIT_EXTETC,edit,1350631552
Control13=IDC_STATIC,static,1342181376

[CLS:CFindFontDlg]
Type=0
HeaderFile=FindFontDlg.h
ImplementationFile=FindFontDlg.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=CFindFontDlg

