'////////////////////////////////////////////////////////////////////////////
' Windows ファイアウォールの設定を追加する
'////////////////////////////////////////////////////////////////////////////

Option Explicit
'On Error Resume Next

'----------------------------------------------------------------------------
' 対象プログラムの登録名とプログラム名
'----------------------------------------------------------------------------
'
Dim RegistName
RegistName = """" & "LookFontV2" & """"

Dim ProgramName
ProgramName = """" & "LookFontV2" & """"

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
	Dim Fs
	Dim fileVer
	Dim osVer

	Set Fs = CreateObject("Scripting.FileSystemObject")
	fileVer = Fs.GetFileVersion(Fs.BuildPath(Fs.GetSpecialfolder(1).Path, "kernel32.dll"))
	osVer = Mid(fileVer, 1, 3)
	GetOSVersion = CDbl(osVer)
End Function

'----------------------------------------------------------------------------
' Vista でのファイアウォールの設定をするサブルーチン
'----------------------------------------------------------------------------
'
Sub RegistFirewallSettingForVista7(wshShell, registName, programPath2, direction, profile)

	Dim commandLine

	commandLine = "netsh advfirewall firewall add rule action=allow name="
	commandLine = commandLine & registName
	commandLine = commandLine & " program="
	commandLine = commandLine & programPath2
	commandLine = commandLine & " dir="
	commandLine = commandLine & direction
	commandLine = commandLine & " description="
	commandLine = commandLine & programName
	commandLine = commandLine & " profile="
	commandLine = commandLine & profile
	commandLine = commandLine & " localip=any remoteip=any protocol=tcp interfacetype=any"

	wshShell.Run commandLine, 0, False

End Sub

'----------------------------------------------------------------------------
' ファイアウォールの設定をする
'----------------------------------------------------------------------------
'
Dim wshShell
Dim osVersion
Dim iCnt
Dim iMax
Dim ProgFile

iMax = 3
'LookFont.exe
'PSAuChecker.exe
'LicBack2.exe

Set wshShell = CreateObject("WScript.Shell")
osVersion = GetOSVersion()

For iCnt = 0 To iMax - 1
	If iCnt = 0 then
		ProgFile = ProgramPath & "LookFont.exe"
	ElseIf iCnt = 1 then
		ProgFile = ProgramPath & "PSAuChecker.exe"
	ElseIf iCnt = 2 then
		ProgFile = ProgramPath & "LicBack2.exe"
	End If

	If 5.1 <= osVersion And osVersion < 6 Then
		' XP, Server 2003
		Dim commandLine

		commandLine = "netsh firewall add allowedprogram program="
		commandLine = commandLine & ProgFile
		commandLine = commandLine & " name=" & ProgramName
		commandLine = commandLine & " mode=enable scope=all profile=all"

		wshShell.Run commandLine, 0, False

	ElseIf 6 <= osVersion Then
		' Vista とみなす(将来の OS は知らない)

		' NOTE: name, program, dir, description, protocol のみの指定でも OK なのだが、
		'       この場合、profile が private, protocol, domain が一度に登録されてしまい、
		'       profile 別に解除できなくなるため、[Winsows ファイアウォールの設定] - [例外] 
		'       からでは設定を削除できなくなる。簡便のためこれを避ける目的で profile 別に登録している。

		RegistFirewallSettingForVista7 wshShell, RegistName, ProgFile, "in", "public"
		RegistFirewallSettingForVista7 wshShell, RegistName, ProgFile, "in", "private"
		RegistFirewallSettingForVista7 wshShell, RegistName, ProgFile, "in", "domain"

		RegistFirewallSettingForVista7 wshShell, RegistName, ProgFile, "out", "public"
		RegistFirewallSettingForVista7 wshShell, RegistName, ProgFile, "out", "private"
		RegistFirewallSettingForVista7 wshShell, RegistName, ProgFile, "out", "domain"

	End If
Next

Set wshShell = Nothing
