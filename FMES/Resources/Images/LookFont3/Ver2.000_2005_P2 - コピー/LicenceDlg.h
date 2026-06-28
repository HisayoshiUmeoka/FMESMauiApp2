#if !defined(AFX_LICENCEDLG_H__3B2C35D3_DF9E_4F1D_9BBE_DED64BF777D6__INCLUDED_)
#define AFX_LICENCEDLG_H__3B2C35D3_DF9E_4F1D_9BBE_DED64BF777D6__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// LicenceDlg.h : ヘッダー ファイル
//

/////////////////////////////////////////////////////////////////////////////
// CLicenceDlg ダイアログ

class CLicenceDlg : public CDialog
{
// コンストラクション
public:
	CLicenceDlg(CWnd* pParent = NULL);   // 標準のコンストラクタ

	HICON	m_hIcon;
	DWORD	m_dwXTBtnStyle;
	BOOL    m_bWinXPThemes;
	BOOL    m_bWordTheme;
	int     m_intTheme;
	BOOL    m_bAlpha;
	bool	m_expired;

	// ダイアログ データ
	//{{AFX_DATA(CLicenceDlg)
	enum { IDD = IDD_DIALOG_LICENCE };
		CXTButton m_btnKey1;
		CXTButton m_btnKey2;
		CXTButton m_btnKey3;
		CXTButton m_btnOK;
		CXTButton m_btnCancel;
		CButton	m_OKButton;
		CButton	m_continueButton;
		CStatic	m_runsLeft;
		CStatic	m_demoText;
		CStatic	m_daysLeft;
		CString	m_daysLeftString;
		CString	m_demoTextString;
		CString	m_runsLeftString;
		// メモ: ClassWizard はこの位置にデータ メンバを追加します。
	//}}AFX_DATA


// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CLicenceDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:

	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CLicenceDlg)
	virtual BOOL OnInitDialog();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	void SetTheme(XTThemeStyle enumTheme);
	void SetWinxpThemes();
	void SetWordTheme();
	void UpdateXTStyle();
	void ModifyBtnStyle(DWORD dwRemove, DWORD dwAdd);
	void UpdateIcons();
	void EnableButtons(XTThemeStyle enumTheme);
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_LICENCEDLG_H__3B2C35D3_DF9E_4F1D_9BBE_DED64BF777D6__INCLUDED_)
