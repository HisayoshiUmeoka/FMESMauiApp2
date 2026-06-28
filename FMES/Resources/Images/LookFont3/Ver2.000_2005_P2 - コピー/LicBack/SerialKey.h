// SerialKey.h: CSerialKey クラスのインターフェイス
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_SERIALKEY_H__8CDC3722_5687_456D_98BF_5F9207A08E50__INCLUDED_)
#define AFX_SERIALKEY_H__8CDC3722_5687_456D_98BF_5F9207A08E50__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include "Cast128.h"

//////////////////////////////////////////////////////////////////////
// 定義
//////////////////////////////////////////////////////////////////////
#define	SLKY_NUM_PID			8			// プロダクトID桁数
#define	SLKY_NUM_PWD			8			// パスワード桁数
#define	SLKY_NUM_KEY			25			// シリアルキー桁数
#define	SLKY_NUM_ID				(SLKY_NUM_PID + SLKY_NUM_PWD)	// ID桁数

class CSerialKey  
{
public:
	CSerialKey();
	virtual ~CSerialKey();
	bool Encode(const char *pProductID, const char *pPassword, char *pSerialKey);
	bool Decode(const char *pSerialKey, char *pProductID, char *pPassword);

protected:
	void MakeChkDegit(const unsigned char *pIDDegit6, unsigned char *pChkDegit6);
	void Degit6ToSerialKey(const unsigned char *pDegit6, char *pSerialKey);
	bool SerialKeyToDegit6(const char *pSerialKey, unsigned char *pDegit6);

	// 変換テーブル
	char					Digit6ToChar[64];		// 6ビット数値を英数字1文字に変換
	static unsigned char	CharToDigit6[128];		// 英数字1文字を6ビット数値に変換
	unsigned char			CharToDigit5[128];		// シリアルキー1文字を5ビット数値に変換
	static char				Digit5ToChar[32];		// 5ビット数値をシリアルキー1文字に変換
	unsigned char			m_cScDigit6[SLKY_NUM_ID + CAST128_BLOCK_SIZE];		// スクランブル用
	CCast128				m_ca;					// 暗号作成
};

#endif // !defined(AFX_SERIALKEY_H__8CDC3722_5687_456D_98BF_5F9207A08E50__INCLUDED_)
