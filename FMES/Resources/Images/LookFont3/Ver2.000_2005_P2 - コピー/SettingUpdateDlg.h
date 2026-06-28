#if !defined(AFX_SETTINGUPDATEDLG_H__7C65A697_9EB5_4815_922D_1C7EE36AB4F2__INCLUDED_)
#define AFX_SETTINGUPDATEDLG_H__7C65A697_9EB5_4815_922D_1C7EE36AB4F2__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SettingUpdateDlg.h : ヘッダー ファイル
//

/////////////////////////////////////////////////////////////////////////////
// CSettingUpdateDlg ダイアログ
#include "LookFontSetting.h"


class CSettingUpdateDlg : public CPropertyPage
{
	DECLARE_DYNCREATE(CSettingUpdateDlg)

// コンストラクション
public:
	CSettingUpdateDlg();
	~CSettingUpdateDlg();

	CLookFontSetting*	m_pIni;
// ダイアログ データ
	//{{AFX_DATA(CSettingUpdateDlg)
	enum { IDD = IDD_DIALOG_SETTING_UPDATE };
	BOOL	m_bAUTOUPDATE;
	//}}AFX_DATA


// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。

	//{{AFX_VIRTUAL(CSettingUpdateDlg)
	public:
	virtual void OnOK();
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:
	void InitData(BOOL bInit);
	void UpdateButton();

	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CSettingUpdateDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnItemModify();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

public:
	afx_msg void OnCbnSelchangeComboFavmax();
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_SETTINGUPDATEDLG_H__7C65A697_9EB5_4815_922D_1C7EE36AB4F2__INCLUDED_)
