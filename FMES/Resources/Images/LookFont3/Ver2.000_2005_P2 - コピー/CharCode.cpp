// CharCode.cpp: CCharCode クラスのインプリメンテーション
//
//////////////////////////////////////////////////////////////////////

#include <afxwin.h>
#include "CharCode.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// 構築/消滅
//////////////////////////////////////////////////////////////////////

CCharCode::CCharCode()
{

}

CCharCode::~CCharCode()
{

}

LPCSTR CCharCode::SJis2Utf8(LPCSTR pText)
{
	CWordArray	abWide;
	int			nWide, nUtf8;

	nWide = MultiByteToWideChar(CP_ACP, 0, pText, -1, NULL, 0);
	abWide.SetSize(nWide);
	MultiByteToWideChar(CP_ACP, 0, pText, -1, (LPWSTR)(abWide.GetData()), nWide);
	abWide.Add('\0');

	nUtf8 = WideCharToMultiByte(CP_UTF8, 0, (LPWSTR)(abWide.GetData()), nWide, NULL, 0, NULL, NULL);
	m_abChar.SetSize(nUtf8);
	WideCharToMultiByte(CP_UTF8, 0, (LPWSTR)(abWide.GetData()), nWide, (LPSTR)m_abChar.GetData(), nUtf8, NULL, NULL);
	m_abChar.Add('\0');

	return (LPCSTR)m_abChar.GetData();
}

LPCSTR CCharCode::TChar2Utf8(LPCTSTR pText)
{
	CWordArray	abWide;
	int			nWide, nUtf8;

#ifndef _UNICODE
	nWide = MultiByteToWideChar(CP_ACP, 0, pText, -1, NULL, 0);
	abWide.SetSize(nWide);
	MultiByteToWideChar(CP_ACP, 0, pText, -1, (LPWSTR)(abWide.GetData()), nWide);
	abWide.Add('\0');
#else
	nWide = wcslen(pText);
	abWide.SetSize(nWide + 1);
	wcscpy(abWide.GetData(), pText);
#endif

	nUtf8 = WideCharToMultiByte(CP_UTF8, 0, (LPWSTR)(abWide.GetData()), nWide, NULL, 0, NULL, NULL);
	m_abChar.SetSize(nUtf8);
	WideCharToMultiByte(CP_UTF8, 0, (LPWSTR)(abWide.GetData()), nWide, (LPSTR)m_abChar.GetData(), nUtf8, NULL, NULL);
	m_abChar.Add('\0');

	return (LPCSTR)m_abChar.GetData();
}

LPCSTR CCharCode::WChar2SJis(const wchar_t* pText)
{
	int			nChar;

	nChar = WideCharToMultiByte(CP_ACP, 0, pText, -1, NULL, 0, NULL, NULL);
	m_abChar.SetSize(nChar);
	WideCharToMultiByte(CP_ACP, 0, pText, -1, (LPSTR)m_abChar.GetData(), nChar, NULL, NULL);
	m_abChar.Add('\0');

	return (LPCSTR)m_abChar.GetData();
}

LPCSTR CCharCode::TChar2SJis(LPCTSTR pText)
{
#ifndef _UNICODE
	m_abChar.SetSize(strlen(pText) + 1);
	strcpy((LPSTR)m_abChar.GetData(), pText);
	return (LPCSTR)m_abChar.GetData();
#else
	return WChar2SJis(pText);
#endif
}

const wchar_t* CCharCode::TChar2WChar(LPCTSTR pText)
{
#ifndef _UNICODE
	int			nWide;

	nWide = MultiByteToWideChar(CP_ACP, 0, pText, -1, NULL, 0);
	m_abWord.SetSize(nWide);
	MultiByteToWideChar(CP_ACP, 0, pText, -1, (LPWSTR)(m_abWord.GetData()), nWide);
	m_abWord.Add('\0');
#else
	m_abWord.SetSize(wcslen(pText) + 1);
	wcscpy(m_abWord.GetData(), pText);
#endif

	return (wchar_t*)m_abWord.GetData();
}

