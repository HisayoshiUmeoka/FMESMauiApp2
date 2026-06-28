#if !defined(AFX_DIALOGUSER_H__1D864C16_95A8_460C_93BA_8D243C8F6BB0__INCLUDED_)
#define AFX_DIALOGUSER_H__1D864C16_95A8_460C_93BA_8D243C8F6BB0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// DialogUser.h : ヘッダー ファイル
//
#include "NumberEdit.h"

/////////////////////////////////////////////////////////////////////////////
// CDialogUser ダイアログ

class CDialogUser : public CDialog
{
// コンストラクション
public:
	CDialogUser(CWnd* pParent = NULL);   // 標準のコンストラクタ
	LONG	m_LicenseID	;
	CString	m_LicensePW	;

// ダイアログ データ
	//{{AFX_DATA(CDialogUser)
	enum { IDD = IDD_DIALOG_USER };
	CNumberEdit	m_Fax3;
	CNumberEdit	m_Fax2;
	CNumberEdit	m_Fax1;
	CNumberEdit	m_Zip2;
	CNumberEdit	m_Zip1;
	CNumberEdit	m_Tel3;
	CNumberEdit	m_Tel2;
	CNumberEdit	m_Tel1;
	CNumberEdit	m_Birth3;
	CNumberEdit	m_Birth2;
	CNumberEdit	m_Birth1;
	CString	m_strBirth1;
	CString	m_strBirth2;
	CString	m_strBirth3;
	CString	m_strCompany;
	CString	m_strName1;
	CString	m_strName2;
	CString	m_strZip1;
	CString	m_strZip2;
	CString	m_strEmail;
	//}}AFX_DATA


// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CDialogUser)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:
	HICON	m_hIcon;

	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CDialogUser)
	virtual BOOL OnInitDialog();
	virtual void OnOK();
	afx_msg void OnPaint();
	afx_msg void OnButtonRegist();
	afx_msg void OnButtonPrivacy();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnChangeEditCompany();
	afx_msg void OnChangeEditName1();
	afx_msg void OnChangeEditName2();
	afx_msg void OnSelchangeComboSex();
	afx_msg void OnChangeEditBirth1();
	afx_msg void OnChangeEditBirth2();
	afx_msg void OnChangeEditBirth3();
	afx_msg void OnSelchangeComboPref();
	afx_msg void OnChangeEditAdr1();
	afx_msg void OnChangeEditAdr2();
	afx_msg void OnChangeEditAdr3();
	afx_msg void OnChangeEditPhone1();
	afx_msg void OnChangeEditPhone2();
	afx_msg void OnChangeEditPhone3();
	afx_msg void OnChangeEditZip1();
	afx_msg void OnChangeEditZip2();
	afx_msg void OnChangeEditFax1();
	afx_msg void OnChangeEditFax2();
	afx_msg void OnChangeEditFax3();
	afx_msg void OnChangeEditEmail();
	afx_msg void OnSelchangeComboOccu();
	afx_msg void OnSelchangeComboJob();
	afx_msg void OnSelchangeComboDm();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	void	InitItems()			;
	BOOL	CheckUserInfoVal()	;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_DIALOGUSER_H__1D864C16_95A8_460C_93BA_8D243C8F6BB0__INCLUDED_)
