//////////////////////////////////////////////////////////////////////
// プロダクトID＋パスワード／シリアルキー変換クラス
// 8桁のプロダクトID＋8桁のパスワード と 25桁のシリアルキー の変換を行う
// プロダクトID,パスワード は英数字からなる
// シリアルキーは数字と22種の英大文字からなる
//////////////////////////////////////////////////////////////////////
#include <string.h>
#include <crtdbg.h>
#include "SerialKey.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// 定義
//////////////////////////////////////////////////////////////////////
#define	SLKY_NUM_CHK			5								// チェックデジット桁数
#define	SLKY_CI_KEY				((unsigned char*)"Pluss_SerialKey:")	// 暗号キー
#define	SLKY_CI_NUM				16								// 暗号キー桁数

//////////////////////////////////////////////////////////////////////
// 変換テーブル
//////////////////////////////////////////////////////////////////////
unsigned char CSerialKey::CharToDigit6[128] = {
//	 0   1   2   3   4   5   6   7   8   9   A   B   C   D   E   F
	 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,		// 0x00
	 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,		// 0x10
	 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,		// 0x20
	53, 54, 55, 56, 57, 58, 59, 60, 61, 62,  0,  0,  0,  0,  0,  0,		// 0x30
	 0, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41,		// 0x40
	42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52,  0,  0,  0,  0,  0,		// 0x50
	 0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15,		// 0x60
	16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26,  0,  0,  0,  0,  0		// 0x70
};

char CSerialKey::Digit5ToChar[32] = {
	'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S',
	'T', 'U', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
};

//////////////////////////////////////////////////////////////////////
// 構築/消滅
//////////////////////////////////////////////////////////////////////

CSerialKey::CSerialKey()
{
	int		i;

	// 変換テーブル作成

	// 6ビット数値を英数字1文字に変換
	for(i = 0; i < 64; i++)
		Digit6ToChar[i] = '\0';
	for(i = 0; i < 128; i++) {
		if(CharToDigit6[i] != 0) {
			_ASSERT(Digit6ToChar[CharToDigit6[i]] == '\0');
			Digit6ToChar[CharToDigit6[i]] = (char)i;
		}
	}

	// シリアルキー1文字を5ビット数値に変換
	for(i = 0; i < 128; i++)
		CharToDigit5[i] = 0xff;
	for(i = 0; i < 32; i++) {
		_ASSERT(CharToDigit5[Digit5ToChar[i]] == 0xff);
		CharToDigit5[Digit5ToChar[i]] = (char)i;
	}

	// 暗号キー作成
	m_ca.SetKey(SLKY_CI_KEY, SLKY_CI_NUM);

	// スクランブル数値作成
	for(i = 0; i < SLKY_NUM_ID + CAST128_BLOCK_SIZE; i++)
		m_cScDigit6[i] = (unsigned char)i;
	for(i = 0; i < SLKY_NUM_ID; i += SLKY_NUM_ID)
		m_ca.Encode(m_cScDigit6 + i);
	for(i = 0; i < SLKY_NUM_ID; i++)
		m_cScDigit6[i] &= 0x3f;
}

CSerialKey::~CSerialKey()
{

}

//////////////////////////////////////////////////////////////////////
// インタフェース関数
//////////////////////////////////////////////////////////////////////

