/////////////////////////////////////////////////////////////////////////////
// 文字列分割クラス
/////////////////////////////////////////////////////////////////////////////
#define	WINVER	0x0400
#include <afxwin.h>
#ifdef _DEBUG
#define new DEBUG_NEW
#endif
#include "SepLine.h"

CSepLine::CSepLine(void)
{
}

CSepLine::~CSepLine(void)
{
}

// 文字列分割
//	pLine				I	文字列
//	pTokens				I	デリミタ群
//	bOneTokenOneItem	I	FALSE:連続したデリミタを１デリミタとして扱う TRUE:長さ０でも１アイテムとする
//	bDisableDblQuo		I	ダブルクォーテーションを無効にする
//	bTokenAddList		I	デリミタもアイテムとする
int CSepLine::Sep(LPCTSTR pLine, LPCTSTR pTokens/*=" \t\r\n"*/, BOOL bOneTokenOneItem/*=FALSE*/, BOOL bDisableDblQuo/*=FALSE*/, BOOL bTokenAddList/*=FALSE*/)
{
	CString		sLine = pLine;
	LPCTSTR		pCur;
	int			nStart, nEnd;
	BOOL		bInDQ;

	RemoveAll();

	nStart = 0;
	nEnd   = 0;
	bInDQ  = FALSE;
	for(pCur = pLine; *pCur != '\0'; pCur = _tcsinc(pCur)) {
		if(!bInDQ) {
			if(_tcschr(pTokens, *pCur) != NULL) {
				nEnd = pCur - pLine;
				if(bOneTokenOneItem || nEnd - nStart > 0)
					Add(sLine.Mid(nStart, nEnd - nStart));
				if(bTokenAddList)
					Add(sLine.Mid(nEnd, 1));
				nStart = nEnd + 1;
			}
			else {
				if(!bDisableDblQuo) {
					if(*pCur == '\"')
						bInDQ = TRUE;
				}
			}
		}
		else {
			if(*pCur == '\"')
				bInDQ = FALSE;
		}
	}
	nEnd = _tcslen(pLine);
	if(bOneTokenOneItem || nEnd - nStart > 0)
		Add(sLine.Mid(nStart, nEnd - nStart));

	return GetSize();
}

// 文字列のダブルクォーテーションを取る
//	pText	I	文字列
//	return 結果文字列
CString CSepLine::DelDblQuo(LPCTSTR pText)
{
	CString		str = pText;
	LPCTSTR		pLast = _tcsdec(pText, pText + _tcslen(pText));

	if(pLast != NULL && *pText == '\"' && *pLast == '\"')
		str = str.Mid(1, str.GetLength() - 2);

	return str;
}

// 文字列にダブルクォーテーションを付加する
//	pText	I	文字列
//	return 結果文字列
CString CSepLine::AddDblQuo(LPCTSTR pText)
{
	return (CString)"\"" + pText + "\"";
}

// 全てのアイテムのダブルクォーテーションを取る
void CSepLine::DelDblQuoAll()
{
	int		i;
	for(i = 0; i < GetSize(); i++)
		SetAt(i, DelDblQuo(GetAt(i)));
}

// 前後の余白を取る
CString CSepLine::TrimLR(LPCTSTR pText)
{
	CString		str = pText;

	str.TrimLeft();
	str.TrimRight();

	return str;
}

// 全てのアイテムの前後の余白を取る
void CSepLine::TrimLRAll()
{
	int		i;
	for(i = 0; i < GetSize(); i++)
		SetAt(i, TrimLR(GetAt(i)));
}
