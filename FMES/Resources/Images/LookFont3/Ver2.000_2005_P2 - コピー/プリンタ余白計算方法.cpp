	// Init our pt struct in case escape not supported
	pt.x = 0; pt.y = 0;

	// Locate the upper left corner of the printable area
	pt.x = GetDeviceCaps(hPrnDC, PHYSICALOFFSETX);
	pt.y = GetDeviceCaps(hPrnDC, PHYSICALOFFSETY);

	// Figure out how much you need to offset output to produce the left 
	// and top margins for the output in the printer. Note the
	// use of the "max" macro. It is possible that you are asking for
	// margins that are not possible on this printer. For example, the HP
	// LaserJet has a 0.25" unprintable area so we cannot get margins of
	// 0.1".

	xOffset = max (0, GetDeviceCaps (hPrnDC, LOGPIXELSX) * nInchesWeWant - pt.x);

	yOffset = max (0, GetDeviceCaps (hPrnDC, LOGPIXELSY) * nInchesWeWant - pt.y);

	// When doing all the output, you can either offset it by the above
	// values or call SetViewportOrg() to set the point (0,0) at
	// the margin offset you calculated.

	SetViewportOrg (hPrnDC, xOffset, yOffset);
	all other output here


	// Get the size of the printable area
	pt.x = GetDeviceCaps(hPrnDC, PHYSICALWIDTH);
	pt.y = GetDeviceCaps(hPrnDC, PHYSICALHEIGHT);

	xOffsetOfRightMargin = xOffset + GetDeviceCaps (hPrnDC, HORZRES) - pt.x - GetDeviceCaps (hPrnDC, LOGPIXELSX) * wInchesWeWant;
	yOffsetOfBottomMargin = yOffset + GetDeviceCaps (hPrnDC, VERTRES) - pt.y - GetDeviceCaps (hPrnDC, LOGPIXELSY) * wInchesWeWant;




	xOffset = max (0, GetDeviceCaps (hPrnDC, LOGPIXELSX) * nInchesWeWant - GetDeviceCaps(hPrnDC, PHYSICALOFFSETX));
	yOffset = max (0, GetDeviceCaps (hPrnDC, LOGPIXELSY) * nInchesWeWant - GetDeviceCaps(hPrnDC, PHYSICALOFFSETY));

	xOffsetOfRightMargin = xOffset + GetDeviceCaps (hPrnDC, HORZRES) - GetDeviceCaps(hPrnDC, PHYSICALWIDTH) - GetDeviceCaps (hPrnDC, LOGPIXELSX) * wInchesWeWant;
	yOffsetOfBottomMargin = yOffset + GetDeviceCaps (hPrnDC, VERTRES) - GetDeviceCaps(hPrnDC, PHYSICALHEIGHT) - GetDeviceCaps (hPrnDC, LOGPIXELSY) * wInchesWeWant;








------------------------------------------------------------------------------------------------------------------------------------
//印刷初期化API
void WINAPI InitPrinterSet(LPPRINTDLG pPd){
    pPd->lStructSize = sizeof(PRINTDLG);
    DWORD dwFlags = pPd->Flags;
    dwFlags = dwFlags ^ (PD_USEDEVMODECOPIESANDCOLLATE | PD_HIDEPRINTTOFILE);
    dwFlags |= PD_NOSELECTION | PD_NOPAGENUMS;
    pPd->Flags = dwFlags;
}

