#if !defined(AFX_NUMINTEDIT_H__72558DCB_E81F_418D_B0D2_010E2E50B7B3__INCLUDED_)
#define AFX_NUMINTEDIT_H__72558DCB_E81F_418D_B0D2_010E2E50B7B3__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ProKeyEdit.h : ヘッダー ファイル
//

/////////////////////////////////////////////////////////////////////////////
// CProKeyEdit ウィンドウ

class CProKeyEdit : public CEdit
{
// コンストラクション
public:
	CProKeyEdit();

// アトリビュート
public:

// オペレーション
public:

// ファンクション
public:
	void	SetValue(CString wStr); 
	CString	GetValue(); 
	void	ResetValue(); 
	
// ローカル変数
private:
	CString	KeepVal	;


// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CProKeyEdit)
	//}}AFX_VIRTUAL

// インプリメンテーション
public:
	virtual ~CProKeyEdit();

	// 生成されたメッセージ マップ関数
protected:
	//{{AFX_MSG(CProKeyEdit)
	afx_msg void OnChar(UINT nChar, UINT nRepCnt, UINT nFlags);
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_NUMINTEDIT_H__72558DCB_E81F_418D_B0D2_010E2E50B7B3__INCLUDED_)
