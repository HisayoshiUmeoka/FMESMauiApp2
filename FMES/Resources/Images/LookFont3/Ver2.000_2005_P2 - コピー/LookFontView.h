// LookFontView.h : CLookFontView クラスの宣言およびインターフェイスの定義をします。
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_LOOKFONTVIEW_H__481B6A82_689B_495E_B997_E51FB8FDFBD3__INCLUDED_)
#define AFX_LOOKFONTVIEW_H__481B6A82_689B_495E_B997_E51FB8FDFBD3__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "LookFontSetting.h"
#include "FontList.h"


#define	LF_SPACELINE		1					// 行間スペース
#define	LF_SPACEROW			1					// 列間スペース
#define	LF_SPACEFRAME		2					// 周囲スペース
#define	LF_RECTWIDTH		2					// 選択線幅
#define	LF_NUMLINE			100					// 最大行数
#define	LF_MC_HITPIX		2					// マウスカーソルヒット範囲
#define	LF_CB_RETRY			10					// クリップボードオープンリトライ回数
#define	LF_CB_WAIT			10					// クリップボードリトライ間隔

#define	LF_PRN_SPACEFRAME	5					// 周囲スペース
#define	LF_PRN_SPACELINE	5					// 行間スペース
#define	LF_PRN_SPACEROW		5					// 列間スペース


#define	LF_CLR_FRAME		RGB(0xff,0x00,0x00)	// フレームカラー
#define	LF_CLR_SEPROW		RGB(0xc0,0xc0,0xc0)	// 列境界線カラー
#define	LF_CLR_FONTTMP		RGB(0x00,0xff,0x00)	// 一時インストールフォントカラー

// INIセクション名
#define	LF_REG_WINDOW		_T("Window")		// Window設定セクション名
#define	LF_REG_SETTINGS		_T("Settings")		// 設定セクション名
#define	LF_REG_FAVORITE		_T("Favorite")		// 設定セクション名

#define	LF_UI_FONT			_T("MS UI Gothic")	// UIフォント


class CLookFontView : public CView
{
protected: // シリアライズ機能のみから作成します。
	CLookFontView();
	DECLARE_DYNCREATE(CLookFontView)

// アトリビュート
public:
	CLookFontDoc* GetDocument();

	CLookFontSetting	m_Ini;				// 設定

// オペレーション
public:

// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CLookFontView)
	public:
	virtual void OnDraw(CDC* pDC);  // このビューを描画する際にオーバーライドされます。
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	virtual void OnPrepareDC(CDC* pDC, CPrintInfo* pInfo = NULL);
	protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnFilePrintPreview();
	//}}AFX_VIRTUAL

// インプリメンテーション
public:
	virtual ~CLookFontView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
	void ReadIni();
	void WriteIni();
	void ReadWriteIni(BOOL bRead);
	void RemoveTmpFont();
	CString LoadString(UINT nID);
	BOOL StartAutoUpdate(BOOL bAuto);
	HENHMETAFILE CreateEmf(LPCTSTR pText);
	BOOL Init(CDC *pDC);
	void UpdateText();
	void UpdateText(CDC *pDC);
	void SetCur(CDC *pDC, int nPos, BOOL bShow);
	void SetFontBox(CDC *pDC, int nPos);
	void MoveWidth(CPoint point);
	void ChangeSelect();
	void ChangeSelect(CDC *pDC);
	BOOL DrawPrinter(CDC *pDC, int iPage)	;
	int	 GetPageCount(CPrintInfo* pInfo) ;
	int  GetToolBarDispMode (int iNid) ;
	BOOL InstallFontVBS(CString csFontPath)	;
	BOOL InstallFontVBS2(CString csFontPath)	;
	BOOL CanDoFromWindowsVer ()	;


// 生成されたメッセージ マップ関数
protected:
	//{{AFX_MSG(CLookFontView)
	afx_msg void OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg BOOL OnMouseWheel(UINT nFlags, short zDelta, CPoint pt);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnLButtonDblClk(UINT nFlags, CPoint point);
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnChangeEditDisptext();
	afx_msg void OnEditchangeComboFontsize();
	afx_msg void OnSelchangeComboFontsize();
	afx_msg void OnEditCopy();
	afx_msg void OnMenuCopyfontface();
	afx_msg void OnDestroy();
	afx_msg void OnEditPaste();
	afx_msg void OnMenuUpdateCheck();
	afx_msg void OnUpdateMenuUpdateCheck(CCmdUI* pCmdUI);
	afx_msg void OnMenuTmpfontInstall();
	afx_msg void OnUpdateMenuTmpfontInstall(CCmdUI* pCmdUI);
	afx_msg void OnMenuTmpfontUninstall();
	afx_msg void OnUpdateMenuTmpfontUninstall(CCmdUI* pCmdUI);
	afx_msg void OnMenuSelLangEn();
	afx_msg void OnUpdateMenuSelLangEn(CCmdUI* pCmdUI);
	afx_msg void OnMenuSelLangJa();
	afx_msg void OnUpdateMenuSelLangJa(CCmdUI* pCmdUI);
	afx_msg void OnMenuSelLangAll();
	afx_msg void OnUpdateMenuSelLangAll(CCmdUI* pCmdUI);
	afx_msg void OnMenuSelFontTmp();
	afx_msg void OnUpdateMenuSelFontTmp(CCmdUI* pCmdUI);
	afx_msg void OnMenuSelFontOrg();
	afx_msg void OnUpdateMenuSelFontOrg(CCmdUI* pCmdUI);
	afx_msg void OnMenuSetting();
	afx_msg void OnMenuTmpfontFind();
	afx_msg void OnUpdateMenuTmpfontFind(CCmdUI* pCmdUI);
	afx_msg void OnMenuPropaty();
	afx_msg void OnUpdateMenuPropaty(CCmdUI* pCmdUI);

