#if !defined(AFX_FINDFONTDLG_H__1BE49A55_7B3A_4BB0_9478_2B383038D489__INCLUDED_)
#define AFX_FINDFONTDLG_H__1BE49A55_7B3A_4BB0_9478_2B383038D489__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// FindFontDlg.h : ヘッダー ファイル
//

/////////////////////////////////////////////////////////////////////////////
// CFindFontDlg ダイアログ

class CFindFontDlg : public CDialog
{
// コンストラクション
public:
	CFindFontDlg(CWnd* pParent = NULL);   // 標準のコンストラクタ

	CStringList	m_FontPathList	;
	int			m_FontPathCnt	;

// ダイアログ データ
	//{{AFX_DATA(CFindFontDlg)
	enum { IDD = IDD_DIALOG_FIND };
		// メモ: ClassWizard はこの位置にデータ メンバを追加します。
	//}}AFX_DATA


// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CFindFontDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:

	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CFindFontDlg)
	virtual BOOL OnInitDialog();
	virtual void OnOK();
	afx_msg void OnButtonSearchpath();
	virtual void OnCancel();
	afx_msg void OnCheckExt6();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	int SearchFonts(CString csPath, CStringList &clExt, CStringList &clFont)	;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_FINDFONTDLG_H__1BE49A55_7B3A_4BB0_9478_2B383038D489__INCLUDED_)
