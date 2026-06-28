// LookFontDoc.cpp : CLookFontDoc クラスの動作の定義を行います。
//

#include "stdafx.h"
#include "LookFont.h"

#include "LookFontDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CLookFontDoc

IMPLEMENT_DYNCREATE(CLookFontDoc, CDocument)

BEGIN_MESSAGE_MAP(CLookFontDoc, CDocument)
	//{{AFX_MSG_MAP(CLookFontDoc)
		// メモ - ClassWizard はこの位置にマッピング用のマクロを追加または削除します。
		//        この位置に生成されるコードを編集しないでください。
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CLookFontDoc クラスの構築/消滅

CLookFontDoc::CLookFontDoc()
{
	// TODO: この位置に１度だけ呼ばれる構築用のコードを追加してください。

}

CLookFontDoc::~CLookFontDoc()
{
}

BOOL CLookFontDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: この位置に再初期化処理を追加してください。
	// (SDI ドキュメントはこのドキュメントを再利用します。)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CLookFontDoc シリアライゼーション

void CLookFontDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: この位置に保存用のコードを追加してください。
	}
	else
	{
		// TODO: この位置に読み込み用のコードを追加してください。
	}
}

/////////////////////////////////////////////////////////////////////////////
// CLookFontDoc クラスの診断

#ifdef _DEBUG
void CLookFontDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CLookFontDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CLookFontDoc コマンド
