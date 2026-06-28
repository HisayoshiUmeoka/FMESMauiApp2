#pragma once

class CBrowseFile
{
public:
	CBrowseFile(void);
	~CBrowseFile(void);
	static BOOL BrowseFolder(CWnd *pParentWnd, CString &sPath, LPCTSTR pTitle);
	static BOOL BrowseFile(CWnd *pParentWnd, CString &sPath, LPCTSTR pTitle, LPCTSTR pFilter, BOOL bOpenFile=TRUE, DWORD dwFlags=OFN_HIDEREADONLY);
	static BOOL BrowseFile(CWnd *pParentWnd, CString &sPath, CStringArray &asNames, LPCTSTR pTitle, LPCTSTR pFilter, BOOL bOpenFile=TRUE, DWORD dwFlags=OFN_HIDEREADONLY);
};
