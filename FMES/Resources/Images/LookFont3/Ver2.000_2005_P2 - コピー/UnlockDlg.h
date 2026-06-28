#if !defined(AFX_UNLOCKDLG_H__B310D695_82D9_4A75_B086_68CD76DEC460__INCLUDED_)
#define AFX_UNLOCKDLG_H__B310D695_82D9_4A75_B086_68CD76DEC460__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// UnlockDlg.h : ヘッダー ファイル
//
#include "NumberEdit.h"

/////////////////////////////////////////////////////////////////////////////
// CUnlockDlg ダイアログ

class CUnlockDlg : public CDialog
{
// コンストラクション
public:
	CUnlockDlg(CWnd* pParent = NULL);   // 標準のコンストラクタ
	void TurnOffPayments();
	VOID ExtendPayment();

// ダイアログ データ
	//{{AFX_DATA(CUnlockDlg)
	enum { IDD = IDD_DIALOG_UNLOCK };
	CStatic	m_userCode2;
	CStatic	m_userCode1;
	CEdit	m_regKey2;
	CEdit	m_regKey1;
	CString	m_userCode1String;
	CString	m_userCode2String;
	//}}AFX_DATA

	long m_sessionCode;


// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CUnlockDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:
	HICON	m_hIcon;

	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CUnlockDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	virtual void OnOK();
	virtual void OnCancel();
	afx_msg void OnButtonGetweb();
	afx_msg void OnButtonFax();
	afx_msg void OnButtonEmail();
	afx_msg void OnChangeEditRegKey1();
	afx_msg void OnChangeEditRegKey2();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_UNLOCKDLG_H__B310D695_82D9_4A75_B086_68CD76DEC460__INCLUDED_)
