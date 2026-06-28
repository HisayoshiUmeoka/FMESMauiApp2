#pragma once

/////////////////////////////////////////////////////////////////////////////
// 定義
/////////////////////////////////////////////////////////////////////////////
#define	CAST128_MAX_KEY			16		// 最大暗号キーサイズ
#define	CAST128_BLOCK_SIZE		8		// 変換ブロックサイズ

class CCast128
{
public:
	CCast128(void);
	~CCast128(void);
	bool SetKey(const unsigned char* pKey, int nKeyLength);
	bool Encode(unsigned char* pData);
	bool Decode(unsigned char* pData);

protected:
	bool			m_bSetKey;			// 暗号キー設定フラグ
	unsigned long	m_uSubkey[32];		// 中間キー32DWORD
};
