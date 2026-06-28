/////////////////////////////////////////////////////////////////////////////
// ファイル／フォルダ選択ダイアログ
/////////////////////////////////////////////////////////////////////////////
#define	WINVER	0x0400
#include <afxwin.h>
#ifdef _DEBUG
#define new DEBUG_NEW
#endif
#include <afxdlgs.h>
#include "FileName.h"
#include "BrowseFile.h"
#include "BrowseFolder.h"

CBrowseFile::CBrowseFile(void)
{
}

CBrowseFile::~CBrowseFile(void)
{
}

// フォルダ選択ダイアログ
//	pParentWnd	I	親CWnd
//	sPath		I/O	I:初期フォルダ O:結果フォルダ
//	pTitle		I	タイトル NULL:ディフォルト
//	return TRUE:変更されたOK FALSE:キャンセル又は変更なし(sPathは変化しない)
BOOL CBrowseFile::BrowseFolder(CWnd *pParentWnd, CString &sPath, LPCTSTR pTitle)
{
	CBrowseFolder dlg(pParentWnd, sPath.IsEmpty() ? (LPCTSTR)NULL : sPath, pTitle);
	if(dlg.DoModal() != IDOK)
		return FALSE;
	CString str = dlg.GetPath();
	if(str.CompareNoCase(sPath) == 0)
		return FALSE;
	sPath = str;
	return TRUE;
}

// ファイル選択ダイアログ
//	pParentWnd	I	親CWnd
//	sPath		I/O	I:初期ファイル又はフォルダ O:結果ファイル
//	asNames		I/O	ファイル名リスト
//	pTitle		I	タイトル NULL:ディフォルト
//	pFilter		I	フィルタ NULL:ディフォルト 例:"Text Files (*.txt)|*.txt|Data Files (*.xlc;*.xls)|*.xlc; *.xls|All Files (*.*)|*.*||";
//	bOpenFile	I	TRUE:オープン FALSE:セーブ
//	dwFlags		I	制御フラグ
//					OFN_HIDEREADONLY / OFN_OVERWRITEPROMPT / OFN_ALLOWMULTISELECT
//	return TRUE:OK FALSE:キャンセル(sPathFileは変化しない)
BOOL CBrowseFile::BrowseFile(CWnd *pParentWnd, CString &sPath, LPCTSTR pTitle, LPCTSTR pFilter, BOOL bOpenFile/*=TRUE*/, DWORD dwFlags/*=0*/)
{
	CStringArray	asDmy;
	return BrowseFile(pParentWnd, sPath, asDmy, pTitle, pFilter, bOpenFile, dwFlags);
}

#if _MSC_VER <= 1200
#define	POFN(d)	(&((d).m_ofn))
#else
#define	POFN(d)	(d).m_pOFN
#endif

//BOOL CBrowseFile::BrowseFile(CWnd *pParentWnd, CString &sPath, CStringArray &asNames, LPCTSTR pTitle, LPCTSTR pFilter, BOOL bOpenFile/*=TRUE*/, DWORD dwFlags/*=0*/)
//{
//	CByteArray	abFilesBuf;		// 名前格納バッファ拡張
////	char		*c_path	;
//
//	LPCTSTR	pPathFile = NULL;
////	if ((c_path = (char *)malloc (sizeof (char) * 102400)) == NULL) {
////		::MessageBox (NULL, "ワーク領域のメモリー確保に失敗しました。\nフォントの一時インストール又はフォント登録を終了します。", "エラーメッセージ", MB_OK | MB_ICONERROR) ;
////		return FALSE ;
////	}
////	memset (c_path, 0, sizeof (char) * 102400) ;
//
//	asNames.RemoveAll();
//
//	if(!CFileName::IsDirectory(sPath))
//		pPathFile = sPath;
//	CFileDialog dlg(bOpenFile, NULL, pPathFile, dwFlags, pFilter, pParentWnd);
//	if(pPathFile == NULL)
//		dlg.m_ofn.lpstrInitialDir = sPath;
////		POFN(dlg)->lpstrInitialDir = sPath;
//	if(pTitle)
//		dlg.m_ofn.lpstrTitle = pTitle;
////		POFN(dlg)->lpstrTitle = pTitle;
//	if(dwFlags | OFN_ALLOWMULTISELECT) {
////		abFilesBuf.SetSize(1024 * 1024);
////		abFilesBuf.SetAt(0, 0x00);
////		POFN(dlg)->nMaxFile  = abFilesBuf.GetSize() / sizeof TCHAR;
////		POFN(dlg)->lpstrFile = (LPTSTR)abFilesBuf.GetData();
////		dlg.m_ofn.nMaxFile  = abFilesBuf.GetSize() / sizeof TCHAR;
////		dlg.m_ofn.lpstrFile = (LPTSTR)abFilesBuf.GetData();
//		dlg.m_ofn.nMaxFile  = 102400;
//		dlg.m_ofn.lpstrFile = c_path ;
//	}
//
//	if(dlg.DoModal() != IDOK) {
//		DWORD eee = CommDlgExtendedError () ;
//		free (c_path) ;
//		return FALSE;
//	}
//
//	sPath = dlg.GetPathName();
//
//	if(dwFlags | OFN_ALLOWMULTISELECT) {
//		POSITION	pos;
//		pos = dlg.GetStartPosition();
//		while(pos)
//			asNames.Add(dlg.GetNextPathName(pos));
//	}
//	else
//		asNames.Add(sPath);
//	free (c_path) ;
//	return TRUE;
//}

BOOL CBrowseFile::BrowseFile(CWnd *pParentWnd, CString &sPath, CStringArray &asNames, LPCTSTR pTitle, LPCTSTR pFilter, BOOL bOpenFile/*=TRUE*/, DWORD dwFlags/*=0*/)
{
	CByteArray	abFilesBuf;		// 名前格納バッファ拡張

	LPCTSTR	pPathFile = NULL;

	asNames.RemoveAll();

	if(!CFileName::IsDirectory(sPath))
		pPathFile = sPath;
	CFileDialog dlg(bOpenFile, NULL, pPathFile, dwFlags, pFilter, pParentWnd);
	if(pPathFile == NULL)
		POFN(dlg)->lpstrInitialDir = sPath;
	if(pTitle)
		POFN(dlg)->lpstrTitle = pTitle;

	if(dwFlags | OFN_ALLOWMULTISELECT) {
		abFilesBuf.SetSize(102400);
		abFilesBuf.SetAt(0, 0x00);
		POFN(dlg)->nMaxFile  = abFilesBuf.GetSize() / sizeof TCHAR;
		POFN(dlg)->lpstrFile = (LPTSTR)abFilesBuf.GetData();
	}

	if(dlg.DoModal() != IDOK)
		return FALSE;

	sPath = dlg.GetPathName();

	if(dwFlags | OFN_ALLOWMULTISELECT) {
		POSITION	pos;
		pos = dlg.GetStartPosition();
		while(pos)
			asNames.Add(dlg.GetNextPathName(pos));
	}
	else
		asNames.Add(sPath);

	return TRUE;
}
