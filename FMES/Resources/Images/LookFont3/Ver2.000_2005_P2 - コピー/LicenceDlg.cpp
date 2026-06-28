// LicenceDlg.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "lookfont.h"
#include "LicenceDlg.h"
#include <keylib.h>
#include <Skca.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CLicenceDlg ダイアログ


CLicenceDlg::CLicenceDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CLicenceDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CLicenceDlg)
		// メモ - ClassWizard はこの位置にマッピング用のマクロを追加または削除します。
	//}}AFX_DATA_INIT
	m_daysLeftString = (_T("")) ;
	m_demoTextString = (_T("")) ;
	m_runsLeftString = (_T("")) ;
	m_hIcon = AfxGetApp()->LoadIcon(IDI_ICON_LICENCE);
	m_bWinXPThemes = TRUE;
	m_bWordTheme = FALSE;
	m_intTheme = 2;
	m_bAlpha = TRUE;
	m_dwXTBtnStyle = (BS_XT_WINXP_COMPAT | xtThemeOffice2003 | BS_XT_TWOROWS) ;
}

void CLicenceDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CLicenceDlg)
	DDX_Control(pDX, IDC_RUNS_LEFT, m_runsLeft);
	DDX_Control(pDX, IDC_DEMO_TEXT, m_demoText);
	DDX_Control(pDX, IDC_DAYS_LEFT, m_daysLeft);
	DDX_Text(pDX, IDC_DAYS_LEFT, m_daysLeftString);
	DDX_Text(pDX, IDC_DEMO_TEXT, m_demoTextString);
	DDX_Text(pDX, IDC_RUNS_LEFT, m_runsLeftString);
	DDX_Control(pDX, IDC_BUTTON_KEY1, m_btnKey1);
	DDX_Control(pDX, IDC_BUTTON_KEY2, m_btnKey2);
	DDX_Control(pDX, IDC_BUTTON_KEY3, m_btnKey3);
	DDX_Control(pDX, IDC_GOEXEIT, m_btnOK);
	DDX_Control(pDX, IDC_GOSKIP, m_btnCancel);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CLicenceDlg, CDialog)
	//{{AFX_MSG_MAP(CLicenceDlg)
	ON_BN_CLICKED(IDC_BUTTON_KEY1, OnBnClickedButtonKey1)
	ON_BN_CLICKED(IDC_BUTTON_KEY2, OnBnClickedButtonKey2)
	ON_BN_CLICKED(IDC_BUTTON_KEY3, OnBnClickedButtonKey3)
	ON_WM_PAINT()
	ON_BN_CLICKED(IDC_CANCEL, OnBnClickedGoskip)
	ON_BN_CLICKED(IDC_OK, OnBnClickedGoexeit)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CLicenceDlg メッセージ ハンドラ

BOOL CLicenceDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	SetIcon(m_hIcon, TRUE);			// 大きいアイコンの設定
	SetIcon(m_hIcon, FALSE);		// 小さいアイコンの設定
	long runsleft;
//	m_runsLeftString = "";
//	if (m_expired)
//	{
//		m_daysLeftString = "";
//		m_demoTextString.Format("この製品の試用期間が切れました。継続してご使用をご希望される場合は製品をご購入して下さい。");
//	}
//	else
//	{
//		pp_daysleft(theApp.m_lfhandle, &daysleft);
//		m_daysLeftString.Format("試用期間は %d日間経過しました。", daysleft);
//		m_demoTextString.Format("現在、この製品を試用でご利用中です。すぐに製品のご購入をされる場合は、オンラインショッピングでご購入されるか、弊社セールス担当までご連絡下さい。");
//		pp_getvarnum(theApp.m_lfhandle, VAR_EXP_LIMIT, &runsleft);
//		if (runsleft >= 0)
//		{
//			m_runsLeftString.Format("試用期間終了まであと %d日間です。", runsleft);
//		}
//	}

