// LicBackDlg.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "LicBack.h"
#include "LicBackDlg.h"
#include <keylib.h>
#include <Skca.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// アプリケーションのバージョン情報で使われている CAboutDlg ダイアログ

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// ダイアログ データ
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard は仮想関数のオーバーライドを生成します
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV のサポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// メッセージ ハンドラがありません。
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CLicBackDlg ダイアログ

CLicBackDlg::CLicBackDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CLicBackDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CLicBackDlg)
		// メモ: この位置に ClassWizard によってメンバの初期化が追加されます。
	//}}AFX_DATA_INIT
	// メモ: LoadIcon は Win32 の DestroyIcon のサブシーケンスを要求しません。
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CLicBackDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CLicBackDlg)
		// メモ: この場所には ClassWizard によって DDX と DDV の呼び出しが追加されます。
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CLicBackDlg, CDialog)
	//{{AFX_MSG_MAP(CLicBackDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_ABOUTTHIS, OnButtonAboutthis)
	ON_BN_CLICKED(IDC_BUTTON_ONLINE, OnButtonOnline)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CLicBackDlg メッセージ ハンドラ

BOOL CLicBackDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// "バージョン情報..." メニュー項目をシステム メニューへ追加します。

	// IDM_ABOUTBOX はコマンド メニューの範囲でなければなりません。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// このダイアログ用のアイコンを設定します。フレームワークはアプリケーションのメイン
	// ウィンドウがダイアログでない時は自動的に設定しません。
	SetIcon(m_hIcon, TRUE);			// 大きいアイコンを設定
	SetIcon(m_hIcon, FALSE);		// 小さいアイコンを設定
	
	// TODO: 特別な初期化を行う時はこの場所に追加してください。
	
	return TRUE;  // TRUE を返すとコントロールに設定したフォーカスは失われません。
}

void CLicBackDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// もしダイアログボックスに最小化ボタンを追加するならば、アイコンを描画する
// コードを以下に記述する必要があります。MFC アプリケーションは document/view
// モデルを使っているので、この処理はフレームワークにより自動的に処理されます。

void CLicBackDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 描画用のデバイス コンテキスト

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// クライアントの矩形領域内の中央
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// アイコンを描画します。
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// システムは、ユーザーが最小化ウィンドウをドラッグしている間、
// カーソルを表示するためにここを呼び出します。
HCURSOR CLicBackDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CLicBackDlg::OnButtonAboutthis() 
{
	CString	cs_web	;
	cs_web = theApp.m_appBoot + "\\アンインストール認証.pdf" ;
	ShellExecute (NULL, "open", cs_web, NULL, NULL, SW_SHOWNORMAL) ;
}

