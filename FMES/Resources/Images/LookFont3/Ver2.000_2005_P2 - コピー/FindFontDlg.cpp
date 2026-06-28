// FindFontDlg.cpp : インプリメンテーション ファイル
//

#include "stdafx.h"
#include "io.h"
#include "lookfont.h"
#include "FindFontDlg.h"
#include "BrowseFolder.h"
#include "FileName.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CFindFontDlg ダイアログ


CFindFontDlg::CFindFontDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CFindFontDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CFindFontDlg)
		// メモ - ClassWizard はこの位置にマッピング用のマクロを追加または削除します。
	//}}AFX_DATA_INIT
	m_FontPathList.RemoveAll ()	;
	m_FontPathCnt = 0 ;
}


void CFindFontDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CFindFontDlg)
		// メモ - ClassWizard はこの位置にマッピング用のマクロを追加または削除します。
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CFindFontDlg, CDialog)
	//{{AFX_MSG_MAP(CFindFontDlg)
	ON_BN_CLICKED(IDC_BUTTON_SEARCHPATH, OnButtonSearchpath)
	ON_BN_CLICKED(IDC_CHECK_EXT6, OnCheckExt6)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFindFontDlg メッセージ ハンドラ

BOOL CFindFontDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	SetDlgItemText (IDC_EDIT_FINDPATH, "") ;
	SetDlgItemText (IDC_EDIT_EXTETC, "") ;
	((CWnd *)(GetDlgItem (IDC_EDIT_EXTETC)))->EnableWindow (FALSE) ;
	int	i	;
	for (i = 0 ; i < 5 ; i++) {
		((CButton *)(GetDlgItem (IDC_CHECK_EXT1 + i)))->SetCheck (1) ;
	}
	((CButton *)(GetDlgItem (IDC_CHECK_EXT6)))->SetCheck (0) ;
	return TRUE;  // コントロールにフォーカスを設定しないとき、戻り値は TRUE となります
	              // 例外: OCX プロパティ ページの戻り値は FALSE となります
}

void CFindFontDlg::OnOK() 
{
	CString		csPath		,
				csEtc		,
				csMsg		;
	CStringList	clExt		;
	int			i = 0		;

	GetDlgItemText (IDC_EDIT_FINDPATH, csPath) ;
	m_FontPathCnt = 0 ;
	clExt.RemoveAll () ;
	m_FontPathList.RemoveAll () ;
	if (((CButton *)(GetDlgItem (IDC_CHECK_EXT1 + i++)))->GetCheck () == 1) {
		clExt.AddTail (_T(".ttf")) ;
	}
	if (((CButton *)(GetDlgItem (IDC_CHECK_EXT1 + i++)))->GetCheck () == 1) {
		clExt.AddTail (_T(".ttc")) ;
	}
	if (((CButton *)(GetDlgItem (IDC_CHECK_EXT1 + i++)))->GetCheck () == 1) {
		clExt.AddTail (_T(".fon")) ;
	}
	if (((CButton *)(GetDlgItem (IDC_CHECK_EXT1 + i++)))->GetCheck () == 1) {
		clExt.AddTail (_T(".pfm")) ;
	}
	if (((CButton *)(GetDlgItem (IDC_CHECK_EXT1 + i++)))->GetCheck () == 1) {
		clExt.AddTail (_T(".otf")) ;
	}
	if (((CButton *)(GetDlgItem (IDC_CHECK_EXT1 + i++)))->GetCheck () == 1) {
		GetDlgItemText (IDC_EDIT_EXTETC, csEtc) ;
		if (csEtc.GetLength () > 0) {
			csEtc.Insert (0, _T(".")) ;
			clExt.AddTail (csEtc) ;
		}
	}
	if (clExt.GetCount () == 0) {
		MessageBox (_T("検索するフォントファイルの拡張子が１個も選択されていません。"), _T("エラーメッセージ"), MB_OK) ;
		return ;
	}
	if (csPath.GetLength () == 0) {
		MessageBox (_T("検索するＣＤ／フォルダのパスが設定されていません。\n実在するパスを設定して下さい。"), _T("エラーメッセージ"), MB_OK) ;
		return ;
	}
	if (_taccess (csPath, 0) != 0) {
		MessageBox (_T("検索するＣＤ／フォルダのパスに実在しないパスが設定されています。\n実在するパスを設定して下さい。"), _T("エラーメッセージ"), MB_OK) ;
		return ;
	}
	if (MessageBox (_T("これより指定パス以下の指定拡張子のフォントファイルを検索します。\nパス以下の検索には時間が掛かる場合があります。\n処理を開始して宜しいですか？"), _T("ワーニング"), MB_ICONWARNING | MB_OKCANCEL) == IDCANCEL) {
		CDialog::OnCancel();
		return ;
	}
	m_FontPathCnt = SearchFonts(csPath, clExt, m_FontPathList) ;
	if (m_FontPathCnt > 0) {
		csMsg.Format (_T("指定パス以下に %d 個の対象ファイルが見つかりました\nこのまま一時インストールを実行して宜しいですか？"), m_FontPathCnt) ;
		if (MessageBox (csMsg, _T("インフォメーション"), MB_ICONINFORMATION | MB_OKCANCEL) == IDCANCEL) {
			CDialog::OnCancel();
			return ;
		}
	}
	else {
		MessageBox (_T("検索するＣＤ／フォルダのパスが設定されていません。\n実在するパスを設定して下さい。"), _T("エラーメッセージ"), MB_OK) ;
		return ;
	}
	CDialog::OnOK();
}

