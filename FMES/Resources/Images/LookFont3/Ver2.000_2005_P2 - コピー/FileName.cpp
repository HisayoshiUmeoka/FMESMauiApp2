/////////////////////////////////////////////////////////////////////////////
// ファイル名操作関数郡
// 命名規則
//	Title			ファイルタイトル
//	Ext				ファイル拡張子
//	File			ファイル名(タイトル + "." + 拡張子)
//	FileName		ファイル名(絶対パス又は相対パス名を含む場合もある)
//	Path			絶対又は相対パス名(ファイル名を含む場合もある)
//	PathFile		パス名+ファイル名
//	FullPath		絶対パス名(ファイル名を含む場合もある)
//	FullPathFile	絶対パス名+ファイル名
/////////////////////////////////////////////////////////////////////////////
#define	WINVER	0x0400
#include <afxwin.h>
#ifdef _DEBUG
#define new DEBUG_NEW
#endif
#include <direct.h>
#include "SepLine.h"
#include "FileName.h"

CFileName::CFileName(void)
{
}

CFileName::~CFileName(void)
{
}

/////////////////////////////////////////////////////////////////////////////
// 情報取得
/////////////////////////////////////////////////////////////////////////////

CString CFileName::GetCurrentDirectory()
{
	TCHAR	cPath[_MAX_PATH];
	::GetCurrentDirectory(sizeof cPath, cPath);
	return cPath;
}

/////////////////////////////////////////////////////////////////////////////
// 判定
/////////////////////////////////////////////////////////////////////////////

// パスがファイル名か調べる
BOOL CFileName::IsFile(LPCTSTR pPath)
{
	DWORD dwAttrib = GetFileAttributes(pPath);
	if(dwAttrib == -1)
		return FALSE;
	if(dwAttrib & FILE_ATTRIBUTE_DIRECTORY)
		return FALSE;
	return TRUE;
}

// パスがディレクトリか調べる
BOOL CFileName::IsDirectory(LPCTSTR pPath)
{
	DWORD dwAttrib = GetFileAttributes(pPath);
	if(dwAttrib == -1)
		return FALSE;
	if(dwAttrib & FILE_ATTRIBUTE_DIRECTORY)
		return TRUE;
	return FALSE;
}

// 最後の文字がディレクトリ文字か調べる
BOOL CFileName::IsLastCharDir(LPCTSTR pPath)
{
	LPCTSTR	pCur;

	pCur = _tcsdec(pPath, pPath + _tcslen(pPath));
	if(pCur != NULL && IsSepChar(*pCur))
		return TRUE;
	return FALSE;
}

/////////////////////////////////////////////////////////////////////////////
// ファイル名操作関数郡
/////////////////////////////////////////////////////////////////////////////

// 階層フォルダ作成
BOOL CFileName::MakeDirs(LPCTSTR pPath)
{
	CSepLine	sl;
	CString		sCurPath;
	int			i, l;

	l = sl.Sep(pPath, _T("\\/"), TRUE, TRUE, TRUE);

	for(i = 0; i < l; i++) {
		sCurPath += sl.GetAt(i);
		if(IsSepChar(sl.GetAt(i).GetAt(0)))	// セパレータ
			continue;
		if(IsDirectory(sCurPath))	// 既に存在する
			continue;
		if(CreateDirectory(sCurPath, NULL) != 0)
			return FALSE;
	}

	return TRUE;
}

