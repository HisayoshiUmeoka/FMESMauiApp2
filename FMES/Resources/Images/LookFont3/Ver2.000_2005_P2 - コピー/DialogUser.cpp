// DialogUser.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "lookfont.h"
#include "DialogUser.h"
#include <keylib.h>
#include <Skca.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CDialogUser ダイアログ


CDialogUser::CDialogUser(CWnd* pParent /*=NULL*/)
	: CDialog(CDialogUser::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDialogUser)
	m_strBirth1 = _T("");
	m_strBirth2 = _T("");
	m_strBirth3 = _T("");
	m_strCompany = _T("");
	m_strName1 = _T("");
	m_strName2 = _T("");
	m_strZip1 = _T("");
	m_strZip2 = _T("");
	m_strEmail = _T("");
	//}}AFX_DATA_INIT
	m_hIcon = AfxGetApp()->LoadIcon(IDI_ICON_LICENCE);
	m_LicenseID = 0;
	m_LicensePW = "";
}


void CDialogUser::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDialogUser)
	DDX_Control(pDX, IDC_EDIT_FAX3, m_Fax3);
	DDX_Control(pDX, IDC_EDIT_FAX2, m_Fax2);
	DDX_Control(pDX, IDC_EDIT_FAX1, m_Fax1);
	DDX_Control(pDX, IDC_EDIT_ZIP2, m_Zip2);
	DDX_Control(pDX, IDC_EDIT_ZIP1, m_Zip1);
	DDX_Control(pDX, IDC_EDIT_PHONE3, m_Tel3);
	DDX_Control(pDX, IDC_EDIT_PHONE2, m_Tel2);
	DDX_Control(pDX, IDC_EDIT_PHONE1, m_Tel1);
	DDX_Control(pDX, IDC_EDIT_BIRTH3, m_Birth3);
	DDX_Control(pDX, IDC_EDIT_BIRTH2, m_Birth2);
	DDX_Control(pDX, IDC_EDIT_BIRTH1, m_Birth1);
	DDX_Text(pDX, IDC_EDIT_BIRTH1, m_strBirth1);
	DDV_MaxChars(pDX, m_strBirth1, 4);
	DDX_Text(pDX, IDC_EDIT_BIRTH2, m_strBirth2);
	DDV_MaxChars(pDX, m_strBirth2, 2);
	DDX_Text(pDX, IDC_EDIT_BIRTH3, m_strBirth3);
	DDV_MaxChars(pDX, m_strBirth3, 2);
	DDX_Text(pDX, IDC_EDIT_COMPANY, m_strCompany);
	DDV_MaxChars(pDX, m_strCompany, 40);
	DDX_Text(pDX, IDC_EDIT_NAME1, m_strName1);
	DDV_MaxChars(pDX, m_strName1, 20);
	DDX_Text(pDX, IDC_EDIT_NAME2, m_strName2);
	DDV_MaxChars(pDX, m_strName2, 18);
	DDX_Text(pDX, IDC_EDIT_ZIP1, m_strZip1);
	DDV_MaxChars(pDX, m_strZip1, 3);
	DDX_Text(pDX, IDC_EDIT_ZIP2, m_strZip2);
	DDV_MaxChars(pDX, m_strZip2, 4);
	DDX_Text(pDX, IDC_EDIT_EMAIL, m_strEmail);
	DDV_MaxChars(pDX, m_strEmail, 40);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CDialogUser, CDialog)
	//{{AFX_MSG_MAP(CDialogUser)
	ON_BN_CLICKED(IDC_BUTTON_REGIST, OnButtonRegist)
	ON_BN_CLICKED(IDC_BUTTON_PRIVACY, OnButtonPrivacy)
	ON_WM_PAINT()
	ON_WM_SYSCOMMAND()
	ON_EN_CHANGE(IDC_EDIT_COMPANY, OnChangeEditCompany)
	ON_EN_CHANGE(IDC_EDIT_NAME1, OnChangeEditName1)
	ON_EN_CHANGE(IDC_EDIT_NAME2, OnChangeEditName2)
	ON_CBN_SELCHANGE(IDC_COMBO_SEX, OnSelchangeComboSex)
	ON_EN_CHANGE(IDC_EDIT_BIRTH1, OnChangeEditBirth1)
	ON_EN_CHANGE(IDC_EDIT_BIRTH2, OnChangeEditBirth2)
	ON_EN_CHANGE(IDC_EDIT_BIRTH3, OnChangeEditBirth3)
	ON_CBN_SELCHANGE(IDC_COMBO_PREF, OnSelchangeComboPref)
	ON_EN_CHANGE(IDC_EDIT_ADR1, OnChangeEditAdr1)
	ON_EN_CHANGE(IDC_EDIT_ADR2, OnChangeEditAdr2)
	ON_EN_CHANGE(IDC_EDIT_ADR3, OnChangeEditAdr3)
	ON_EN_CHANGE(IDC_EDIT_PHONE1, OnChangeEditPhone1)
	ON_EN_CHANGE(IDC_EDIT_PHONE2, OnChangeEditPhone2)
	ON_EN_CHANGE(IDC_EDIT_PHONE3, OnChangeEditPhone3)
	ON_EN_CHANGE(IDC_EDIT_ZIP1, OnChangeEditZip1)
	ON_EN_CHANGE(IDC_EDIT_ZIP2, OnChangeEditZip2)
	ON_EN_CHANGE(IDC_EDIT_FAX1, OnChangeEditFax1)
	ON_EN_CHANGE(IDC_EDIT_FAX2, OnChangeEditFax2)
	ON_EN_CHANGE(IDC_EDIT_FAX3, OnChangeEditFax3)
	ON_EN_CHANGE(IDC_EDIT_EMAIL, OnChangeEditEmail)
	ON_CBN_SELCHANGE(IDC_COMBO_OCCU, OnSelchangeComboOccu)
	ON_CBN_SELCHANGE(IDC_COMBO_JOB, OnSelchangeComboJob)
	ON_CBN_SELCHANGE(IDC_COMBO_DM, OnSelchangeComboDm)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDialogUser メッセージ ハンドラ