	afx_msg void OnMenuAddFavorite();
	afx_msg void OnUpdateMenuAddFavorite(CCmdUI* pCmdUI);
	afx_msg void OnMenuAddCandidate();
	afx_msg void OnUpdateMenuAddCandidate(CCmdUI* pCmdUI);
	afx_msg void OnMenuDelFavorite();
	afx_msg void OnUpdateMenuDelFavorite(CCmdUI* pCmdUI);
	afx_msg void OnMenuDelCandidate();
	afx_msg void OnUpdateMenuDelCandidate(CCmdUI* pCmdUI);
	afx_msg void OnMenuFontInstall();
	afx_msg void OnUpdateMenuFontInstall(CCmdUI* pCmdUI);

	afx_msg void OnMenuSelNormal();
	afx_msg void OnUpdateMenuSelNormal(CCmdUI* pCmdUI);
	afx_msg void OnMenuSelFavorite();
	afx_msg void OnUpdateMenuSelFavorite(CCmdUI* pCmdUI);
	afx_msg void OnMenuSelCandidate();
	afx_msg void OnUpdateMenuSelCandidate(CCmdUI* pCmdUI);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
	LRESULT OnFrameClose(WPARAM wParam, LPARAM lParam);
	LRESULT OnAuiEvent(WPARAM wParam, LPARAM lParam);

	void	OnEditCopy2()	;

//	CLookFontSetting	m_Ini;				// 設定

	BOOL				m_bInit;			// 初期化フラグ
	CFontList			m_FontList;			// フォントリストオブジェクト
	CDialogBar*			m_pWndTextCtrlBar;	// テキスト入力バー
	CFont				m_fontTextEdit;		// テキスト入力エリアフォント

	CString				m_sSysFontFace;		// システムフォント名
	int					m_nSysFontSize;		// システムフォントサイズ

	// GDI(Win9xリソースリーク対策の冗長コード)
	CFont				m_fontFace;			// フォント名用フォント
	CPen				m_penSepRow;		// 列仕切線

	// フォントファイル
	CString				m_sFontOpenPath;	// フォントオープンパス

	// 一時フォント
	CStringArray		m_asTmpFontFiles;	// 一時インストールファイル名

	// 表示
	CSize				m_sizeDispFace;		// フォント名最大サイズ
	BOOL				m_bDispMoveWidth;	// フォント名幅変更中
	int					m_nDispCount;		// フォント総数
	int					m_nDispTopPos;		// 先頭行表示位置
	int					m_nDispCurPos;		// 選択位置
	int					m_nDispLine;		// 表示中の行数
	BOOL				m_bDispBottomView;	// 最後の行を表示
	CDWordArray			m_anDispY;			// 描画開始Y座標
	CDWordArray			m_anDispCY;			// 描画高さ
	int					m_nDispBottom;		// 最下段Y座標

	// 操作
	int					m_nCtrlWheel;		// マウスホイール移動量

	// 印刷
	BOOL				m_bReview		;
	CRect				m_prnArea		;
	int					m_PrintPageNo	;
	int					m_PrintPageMax	;
	CSize				m_sizePrintFace	;
	CDWordArray			m_PrtPageIdx	;	// ページ毎の印刷開始インデックス
	CString				m_sPrtSysFontFace;	// システムフォント名
	int					m_nPrtSysFontSize;	// システムフォントサイズ

//	CFont				m_fontFace;			// フォント名用フォント
//	CPen				m_penSepRow;		// 列仕切線

	BOOL				m_bInstall		;

	// 自動アップデート
	enum {									// アップデートステータス
		AUSTATUS_IDLE = 0,					// 停止
		AUSTATUS_AUTO,						// 自動アップデート中
		AUSTATUS_MANUAL,					// 手動アップデート中
		AUSTATUS_CANCEL,					// キャンセル
		AUSTATUS_END						// 終了
	}					m_eAuStatus;		// アップデートステータス

public:
	afx_msg void OnMenuTest();
};

#ifndef _DEBUG  // LookFontView.cpp ファイルがデバッグ環境の時使用されます。
inline CLookFontDoc* CLookFontView::GetDocument()
   { return (CLookFontDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_LOOKFONTVIEW_H__481B6A82_689B_495E_B997_E51FB8FDFBD3__INCLUDED_)
