/////////////////////////////////////////////////////////////////////////////
// LookFont 設定内容構造体
/////////////////////////////////////////////////////////////////////////////

#pragma once
//#include	<afxmt.h>
#define	MAX_FAVARTE_NO	20
class CLookFontSetting {
public:
	// Window項目
	int				Width1;					// フォント名幅
	// 表示選択
	int				SelLangEN;				// 欧文
	int				SelLangJA;				// 日本語
	int				SelLangALL;				// 全て
	int				SelFontTMP;				// 一時インストールフォント
	int				SelFontORG;				// 既存フォント
	// 表示テキスト
	int 			DispFontSize;			// 表示サイズ ポイント * 10
	CString			DispText;				// 表示テキスト
	// 自動アップデート
	int				AutoUpdate;				// 自動アップデート
	int				FavMax;					// お気に入りフォント最大数
	CStringList		Favorite;				// お気に入りフォント名リスト
	// 表示モード
	int 			DispMode;				// 表示モード（0：標準/1：お気に入り/2：候補）
//	CCriticalSection _section;
};
