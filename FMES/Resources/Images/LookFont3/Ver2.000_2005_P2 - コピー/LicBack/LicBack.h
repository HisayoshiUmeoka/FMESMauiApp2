// LicBack.h : LICBACK アプリケーションのメイン ヘッダー ファイルです。
//

#if !defined(AFX_LICBACK_H__2E400E83_D0B5_49B0_A071_417145582B45__INCLUDED_)
#define AFX_LICBACK_H__2E400E83_D0B5_49B0_A071_417145582B45__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// メイン シンボル

/////////////////////////////////////////////////////////////////////////////
// CLicBackApp:
// このクラスの動作の定義に関しては LicBack.cpp ファイルを参照してください。
//

class CLicBackApp : public CWinApp
{
public:
	CLicBackApp();

	CString	m_appPath	;
	CString	m_appBoot	;

	LONG	m_lfhandle;
	LONG	m_compno;
	LONG	m_semhandle;

	CString	m_UrlUnlock	;

	char	m_LicenseID[16]	;
	char	m_LicensePW[16]	;

	CString	m_ProKey1	;
	CString	m_ProKey2	;
	CString	m_ProKey3	;
	CString	m_ProKey4	;
	CString	m_ProKey5	;
	BOOL	m_ProKeyOK	;
	BOOL	m_ActivatOK	;

// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CLicBackApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// インプリメンテーション

	//{{AFX_MSG(CLicBackApp)
		// メモ - ClassWizard はこの位置にメンバ関数を追加または削除します。
		//        この位置に生成されるコードを編集しないでください。
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	BOOL	CheckProKey()	;
	BOOL	UpdateProKey()	;
};

extern CLicBackApp theApp ;

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_LICBACK_H__2E400E83_D0B5_49B0_A071_417145582B45__INCLUDED_)