//	if(m_pDoc->m_Ini.AutoUpdate && m_pDoc->CheckUpdatedDate ())
//		StartAutoUpdate(TRUE);

	// TODO: 初期化をここに追加します。
	if (!XTOSVersionInfo()->IsWinXPOrGreater())
	{
//		m_bWinXPThemes = FALSE;
//		m_btnWinXPThemes.EnableWindow(FALSE);
	}

	m_runsLeftString = "";
	if (m_expired)
	{
		m_btnCancel.ShowWindow(SW_HIDE);
		m_btnOK.ShowWindow(SW_SHOW);
		m_daysLeft.ShowWindow(SW_HIDE);
	}
	else
	{
		m_btnOK.ShowWindow(SW_HIDE);
		m_btnCancel.ShowWindow(SW_SHOW);
		pp_getvarnum(theApp.m_lfhandle, VAR_EXP_LIMIT, &runsleft);

		m_daysLeft.ShowWindow(SW_SHOW);
		if (runsleft >= 0)
		{
			m_runsLeft.ShowWindow(SW_SHOW);
		}

	}

//	UpdateIcons();

	m_btnKey1.	SetTheme(xtThemeOffice2003);
	m_btnKey2.	SetTheme(xtThemeOffice2003);
	m_btnKey3.	SetTheme(xtThemeOffice2003);
	m_btnOK.	SetTheme(xtThemeOffice2003);
	m_btnCancel.SetTheme(xtThemeOffice2003);

	m_btnKey1.	SetBitmap(CSize(57, 40), IDB_PLUSS);
	m_btnKey2.	SetBitmap(CSize(46, 40), IDB_UNLOCK01);
	m_btnKey3.	SetBitmap(CSize(46, 40), IDB_UNLOCK02);
	m_btnOK.	SetBitmap(CSize(46, 40), IDB_EXIT);
	m_btnCancel.SetBitmap(CSize(46, 40), IDB_SKIP);

	m_btnKey1.	SetXButtonStyle(m_dwXTBtnStyle);
	m_btnKey2.	SetXButtonStyle(m_dwXTBtnStyle);
	m_btnKey3.	SetXButtonStyle(m_dwXTBtnStyle);
	m_btnOK.	SetXButtonStyle(m_dwXTBtnStyle);
	m_btnCancel.SetXButtonStyle(m_dwXTBtnStyle);

//	OnRadOffice2003();
	SetWinxpThemes();
	EnableButtons(XTThemeManager()->GetTheme());
	SetTheme(xtThemeOffice2003);
	UpdateXTStyle();
	return TRUE;  // コントロールにフォーカスを設定しないとき、戻り値は TRUE となります
	              // 例外: OCX プロパティ ページの戻り値は FALSE となります
}

void CLicenceDlg::OnPaint()
{
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
	else
	{
		CDialog::OnPaint();
	}
}

void CLicenceDlg::OnBnClickedButtonKey1()
{
	CString	cs_web = "http://www.pluss-inc.com/" ;
	ShellExecute (NULL, "open", cs_web, NULL, NULL, SW_SHOWNORMAL) ;
}

