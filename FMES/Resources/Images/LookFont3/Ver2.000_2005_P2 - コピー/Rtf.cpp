//////////////////////////////////////////////////////////////////////
// RTF形式テキストに変換する
// Rtf.cpp
//////////////////////////////////////////////////////////////////////

#include <afxwin.h>
#include <mbstring.h>
#include "CharCode.h"
#include "Rtf.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// 構築/消滅
//////////////////////////////////////////////////////////////////////

CRtf::CRtf()
{

}

CRtf::~CRtf()
{

}

// 8ビット，2バイト文字列をRTF形式に変換する
void CRtf::ConvRtfChar(CByteArray &abDst, LPCTSTR pText)
{
	CCharCode	cc;
	LPCSTR		pSJis;
	int			i, j, k, nByte;
	char		cBuf[256];

	abDst.RemoveAll();

	pSJis = cc.TChar2SJis(pText);

	for(i = 0; pSJis[i] != '\0'; i++) {
		nByte = 0;
		if(_ismbclegal((UCHAR)pSJis[i] << 8 | (UCHAR)pSJis[i + 1]))
			nByte = 2;
		else if(pSJis[i] & 0x80)
			nByte = 1;
		if(nByte > 0) {
			for(j = 0; j < nByte; j++) {
				sprintf(cBuf, "\\'%02x", (UCHAR)pSJis[i + j]);
				for(k = 0; cBuf[k]; k++)
					abDst.Add(cBuf[k]);
			}
			i += nByte - 1;
		}
		else {
			abDst.Add(pSJis[i]);
		}
	}

	abDst.Add('\0');
}

// 8ビット，2バイト文字列をRTF(UNICODE)形式に変換する
void CRtf::ConvURtfChar(CByteArray &abDst, LPCTSTR pText)
{
	CCharCode	cc;
	char		cBuf[100];
	LPCSTR		pUtf8;
	int			i, j;

	abDst.RemoveAll();

	pUtf8 = cc.TChar2Utf8(pText);

	for(i = 0; pUtf8[i]; i++) {
		if(((UCHAR)pUtf8[i]) & 0x80) {
			sprintf(cBuf, "\\'%02x", (UCHAR)pUtf8[i]);
			for(j = 0; cBuf[j]; j++)
				abDst.Add(cBuf[j]);
		}
		else {
			abDst.Add(pUtf8[i]);
		}
	}
	abDst.Add('\0');
}

// RTF文字列を作成する
//	pText			I	テキスト
//	pFontFace		I	フォント名
//	nPointSize		I	文字ポイントx10
//	cCharSet		I	キャラクラセット
//	bBold			I	Bold
void CRtf::ConvRtfText(CByteArray &abDst, LPCTSTR pText, LPCTSTR pFontFace, int nPointSize, BYTE cCharSet, BOOL bBold)
{
	CByteArray	abText;
	CByteArray	abFontFace;
	CByteArray	dt;
	CCharCode	cc;

	ConvRtfChar(abText, pText);
	ConvRtfChar(abFontFace, pFontFace);

	dt.SetSize(abText.GetSize() + abFontFace.GetSize() + 1000);
	sprintf(
		(LPSTR)dt.GetData(),
		"{\\rtf1\\ansi\\ansicpg932\\deff0\\deflang1033\\deflangfe1041{\\fonttbl{\\f0\\fscript\\fprq1\\fcharset%d %s;}}\r\n"
		"\\uc1\\pard%s\\f0\\fs%d %s}",
		cCharSet, abFontFace.GetData(),
		bBold ? "\\b" : "",
		nPointSize / 5, abText.GetData());

	abDst.SetSize(strlen((LPCSTR)dt.GetData()) + 1);
	strcpy((LPSTR)abDst.GetData(), (LPCSTR)dt.GetData());
}

