#pragma once

#include <Shlobj.h>

class CBrowseFolder
{
public:
	CBrowseFolder(CWnd *pParentWnd=NULL, LPCTSTR pDefaultPath=NULL, LPCTSTR pTitle=NULL, ULONG ulFlags=BIF_RETURNONLYFSDIRS|BIF_STATUSTEXT
#ifdef BIF_NEWDIALOGSTYLE
		|BIF_NEWDIALOGSTYLE
#endif
		);
	~CBrowseFolder(void);
	int DoModal();
	CString GetPath();

protected:
	static int CALLBACK BrowseCallbackProc(HWND hWnd, UINT uMsg, LPARAM lParam, LPARAM lpData);

	CWnd*					m_pWnd;
	CString					m_sDefaultPath;
	CString					m_sTitle;
	ULONG					m_ulFlags;
	CString					m_sSelectPath;
};