BOOL CDialogUser::OnInitDialog() 
{
	CDialog::OnInitDialog();
	SetIcon(m_hIcon, TRUE);			// 大きいアイコンの設定
	SetIcon(m_hIcon, FALSE);		// 小さいアイコンの設定
	
	// TODO: この位置に初期化の補足処理を追加してください
	InitItems () ;
	return TRUE;  // コントロールにフォーカスを設定しないとき、戻り値は TRUE となります
	              // 例外: OCX プロパティ ページの戻り値は FALSE となります
}

void CDialogUser::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == SC_CLOSE)
	{
		CDialog::OnCancel();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

void CDialogUser::OnPaint() 
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

void CDialogUser::OnOK() 
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
	CComboBox *cmbWork	;
	int		i_idx		;

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
	LONG	regid = m_LicenseID	;

	memset (firstname, 0, sizeof (char) * 100) ;
	memset (lastname, 0, sizeof (char) * 100) ;
	memset (email, 0, sizeof (char) * 100) ;
	memset (phone, 0, sizeof (char) * 100) ;
	memset (ud1, 0, sizeof (char) * 100) ;
	memset (ud2, 0, sizeof (char) * 100) ;
	memset (ud3, 0, sizeof (char) * 100) ;
	memset (ud4, 0, sizeof (char) * 100) ;
	memset (ud5, 0, sizeof (char) * 100) ;

	strcpy (email, (char *)(const char*)m_LicensePW) ;	/* パスワード用	*/
	_stprintf (ud5, "%d", m_LicenseID) ;					/* ライセンスＩＤ用	*/

	GetDlgItemText(IDC_EDIT_COMPANY, cs_work1) ;
	if (cs_work1.GetLength () > 0) {
		strcpy (firstname, (char *)(const char*)cs_work1) ;	/* 会社名用	*/
	}

	GetDlgItemText(IDC_EDIT_NAME1, cs_work2) ;
	GetDlgItemText(IDC_EDIT_NAME2, cs_work3) ;
	cs_work1 = cs_work2 + "　" + cs_work3 ;
	strcpy (lastname, (char *)(const char*)cs_work1) ;	/* 氏名用	*/

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_PREF)) ;
	if ((i_idx = cmbWork->GetCurSel ()) > -1) {
		cmbWork->GetLBText(i_idx, cs_work1) ;
	}
	strcpy (ud1, (char *)(const char*)cs_work1) ;	/* 都道府県用	*/

	GetDlgItemText(IDC_EDIT_ADR1, cs_work2) ;
	GetDlgItemText(IDC_EDIT_ADR2, cs_work3) ;
	cs_work1 = cs_work2 + cs_work3 ;
	if (cs_work1.GetLength () > 0) {
		strcpy (ud2, (char *)(const char*)cs_work1) ;	/* 住所１＋２用	*/
	}

	GetDlgItemText(IDC_EDIT_ADR3, cs_work1) ;
	if (cs_work1.GetLength () > 0) {
		strcpy (ud3, (char *)(const char*)cs_work1) ;	/* 住所３用	*/
	}

	GetDlgItemText(IDC_EDIT_PHONE1, cs_work1) ;
	GetDlgItemText(IDC_EDIT_PHONE2, cs_work2) ;
	GetDlgItemText(IDC_EDIT_PHONE3, cs_work3) ;
	cs_work1 += "-" + cs_work2 + "-" + cs_work3 ;
	strcpy (phone, (char *)(const char*)cs_work1) ;	/* 電話番号用	*/

	GetDlgItemText(IDC_EDIT_EMAIL, cs_work1) ;
	strcpy (email, (char *)(const char*)cs_work1) ;	/* ＥＭａｉｌ用	*/

	cs_asp = "/unlock/postuserregdata.asp" ;
	if ((result = SK_PostEvalData((char *)(const char*)cs_Url, proxy, (char *)(const char*)cs_asp, firstname, lastname, email, phone, ud1, ud2, ud3, ud4, ud5, (LPLONG)&regid)) != 0) {
		regid = m_LicenseID ;
		if ((result = SK_PostEvalDataEx(SK_SECURE_CONNECTION, (char *)(const char*)cs_Url, proxy, (char *)(const char*)cs_asp, firstname, lastname, email, phone, ud1, ud2, ud3, ud4, ud5, (LPLONG)&regid)) != 0) {
			cs_err.Format ("ユーザー情報登録エラー エラーコード:%d\nこのライセンス（プロダクトキー）はご利用を許可されておりません。\n弊社サポートまでご連絡下さい。", result) ;
			MessageBox(cs_err, "オンラインユーザー登録　エラー", MB_OK | MB_ICONERROR);
			CDialog::OnCancel();
			return ;
		}
	}

	memset (firstname, 0, sizeof (char) * 100) ;
	memset (lastname, 0, sizeof (char) * 100) ;
	memset (email, 0, sizeof (char) * 100) ;
	memset (phone, 0, sizeof (char) * 100) ;
	memset (ud1, 0, sizeof (char) * 100) ;
	memset (ud2, 0, sizeof (char) * 100) ;
	memset (ud3, 0, sizeof (char) * 100) ;
	memset (ud4, 0, sizeof (char) * 100) ;

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_SEX)) ;
	_stprintf (firstname, "%d", cmbWork->GetCurSel ()) ;		/* 性別用	*/

	GetDlgItemText(IDC_EDIT_BIRTH1, cs_work1) ;
	GetDlgItemText(IDC_EDIT_BIRTH2, cs_work2) ;
	GetDlgItemText(IDC_EDIT_BIRTH3, cs_work3) ;
	cs_work.Format ("%d/%02d/%02d", atoi ((const char *)cs_work1), atoi ((const char *)cs_work2), atoi ((const char *)cs_work3)) ;
	strcpy (lastname, (char *)(const char*)cs_work) ;	/* 生年月日用	*/

	GetDlgItemText(IDC_EDIT_ZIP1, cs_work1) ;
	GetDlgItemText(IDC_EDIT_ZIP2, cs_work2) ;
	cs_work1 += "-" + cs_work2 ;
	strcpy (ud1, (char *)(const char*)cs_work1) ;	/* 〒用	*/

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_DM)) ;
	_stprintf (ud2, "%d", cmbWork->GetCurSel ()) ;		/* DM用	*/

	GetDlgItemText(IDC_EDIT_FAX1, cs_work1) ;
	GetDlgItemText(IDC_EDIT_FAX2, cs_work2) ;
	GetDlgItemText(IDC_EDIT_FAX3, cs_work3) ;
	cs_work1 += "-" + cs_work2 + "-" + cs_work3 ;
	strcpy (phone, (char *)(const char*)cs_work1) ;	/* 電話番号用	*/

	cs_asp = "/unlock/postuserregdata2.asp" ;
	if ((result = SK_PostEvalData((char *)(const char*)cs_Url, proxy, (char *)(const char*)cs_asp, firstname, lastname, email, phone, ud1, ud2, ud3, ud4, ud5, (LPLONG)&regid)) != 0) {
		regid = m_LicenseID ;
		if ((result = SK_PostEvalDataEx(SK_SECURE_CONNECTION, (char *)(const char*)cs_Url, proxy, (char *)(const char*)cs_asp, firstname, lastname, email, phone, ud1, ud2, ud3, ud4, ud5, (LPLONG)&regid)) != 0) {
			cs_err.Format ("ユーザー情報登録エラー エラーコード:%d\nこのライセンス（プロダクトキー）はご利用を許可されておりません。\n弊社サポートまでご連絡下さい。", result) ;
			MessageBox(cs_err, "オンラインユーザー登録　エラー", MB_OK | MB_ICONERROR);
			CDialog::OnCancel();
			return ;
		}
	}

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_OCCU)) ;
	_stprintf (ud1, "%d", (cmbWork->GetCurSel ()+1)) ;		/* 職業用	*/

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_JOB)) ;
	_stprintf (ud2, "%d", (cmbWork->GetCurSel ()+1)) ;		/* 職種用	*/

	cs_asp = "/unlock/postuserregdata3.asp" ;
	if ((result = SK_PostEvalData((char *)(const char*)cs_Url, proxy, (char *)(const char*)cs_asp, firstname, lastname, email, phone, ud1, ud2, ud3, ud4, ud5, (LPLONG)&regid)) != 0) {
		regid = m_LicenseID ;
		if ((result = SK_PostEvalDataEx(SK_SECURE_CONNECTION, (char *)(const char*)cs_Url, proxy, (char *)(const char*)cs_asp, firstname, lastname, email, phone, ud1, ud2, ud3, ud4, ud5, (LPLONG)&regid)) != 0) {
			cs_err.Format ("ユーザー情報登録エラー エラーコード:%d\nこのライセンス（プロダクトキー）はご利用を許可されておりません。\n弊社サポートまでご連絡下さい。", result) ;
			MessageBox(cs_err, "オンラインユーザー登録　エラー", MB_OK | MB_ICONERROR);
			CDialog::OnCancel();
			return ;
		}
	}
	MessageBox("オンラインユーザー登録が完了しました。", "インフォメーション", MB_OK | MB_ICONINFORMATION) ;
	CDialog::OnOK();
}

