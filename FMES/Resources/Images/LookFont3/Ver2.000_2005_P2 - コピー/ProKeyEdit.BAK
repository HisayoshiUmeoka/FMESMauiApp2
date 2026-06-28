// NumIntEdit.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
//#include "RLFCalcOrdinary.h"
#include "ProKeyEdit.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CProKeyEdit

CProKeyEdit::CProKeyEdit()
{
	KeepVal = "";
}

CProKeyEdit::~CProKeyEdit()
{
}

BEGIN_MESSAGE_MAP(CProKeyEdit, CEdit)
	//{{AFX_MSG_MAP(CProKeyEdit)
	ON_WM_CHAR()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CProKeyEdit メッセージ ハンドラ

void CProKeyEdit::OnChar(UINT nChar, UINT nRepCnt, UINT nFlags) 
{
// 数値以外のキーが押されたら警告音を鳴らす
	if ((((nChar >= 'a' && nChar <= 'z') || (nChar >= 'A' && nChar <= 'Z') || (nChar >= '0' && nChar <= '9'))
	   && (nChar != 'i') && (nChar != 'I')
	   && (nChar != 'o') && (nChar != 'O')
	   && (nChar != 'q') && (nChar != 'Q')
	   && (nChar != 'v') && (nChar != 'V'))
	||    nChar == VK_BACK ) {
		CEdit::OnChar(nChar, nRepCnt, nFlags);
	}
	else {
		MessageBeep( MB_ICONASTERISK );
	}
}

void CProKeyEdit::SetValue(CString wStr)
{
	KeepVal	= wStr;
	SetWindowText( wStr );
}

CString	CProKeyEdit::GetValue()
{
	CString	wStr;
	GetWindowText(wStr) ;
	return	(wStr) ;
}

void CProKeyEdit::ResetValue()
{
	SetValue( KeepVal );
}

