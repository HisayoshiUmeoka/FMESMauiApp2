// LookFontView.cpp : CLookFontView クラスの動作の定義を行います。
//

#include "stdafx.h"
#include <math.h>
#include <shellapi.h>
#include <AutoUpdateIf.h>
#include "LookFont.h"
#include "Rtf.h"
#include "CharCode.h"
#include "FileName.h"
#include "BrowseFile.h"
#include "BrowseFolder.h"
#include "LookFontDoc.h"
#include "LookFontView.h"
#include "SettingDlg.h"
#include "FindFontDlg.h"
#include "DialogFontInfo.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CLookFontView

IMPLEMENT_DYNCREATE(CLookFontView, CView)

BEGIN_MESSAGE_MAP(CLookFontView, CView)
	//{{AFX_MSG_MAP(CLookFontView)
	ON_WM_VSCROLL()
	ON_WM_SIZE()
	ON_WM_KEYDOWN()
	ON_WM_MOUSEWHEEL()
	ON_WM_MOUSEMOVE()
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_LBUTTONDBLCLK()
	ON_WM_RBUTTONDOWN()
	ON_EN_CHANGE(IDC_EDIT_DISPTEXT, OnChangeEditDisptext)
	ON_CBN_EDITCHANGE(IDC_COMBO_FONTSIZE, OnEditchangeComboFontsize)
	ON_CBN_SELCHANGE(IDC_COMBO_FONTSIZE, OnSelchangeComboFontsize)
	ON_COMMAND(ID_EDIT_COPY, OnEditCopy)
	ON_COMMAND(ID_MENU_COPYFONTFACE, OnMenuCopyfontface)
	ON_COMMAND(ID_EDIT_PASTE, OnEditPaste)
	ON_COMMAND(ID_MENU_UPDATE_CHECK, OnMenuUpdateCheck)
	ON_UPDATE_COMMAND_UI(ID_MENU_UPDATE_CHECK, OnUpdateMenuUpdateCheck)
	ON_COMMAND(ID_MENU_TMPFONT_INSTALL, OnMenuTmpfontInstall)
	ON_UPDATE_COMMAND_UI(ID_MENU_TMPFONT_INSTALL, OnUpdateMenuTmpfontInstall)
	ON_COMMAND(ID_MENU_TMPFONT_UNINSTALL, OnMenuTmpfontUninstall)
	ON_UPDATE_COMMAND_UI(ID_MENU_TMPFONT_UNINSTALL, OnUpdateMenuTmpfontUninstall)
	ON_COMMAND(ID_MENU_SEL_LANG_EN, OnMenuSelLangEn)
	ON_UPDATE_COMMAND_UI(ID_MENU_SEL_LANG_EN, OnUpdateMenuSelLangEn)
	ON_COMMAND(ID_MENU_SEL_LANG_JA, OnMenuSelLangJa)
	ON_UPDATE_COMMAND_UI(ID_MENU_SEL_LANG_JA, OnUpdateMenuSelLangJa)
	ON_COMMAND(ID_MENU_SEL_LANG_ALL, OnMenuSelLangAll)
	ON_UPDATE_COMMAND_UI(ID_MENU_SEL_LANG_ALL, OnUpdateMenuSelLangAll)
	ON_COMMAND(ID_MENU_SEL_FONT_TMP, OnMenuSelFontTmp)
	ON_UPDATE_COMMAND_UI(ID_MENU_SEL_FONT_TMP, OnUpdateMenuSelFontTmp)
	ON_COMMAND(ID_MENU_SEL_FONT_ORG, OnMenuSelFontOrg)
	ON_UPDATE_COMMAND_UI(ID_MENU_SEL_FONT_ORG, OnUpdateMenuSelFontOrg)
	ON_COMMAND(ID_MENU_SETTING, OnMenuSetting)
	ON_COMMAND(ID_MENU_TMPFONT_FIND, OnMenuTmpfontFind)
	ON_UPDATE_COMMAND_UI(ID_MENU_TMPFONT_FIND, OnUpdateMenuTmpfontFind)
	ON_MESSAGE(LF_WM_FRAME_CLOSE, OnFrameClose)
	ON_MESSAGE(AUI_WM_EVENT, OnAuiEvent)
	ON_COMMAND(ID_MENU_PROPATY, OnMenuPropaty)
	ON_UPDATE_COMMAND_UI(ID_MENU_PROPATY, OnUpdateMenuPropaty)
	ON_COMMAND(ID_ADD_FAVORITE, OnMenuAddFavorite)
	ON_UPDATE_COMMAND_UI(ID_ADD_FAVORITE, OnUpdateMenuAddFavorite)
	ON_COMMAND(ID_ADD_CANDIDATE, OnMenuAddCandidate)
	ON_UPDATE_COMMAND_UI(ID_ADD_CANDIDATE, OnUpdateMenuAddCandidate)
	ON_COMMAND(ID_DEL_FAVORITE, OnMenuDelFavorite)
	ON_UPDATE_COMMAND_UI(ID_DEL_FAVORITE, OnUpdateMenuDelFavorite)
	ON_COMMAND(ID_DEL_CANDIDATE, OnMenuDelCandidate)
	ON_UPDATE_COMMAND_UI(ID_DEL_CANDIDATE, OnUpdateMenuDelCandidate)
	ON_COMMAND(ID_INSTALL_FONT, OnMenuFontInstall)
	ON_UPDATE_COMMAND_UI(ID_INSTALL_FONT, OnUpdateMenuFontInstall)

	ON_COMMAND(ID_NORMAL_LIST, OnMenuSelNormal)
	ON_UPDATE_COMMAND_UI(ID_NORMAL_LIST, OnUpdateMenuSelNormal)
	ON_COMMAND(ID_FAVORITE, OnMenuSelFavorite)
	ON_UPDATE_COMMAND_UI(ID_FAVORITE, OnUpdateMenuSelFavorite)
	ON_COMMAND(ID_CANDIDATE, OnMenuSelCandidate)
	ON_UPDATE_COMMAND_UI(ID_CANDIDATE, OnUpdateMenuSelCandidate)
	//}}AFX_MSG_MAP
	ON_COMMAND(ID_FILE_PRINT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, OnFilePrintPreview)
	ON_COMMAND(ID_MENU_TEST, &CLookFontView::OnMenuTest)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CLookFontView クラスの構築/消滅

CLookFontView::CLookFontView()
{
	m_bInit          = FALSE;
	m_Ini.SelLangEN  = FALSE;
	m_Ini.SelLangJA  = FALSE;
	m_Ini.SelLangALL = FALSE;
	m_Ini.SelFontTMP = FALSE;
	m_Ini.SelFontORG = FALSE;
	m_Ini.DispMode = 0;
	ReadIni();
	m_FontList.m_Ini = &m_Ini ;
	m_eAuStatus      = AUSTATUS_IDLE;
	m_bReview = FALSE ;
	m_bInstall = CanDoFromWindowsVer() ;
}

CLookFontView::~CLookFontView()
{
}

BOOL CLookFontView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: この位置で CREATESTRUCT cs を修正して Window クラスまたはスタイルを
	//  修正してください。
	cs.style |= WS_VSCROLL;
	BOOL bRet = CView::PreCreateWindow(cs);
	return bRet;
}

/////////////////////////////////////////////////////////////////////////////
// CLookFontView クラスの描画

void CLookFontView::OnDraw(CDC* pDC)
{
	CLookFontDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	// TODO: この場所にネイティブ データ用の描画コードを追加します。
	int		cdDv = pDC->GetDeviceCaps (TECHNOLOGY) ;
	if (m_bReview == TRUE && cdDv == DT_RASPRINTER) {
		DrawPrinter(pDC, m_PrintPageNo) ;
	}
	else {
		if(!m_bInit) {
			Init(pDC);
			m_bInit = TRUE;
		}
		else {
			UpdateText(pDC);
		}
	}
}

/////////////////////////////////////////////////////////////////////////////
// CLookFontView クラスの診断

#ifdef _DEBUG
void CLookFontView::AssertValid() const
{
	CView::AssertValid();
}

void CLookFontView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CLookFontDoc* CLookFontView::GetDocument() // 非デバッグ バージョンはインラインです。
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CLookFontDoc)));
	return (CLookFontDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// システムメッセージハンドラ
/////////////////////////////////////////////////////////////////////////////

// F1ヘルプ

void CLookFontView::OnSize(UINT nType, int cx, int cy) 
{
	CView::OnSize(nType, cx, cy);
	
	// TODO: この位置にメッセージ ハンドラ用のコードを追加してください
	if(!m_bInit)
		return;

	UpdateText();
}

void CLookFontView::OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	int 	nNewPos;

	if(m_nDispCount == 0)
		return;

	nNewPos = m_nDispTopPos;

	switch(nSBCode) {
	case SB_TOP:
		m_nDispCurPos = nNewPos = 0;
		break;
	case SB_BOTTOM:
		m_nDispCurPos = nNewPos = m_nDispCount - 1;
		break;
	case SB_ENDSCROLL:
		break;
	case SB_LINEUP:
		if(nNewPos > 0)
			nNewPos--;
		break;
	case SB_LINEDOWN:
		if(nNewPos <= m_nDispCount - m_nDispLine - 1)
			nNewPos++;
		break;
	case SB_PAGEUP:
		if(nNewPos > 0)
			nNewPos -= m_nDispLine;
		if(nNewPos < 0)
			nNewPos = 0;
		break;
	case SB_PAGEDOWN:
		if(nNewPos <= m_nDispCount - m_nDispLine - 1)
			nNewPos += m_nDispLine;
		break;
	case SB_THUMBPOSITION:
	case SB_THUMBTRACK:
		nNewPos = nPos;
		break;
		if(nNewPos < 0)
			nNewPos = 0;
		else if(nNewPos <= m_nDispCount - m_nDispLine - 1)
			nNewPos = m_nDispCount - m_nDispLine;
	default:
		break;
	}

	if(nNewPos == m_nDispTopPos)
		return;

	m_nCtrlWheel = 0;

	m_nDispTopPos = nNewPos;
	UpdateText();

//	CView::OnVScroll(nSBCode, nPos, pScrollBar);
}

void CLookFontView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	int			i;
	CClientDC	dc(this);

	switch(nChar) {
	case VK_UP:
		if(m_nDispCurPos > m_nDispTopPos) {
			SetCur(&dc, m_nDispCurPos, FALSE);
			m_nDispCurPos--;
			SetCur(&dc, m_nDispCurPos, TRUE);
		}
		else if(m_nDispCurPos > 0) {
			m_nDispTopPos--;
			m_nDispCurPos--;
			UpdateText(&dc);
		}
		return;
	case VK_DOWN:
		if(m_nDispCurPos < m_nDispTopPos + m_nDispLine - 1) {
			SetCur(&dc, m_nDispCurPos, FALSE);
			m_nDispCurPos++;
			SetCur(&dc, m_nDispCurPos, TRUE);
		}
		else if(m_nDispTopPos <= m_nDispCount - m_nDispLine - 1) {
			m_nDispTopPos++;
			m_nDispCurPos++;
			UpdateText(&dc);
		}
		return;
	case VK_PRIOR:
		OnVScroll(SB_PAGEUP, 0, NULL);
		return;
	case VK_NEXT:
		OnVScroll(SB_PAGEDOWN, 0, NULL);
		return;
	case VK_HOME:
		if(GetKeyState(VK_LCONTROL) || GetKeyState(VK_RCONTROL)) {
			OnVScroll(SB_TOP, 0, NULL);
			return;
		}
	case VK_END:
		if(GetKeyState(VK_LCONTROL) || GetKeyState(VK_RCONTROL)) {
			OnVScroll(SB_BOTTOM, 0, NULL);
			return;
		}
		return;
	default:
		if(_istgraph(nChar)) {
			// ファーストキャラクタ検索
			for(i = 0; i < m_FontList.GetSize(); i++) {
				if(toupper(m_FontList.GetFontFaceAt(i).GetAt(0)) == toupper(nChar))
					break;
			}
			if(i < m_FontList.GetSize()) {
				m_nDispTopPos = i;
				m_nDispCurPos = i;
				UpdateText(&dc);
			}
			return;
		}
	}

	CView::OnKeyDown(nChar, nRepCnt, nFlags);
}

