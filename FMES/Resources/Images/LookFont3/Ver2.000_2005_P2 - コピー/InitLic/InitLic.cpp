// InitLic.cpp : アプリケーションのクラス動作を定義します。
//

#include "stdafx.h"
#include "InitLic.h"
#include "InitLicDlg.h"
#include <keylib.h>
#include <Skca.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CInitLicApp

BEGIN_MESSAGE_MAP(CInitLicApp, CWinApp)
	ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()


// CInitLicApp コンストラクション

CInitLicApp::CInitLicApp()
{
	// TODO: この位置に構築用コードを追加してください。
	// ここに InitInstance 中の重要な初期化処理をすべて記述してください。
}


// 唯一の CInitLicApp オブジェクトです。

CInitLicApp theApp;


// CInitLicApp 初期化

BOOL CInitLicApp::InitInstance()
{
	// アプリケーション マニフェストが visual スタイルを有効にするために、
	// ComCtl32.dll Version 6 以降の使用を指定する場合は、
	// Windows XP に InitCommonControlsEx() が必要です。さもなければ、ウィンドウ作成はすべて失敗します。
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// アプリケーションで使用するすべてのコモン コントロール クラスを含めるには、
	// これを設定します。
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();

	AfxEnableControlContainer();

	// 標準初期化
	// これらの機能を使わずに最終的な実行可能ファイルの
	// サイズを縮小したい場合は、以下から不要な初期化
	// ルーチンを削除してください。
	// 設定が格納されているレジストリ キーを変更します。
	// TODO: 会社名または組織名などの適切な文字列に
	// この文字列を変更してください。
//	SetRegistryKey(_T("アプリケーション ウィザードで生成されたローカル アプリケーション"));

	CString	csWrokPath (m_pszHelpFilePath) ;
	int	iFund = csWrokPath.ReverseFind ('\\') ;
	m_appPath = csWrokPath.Left (iFund) ;
//	m_appPath.Format("%s", _getcwd(buffer, _MAX_PATH));
	m_appPath.MakeUpper();
	m_appBoot = m_appPath ;
	m_appPath += CString("\\LookFont.lf");
//MessageBox (NULL, m_appPath, "debug_lf", MB_OK) ;
//	m_compno = pp_compno(COMPNO_WINPRODID | COMPNO_HDSERIAL, "", "C");
//MessageBox (NULL, "debug001", "debug", MB_OK) ;
	CheckProKey() ;
//	if (m_ActivatOK == 0) {
//MessageBox (NULL, "debug002-err", "debug", MB_OK) ;
//		UpdateProKey () ;
//MessageBox (NULL, "debug003-err", "debug", MB_OK) ;
//		MessageBox (NULL, "ライセンスの削除が完了しました。\n手動によるアンインストール認証は必要ありません。", "アンインストール認証", MB_OK) ;
//		return FALSE;
//	}
//
//
//	CInitLicDlg dlg;
//	m_pMainWnd = &dlg;
//	INT_PTR nResponse = dlg.DoModal();
//	if (nResponse == IDOK)
//	{
//		// TODO: ダイアログが <OK> で消された時のコードを
//		//  記述してください。
//	}
//	else if (nResponse == IDCANCEL)
//	{
//		// TODO: ダイアログが <キャンセル> で消された時のコードを
//		//  記述してください。
//	}

	// ダイアログは閉じられました。アプリケーションのメッセージ ポンプを開始しないで
	//  アプリケーションを終了するために FALSE を返してください。
	return FALSE;
}

BOOL CInitLicApp::CheckProKey()
{	BOOL	b_ret = FALSE	,
			b_open = FALSE	;
	bool	b_dec			;
	long	l_ret			;
	char	buffer[128]		;
	CString	cs_work			;
//	CSerialKey	proChk		;
	
//MessageBox (NULL, "debug002-001", "debug", MB_OK) ;
//	memset (m_LicenseID, 0, sizeof (char) * 16) ;
//	memset (m_LicensePW, 0, sizeof (char) * 16) ;
	if (m_lfhandle == 0) {
		b_open = TRUE ;
		if ((l_ret = pp_lfopen((char *)((const char *)m_appPath), LF_CREATE_NORMAL, LF_FILE, "pluss21", &m_lfhandle)) != PP_SUCCESS) {
			return (b_ret) ;
		}
	}
	if (b_open == TRUE) {
		pp_lfclose(m_lfhandle) ;
		m_lfhandle = 0 ;
	}
//MessageBox (NULL, "debug002-006", "debug", MB_OK) ;
	return (b_ret) ;
}