//画像印刷API
LONG WINAPI CDCToPRinterCDC(CDC *pFromDC, CBitmap *pFromBmp, int nFromX, int nFromY, int nWidth, int nHeight,
                            CDC* pPrinterCD, int nPrinterX, int nPrinterY){

    //-------------------------------------------------------------------------
    //APIの判定準備
    LONG lFlug = pPrinterCD->GetDeviceCaps(RASTERCAPS);
    BOOL bBitBlt        = (lFlug & RC_BITBLT)       == RC_BITBLT;
    BOOL bStretchBlt    = (lFlug & RC_STRETCHBLT)   == RC_STRETCHBLT;
    BOOL bStretchDIBits = (lFlug & RC_STRETCHDIB)   == RC_STRETCHDIB;
    BOOL bSetDIBitsToDevice = (lFlug & RC_DIBTODEV) == RC_DIBTODEV;
    BOOL bOK;

    BITMAP bmp;
    int x = 0;
    HBITMAP hBmp;
    try{
        //-------------------------------------------------------------------------
        //BITMAP構造体を用意
        memset(&bmp, 0, sizeof(BITMAP));
        hBmp = *pFromBmp;
        pFromBmp->GetBitmap(&bmp);

    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0001;
    }

    //-------------------------------------------------------------------------
    // バッファの１ラインの長さを計算
    // pixCnt は 画面の色のビット数をバイトに変換したもの。
    // バッファの1ラインは4の倍数バイトでなければならない。
    DWORD dwLength;
    int pixCnt = bmp.bmBitsPixel;
    int nPixX = 4;
    BYTE *lpBits = NULL;
    try{

        if ((nWidth * pixCnt) % nPixX == 0){
            dwLength = nWidth * pixCnt;
        }else{
            dwLength = nWidth * pixCnt + (nPixX - (nWidth * pixCnt) % nPixX);
        }

        lpBits = new BYTE[dwLength * nHeight];
        if(lpBits == NULL){
            AfxMessageBox("Printer buffer Create Error", MB_OK);
            return -1;
        }
    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0002;
    }

    try{
        pFromBmp->GetBitmapBits(dwLength * nHeight, lpBits);
        bmp.bmBits = lpBits;
    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0003;
    }

    //-------------------------------------------------------------------------
    //パレット情報を取得
    CPalette palette;
    int nPltCnt;
    PALETTEENTRY *pPEntry = NULL;
    try{
        palette.CreateHalftonePalette ( pFromDC );
        nPltCnt = palette.GetEntryCount();
        pPEntry = new PALETTEENTRY[nPltCnt];
        if(pPEntry == NULL){
            AfxMessageBox("Printer buffer Create Error", MB_OK);
            delete lpBits;
            return -1;
        }

        palette.GetPaletteEntries(0, nPltCnt-1, pPEntry);
    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0004;
    }

    //-------------------------------------------------------------------------
    //BITMAPINFO構造体を用意
    int nSize;
    BITMAPINFO *pBmpInfo;
    try{
        nSize = sizeof(BITMAPINFOHEADER) + (256 * sizeof(RGBQUAD));
        pBmpInfo = (BITMAPINFO*)new char[nSize];
        memset(pBmpInfo, 0, sizeof(BITMAPINFO));
        pBmpInfo->bmiHeader.biSize          = sizeof(BITMAPINFOHEADER);
        pBmpInfo->bmiHeader.biWidth         = bmp.bmWidth;
        pBmpInfo->bmiHeader.biHeight        = bmp.bmHeight * -1;
        pBmpInfo->bmiHeader.biPlanes        = bmp.bmPlanes;
        pBmpInfo->bmiHeader.biBitCount      = bmp.bmBitsPixel;
        pBmpInfo->bmiHeader.biCompression   = BI_RGB;
        pBmpInfo->bmiHeader.biSizeImage     = 0;
        pBmpInfo->bmiHeader.biXPelsPerMeter = 0;
        pBmpInfo->bmiHeader.biYPelsPerMeter = 0;
        pBmpInfo->bmiHeader.biClrUsed       = 0;
        pBmpInfo->bmiHeader.biClrImportant  = 0;
        for(int i = 0; i < nPltCnt; i++){
            pBmpInfo->bmiColors[i].rgbRed       = pPEntry[i].peRed;
            pBmpInfo->bmiColors[i].rgbGreen     = pPEntry[i].peGreen;
            pBmpInfo->bmiColors[i].rgbBlue      = pPEntry[i].peBlue;
        }
        delete pPEntry;

        //DIBitmapを扱うAPIでも安全に利用できるように、CBitmapをデバイスコンテキストから切り離す。
        pFromBmp->DeleteObject();
    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0005;
    }

    try{
        //-------------------------------------------------------------------------
        //Try BitBlt
        if(bBitBlt){
            //********************************************************************
            bOK = BitBlt(pPrinterCD->m_hDC,
                         nPrinterX, nPrinterY, nWidth, nHeight,
                         pFromDC->m_hDC,
                         nFromX, nFromY, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_BITBLT;
            }

            //********************************************************************
            bOK = StretchBlt(pPrinterCD->m_hDC,
                             nPrinterX, nPrinterY, nWidth, nHeight,
                             pFromDC->m_hDC,
                             nFromX, nFromY, nWidth, nHeight, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_STRETCHBLT;
            }

            //********************************************************************
            bOK = StretchDIBits(pPrinterCD->m_hDC,
                                nPrinterX, nPrinterY, nWidth, nHeight,
                                nFromX, nFromY, nWidth, nHeight,
                                bmp.bmBits, pBmpInfo, DIB_RGB_COLORS, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_STRETCHDIB;
            }

            //********************************************************************
            bOK = SetDIBitsToDevice(pPrinterCD->m_hDC,
                                    nPrinterX,  nPrinterY,  //転送先座標
                                    nWidth,     nHeight,    //転送元長方形の幅
                                    nFromX,     nFromY,     //転送元座標
                                    0,                      //配列内の最初の走査行
                                    nHeight,                //走査行の数
                                    bmp.bmBits, pBmpInfo,
                                    DIB_PAL_COLORS);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return -1;
            }

            //********************************************************************
            //描画に失敗
            delete pBmpInfo;
            delete lpBits;
            return 0;
        }
    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0006;
    }

    try{
        //-------------------------------------------------------------------------
        //Try StretchBlt
        if(bStretchBlt){
            //********************************************************************
            bOK = StretchBlt(pPrinterCD->m_hDC,
                             nPrinterX, nPrinterY, nWidth, nHeight,
                             pFromDC->m_hDC,
                             nFromX, nFromY, nWidth, nHeight, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_STRETCHBLT;
            }

            //********************************************************************
            bOK = StretchDIBits(pPrinterCD->m_hDC,
                                nPrinterX, nPrinterY, nWidth, nHeight,
                                nFromX, nFromY, nWidth, nHeight,
                                bmp.bmBits, pBmpInfo, DIB_RGB_COLORS, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_STRETCHDIB;
            }

            //********************************************************************
            bOK = BitBlt(pPrinterCD->m_hDC,
                         nPrinterX, nPrinterY, nWidth, nHeight,
                         pFromDC->m_hDC,
                         nFromX, nFromY, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_BITBLT;
            }

            //********************************************************************
            bOK = SetDIBitsToDevice(pPrinterCD->m_hDC,
                                    nPrinterX,  nPrinterY,  //転送先座標
                                    nWidth,     nHeight,    //転送元長方形の幅
                                    nFromX,     nFromY,     //転送元座標
                                    0,                      //配列内の最初の走査行
                                    nHeight,                //走査行の数
                                    bmp.bmBits, pBmpInfo,
                                    DIB_PAL_COLORS);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return -1;
            }
            //********************************************************************
            //描画に失敗
            delete pBmpInfo;
            delete lpBits;
            return 0;
        }
    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0007;
    }

    try{
        //-------------------------------------------------------------------------
        //Try StretchDIBits
        if(bStretchDIBits){
            //********************************************************************
            bOK = StretchDIBits(pPrinterCD->m_hDC,
                                nPrinterX, nPrinterY, nWidth, nHeight,
                                nFromX, nFromY, nWidth, nHeight,
                                bmp.bmBits, pBmpInfo, DIB_RGB_COLORS, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_STRETCHDIB;
            }

            //********************************************************************
            bOK = StretchBlt(pPrinterCD->m_hDC,
                             nPrinterX, nPrinterY, nWidth, nHeight,
                             pFromDC->m_hDC,
                             nFromX, nFromY, nWidth, nHeight, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_STRETCHBLT;
            }

            //********************************************************************
            bOK = BitBlt(pPrinterCD->m_hDC,
                         nPrinterX, nPrinterY, nWidth, nHeight,
                         pFromDC->m_hDC,
                         nFromX, nFromY, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_BITBLT;
            }

            //********************************************************************
            bOK = SetDIBitsToDevice(pPrinterCD->m_hDC,
                                    nPrinterX,  nPrinterY,  //転送先座標
                                    nWidth,     nHeight,    //転送元長方形の幅
                                    nFromX,     nFromY,     //転送元座標
                                    0,                      //配列内の最初の走査行
                                    nHeight,                //走査行の数
                                    bmp.bmBits, pBmpInfo,
                                    DIB_PAL_COLORS);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return -1;
            }
            //********************************************************************
            //描画に失敗
            delete pBmpInfo;
            delete lpBits;
            return 0;
        }
    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0008;
    }

    try{
        //-------------------------------------------------------------------------
        //Try SetDIBitsToDevice
        if(bSetDIBitsToDevice){
            //********************************************************************
            bOK = SetDIBitsToDevice(pPrinterCD->m_hDC,
                                    nPrinterX,  nPrinterY,  //転送先座標
                                    nWidth,     nHeight,    //転送元長方形の幅
                                    nFromX,     nFromY,     //転送元座標
                                    0,                      //配列内の最初の走査行
                                    nHeight,                //走査行の数
                                    bmp.bmBits, pBmpInfo,
                                    DIB_PAL_COLORS);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_DIBTODEV;
            }

            //********************************************************************
            bOK = StretchDIBits(pPrinterCD->m_hDC,
                                nPrinterX, nPrinterY, nWidth, nHeight,
                                nFromX, nFromY, nWidth, nHeight,
                                bmp.bmBits, pBmpInfo, DIB_RGB_COLORS, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_STRETCHDIB;
            }

            //********************************************************************
            bOK = StretchBlt(pPrinterCD->m_hDC,
                             nPrinterX, nPrinterY, nWidth, nHeight,
                             pFromDC->m_hDC,
                             nFromX, nFromY, nWidth, nHeight, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_STRETCHBLT;
            }

            //********************************************************************
            bOK = BitBlt(pPrinterCD->m_hDC,
                         nPrinterX, nPrinterY, nWidth, nHeight,
                         pFromDC->m_hDC,
                         nFromX, nFromY, SRCCOPY);
            if(bOK){
                delete pBmpInfo;
                delete lpBits;
                return RC_BITBLT;
            }

            //********************************************************************
            //描画に失敗
            delete pBmpInfo;
            delete lpBits;
            return 0;
        }
    }catch (CException *e){
        ShowLastError(GetLastError());
        char *pError = new char[MAX_PATH];
        e->GetErrorMessage(pError, MAX_PATH);
        AfxMessageBox(pError, MB_OK);
        return CDCPRN_ERROR_0009;
    }

    delete lpBits;
    delete pBmpInfo;
    return 0;
}

 