void CLicBackDlg::OnButtonOnline() 
{
	long result;
	char LicenseUpdate[56] = {0};
	char status[4] = {0};
	char proxy[51] = {0};
	CString tempStr;
	int	i_work	;
	CString	cs_Url		,
			cs_Unlock	;
	CString	cs_err	;
	CString	cs_work		,
			cs_work1	,
			cs_work2	,
			cs_work3	,
			cs_asp		;

//	m_LicIDText.GetWindowText(tempStr);
//	m_LicenseID = atoi((char *)(const char*)tempStr);
//	m_LicPWText.GetWindowText(m_LicensePW);
	// Unconditionally clear out our stored session code
	// to prevent hacking on the stored session code
	// (refer to Delayed Trigger Codes in the index for information)
	if ((i_work = theApp.m_UrlUnlock.FindOneOf ("/")) == -1) {
		MessageBox ("オンライン認証URL情報エラーが発生しました。\nサポートまでご連絡下さい。", "アプリケーション・エラー", MB_OK | MB_ICONERROR) ;
		CDialog::OnCancel();
		return ;
	}
	cs_Url = theApp.m_UrlUnlock.Left (i_work) ;
	cs_Unlock = theApp.m_UrlUnlock.Mid (i_work) ;
	char	firstname[100]	,
			lastname[100]	,
			email[100]		,
			phone[100]		,
			ud1[100]			,
			ud2[100]			,
			ud3[100]			,
			ud4[100]			,
			ud5[100]			;
	LONG	regid = atol (theApp.m_LicenseID)	;

	memset (firstname, 0, sizeof (char) * 100) ;
	memset (lastname, 0, sizeof (char) * 100) ;
	memset (email, 0, sizeof (char) * 100) ;
	memset (phone, 0, sizeof (char) * 100) ;
	memset (ud1, 0, sizeof (char) * 100) ;
	memset (ud2, 0, sizeof (char) * 100) ;
	memset (ud3, 0, sizeof (char) * 100) ;
	memset (ud4, 0, sizeof (char) * 100) ;
	memset (ud5, 0, sizeof (char) * 100) ;

	_stprintf (phone, "%ld", theApp.m_compno) ;				/* コンピュータＩＤ用	*/
//	strcpy (phone, (char *)(const char*)theApp.m_compno) ;	/* パスワード用	*/
	strcpy (email, (char *)(const char*)theApp.m_LicensePW) ;	/* パスワード用	*/
	strcpy (ud5, theApp.m_LicenseID) ;					/* ライセンスＩＤ用	*/

	cs_asp = "/unlock/postuninstall.asp" ;
	if ((result = SK_PostEvalData((char *)(const char*)cs_Url, proxy, (char *)(const char*)cs_asp, firstname, lastname, email, phone, ud1, ud2, ud3, ud4, ud5, (LPLONG)&regid)) != 0) {
		regid = atol (theApp.m_LicenseID) ;
		if ((result = SK_PostEvalDataEx(SK_SECURE_CONNECTION, (char *)(const char*)cs_Url, proxy, (char *)(const char*)cs_asp, firstname, lastname, email, phone, ud1, ud2, ud3, ud4, ud5, (LPLONG)&regid)) != 0) {
			CString	csErr	;
			if (result == 100) {
				csErr = "エラー内容：ライセンスID不明" ;
			}
			else if (result == 106) {
				csErr = "エラー内容：コンピュータID不整合" ;
			}
			else if (result == 101) {
				csErr = "エラー内容：認証ログ不明" ;
			}
			else if (result == 102) {
				csErr = "エラー内容：ライセンス（ステータス）不明" ;
			}
			else {
				csErr = "エラー内容：不明" ;
			}
			cs_err.Format ("エラーコード:%d  %s\nオンライン自動返却での処理に失敗しました。\nお手数ですが、手動返却をで再度お試し下さい。\nもし手動返却でも失敗した場合は、弊社サポートまでご連絡下さい。", result, csErr) ;
			MessageBox(cs_err, "アンインストール認証エラー", MB_OK | MB_ICONERROR);
			OnCancel();
			return ;
		}
	}

	MessageBox("アンインストール認証（ライセンス一時返却）が完了しました。", "インフォメーション", MB_OK | MB_ICONINFORMATION) ;
	CDialog::OnOK();
}

void CLicBackDlg::OnCancel() 
{
	CString		csMsg	,
				cs_Url	;
	int			i_work	;
	if ((i_work = theApp.m_UrlUnlock.FindOneOf ("/")) == -1) {
		MessageBox ("オンライン認証URL情報エラーが発生しました。\nサポートまでご連絡下さい。", "アプリケーション・エラー", MB_OK | MB_ICONERROR) ;
		CDialog::OnCancel();
		return ;
	}
	cs_Url = theApp.m_UrlUnlock.Left (i_work) ;
	csMsg.Format ("本ＰＣのライセンスの削除を完了しました。\n下記をメモし、アンインストール認証ページよりライセンスの一時返却を必ず完了して下さい。\n\nアンインストール認証URL：\n　　https://%s/unlock/uninstall.asp\n\nアンインストールコード：\n　　%s－%08ld－%ld", cs_Url, theApp.m_LicensePW, atol (theApp.m_LicenseID), theApp.m_compno) ;
	MessageBox(csMsg, "インフォメーション", MB_OK | MB_ICONINFORMATION) ;
	CDialog::OnOK();
}