BOOL CLookFontView::OnMouseWheel(UINT nFlags, short zDelta, CPoint pt) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	int		nLine;

	m_nCtrlWheel += zDelta;
	nLine = m_nCtrlWheel / WHEEL_DELTA;

	if(nLine == 0)
		return TRUE;

	m_nCtrlWheel -= nLine * WHEEL_DELTA;

	int 	nNewPos;
	nNewPos = m_nDispTopPos;

	if(nLine > 0) {
		nNewPos -= nLine;
		if(nNewPos < 0)
			nNewPos = 0;
	}
	else {
		if(nNewPos <= m_nDispCount - m_nDispLine - 1)
			nNewPos -= nLine;
		if(nNewPos >= m_nDispCount) {
			if(m_nDispCount == 0)
				nNewPos = 0;
			else
				nNewPos = m_nDispCount - 1;
		}
	}

	if(nNewPos == m_nDispTopPos)
		return TRUE;

	m_nDispTopPos = nNewPos;
	UpdateText();

	return TRUE;
//	return CView::OnMouseWheel(nFlags, zDelta, pt);
}

void CLookFontView::OnMouseMove(UINT nFlags, CPoint point) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	if(m_bDispMoveWidth) {
		MoveWidth(point);
		SetCursor(AfxGetApp()->LoadStandardCursor(IDC_SIZEWE));
		return;
	}

	if(point.y <= m_nDispBottom &&
		point.x >= m_Ini.Width1 - LF_MC_HITPIX && point.x <= m_Ini.Width1 + LF_MC_HITPIX + 1)
		SetCursor(AfxGetApp()->LoadStandardCursor(IDC_SIZEWE));

	CView::OnMouseMove(nFlags, point);
}

// nFlags	I	-1 内部コール＝列境界移動はしない
void CLookFontView::OnLButtonDown(UINT nFlags, CPoint point) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	int			i, nNewPos;
	CClientDC	dc(this);

	// 列幅変更
	if(m_bDispMoveWidth)
		return;

	if(nFlags != -1) {
		// 列境界線上か？
		if(point.y <= m_nDispBottom &&
			point.x >= m_Ini.Width1 - LF_MC_HITPIX && point.x <= m_Ini.Width1 + LF_MC_HITPIX + 1) {
			m_bDispMoveWidth = TRUE;
			SetCapture();
			MoveWidth(point);
			return;
		}
	}

	// クリックされた場所
	nNewPos = -1;
	for(i = 0; i < m_anDispY.GetSize(); i++) {
		if(point.y < (int)(m_anDispY.GetAt(i) + m_anDispCY.GetAt(i) + LF_SPACEFRAME * 2 + LF_SPACELINE)) {
			nNewPos = i;
			break;
		}
	}

	if(nNewPos < 0)
		return;

	if(nNewPos < m_nDispLine) {
		// 画面範囲内
		if(m_nDispTopPos + nNewPos != m_nDispCurPos) {
			SetCur(&dc, m_nDispCurPos, FALSE);
			m_nDispCurPos = m_nDispTopPos + nNewPos;
			SetCur(&dc, m_nDispCurPos, TRUE);
		}
	}
	else {
		// 最下段の半端表示域
		nNewPos = m_anDispY.GetSize();
		if(m_nDispTopPos + nNewPos < m_nDispCount) {
			m_nDispCurPos = m_nDispTopPos + nNewPos;
			m_nDispTopPos++;
			UpdateText(&dc);
		}
	}

	CView::OnLButtonDown(nFlags, point);
}

void CLookFontView::OnLButtonUp(UINT nFlags, CPoint point) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	if(m_bDispMoveWidth) {
		MoveWidth(point);
		m_bDispMoveWidth = FALSE;
		ReleaseCapture();
		return;
	}

	CView::OnLButtonUp(nFlags, point);
}

void CLookFontView::OnLButtonDblClk(UINT nFlags, CPoint point) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	
	CView::OnLButtonDblClk(nFlags, point);
}

void CLookFontView::OnRButtonDown(UINT nFlags, CPoint point) 
{
	// TODO: この位置にメッセージ ハンドラ用のコードを追加するかまたはデフォルトの処理を呼び出してください
	POINT		pointScrn;
	CMenu		menuTop;
	CMenu*		pMenu;

	OnLButtonDown(-1, point);

	GetCursorPos(&pointScrn);
	menuTop.LoadMenu(IDR_MENU_RCLICK);
	pMenu = menuTop.GetSubMenu(0);

	pMenu->TrackPopupMenu(TPM_TOPALIGN | TPM_LEFTALIGN | TPM_RIGHTBUTTON, pointScrn.x, pointScrn.y, this);
//	CView::OnRButtonDown(nFlags, point);
}

/////////////////////////////////////////////////////////////////////////////
// メニューメッセージハンドラ
/////////////////////////////////////////////////////////////////////////////

void CLookFontView::OnMenuSetting() 
{
	CSettingDlg	dlg(IDS_SETTING_DIALOG, this);
	dlg.DoModal(&m_Ini);
}

// クリップボードへコピー
//	テキスト,Rich Text Format(RTF),拡張メタファイル(EMF) にコピー
//	暫定，試験コード多数あるため注意
//	根拠不明コードも多々あり
void CLookFontView::OnEditCopy() 
{
	int				i;
	CString			sCurText;	// クリップボードへセットする加工後文字
	CCharCode		cc;
	CByteArray		abText;
	HGLOBAL			hMem;
	LPTSTR			pStr;
	LPCSTR			pmText;
	LPSTR			pmStr;

	if(m_nDispCurPos < 0)
		return;

	// 有効文字に変換
	sCurText = m_FontList.AvailableChar(m_nDispCurPos, m_Ini.DispText);

	// クリップボードオープン
	for(i = 0; i < LF_CB_RETRY; i++) {
		if(OpenClipboard())
			break;
		Sleep(LF_CB_WAIT);
	}
	if(i >= LF_CB_RETRY)
		return;

	EmptyClipboard();

#ifndef _UNICODE
	// テキスト
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, sCurText.GetLength() + 1);
	pStr = (LPSTR)GlobalLock(hMem);
	strcpy(pStr, sCurText);
	GlobalUnlock(hMem);
	SetClipboardData(CF_TEXT, hMem);
#else
	// UNICODEテキスト
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, (sCurText.GetLength() + 1) * sizeof(wchar_t));
	pStr = (wchar_t*)GlobalLock(hMem);
	wcscpy(pStr, sCurText);
	GlobalUnlock(hMem);
	SetClipboardData(CF_UNICODETEXT, hMem);
#endif

	// RTF形式
	CRtf::ConvRtfText(abText, sCurText, m_FontList.GetFontFaceAt(m_nDispCurPos), m_Ini.DispFontSize, m_FontList.GetCharSetAt(m_nDispCurPos), m_FontList.IsBoldAt(m_nDispCurPos));
	pmText = (LPCSTR)abText.GetData();
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, strlen(pmText) + 1);
	pmStr = (LPSTR)GlobalLock(hMem);
	strcpy(pmStr, pmText);
	GlobalUnlock(hMem);
	SetClipboardData(RegisterClipboardFormat(CF_RTF), hMem);

	// 拡張メタファイル設定
	HENHMETAFILE	hMeta;
	hMeta = CreateEmf(sCurText);
	if(hMeta)
		SetClipboardData(CF_ENHMETAFILE, hMeta);

	CloseClipboard();
}

void CLookFontView::OnEditCopy2() 
{
	int				i;
	CString		sCurText;	// クリップボードへセットする加工後文字
	CCharCode		cc;
	CByteArray		abText;
	HGLOBAL			hMem;
	LPWSTR			pStr;
	LPCSTR			pmText;
	LPSTR			pmStr;

	if(m_nDispCurPos < 0)
		return;

	// 有効文字に変換
	sCurText = m_FontList.AvailableChar(m_nDispCurPos, m_Ini.DispText);

	// クリップボードオープン
	for(i = 0; i < LF_CB_RETRY; i++) {
		if(OpenClipboard())
			break;
		Sleep(LF_CB_WAIT);
	}
	if(i >= LF_CB_RETRY)
		return;

	EmptyClipboard();

//#ifndef _UNICODE
	// テキスト
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, sCurText.GetLength() + 1);
//	pStr = (LPSTR)GlobalLock(hMem);
//	strcpy(pStr, sCurText);
	GlobalUnlock(hMem);
	SetClipboardData(CF_UNICODETEXT, hMem);
//#else
//	// UNICODEテキスト
//	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, (sCurText.GetLength() + 1) * sizeof(wchar_t));
//	pStr = (wchar_t*)GlobalLock(hMem);
//	wcscpy(pStr, sCurText);
//	GlobalUnlock(hMem);
//	SetClipboardData(CF_UNICODETEXT, hMem);
//#endif

	// RTF形式
	CRtf::ConvURtfText(abText, sCurText, m_FontList.GetFontFaceAt(m_nDispCurPos), m_Ini.DispFontSize, m_FontList.GetCharSetAt(m_nDispCurPos), m_FontList.IsBoldAt(m_nDispCurPos));
	pmText = (LPCSTR)abText.GetData();
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, strlen(pmText) + 1);
	pmStr = (LPSTR)GlobalLock(hMem);
	strcpy(pmStr, pmText);
	GlobalUnlock(hMem);
	SetClipboardData(RegisterClipboardFormat(CF_RTF), hMem);

	// 拡張メタファイル設定
	HENHMETAFILE	hMeta;
	hMeta = CreateEmf(sCurText);
	if(hMeta)
		SetClipboardData(CF_ENHMETAFILE, hMeta);

	CloseClipboard();
}

void CLookFontView::OnMenuCopyfontface() 
{
	int				i;
	CCharCode		cc;
	LPCTSTR			pText;
	HGLOBAL			hMem;
	LPTSTR			pStr;

	// クリップボードオープン
	for(i = 0; i < LF_CB_RETRY; i++) {
		if(OpenClipboard())
			break;
		Sleep(LF_CB_WAIT);
	}
	if(i >= LF_CB_RETRY)
		return;

	EmptyClipboard();

#ifndef _UNICODE
	// テキスト
	pText = m_FontList.GetFontFaceAt(m_nDispCurPos);
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, strlen(pText) + 1);
	pStr = (LPSTR)GlobalLock(hMem);
	strcpy(pStr, pText);
	GlobalUnlock(hMem);
	SetClipboardData(CF_TEXT, hMem);
