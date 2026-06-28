# Microsoft Developer Studio Project File - Name="LookFont" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** 編集しないでください **

# TARGTYPE "Win32 (x86) Application" 0x0101

CFG=LookFont - Win32 Debug
!MESSAGE これは有効なﾒｲｸﾌｧｲﾙではありません。 このﾌﾟﾛｼﾞｪｸﾄをﾋﾞﾙﾄﾞするためには NMAKE を使用してください。
!MESSAGE [ﾒｲｸﾌｧｲﾙのｴｸｽﾎﾟｰﾄ] ｺﾏﾝﾄﾞを使用して実行してください
!MESSAGE 
!MESSAGE NMAKE /f "LookFont.mak".
!MESSAGE 
!MESSAGE NMAKE の実行時に構成を指定できます
!MESSAGE ｺﾏﾝﾄﾞ ﾗｲﾝ上でﾏｸﾛの設定を定義します。例:
!MESSAGE 
!MESSAGE NMAKE /f "LookFont.mak" CFG="LookFont - Win32 Debug"
!MESSAGE 
!MESSAGE 選択可能なﾋﾞﾙﾄﾞ ﾓｰﾄﾞ:
!MESSAGE 
!MESSAGE "LookFont - Win32 Release" ("Win32 (x86) Application" 用)
!MESSAGE "LookFont - Win32 Debug" ("Win32 (x86) Application" 用)
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "LookFont - Win32 Release"

# PROP BASE Use_MFC 5
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 5
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "Release"
# PROP Intermediate_Dir "Release"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MT /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /Yu"stdafx.h" /FD /c
# ADD CPP /nologo /MT /W3 /GX /O2 /I "..\include" /I "c:\swkey\plus\include" /I "C:\Program Files\Codejock Software\MFC\Xtreme ToolkitPro 2006\Source" /I "C:\SWKey\PLUS\Include" /I "C:\Program Files\HTML Help Workshop\include" /I "..\AutoUpdate\include" /D "NDEBUG" /D "WIN32" /D "_WINDOWS" /D "_MBCS" /FAcs /FD /c
# SUBTRACT CPP /YX /Yc /Yu
# ADD BASE MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x411 /d "NDEBUG"
# ADD RSC /l 0x411 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 /nologo /subsystem:windows /machine:I386
# ADD LINK32 skca32.lib keylib32.lib PSAui.lib version.lib htmlhelp.lib comdlg32.lib /nologo /subsystem:windows /map /machine:I386 /libpath:"..\lib" /libpath:"c:\swkey\plus\library" /libpath:"C:\Program Files\Codejock Software\MFC\Xtreme ToolkitPro 2006\lib\vc60" /libpath:"C:\Program Files\HTML Help Workshop\lib" /libpath:"..\AutoUpdate\lib" /libpath:""C:\SWKey\PLUS\Library"" /libpath:""C:\Program /libpath:""C:\Program

!ELSEIF  "$(CFG)" == "LookFont - Win32 Debug"

# PROP BASE Use_MFC 5
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 5
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "Debug"
# PROP Intermediate_Dir "Debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MTd /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /Yu"stdafx.h" /FD /GZ /c
# ADD CPP /nologo /MTd /W3 /Gm /GX /ZI /Od /I "..\include" /I "c:\swkey\plus\include" /I "C:\Program Files\Codejock Software\MFC\Xtreme ToolkitPro 2006\Source" /I "C:\SWKey\PLUS\Include" /I "C:\Program Files\HTML Help Workshop\include" /I "..\AutoUpdate\include" /D "_DEBUG" /D "WIN32" /D "_WINDOWS" /D "_MBCS" /FAcs /FR /FD /GZ /c
# SUBTRACT CPP /YX /Yc /Yu
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x411 /d "_DEBUG"
# ADD RSC /l 0x411 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 /nologo /subsystem:windows /debug /machine:I386 /pdbtype:sept
# ADD LINK32 skca32.lib keylib32.lib PSAui.lib version.lib htmlhelp.lib comdlg32.lib /nologo /subsystem:windows /map /debug /machine:I386 /out:"C:\Program Files\PLUS S\LookFont\LookFont.exe" /pdbtype:sept /libpath:"..\lib" /libpath:"c:\swkey\plus\library" /libpath:"C:\Program Files\Codejock Software\MFC\Xtreme ToolkitPro 2006\lib\vc60" /libpath:"C:\Program Files\HTML Help Workshop\lib" /libpath:"..\AutoUpdate\lib" /libpath:""C:\SWKey\PLUS\Library"" /libpath:""C:\Program /libpath:""C:\Program

!ENDIF 

# Begin Target

# Name "LookFont - Win32 Release"
# Name "LookFont - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=.\BrowseFile.cpp
# End Source File
# Begin Source File

SOURCE=.\BrowseFolder.cpp
# End Source File
# Begin Source File

