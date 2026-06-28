// NumberEdit.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "lookfont.h"
#include "NumberEdit.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CNumberEdit

CNumberEdit::CNumberEdit()
{
	KeepVal = "";
}

CNumberEdit::~CNumberEdit()
{
}


BEGIN_MESSAGE_MAP(CNumberEdit, CEdit)
	//{{AFX_MSG_MAP(CNumberEdit)
	ON_WM_CHAR()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CNumberEdit メッセージ ハンドラ
void CNumberEdit::SetValue(CString wStr)
{
	KeepVal	= wStr;
	SetWindowText( wStr );
}

CString	CNumberEdit::GetValue()
{
	CString	wStr;
	GetWindowText(wStr) ;
	return	(wStr) ;
}

void CNumberEdit::ResetValue()
{
	SetValue( KeepVal );
}

void CNumberEdit::OnChar(UINT nChar, UINT nRepCnt, UINT nFlags) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	if ((nChar >= '0' && nChar <= '9') || nChar == VK_BACK ) {
		CEdit::OnChar(nChar, nRepCnt, nFlags);
	}
	else {
		MessageBeep (MB_ICONASTERISK) ;
	}
}