印刷API呼び出し 
void CHogeDlg::OnEngphgBtnPrint() {

    CPrintDialog cPd(FALSE);
    ::InitPrinterSet(&cPd.m_pd);

    if(cPd.DoModal() == IDCANCEL){
        return;
    }

    CDC cDC;
    int pwd,pht,wid,hit,top,lft;
    CFont cft,*cfs;
    TEXTMETRIC tmt;

    HDC hd=cPd.GetPrinterDC();
    LOGFONT logFont;
    DOCINFO p;

    cDC.Attach(hd);
    cDC.m_bPrinting=TRUE;
    pwd=cDC.GetDeviceCaps(HORZRES);
    pht=cDC.GetDeviceCaps(VERTRES);

    memset(&p,0x00,sizeof(DOCINFO));
    p.cbSize=sizeof(DOCINFO);
    /*lpszDocName メンバはプリンタのジョブの名前を入れます。*/
    p.lpszDocName="Hoge";
    p.lpszOutput=NULL;
    cDC.StartDoc(&p);
    cDC.StartPage();

    memset(&logFont,0,sizeof(LOGFONT));
    logFont.lfCharSet=DEFAULT_CHARSET;
    logFont.lfHeight=pht/(54+18+1);
    logFont.lfWidth=pwd/(51+1)/2;

    /*"Font Name"のところにはフォント名を入れます*/
    lstrcpyn(logFont.lfFaceName, "Font Name", sizeof(logFont.lfFaceName));
    cft.CreateFontIndirect(&logFont);
    cfs=cDC.SelectObject(&cft);

    cDC.GetTextMetrics(&tmt);
    hit=tmt.tmHeight+tmt.tmExternalLeading;
    top=hit/6;
    hit+=top*2;
    wid=tmt.tmAveCharWidth*2;
    lft=wid*4;

    /*用紙のピクセル単位での大きさを取得します*/
    int DivX    = GetDeviceCaps(cDC.m_hDC, HORZRES);
    int DivY    = GetDeviceCaps(cDC.m_hDC, VERTRES);

    /*余白の取得は下記の様に行う
    int iOffX   = GetDeviceCaps(cDC.m_hDC, PHYSICALOFFSETX);
    int iOffY   = GetDeviceCaps(cDC.m_hDC, PHYSICALOFFSETY);
    */

    //-------------------------------------------------------------------------
    //プリンタへ描画した内容を転送するprnDCとprnBmpは印刷する画像。
    BOOL bOK = TRUE;
    LONG lRet = CDCToPRinterCDC(&prnDC, &prnBmp, 0, 0, BmpWidth, BmpHeight,
                                &cDC, 10, hit * 3 + 10);

    switch(lRet){
    /*印刷失敗の時はこのswitchの中にエラー処理を記述しましょう*/
    }

    //-------------------------------------------------------------------------
    cDC.SelectObject(cfs);
    cDC.EndPage();
    cDC.EndDoc();
//  cDC.AbortDoc();
}

 
