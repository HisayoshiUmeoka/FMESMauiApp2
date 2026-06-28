// ProKeyDlg.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "lookfont.h"
#include "ProKeyDlg.h"
#include "SerialKey.h"
#include "UnlockDlg.h"
#include "DialogUser.h"
#include <keylib.h>
#include <Skca.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CProKeyDlg ダイアログ


CProKeyDlg::CProKeyDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CProKeyDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CProKeyDlg)
	m_ProKey1 = _T("");
	m_ProKey2 = _T("");
	m_ProKey3 = _T("");
	m_ProKey4 = _T("");
	m_ProKey5 = _T("");
	//}}AFX_DATA_INIT
	m_hIcon = AfxGetApp()->LoadIcon(IDI_ICON_LICENCE);
	m_LicenseID = 0;
	m_LicensePW = "";
	m_LeaveDays = 0;
}


void CProKeyDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CProKeyDlg)
	DDX_Control(pDX, IDC_EDIT_PROKEY5, m_ProKeyEd5);
	DDX_Control(pDX, IDC_EDIT_PROKEY4, m_ProKeyEd4);
	DDX_Control(pDX, IDC_EDIT_PROKEY3, m_ProKeyEd3);
	DDX_Control(pDX, IDC_EDIT_PROKEY2, m_ProKeyEd2);
	DDX_Control(pDX, IDC_EDIT_PROKEY1, m_ProKeyEd1);
	DDX_Text(pDX, IDC_EDIT_PROKEY1, m_ProKey1);
	DDV_MaxChars(pDX, m_ProKey1, 5);
	DDX_Text(pDX, IDC_EDIT_PROKEY2, m_ProKey2);
	DDV_MaxChars(pDX, m_ProKey2, 5);
	DDX_Text(pDX, IDC_EDIT_PROKEY3, m_ProKey3);
	DDV_MaxChars(pDX, m_ProKey3, 5);
	DDX_Text(pDX, IDC_EDIT_PROKEY4, m_ProKey4);
	DDV_MaxChars(pDX, m_ProKey4, 5);
	DDX_Text(pDX, IDC_EDIT_PROKEY5, m_ProKey5);
	DDV_MaxChars(pDX, m_ProKey5, 5);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CProKeyDlg, CDialog)
	//{{AFX_MSG_MAP(CProKeyDlg)
	ON_BN_CLICKED(IDC_BUTTON_ACT1, OnButtonAct1)
	ON_BN_CLICKED(IDC_BUTTON_ACT2, OnButtonAct2)
	ON_BN_CLICKED(IDC_BUTTON_ACT3, OnButtonAct3)
	ON_EN_CHANGE(IDC_EDIT_PROKEY1, OnChangeEditProkey1)
	ON_EN_CHANGE(IDC_EDIT_PROKEY2, OnChangeEditProkey2)
	ON_EN_CHANGE(IDC_EDIT_PROKEY3, OnChangeEditProkey3)
	ON_EN_CHANGE(IDC_EDIT_PROKEY4, OnChangeEditProkey4)
	ON_EN_CHANGE(IDC_EDIT_PROKEY5, OnChangeEditProkey5)
	ON_WM_PAINT()
	ON_WM_SYSCOMMAND()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CProKeyDlg メッセージ ハンドラ

BOOL CProKeyDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	SetIcon(m_hIcon, TRUE);			// 大きいアイコンの設定
	SetIcon(m_hIcon, FALSE);		// 小さいアイコンの設定

	// TODO :  ここに初期化を追加してください

	m_ProKey1 = theApp.m_ProKey1 ;
	m_ProKey2 = theApp.m_ProKey2 ;
	m_ProKey3 = theApp.m_ProKey3 ;
	m_ProKey4 = theApp.m_ProKey4 ;
	m_ProKey5 = theApp.m_ProKey5 ;
	UpdateData (FALSE) ;
	SetDlgItemInt (IDC_STATIC_LIMITDAYS, m_LeaveDays, FALSE) ;
	BOOL	b_enb = CheckProkeyVal() ;
//	GetDlgItem (IDOK)->EnableWindow (b_enb) ;
	GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (b_enb) ;
	GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (b_enb) ;
	return TRUE;  // コントロールにフォーカスを設定しないとき、戻り値は TRUE となります
	              // 例外: OCX プロパティ ページの戻り値は FALSE となります
}

void CProKeyDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == SC_CLOSE)
	{
		OnCancel () ;
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

void CProKeyDlg::OnButtonAct1() 
{
	CString	cs_web = "http://www.ss.pluss-inc.com/support/" ;
	ShellExecute (NULL, "open", cs_web, NULL, NULL, SW_SHOWNORMAL) ;
}

void CProKeyDlg::OnButtonAct2() 
{
	// TODO : ここにコントロール通知ハンドラ コードを追加します。
	long result;
	long TcValue;	// trigger code value
	long TcData;	// trigger code data
	long RegKey1, RegKey2;
	long SessionCode;
	char LicenseUpdate[56] = {0};
	char status[4] = {0};
	char proxy[51] = {0};
	CString tempStr;
	int	i_work	;
	CString	cs_Url		,
			cs_Unlock	;

	SessionCode = pp_cenum();
//	m_LicIDText.GetWindowText(tempStr);
//	m_LicenseID = atoi((char *)(const char*)tempStr);
//	m_LicPWText.GetWindowText(m_LicensePW);
	// Unconditionally clear out our stored session code
	// to prevent hacking on the stored session code
	// (refer to Delayed Trigger Codes in the index for information)
	if ((i_work = theApp.m_UrlUnlock.FindOneOf ("/")) == -1) {
		MessageBox ("オンライン認証URL情報エラーが発生しました。\nサポートまでご連絡下さい。", "アプリケーション・エラー", MB_OK | MB_ICONERROR) ;
		return ;
	}
	cs_Url = theApp.m_UrlUnlock.Left (i_work) ;
	cs_Unlock = theApp.m_UrlUnlock.Mid (i_work) ;

	if ((result = SK_GetTCData((char *)(const char*)cs_Url, "", (char *)(const char*)cs_Unlock, m_LicenseID, (char *)(const char*)m_LicensePW, SessionCode, theApp.m_compno, &RegKey1, &RegKey2, LicenseUpdate)) != SWKERR_NONE) {
		if ((result = SK_GetTCDataEx(SK_SECURE_CONNECTION, (char *)(const char*)cs_Url, "", (char *)(const char*)cs_Unlock, m_LicenseID, (char *)(const char*)m_LicensePW, SessionCode, theApp.m_compno, &RegKey1, &RegKey2, LicenseUpdate)) != SWKERR_NONE) {
			CString ErrMsg;
			ErrMsg.Format("通信エラー #%d 発生！\nサポートまでご連絡下さい",result);
			MessageBox(ErrMsg,"エラーメッセージ",MB_OK | MB_ICONERROR);
			return ;
		}
	}
	pp_setvarnum(theApp.m_lfhandle, VAR_UDEF_NUM_5, 0);

	result = SK_GetLicenseStatus("ss.pluss-inc.com", proxy, "/unlock/getlicensestatus.asp", m_LicenseID, (char *)(const char*)m_LicensePW, status, 0, 0);
	if (result == 0)
	{
		if (toupper(status[0]) != 'O' && toupper(status[1]) != 'K')
		{
			//turn off the software
			MessageBox("このライセンスはご利用を許可されておりません。\n弊社サポートまでご連絡下さい。", "ライセンス　エラー", MB_OK);
//			pp_setvarchar (theApp.m_lfhandle, VAR_EXPIRE_TYPE, "D");
//			pp_setvardate (theApp.m_lfhandle, VAR_EXP_DATE_HARD, 1, 1, 2000);
//			pp_copydelete (theApp.m_lfhandle, -1);
//			pp_upddate (theApp.m_lfhandle, 0);
		}
	}
	else {
		result = SK_GetLicenseStatusEx(SK_SECURE_CONNECTION, "ss.pluss-inc.com", proxy, "/unlock/getlicensestatus.asp", m_LicenseID, (char *)(const char*)m_LicensePW, status, 0, 0);
		if (result == 0)
		{
			if (toupper(status[0]) != 'O' && toupper(status[1]) != 'K')
			{
				//turn off the software
				MessageBox("このライセンスはご利用を許可されておりません。\n弊社サポートまでご連絡下さい。", "ライセンス　エラー", MB_OK);
	//			pp_setvarchar (theApp.m_lfhandle, VAR_EXPIRE_TYPE, "D");
	//			pp_setvardate (theApp.m_lfhandle, VAR_EXP_DATE_HARD, 1, 1, 2000);
	//			pp_copydelete (theApp.m_lfhandle, -1);
	//			pp_upddate (theApp.m_lfhandle, 0);
			}
		}
	}

	// See if a valid code was entered
	// Also decode the Trigger Code additional number, if applicable
	/*CString tempStr;
	m_LicenseID.GetWindowText(*/
//	TcValue = pp_tcode(RegKey1, SessionCode, theApp.m_compno, 398);
	/*m_regKey2.GetWindowText(tempStr);
	TcData = pp_ndecrypt(atoi((char *)(const char *)tempStr), 125);*/

	if ((result = pp_eztrig1ex(theApp.m_lfhandle, RegKey1, RegKey2, 0, SessionCode, theApp.m_compno, 58371 /*398*/, 195 /*125*/, &TcValue, &TcData)) != PP_SUCCESS) {
		CString	cs_err	;
		cs_err.Format ("ライセンス認証解除コード取得エラー発生！\nエラーコード:%d\nライセンス認証時の解除コード取得に失敗しました。\n弊社サポートまでご連絡下さい。", result) ;
		MessageBox(cs_err, "ライセンス認証解除エラー", MB_OK | MB_ICONERROR);
		theApp.m_showMain = false ;
		theApp.m_ActivatOK = FALSE ;
	}

	switch(TcValue)
	{
		case 1:
		{
			// This code should authorize this computer and turn on payments
			theApp.CheckError(pp_copyadd(theApp.m_lfhandle, COPYADD_ERASEALL, theApp.m_compno));

			// Extend Payment Expiration for the next quarter - on the 15th of the month
//ExtendPayment();
			break;
		}
		case 2:
		{
			// This code should authorize this computer and turn off payments
			theApp.CheckError(pp_copyadd(theApp.m_lfhandle, COPYADD_ERASEALL, theApp.m_compno));

			// Release from Payment Expiration
//TurnOffPayments();
			break;
		}
		case 3:
		{
			// This code should de-authorize this computer
//			if (theApp.CheckError(pp_copydelete(theApp.m_lfhandle, theApp.m_compno)) == PP_SUCCESS)
//			{
//				MessageBox("アプリケーションの認証解除が完了しました。", "インフォメーション", MB_OK | MB_ICONINFORMATION);
//			}
			break;
		}
		case 4:
		{
			// Set Demo Expiration to 30 days from today
//theApp.ConvertToDemo(30);
			break;
		}
		case 5:
		{
			// Extend Payment Expiration for the next quarter - on the 15th of the month
//ExtendPayment();
			break;
		}
		case 6:
		{
			// Release from Payment Expiration
//TurnOffPayments();
			break;
		}
		case 7:
		case 31:
		{
			// If the user turned the date forward by mistake, ran your program and
			// then set the clock back to the correct time, they will not be allowed
			// in becuase pp_valdate() will fail. THey will call you and you will give
			// them code 6 to force the last used date/time fields to be set to the
			// current (and correct) date and time.
			theApp.CheckError(pp_upddate(theApp.m_lfhandle, UPDDATE_FORCE));
			theApp.CheckError(pp_setvarchar(theApp.m_lfhandle, VAR_EXPIRE_TYPE, "N"));
			theApp.m_showMain = true ;
			theApp.m_ActivatOK = TRUE ;
			theApp.m_ProKey1 = m_ProKey1;	//	J34FJP
			theApp.m_ProKey2 = m_ProKey2;	//	JRJRJ3
			theApp.m_ProKey3 = m_ProKey3;	//	JFKP47
			theApp.m_ProKey4 = m_ProKey4;	//	J7KB6M
			theApp.m_ProKey5 = m_ProKey5;	//	4RKR63
			if (MessageBox("オンライン認証が完了しました。\n引き続き、オンラインでユーザー登録する事が出来ます。\nサポートにはユーザー登録が必要です。\nこのままオンラインユーザー登録をする場合は「はい」を、しない場合は「いいえ」をクリックして下さい。", "インフォメーション", MB_YESNO | MB_ICONINFORMATION) == IDYES) {
				CDialogUser	dlgUser	;
				dlgUser.m_LicenseID = m_LicenseID ;
				dlgUser.m_LicensePW = m_LicensePW ;
				dlgUser.DoModal () ;
			}
			OnOK();
			break;
		}
		case 8:
		{
			// Set Demo Expireation to X days from today, where X is the
			// Trigger Code Additional Number (regkey2)
//theApp.ConvertToDemo(TcData);
			break;
		}
		case 49:
		{
			// These codes could be used for something else
			break;
		}
		default:
		{
			// Invalid code was entered.
			if (result == 100) //If invalid license information was entered...
				MessageBox("不正なライセンス情報が入力されました。\nライセンスＩＤとパスワードを確認し、再度入力して下さい。\nまたはサポートまでご連絡下さい。","エラーメッセージ", MB_OK | MB_ICONERROR);
			else if (result == 7)//if no more keys are available...
				MessageBox("規定のインストール回数を超えてしまいました。\nサポートまでご連絡下さい。","エラーメッセージ", MB_OK | MB_ICONERROR);
			else if (result != 0)
			{
				CString ErrMsg;
				ErrMsg.Format("Error #%d 発生！\nサポートまでご連絡下さい",result);
				MessageBox(ErrMsg,"エラーメッセージ",MB_OK | MB_ICONERROR);
			}
			else
				MessageBox("不正なコードが入力されました！", "エラーメッセージ", MB_OK | MB_ICONERROR);
		}
	}
}

void CProKeyDlg::OnButtonAct3() 
{
	CUnlockDlg	dlg	;
	if (dlg.DoModal () == IDOK) {
		if (theApp.m_showMain == true && theApp.m_ActivatOK == TRUE) {
			theApp.m_showMain = true ;
			theApp.m_ActivatOK = TRUE ;
			theApp.m_ProKey1 = m_ProKey1;	//	J34FJP
			theApp.m_ProKey2 = m_ProKey2;	//	JRJRJ3
			theApp.m_ProKey3 = m_ProKey3;	//	JFKP47
			theApp.m_ProKey4 = m_ProKey4;	//	J7KB6M
			theApp.m_ProKey5 = m_ProKey5;	//	4RKR63

//	theApp.StatusChanged();
			OnOK();
		}
	}
	else {
	}
}

void CProKeyDlg::OnCancel() 
{
	// TODO: この位置に特別な後処理を追加してください。
	theApp.m_ProKey1 = m_ProKey1;	//	J34FJP
	theApp.m_ProKey2 = m_ProKey2;	//	JRJRJ3
	theApp.m_ProKey3 = m_ProKey3;	//	JFKP47
	theApp.m_ProKey4 = m_ProKey4;	//	J7KB6M
	theApp.m_ProKey5 = m_ProKey5;	//	4RKR63
	if ((theApp.m_ProKeyOK = CheckProkeyVal ()) == FALSE && theApp.m_ActivatOK == FALSE) {
		MessageBox ("認証残日数以内にライセンス認証を完了して下さい。\nライセンス認証完了まで毎回このライセンス認証画面が起動します。\n期日を過ぎるとメイン画面は起動しなくなります。", "インフォメーション", MB_OK | MB_ICONWARNING) ;
		if (pp_expired (theApp.m_lfhandle) == PP_FALSE) {
			theApp.m_showMain = true ;
		}
		else {
			theApp.m_showMain = false ;
		}
	}
	else {
		if (theApp.m_ActivatOK == TRUE) {
			theApp.m_showMain = true ;
		}
		else {
			if (pp_expired (theApp.m_lfhandle) == PP_FALSE) {
				MessageBox ("認証残日数以内にライセンス認証を完了して下さい。\nライセンス認証完了まで毎回このライセンス認証画面が起動します。\n期日を過ぎるとメイン画面は起動しなくなります。", "インフォメーション", MB_OK | MB_ICONWARNING) ;
				theApp.m_showMain = true ;
			}
			else {
				theApp.m_showMain = false ;
			}
		}
	}
	CDialog::OnCancel();
}

void CProKeyDlg::OnChangeEditProkey1() 
{
	// TODO: これが RICHEDIT コントロールの場合、コントロールは、 lParam マスク
	// 内での論理和の ENM_CHANGE フラグ付きで CRichEditCrtl().SetEventMask()
	// メッセージをコントロールへ送るために CDialog::OnInitDialog() 関数をオーバー
	// ライドしない限りこの通知を送りません。
	
	// TODO: この位置にコントロール通知ハンドラ用のコードを追加してください
	BOOL	b_enb	;
	UpdateData (TRUE) ;
	if (m_ProKey1.GetLength () == 5) {
		if (GetDlgItem (IDC_EDIT_PROKEY1) == GetFocus ()) {
			GetDlgItem (IDC_EDIT_PROKEY2)->SetFocus () ;
			b_enb = CheckProkeyVal() ;
//			GetDlgItem (IDOK)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (b_enb) ;
		}
		else {
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
		}
	}
	else {
		GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
		GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
	}
}

void CProKeyDlg::OnChangeEditProkey2() 
{
	// TODO: これが RICHEDIT コントロールの場合、コントロールは、 lParam マスク
	// 内での論理和の ENM_CHANGE フラグ付きで CRichEditCrtl().SetEventMask()
	// メッセージをコントロールへ送るために CDialog::OnInitDialog() 関数をオーバー
	// ライドしない限りこの通知を送りません。
	
	// TODO: この位置にコントロール通知ハンドラ用のコードを追加してください
	BOOL	b_enb	;
	UpdateData (TRUE) ;
	if (m_ProKey2.GetLength () == 5) {
		if (GetDlgItem (IDC_EDIT_PROKEY2) == GetFocus ()) {
			GetDlgItem (IDC_EDIT_PROKEY3)->SetFocus () ;
			b_enb = CheckProkeyVal() ;
//			GetDlgItem (IDOK)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (b_enb) ;
		}
		else {
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
		}
	}
	else {
		GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
		GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
	}
}

void CProKeyDlg::OnChangeEditProkey3() 
{
	// TODO: これが RICHEDIT コントロールの場合、コントロールは、 lParam マスク
	// 内での論理和の ENM_CHANGE フラグ付きで CRichEditCrtl().SetEventMask()
	// メッセージをコントロールへ送るために CDialog::OnInitDialog() 関数をオーバー
	// ライドしない限りこの通知を送りません。
	
	// TODO: この位置にコントロール通知ハンドラ用のコードを追加してください
	BOOL	b_enb	;
	UpdateData (TRUE) ;
	if (m_ProKey3.GetLength () == 5) {
		if (GetDlgItem (IDC_EDIT_PROKEY3) == GetFocus ()) {
			GetDlgItem (IDC_EDIT_PROKEY4)->SetFocus () ;
			b_enb = CheckProkeyVal() ;
//			GetDlgItem (IDOK)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (b_enb) ;
		}
		else {
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
		}
	}
	else {
		GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
		GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
	}
}

void CProKeyDlg::OnChangeEditProkey4() 
{
	// TODO: これが RICHEDIT コントロールの場合、コントロールは、 lParam マスク
	// 内での論理和の ENM_CHANGE フラグ付きで CRichEditCrtl().SetEventMask()
	// メッセージをコントロールへ送るために CDialog::OnInitDialog() 関数をオーバー
	// ライドしない限りこの通知を送りません。
	
	// TODO: この位置にコントロール通知ハンドラ用のコードを追加してください
	BOOL	b_enb	;
	UpdateData (TRUE) ;
	if (m_ProKey4.GetLength () == 5) {
		if (GetDlgItem (IDC_EDIT_PROKEY4) == GetFocus ()) {
			GetDlgItem (IDC_EDIT_PROKEY5)->SetFocus () ;
			b_enb = CheckProkeyVal() ;
//			GetDlgItem (IDOK)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (b_enb) ;
		}
		else {
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
		}
	}
	else {
		GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
		GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
	}
}

void CProKeyDlg::OnChangeEditProkey5() 
{
	// TODO: これが RICHEDIT コントロールの場合、コントロールは、 lParam マスク
	// 内での論理和の ENM_CHANGE フラグ付きで CRichEditCrtl().SetEventMask()
	// メッセージをコントロールへ送るために CDialog::OnInitDialog() 関数をオーバー
	// ライドしない限りこの通知を送りません。
	
	// TODO: この位置にコントロール通知ハンドラ用のコードを追加してください
	BOOL	b_enb	;
	UpdateData (TRUE) ;
	if (m_ProKey5.GetLength () == 5) {
		if (GetDlgItem (IDC_EDIT_PROKEY5) == GetFocus ()) {
			b_enb = CheckProkeyVal() ;
//			GetDlgItem (IDOK)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (b_enb) ;
			GetDlgItem (IDC_BUTTON_ACT2)->SetFocus () ;
		}
		else {
			GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
			GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
		}
	}
	else {
		GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (FALSE) ;
		GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (FALSE) ;
	}
}

BOOL CProKeyDlg::CheckProkeyVal()
{
	CSerialKey	proChk	;
	CString		cs_work	;
	char		c_LicenseID[16]	,
				c_LicensePW[16]	;

	memset (c_LicenseID, 0, sizeof (char) * 16) ;
	memset (c_LicensePW, 0, sizeof (char) * 16) ;
	cs_work = m_ProKey1 + m_ProKey2 + m_ProKey3 + m_ProKey4 + m_ProKey5 ;
	if (cs_work.GetLength () == 25) {
		if (proChk.Decode (cs_work, c_LicenseID, c_LicensePW) == true) {
			m_LicenseID = atoi (c_LicenseID) ;
			m_LicensePW = c_LicensePW ;
			return (TRUE) ;
		}
	}
	return (FALSE) ;
}

void CProKeyDlg::OnPaint() 
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