// 階層フォルダ削除
BOOL CFileName::RemoveDirs(LPCTSTR pPath)
{
	HANDLE			hFind;
	WIN32_FIND_DATA	fd;
	CString			sFind;
	CString			sSubName;
	CStringArray	asFile;
	CStringArray	asDir;
	int				i;

	sFind = Join(pPath, _T("*"));
	hFind = FindFirstFile(sFind, &fd);
	if(hFind != INVALID_HANDLE_VALUE) {
		do {
			if(_tcscmp(fd.cFileName, _T(".")) == 0 || _tcscmp(fd.cFileName, _T("..")) == 0)
				continue;
			sSubName = Join(pPath, fd.cFileName);
			if(fd.dwFileAttributes | (FILE_ATTRIBUTE_READONLY | FILE_ATTRIBUTE_HIDDEN))
				SetFileAttributes(sSubName, fd.dwFileAttributes & ~(FILE_ATTRIBUTE_READONLY | FILE_ATTRIBUTE_HIDDEN));
			if(fd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
				asDir.Add(sSubName);
			else
				asFile.Add(sSubName);
		} while(FindNextFile(hFind, &fd));
		FindClose(hFind);
	}

	for(i = 0; i < asFile.GetSize(); i++)
		DeleteFile(asFile.GetAt(i));

	for(i = 0; i < asDir.GetSize(); i++)
		RemoveDirs(asDir.GetAt(i));

	return RemoveDirectory(pPath);
}

// パスの最後にディレクトリ文字を付加する
CString CFileName::AddLastDirChar(LPCTSTR pPath)
{
	if(IsLastCharDir(pPath))
		return pPath;
	return (CString)pPath + "\\";
}

// ２つのパスを繋げる
CString CFileName::Join(LPCTSTR pPath1, LPCTSTR pPath2)
{
	CString	str = pPath1;
	LPCTSTR	pCur;

	if(_tcslen(pPath1) == 0 && _tcslen(pPath2) == 0) {
		str += pPath2;
		return str;
	}

	pCur = _tcsdec(pPath1, pPath1 + _tcslen(pPath1));
	if(pCur != NULL && !IsSepChar(*pCur))
		str += "\\";
	str += pPath2;

	return str;
}

// 拡張子を付加する
CString CFileName::AddExt(LPCTSTR pFileName, LPCTSTR pExt)
{
	CString	str = pFileName;
	LPCTSTR	pCur;

	pCur = _tcsdec(pFileName, pFileName + _tcslen(pFileName));
	if(pCur != NULL && (*pCur) != '.')
		str += ".";
	str += pExt;

	return str;
}

// パスファイル名からパスを得る
CString CFileName::GetPath(LPCTSTR pFileName)
{
	CString	str = pFileName;
	LPCTSTR	pCur;
	int		nFilePos = 0;

	for(pCur = pFileName; *pCur != '\0'; pCur = _tcsinc(pCur)) {
		if(IsSepChar(*pCur))
			nFilePos = pCur - pFileName;
	}

	return str.Left(nFilePos);
}

// パスファイル名からファイル名を得る
CString CFileName::GetFile(LPCTSTR pFileName)
{
	CString	str = pFileName;
	LPCTSTR	pCur;
	int		nFilePos = 0;

	for(pCur = pFileName; *pCur != '\0'; pCur = _tcsinc(pCur)) {
		if(IsSepChar(*pCur))
			nFilePos = pCur - pFileName;
	}

	return str.Mid(nFilePos + 1);
}

// パスファイル名からファイルタイトル名を得る
CString CFileName::GetTitle(LPCTSTR pFileName)
{
	CString	str = pFileName;
	LPCTSTR	pCur;
	int		nFilePos = 0;
	int		nExtPos = -1;

	for(pCur = pFileName; *pCur != '\0'; pCur = _tcsinc(pCur)) {
		if(IsSepChar(*pCur))
			nFilePos = pCur - pFileName + 1;
		if(*pCur == '.')
			nExtPos = pCur - pFileName;
	}

	if(nExtPos < nFilePos)
		return str.Mid(nFilePos);

	return str.Mid(nFilePos, nExtPos - nFilePos);
}

// パスファイル名からファイル拡張子名を得る
CString CFileName::GetExt(LPCTSTR pFileName)
{
	CString	str = pFileName;
	LPCTSTR	pCur;
	int		nFilePos = 0;
	int		nExtPos = -1;

	for(pCur = pFileName; *pCur != '\0'; pCur = _tcsinc(pCur)) {
		if(IsSepChar(*pCur))
			nFilePos = pCur - pFileName + 1;
		if(*pCur == '.')
			nExtPos = pCur - pFileName;
	}

	if(nExtPos < nFilePos)
		return "";

	return str.Mid(nExtPos + 1);
}

// パスファイル名からファイル拡張子名を除いたものを得る
CString CFileName::GetDeleteExt(LPCTSTR pFileName)
{
	CString	str = pFileName;
	LPCTSTR	pCur;
	int		nFilePos = 0;
	int		nExtPos = -1;

	for(pCur = pFileName; *pCur != '\0'; pCur = _tcsinc(pCur)) {
		if(IsSepChar(*pCur))
			nFilePos = pCur - pFileName + 1;
		if(*pCur == '.')
			nExtPos = pCur - pFileName;
	}

	if(nExtPos < nFilePos)
		return str;

	return str.Left(nExtPos);
}

// パス名からフルパス名を得る
CString CFileName::GetFullPath(LPCTSTR pPath)
{
	TCHAR	cFullPath[_MAX_PATH];
	LPTSTR	pFilePart;

	if(!GetFullPathName(pPath, sizeof cFullPath , cFullPath, &pFilePart))
		return "";

	return cFullPath;
}

// パス名の最後に \ があれば取る
CString CFileName::DeleteRightBS(LPCTSTR pPath)
{
	CString	str = pPath;
	LPCTSTR	pCur;

	while((pCur = _tcsdec(pPath, pPath + _tcslen(pPath))) != NULL) {
		if(IsSepChar(*pCur))
			str = str.Left(str.GetLength() - 1);
	}

	return str;
}

// パス名の最後に \ が無ければ付ける
CString CFileName::AddRightBS(LPCTSTR pPath)
{
	CString	str = pPath;
	LPCTSTR	pCur;

	pCur = _tcsdec(pPath, pPath + _tcslen(pPath));
	if(pCur == NULL || !IsSepChar(*pCur))
		str += "\\";

	return str;
}

/////////////////////////////////////////////////////////////////////////////
// ファイル関連汎用関数郡
/////////////////////////////////////////////////////////////////////////////