void CLicenceDlg::OnBnClickedButtonKey2()
{
	//Variables for SK_ProcEZTrig1
	long result;
	long lfresult;
	CString ErrMsg;
	//Variables for SK_GetRegData and SK_GetLicenseStatus
	long LicenseID = 0;
	char LicensePW[16] = {0};
	char proxy[51] = {0};
	char company[56] = {0};
	char contact[56] = {0};
	char ignore[55] = {0};
	char status[4] = {0};

	result = SK_ProcEZTrig1(0, (char *)(const char *)theApp.m_appPath, "pluss21", 0, &lfresult);
	
	if (result == 100)
		MessageBox("無効なライセンス情報です。\nLicense ID と Password を確認し、再度認証して頂くか、\n弊社サポートまでご連絡下さい。", "エラーメッセージ", MB_OK);
	else if (result == 7)//7=SWKERR_NO_MORE_SOFTWARE_KEYS_AVAILABLE
		MessageBox("入手可能なライセンスは全てご利用済みです。\n弊社サポートまでご連絡下さい。", "エラーメッセージ", MB_OK);
	else if (result != 1)
	{
		if (result != 0)
		{
			ErrMsg.Format("エラー #%d 発生！\nサポートへお問い合わせ下さい。", result);
			MessageBox(ErrMsg, "Error!", MB_OK | MB_ICONEXCLAMATION);
		}
	}
	else
	{
		MessageBox("認証成功！\n正規ライセンスの取得に成功しました。", "Success!", MB_OK);

		pp_getvarnum(theApp.m_lfhandle, 28, &LicenseID);//28 = VAR_LICENSE_ID
		pp_getvarchar(theApp.m_lfhandle, 33, LicensePW);//33 = VAR_LICENSEPW
		pp_getvarchar(theApp.m_lfhandle, 26, proxy);//26 = VAR_AUTOCL_PROXY_ADDR

		result = SK_GetRegData("ss.pluss-inc.com", proxy, "/test3/getregdata.asp", LicenseID, LicensePW, company, contact, ignore, ignore, ignore, ignore, ignore, ignore, ignore);
		if (result == 0)
		{
			pp_setvarchar(theApp.m_lfhandle, VAR_COMPANY, company);
			pp_setvarchar(theApp.m_lfhandle, VAR_NAME, contact);
		}

		result = SK_GetLicenseStatus("ss.pluss-inc.com", proxy, "/test33/getlicensestatus.asp", LicenseID, LicensePW, status, 0, 0);
		if (result == 0)
		{
			if (toupper(status[0]) != 'O' && toupper(status[1]) != 'K')
			{
				//turn off the software
				MessageBox("このライセンスはご利用を許可されておりません。\n弊社サポートまでご連絡下さい。", "ライセンス　エラー", MB_OK);
				pp_setvarchar (theApp.m_lfhandle, VAR_EXPIRE_TYPE, "D");
				pp_setvardate (theApp.m_lfhandle, VAR_EXP_DATE_HARD, 1, 1, 2000);
				pp_copydelete (theApp.m_lfhandle, -1);
				pp_upddate (theApp.m_lfhandle, 0);
			}
		}

		// call the StatusChanged subroutine again to reset all of our indicators
		OnOK();
		theApp.m_startOver = true;
	}
}

void CLicenceDlg::OnBnClickedButtonKey3()
{
	long result;
	long lfresult;
	CString ErrMsg;
	
	// now run eztrig to possibly change the mode
	result = pp_eztrig1((LONG)this->m_hWnd, (char *)(const char *)theApp.m_appPath, "pluss21", &lfresult);

	if (result == 0)
	{
		if (lfresult != 0)
		{
			ErrMsg.Format("ファイル エラー #%d 発生！\nサポートへお問い合わせ下さい。", lfresult);
			MessageBox(ErrMsg, "Error!", MB_OK | MB_ICONEXCLAMATION);
		}
	}
	else if (result == ERR_INVALID_CODE_ENTERED)
	{
		MessageBox("入力コードエラー\n不正なコードが入力されました。", "エラーメッセージ", MB_OK | MB_ICONEXCLAMATION);
	}
	else
	{
		MessageBox("認証成功！\n正規ライセンスの取得に成功しました。", "インフォメーション", MB_OK | MB_ICONINFORMATION);

		// call the StatusChanged subroutine again to reset all of our indicators
		CDialog::OnOK();
		theApp.m_startOver = true;
	}
}

void CLicenceDlg::OnBnClickedGoskip()
{
	CDialog::OnOK();
	theApp.m_showMain = true;	
}

void CLicenceDlg::OnBnClickedGoexeit()
{
	OnOK();
	theApp.m_startOver = false;
}
