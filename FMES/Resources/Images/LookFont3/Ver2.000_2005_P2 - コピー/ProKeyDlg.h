#if !defined(AFX_PROKEYDLG_H__64DBD82C_52CB_4E15_8FC7_F4AE02EAF231__INCLUDED_)
#define AFX_PROKEYDLG_H__64DBD82C_52CB_4E15_8FC7_F4AE02EAF231__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ProKeyDlg.h : ヘッダー ファイル
//
#include "ProKeyEdit.h"

/////////////////////////////////////////////////////////////////////////////
// CProKeyDlg ダイアログ

class CProKeyDlg : public CDialog
{
// コンストラクション
public:
	CProKeyDlg(CWnd* pParent = NULL);   // 標準のコンストラクタ

// ダイアログ データ
	//{{AFX_DATA(CProKeyDlg)
	enum { IDD = IDD_DIALOG_SNKEY };
	CProKeyEdit	m_ProKeyEd5;
	CProKeyEdit	m_ProKeyEd4;
	CProKeyEdit	m_ProKeyEd3;
	CProKeyEdit	m_ProKeyEd2;
	CProKeyEdit	m_ProKeyEd1;
	CString	m_ProKey1;
	CString	m_ProKey2;
	CString	m_ProKey3;
	CString	m_ProKey4;
	CString	m_ProKey5;
	//}}AFX_DATA


// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CProKeyDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:
	HICON	m_hIcon;

	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CProKeyDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnButtonAct1();
	afx_msg void OnButtonAct2();
	afx_msg void OnButtonAct3();
	virtual void OnCancel();
	afx_msg void OnChangeEditProkey1();
	afx_msg void OnChangeEditProkey2();
	afx_msg void OnChangeEditProkey3();
	afx_msg void OnChangeEditProkey4();
	afx_msg void OnChangeEditProkey5();
	afx_msg void OnPaint();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	BOOL	CheckProkeyVal()	;

public:
	LONG	m_LicenseID;
	CString m_LicensePW;
	LONG	m_LeaveDays;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_PROKEYDLG_H__64DBD82C_52CB_4E15_8FC7_F4AE02EAF231__INCLUDED_)
