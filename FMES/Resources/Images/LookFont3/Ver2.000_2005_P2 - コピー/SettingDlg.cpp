// SettingDlg.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "lookfont.h"
#include "SettingDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSettingDlg

IMPLEMENT_DYNAMIC(CSettingDlg, CPropertySheet)

CSettingDlg::CSettingDlg(UINT nIDCaption, CWnd* pParentWnd, UINT iSelectPage)
	:CPropertySheet(nIDCaption, pParentWnd, iSelectPage)
{
}

CSettingDlg::CSettingDlg(LPCTSTR pszCaption, CWnd* pParentWnd, UINT iSelectPage)
	:CPropertySheet(pszCaption, pParentWnd, iSelectPage)
{
}

CSettingDlg::~CSettingDlg()
{
}


BEGIN_MESSAGE_MAP(CSettingDlg, CPropertySheet)
	//{{AFX_MSG_MAP(CSettingDlg)
	ON_COMMAND(ID_HELP, OnHelp)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// システムメッセージハンドラ
/////////////////////////////////////////////////////////////////////////////

int CSettingDlg::DoModal(CLookFontSetting* pIni) 
{
	m_psh.dwFlags |= PSH_NOAPPLYNOW;

	m_wndUpdateDlg.m_pIni = pIni;

	AddPage(&m_wndUpdateDlg);

	return CPropertySheet::DoModal();
}

void CSettingDlg::OnHelp() 
{
	// TODO: この位置にコマンド ハンドラ用のコードを追加してください
}
