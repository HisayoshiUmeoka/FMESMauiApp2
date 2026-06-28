// MainFrm.h : CMainFrame クラスの宣言およびインターフェイスの定義をします。
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_MAINFRM_H__03E0D242_F48A_4EFD_BD2A_9AF719494AFB__INCLUDED_)
#define AFX_MAINFRM_H__03E0D242_F48A_4EFD_BD2A_9AF719494AFB__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

// レジストリ
#define	LF_REG_WINDOW_VER				_T("02000")				// レジストリバージョン
																// コントロールバーに変更があり、前版との整合性が保てない場合は文字列を変える
#define	LF_REG_WINDOW_SEC				_T("Window")			// セクション名
#define	LF_REG_WINDOW_KEY_VER			_T("Ver")				// キー名
#define	LF_REG_WINDOW_KEY_CONTROLBAR	_T("ControlBar")		// コントロールバー

class CMainFrame : public CFrameWnd
{
	
protected: // シリアライズ機能のみから作成します。
	CMainFrame();
	DECLARE_DYNCREATE(CMainFrame)

// アトリビュート
public:

// オペレーション
public:

// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CMainFrame)
	public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	virtual BOOL DestroyWindow();
	virtual void WinHelp(DWORD dwData, UINT nCmd = HELP_CONTEXT);
	//}}AFX_VIRTUAL

// インプリメンテーション
public:
	virtual ~CMainFrame();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:  // コントロール バー用メンバ
	void DockControlBarLeftOf(CControlBar* pBar, CControlBar* pLeftOf);

	CToolBar		m_wndToolBar;
	CToolBar		m_wndFontSelectBar;
	CToolBar		m_wndFontTabBar;
	CDialogBar		m_wndTextCtrlBar;

// 生成されたメッセージ マップ関数
protected:
	//{{AFX_MSG(CMainFrame)
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnClose();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_MAINFRM_H__03E0D242_F48A_4EFD_BD2A_9AF719494AFB__INCLUDED_)