void CDialogUser::OnButtonRegist() 
{
	CString	csUrl	;
	csUrl = "https://www.ss.pluss-inc.com/regist/index.html" ;
	ShellExecute (NULL, _T("open"), csUrl, NULL, NULL, SW_SHOWNORMAL) ;
}

void CDialogUser::OnButtonPrivacy() 
{
	CString	csUrl	;
	csUrl = "http://www.ss.pluss-inc.com/privasy/index.html" ;
	ShellExecute (NULL, _T("open"), csUrl, NULL, NULL, SW_SHOWNORMAL) ;
}

void CDialogUser::InitItems()
{
	BOOL	b_enb = CheckUserInfoVal () ;
//		GetDlgItem (IDC_BUTTON_ACT2)->EnableWindow (b_enb2) ;
//		GetDlgItem (IDC_BUTTON_ACT3)->EnableWindow (b_enb) ;
	CComboBox *cmbWork	;
	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_SEX)) ;
	cmbWork->SetCurSel (cmbWork->AddString ("男性")) ;
	cmbWork->AddString ("女性") ;

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_PREF)) ;
	cmbWork->AddString ("北海道") ;
	cmbWork->AddString ("青森県") ;
	cmbWork->AddString ("岩手県") ;
	cmbWork->AddString ("宮城県") ;
	cmbWork->AddString ("秋田県") ;
	cmbWork->AddString ("山形県") ;
	cmbWork->AddString ("福島県") ;
	cmbWork->AddString ("茨城県") ;
	cmbWork->AddString ("栃木県") ;
	cmbWork->AddString ("群馬県") ;
	cmbWork->AddString ("埼玉県") ;
	cmbWork->AddString ("千葉県") ;
	cmbWork->AddString ("東京都") ;
	cmbWork->AddString ("神奈川県") ;
	cmbWork->AddString ("新潟県") ;
	cmbWork->AddString ("富山県") ;
	cmbWork->AddString ("石川県") ;
	cmbWork->AddString ("福井県") ;
	cmbWork->AddString ("山梨県") ;
	cmbWork->AddString ("長野県") ;
	cmbWork->AddString ("岐阜県") ;
	cmbWork->AddString ("静岡県") ;
	cmbWork->AddString ("愛知県") ;
	cmbWork->AddString ("三重県") ;
	cmbWork->AddString ("滋賀県") ;
	cmbWork->AddString ("京都府") ;
	cmbWork->AddString ("大阪府") ;
	cmbWork->AddString ("兵庫県") ;
	cmbWork->AddString ("奈良県") ;
	cmbWork->AddString ("和歌山県") ;
	cmbWork->AddString ("鳥取県") ;
	cmbWork->AddString ("島根県") ;
	cmbWork->AddString ("岡山県") ;
	cmbWork->AddString ("広島県") ;
	cmbWork->AddString ("山口県") ;
	cmbWork->AddString ("徳島県") ;
	cmbWork->AddString ("香川県") ;
	cmbWork->AddString ("愛媛県") ;
	cmbWork->AddString ("高知県") ;
	cmbWork->AddString ("福岡県") ;
	cmbWork->AddString ("佐賀県") ;
	cmbWork->AddString ("長崎県") ;
	cmbWork->AddString ("熊本県") ;
	cmbWork->AddString ("大分県") ;
	cmbWork->AddString ("宮崎県") ;
	cmbWork->AddString ("鹿児島県") ;
	cmbWork->AddString ("沖縄県") ;

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_OCCU)) ;
	cmbWork->AddString ("製造業") ;
	cmbWork->AddString ("サービス業") ;
	cmbWork->AddString ("小売り") ;
	cmbWork->AddString ("IT") ;
	cmbWork->AddString ("広告") ;
	cmbWork->AddString ("デザイン") ;
	cmbWork->AddString ("公務員") ;
	cmbWork->AddString ("その他") ;

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_JOB)) ;
	cmbWork->AddString ("営業") ;
	cmbWork->AddString ("技術") ;
	cmbWork->AddString ("企画") ;
	cmbWork->AddString ("事務") ;
	cmbWork->AddString ("管理職") ;
	cmbWork->AddString ("会社役員") ;
	cmbWork->AddString ("個人事業主") ;
	cmbWork->AddString ("その他") ;

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_DM)) ;
	cmbWork->SetCurSel (cmbWork->AddString ("許可")) ;
	cmbWork->AddString ("禁止") ;
	GetDlgItem (IDOK)->EnableWindow (b_enb) ;
}

