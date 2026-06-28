// FontList.h: CFontList クラスのインターフェイス
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_FONTLIST_H__E6DC3C51_16A3_4698_BC9F_535046FB3166__INCLUDED_)
#define AFX_FONTLIST_H__E6DC3C51_16A3_4698_BC9F_535046FB3166__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include <afxtempl.h>

#include	"LookFontSetting.h"

#define	FONTLIST_ITALIC		_T("Italic")	// イタリック

// フォント選択
#define	FONTLIST_LANG_ALL		((DWORD)-1)	// 全言語
#define	FONTLIST_FONT_ORG		((DWORD)-2)	// 既存フォント
#define	FONTLIST_FONT_TMP		((DWORD)-3)	// 一時フォント


#define	FONTLIST_CHARSET_LANG	128			// キャラクタセットの言語開始値


class CFontList  
{
public:
	CLookFontSetting	*m_Ini;				// 設定

	// フォントインストールモード
	typedef enum {
		INSTALLMODE_ORG = 0,				// 既存フォント
		INSTALLMODE_REG,					// 登録インストール
		INSTALLMODE_TMP						// 一時インストール
	} INSTALLMODE;

	// フォント定義
	typedef struct _FONTDEF {
		CString				sFaceName;		// フォント名
		CStringArray		asScript;		// 言語名
		CByteArray			acCharSet;		// キャラクタセット
		CDWordArray			adwWeight;		// Weight
		CByteArray			acItalic;		// Italic
		INSTALLMODE			eInstallMode;	// インストールモード
		int					nSelIndex;		// 選択インデックス
//		// 下記は2009.03.27 Ver1.3.0 で追加
//		CString				sFileName;		// フォントファイル名
		BOOL				bFavorite;		// お気に入り
		BOOL				bCandidate;		// 候補
	} FONTDEF;

	typedef CTypedPtrArray<CPtrArray, FONTDEF*> FONTDEFARRAY;

	// 一時フォント定義
	typedef struct _TMPFONTDEF {
		CString				sFaceName;		// フォント名
		CString				sFileName;		// フォントファイル名
	} TMPFONTDEF;

	typedef CTypedPtrArray<CPtrArray, TMPFONTDEF*> TMPFONTDEFARRAY;

public:
	CFontList();
	virtual ~CFontList();
	void Reset();
	void ResetTmpFont() ;
	void SelectFontList(CDWordArray &adwSelect);
	void GetLogFont(int nIndex, LOGFONT *pLogFont);
	void GetFontList(BOOL bOrgFont);
	int AddFont(LPCTSTR pName, LPCTSTR pFName=NULL);
	BOOL RemoveFont(LPCTSTR pName);
	CString AvailableChar(int nIndex, LPCTSTR pText);
	CString AvailableUniChar(int nIndex, LPCWSTR pText);
	int GetSize() {
		return m_aFontSel.GetSize();
	};
	FONTDEF* GetAt(int nIndex) {
		return m_aFontSel.GetAt(nIndex);
	};
	CString GetFontFaceAt(int nIndex) {
		return m_aFontSel.GetAt(nIndex)->sFaceName;
	};
//	CString GetFontFileAt(int nIndex) {
//		return m_aFontSel.GetAt(nIndex)->sFileName;
//	};
	UINT GetCharSetAt(int nIndex) {
		UINT uiRet = 0 ;
		if (m_aFontSel.GetCount() > 0) {
			uiRet = m_aFontSel.GetAt(nIndex)->acCharSet.GetAt(m_aFontSel.GetAt(nIndex)->nSelIndex);
		}
		return uiRet ;
	};
	BOOL IsBoldAt(int nIndex) {
		BOOL bRet = FALSE	;
		if (m_aFontSel.GetCount() > 0) {
			bRet = m_aFontSel.GetAt(nIndex)->adwWeight.GetAt(m_aFontSel.GetAt(nIndex)->nSelIndex) == FW_BOLD ;
		}
		return bRet;
	};
	BOOL IsFontTmpAt(int nIndex) {
		BOOL bRet = FALSE	;
		if (m_aFontSel.GetCount() > 0) {
			bRet = m_aFontSel.GetAt(nIndex)->eInstallMode == INSTALLMODE_TMP ;
		}
		return bRet;
	};
	BOOL IsFontFavoriteAt(int nIndex) {
		BOOL bRet = FALSE	;
		if (m_aFontSel.GetCount() > 0) {
			bRet = m_aFontSel.GetAt(nIndex)->bFavorite == TRUE ;
		}
		return bRet;
	};
	BOOL IsFontCandidateAt(int nIndex) {
		BOOL bRet = FALSE	;
		if (m_aFontSel.GetCount() > 0) {
			bRet = m_aFontSel.GetAt(nIndex)->bCandidate == TRUE ;
		}
		return bRet;
	};
	BOOL SetFontFavoriteAt(int nIndex, BOOL bVal) ;
	void SetFontCandidateAt(int nIndex, BOOL bVal) ;
	
	int FindUpdateTmpFontList(CString csFontN, CString csFontF)	;
	CString GetTmpFontName(CString csFontN)	;
	void	FindRemoveTmpFontList(CString csFontF)	;
	CString GetTempFontFile(int nIndex);
	void	ChangeFontInstaledlModeAt(int nIndex) ;

protected:
	static int CALLBACK EnumFontFamExProc(ENUMLOGFONTEX *lpElfe, NEWTEXTMETRICEX *lpNtme, DWORD dwFontType, LPARAM lParam);

	CStringArray		m_asFontFaceOrg;	// 既存フォント名
	FONTDEFARRAY		m_aFontAll;			// 全フォントリスト
	FONTDEFARRAY		m_aFontSel;			// カレントフォントリスト
	TMPFONTDEFARRAY		m_aTmpFont;
};

#endif // !defined(AFX_FONTLIST_H__E6DC3C51_16A3_4698_BC9F_535046FB3166__INCLUDED_)
