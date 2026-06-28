#if !defined(AFX_SETTINGDLG_H__32938478_111A_43CD_8CB2_6DE79FCE108A__INCLUDED_)
#define AFX_SETTINGDLG_H__32938478_111A_43CD_8CB2_6DE79FCE108A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SettingDlg.h : ヘッダー ファイル
//

/////////////////////////////////////////////////////////////////////////////
// CSettingDlg
#include "LookFontSetting.h"
#include "SettingUpdateDlg.h"


class CSettingDlg : public CPropertySheet
{
	DECLARE_DYNAMIC(CSettingDlg)

// コンストラクション
public:
	CSettingDlg(UINT nIDCaption, CWnd* pParentWnd = NULL, UINT iSelectPage = 0);
	CSettingDlg(LPCTSTR pszCaption, CWnd* pParentWnd = NULL, UINT iSelectPage = 0);

// アトリビュート
public:

// オペレーション
public:

// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CSettingDlg)
	public:
	//}}AFX_VIRTUAL

// インプリメンテーション
public:
	virtual ~CSettingDlg();
	virtual int DoModal(CLookFontSetting* pIni);

	// 生成されたメッセージ マップ関数
protected:
	//{{AFX_MSG(CSettingDlg)
	afx_msg void OnHelp();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	CSettingUpdateDlg	m_wndUpdateDlg;
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_SETTINGDLG_H__32938478_111A_43CD_8CB2_6DE79FCE108A__INCLUDED_)