void CFindFontDlg::OnButtonSearchpath() 
{
	CStringArray	asFiles;
	CString			sPath;
	GetDlgItemText (IDC_EDIT_FINDPATH, sPath) ;
	CBrowseFolder	bf(this, sPath, _T("ＣＤ／フォルダ検索 一時インストール"), BIF_RETURNONLYFSDIRS|BIF_STATUSTEXT) ;
	if(bf.DoModal() != IDOK)
		return;
	sPath = bf.GetPath () ;
	SetDlgItemText (IDC_EDIT_FINDPATH, sPath) ;
}

void CFindFontDlg::OnCancel() 
{
	// TODO: この位置に特別な後処理を追加してください。
	
	CDialog::OnCancel();
}

int CFindFontDlg::SearchFonts(CString csPath, CStringList &clExt, CStringList &clFont) 
{
	int			i					,
				i_ret				,
				i_cnt = 0			;
	CString		cs_file				,
				cs_file2			,
				cs_work				;
	char		c_temp[MAX_PATH]	;
	CFileFind	finddir				;
	CFileFind	subfinddir			;
	BOOL		b_dir				,
				b_fund				;
	CStringList	cl_w				;
	CString		csPath2	,
				csExt	,
				cs_dir	,
				cs_dir2	,
				cs_dir3	,
				cs_work2,
				cs_lastdir	,
				cs_newf;
		CString	cs_low	,
				csPath3	,
				csPath4	;
		CString	cs_err	;

//	cl_ret.RemoveAll () ;
//	cl_w.RemoveAll () ;
	/* ベースとなる親フォルダーを取得する	*/
	memset (c_temp, 0, sizeof (char) * MAX_PATH) ;
	strcpy (c_temp, (const char *)csPath) ;
	if (c_temp[strlen (c_temp) - 1] != '\\') {
		strcat (c_temp, "\\") ;
	}
	cs_file2.Format ("%s*%s", c_temp, "*") ;
	cs_file.Format ("%s*", c_temp) ;

	/* 子フォルダーの検索開始	*/
	i_cnt = 0 ;

	b_fund = subfinddir.FindFile (cs_file2) ;

TRACE (csPath + "検索中...\n") ;
	b_dir = finddir.FindFile (cs_file) ;
	while (b_dir) {
		b_dir = finddir.FindNextFile () ;
		if (!finddir.IsDirectory ()) {
			/* TAKO	*/
			if (b_fund == FALSE) {
				continue ;
			}
			cs_work = finddir.GetFileName () ;
			for (i = 0 ; i < clExt.GetCount () ; i++) {
				csExt = clExt.GetAt (clExt.FindIndex (i)) ;
				if (cs_work.GetLength () >= csExt.GetLength ()) {
					cs_work2 = cs_work.Right (csExt.GetLength ()) ;
					cs_work2.MakeLower () ;
					csExt.MakeLower () ;
					if (cs_work2 == csExt) {
						//ファイル発見！
						cs_dir = finddir.GetFilePath () ;
						clFont.AddTail (cs_dir) ;
						i_cnt++ ;
						break ;
					}
				}
			}
		}
		else {
			cs_work = finddir.GetFileName () ;
			if (cs_work == "." || cs_work == "..") {
			}
			else {
				cs_low = cs_work ;
				cs_low.MakeLower () ;
				csPath2 = csPath + "\\" + cs_work ;
				csPath3 = csPath + "\\" + cs_low ;
				_trename ((LPCTSTR)csPath2, (LPCTSTR)csPath3) ;
				i_ret = SearchFonts(csPath3, clExt, clFont) ;
				if (i_ret > 0) {
					i_cnt += i_ret ;
				}
			}
		}
	}
	return (i_cnt) ;
}

void CFindFontDlg::OnCheckExt6() 
{
	if (((CButton *)(GetDlgItem (IDC_CHECK_EXT6)))->GetCheck () == 1) {
		((CWnd *)(GetDlgItem (IDC_EDIT_EXTETC)))->EnableWindow (TRUE) ;
	}
	else {
		((CWnd *)(GetDlgItem (IDC_EDIT_EXTETC)))->EnableWindow (FALSE) ;
	}
}