//********************************************************************
// プロダクトID,パスワードからシリアルキーを作成する
//	pProductID	I	プロダクトID	SLKY_NUM_PID(=8)桁の英数字，最後の'\0'は無くても良い
//	pPassword	I	パスワード		SLKY_NUM_PWD(=8)桁の英数字，最後の'\0'は無くても良い
//	pSerialKey	I/O	シリアルキー	SLKY_NUM_KEY(=25)+1 バイト以上のバッファ
//	return	true:正常 false:異常(プロダクトID,パスワードに使用できない文字がある)
bool CSerialKey::Encode(const char *pProductID, const char *pPassword, char *pSerialKey)
{
	int				i;
	char			cID[SLKY_NUM_ID];
	unsigned char	cDigit6[SLKY_NUM_ID + SLKY_NUM_CHK];		// 6ビット数値列
	unsigned char	cChkDegit6[SLKY_NUM_CHK];					// チェックデジット

	// ID作成
	memcpy(cID, pProductID, SLKY_NUM_PID);
	memcpy(cID + SLKY_NUM_PID, pPassword, SLKY_NUM_PWD);

	// 6ビットに変換
	for(i = 0; i < SLKY_NUM_ID; i++) {
		if(cID[i] < 0 || cID[i] > 127 || CharToDigit6[cID[i]] == 0)	// 使用できない文字がある
			return false;			// エラー
		cDigit6[i] = CharToDigit6[cID[i]];
	}

	// チェックデジット作成
	MakeChkDegit(cDigit6, cChkDegit6);
	memcpy(cDigit6 + SLKY_NUM_ID, cChkDegit6, SLKY_NUM_CHK);

	// スクランブル
	for(i = 0; i < SLKY_NUM_ID; i++) {
		cDigit6[i] = cDigit6[i] ^ m_cScDigit6[i];
	}

	// シリアルキー作成
	Degit6ToSerialKey(cDigit6, pSerialKey);

	return true;
}

//********************************************************************
// シリアルキーからプロダクトID,パスワードを得る
//	pSerialKey	I	シリアルキー	SLKY_NUM_KEY桁の文字列，最後の'\0'は無くても良い
//	pProductID	I/O	プロダクトID	SLKY_NUM_PID(=8)+1 バイト以上のバッファ NULL:格納しない
//	pPassword	I/O	パスワード		SLKY_NUM_PWD(=8)+1 バイト以上のバッファ NULL:格納しない
//	return	true:正常 false:異常(シリアルキーが異常)
// 解説
//	pProductID,pPassword が共に NULL の場合はシリアルキーの有効チェックのみ行う
bool CSerialKey::Decode(const char *pSerialKey, char *pProductID, char *pPassword)
{
	int				i;
	unsigned char	cDigit6[SLKY_NUM_ID + SLKY_NUM_CHK];	// 6ビット数値列
	unsigned char	cChkDegit6[SLKY_NUM_CHK];				// チェックデジット

	// シリアルキーを6ビット数値列に変換
	if(!SerialKeyToDegit6(pSerialKey, cDigit6))
		return false;

	// スクランブル解除
	for(i = 0; i < SLKY_NUM_ID; i++) {
		cDigit6[i] = cDigit6[i] ^ m_cScDigit6[i];
	}

	// チェックデジット作成
	MakeChkDegit(cDigit6, cChkDegit6);

	// チェックデジット比較
	if(memcmp(cDigit6 + SLKY_NUM_ID, cChkDegit6, SLKY_NUM_CHK) != 0)
		return false;

	// プログラムバグチエック
	for(i = 0; i < SLKY_NUM_ID; i++) {
		if(Digit6ToChar[cDigit6[i]] == '\0') {
			_ASSERT(0);
			return false;
		}
	}

	// プロダクトID作成
	if(pProductID) {
		for(i = 0; i < SLKY_NUM_PID; i++)
			pProductID[i] = Digit6ToChar[cDigit6[i]];
		pProductID[SLKY_NUM_PID] = '\0';
	}

	// パスワード作成
	if(pPassword) {
		for(i = 0; i < SLKY_NUM_PWD; i++)
			pPassword[i] = Digit6ToChar[cDigit6[SLKY_NUM_PID + i]];
		pPassword[SLKY_NUM_PWD] = '\0';
	}

	return true;
}

//////////////////////////////////////////////////////////////////////
// 内部関数
//////////////////////////////////////////////////////////////////////

