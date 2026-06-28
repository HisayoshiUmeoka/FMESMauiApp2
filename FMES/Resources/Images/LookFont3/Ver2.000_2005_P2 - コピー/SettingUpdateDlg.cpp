// SettingUpdateDlg.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "lookfont.h"
#include "SettingUpdateDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSettingUpdateDlg プロパティ ページ

IMPLEMENT_DYNCREATE(CSettingUpdateDlg, CPropertyPage)

CSettingUpdateDlg::CSettingUpdateDlg() : CPropertyPage(CSettingUpdateDlg::IDD)
{
	//{{AFX_DATA_INIT(CSettingUpdateDlg)
	m_bAUTOUPDATE = FALSE;
	//}}AFX_DATA_INIT
}

CSettingUpdateDlg::~CSettingUpdateDlg()
{
}

void CSettingUpdateDlg::DoDataExchange(CDataExchange* pDX)
{
	CPropertyPage::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSettingUpdateDlg)
	DDX_Check(pDX, IDC_CHECK_AUTOUPDATE, m_bAUTOUPDATE);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CSettingUpdateDlg, CPropertyPage)
	//{{AFX_MSG_MAP(CSettingUpdateDlg)
	ON_BN_CLICKED(IDC_CHECK_AUTOUPDATE, OnItemModify)
	//}}AFX_MSG_MAP
	ON_CBN_SELCHANGE(IDC_COMBO_FAVMAX, &CSettingUpdateDlg::OnCbnSelchangeComboFavmax)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// システムメッセージ ハンドラ
/////////////////////////////////////////////////////////////////////////////

BOOL CSettingUpdateDlg::OnInitDialog() 
{
	CPropertyPage::OnInitDialog();

	CComboBox* pCb = (CComboBox*)(GetDlgItem(IDC_COMBO_FAVMAX)) ;
	int	iIndex;
	iIndex = pCb->AddString("20");
	pCb->SetItemData(iIndex, 20);
	iIndex = pCb->AddString("30");
	pCb->SetItemData(iIndex, 30);
	iIndex = pCb->AddString("40");
	pCb->SetItemData(iIndex, 40);
	iIndex = pCb->AddString("50");
	pCb->SetItemData(iIndex, 50);
	iIndex = pCb->AddString("60");
	pCb->SetItemData(iIndex, 60);
	iIndex = pCb->AddString("70");
	pCb->SetItemData(iIndex, 70);
	iIndex = pCb->AddString("80");
	pCb->SetItemData(iIndex, 80);
	iIndex = pCb->AddString("90");
	pCb->SetItemData(iIndex, 90);

	InitData(TRUE);

	return TRUE;  // コントロールにフォーカスを設定しないとき、戻り値は TRUE となります
	              // 例外: OCX プロパティ ページの戻り値は FALSE となります
}

void CSettingUpdateDlg::OnOK() 
{
	InitData(FALSE);

	CPropertyPage::OnOK();
}

/////////////////////////////////////////////////////////////////////////////
// アイテムメッセージ ハンドラ
/////////////////////////////////////////////////////////////////////////////

void CSettingUpdateDlg::OnItemModify() 
{
	SetModified();
}

/////////////////////////////////////////////////////////////////////////////
// 内部関数
/////////////////////////////////////////////////////////////////////////////

// データ設定・保存
//	bInit		I	TRUE:情報表示 FALSE:表示内容取り込み
void CSettingUpdateDlg::InitData(BOOL bInit)
{
	int	iMax ;
	if(!bInit) {
		UpdateData();
		CComboBox* pCb = (CComboBox*)(GetDlgItem(IDC_COMBO_FAVMAX)) ;
		int iSel ;
		if ((iSel = pCb->GetCurSel()) > -1) {
			iMax = pCb->GetItemData (iSel) ;
		}
		else {
			iMax = MAX_FAVARTE_NO ;
		}
	}

#define	ITEMDATA(l,i) if(bInit) {l = m_pIni->i;} else {m_pIni->i = l;}
	ITEMDATA(m_bAUTOUPDATE, AutoUpdate);
	ITEMDATA(iMax, FavMax);

	if(bInit) {
		UpdateData(FALSE);
		UpdateButton();
		int		i	;
		CComboBox* pCb = (CComboBox*)(GetDlgItem(IDC_COMBO_FAVMAX)) ;
		for (i = 0 ; i < pCb->GetCount() ; i++) {
			if (iMax == pCb->GetItemData (i)) {
				pCb->SetCurSel(i) ;
				break ;
			}
		}
	}
}

// アイテムEnable設定
void CSettingUpdateDlg::UpdateButton()
{
	UpdateData();

	// 今のところ特に処理なし

	UpdateData(FALSE);
}

void CSettingUpdateDlg::OnCbnSelchangeComboFavmax()
{
	InitData(FALSE);
}
