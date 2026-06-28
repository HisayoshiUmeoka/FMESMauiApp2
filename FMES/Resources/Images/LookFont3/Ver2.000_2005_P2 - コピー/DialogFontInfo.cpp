// DialogFontInfo.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "lookfont.h"
#include "DialogFontInfo.h"
#include <atlbase.h>
#include <io.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CDialogFontInfo ダイアログ


CDialogFontInfo::CDialogFontInfo(CWnd* pParent /*=NULL*/)
	: CDialog(CDialogFontInfo::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDialogFontInfo)
		// メモ - ClassWizard はこの位置にマッピング用のマクロを追加または削除します。
	//}}AFX_DATA_INIT
	m_hIcon = AfxGetApp()->LoadIcon(IDR_LOOKFOTYPE);
}


void CDialogFontInfo::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDialogFontInfo)
		// メモ - ClassWizard はこの位置にマッピング用のマクロを追加または削除します。
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CDialogFontInfo, CDialog)
	//{{AFX_MSG_MAP(CDialogFontInfo)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDialogFontInfo メッセージ ハンドラ

BOOL CDialogFontInfo::OnInitDialog() 
{
	CDialog::OnInitDialog();
	BOOL	win98Me	;
	// TODO: この位置に初期化の補足処理を追加してください
	SetDlgItemText (IDC_EDIT_FONT1, m_FontFace) ;
	if (m_FontMode) {
		SetDlgItemText (IDC_EDIT_FONT2, m_FontFile) ;
		SetDlgItemText (IDC_STATIC_KIND, CString("一時インストール・フォント")) ;
	}
	else {
		CString	retPath	;
		OSVERSIONINFO	VersionInfo	;
		VersionInfo.dwOSVersionInfoSize = sizeof(OSVERSIONINFO);
		if (GetVersionEx (&VersionInfo) == FALSE) {
//			::MessageBox (NULL, _T("Windowsバージョン取得でエラー発生！\n処理を終了します。"), _T("エラーメッセージ"), MB_OK | MB_ICONEXCLAMATION) ;
			SetDlgItemText (IDC_EDIT_FONT2, "---ファイルパス不明---") ;
			SetDlgItemText (IDC_STATIC_KIND, CString("---情報取得エラー---")) ;
		}
		else {
			switch (VersionInfo.dwPlatformId) {
				case VER_PLATFORM_WIN32_WINDOWS:  // Windows 9x系
					if (VersionInfo.dwMajorVersion == 4) {
					switch (VersionInfo.dwMinorVersion) {
						case 0:  // .NET Frameworkのサポートなし
							// 95
							win98Me = TRUE ;
							break;
						case 10:
							// 98/98Se
							win98Me = TRUE ;
							break;
						case 90:
							// Me
							win98Me = TRUE ;
							break;
						}
					}
					break;
				case VER_PLATFORM_WIN32_NT:  // Windows NT系
					win98Me = FALSE ;
					break;
			}

			if (RegSubSearch(m_FontFace, win98Me, retPath)) {
				SetDlgItemText (IDC_EDIT_FONT2, retPath) ;
			}
			else {
				SetDlgItemText (IDC_EDIT_FONT2, "---ファイルパス不明---") ;
			}
			SetDlgItemText (IDC_STATIC_KIND, CString("既存登録フォント")) ;
		}
	}
	return TRUE;  // コントロールにフォーカスを設定しないとき、戻り値は TRUE となります
	              // 例外: OCX プロパティ ページの戻り値は FALSE となります
}

void CDialogFontInfo::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == SC_CLOSE)
	{
		CDialog::OnCancel () ;
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

void CDialogFontInfo::OnPaint() 
{
	CPaintDC dc(this); // 描画用のデバイス コンテキスト
	if (IsIconic())
	{
		CPaintDC dc(this); // 描画のデバイス コンテキスト

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// クライアントの四角形領域内の中央
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// アイコンの描画
		dc.DrawIcon(x, y, m_hIcon);
	}
}

BOOL CDialogFontInfo::RegSubSearch(CString csFont, BOOL win98Me, CString &retPath)
{
	BOOL	bRet = FALSE	;
	CRegKey	crSrc2	;
	DWORD	dwType = 0;
	DWORD	iIndex2 = 0	;
	CString subKey	;
	CString	subKey2	;
	CString	subKeyComp	;
	CString	csErr	;
	CString	csFind	;
	BYTE	pData[256] = {0};
	DWORD	dwDataLength = 256;
	DWORD	dwNameLength = 64;
	TCHAR	szBuffer[64] = {0};
	LPTSTR	psubKey2 = subKey2.GetBufferSetLength (MAX_PATH) ;
	ULONG	sz2 = subKey2.GetLength () ;
	if (win98Me) {
		subKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Fonts" ;
	}
	else {
		subKey = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts" ;
	}
	if (crSrc2.Open (HKEY_LOCAL_MACHINE, subKey) == ERROR_SUCCESS ) {
		psubKey2[0] = 0;
		sz2 = subKey2.GetLength () ;
		dwType = 0;
		iIndex2 = 0;
		pData[0] = 0;
		dwDataLength = 256;
		while (RegEnumValue (crSrc2, iIndex2++, psubKey2, &sz2, NULL, &dwType, pData, &dwDataLength) == ERROR_SUCCESS) {
			switch( dwType ) {
				case REG_SZ:
					{
						ULONG	usiz = MAX_PATH	;
						crSrc2.QueryValue (csFind.GetBufferSetLength (MAX_PATH), psubKey2, &usiz) ;
						subKeyComp = psubKey2 ;
//						csFind.MakeLower () ;
						if (subKeyComp.Find (csFont) > -1) {
							// ファイル発見
//							retPath = csFind ;
							if (_taccess (csFind, 0) != 0) {
								retPath = GetWinFontPath () + "\\" + csFind ;
							}
							else {
								retPath = csFind ;
							}
							bRet = TRUE ;
						}
					}
					break;
				case REG_DWORD:
					break;
				default:
					break;
			}
			if (bRet == TRUE) {
				break ;
			}
			psubKey2[0] = 0;
			sz2 = subKey2.GetLength () ;

			pData[0] = 0;
			dwDataLength = 256;
		}
		crSrc2.Close () ;
	}
	return (bRet) ;
}

CString CDialogFontInfo::GetWinFontPath()
{
	CString	csRet	;
	TCHAR	tPath[MAX_PATH]	;
	memset (tPath, 0, sizeof (TCHAR) * MAX_PATH) ;
	csRet = "" ;
	if (::GetWindowsDirectory (tPath, MAX_PATH) > 0) {
		csRet = tPath ;
		csRet += _T("\\Fonts") ;
		if (_taccess (csRet, 0) != 0) {
			csRet = "" ;
		}
	}
	return (csRet) ;
}