BOOL CDialogUser::CheckUserInfoVal()
{
	CString		cs_work	,
				cs_work2;
	CComboBox	*cmbWork	;

	GetDlgItemText(IDC_EDIT_NAME1, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_NAME1)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_NAME2, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_NAME2)))->SetFocus () ;
		return (FALSE) ;
	}

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_SEX)) ;
	if (cmbWork->GetCurSel () == -1) {
//				cmbWork->SetFocus () ;
		return (FALSE) ;
	}

	GetDlgItemText(IDC_EDIT_BIRTH1, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_BIRTH1)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_BIRTH2, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_BIRTH2)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_BIRTH3, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_BIRTH3)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_ZIP1, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_ZIP1)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_ZIP2, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_ZIP2)))->SetFocus () ;
		return (FALSE) ;
	}

	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_PREF)) ;
	if (cmbWork->GetCurSel () == -1) {
//				cmbWork->SetFocus () ;
		return (FALSE) ;
	}

	GetDlgItemText(IDC_EDIT_ADR1, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_ADR1)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_ADR2, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_ADR2)))->SetFocus () ;
		return (FALSE) ;
	}

	GetDlgItemText(IDC_EDIT_PHONE1, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_PHONE1)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_PHONE2, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_PHONE2)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_PHONE3, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_PHONE3)))->SetFocus () ;
		return (FALSE) ;
	}
	GetDlgItemText(IDC_EDIT_EMAIL, cs_work2) ;
	cs_work2.TrimLeft () ;
	cs_work2.TrimRight () ;
	if (cs_work2.GetLength () == 0 || cs_work2.FindOneOf ("@") == -1) {
//				((CWnd *)(GetDlgItem (IDC_EDIT_PHONE3)))->SetFocus () ;
		return (FALSE) ;
	}
	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_OCCU)) ;
	if (cmbWork->GetCurSel () == -1) {
//				cmbWork->SetFocus () ;
		return (FALSE) ;
	}
	cmbWork = (CComboBox *)(GetDlgItem (IDC_COMBO_JOB)) ;
	if (cmbWork->GetCurSel () == -1) {
//				cmbWork->SetFocus () ;
		return (FALSE) ;
	}
	return (TRUE) ;
}

void CDialogUser::OnChangeEditCompany() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditName1() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditName2() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnSelchangeComboSex() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditBirth1() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditBirth2() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditBirth3() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnSelchangeComboPref() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditAdr1() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditAdr2() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditAdr3() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditPhone1() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditPhone2() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditPhone3() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditZip1() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditZip2() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditFax1() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditFax2() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditFax3() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnChangeEditEmail() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnSelchangeComboOccu() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnSelchangeComboJob() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}

void CDialogUser::OnSelchangeComboDm() 
{
	UpdateData (TRUE) ;
	GetDlgItem (IDOK)->EnableWindow (CheckUserInfoVal()) ;
}