#else
	// UNICODEテキスト
	pText = m_FontList.GetFontFaceAt(m_nDispCurPos);
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, (wcslen(pText) + 1) * sizeof(wchar_t));
	pStr = (wchar_t*)GlobalLock(hMem);
	wcscpy(pStr, pText);
	GlobalUnlock(hMem);
	SetClipboardData(CF_UNICODETEXT, hMem);
#endif

	CloseClipboard();
}

// ペースト
void CLookFontView::OnEditPaste() 
{
	int			i;
	HANDLE		hClipData;
	LPCTSTR		pStr;

	if(GetFocus() == m_pWndTextCtrlBar->GetDlgItem(IDC_EDIT_DISPTEXT)) {
		((CEdit*)m_pWndTextCtrlBar->GetDlgItem(IDC_EDIT_DISPTEXT))->Paste();
		return;
	}

	// クリップボードオープン
	for(i = 0; i < LF_CB_RETRY; i++) {
		if(OpenClipboard())
			break;
		Sleep(LF_CB_WAIT);
	}
	if(i >= LF_CB_RETRY)
		return;

#ifndef _UNICODE
	hClipData = GetClipboardData(CF_TEXT);
#else
	hClipData = GetClipboardData(CF_UNICODETEXT);
#endif
	if(hClipData) {
		pStr = (LPCTSTR)GlobalLock(hClipData);
		m_Ini.DispText = pStr;
		GlobalUnlock(hClipData);
		m_pWndTextCtrlBar->SetDlgItemText(IDC_EDIT_DISPTEXT, m_Ini.DispText);
	}

	CloseClipboard();
}

// フォント一時インストール
void CLookFontView::OnMenuTmpfontInstall() 
{
	CStringArray	asFiles;
	CBrowseFile		bf;
	CString			sPath;
	int				i, j;
	BOOL			bAdd = FALSE;

//	if(!bf.BrowseFile(this, m_sFontOpenPath, asFiles, _T("フォントの一時インストール"), _T("Font Files (*.ttf)|*.ttf|All Files (*.*)|*.*||"), TRUE, OFN_HIDEREADONLY | OFN_ALLOWMULTISELECT))
	if(!bf.BrowseFile(this, m_sFontOpenPath, asFiles, _T("フォントの一時インストール"), _T("Font Files (*.ttf,*.ttc,*.fon,*.pfm,*.otf)|*.ttf;*.ttc;*.fon;*.pfm;*.otf|All Files (*.*)|*.*||"), TRUE, OFN_HIDEREADONLY | OFN_ALLOWMULTISELECT | OFN_FILEMUSTEXIST | OFN_PATHMUSTEXIST))
		return;

	for(i = 0; i < asFiles.GetSize(); i++) {

		for(j = 0; j < m_asTmpFontFiles.GetSize(); j++) {
			if(asFiles.GetAt(i).CompareNoCase(m_asTmpFontFiles.GetAt(j)) == 0)
				break;
		}
		if(j < m_asTmpFontFiles.GetSize())
			continue;
		if(m_FontList.AddFont(asFiles.GetAt(i), asFiles.GetAt(i)) > 0) {
			bAdd = TRUE;
			m_asTmpFontFiles.Add(asFiles.GetAt(i));
		}
	}

	if(bAdd) {
		m_FontList.GetFontList(FALSE);
		ChangeSelect();
	}
}

// 手動アップデート
void CLookFontView::OnMenuUpdateCheck() 
{
	StartAutoUpdate(FALSE);
}

void CLookFontView::OnUpdateMenuUpdateCheck(CCmdUI* pCmdUI) 
{
	pCmdUI->Enable(m_eAuStatus == AUSTATUS_IDLE);
	TRACE("m_eAuStatus %d\n", m_eAuStatus);
}

void CLookFontView::OnUpdateMenuTmpfontInstall(CCmdUI* pCmdUI) 
{
}

