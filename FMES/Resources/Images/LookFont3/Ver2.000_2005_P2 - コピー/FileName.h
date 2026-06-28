#pragma once

class CFileName
{
public:
	CFileName(void);
	~CFileName(void);
	static bool IsSepChar(TCHAR c) { return c == '/' || c == '\\'; };
	static CString GetCurrentDirectory();
	static BOOL IsFile(LPCTSTR pPath);
	static BOOL IsDirectory(LPCTSTR pPath);
	static BOOL IsLastCharDir(LPCTSTR pPath);
	static BOOL MakeDirs(LPCTSTR pPath);
	static BOOL RemoveDirs(LPCTSTR pPath);
	static CString AddLastDirChar(LPCTSTR pPath);
	static CString Join(LPCTSTR pNema1, LPCTSTR pName2);
	static CString AddExt(LPCTSTR pFileName, LPCTSTR pExt);
	static CString GetPath(LPCTSTR pFileName);
	static CString GetFile(LPCTSTR pFileName);
	static CString GetExt(LPCTSTR pFileName);
	static CString GetDeleteExt(LPCTSTR pFileName);
	static CString GetTitle(LPCTSTR pFileName);
	static CString GetFullPath(LPCTSTR pPath);
	static CString DeleteRightBS(LPCTSTR pPath);
	static CString AddRightBS(LPCTSTR pPath);
};
