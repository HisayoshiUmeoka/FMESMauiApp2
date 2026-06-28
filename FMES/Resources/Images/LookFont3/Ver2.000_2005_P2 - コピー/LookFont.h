// LookFont.h : LOOKFONT アプリケーションのメイン ヘッダー ファイル
//

#if !defined(AFX_LOOKFONT_H__9ADC1E7F_FB7D_4DD0_9B5F_0F0D9608D857__INCLUDED_)
#define AFX_LOOKFONT_H__9ADC1E7F_FB7D_4DD0_9B5F_0F0D9608D857__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // メイン シンボル

/////////////////////////////////////////////////////////////////////////////
// CLookFontApp:
// このクラスの動作の定義に関しては LookFont.cpp ファイルを参照してください。
//

// メッセージ
#define	LF_WM_FRAME_CLOSE		(WM_USER + WM_CLOSE)	// WM_CLOSE 通知


class CLookFontApp : public CWinApp
{
public:
	CLookFontApp();

	CDialogBar*		m_pWndTextCtrlBar;

	CString	m_appPath	;
	CString	m_appBoot	;
	bool	m_showMain;
	bool	m_startOver;
	LONG	m_lfhandle;
	LONG	m_compno;
	LONG	m_semhandle;

	CString	m_UrlUnlock	;

	char	m_LicenseID[16]	;
	char	m_LicensePW[16]	;

	CString	m_SoftMode	;

	CString	m_ProKey1	;
	CString	m_ProKey2	;
	CString	m_ProKey3	;
	CString	m_ProKey4	;
	CString	m_ProKey5	;
	BOOL	m_ProKeyOK	;
	BOOL	m_ActivatOK	;

	int		ExitInstance()			;
	void	ShowDemo(bool expired)	;
	void	ShowMain(bool retail)	;
	BOOL	CheckProKey()			;
	BOOL	UpdateProKey()			;
	void	StatusChanged()			;
	void	UpdateBeforeExit()		;
	LONG	CheckError(LONG errornum)	;
	BOOL	CheckActDate2()			;

// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CLookFontApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// インプリメンテーション
	//{{AFX_MSG(CLookFontApp)
	afx_msg void OnAppAbout();
	afx_msg void OnMenuPluss();
	afx_msg void OnMenuSupport();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
protected:
//	void		StatusChanged()		;
//	void		MenuOptionsDemo()	;
//	void		MenuOptionsRetail()	;
//	void		UpdateBeforeExit()	;
//	void		ShowDemo(bool expired)	;
//	void		ShowMain(bool retail)	;
};

extern CLookFontApp theApp ;
/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_LOOKFONT_H__9ADC1E7F_FB7D_4DD0_9B5F_0F0D9608D857__INCLUDED_)

