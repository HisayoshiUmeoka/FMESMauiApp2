'////////////////////////////////////////////////////////////////////////////
' Windows ファイアウォールの設定を解除する
'////////////////////////////////////////////////////////////////////////////

Option Explicit
On Error Resume Next

'----------------------------------------------------------------------------
' 対象プログラムの登録名
'----------------------------------------------------------------------------
'
Dim RegistName
RegistName = """" & "LookFontV2" & """"

'----------------------------------------------------------------------------
' 対象プログラムのパス
'----------------------------------------------------------------------------
' CustomActionData にはプログラムのパスが渡されていることを期待する
'----------------------------------------------------------------------------
'
Dim ProgramPath
ProgramPath = """" & Property("CustomActionData") & """"

'----------------------------------------------------------------------------
' OS を判別する
'     4.1: Windows 98SE
'     4.9: Windows Me
'     5.0: Windows 2000
'     5.1: Windows XP
'     5.2: Windows Server 2003
'     6.0: Windows Vista
'----------------------------------------------------------------------------
'
Function GetOSVersion()
	Dim fs
	Dim fileVer
	Dim osVer

	Set fs = CreateObject("Scripting.FileSystemObject")
	fileVer = fs.GetFileVersion(fs.BuildPath(fs.GetSpecialfolder(1).Path, "kernel32.dll"))
	osVer = Mid(fileVer, 1, 3)
	GetOSVersion = CDbl(osVer)
End Function

'----------------------------------------------------------------------------
' ファイアウォールの設定を解除する
'----------------------------------------------------------------------------
'
Dim wshShell
Dim osVersion

Set wshShell = CreateObject("WScript.Shell")
osVersion = GetOSVersion()

Dim commandLine
Dim iCnt
Dim iMax
Dim ProgFile

iMax = 3
'LookFont.exe
'PSAuChecker.exe
'LicBack2.exe

If 5.1 <= osVersion And osVersion < 6 Then
	' XP, Server 2003
	For iCnt = 0 To iMax - 1
		If iCnt = 0 then
			ProgFile = ProgramPath & "LookFont.exe"
		ElseIf iCnt = 1 then
			ProgFile = ProgramPath & "PSAuChecker.exe"
		ElseIf iCnt = 2 then
			ProgFile = ProgramPath & "LicBack2.exe"
		End If
	Next

	commandLine = "netsh firewall delete allowedprogram program="
	commandLine = commandLine & ProgFile
	commandLine = commandLine & " profile=all"

	wshShell.Run commandLine, 0, False

ElseIf 6 <= osVersion Then
	' Vista とみなす(将来の OS は知らない)
	commandLine = "netsh advfirewall firewall delete rule name="
	commandLine = commandLine & RegistName

	wshShell.Run commandLine, 0, False

End If
Set wshShell = Nothing
