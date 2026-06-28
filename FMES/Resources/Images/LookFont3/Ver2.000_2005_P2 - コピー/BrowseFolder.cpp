/////////////////////////////////////////////////////////////////////////////
// フォルダ選択クラス
//	参考文献：http://techtips.belution.com/ja/vc/0069/ 他
/////////////////////////////////////////////////////////////////////////////
#define	WINVER	0x0400
#include <afxwin.h>
#ifdef _DEBUG
#define new DEBUG_NEW
#endif
#include "BrowseFolder.h"

// コンストラクタ
//	pParent			I	親CWnd NULL:ディフォルト
//	pDefaultPath	I	ディフォルトフォルダ NULL:ディフォルト
//	pTitle			I	タイトル NULL:ディフォルト
//	ulFlags			I	フラグ
//						BIF_RETURNONLYFSDIRS   フォルダのみ選択可能
//						BIF_STATUSTEXT         選択されたフォルダをテキストに表示
//						BIF_NEWDIALOGSTYLE     新規作成ボタン表示
//						BIF_BROWSEINCLUDEFILES 全てのファイルを表示する(？)
//						BIF_DONTGOBELOWDOMAIN  特殊フォルダを表示する
//						他
CBrowseFolder::CBrowseFolder(CWnd *pParentWnd/*=NULL*/, LPCTSTR pDefaultPath/*=NULL*/, LPCTSTR pTitle/*=NULL*/, ULONG ulFlags/*=BIF_RETURNONLYFSDIRS|BIF_STATUSTEXT|BIF_NEWDIALOGSTYLE*/)
{
	m_pWnd = pParentWnd;
	if(pDefaultPath)
		m_sDefaultPath = pDefaultPath;
	if(pTitle)
		m_sTitle = pTitle;
	m_ulFlags = ulFlags;
}

CBrowseFolder::~CBrowseFolder(void)
{
}

// フォルダ選択ダイアログ表示
// return
int CBrowseFolder::DoModal()
{
	BROWSEINFO		bi;
	TCHAR			cSelectPath[_MAX_PATH];
	TCHAR			cDisplayName[_MAX_PATH];
	LPITEMIDLIST	pidl;
	LPMALLOC		pMalloc;
	int				nRet = IDCANCEL;

	// Shell の標準のアロケータを取得
	if(SUCCEEDED(SHGetMalloc(&pMalloc))) {
		// BROWSEINFO 構造体を埋める
		bi.hwndOwner      = m_pWnd == NULL? NULL : m_pWnd->GetSafeHwnd();
		bi.pidlRoot       = NULL;
		bi.pszDisplayName = cDisplayName;
		bi.lpszTitle      = m_sTitle.IsEmpty() ? (LPCTSTR)NULL : m_sTitle;
		bi.ulFlags        = m_ulFlags;
		bi.lpfn           = BrowseCallbackProc;
		bi.lParam         = (LPARAM)this;

		// フォルダの参照ダイアログボックスの表示
		pidl = SHBrowseForFolder(&bi);
		if(pidl) {
			// PIDL をファイルシステムのパスに変換
			if(SHGetPathFromIDList(pidl, cSelectPath)) {
				m_sSelectPath = cSelectPath;
				nRet = IDOK;
			}
			// SHBrowseForFolder によって割り当てられた PIDL を解放
			pMalloc->Free(pidl);
		}
		// Shell のアロケータを開放
		pMalloc->Release();
	}

	return nRet;
}

// 選択されたパスを取得
CString CBrowseFolder::GetPath()
{
	return m_sSelectPath;
}

/////////////////////////////////////////////////////////////////////////////
// 内部関数
/////////////////////////////////////////////////////////////////////////////

// コールバック
int CALLBACK CBrowseFolder::BrowseCallbackProc(HWND hWnd, UINT uMsg, LPARAM lParam, LPARAM pData)
{
	CBrowseFolder* pParent = (CBrowseFolder*)pData;

	switch(uMsg) {
	case BFFM_INITIALIZED:
		if(!pParent->m_sDefaultPath.IsEmpty())
	        SendMessage(hWnd, BFFM_SETSELECTION, (WPARAM)TRUE, (LPARAM)(LPCTSTR)pParent->m_sDefaultPath);
	case BFFM_SELCHANGED:
		//sample if( SHGetPathFromIDList((LPCITEMIDLIST)lParam, lpPath) ) {
		//sample	SendMessage(hWnd, BFFM_SETSTATUSTEXT, 0, (LPARAM)lpPath);
		break;
	default:
		break;
    }

	return 0;
}
