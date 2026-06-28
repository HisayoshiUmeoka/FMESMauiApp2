// Rtf.h: CRtf クラスのインターフェイス
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_RTF_H__CD5D5E4D_747B_43E9_AE03_FA81B8C3FF15__INCLUDED_)
#define AFX_RTF_H__CD5D5E4D_747B_43E9_AE03_FA81B8C3FF15__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CRtf  
{
public:
	CRtf();
	virtual ~CRtf();

	static void ConvRtfChar(CByteArray &abDst, LPCTSTR pText);
	static void ConvURtfChar(CByteArray &abDst, LPCTSTR pText);
	static void ConvRtfText(CByteArray &abDst, LPCTSTR pText, LPCTSTR pFontFace, int nPointSize, BYTE cCharSet, BOOL bBold);
	static void ConvRtfText2(CByteArray &abDst, LPCTSTR pText, CStringList *pFontFaces, int nPointSize, BYTE cCharSet, BOOL bBold);
//	static void ConvURtfText(CByteArray &abDst, LPCWSTR pText, LPCWSTR pFontFace, int nPointSize, BYTE cCharSet, BOOL bBold);
	static void ConvURtfText(CByteArray &abDst, LPCTSTR pText, LPCTSTR pFontFace, int nPointSize, BYTE cCharSet, BOOL bBold);
	static void ConvURtfText2(CByteArray &abDst, LPCTSTR pText, CStringList *pFontFaces, int nPointSize, BYTE cCharSet, BOOL bBold);
};

#endif // !defined(AFX_RTF_H__CD5D5E4D_747B_43E9_AE03_FA81B8C3FF15__INCLUDED_)