//********************************************************************
// チェックデジット作成
//	pIDDegit	I	6ビットID数値列
//	pChkDegit	I/O	チェックデジット SLKY_NUM_CHK(=5)バイトの領域
void CSerialKey::MakeChkDegit(const unsigned char *pIDDegit6, unsigned char *pChkDegit6)
{
	unsigned char	cWork[SLKY_NUM_ID + CAST128_BLOCK_SIZE];
	int				i, j;

	// 初期化
	memset(pChkDegit6, 0x00, SLKY_NUM_CHK);
	memset(cWork, 0x00, sizeof cWork);
	memcpy(cWork, pIDDegit6, SLKY_NUM_ID);

	for(i = 0; i < SLKY_NUM_ID; i += CAST128_BLOCK_SIZE) {
		m_ca.Encode(cWork + i);
		for(j = i; j < i + CAST128_BLOCK_SIZE; j++)
			pChkDegit6[j % SLKY_NUM_CHK] ^= cWork[j];
	}

	// 6ビットマスク
	for(i = 0; i < SLKY_NUM_CHK; i++)
		pChkDegit6[i] &= 0x3f;

	// 最後の桁だけ5ビット
	// 21桁×6ビットで126ビットになるが、必要なビットは25*5で125ビットのため1ビットオーバー
	pChkDegit6[SLKY_NUM_CHK - 1] &= 0x1f;
}

//********************************************************************
// 6ビット数値列→シリアルキー
//	pDegit		I	6ビット数値列
//	pSerialKey	I/O	シリアルキー
void CSerialKey::Degit6ToSerialKey(const unsigned char *pDegit6, char *pSerialKey)
{
	int				i;
	unsigned int	nCurDegit = 0;	// ビット列
	int				nCurBit = 0;	// 現在の残ビット
	int				nCurPos = 0;	// 数値列インデックス
	int				nD5;

	// シリアルキー作成
	// 6ビット数値列を5ビット毎に1文字に変換
	for(i = 0; i < SLKY_NUM_KEY; i++) {
		if(nCurBit < 5) {	// アンダーフロー
			// 数値列から1桁取得
			nCurDegit = nCurDegit | ((unsigned int)pDegit6[nCurPos] << nCurBit);
			nCurBit += 6;
			nCurPos++;
		}
		// 5ビット取得
		nD5 = nCurDegit & 0x1f;
		nCurDegit >>= 5;
		nCurBit -= 5;
		// シリアルキーに変換
		pSerialKey[i] = Digit5ToChar[nD5];
	}

	pSerialKey[SLKY_NUM_KEY] = '\0';
}

//********************************************************************
// シリアルキー→6ビット数値列
//	pSerialKey	I	シリアルキー
//	pDegit		I/O	6ビット数値列
//	return true:正常 false:異常(シリアルキーに不正な文字がある)
bool CSerialKey::SerialKeyToDegit6(const char *pSerialKey, unsigned char *pDegit6)
{
	int				i;
	unsigned int	nCurDegit = 0;	// ビット列
	int				nCurBit = 0;	// 現在の残ビット
	int				nCurPos = 0;	// 数値列インデックス
	unsigned char	cDigit5[SLKY_NUM_KEY+1];	// 5ビット数値列

	// シリアルキーを5ビット数値列に変換
	for(i = 0; i < SLKY_NUM_KEY; i++) {
		if(pSerialKey[i] < 0 || pSerialKey[i] > 127 || CharToDigit5[pSerialKey[i]] == 0xff)	// 不正文字
			return false;
		cDigit5[i] = CharToDigit5[pSerialKey[i]];
	}
	// 25桁*5ビットで125ビット生成されるが、6の倍数126ビットに切り上げる必要があるため1桁追加
	cDigit5[SLKY_NUM_KEY] = 0;	// 冗長ビット

	// 5ビット数値列を6ビット数値列に変換
	for(i = 0; i < SLKY_NUM_ID + SLKY_NUM_CHK; i++) {
		while(nCurBit < 6) {	// アンダーフロー
			// 数値列から1桁取得
			nCurDegit = nCurDegit | ((unsigned int)cDigit5[nCurPos] << nCurBit);
			nCurBit += 5;
			nCurPos++;
		}
		// 6ビット取得
		pDegit6[i] = nCurDegit & 0x3f;
		nCurDegit >>= 6;
		nCurBit -= 6;
	}

	return true;
}
