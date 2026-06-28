#pragma once
#include <afxcoll.h>

class CSepLine :
	public CStringArray
{
public:
	CSepLine(void);
	~CSepLine(void);
	int Sep(LPCTSTR pLine, LPCTSTR pTokens=_T(" \t\r\n"), BOOL bOneTokenOneItem=FALSE, BOOL bDisableDblQuo=FALSE, BOOL bTokenAddList=FALSE);
	static CString DelDblQuo(LPCTSTR pText);
	CString AddDblQuo(LPCTSTR pText);
	void DelDblQuoAll();
	static CString TrimLR(LPCTSTR pText);
	void TrimLRAll();
};
