#if !defined(AFX_NUMBEREDIT_H__FF15C4F6_7E8F_4B87_BB4C_4BECBA579658__INCLUDED_)
#define AFX_NUMBEREDIT_H__FF15C4F6_7E8F_4B87_BB4C_4BECBA579658__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// NumberEdit.h : ヘッダー ファイル
//

/////////////////////////////////////////////////////////////////////////////
// CNumberEdit ウィンドウ

class CNumberEdit : public CEdit
{
// コンストラクション
public:
	CNumberEdit();

// アトリビュート
public:

// オペレーション
public:

// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CNumberEdit)
	//}}AFX_VIRTUAL

// インプリメンテーション
public:
	virtual ~CNumberEdit();

	void	SetValue(CString wStr)	;
	CString	GetValue()				;
	void	ResetValue()			;

	// 生成されたメッセージ マップ関数
protected:
	//{{AFX_MSG(CNumberEdit)
	afx_msg void OnChar(UINT nChar, UINT nRepCnt, UINT nFlags);
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
private:
	CString	KeepVal	;

};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_NUMBEREDIT_H__FF15C4F6_7E8F_4B87_BB4C_4BECBA579658__INCLUDED_)
