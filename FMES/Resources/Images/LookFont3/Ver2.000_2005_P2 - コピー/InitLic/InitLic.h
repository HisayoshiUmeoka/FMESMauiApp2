// InitLic.h : PROJECT_NAME アプリケーションのメイン ヘッダー ファイルです。
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"		// メイン シンボル


// CInitLicApp:
// このクラスの実装については、InitLic.cpp を参照してください。
//

class CInitLicApp : public CWinApp
{
public:
	CInitLicApp();

	CString	m_appPath	;
	CString	m_appBoot	;

	LONG	m_lfhandle;
// オーバーライド
	public:
	virtual BOOL InitInstance();

// 実装

	DECLARE_MESSAGE_MAP()

	BOOL	CheckProKey()	;
};

extern CInitLicApp theApp;