// URTF文字列を作成する
//	pText			I	テキスト
//	pFontFace		I	フォント名
//	nPointSize		I	文字ポイントx10
//	cCharSet		I	キャラクラセット
//	bBold			I	Bold
void CRtf::ConvURtfText(CByteArray &abDst, LPCTSTR pText, LPCTSTR pFontFace, int nPointSize, BYTE cCharSet, BOOL bBold)
{
	CByteArray	abText;
	CByteArray	abFontFace;
	CByteArray	dt;
	CCharCode	cc;
	LPCSTR		pStr;

	pStr = cc.TChar2Utf8(pText);
	abText.SetSize(strlen(pStr) + 1);
	strcpy((LPSTR)abText.GetData(), pStr);
	ConvRtfChar(abFontFace, pFontFace);

	dt.SetSize(abText.GetSize() + abFontFace.GetSize() + 1000);
	sprintf(
		(LPSTR)dt.GetData(),
		"{\\urtf1\\ansi\\ansicpg932\\deff0\\deflang1033\\deflangfe1041{\\fonttbl{\\f0\\fscript\\fprq1\\fcharset%d %s;}}\r\n"
		"\\uc1\\pard%s\\f0\\fs%d %s}",
		cCharSet, abFontFace.GetData(),
		bBold ? "\\b" : "",
		nPointSize / 5, abText.GetData());

	abDst.SetSize(strlen((LPCSTR)dt.GetData()) + 1);
	strcpy((LPSTR)abDst.GetData(), (LPCSTR)dt.GetData());
}

// RTF文字列を作成する
//	pText			I	テキスト
//	pFontFace		I	フォント名
//	nPointSize		I	文字ポイントx10
//	cCharSet		I	キャラクラセット
//	bBold			I	Bold
void CRtf::ConvRtfText2(CByteArray &abDst, LPCTSTR pText, CStringList *pFontFaces, int nPointSize, BYTE cCharSet, BOOL bBold)
{
	CByteArray	abText;
	CByteArray	dt;
	CCharCode	cc;
	int			i;
	ConvRtfChar(abText, pText);
	int	imax = 0;
	strcpy(
		(LPSTR)dt.GetData(),
		"{\\rtf1\\ansi\\ansicpg932\\deff0\\deflang1033\\deflangfe1041{\\fonttbl");
	imax = pFontFaces->GetCount;
	for (i = 0 ; i < imax ; i++) {
		CByteArray	abFontFace;
		CByteArray	dw;
		ConvRtfChar(abFontFace, (LPCTSTR)(pFontFaces->GetAt((POSITION)i)));

		dw.SetSize(abText.GetSize() + abFontFace.GetSize() + 1000);
		sprintf(
			(LPSTR)dw.GetData(),
			"{\\f%d\\fscript\\fprq1\\fcharset%d %s;}",
			i, cCharSet, abFontFace.GetData());
		//memset(cFontName, 0, sizeof(TCHAR) * 100);
		strcpy((LPSTR)dt.GetData(), (LPCSTR)dw.GetData());
		//dw.FreeExtra();
	}
	strcpy((LPSTR)dt.GetData(), (LPCSTR)"}\r\n");
	for (i = 0; i < imax; i++) {
		CByteArray	dw2;
		sprintf(
			(LPSTR)dw2.GetData(),
			"\\uc1\\pard%s\\f%d\\fs%d %s}",
			i, bBold ? "\\b" : "",
			nPointSize / 5, abText.GetData());
			abDst.SetSize(strlen((LPCSTR)dt.GetData()) + 1);
			strcpy((LPSTR)dt.GetData(), (LPCSTR)dw.GetData());
	}

	strcpy((LPSTR)abDst.GetData(), (LPCSTR)dt.GetData());
}

// URTF文字列を作成する
//	pText			I	テキスト
//	pFontFace		I	フォント名
//	nPointSize		I	文字ポイントx10
//	cCharSet		I	キャラクラセット
//	bBold			I	Bold
void CRtf::ConvURtfText2(CByteArray &abDst, LPCTSTR pText, LPCTSTR pFontFace, int nPointSize, BYTE cCharSet, BOOL bBold)
{
	CByteArray	abText;
	CByteArray	abFontFace;
	CByteArray	dt;
	CCharCode	cc;
	LPCSTR		pStr;

	pStr = cc.TChar2Utf8(pText);
	abText.SetSize(strlen(pStr) + 1);
	strcpy((LPSTR)abText.GetData(), pStr);
	ConvRtfChar(abFontFace, pFontFace);

	dt.SetSize(abText.GetSize() + abFontFace.GetSize() + 1000);

	sprintf(
		(LPSTR)dt.GetData(),
		"{\\urtf1\\ansi\\ansicpg932\\deff0\\deflang1033\\deflangfe1041{\\fonttbl{\\f0\\fscript\\fprq1\\fcharset%d %s;}}\r\n"
		"\\uc1\\pard%s\\f0\\fs%d %s}",
		cCharSet, abFontFace.GetData(),
		bBold ? "\\b" : "",
		nPointSize / 5, abText.GetData());

	abDst.SetSize(strlen((LPCSTR)dt.GetData()) + 1);
	strcpy((LPSTR)abDst.GetData(), (LPCSTR)dt.GetData());
}