// 一時フォントアンインストール
void CLookFontView::OnMenuTmpfontUninstall() 
{
	RemoveTmpFont();
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuTmpfontUninstall(CCmdUI* pCmdUI) 
{
	pCmdUI->Enable(m_asTmpFontFiles.GetSize() > 0);
}


void CLookFontView::OnMenuSelLangEn() 
{
	m_Ini.SelLangEN = !m_Ini.SelLangEN;
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuSelLangEn(CCmdUI* pCmdUI) 
{
	pCmdUI->SetCheck(m_Ini.SelLangEN);
}

void CLookFontView::OnMenuSelLangJa() 
{
	m_Ini.SelLangJA = !m_Ini.SelLangJA;
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuSelLangJa(CCmdUI* pCmdUI) 
{
	pCmdUI->SetCheck(m_Ini.SelLangJA);
}

void CLookFontView::OnMenuSelLangAll() 
{
	m_Ini.SelLangALL = !m_Ini.SelLangALL;
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuSelLangAll(CCmdUI* pCmdUI) 
{
	pCmdUI->SetCheck(m_Ini.SelLangALL);
}

void CLookFontView::OnMenuSelFontTmp() 
{
	m_Ini.SelFontTMP = !m_Ini.SelFontTMP;
	if(!m_Ini.SelFontTMP && !m_Ini.SelFontORG)
		m_Ini.SelFontORG = TRUE;
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuSelFontTmp(CCmdUI* pCmdUI) 
{
	pCmdUI->SetCheck(m_Ini.SelFontTMP);	
}

void CLookFontView::OnMenuSelFontOrg() 
{
	m_Ini.SelFontORG = !m_Ini.SelFontORG;
	if(!m_Ini.SelFontTMP && !m_Ini.SelFontORG)
		m_Ini.SelFontTMP = TRUE;
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuSelFontOrg(CCmdUI* pCmdUI) 
{
	pCmdUI->SetCheck(m_Ini.SelFontORG);	
}

/////////////////////////////////////////////////////////////////////////////
// ツールバーメッセージハンドラ
/////////////////////////////////////////////////////////////////////////////

void CLookFontView::OnChangeEditDisptext() 
{
	if(!m_bInit)
		return;

	m_pWndTextCtrlBar->GetDlgItemText(IDC_EDIT_DISPTEXT, m_Ini.DispText);
	UpdateText();
}

void CLookFontView::OnEditchangeComboFontsize() 
{
	if(!m_bInit)
		return;

	CCharCode	cc;

	CString	str;
	m_pWndTextCtrlBar->GetDlgItemText(IDC_COMBO_FONTSIZE, str);
	m_Ini.DispFontSize = (int)(0.5 + atof(cc.TChar2SJis(str)) * 10);
	UpdateText();
}

void CLookFontView::OnSelchangeComboFontsize() 
{
	CComboBox* pCb = (CComboBox*)m_pWndTextCtrlBar->GetDlgItem(IDC_COMBO_FONTSIZE);
	CString		str;
	int			nIdx;
	CCharCode	cc;

	if((nIdx = pCb->GetCurSel()) == CB_ERR)
		return;

	pCb->GetLBText(nIdx, str);
	m_Ini.DispFontSize = (int)(0.5 + atof(cc.TChar2SJis(str)) * 10);
	UpdateText();
}

/////////////////////////////////////////////////////////////////////////////
// ユーザメッセージハンドラ
/////////////////////////////////////////////////////////////////////////////

// プログラム終了
LRESULT CLookFontView::OnFrameClose(WPARAM wParam, LPARAM lParam)
{
	if(m_asTmpFontFiles.GetSize()) {
		if(AfxMessageBox(_T("一時インストールしたフォントを解除しますか？\r\n「いいえ」を選ぶとコンピュータを再起動するまでそのフォントが使用可能です."), MB_YESNO) == IDYES)
			RemoveTmpFont();
	}
	WriteIni();

	return 0;
}

// AUIメッセージ受信
LRESULT CLookFontView::OnAuiEvent(WPARAM wParam, LPARAM lParam)
{
	// AUIメッセージ
	// wParam メッセージ種別(AUI_MSG)
	// lParam 固有パラメータ
	AUI_MSG	eAuiMsg = (AUI_MSG)wParam;
	UINT	uData   = lParam;

	// AUIメッセージ解説
	// AUI_WM_EVENTメッセージ，AUIコールバックも使用は同じである
	// このメッセージを受信した場合、原則として即時復帰する必要がある
	// ただし、ユーザへの問い合わせ，一時的なメッセージ表示の場合はこの限りでない
	// ユーザはいつでも AUI_Cancel を呼び出し、自動アップデートを中止させる事ができる

	switch(eAuiMsg) {
	case AUI_MSG_ABORT:				// 自動アップデート終了
	case AUI_MSG_ERROR:				// エラー
	case AUI_MSG_CONNECT_ERROR:		// コネクション失敗
		if(m_eAuStatus == AUSTATUS_MANUAL)
			AfxMessageBox(_T("アップデートのための接続に失敗しました."));
		if(eAuiMsg == AUI_MSG_ABORT)
			m_eAuStatus = AUSTATUS_IDLE;
		else
			m_eAuStatus = AUSTATUS_END;
		break;

	case AUI_MSG_START:				// 開始
		break;

	case AUI_MSG_UPDATE_NONE:		// アップデートなし
		if(m_eAuStatus == AUSTATUS_MANUAL)
			AfxMessageBox(_T("最新のアップデートはありません."));
		m_eAuStatus = AUSTATUS_END;
		break;

	case AUI_MSG_UPDATE_EXIST:		// アップデートあり
		// アップデート可否問い合わせ
		if(AfxMessageBox(_T("新しいアップデートがあります.\r\nアップデートしますか？"), MB_YESNO) != IDYES) {
			// アップデート非実行
			m_eAuStatus = AUSTATUS_END;
			AUI_Cancel();	// キャンセル
			break;
		}
		// アップデート実行
		AUI_OpenProgDialog(m_hWnd);	// AutoUpdate組み込みの「アップデート中ダイアログ」を表示
		AUI_ContinueUpdate(NULL);	// アップデート継続
		break;

	case AUI_MSG_DOWNLOAD_SIZE:		// ダウンロードサイズ
	case AUI_MSG_DOWNLOAD_PROG:		// ダウンロード進捗
	case AUI_MSG_DOWNLOAD_DONE:		// ダウンロード完了
		break;

	case AUI_MSG_DOWNLOAD_FATAL:	// ダウンロード失敗
		AfxMessageBox(_T("アップデートのダウンロードに失敗しました."));
		break;

	case AUI_MSG_CANCEL:			// キャンセル
		m_eAuStatus = AUSTATUS_CANCEL;
		break;

	case AUI_MSG_FINAL:				// アップデート準備完了
		// プログラム終了
		m_eAuStatus = AUSTATUS_END;
		PostQuitMessage(0);
		break;
	}

	return 0;
}

/////////////////////////////////////////////////////////////////////////////
// INIアクセス
/////////////////////////////////////////////////////////////////////////////

void CLookFontView::ReadIni()
{
	ReadWriteIni(TRUE);
}

void CLookFontView::WriteIni()
{
	ReadWriteIni(FALSE);
}

void CLookFontView::ReadWriteIni(BOOL bRead)
{
#define	RWINI_STR(w,s,v,d)	if(bRead) m_Ini.v = pApp->GetProfileString(s,#v,d); else if(w) pApp->WriteProfileString(s,#v,m_Ini.v)
#define	RWINI_INT(w,s,v,d)	if(bRead) m_Ini.v = pApp->GetProfileInt   (s,#v,d); else if(w) pApp->WriteProfileInt   (s,#v,m_Ini.v)
	CWinApp *pApp = AfxGetApp();

	// LOOKFONTINI 項目
	int		i	;
	CString	csFont	;
	CString	csRegNo	;
	RWINI_INT(TRUE,  LF_REG_WINDOW,   Width1,       -1);
	RWINI_INT(TRUE,  LF_REG_SETTINGS, SelLangEN,    1);
	RWINI_INT(TRUE,  LF_REG_SETTINGS, SelLangJA,    1);
	RWINI_INT(TRUE,  LF_REG_SETTINGS, SelLangALL,   0);
	RWINI_INT(TRUE,  LF_REG_SETTINGS, SelFontTMP,   1);
	RWINI_INT(TRUE,  LF_REG_SETTINGS, SelFontORG,   1);
	RWINI_STR(FALSE, LF_REG_SETTINGS, DispText,     _T(""));
	RWINI_INT(TRUE,  LF_REG_SETTINGS, DispFontSize, 240);
	RWINI_INT(TRUE,  LF_REG_SETTINGS, AutoUpdate,   1);
	RWINI_INT(TRUE,  LF_REG_SETTINGS, FavMax,   MAX_FAVARTE_NO);
	if (bRead == TRUE) {
		// Read
		m_Ini.Favorite.RemoveAll();
		for (i = 0 ; i < m_Ini.FavMax ; i++){
			csRegNo.Format(_T("%s%02d"), LF_REG_FAVORITE, i+1);
			csFont = pApp->GetProfileString(LF_REG_SETTINGS, csRegNo, _T(""));
			if (csFont != "") 	m_Ini.Favorite.AddTail(csFont);
		}
	}
	else {
		// Write
    	for (i = 0 ; i < m_Ini.FavMax ; i++){
			csRegNo.Format(_T("%s%02d"), LF_REG_FAVORITE, i+1);
			if (i <= m_Ini.Favorite.GetCount()-1) {
				csFont = m_Ini.Favorite.GetAt(m_Ini.Favorite.FindIndex(i));
			}
			else {
				csFont = "";
			}
			pApp->WriteProfileString(LF_REG_SETTINGS, csRegNo, csFont);
		}
	}
}

/////////////////////////////////////////////////////////////////////////////
// フォント関数
/////////////////////////////////////////////////////////////////////////////

// 一時フォント抹消
void CLookFontView::RemoveTmpFont()
{
	int		i;

	for(i = 0; i < m_asTmpFontFiles.GetSize(); i++)
		m_FontList.RemoveFont(m_asTmpFontFiles.GetAt(i));

	m_asTmpFontFiles.RemoveAll();

	m_FontList.GetFontList(TRUE);
}

/////////////////////////////////////////////////////////////////////////////
// 内部関数
/////////////////////////////////////////////////////////////////////////////

// リソース文字列取得
CString CLookFontView::LoadString(UINT nID)
{
	CString	str;
	str.LoadString(nID);
	return str;
}

// アップデート実行
//	bAuto		I	TRUE:自動 FALSE:手動
BOOL CLookFontView::StartAutoUpdate(BOOL bAuto)
{
	CWinApp *pApp = AfxGetApp();
	SYSTEMTIME	st;

	if (theApp.m_ActivatOK == FALSE) {
		if (!bAuto) {
			AfxMessageBox("ライセンス認証が終了してません。\n当機能はライセンス認証完了後に機能します。");
		}
		return FALSE ;
	}
	if(m_eAuStatus != AUSTATUS_IDLE)
		return FALSE;

	if(bAuto) {
		// 自動モード
		if(!m_Ini.AutoUpdate)
			return FALSE;

		// 自動モードの場合１日１回だけ実行
		GetLocalTime(&st);
		CString	sNowDate, sLastDate;
		sNowDate.Format(_T("%04d%02d%02d"), st.wYear, st.wMonth, st.wDay);
		sLastDate = pApp->GetProfileString(_T("AutoUpdate"), _T("LastConnectDate"), _T(""));
		if(sNowDate.CompareNoCase(sLastDate) == 0)
			return FALSE;
		pApp->WriteProfileString(_T("AutoUpdate"), _T("LastConnectDate"), sNowDate);

		m_eAuStatus = AUSTATUS_AUTO;
	}
	else {
		// 手動モード
		m_eAuStatus = AUSTATUS_MANUAL;
	}

	if(!AUI_Start(LoadString(IDS_AUI_URL), LoadString(IDS_AUI_VER), GetSafeHwnd(), NULL, NULL)) {
		if(!bAuto)
			AfxMessageBox("既に実行中です");
		m_eAuStatus = AUSTATUS_IDLE;
		return FALSE;
	}

	return TRUE;
}

// 拡張メタファイル作成
//	return	NULL:エラー
HENHMETAFILE CLookFontView::CreateEmf(LPCTSTR pText)
{
	CMetaFileDC		mf;
	HENHMETAFILE	hMeta;
	CFont			fontd, font;
	LOGFONT			lf;
	CSize			size;
	CClientDC		dc(GetDesktopWindow());
	CByteArray		abMeta;
	double			fRate;
	int				nAj;
	UINT			uSize;
	ENHMETAHEADER*	pEmrHdr;
	int				x;
	int				l;
	int				nStrType, nCurType;		// 文字タイプ 0:英数字 1:半角マルチバイト 2:全角マルチバイト
	int				nStrLen;
	LPCTSTR			nStrStart, pStrNext, pStrCur;

	// 参照文字サイズ
	m_FontList.GetLogFont(m_nDispCurPos, &lf);
	lf.lfHeight  = -m_Ini.DispFontSize;
	fontd.CreateFontIndirect(&lf);
	dc.SelectObject(fontd);
	size = dc.GetTextExtent(pText);

	// 拡張メタファイル作成
	fRate = (dc.GetDeviceCaps(VERTSIZE) * 72.0 * 10.0) / (dc.GetDeviceCaps(VERTRES) * 25.4);
	mf.CreateEnhanced(NULL, NULL, NULL, NULL);
	mf.SetMapMode(MM_ANISOTROPIC);
	mf.SetBkMode(TRANSPARENT);
	mf.SetWindowOrg(0, 0);
	mf.SetViewportOrg(0, 0);
	mf.SetWindowExt((int)(0.5 + size.cx * fRate), (int)(0.5 + size.cy * fRate));		// 根拠不明
	mf.SetViewportExt(size.cx, size.cy);	// 根拠不明
	mf.m_hAttribDC = mf.m_hDC;	// 原因不明回避 SelectObject で ASSERT(m_hDC == m_hAttribDC)
	font.CreateFontIndirect(&lf);
	mf.SelectObject(font);
	// Photoshop CS4用に追加
	mf.SetTextColor(GetSysColor(COLOR_WINDOWTEXT));
	mf.m_hAttribDC = NULL;	// 原因不明回避 TextOut で ASSERT(m_hDC != m_hAttribDC)

	// 文字描画
	x         = 0;
	pStrCur   = pText;
	nStrType  = -1;
	nStrLen   = 0;
	nStrStart = pStrCur;
	while(*pStrCur) {
		pStrNext = _tcsinc(pStrCur);
		l = pStrNext - pStrCur;
#ifndef _UNICODE
		if(l == 2) {
			nCurType = 2;
		}
		else {
			nCurType = 0;
			if(((UCHAR)(*pStrCur)) >= 0x80)
				nCurType = 1;
		}
#else
		ASSERT(0);
		// 未確認
		nCurType = 0;
		if((*pStrCur) >= 0x100) {
			nCurType = 2;
			if((*pStrCur) & 0xff00) == 0xff)	// 上位ワードが FF なら半角？(未確認)
				nCurType = 1;
		}
#endif
		if(nStrType != nCurType) {
			if(nStrLen > 0) {
				size = dc.GetTextExtent(nStrStart, nStrLen);
				mf.TextOut(x, 0, nStrStart, nStrLen);
				x += size.cx;
			}
			nStrType  = nCurType;
			nStrStart = pStrCur;
			nStrLen = l;
		}
		else {
			nStrLen += l;
		}
		pStrCur = pStrNext;
	}
	if(nStrLen > 0)
		mf.TextOut(x, 0, nStrStart, nStrLen);

	hMeta = mf.CloseEnhanced();

	// 拡張メタファイル細工
	uSize = GetEnhMetaFileBits(hMeta, 0, NULL);
	abMeta.SetSize(uSize);
	GetEnhMetaFileBits(hMeta, uSize, abMeta.GetData());
	DeleteEnhMetaFile(hMeta);
	pEmrHdr = (ENHMETAHEADER*)abMeta.GetData();
	// 枠に収まりきらないフォントがあるため縦の5%を縦横に加算 根拠不明
	nAj = (int)ceil(pEmrHdr->rclFrame.bottom / 20.0);
	pEmrHdr->rclFrame.right  += nAj;
	pEmrHdr->rclFrame.bottom += nAj;
	hMeta = SetEnhMetaFileBits(uSize, abMeta.GetData());

	return hMeta;
}

/////////////////////////////////////////////////////////////////////////////
// 表示関数
/////////////////////////////////////////////////////////////////////////////

// 画面初期化
BOOL CLookFontView::Init(CDC *pDC)
{
	int					i;
	CString				str;
	CSize				size;
	NONCLIENTMETRICS	nbm;
	CRect				rectw;

	// INI
//	ReadIni();

	// 自動アップデート
	StartAutoUpdate(TRUE);

	// サイズ調整
	GetClientRect(rectw);

	if(m_Ini.DispFontSize < 60)
		m_Ini.DispFontSize = 60;
	else if(m_Ini.DispFontSize > 760)
		m_Ini.DispFontSize = 760;

	if(m_Ini.Width1 > rectw.Width() - 2)
		m_Ini.Width1 = rectw.Width() - 2;

	// サンプル文字
	if(m_Ini.DispText.IsEmpty()) {
		if(!m_Ini.SelLangJA)
			m_Ini.DispText = LoadString(IDS_DISPTEXT_EN);
		else
			m_Ini.DispText = LoadString(IDS_DISPTEXT_JA);
	}

	// 変数初期化
	m_nDispTopPos    = 0;
	m_nDispCurPos    = 0;
	m_nCtrlWheel     = 0;
	m_bDispMoveWidth = FALSE;

	// システムメニューフォントサイズ
	nbm.cbSize = sizeof(nbm);
	SystemParametersInfo(SPI_GETNONCLIENTMETRICS, nbm.cbSize, &nbm, 0);
	m_nSysFontSize = (int)(0.5 + fabs(nbm.lfMenuFont.lfHeight * 720.0 / pDC->GetDeviceCaps(LOGPIXELSY)));
	m_sSysFontFace = nbm.lfMenuFont.lfFaceName;

	// コントロール取得
	m_pWndTextCtrlBar = ((CLookFontApp*)AfxGetApp())->m_pWndTextCtrlBar;

	// フォント一覧取得
	m_FontList.GetFontList(TRUE);

	// コントロール設定
	// マルチランゲージ対応
	// 他コードがマルチランゲージ対応になるまで封印
	////LOGFONT	lfTB;
	////int		nFontSizeTB;
	////m_pWndTextCtrlBar->GetDlgItem(IDC_EDIT_DISPTEXT)->GetFont()->GetLogFont(&lfTB);
	////nFontSizeTB = (int)(0.5 + fabs(lfTB.lfHeight * 720.0 / pDC->GetDeviceCaps(LOGPIXELSY)));
	////m_fontTextEdit.CreatePointFont(nFontSizeTB, LF_UI_FONT);
	////m_pWndTextCtrlBar->GetDlgItem(IDC_EDIT_DISPTEXT)->SetFont(&m_fontTextEdit);

	m_pWndTextCtrlBar->SetDlgItemText(IDC_EDIT_DISPTEXT, m_Ini.DispText);
	if(m_Ini.DispFontSize % 10)
		str.Format(_T("%d.%d"), m_Ini.DispFontSize / 10, m_Ini.DispFontSize % 10);
	else
		str.Format(_T("%d"), m_Ini.DispFontSize / 10);
	m_pWndTextCtrlBar->SetDlgItemText(IDC_COMBO_FONTSIZE, str);

	// フォント名サイズ
	// 書体文字列フォント
	m_fontFace.CreatePointFont(m_nSysFontSize, m_sSysFontFace);
	pDC->SelectObject(m_fontFace);

	// フォント名描画サイズ
	m_sizeDispFace.cx = m_sizeDispFace.cy = 0;
	for(i = 0; i < m_FontList.GetSize(); i++) {
		size = pDC->GetTextExtent(m_FontList.GetFontFaceAt(i));
		if(m_sizeDispFace.cx < size.cx)
			m_sizeDispFace.cx = size.cx;
		if(m_sizeDispFace.cy < size.cy)
			m_sizeDispFace.cy = size.cy;
	}

	// 1列目幅
	if(m_Ini.Width1 < 0) {
		if(m_sizeDispFace.cx > 0)
			m_Ini.Width1 = m_sizeDispFace.cx;
		else
			m_Ini.Width1 = rectw.Width() / 2;
	}

	m_penSepRow.CreatePen(PS_SOLID, 1, LF_CLR_SEPROW);

	ChangeSelect(pDC);

	return TRUE;
}

// 表示内容更新
void CLookFontView::UpdateText()
{
	CClientDC	dc(this);
	UpdateText(&dc);
}

void CLookFontView::UpdateText(CDC *pDC)
{
	int			idx, y, cy;
	CString		sCurText;
	CString		sAttrText;
	BOOL		bAttr;
	BOOL		bFon = FALSE;
	CRect		rectw;
	CSize		sizet;

	CPen		penSepLine(PS_SOLID, 1, GetSysColor(COLOR_WINDOWTEXT));
	COLORREF 	rgbKeep;

	// 画面サイズ
	GetClientRect(rectw);

	// Y座標列クリア
	m_anDispY.RemoveAll();
	m_anDispCY.RemoveAll();

	// カレント位置が上方へずれた場合
	if(m_nDispCurPos < m_nDispTopPos)
		m_nDispCurPos = m_nDispTopPos;

	// 描画
	pDC->SetTextColor(GetSysColor(COLOR_WINDOWTEXT));
	y = 0;
	m_nDispLine = 0;
	for(idx = m_nDispTopPos; idx < m_nDispCount; idx++) {
		// 有効文字に変換
		sCurText = m_FontList.AvailableChar(idx, m_Ini.DispText);

		// 現在のフォントでテキストの表示サイズを取得
		LOGFONT		lf;
		CFont		fontt;
		m_FontList.GetLogFont(idx, &lf);
		lf.lfHeight = m_Ini.DispFontSize;
//		if (lf.lfWidth == 0) {
//			lf.lfWidth = 10 ;
//		}
		if (fontt.CreatePointFontIndirect(&lf) != 0) {
			pDC->SelectObject(fontt);
			sizet = pDC->GetTextExtent(sCurText);
			bFon = TRUE;
		}
		else {
			sizet.cy = 1 ;
			sizet.cx = 1 ;
			bFon = FALSE;
		}

		cy = __max(sizet.cy, (m_sizeDispFace.cy*2.2));

		// 位置情報格納
		m_anDispY.Add(y);
		m_anDispCY.Add(cy);

		// 背景クリア
		pDC->FillSolidRect(0, y, rectw.right, y + cy + LF_SPACEFRAME * 2 + 1, GetSysColor(COLOR_WINDOW));

		// テキスト描画
		if (bFon == TRUE) {
			pDC->TextOut(LF_SPACEFRAME + m_Ini.Width1 + LF_SPACEROW + LF_SPACEFRAME, y + LF_SPACEFRAME, sCurText);
		}

		// フォント名描画
		CRect	rect(LF_SPACEFRAME, y + LF_SPACEFRAME, m_Ini.Width1, y + LF_SPACEFRAME + cy);
		pDC->SelectObject(m_fontFace);
		pDC->DrawText(m_FontList.GetFontFaceAt(idx), rect, DT_VCENTER | DT_NOPREFIX | DT_WORDBREAK);
//		pDC->DrawText(m_FontList.    .GetFontFaceAt(idx), rect, DT_VCENTER | DT_NOPREFIX | DT_WORDBREAK);
		sAttrText = "";
		bAttr = FALSE;
		if (m_FontList.IsFontFavoriteAt(idx)) {
			sAttrText += _T(" ★　") ;
			bAttr = TRUE;
		}
		else {
			sAttrText += _T(" 　　") ;
		}
		if (m_FontList.IsFontCandidateAt(idx)) {
			sAttrText += _T("！") ;
			bAttr = TRUE;
		}
		if (bAttr) {
			CRect	rect2(LF_SPACEFRAME, y + LF_SPACEFRAME+(m_sizeDispFace.cy*1.2), m_Ini.Width1, y + LF_SPACEFRAME + cy);
			rgbKeep = pDC->SetTextColor(RGB(0,255,0));
			pDC->DrawText(sAttrText, rect2, DT_VCENTER | DT_NOPREFIX | DT_WORDBREAK);
			pDC->SetTextColor(rgbKeep);
		}

		// 列線
		pDC->SelectObject(m_penSepRow);
		pDC->MoveTo(m_Ini.Width1 + 1, y);
		pDC->LineTo(m_Ini.Width1 + 1, y + cy + LF_SPACEFRAME * 2);

		// Y座標調整
		y += cy + LF_SPACEFRAME * 2;
		m_nDispBottom = y;
		// 画面下端
		if(y > rectw.bottom)
			break;

		// 表示行数加算
		m_nDispLine++;

		// フォントBOX設定
		SetFontBox(pDC, idx);

		// カーソル表示
		if(idx == m_nDispCurPos)
			SetCur(pDC, idx, TRUE);

		// グリッド線
		pDC->SelectObject(penSepLine);
		pDC->MoveTo(0, y);
		pDC->LineTo(rectw.right, y);
		y += LF_SPACELINE;
	}

	// カレント位置が下方へずれた場合
	if(m_nDispCurPos >= m_nDispTopPos + m_nDispLine) {
		m_nDispCurPos = m_nDispTopPos + m_nDispLine - 1;
		SetCur(pDC, m_nDispCurPos, TRUE);
	}

	// 残領域クリア
	pDC->FillSolidRect(0, y, rectw.right, rectw.bottom, GetSysColor(COLOR_WINDOW));

	if(m_nDispLine <= 0)
		m_nDispLine = 1;

	// スクロールバー設定
	SCROLLINFO	si;
	si.cbSize    = sizeof(si);
	si.fMask     = SIF_ALL;
	si.nMin      = 0;
	si.nMax      = m_nDispCount - 1;
	si.nPage     = m_nDispLine;
	si.nPos      = m_nDispTopPos;
	si.nTrackPos = 0;
	SetScrollInfo(SB_VERT, &si);

	pDC->SelectObject(m_penSepRow);	// Win9xリソースリーク対策の無駄コード
}

// カレントフォント設定
void CLookFontView::SetCur(CDC *pDC, int nPos, BOOL bShow)
{
	COLORREF	clr = bShow ? LF_CLR_FRAME : GetSysColor(COLOR_WINDOW);
	CRect		rectw;
	int			y, cy;

	if(m_nDispCount == 0)
		return;

	// 位置取得
	GetClientRect(rectw);
	y  = m_anDispY .GetAt(nPos - m_nDispTopPos);
	cy = m_anDispCY.GetAt(nPos - m_nDispTopPos);

	// 枠描画
	pDC->FillSolidRect(m_Ini.Width1 + LF_SPACEROW, y, LF_SPACEFRAME, cy + LF_SPACEFRAME * 2, clr);
	pDC->FillSolidRect(rectw.right - LF_SPACEFRAME, y, LF_SPACEFRAME,  cy + LF_SPACEFRAME * 2, clr);
	pDC->FillSolidRect(m_Ini.Width1 + LF_SPACEROW, y, rectw.right - m_Ini.Width1, LF_SPACEFRAME, clr);
	pDC->FillSolidRect(m_Ini.Width1 + LF_SPACEROW, y + cy + LF_SPACEFRAME, rectw.right - m_Ini.Width1, LF_SPACEFRAME, clr);
}

// フォントBOX設定
void CLookFontView::SetFontBox(CDC *pDC, int nPos)
{
	COLORREF	clr;
	CRect		rectw;
	int			y, cy;

	if(!m_FontList.IsFontTmpAt(nPos))
		return;

	clr = LF_CLR_FONTTMP;

	// 位置取得
	GetClientRect(rectw);
	y  = m_anDispY .GetAt(nPos - m_nDispTopPos);
	cy = m_anDispCY.GetAt(nPos - m_nDispTopPos);

	// 枠描画
	pDC->FillSolidRect(0, y, LF_SPACEFRAME, cy + LF_SPACEFRAME * 2, clr);
	pDC->FillSolidRect(m_Ini.Width1 - LF_SPACEFRAME, y, LF_SPACEFRAME,  cy + LF_SPACEFRAME * 2, clr);
	pDC->FillSolidRect(0, y, m_Ini.Width1, LF_SPACEFRAME, clr);
	pDC->FillSolidRect(0, y + cy + LF_SPACEFRAME, m_Ini.Width1, LF_SPACEFRAME, clr);
}

// 列幅変更
void CLookFontView::MoveWidth(CPoint point)
{
	CRect		rectw;
	CClientDC	dc(this);

	GetClientRect(rectw);

	m_Ini.Width1 = point.x;
	if(m_Ini.Width1 < 0)
		m_Ini.Width1 = 0;
	else if(m_Ini.Width1 >= rectw.right)
		m_Ini.Width1 = rectw.right - 1;

	UpdateText(&dc);
}

// フォント種別変更
void CLookFontView::ChangeSelect()
{
	CClientDC	dc(this);
	ChangeSelect(&dc);
}

// フォント種別変更
void CLookFontView::ChangeSelect(CDC *pDC)
{
	CDWordArray adwCharSet;

	m_nDispTopPos    = 0;
	m_nDispCurPos    = 0;
	m_nCtrlWheel     = 0;

	// 言語選択
	if(m_Ini.SelLangEN)
		adwCharSet.Add(ANSI_CHARSET);
	if(m_Ini.SelLangJA)
		adwCharSet.Add(SHIFTJIS_CHARSET);
	if(m_Ini.SelLangALL)
		adwCharSet.Add(FONTLIST_LANG_ALL);

	// インストールフォント選択
	if(m_Ini.SelFontORG)
		adwCharSet.Add(FONTLIST_FONT_ORG);
	if(m_Ini.SelFontTMP)
		adwCharSet.Add(FONTLIST_FONT_TMP);

	if(adwCharSet.GetSize() == 0)
		adwCharSet.Add(-1);
	m_FontList.SelectFontList(adwCharSet);
	m_nDispCount = m_FontList.GetSize();

	if(m_Ini.DispText == LoadString(IDS_DISPTEXT_EN) || m_Ini.DispText == LoadString(IDS_DISPTEXT_JA)) {
		if(!m_Ini.SelLangJA)
			m_Ini.DispText = LoadString(IDS_DISPTEXT_EN);
		else
			m_Ini.DispText = LoadString(IDS_DISPTEXT_JA);
		m_pWndTextCtrlBar->SetDlgItemText(IDC_EDIT_DISPTEXT, m_Ini.DispText);
	}

	UpdateText(pDC);
}

void CLookFontView::OnMenuTmpfontFind() 
{
	CStringArray	asFiles;
	CString			sPath;
	int				i, j;
	CFindFontDlg	dlg	;
	BOOL			bAdd = FALSE;

	if(dlg.DoModal() != IDOK)
		return;
	CWaitCursor	cuWait	;
	for(i = 0; i < dlg.m_FontPathCnt ; i++) {
		for(j = 0; j < m_asTmpFontFiles.GetSize(); j++) {
			if(dlg.m_FontPathList.GetAt(dlg.m_FontPathList.FindIndex (i)).CompareNoCase(m_asTmpFontFiles.GetAt(j)) == 0)
				break;
		}
		if(j < m_asTmpFontFiles.GetSize())
			continue;
		if(m_FontList.AddFont(dlg.m_FontPathList.GetAt(dlg.m_FontPathList.FindIndex (i)), dlg.m_FontPathList.GetAt(dlg.m_FontPathList.FindIndex (i))) > 0) {
			bAdd = TRUE;
			m_asTmpFontFiles.Add(dlg.m_FontPathList.GetAt(dlg.m_FontPathList.FindIndex (i)));
		}
	}

	if(bAdd) {
		m_FontList.GetFontList(FALSE);
		ChangeSelect();
	}
}

void CLookFontView::OnUpdateMenuTmpfontFind(CCmdUI* pCmdUI) 
{
	// TODO: この位置に command update UI ハンドラ用のコードを追加してください
}

void CLookFontView::OnMenuPropaty() 
{
	CDialogFontInfo	dlgFont	;
	dlgFont.m_FontFace = m_FontList.GetFontFaceAt(m_nDispCurPos) ;
	dlgFont.m_FontFile = m_FontList.GetTmpFontName(dlgFont.m_FontFace) ;
	dlgFont.m_FontMode = m_FontList.IsFontTmpAt(m_nDispCurPos) ;
	dlgFont.DoModal () ;
}

void CLookFontView::OnUpdateMenuPropaty(CCmdUI* pCmdUI) 
{
//	pCmdUI->Enable (m_FontList.IsFontTmpAt (m_nDispCurPos)) ;
//	(GetDlgItem (ID_MENU_PROPATY))->EnableWindow (m_FontList.IsFontTmpAt (m_nDispCurPos)) ;
}

BOOL CLookFontView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default CEditView preparation
	m_bReview = TRUE ;
//	if(pInfo->m_bPreview){	//印刷プレビューの時
//		pInfo->m_nCurPage=m_nNowPage+1;	//現在表示中のページをプレビュー開始時のページにする
//	}else{	//印刷の時
//	}
	BOOL	bRet	;
	int		iMaxPage;
//	iMaxPage = GetPageCount (pInfo) ;
	if ((bRet = DoPreparePrinting(pInfo)) == TRUE) {
		iMaxPage = GetPageCount (pInfo) ;
		pInfo->SetMaxPage(iMaxPage);		//最大ﾍﾟｰｼﾞ数を設定します
		m_bReview = TRUE ;
		m_PrintPageNo = 0 ;
	}
	return bRet ;
}

void CLookFontView::OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo)
{
	// Default CEditView begin printing.
//	m_PrintPageNo++ ;
}

void CLookFontView::OnEndPrinting(CDC* pDC, CPrintInfo* pInfo)
{
	// Default CEditView end printing
	m_bReview = FALSE ;	
}

void CLookFontView::OnFilePrintPreview()
{
	CView::OnFilePrintPreview() ;

//	CPrintPreviewState* pState = new CPrintPreviewState;

	// DoPrintPreview's return value does not necessarily indicate that
	// Print preview succeeded or failed, but rather what actions are necessary
	// at this point.  If DoPrintPreview returns TRUE, it means that
	// OnEndPrintPreview will be (or has already been) called and the
	// pState structure will be/has been deleted.
	// If DoPrintPreview returns FALSE, it means that OnEndPrintPreview
	// WILL NOT be called and that cleanup, including deleting pState
	// must be done here.

//	if ( !DoPrintPreview( XTP_IDD_PREVIEW_TOOLBAR, this,
//		RUNTIME_CLASS( CXTPPreviewView ), pState ))
//	{
//		// In derived classes, reverse special window handling here for
//		// Preview failure case
//		TRACE0( "Error: DoPrintPreview failed.\n" );
//		AfxMessageBox( AFX_IDP_COMMAND_FAILURE );
//		delete pState;      // preview failed to initialize, delete State now
//
//	}
}

BOOL CLookFontView::DrawPrinter(CDC *pDC, int iPage)
{
	CString				str;
	CRect				rectw;
//	CRect				prnArea	;

	int					iYFont = 0	,
						cy			,
						y			;
	int					idx			;
	CSize				size		,
						sizet		;
	CString				sCurText	;
	CString				csFooter	;
	CFont				*oldFont	;
	CPen				*oldPen		;
	COLORREF			oldTxtCol	;

//	prnArea.left = GetDeviceCaps(pDC->m_hDC, PHYSICALOFFSETX);
//	prnArea.top = GetDeviceCaps(pDC->m_hDC, PHYSICALOFFSETY);
//	prnArea.right = prnArea.left + tPixX ;
//	prnArea.bottom = prnArea.top + tPixY ;
    CDC dcPrinter;
//	if (pDC->m_hDC == pDC->m_hAttribDC) {
		dcPrinter.Attach(pDC->m_hAttribDC);
//	}
//	else {
//		dcPrinter.Attach(pDC->m_hDC);
//	}
	int					tXmm = GetDeviceCaps (dcPrinter.m_hDC, HORZSIZE) ;
	int					tYmm = GetDeviceCaps (dcPrinter.m_hDC, VERTSIZE) ;
	int					tpXmm = tXmm + 15 ;
	int					tpYmm = tYmm + 15 ;
	int					tlogPixY = GetDeviceCaps (dcPrinter.m_hDC, LOGPIXELSY) ;	// 高さ
	int					tPixX = GetDeviceCaps (dcPrinter.m_hDC, HORZRES) ;
	int					tPixY = GetDeviceCaps (dcPrinter.m_hDC, VERTRES) ;
	// フォント名サイズ

	// 書体文字列フォント
	CFont	sysfontFace	;
	NONCLIENTMETRICS	nbm;
	nbm.cbSize = sizeof(nbm);
	SystemParametersInfo(SPI_GETNONCLIENTMETRICS, nbm.cbSize, &nbm, 0);
//	m_nPrtSysFontSize = (int)(0.5 + fabs(nbm.lfMenuFont.lfHeight * 720.0 / cPreV.GetDeviceCaps(LOGPIXELSY)));
	m_nPrtSysFontSize = 100 ;
	m_sPrtSysFontFace = nbm.lfMenuFont.lfFaceName;
	sysfontFace.CreatePointFont(m_nPrtSysFontSize, m_sPrtSysFontFace, &dcPrinter);
	oldFont = pDC->SelectObject(&sysfontFace);

	csFooter.Format ("Page %ld／%ld", iPage, m_PrintPageMax) ;
	size = pDC->GetTextExtent(csFooter);
//	prnArea.bottom -= (size.cy + (LF_SPACEFRAME * 2)) ;

//	CBitmap	cbLookFont ;
//	CPoint	ptBT ((int)((m_prnArea.Width () / 3.0) * 2.0) + m_prnArea.left, m_prnArea.top) 	;
//	CSize	sizeBT (160, 31);
//	pDC->LPtoHIMETRIC (&sizeBT) ;
//	cbLookFont.LoadBitmap (IDB_BITMAP_LOOKFONT) ;
//	pDC->DrawState (ptBT, sizeBT, &cbLookFont, DST_BITMAP | DSS_NORMAL, NULL) ;
//	pDC->DrawIcon (m_prnArea.right - 30, m_prnArea.top, m_LookFontIcon) ;

//	m_penSepRow.CreatePen(PS_SOLID, 1, LF_CLR_SEPROW);
	CPen		penSepLine(PS_SOLID, 1, GetSysColor(COLOR_WINDOWTEXT));

	// 描画
	oldTxtCol = pDC->SetTextColor(GetSysColor(COLOR_WINDOWTEXT));
	y = m_prnArea.top + ((size.cy + LF_PRN_SPACEFRAME + LF_PRN_SPACEFRAME) * 2);
//	m_nDispLine = 0;

	CRect	rect2(m_prnArea.left, m_prnArea.top + LF_PRN_SPACEFRAME, m_prnArea.right, m_prnArea.top + size.cy + LF_PRN_SPACEFRAME + LF_PRN_SPACEFRAME);
	pDC->DrawText("フォントまる見え！！　ＬｏｏｋＦｏｎｔ Ver.2", rect2, DT_VCENTER | DT_CENTER | DT_NOPREFIX);

	int		iStart	,
			iEnd	;

	oldPen = pDC->SelectObject(&penSepLine);

	pDC->MoveTo(m_prnArea.left, y - LF_PRN_SPACELINE);
	pDC->LineTo(m_prnArea.right, y - LF_PRN_SPACELINE);

	iStart = m_PrtPageIdx.GetAt (iPage - 1) ;
	if (m_PrintPageMax > iPage) {
		iEnd = m_PrtPageIdx.GetAt (iPage) ;
	}
	else {
		iEnd = m_nDispCount ;
	}
	for(idx = iStart; idx < iEnd; idx++) {
		// 有効文字に変換
		sCurText = m_FontList.AvailableChar(idx, m_Ini.DispText);

		// 現在のフォントでテキストの表示サイズを取得
		LOGFONT		lf;
		CFont		fontt;
		m_FontList.GetLogFont(idx, &lf);
		lf.lfHeight = m_Ini.DispFontSize;
		fontt.CreatePointFontIndirect(&lf, &dcPrinter);
		pDC->SelectObject(&fontt);
		sizet = pDC->GetTextExtent(sCurText);
		cy = __max(sizet.cy, m_sizePrintFace.cy);

//		// 背景クリア
//		pDC->FillSolidRect(0, y, rectw.right, y + cy + LF_SPACEFRAME * 2 + 1, GetSysColor(COLOR_WINDOW));

		// テキスト描画
		pDC->TextOut(m_prnArea.left + LF_PRN_SPACEFRAME + m_sizePrintFace.cx + LF_PRN_SPACEROW + LF_PRN_SPACEFRAME, y + LF_PRN_SPACEFRAME, sCurText);

		// フォント名描画
		CRect	rect(LF_PRN_SPACEFRAME + m_prnArea.left, y + LF_PRN_SPACEFRAME, m_sizePrintFace.cx, y + LF_PRN_SPACEFRAME + cy);
		pDC->SelectObject(&sysfontFace);
		fontt.DeleteObject ();
		pDC->DrawText(m_FontList.GetFontFaceAt(idx), rect, DT_VCENTER | DT_NOPREFIX | DT_WORDBREAK);
//		pDC->DrawText(m_FontList.    .GetFontFaceAt(idx), rect, DT_VCENTER | DT_NOPREFIX | DT_WORDBREAK);

		// 列線
//		pDC->SelectObject(m_penSepRow);
//		pDC->MoveTo(m_sizePrintFace.x + 1, y);
//		pDC->LineTo(m_sizePrintFace.x + 1, y + cy + LF_SPACEFRAME * 2);

		// Y座標調整
		y += cy + LF_PRN_SPACEFRAME * 2;

//		// フォントBOX設定
//		SetFontBox(pDC, idx);

//		// カーソル表示
//		if(idx == m_nDispCurPos)
//			SetCur(pDC, idx, TRUE);

		// グリッド線
		pDC->MoveTo(m_prnArea.left, y);
		pDC->LineTo(m_prnArea.right, y);
		y += LF_PRN_SPACELINE;
	}

	size = pDC->GetTextExtent(csFooter);
	CRect	rect(m_prnArea.left, m_prnArea.bottom - (int)(size.cy * 2.5) - LF_PRN_SPACEFRAME, m_prnArea.right, m_prnArea.bottom + LF_PRN_SPACEFRAME - size.cy);
	pDC->DrawText(csFooter, rect, DT_VCENTER | DT_CENTER | DT_NOPREFIX);

	dcPrinter.Detach () ;
	pDC->SelectObject (oldPen) ;
	penSepLine.DeleteObject () ;
	pDC->SelectObject (oldFont) ;
	sysfontFace.DeleteObject () ;
	oldTxtCol = pDC->SetTextColor(oldTxtCol);
//	pDC->SelectObject(&m_penSepRow);	// Win9xリソースリーク対策の無駄コード

	return TRUE;
}

int CLookFontView::GetPageCount(CPrintInfo* pInfo)
{
	int	iMaxPage = 0	;
	int	i	;
	int	tXmm = GetDeviceCaps (pInfo->m_pPD->m_pd.hDC, HORZSIZE) ;
	int	tYmm = GetDeviceCaps (pInfo->m_pPD->m_pd.hDC, VERTSIZE) ;
	int	tpXmm = tXmm + 15 ;
	int	tpYmm = tYmm + 15 ;
	int	tlogPixY = GetDeviceCaps (pInfo->m_pPD->m_pd.hDC, LOGPIXELSY) ;	// 高さ
	int	tPixX = GetDeviceCaps (pInfo->m_pPD->m_pd.hDC, HORZRES) ;
	int	tPixY = GetDeviceCaps (pInfo->m_pPD->m_pd.hDC, VERTRES) ;
//	CRect	prnArea	;

	int	iYFont = 0	,
		cy			,
		y			;
	int	idx			;
	CSize	size	,
			sizet	;
	CRect	rectw	;
	CString	sCurText;
	CFont	*oldFont;
	int		iHMargn	;
//	keep_sizeDispFace = m_sizeDispFace ;


	m_prnArea.left = GetDeviceCaps(pInfo->m_pPD->m_pd.hDC, PHYSICALOFFSETX);
	m_prnArea.top = GetDeviceCaps(pInfo->m_pPD->m_pd.hDC, PHYSICALOFFSETY);
	m_prnArea.right = m_prnArea.left + tPixX ;
	m_prnArea.bottom = m_prnArea.top + tPixY ;

	// サイズ調整
//	pDC->GetClientRect(rectw);

	// 変数初期化
//	m_nDispTopPos    = 0;
//	m_nDispCurPos    = 0;
//	m_bDispMoveWidth = FALSE;

	// フォント名サイズ
	// 書体文字列フォント
	CDC	cPreV ;
	cPreV.Attach (pInfo->m_pPD->m_pd.hDC) ;
//	m_fontFace.CreatePointFont(m_nSysFontSize, m_sSysFontFace);

	CFont	sysfontFace	;
	NONCLIENTMETRICS	nbm;
	nbm.cbSize = sizeof(nbm);
	SystemParametersInfo(SPI_GETNONCLIENTMETRICS, nbm.cbSize, &nbm, 0);
//	m_nPrtSysFontSize = (int)(0.5 + fabs(nbm.lfMenuFont.lfHeight * 720.0 / cPreV.GetDeviceCaps(LOGPIXELSY)));
//	m_nPrtSysFontSize = (int)(0.5 + fabs(nbm.lfMenuFont.lfHeight * 720.0 / cPreV.GetDeviceCaps(LOGPIXELSY)));
	m_nPrtSysFontSize = 100 ;
	m_sPrtSysFontFace = nbm.lfMenuFont.lfFaceName;
	sysfontFace.CreatePointFont(m_nPrtSysFontSize, m_sPrtSysFontFace, &cPreV);
	oldFont = cPreV.SelectObject(&sysfontFace);

	CString	csFooter	;
	csFooter.Format ("Page %ld／%ld", m_PrintPageMax, m_PrintPageMax) ;
	size = cPreV.GetTextExtent(csFooter);
	m_prnArea.bottom -= (size.cy + (LF_PRN_SPACEFRAME * 2)) ;
	iHMargn = (int)((size.cy + (LF_PRN_SPACEFRAME * 2)) * 5) ;

	y = 0 ;
	// フォント名描画サイズ
	m_sizePrintFace.cx = m_sizePrintFace.cy = 0;
	for(i = 0; i < m_FontList.GetSize(); i++) {
		size = cPreV.GetTextExtent(m_FontList.GetFontFaceAt(i));
		if(m_sizePrintFace.cx < size.cx + 40)
			m_sizePrintFace.cx = size.cx + 40;
		if(m_sizePrintFace.cy < size.cy)
			m_sizePrintFace.cy = size.cy;
	}
//	m_penSepRow.CreatePen(PS_SOLID, 1, LF_CLR_SEPROW);

//	ChangeSelect(&cPreV);
	m_PrtPageIdx.RemoveAll () ;
	m_PrtPageIdx.Add (0) ;
	for(idx = 0 ; idx < m_nDispCount; idx++) {
		// 有効文字に変換
		sCurText = m_FontList.AvailableChar(idx, m_Ini.DispText);

		// 現在のフォントでテキストの表示サイズを取得
		LOGFONT		lf;
		CFont		fontt;
		m_FontList.GetLogFont(idx, &lf);
		lf.lfHeight = m_Ini.DispFontSize;
		fontt.CreatePointFontIndirect(&lf, &cPreV);
		cPreV.SelectObject(&fontt);
		sizet = cPreV.GetTextExtent(sCurText);
		cy = __max(sizet.cy, m_sizePrintFace.cy);
		fontt.DeleteObject ();

		// Y座標調整
		y += cy + LF_PRN_SPACEFRAME * 2;
		if (m_prnArea.Height() - iHMargn < y) {
			m_PrtPageIdx.Add (idx - 1) ;
			iMaxPage += 1 ;
			y = 0 ;
			idx-- ;
			continue ;
		}
		else {
			if (iMaxPage == 0) {
				iMaxPage++ ;
			}
		}

		y += LF_PRN_SPACELINE;
	}

//	// カレント位置が下方へずれた場合
//	if(m_nDispCurPos >= m_nDispTopPos + m_nDispLine) {
//		m_nDispCurPos = m_nDispTopPos + m_nDispLine - 1;
//		SetCur(pDC, m_nDispCurPos, TRUE);
//	}
	cPreV.SelectObject (oldFont) ;
	sysfontFace.DeleteObject () ;
	cPreV.Detach () ;
	return iMaxPage	;
}

void CLookFontView::OnPrepareDC(CDC* pDC, CPrintInfo* pInfo) 
{
	// TODO: この位置に固有の処理を追加するか、または基本クラスを呼び出してください
//	m_PrintPageNo++ ;
	CView::OnPrepareDC(pDC, pInfo);
	if (pInfo) {
		m_PrintPageNo = pInfo->m_nCurPage ;
		m_PrintPageMax = pInfo->GetMaxPage () ;
	}
}

void CLookFontView::OnMenuAddFavorite() 
{
//	RemoveTmpFont();
	if (m_Ini.Favorite.GetCount() >= m_Ini.FavMax) {
	}
	if (m_FontList.SetFontFavoriteAt(m_nDispCurPos, TRUE) == FALSE) {
		MessageBox(_T("お気に入りに追加出来る上限以上は登録出来ません。"), _T("お気に入り追加エラー"), MB_ICONEXCLAMATION);
	}
	else {
		if (m_Ini.Favorite.GetCount() >= m_Ini.FavMax) {
			MessageBox(_T("お気に入りに追加出来る上限以上は登録出来ません。"), _T("お気に入り追加エラー"), MB_ICONEXCLAMATION);
		}
		else {
			Invalidate();
			WriteIni();
		}
	}
}

void CLookFontView::OnUpdateMenuAddFavorite(CCmdUI* pCmdUI) 
{
	if(m_nDispCurPos < 0)
		return;
	if (m_Ini.Favorite.GetCount() >= m_Ini.FavMax) {
		pCmdUI->Enable(FALSE);
	}
	else {
		pCmdUI->Enable(m_FontList.IsFontFavoriteAt(m_nDispCurPos) == FALSE);
	}
}

void CLookFontView::OnMenuAddCandidate() 
{
	m_FontList.SetFontCandidateAt(m_nDispCurPos, TRUE);
	Invalidate();
}

void CLookFontView::OnUpdateMenuAddCandidate(CCmdUI* pCmdUI) 
{
	if(m_nDispCurPos < 0)
		return;
	pCmdUI->Enable(m_FontList.IsFontCandidateAt(m_nDispCurPos) == FALSE);
}

void CLookFontView::OnMenuDelFavorite() 
{
	m_FontList.SetFontFavoriteAt(m_nDispCurPos, FALSE);
	Invalidate();
	WriteIni();
}

void CLookFontView::OnUpdateMenuDelFavorite(CCmdUI* pCmdUI) 
{
	if(m_nDispCurPos < 0)
		return;
	pCmdUI->Enable(m_FontList.IsFontFavoriteAt(m_nDispCurPos) == TRUE);
}

void CLookFontView::OnMenuDelCandidate() 
{
	m_FontList.SetFontCandidateAt(m_nDispCurPos, FALSE);
	Invalidate();
}

void CLookFontView::OnUpdateMenuDelCandidate(CCmdUI* pCmdUI) 
{
	if(m_nDispCurPos < 0)
		return;
	pCmdUI->Enable(m_FontList.IsFontCandidateAt(m_nDispCurPos) == TRUE);
}

void CLookFontView::OnMenuFontInstall() 
{
//	RemoveTmpFont();
//	ChangeSelect();
	int		i;
	CString	csTmpFile = m_FontList.GetTempFontFile(m_nDispCurPos);

	if (MessageBox(_T("VistaやWindows7では、管理者権限を求める警告が出る事があります。\n警告画面が出た場合、一定時間内に「許可」又は「実行」して下さい。\nこれより指定の一時フォントを本番インストールしますが宜しいですか？"), _T("フォントインストール注意"), MB_ICONWARNING | MB_OKCANCEL) == IDCANCEL) {
		return ;
	}

	if (InstallFontVBS2(csTmpFile) == TRUE) {
		m_FontList.ChangeFontInstaledlModeAt (m_nDispCurPos) ;
		for(i = 0; i < m_asTmpFontFiles.GetSize(); i++) {
			if (m_asTmpFontFiles.GetAt(i).CompareNoCase(csTmpFile) == 0) {
				m_asTmpFontFiles.RemoveAt(i);
				break ;
			}
		}
	}
	else {
		MessageBox(_T("フォントのインストールに失敗しました。\n手動でインストールしてみて下さい。"), _T("フォントインストール中止"), MB_ICONERROR);
		return;
	}
//	m_asTmpFontFiles.RemoveAll();

//	m_FontList.GetFontList(TRUE);
//	m_FontList.SetFontCandidateAt(m_nDispCurPos, FALSE);
//	Invalidate();
	ChangeSelect();
}

BOOL CLookFontView::InstallFontVBS(CString csFontPath)
{
	CString	csVBS ;
	csVBS = theApp.m_appBoot + _T("\\InstallTmpFont.exe") ;
//	HINSTANCE hRet = ShellExecute (NULL, "open", csVBS, csFontPath, theApp.m_appBoot, SW_SHOWNORMAL) ;

	SHELLEXECUTEINFO	sei = { 0 };
	//構造体のサイズ
	sei.cbSize = sizeof(SHELLEXECUTEINFO);
	//起動側のウインドウハンドル
	sei.hwnd = m_hWnd;
	//起動後の表示状態
	sei.nShow = SW_SHOWNORMAL;
	//このパラメータが重要で、セットしないとSHELLEXECUTEINFO構造体のhProcessメンバがセットされない。
	sei.fMask = SEE_MASK_NOCLOSEPROCESS;
	//起動プログラム
	sei.lpFile = (LPCSTR)csVBS;
	sei.lpParameters = (LPCSTR)csFontPath;
	sei.lpDirectory = (LPCSTR)theApp.m_appBoot;
	//プロセス起動
	if(!ShellExecuteEx(&sei) || (const int)sei.hInstApp <= 32){	
		TRACE("error ShellExecuteEx\n");
		return FALSE;
	}
	//終了を待つ
	WaitForSingleObject( sei.hProcess, INFINITE ) ;				


//	if (hRet > 32) {
//	}
	return TRUE;
}


BOOL CLookFontView::InstallFontVBS2(CString csFontPath)
{
//	CInstallLuncherDlg*	pParent = (CInstallLuncherDlg*)pParam;
	CByteArray			abCmd;	// コマンドラインバッファ
	STARTUPINFO			si;
	PROCESS_INFORMATION	pi;
	BOOL				bRet;
	int					i;

	ZeroMemory( &si, sizeof(STARTUPINFO));
	ZeroMemory( &pi, sizeof(PROCESS_INFORMATION));

	CString	csVBS ;
	csVBS = theApp.m_appBoot + _T("\\InstallTmpFont.exe ") + csFontPath ;
	abCmd.SetSize(csVBS.GetLength() + 1);
	strcpy((LPSTR)abCmd.GetData(), csVBS);

	// プロセス起動
	bRet = CreateProcess(NULL, (LPSTR)abCmd.GetData(), 
		NULL, NULL, FALSE, CREATE_DEFAULT_ERROR_MODE, NULL, NULL, &si, &pi);

	if(!bRet) {
//		pParent->SendMessage(IL_MSG_ENDPROCESS, (WPARAM)FALSE);
		return FALSE;
	}

	// プロセス起動まで待機
	for(i = 0; i < 500; i++) {
		if(WaitForSingleObject(pi.hProcess, 0) != WAIT_OBJECT_0)
			break;
		Sleep(10);
	}

	// プロセス終了まで待機
	WaitForSingleObject(pi.hProcess, INFINITE);
	DWORD	wExCode	;
	GetExitCodeProcess(pi.hProcess, &wExCode) ;

	CloseHandle(pi.hThread);
	CloseHandle(pi.hProcess);

//	pParent->SendMessage(IL_MSG_ENDPROCESS, (WPARAM)TRUE);
	
	return (wExCode == 1)? TRUE : FALSE;
}

void CLookFontView::OnUpdateMenuFontInstall(CCmdUI* pCmdUI) 
{
	if (m_bInstall) {
		if(m_nDispCurPos < 0)
			return;
		pCmdUI->Enable(m_FontList.IsFontTmpAt(m_nDispCurPos) == TRUE);
	}
	else {
		pCmdUI->Enable(FALSE);
	}
}

void CLookFontView::OnMenuSelNormal() 
{
	if (GetToolBarDispMode(ID_NORMAL_LIST) == 1) {
		m_Ini.DispMode = 0;
	}
	else {
		m_Ini.DispMode = 0;
	}
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuSelNormal(CCmdUI* pCmdUI) 
{
	pCmdUI->SetCheck(m_Ini.DispMode == 0? 1 : 0);
}

void CLookFontView::OnMenuSelFavorite() 
{
	if (GetToolBarDispMode(ID_FAVORITE) == 1) {
		m_Ini.DispMode = 1;
	}
	else {
		m_Ini.DispMode = 1;
	}
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuSelFavorite(CCmdUI* pCmdUI) 
{
	pCmdUI->SetCheck(m_Ini.DispMode == 1? 1 : 0);
}

void CLookFontView::OnMenuSelCandidate() 
{
	if (GetToolBarDispMode(ID_CANDIDATE) == 1) {
		m_Ini.DispMode = 2;
	}
	else {
		m_Ini.DispMode = 2;
	}
	ChangeSelect();
}

void CLookFontView::OnUpdateMenuSelCandidate(CCmdUI* pCmdUI) 
{
	pCmdUI->SetCheck(m_Ini.DispMode == 2? 1 : 0);
}

int	CLookFontView::GetToolBarDispMode (int iNid)
{
	int iRet = 0	;
	CToolBar* pBar = (CToolBar*)GetParentFrame()->GetDescendantWindow(ID_VIEW_TABSELECTBAR);
	if (pBar != NULL) {
		UINT iButtonID;
		UINT iButtonStyle;
		int iButtonImage;
		int iButtonIndex = pBar->CommandToIndex(iNid);
		pBar->GetButtonInfo(iButtonIndex, iButtonID, iButtonStyle, iButtonImage);
		if (iButtonStyle & TBBS_PRESSED) {
		// Button Down
		}
		else {
			if (iButtonStyle & (TBBS_CHECKED & TBBS_DISABLED)) {
			// Button Down & Unavailable
			}
			else {
				if (iButtonStyle & TBBS_DISABLED) {
				// Button Disabled
				}
				else {
					if (iButtonStyle & TBBS_INDETERMINATE) {
					// Button State Indeterminate
					}
					else {
						if (iButtonStyle & TBBS_CHECKED) {
							// Button Checked
							iRet = 1 ;
						}
						else {
							// Button Up & Enabled
						}
					}
				}
			}
		}
	}
	return iRet;
}

BOOL	CLookFontView::CanDoFromWindowsVer ()
{
	BOOL	bRet = FALSE ;
//	LPOSVERSIONINFOEX lpVersionInformation,  // バージョン情報
//	DWORD dwTypeMask,           // 比較対象の属性
//	DWORDLONG dwlConditionMask  // 比較演算子の種類
//	VerifyVersionInfo(
//);

	OSVERSIONINFO osvi;
	ZeroMemory (&osvi, sizeof OSVERSIONINFO);
	osvi.dwOSVersionInfoSize = sizeof OSVERSIONINFO;

	if ( !GetVersionEx (&osvi) ){
		// エラー
		return bRet;
	}
	if ( osvi.dwPlatformId == VER_PLATFORM_WIN32s ){
		return FALSE;
	}
	if ( osvi.dwPlatformId == VER_PLATFORM_WIN32_NT ){
		if ( osvi.dwMajorVersion == 4L ){
			return FALSE;
		}
		if ( osvi.dwMajorVersion == 5L && osvi.dwMinorVersion == 0L ){
			// 2000
			return TRUE;
		}
		if ( osvi.dwMajorVersion == 5L && osvi.dwMinorVersion == 1L ){
			// XP
			return TRUE;
		}
		if ( osvi.dwMajorVersion == 6L && osvi.dwMinorVersion == 0L ){
			// Vista
			return FALSE;
		}
		if ( osvi.dwMajorVersion == 6L && osvi.dwMinorVersion == 1L ){
			// 7
			return FALSE;
		}
		// NT3
		return FALSE;
	}
	ASSERT(osvi.dwPlatformId == VER_PLATFORM_WIN32_WINDOWS);
	if ( osvi.dwMajorVersion == 4L && osvi.dwMinorVersion == 10L ){
		// 98
		return FALSE;
	}
	if ( osvi.dwMajorVersion == 4L && osvi.dwMinorVersion == 90L ){
		// Me
		return FALSE;
	}
	// 95
	return FALSE;
}

void CLookFontView::OnMenuTest()
{
	// TODO: ここにコマンド ハンドラー コードを追加します。
	int				i;
	CString			sCurText;	// クリップボードへセットする加工後文字
	CCharCode		cc;
	CByteArray		abText;
	HGLOBAL			hMem;
	LPTSTR			pStr;
	LPCSTR			pmText;
	LPSTR			pmStr;

	if (m_nDispCurPos < 0)
		return;

	// 有効文字に変換
	sCurText = m_FontList.AvailableChar(m_nDispCurPos, m_Ini.DispText);

	// クリップボードオープン
	for (i = 0; i < LF_CB_RETRY; i++) {
		if (OpenClipboard())
			break;
		Sleep(LF_CB_WAIT);
	}
	if (i >= LF_CB_RETRY)
		return;

	EmptyClipboard();

#ifndef _UNICODE
	// テキスト
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, sCurText.GetLength() + 1);
	pStr = (LPSTR)GlobalLock(hMem);
	strcpy(pStr, sCurText);
	GlobalUnlock(hMem);
	SetClipboardData(CF_TEXT, hMem);
#else
	// UNICODEテキスト
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, (sCurText.GetLength() + 1) * sizeof(wchar_t));
	pStr = (wchar_t*)GlobalLock(hMem);
	wcscpy(pStr, sCurText);
	GlobalUnlock(hMem);
	SetClipboardData(CF_UNICODETEXT, hMem);
#endif

	// RTF形式
	CStringList	wList;
	wList.AddTail(m_FontList.GetFontFaceAt(m_nDispCurPos));
	wList.AddTail(m_FontList.GetFontFaceAt(m_nDispCurPos+1));
	wList.AddTail(m_FontList.GetFontFaceAt(m_nDispCurPos+2));

	CRtf::ConvRtfText2(abText, sCurText, &wList, m_Ini.DispFontSize, m_FontList.GetCharSetAt(m_nDispCurPos), m_FontList.IsBoldAt(m_nDispCurPos));
	pmText = (LPCSTR)abText.GetData();
	hMem = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, strlen(pmText) + 1);
	pmStr = (LPSTR)GlobalLock(hMem);
	strcpy(pmStr, pmText);
	GlobalUnlock(hMem);
	SetClipboardData(RegisterClipboardFormat(CF_RTF), hMem);

	// 拡張メタファイル設定
	HENHMETAFILE	hMeta;
	hMeta = CreateEmf(sCurText);
	if (hMeta)
		SetClipboardData(CF_ENHMETAFILE, hMeta);

	CloseClipboard();
}