SOURCE=.\Cast128.cpp
# End Source File
# Begin Source File

SOURCE=.\CharCode.cpp
# End Source File
# Begin Source File

SOURCE=.\DialogFontInfo.cpp
# End Source File
# Begin Source File

SOURCE=.\DialogUser.cpp
# End Source File
# Begin Source File

SOURCE=.\FileName.cpp
# End Source File
# Begin Source File

SOURCE=.\FindFontDlg.cpp
# End Source File
# Begin Source File

SOURCE=.\FontList.cpp
# End Source File
# Begin Source File

SOURCE=.\LookFont.cpp
# End Source File
# Begin Source File

SOURCE=.\LookFont.rc
# End Source File
# Begin Source File

SOURCE=.\LookFontDoc.cpp
# End Source File
# Begin Source File

SOURCE=.\LookFontView.cpp
# End Source File
# Begin Source File

SOURCE=.\MainFrm.cpp
# End Source File
# Begin Source File

SOURCE=.\NumberEdit.cpp
# End Source File
# Begin Source File

SOURCE=.\ProKeyDlg.cpp
# End Source File
# Begin Source File

SOURCE=.\ProKeyEdit.cpp
# End Source File
# Begin Source File

SOURCE=.\Rtf.cpp
# End Source File
# Begin Source File

SOURCE=.\SepLine.cpp
# End Source File
# Begin Source File

SOURCE=.\SerialKey.cpp
# End Source File
# Begin Source File

SOURCE=.\SettingDlg.cpp
# End Source File
# Begin Source File

SOURCE=.\SettingUpdateDlg.cpp
# End Source File
# Begin Source File

SOURCE=.\StdAfx.cpp
# ADD CPP /Yc"stdafx.h"
# End Source File
# Begin Source File

SOURCE=.\UnlockDlg.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Source File

SOURCE=.\BrowseFile.h
# End Source File
# Begin Source File

SOURCE=.\BrowseFolder.h
# End Source File
# Begin Source File

SOURCE=.\Cast128.h
# End Source File
# Begin Source File

SOURCE=.\CharCode.h
# End Source File
# Begin Source File

SOURCE=.\DialogFontInfo.h
# End Source File
# Begin Source File

SOURCE=.\DialogUser.h
# End Source File
# Begin Source File

SOURCE=.\FileName.h
# End Source File
# Begin Source File

SOURCE=.\FindFontDlg.h
# End Source File
# Begin Source File

SOURCE=.\FontList.h
# End Source File
# Begin Source File

SOURCE=.\LookFont.h
# End Source File
# Begin Source File

SOURCE=.\LookFontDoc.h
# End Source File
# Begin Source File

SOURCE=.\LookFontSetting.h
# End Source File
# Begin Source File

SOURCE=.\LookFontView.h
# End Source File
# Begin Source File

SOURCE=.\MainFrm.h
# End Source File
# Begin Source File

SOURCE=.\NumberEdit.h
# End Source File
# Begin Source File

SOURCE=.\ProKeyDlg.h
# End Source File
# Begin Source File

SOURCE=.\ProKeyEdit.h
# End Source File
# Begin Source File

SOURCE=.\Resource.h
# End Source File
# Begin Source File

SOURCE=.\Rtf.h
# End Source File
# Begin Source File

SOURCE=.\SepLine.h
# End Source File
# Begin Source File

SOURCE=.\SerialKey.h
# End Source File
# Begin Source File

SOURCE=.\SettingDlg.h
# End Source File
# Begin Source File

SOURCE=.\SettingUpdateDlg.h
# End Source File
# Begin Source File

SOURCE=.\StdAfx.h
# End Source File
# Begin Source File

SOURCE=.\UnlockDlg.h
# End Source File
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# Begin Source File

SOURCE=.\res\icon_CAUTION.ico
# End Source File
# Begin Source File

SOURCE=.\res\icon_licence.ico
# End Source File
# Begin Source File

SOURCE=.\res\icon_loo.ico
# End Source File
# Begin Source File

SOURCE=.\res\LookFont.bmp
# End Source File
# Begin Source File

SOURCE=.\res\LookFont.ico
# End Source File
# Begin Source File

SOURCE=.\res\LookFont.rc2
# End Source File
# Begin Source File

SOURCE=.\res\LookFontDoc.ico
# End Source File
# Begin Source File

SOURCE=.\res\sn_key.bmp
# End Source File
# Begin Source File

SOURCE=.\res\tbar_preview.bmp
# End Source File
# Begin Source File

SOURCE=.\res\Toolbar.bmp
# End Source File
# Begin Source File

SOURCE=.\res\toolbar_fontselect.bmp
# End Source File
# Begin Source File

SOURCE=.\res\unlock02.bmp
# End Source File
# End Group
# End Target
# End Project
