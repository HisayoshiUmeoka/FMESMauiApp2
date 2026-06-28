#if !defined(AFX_DIALOGFONTINFO_H__D82C83EF_20C1_4D6A_B2AB_0A79C015CE3B__INCLUDED_)
#define AFX_DIALOGFONTINFO_H__D82C83EF_20C1_4D6A_B2AB_0A79C015CE3B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// DialogFontInfo.h : ヘッダー ファイル
//

#include	"FontList.h"
/////////////////////////////////////////////////////////////////////////////
// CDialogFontInfo ダイアログ

class CDialogFontInfo : public CDialog
{
// コンストラクション
public:
	CDialogFontInfo(CWnd* pParent = NULL);   // 標準のコンストラクタ

	CString		m_FontFace	;
	CString		m_FontFile	;
	BOOL		m_FontMode	;
// ダイアログ データ
	//{{AFX_DATA(CDialogFontInfo)
	enum { IDD = IDD_DIALOG_FONTINFO };
		// メモ: ClassWizard はこの位置にデータ メンバを追加します。
	//}}AFX_DATA


// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CDialogFontInfo)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:
	HICON	m_hIcon;
	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CDialogFontInfo)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	BOOL	RegSubSearch(CString csFont, BOOL win98Me, CString &retPath)	;
	CString	GetWinFontPath()	;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_DIALOGFONTINFO_H__D82C83EF_20C1_4D6A_B2AB_0A79C015CE3B__INCLUDED_)
