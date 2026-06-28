// CharCode.h: CCharCode クラスのインターフェイス
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CHARCODE_H__40D1F8AD_912E_4CF0_9C46_E3CFB0587508__INCLUDED_)
#define AFX_CHARCODE_H__40D1F8AD_912E_4CF0_9C46_E3CFB0587508__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CCharCode  
{
public:
	CCharCode();
	virtual ~CCharCode();

	LPCSTR SJis2Utf8(LPCSTR pText);
	LPCSTR TChar2Utf8(LPCTSTR pText);
	LPCSTR WChar2SJis(const wchar_t* pText);
	LPCSTR TChar2SJis(LPCTSTR pText);
	const wchar_t* TChar2WChar(LPCTSTR pText);

	CByteArray		m_abChar;
	CWordArray		m_abWord;
};

#endif // !defined(AFX_CHARCODE_H__40D1F8AD_912E_4CF0_9C46_E3CFB0587508__INCLUDED_)
