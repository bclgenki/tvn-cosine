﻿using System;
using System.Runtime.InteropServices;

namespace Leptonica.Native
{
    internal class DllImports
    {
        #region set up
        private const string zlibDllName = "zlib.dll";
        private const string libPngDllName = "libpng16.dll";
        private const string jpegDllName = "jpeg.dll";
        private const string tiffDllName = "tiff.dll";
        private const string tiffXxDllName = "tiffxx.dll";
        private const string leptonicaDllName = "leptonica-1.74.1.dll";

        private static readonly bool initialised;

        static DllImports()
        {
            if (!initialised)
            {
                var path = string.Format("{0}\\lib", AppDomain.CurrentDomain.BaseDirectory);
                if (Architecture.is64BitProcess)
                {
                    path = string.Format("{0}\\{1}", path, "x64");
                }
                else
                {
                    path = string.Format("{0}\\{1}", path, "x86");
                }

                Architecture.LoadLibrary(string.Format("{0}\\{1}", path, zlibDllName));
                Architecture.LoadLibrary(string.Format("{0}\\{1}", path, libPngDllName));
                Architecture.LoadLibrary(string.Format("{0}\\{1}", path, jpegDllName));
                Architecture.LoadLibrary(string.Format("{0}\\{1}", path, tiffDllName));
                Architecture.LoadLibrary(string.Format("{0}\\{1}", path, tiffXxDllName));
                Architecture.LoadLibrary(string.Format("{0}\\{1}", path, leptonicaDllName));

                initialised = true;
            }
        }

        internal static int pixWriteStreamBmp(object cdata, HandleRef size)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region internal constants
        #region Colors for 32 bpp
        /// <summary>
        /// 24
        /// </summary>
        internal const int L_RED_SHIFT = 8 * (sizeof(uint) - 1 - (int)ColorsFor32Bpp.COLOR_RED);
        /// <summary>
        /// 16
        /// </summary>
        internal const int L_GREEN_SHIFT = 8 * (sizeof(uint) - 1 - (int)ColorsFor32Bpp.COLOR_GREEN);
        /// <summary>
        /// 8
        /// </summary>
        internal const int L_BLUE_SHIFT = 8 * (sizeof(uint) - 1 - (int)ColorsFor32Bpp.COLOR_BLUE);
        /// <summary>
        /// 0
        /// </summary>
        internal const int L_ALPHA_SHIFT = 8 * (sizeof(uint) - 1 - (int)ColorsFor32Bpp.L_ALPHA_CHANNEL);
        #endregion

        #region Perceptual color weigths
        /// <summary>
        /// Percept. weight for red
        /// </summary>
        internal const float L_RED_WEIGHT = 0.3F;
        /// <summary>
        /// Percept. weight for green
        /// </summary>
        internal const float L_GREEN_WEIGHT = 0.5F;
        /// <summary>
        /// Percept. weight for blue
        /// </summary>
        internal const float L_BLUE_WEIGHT = 0.2F;
        #endregion

        #region Standard size of border added around images for special processing
        /// <summary>
        /// pixels, not bits 
        /// </summary>
        internal const int ADDED_BORDER = 32;
        #endregion
        #endregion

        #region adaptmap.c 
        // Clean background to white using background normalization
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixCleanBackgroundToWhite")]
        internal static extern IntPtr pixCleanBackgroundToWhite(HandleRef pixs, HandleRef pixim, HandleRef pixg, float gamma, int blackval, int whiteval);

        // Adaptive background normalization (top-level functions)
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBackgroundNormSimple")]
        internal static extern IntPtr pixBackgroundNormSimple(HandleRef pixs, HandleRef pixim, HandleRef pixg);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBackgroundNorm")]
        internal static extern IntPtr pixBackgroundNorm(HandleRef pixs, HandleRef pixim, HandleRef pixg, int sx, int sy, int thresh, int mincount, int bgval, int smoothx, int smoothy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBackgroundNormMorph")]
        internal static extern IntPtr pixBackgroundNormMorph(HandleRef pixs, HandleRef pixim, int reduction, int size, int bgval);

        // Arrays of inverted background values for normalization (16 bpp)
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBackgroundNormGrayArray")]
        internal static extern int pixBackgroundNormGrayArray(HandleRef pixs, HandleRef pixim, int sx, int sy, int thresh, int mincount, int bgval, int smoothx, int smoothy, out IntPtr ppixd);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBackgroundNormRGBArrays")]
        internal static extern int pixBackgroundNormRGBArrays(HandleRef pixs, HandleRef pixim, HandleRef pixg, int sx, int sy, int thresh, int mincount, int bgval, int smoothx, int smoothy, out IntPtr ppixr, out IntPtr ppixg, out IntPtr ppixb);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBackgroundNormGrayArrayMorph")]
        internal static extern int pixBackgroundNormGrayArrayMorph(HandleRef pixs, HandleRef pixim, int reduction, int size, int bgval, out IntPtr ppixd);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBackgroundNormRGBArraysMorph")]
        internal static extern int pixBackgroundNormRGBArraysMorph(HandleRef pixs, HandleRef pixim, int reduction, int size, int bgval, out IntPtr ppixr, out IntPtr ppixg, out IntPtr ppixb);

        // Measurement of local background
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetBackgroundGrayMap")]
        internal static extern int pixGetBackgroundGrayMap(HandleRef pixs, HandleRef pixim, int sx, int sy, int thresh, int mincount, out IntPtr ppixd);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetBackgroundRGBMap")]
        internal static extern int pixGetBackgroundRGBMap(HandleRef pixs, HandleRef pixim, HandleRef pixg, int sx, int sy, int thresh, int mincount, out IntPtr ppixmr, out IntPtr ppixmg, out IntPtr ppixmb);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetBackgroundGrayMapMorph")]
        internal static extern int pixGetBackgroundGrayMapMorph(HandleRef pixs, HandleRef pixim, int reduction, int size, out IntPtr ppixm);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetBackgroundRGBMapMorph")]
        internal static extern int pixGetBackgroundRGBMapMorph(HandleRef pixs, HandleRef pixim, int reduction, int size, out IntPtr ppixmr, out IntPtr ppixmg, out IntPtr ppixmb);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixFillMapHoles")]
        internal static extern int pixFillMapHoles(HandleRef pix, int nx, int ny, int filltype);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixExtendByReplication")]
        internal static extern IntPtr pixExtendByReplication(HandleRef pixs, int addw, int addh);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSmoothConnectedRegions")]
        internal static extern int pixSmoothConnectedRegions(HandleRef pixs, HandleRef pixm, int factor);

        // Generate inverted background map for each component 
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetInvBackgroundMap")]
        internal static extern IntPtr pixGetInvBackgroundMap(HandleRef pixs, int bgval, int smoothx, int smoothy);

        // Apply inverse background map to image
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixApplyInvBackgroundGrayMap")]
        internal static extern IntPtr pixApplyInvBackgroundGrayMap(HandleRef pixs, HandleRef pixm, int sx, int sy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixApplyInvBackgroundRGBMap")]
        internal static extern IntPtr pixApplyInvBackgroundRGBMap(HandleRef pixs, HandleRef pixmr, HandleRef pixmg, HandleRef pixmb, int sx, int sy);

        // Apply variable map
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixApplyVariableGrayMap")]
        internal static extern IntPtr pixApplyVariableGrayMap(HandleRef pixs, HandleRef pixg, int target);

        // Non-adaptive (global) mapping
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGlobalNormRGB")]
        internal static extern IntPtr pixGlobalNormRGB(HandleRef pixd, HandleRef pixs, int rval, int gval, int bval, int mapval);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGlobalNormNoSatRGB")]
        internal static extern IntPtr pixGlobalNormNoSatRGB(HandleRef pixd, HandleRef pixs, int rval, int gval, int bval, int factor, float rank);

        // Adaptive threshold spread normalization
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixThresholdSpreadNorm")]
        internal static extern int pixThresholdSpreadNorm(HandleRef pixs, int filtertype, int edgethresh, int smoothx, int smoothy, float gamma, int minval, int maxval, int targetthresh, out IntPtr ppixth, out IntPtr ppixb, out IntPtr ppixd);

        // Adaptive background normalization (flexible adaptaption)
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBackgroundNormFlex")]
        internal static extern IntPtr pixBackgroundNormFlex(HandleRef pixs, int sx, int sy, int smoothx, int smoothy, int delta);

        // Adaptive contrast normalization
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixContrastNorm")]
        internal static extern IntPtr pixContrastNorm(HandleRef pixd, HandleRef pixs, int sx, int sy, int mindiff, int smoothx, int smoothy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixMinMaxTiles")]
        internal static extern int pixMinMaxTiles(HandleRef pixs, int sx, int sy, int mindiff, int smoothx, int smoothy, out IntPtr ppixmin, out IntPtr ppixmax);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSetLowContrast")]
        internal static extern int pixSetLowContrast(HandleRef pixs1, HandleRef pixs2, int mindiff);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixLinearTRCTiled")]
        internal static extern IntPtr pixLinearTRCTiled(HandleRef pixd, HandleRef pixs, int sx, int sy, HandleRef pixmin, HandleRef pixmax);
        #endregion

        #region affine.c 
        // Affine (3 pt) image transformation using a sampled (to nearest integer) transform on each dest point
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffineSampledPta")]
        internal static extern IntPtr pixAffineSampledPta(HandleRef pixs, HandleRef ptad, HandleRef ptas, int incolor);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffineSampled")]
        internal static extern IntPtr pixAffineSampled(HandleRef pixs, IntPtr vc, int incolor);

        // Affine (3 pt) image transformation using interpolation (or area mapping) for anti-aliasing images that are 2, 4, or 8 bpp gray, or colormapped, or 32 bpp RGB
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffinePta")]
        internal static extern IntPtr pixAffinePta(HandleRef pixs, HandleRef ptad, HandleRef ptas, int incolor);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffine")]
        internal static extern IntPtr pixAffine(HandleRef pixs, IntPtr vc, int incolor);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffinePtaColor")]
        internal static extern IntPtr pixAffinePtaColor(HandleRef pixs, HandleRef ptad, HandleRef ptas, uint colorval);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffineColor")]
        internal static extern IntPtr pixAffineColor(HandleRef pixs, IntPtr vc, uint colorval);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffinePtaGray")]
        internal static extern IntPtr pixAffinePtaGray(HandleRef pixs, HandleRef ptad, HandleRef ptas, byte grayval);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffineGray")]
        internal static extern IntPtr pixAffineGray(HandleRef pixs, IntPtr vc, byte grayval);

        // Affine transform including alpha (blend) component
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffinePtaWithAlpha")]
        internal static extern IntPtr pixAffinePtaWithAlpha(HandleRef pixs, HandleRef ptad, HandleRef ptas, HandleRef pixg, float fract, int border);

        // Affine coordinate transformation
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "getAffineXformCoeffs")]
        internal static extern int getAffineXformCoeffs(HandleRef ptas, HandleRef ptad, out IntPtr pvc);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "affineInvertXform")]
        internal static extern int affineInvertXform(IntPtr vc, out IntPtr pvci);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "affineXformSampledPt")]
        internal static extern int affineXformSampledPt(IntPtr vc, int x, int y, out int pxp, out int pyp);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "affineXformPt")]
        internal static extern int affineXformPt(IntPtr vc, int x, int y, out float pxp, out float pyp);

        // Interpolation helper functions 
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "linearInterpolatePixelColor")]
        internal static extern int linearInterpolatePixelColor(IntPtr datas, int wpls, int w, int h, float x, float y, uint colorval, out uint pval);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "linearInterpolatePixelGray")]
        internal static extern int linearInterpolatePixelGray(IntPtr datas, int wpls, int w, int h, float x, float y, int grayval, out int pval);

        // Gauss-jordan linear equation solver
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gaussjordan")]
        internal static extern int gaussjordan(IntPtr a, IntPtr b, int n);

        // Affine image transformation using a sequence of  shear/scale/translation operations
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAffineSequential")]
        internal static extern IntPtr pixAffineSequential(HandleRef pixs, HandleRef ptad, HandleRef ptas, int bw, int bh);
        #endregion

        #region affinecompose.c
        // Composable coordinate transforms
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "createMatrix2dTranslate")]
        internal static extern IntPtr createMatrix2dTranslate(float transx, float transy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "createMatrix2dScale")]
        internal static extern IntPtr createMatrix2dScale(float scalex, float scaley);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "createMatrix2dRotate")]
        internal static extern IntPtr createMatrix2dRotate(float xc, float yc, float angle);

        // Special coordinate transforms on pta
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ptaTranslate")]
        internal static extern IntPtr ptaTranslate(HandleRef ptas, float transx, float transy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ptaScale")]
        internal static extern IntPtr ptaScale(HandleRef ptas, float scalex, float scaley);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ptaRotate")]
        internal static extern IntPtr ptaRotate(HandleRef ptas, float xc, float yc, float angle);

        // Special coordinate transforms on boxa
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaTranslate")]
        internal static extern IntPtr boxaTranslate(HandleRef boxas, float transx, float transy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaScale")]
        internal static extern IntPtr boxaScale(HandleRef boxas, float scalex, float scaley);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaRotate")]
        internal static extern IntPtr boxaRotate(HandleRef boxas, float xc, float yc, float angle);

        // General coordinate transform on pta and boxa
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ptaAffineTransform")]
        internal static extern IntPtr ptaAffineTransform(HandleRef ptas, IntPtr mat);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaAffineTransform")]
        internal static extern IntPtr boxaAffineTransform(HandleRef boxas, IntPtr mat);

        // Matrix operations
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_productMatVec")]
        internal static extern int l_productMatVec(IntPtr mat, IntPtr vecs, IntPtr vecd, int size);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_productMat2")]
        internal static extern int l_productMat2(IntPtr mat1, IntPtr mat2, IntPtr matd, int size);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_productMat3")]
        internal static extern int l_productMat3(IntPtr mat1, IntPtr mat2, IntPtr mat3, IntPtr matd, int size);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_productMat4")]
        internal static extern int l_productMat4(IntPtr mat1, IntPtr mat2, IntPtr mat3, IntPtr mat4, IntPtr matd, int size);
        #endregion

        #region arrayaccess.c
        // Access within an array of 32-bit words
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_getDataBit")]
        internal static extern int l_getDataBit(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_setDataBit")]
        internal static extern void l_setDataBit(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_clearDataBit")]
        internal static extern void l_clearDataBit(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_setDataBitVal")]
        internal static extern void l_setDataBitVal(IntPtr line, int n, int val);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_getDataDibit")]
        internal static extern int l_getDataDibit(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_setDataDibit")]
        internal static extern void l_setDataDibit(IntPtr line, int n, int val);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_clearDataDibit")]
        internal static extern void l_clearDataDibit(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_getDataQbit")]
        internal static extern int l_getDataQbit(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_setDataQbit")]
        internal static extern void l_setDataQbit(IntPtr line, int n, int val);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_clearDataQbit")]
        internal static extern void l_clearDataQbit(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_getDataByte")]
        internal static extern int l_getDataByte(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_setDataByte")]
        internal static extern void l_setDataByte(IntPtr line, int n, int val);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_getDataTwoBytes")]
        internal static extern int l_getDataTwoBytes(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_setDataTwoBytes")]
        internal static extern void l_setDataTwoBytes(IntPtr line, int n, int val);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_getDataFourBytes")]
        internal static extern int l_getDataFourBytes(IntPtr line, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_setDataFourBytes")]
        internal static extern void l_setDataFourBytes(IntPtr line, int n, int val);
        #endregion

        #region bardecode.c 
        // Dispatcher
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_getDataBit")]
        internal static extern IntPtr barcodeDispatchDecoder([MarshalAs(UnmanagedType.AnsiBStr)] string barstr, int format, int debugflag);

        // Format Determination
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_getDataBit")]
        internal static extern int barcodeFormatIsSupported(int format);
        #endregion

        #region baseline.c
        // Locate text baselines in an image
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixFindBaselines")]
        internal static extern IntPtr pixFindBaselines(HandleRef pixs, out IntPtr ppta, HandleRef pixadb);

        // Projective transform to remove local skew
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixDeskewLocal")]
        internal static extern IntPtr pixDeskewLocal(HandleRef pixs, int nslices, int redsweep, int redsearch, float sweeprange, float sweepdelta, float minbsdelta);

        // Determine local skew
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetLocalSkewTransform")]
        internal static extern int pixGetLocalSkewTransform(HandleRef pixs, int nslices, int redsweep, int redsearch, float sweeprange, float sweepdelta, float minbsdelta, out IntPtr pptas, out IntPtr pptad);

        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetLocalSkewAngles")]
        internal static extern IntPtr pixGetLocalSkewAngles(HandleRef pixs, int nslices, int redsweep, int redsearch, float sweeprange, float sweepdelta, float minbsdelta, out float pa, out float pb, int debug);
        #endregion

        #region bbuffer.c
        // Create/Destroy BBuffer
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferCreate")]
        internal static extern IntPtr bbufferCreate(IntPtr indata, int nalloc);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferDestroy")]
        internal static extern void bbufferDestroy(ref IntPtr pbb);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferDestroyAndSaveData")]
        internal static extern IntPtr bbufferDestroyAndSaveData(ref IntPtr pbb, IntPtr pnbytes);

        // Operations to read data TO a BBuffer
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferRead")]
        internal static extern int bbufferRead(HandleRef bb, IntPtr src, int nbytes);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferReadStream")]
        internal static extern int bbufferReadStream(HandleRef bb, IntPtr fp, int nbytes);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferExtendArray")]
        internal static extern int bbufferExtendArray(HandleRef bb, int nbytes);

        // Operations to write data FROM a BBuffer
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferWrite")]
        internal static extern int bbufferWrite(HandleRef bb, IntPtr dest, IntPtr nbytes, IntPtr pnout);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferWriteStream")]
        internal static extern int bbufferWriteStream(HandleRef bb, IntPtr fp, IntPtr nbytes, IntPtr pnout);
        #endregion

        #region bilateral.c
        // Top level approximate separable grayscale or color bilateral filtering
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferCreate")]
        internal static extern IntPtr pixBilateral(HandleRef pixs, float spatial_stdev, float range_stdev, int ncomps, int reduction);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferCreate")]
        internal static extern IntPtr pixBilateralGray(HandleRef pixs, float spatial_stdev, float range_stdev, int ncomps, int reduction);

        // Slow, exact implementation of grayscale or color bilateral filtering
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferCreate")]
        internal static extern IntPtr pixBilateralExact(HandleRef pixs, HandleRef spatial_kel, HandleRef range_kel);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferCreate")]
        internal static extern IntPtr pixBilateralGrayExact(HandleRef pixs, HandleRef spatial_kel, HandleRef range_kel);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferCreate")]
        internal static extern IntPtr pixBlockBilateralExact(HandleRef pixs, float spatial_stdev, float range_stdev);

        // Kernel helper function
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbufferCreate")]
        internal static extern IntPtr makeRangeKernel(float range_stdev);
        #endregion

        #region bilinear.c
        // Bilinear (4 pt) image transformation using a sampled (to nearest integer) transform on each dest point
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinearSampledPta")]
        internal static extern IntPtr pixBilinearSampledPta(HandleRef pixs, HandleRef ptad, HandleRef ptas, int incolor);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinearSampled")]
        internal static extern IntPtr pixBilinearSampled(HandleRef pixs, IntPtr vc, int incolor);

        // Bilinear (4 pt) image transformation using interpolation (or area mapping) for anti-aliasing images that are 2, 4, or 8 bpp gray, or colormapped, or 32 bpp RGB
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinearPta")]
        internal static extern IntPtr pixBilinearPta(HandleRef pixs, HandleRef ptad, HandleRef ptas, int incolor);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinear")]
        internal static extern IntPtr pixBilinear(HandleRef pixs, IntPtr vc, int incolor);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinearPtaColor")]
        internal static extern IntPtr pixBilinearPtaColor(HandleRef pixs, HandleRef ptad, HandleRef ptas, uint colorval);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinearColor")]
        internal static extern IntPtr pixBilinearColor(HandleRef pixs, IntPtr vc, uint colorval);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinearPtaGray")]
        internal static extern IntPtr pixBilinearPtaGray(HandleRef pixs, HandleRef ptad, HandleRef ptas, byte grayval);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinearGray")]
        internal static extern IntPtr pixBilinearGray(HandleRef pixs, IntPtr vc, byte grayval);

        // Bilinear transform including alpha (blend) component
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBilinearPtaWithAlpha")]
        internal static extern IntPtr pixBilinearPtaWithAlpha(HandleRef pixs, HandleRef ptad, HandleRef ptas, HandleRef pixg, float fract, int border);

        // Bilinear coordinate transformation
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "getBilinearXformCoeffs")]
        internal static extern int getBilinearXformCoeffs(HandleRef ptas, HandleRef ptad, out IntPtr pvc);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bilinearXformSampledPt")]
        internal static extern int bilinearXformSampledPt(IntPtr vc, int x, int y, out int pxp, out int pyp);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bilinearXformPt")]
        internal static extern int bilinearXformPt(IntPtr vc, int x, int y, out float pxp, out float pyp);
        #endregion

        #region binarize.c 
        // Adaptive Otsu-based thresholding
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixOtsuAdaptiveThreshold")]
        internal static extern int pixOtsuAdaptiveThreshold(HandleRef pixs, int sx, int sy, int smoothx, int smoothy, float scorefract, out IntPtr ppixth, out IntPtr ppixd);

        // Otsu thresholding on adaptive background normalization
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixOtsuThreshOnBackgroundNorm")]
        internal static extern IntPtr pixOtsuThreshOnBackgroundNorm(HandleRef pixs, HandleRef pixim, int sx, int sy, int thresh, int mincount, int bgval, int smoothx, int smoothy, float scorefract, out int pthresh);

        // Masking and Otsu estimate on adaptive background normalization
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixMaskedThreshOnBackgroundNorm")]
        internal static extern IntPtr pixMaskedThreshOnBackgroundNorm(HandleRef pixs, HandleRef pixim, int sx, int sy, int thresh, int mincount, int smoothx, int smoothy, float scorefract, out int pthresh);

        // Sauvola local thresholding
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSauvolaBinarizeTiled")]
        internal static extern int pixSauvolaBinarizeTiled(HandleRef pixs, int whsize, float factor, int nx, int ny, out IntPtr ppixth, out IntPtr ppixd);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSauvolaBinarize")]
        internal static extern int pixSauvolaBinarize(HandleRef pixs, int whsize, float factor, int addborder, out IntPtr ppixm, out IntPtr ppixsd, out IntPtr ppixth, out IntPtr ppixd);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSauvolaGetThreshold")]
        internal static extern IntPtr pixSauvolaGetThreshold(HandleRef pixm, HandleRef pixms, float factor, out IntPtr ppixsd);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixApplyLocalThreshold")]
        internal static extern IntPtr pixApplyLocalThreshold(HandleRef pixs, HandleRef pixth, int redfactor);

        // Thresholding using connected components
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixThresholdByConnComp")]
        internal static extern int pixThresholdByConnComp(HandleRef pixs, HandleRef pixm, int start, int end, int incr, float thresh48, float threshdiff, out int pglobthresh, out IntPtr ppixd, int debugflag);
        #endregion

        #region binexpand.c
        // Replicated expansion (integer scaling)
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixExpandBinaryReplicate")]
        internal static extern IntPtr pixExpandBinaryReplicate(HandleRef pixs, int xfact, int yfact);

        // Special case: power of 2 replicated expansion
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixExpandBinaryPower2")]
        internal static extern IntPtr pixExpandBinaryPower2(HandleRef pixs, int factor);
        #endregion

        #region binreduce.c
        // Subsampled 2x reduction
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixExpandBinaryPower2")]
        internal static extern IntPtr pixReduceBinary2(HandleRef pixs, IntPtr intab);

        // Rank filtered 2x reductions
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixExpandBinaryPower2")]
        internal static extern IntPtr pixReduceRankBinaryCascade(HandleRef pixs, int level1, int level2, int level3, int level4);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixExpandBinaryPower2")]
        internal static extern IntPtr pixReduceRankBinary2(HandleRef pixs, int level, IntPtr intab);

        // Permutation table for 2x rank binary reduction
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixExpandBinaryPower2")]
        internal static extern IntPtr makeSubsampleTab2x(IntPtr v);
        #endregion

        #region blend.c 
        // Blending two images that are not colormapped 
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlend")]
        internal static extern IntPtr pixBlend(HandleRef pixs1, HandleRef pixs2, int x, int y, float fract);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendMask")]
        internal static extern IntPtr pixBlendMask(HandleRef pixd, HandleRef pixs1, HandleRef pixs2, int x, int y, float fract, int type);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendGray")]
        internal static extern IntPtr pixBlendGray(HandleRef pixd, HandleRef pixs1, HandleRef pixs2, int x, int y, float fract, int type, int transparent, uint transpix);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendGrayInverse")]
        internal static extern IntPtr pixBlendGrayInverse(HandleRef pixd, HandleRef pixs1, HandleRef pixs2, int x, int y, float fract);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendColor")]
        internal static extern IntPtr pixBlendColor(HandleRef pixd, HandleRef pixs1, HandleRef pixs2, int x, int y, float fract, int transparent, uint transpix);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendColorByChannel")]
        internal static extern IntPtr pixBlendColorByChannel(HandleRef pixd, HandleRef pixs1, HandleRef pixs2, int x, int y, float rfract, float gfract, float bfract, int transparent, uint transpix);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendGrayAdapt")]
        internal static extern IntPtr pixBlendGrayAdapt(HandleRef pixd, HandleRef pixs1, HandleRef pixs2, int x, int y, float fract, int shift);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixFadeWithGray")]
        internal static extern IntPtr pixFadeWithGray(HandleRef pixs, HandleRef pixb, float factor, int type);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendHardLight")]
        internal static extern IntPtr pixBlendHardLight(HandleRef pixd, HandleRef pixs1, HandleRef pixs2, int x, int y, float fract);

        // Blending two colormapped images
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendCmap")]
        internal static extern int pixBlendCmap(HandleRef pixs, HandleRef pixb, int x, int y, int sindex);

        // Blending two images using a third (alpha mask)
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendWithGrayMask")]
        internal static extern IntPtr pixBlendWithGrayMask(HandleRef pixs1, HandleRef pixs2, HandleRef pixg, int x, int y);

        // Blending background to a specific color
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendBackgroundToColor")]
        internal static extern IntPtr pixBlendBackgroundToColor(HandleRef pixd, HandleRef pixs, HandleRef box, uint color, float gamma, int minval, int maxval);

        // Multiplying by a specific color
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixMultiplyByColor")]
        internal static extern IntPtr pixMultiplyByColor(HandleRef pixd, HandleRef pixs, HandleRef box, uint color);

        // Rendering with alpha blending over a uniform background
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAlphaBlendUniform")]
        internal static extern IntPtr pixAlphaBlendUniform(HandleRef pixs, uint color);

        // Adding an alpha layer for blending
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixAddAlphaToBlend")]
        internal static extern IntPtr pixAddAlphaToBlend(HandleRef pixs, float fract, int invert);

        // Setting a transparent alpha component over a white background
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSetAlphaOverWhite")]
        internal static extern IntPtr pixSetAlphaOverWhite(HandleRef pixs);
        #endregion

        #region bmf.c 
        // Acquisition and generation of bitmap fonts. 
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bmfCreate")]
        internal static extern IntPtr bmfCreate([MarshalAs(UnmanagedType.AnsiBStr)] string dir, int fontsize);

        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bmfDestroy")]
        internal static extern void bmfDestroy(ref IntPtr pbmf);

        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bmfGetPix")]
        internal static extern IntPtr bmfGetPix(HandleRef bmf, char chr);

        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bmfGetWidth")]
        internal static extern int bmfGetWidth(HandleRef bmf, char chr, out int pw);

        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bmfGetBaseline")]
        internal static extern int bmfGetBaseline(HandleRef bmf, char chr, out int pbaseline);

        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixaGetFont")]
        internal static extern IntPtr pixaGetFont([MarshalAs(UnmanagedType.AnsiBStr)] string dir, int fontsize, out int pbl0, out int pbl1, out int pbl2);

        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixaSaveFont")]
        internal static extern int pixaSaveFont([MarshalAs(UnmanagedType.AnsiBStr)] string indir, [MarshalAs(UnmanagedType.AnsiBStr)] string outdir, int fontsize);

        #endregion

        #region bmpio.c
        // Read bmp
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixReadStreamBmp")]
        internal static extern IntPtr pixReadStreamBmp(IntPtr fp);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixReadMemBmp")]
        internal static extern IntPtr pixReadMemBmp(IntPtr cdata, IntPtr size);

        // Write bmp
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixWriteStreamBmp")]
        internal static extern int pixWriteStreamBmp(IntPtr fp, HandleRef pix);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixWriteMemBmp")]
        internal static extern int pixWriteMemBmp(out IntPtr pfdata, out IntPtr pfsize, HandleRef pixs);
        #endregion

        #region bootnumgen1.c
        // Auto-generated deserializer
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_bootnum_gen1")]
        internal static extern IntPtr l_bootnum_gen1(IntPtr vo);
        #endregion

        #region bootnumgen2.c
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_bootnum_gen2")]
        internal static extern IntPtr l_bootnum_gen2(IntPtr vo);
        #endregion

        #region bootnumgen3.c
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_bootnum_gen3")]
        internal static extern IntPtr l_bootnum_gen3(IntPtr vo);
        #endregion

        #region boxbasic.c 
        // Box creation, copy, clone, destruction
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxCreate")]
        internal static extern IntPtr boxCreate(int x, int y, int w, int h);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxCreateValid")]
        internal static extern IntPtr boxCreateValid(int x, int y, int w, int h);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxCopy")]
        internal static extern IntPtr boxCopy(HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxClone")]
        internal static extern IntPtr boxClone(HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxDestroy")]
        internal static extern void boxDestroy(ref IntPtr pbox);

        // Box accessors
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxGetGeometry")]
        internal static extern int boxGetGeometry(HandleRef box, out int px, out int py, out int pw, out int ph);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxSetGeometry")]
        internal static extern int boxSetGeometry(HandleRef box, int x, int y, int w, int h);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxGetSideLocations")]
        internal static extern int boxGetSideLocations(HandleRef box, out int pl, out int pr, out int pt, out int pb);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxSetSideLocations")]
        internal static extern int boxSetSideLocations(HandleRef box, int l, int r, int t, int b);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxGetRefcount")]
        internal static extern int boxGetRefcount(HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxChangeRefcount")]
        internal static extern int boxChangeRefcount(HandleRef box, int delta);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxIsValid")]
        internal static extern int boxIsValid(HandleRef box, out int pvalid);

        // Boxa creation, copy, destruction
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaCreate")]
        internal static extern IntPtr boxaCreate(int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaCopy")]
        internal static extern IntPtr boxaCopy(HandleRef boxa, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaDestroy")]
        internal static extern void boxaDestroy(ref IntPtr pboxa);

        // Boxa array extension
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaAddBox")]
        internal static extern int boxaAddBox(HandleRef boxa, HandleRef box, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaExtendArray")]
        internal static extern int boxaExtendArray(HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaExtendArrayToSize")]
        internal static extern int boxaExtendArrayToSize(HandleRef boxa, int size);

        // Boxa accessors
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetCount")]
        internal static extern int boxaGetCount(HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetValidCount")]
        internal static extern int boxaGetValidCount(HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetBox")]
        internal static extern IntPtr boxaGetBox(HandleRef boxa, int index, int accessflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetValidBox")]
        internal static extern IntPtr boxaGetValidBox(HandleRef boxa, int index, int accessflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaFindInvalidBoxes")]
        internal static extern IntPtr boxaFindInvalidBoxes(HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetBoxGeometry")]
        internal static extern int boxaGetBoxGeometry(HandleRef boxa, int index, out int px, out int py, out int pw, out int ph);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaIsFull")]
        internal static extern int boxaIsFull(HandleRef boxa, out int pfull);

        // Boxa array modifiers
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaReplaceBox")]
        internal static extern int boxaReplaceBox(HandleRef boxa, int index, HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaInsertBox")]
        internal static extern int boxaInsertBox(HandleRef boxa, int index, HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaRemoveBox")]
        internal static extern int boxaRemoveBox(HandleRef boxa, int index);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaRemoveBoxAndSave")]
        internal static extern int boxaRemoveBoxAndSave(HandleRef boxa, int index, out IntPtr pbox);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSaveValid")]
        internal static extern IntPtr boxaSaveValid(HandleRef boxas, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaInitFull")]
        internal static extern int boxaInitFull(HandleRef boxa, HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaClear")]
        internal static extern int boxaClear(HandleRef boxa);

        // Boxaa creation, copy, destruction
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaCreate")]
        internal static extern IntPtr boxaaCreate(int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaCopy")]
        internal static extern IntPtr boxaaCopy(HandleRef baas, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaDestroy")]
        internal static extern void boxaaDestroy(ref IntPtr pbaa);

        // Boxaa array extension
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaAddBoxa")]
        internal static extern int boxaaAddBoxa(HandleRef baa, HandleRef ba, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaExtendArray")]
        internal static extern int boxaaExtendArray(HandleRef baa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaExtendArrayToSize")]
        internal static extern int boxaaExtendArrayToSize(HandleRef baa, int size);

        // Boxaa accessors
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaGetCount")]
        internal static extern int boxaaGetCount(HandleRef baa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaGetBoxCount")]
        internal static extern int boxaaGetBoxCount(HandleRef baa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaGetBoxa")]
        internal static extern IntPtr boxaaGetBoxa(HandleRef baa, int index, int accessflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaGetBox")]
        internal static extern IntPtr boxaaGetBox(HandleRef baa, int iboxa, int ibox, int accessflag);

        // Boxaa array modifiers
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaInitFull")]
        internal static extern int boxaaInitFull(HandleRef baa, HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaExtendWithInit")]
        internal static extern int boxaaExtendWithInit(HandleRef baa, int maxindex, HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaReplaceBoxa")]
        internal static extern int boxaaReplaceBoxa(HandleRef baa, int index, HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaInsertBoxa")]
        internal static extern int boxaaInsertBoxa(HandleRef baa, int index, HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaRemoveBoxa")]
        internal static extern int boxaaRemoveBoxa(HandleRef baa, int index);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaAddBox")]
        internal static extern int boxaaAddBox(HandleRef baa, int index, HandleRef box, int accessflag);

        // Boxaa serialized I/O
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaReadFromFiles")]
        internal static extern IntPtr boxaaReadFromFiles([MarshalAs(UnmanagedType.AnsiBStr)] string dirname, [MarshalAs(UnmanagedType.AnsiBStr)] string substr, int first, int nfiles);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaRead")]
        internal static extern IntPtr boxaaRead([MarshalAs(UnmanagedType.AnsiBStr)] string filename);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaReadStream")]
        internal static extern IntPtr boxaaReadStream(IntPtr fp);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaReadMem")]
        internal static extern IntPtr boxaaReadMem(IntPtr data, IntPtr size);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaWrite")]
        internal static extern int boxaaWrite([MarshalAs(UnmanagedType.AnsiBStr)] string filename, HandleRef baa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaWriteStream")]
        internal static extern int boxaaWriteStream(IntPtr fp, HandleRef baa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaWriteMem")]
        internal static extern int boxaaWriteMem(IntPtr pdata, IntPtr psize, HandleRef baa);

        // Boxa serialized I/O
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaRead")]
        internal static extern IntPtr boxaRead([MarshalAs(UnmanagedType.AnsiBStr)] string filename);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaReadStream")]
        internal static extern IntPtr boxaReadStream(IntPtr fp);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaReadMem")]
        internal static extern IntPtr boxaReadMem(IntPtr data, IntPtr size);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaWrite")]
        internal static extern int boxaWrite([MarshalAs(UnmanagedType.AnsiBStr)] string filename, HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaWriteStream")]
        internal static extern int boxaWriteStream(IntPtr fp, HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaWriteMem")]
        internal static extern int boxaWriteMem(IntPtr pdata, IntPtr psize, HandleRef boxa);

        // Box print (for debug)
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxPrintStreamInfo")]
        internal static extern int boxPrintStreamInfo(IntPtr fp, HandleRef box);
        #endregion

        #region boxfunc1.c 
        //  Box geometry
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxContains")]
        internal static extern int boxContains(HandleRef box1, HandleRef box2, out int presult);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxIntersects")]
        internal static extern int boxIntersects(HandleRef box1, HandleRef box2, out int presult);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaContainedInBox")]
        internal static extern IntPtr boxaContainedInBox(HandleRef boxas, HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaContainedInBoxCount")]
        internal static extern int boxaContainedInBoxCount(HandleRef boxa, HandleRef box, out int pcount);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaContainedInBoxa")]
        internal static extern int boxaContainedInBoxa(HandleRef boxa1, HandleRef boxa2, out int pcontained);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaIntersectsBox")]
        internal static extern IntPtr boxaIntersectsBox(HandleRef boxas, HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaIntersectsBoxCount")]
        internal static extern int boxaIntersectsBoxCount(HandleRef boxa, HandleRef box, out int pcount);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaClipToBox")]
        internal static extern IntPtr boxaClipToBox(HandleRef boxas, HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaCombineOverlaps")]
        internal static extern IntPtr boxaCombineOverlaps(HandleRef boxas, HandleRef pixadb);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaCombineOverlapsInPair")]
        internal static extern int boxaCombineOverlapsInPair(HandleRef boxas1, HandleRef boxas2, out IntPtr pboxad1, out IntPtr pboxad2, HandleRef pixadb);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxOverlapRegion")]
        internal static extern IntPtr boxOverlapRegion(HandleRef box1, HandleRef box2);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxBoundingRegion")]
        internal static extern IntPtr boxBoundingRegion(HandleRef box1, HandleRef box2);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxOverlapFraction")]
        internal static extern int boxOverlapFraction(HandleRef box1, HandleRef box2, out float pfract);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxOverlapArea")]
        internal static extern int boxOverlapArea(HandleRef box1, HandleRef box2, out int parea);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaHandleOverlaps")]
        internal static extern IntPtr boxaHandleOverlaps(HandleRef boxas, int op, int range, float min_overlap, float max_ratio, out IntPtr pnamap);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxSeparationDistance")]
        internal static extern int boxSeparationDistance(HandleRef box1, HandleRef box2, out int ph_sep, out int pv_sep);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxCompareSize")]
        internal static extern int boxCompareSize(HandleRef box1, HandleRef box2, int type, out int prel);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxContainsPt")]
        internal static extern int boxContainsPt(HandleRef box, float x, float y, out int pcontains);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetNearestToPt")]
        internal static extern IntPtr boxaGetNearestToPt(HandleRef boxa, int x, int y);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetNearestToLine")]
        internal static extern IntPtr boxaGetNearestToLine(HandleRef boxa, int x, int y);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxGetCenter")]
        internal static extern int boxGetCenter(HandleRef box, out float pcx, out float pcy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxIntersectByLine")]
        internal static extern int boxIntersectByLine(HandleRef box, int x, int y, float slope, out int px1, out int py1, out int px2, out int py2, out int pn);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxClipToRectangle")]
        internal static extern IntPtr boxClipToRectangle(HandleRef box, int wi, int hi);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxClipToRectangleParams")]
        internal static extern int boxClipToRectangleParams(HandleRef box, int w, int h, out int pxstart, out int pystart, out int pxend, out int pyend, out int pbw, out int pbh);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxRelocateOneSide")]
        internal static extern IntPtr boxRelocateOneSide(HandleRef boxd, HandleRef boxs, int loc, int sideflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaAdjustSides")]
        internal static extern IntPtr boxaAdjustSides(HandleRef boxas, int delleft, int delright, int deltop, int delbot);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxAdjustSides")]
        internal static extern IntPtr boxAdjustSides(HandleRef boxd, HandleRef boxs, int delleft, int delright, int deltop, int delbot);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSetSide")]
        internal static extern IntPtr boxaSetSide(HandleRef boxad, HandleRef boxas, int side, int val, int thresh);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaAdjustWidthToTarget")]
        internal static extern IntPtr boxaAdjustWidthToTarget(HandleRef boxad, HandleRef boxas, int sides, int target, int thresh);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaAdjustHeightToTarget")]
        internal static extern IntPtr boxaAdjustHeightToTarget(HandleRef boxad, HandleRef boxas, int sides, int target, int thresh);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxEqual")]
        internal static extern int boxEqual(HandleRef box1, HandleRef box2, out int psame);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaEqual")]
        internal static extern int boxaEqual(HandleRef boxa1, HandleRef boxa2, int maxdist, out IntPtr pnaindex, out int psame);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxSimilar")]
        internal static extern int boxSimilar(HandleRef box1, HandleRef box2, int leftdiff, int rightdiff, int topdiff, int botdiff, out int psimilar);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSimilar")]
        internal static extern int boxaSimilar(HandleRef boxa1, HandleRef boxa2, int leftdiff, int rightdiff, int topdiff, int botdiff, int debug, out int psimilar, out IntPtr pnasim);

        // Boxa combine and split
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaJoin")]
        internal static extern int boxaJoin(HandleRef boxad, HandleRef boxas, int istart, int iend);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaJoin")]
        internal static extern int boxaaJoin(HandleRef baad, HandleRef baas, int istart, int iend);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSplitEvenOdd")]
        internal static extern int boxaSplitEvenOdd(HandleRef boxa, int fillflag, out IntPtr pboxae, out IntPtr pboxao);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaMergeEvenOdd")]
        internal static extern IntPtr boxaMergeEvenOdd(HandleRef boxae, HandleRef boxao, int fillflag);
        #endregion

        #region boxfunc2.c 
        // Boxa/Box transform(shift, scale) and orthogonal rotation
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaTransform")]
        internal static extern IntPtr boxaTransform(HandleRef boxas, int shiftx, int shifty, float scalex, float scaley);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxTransform")]
        internal static extern IntPtr boxTransform(HandleRef box, int shiftx, int shifty, float scalex, float scaley);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaTransformOrdered")]
        internal static extern IntPtr boxaTransformOrdered(HandleRef boxas, int shiftx, int shifty, float scalex, float scaley, int xcen, int ycen, float angle, int order);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxTransformOrdered")]
        internal static extern IntPtr boxTransformOrdered(HandleRef boxs, int shiftx, int shifty, float scalex, float scaley, int xcen, int ycen, float angle, int order);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaRotateOrth")]
        internal static extern IntPtr boxaRotateOrth(HandleRef boxas, int w, int h, int rotation);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxRotateOrth")]
        internal static extern IntPtr boxRotateOrth(HandleRef box, int w, int h, int rotation);

        // Boxa sort
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSort")]
        internal static extern IntPtr boxaSort(HandleRef boxas, int sorttype, int sortorder, out IntPtr pnaindex);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaBinSort")]
        internal static extern IntPtr boxaBinSort(HandleRef boxas, int sorttype, int sortorder, out IntPtr pnaindex);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSortByIndex")]
        internal static extern IntPtr boxaSortByIndex(HandleRef boxas, HandleRef naindex);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSort2d")]
        internal static extern IntPtr boxaSort2d(HandleRef boxas, out IntPtr pnaad, int delta1, int delta2, int minh1);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSort2dByIndex")]
        internal static extern IntPtr boxaSort2dByIndex(HandleRef boxas, HandleRef naa);

        // Boxa statistics
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetRankVals")]
        internal static extern int boxaGetRankVals(HandleRef boxa, float fract, out int px, out int py, out int pw, out int ph);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetMedianVals")]
        internal static extern int boxaGetMedianVals(HandleRef boxa, out int px, out int py, out int pw, out int ph);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetAverageSize")]
        internal static extern int boxaGetAverageSize(HandleRef boxa, out float pw, out float ph);

        // Boxa array extraction
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaExtractAsNuma")]
        internal static extern int boxaExtractAsNuma(HandleRef boxa, out IntPtr pnal, out IntPtr pnat, out IntPtr pnar, out IntPtr pnab, out IntPtr pnaw, out IntPtr pnah, int keepinvalid);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaExtractAsPta")]
        internal static extern int boxaExtractAsPta(HandleRef boxa, out IntPtr pptal, out IntPtr pptat, out IntPtr pptar, out IntPtr pptab, out IntPtr pptaw, out IntPtr pptah, int keepinvalid);

        //Other Boxaa functions
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaGetExtent")]
        internal static extern int boxaaGetExtent(HandleRef baa, out int pw, out int ph, out IntPtr pbox, out IntPtr pboxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaFlattenToBoxa")]
        internal static extern IntPtr boxaaFlattenToBoxa(HandleRef baa, out IntPtr pnaindex, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaFlattenAligned")]
        internal static extern IntPtr boxaaFlattenAligned(HandleRef baa, int num, HandleRef fillerbox, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaEncapsulateAligned")]
        internal static extern IntPtr boxaEncapsulateAligned(HandleRef boxa, int num, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaTranspose")]
        internal static extern IntPtr boxaaTranspose(HandleRef baas);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaAlignBox")]
        internal static extern int boxaaAlignBox(HandleRef baa, HandleRef box, int delta, out int pindex);
        #endregion

        #region boxfunc3.c
        // Boxa/Boxaa painting into pix
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixMaskConnComp")]
        internal static extern IntPtr pixMaskConnComp(HandleRef pixs, int connectivity, out IntPtr pboxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixMaskBoxa")]
        internal static extern IntPtr pixMaskBoxa(HandleRef pixd, HandleRef pixs, HandleRef boxa, int op);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixPaintBoxa")]
        internal static extern IntPtr pixPaintBoxa(HandleRef pixs, HandleRef boxa, uint val);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSetBlackOrWhiteBoxa")]
        internal static extern IntPtr pixSetBlackOrWhiteBoxa(HandleRef pixs, HandleRef boxa, int op);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixPaintBoxaRandom")]
        internal static extern IntPtr pixPaintBoxaRandom(HandleRef pixs, HandleRef boxa);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixBlendBoxaRandom")]
        internal static extern IntPtr pixBlendBoxaRandom(HandleRef pixs, HandleRef boxa, float fract);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixDrawBoxa")]
        internal static extern IntPtr pixDrawBoxa(HandleRef pixs, HandleRef boxa, int width, uint val);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixDrawBoxaRandom")]
        internal static extern IntPtr pixDrawBoxaRandom(HandleRef pixs, HandleRef boxa, int width);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaDisplay")]
        internal static extern IntPtr boxaaDisplay(HandleRef pixs, HandleRef baa, int linewba, int linewb, uint colorba, uint colorb, int w, int h);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixaDisplayBoxaa")]
        internal static extern IntPtr pixaDisplayBoxaa(HandleRef pixas, HandleRef baa, int colorflag, int width);

        // Split mask components into Boxa
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSplitIntoBoxa")]
        internal static extern IntPtr pixSplitIntoBoxa(HandleRef pixs, int minsum, int skipdist, int delta, int maxbg, int maxcomps, int remainder);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSplitComponentIntoBoxa")]
        internal static extern IntPtr pixSplitComponentIntoBoxa(HandleRef pix, HandleRef box, int minsum, int skipdist, int delta, int maxbg, int maxcomps, int remainder);

        // Represent horizontal or vertical mosaic strips
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "makeMosaicStrips")]
        internal static extern IntPtr makeMosaicStrips(int w, int h, int direction, int size);

        // Comparison between boxa
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaCompareRegions")]
        internal static extern int boxaCompareRegions(HandleRef boxa1, HandleRef boxa2, int areathresh, out int pnsame, out float pdiffarea, out float pdiffxor, out IntPtr ppixdb);

        // Reliable selection of a single large box
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixSelectLargeULComp")]
        internal static extern IntPtr pixSelectLargeULComp(HandleRef pixs, float areaslop, int yslop, int connectivity);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSelectLargeULBox")]
        internal static extern IntPtr boxaSelectLargeULBox(HandleRef boxas, float areaslop, int yslop);
        #endregion

        #region boxfunc4.c
        // Boxa and Boxaa range selection
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSelectRange")]
        internal static extern IntPtr boxaSelectRange(HandleRef boxas, int first, int last, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaSelectRange")]
        internal static extern IntPtr boxaaSelectRange(HandleRef baas, int first, int last, int copyflag);

        // Boxa size selection
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSelectBySize")]
        internal static extern IntPtr boxaSelectBySize(HandleRef boxas, int width, int height, int type, int relation, out int pchanged);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaMakeSizeIndicator")]
        internal static extern IntPtr boxaMakeSizeIndicator(HandleRef boxa, int width, int height, int type, int relation);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSelectByArea")]
        internal static extern IntPtr boxaSelectByArea(HandleRef boxas, int area, int relation, out int pchanged);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaMakeAreaIndicator")]
        internal static extern IntPtr boxaMakeAreaIndicator(HandleRef boxa, int area, int relation);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSelectByWHRatio")]
        internal static extern IntPtr boxaSelectByWHRatio(HandleRef boxas, float ratio, int relation, out int pchanged);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaMakeWHRatioIndicator")]
        internal static extern IntPtr boxaMakeWHRatioIndicator(HandleRef boxa, float ratio, int relation);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSelectWithIndicator")]
        internal static extern IntPtr boxaSelectWithIndicator(HandleRef boxas, HandleRef na, out int pchanged);

        // Boxa permutation
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaPermutePseudorandom")]
        internal static extern IntPtr boxaPermutePseudorandom(HandleRef boxas);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaPermuteRandom")]
        internal static extern IntPtr boxaPermuteRandom(HandleRef boxad, HandleRef boxas);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSwapBoxes")]
        internal static extern int boxaSwapBoxes(HandleRef boxa, int i, int j);

        // Boxa and box conversions
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaConvertToPta")]
        internal static extern IntPtr boxaConvertToPta(HandleRef boxa, int ncorners);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ptaConvertToBoxa")]
        internal static extern IntPtr ptaConvertToBoxa(HandleRef pta, int ncorners);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxConvertToPta")]
        internal static extern IntPtr boxConvertToPta(HandleRef box, int ncorners);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ptaConvertToBox")]
        internal static extern IntPtr ptaConvertToBox(HandleRef pta);

        // Boxa sequence fitting
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSmoothSequenceLS")]
        internal static extern IntPtr boxaSmoothSequenceLS(HandleRef boxas, float factor, int subflag, int maxdiff, int debug);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSmoothSequenceMedian")]
        internal static extern IntPtr boxaSmoothSequenceMedian(HandleRef boxas, int halfwin, int subflag, int maxdiff, int debug);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaLinearFit")]
        internal static extern IntPtr boxaLinearFit(HandleRef boxas, float factor, int debug);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaWindowedMedian")]
        internal static extern IntPtr boxaWindowedMedian(HandleRef boxas, int halfwin, int debug);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaModifyWithBoxa")]
        internal static extern IntPtr boxaModifyWithBoxa(HandleRef boxas, HandleRef boxam, int subflag, int maxdiff);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaConstrainSize")]
        internal static extern IntPtr boxaConstrainSize(HandleRef boxas, int width, int widthflag, int height, int heightflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaReconcileEvenOddHeight")]
        internal static extern IntPtr boxaReconcileEvenOddHeight(HandleRef boxas, int sides, int delh, int op, float factor, int start);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaReconcilePairWidth")]
        internal static extern IntPtr boxaReconcilePairWidth(HandleRef boxas, int delw, int op, float factor, HandleRef na);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaPlotSides")]
        internal static extern int boxaPlotSides(HandleRef boxa, [MarshalAs(UnmanagedType.AnsiBStr)] string plotname, out IntPtr pnal, out IntPtr pnat, out IntPtr pnar, out IntPtr pnab, out IntPtr ppixd);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaPlotSizes")]
        internal static extern int boxaPlotSizes(HandleRef boxa, [MarshalAs(UnmanagedType.AnsiBStr)] string plotname, out IntPtr pnaw, out IntPtr pnah, out IntPtr ppixd);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaFillSequence")]
        internal static extern IntPtr boxaFillSequence(HandleRef boxas, int useflag, int debug);

        // Miscellaneous boxa functions
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetExtent")]
        internal static extern int boxaGetExtent(HandleRef boxa, out int pw, out int ph, out IntPtr pbox);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetCoverage")]
        internal static extern int boxaGetCoverage(HandleRef boxa, int wc, int hc, int exactflag, out float pfract);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaaSizeRange")]
        internal static extern int boxaaSizeRange(HandleRef baa, out int pminw, out int pminh, out int pmaxw, out int pmaxh);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaSizeRange")]
        internal static extern int boxaSizeRange(HandleRef boxa, out int pminw, out int pminh, out int pmaxw, out int pmaxh);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaLocationRange")]
        internal static extern int boxaLocationRange(HandleRef boxa, out int pminx, out int pminy, out int pmaxx, out int pmaxy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetSizes")]
        internal static extern int boxaGetSizes(HandleRef boxa, out IntPtr pnaw, out IntPtr pnah);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaGetArea")]
        internal static extern int boxaGetArea(HandleRef boxa, out int parea);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaDisplayTiled")]
        internal static extern IntPtr boxaDisplayTiled(HandleRef boxas, HandleRef pixa, int maxwidth, int linewidth, float scalefactor, int background, int spacing, int border);
        #endregion

        #region bytearray.c
        // Creation, copy, clone, destruction
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaCreate")]
        internal static extern IntPtr l_byteaCreate(IntPtr nbytes);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaInitFromMem")]
        internal static extern IntPtr l_byteaInitFromMem(IntPtr data, IntPtr size);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaInitFromFile")]
        internal static extern IntPtr l_byteaInitFromFile([MarshalAs(UnmanagedType.AnsiBStr)]  string fname);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaInitFromStream")]
        internal static extern IntPtr l_byteaInitFromStream(IntPtr fp);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaCopy")]
        internal static extern IntPtr l_byteaCopy(HandleRef bas, int copyflag);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaDestroy")]
        internal static extern void l_byteaDestroy(ref IntPtr pba);

        // Accessors
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaGetSize")]
        internal static extern IntPtr l_byteaGetSize(HandleRef ba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaGetData")]
        internal static extern IntPtr l_byteaGetData(HandleRef ba, IntPtr psize);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaCopyData")]
        internal static extern IntPtr l_byteaCopyData(HandleRef ba, IntPtr psize);

        // Appending
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaAppendData")]
        internal static extern int l_byteaAppendData(HandleRef ba, IntPtr newdata, IntPtr newbytes);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaAppendString")]
        internal static extern int l_byteaAppendString(HandleRef ba, [MarshalAs(UnmanagedType.AnsiBStr)]  string str);

        // Join/Split
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaJoin")]
        internal static extern int l_byteaJoin(HandleRef ba1, out IntPtr pba2);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaSplit")]
        internal static extern int l_byteaSplit(HandleRef ba1, IntPtr splitloc, out IntPtr pba2);

        // Search
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaFindEachSequence")]
        internal static extern int l_byteaFindEachSequence(HandleRef ba, IntPtr sequence, int seqlen, out IntPtr pda);

        // Output to file
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaWrite")]
        internal static extern int l_byteaWrite([MarshalAs(UnmanagedType.AnsiBStr)]  string fname, HandleRef ba, IntPtr startloc, IntPtr endloc);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "l_byteaWriteStream")]
        internal static extern int l_byteaWriteStream(IntPtr fp, HandleRef ba, IntPtr startloc, IntPtr endloc);
        #endregion

        #region ccbord.c    
        // CCBORDA and CCBORD creation and destruction
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaCreate")]
        internal static extern IntPtr ccbaCreate(HandleRef pixs, int n);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaDestroy")]
        internal static extern void ccbaDestroy(ref IntPtr pccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbCreate")]
        internal static extern IntPtr ccbCreate(HandleRef pixs);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbDestroy")]
        internal static extern void ccbDestroy(ref IntPtr pccb);

        // CCBORDA addition
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaAddCcb")]
        internal static extern int ccbaAddCcb(HandleRef ccba, HandleRef ccb);

        // CCBORDA accessors
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaGetCount")]
        internal static extern int ccbaGetCount(HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaGetCcb")]
        internal static extern IntPtr ccbaGetCcb(HandleRef ccba, int index);

        // Top-level border-finding routines
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetAllCCBorders")]
        internal static extern IntPtr pixGetAllCCBorders(HandleRef pixs);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetCCBorders")]
        internal static extern IntPtr pixGetCCBorders(HandleRef pixs, HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetOuterBordersPtaa")]
        internal static extern IntPtr pixGetOuterBordersPtaa(HandleRef pixs);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetOuterBorderPta")]
        internal static extern IntPtr pixGetOuterBorderPta(HandleRef pixs, HandleRef box);

        // Lower-level border location routines
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetOuterBorder")]
        internal static extern int pixGetOuterBorder(HandleRef ccb, HandleRef pixs, HandleRef box);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetHoleBorder")]
        internal static extern int pixGetHoleBorder(HandleRef ccb, HandleRef pixs, HandleRef box, int xs, int ys);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "findNextBorderPixel")]
        internal static extern int findNextBorderPixel(int w, int h, IntPtr data, int wpl, int px, int py, out int pqpos, out int pnpx, out int pnpy);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "locateOutsideSeedPixel")]
        internal static extern void locateOutsideSeedPixel(int fpx, int fpy, int spx, int spy, out int pxs, out int pys);

        // Border conversions
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaGenerateGlobalLocs")]
        internal static extern int ccbaGenerateGlobalLocs(HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaGenerateStepChains")]
        internal static extern int ccbaGenerateStepChains(HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaStepChainsToPixCoords")]
        internal static extern int ccbaStepChainsToPixCoords(HandleRef ccba, int coordtype);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaGenerateSPGlobalLocs")]
        internal static extern int ccbaGenerateSPGlobalLocs(HandleRef ccba, int ptsflag);

        // Conversion to single path
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaGenerateSinglePath")]
        internal static extern int ccbaGenerateSinglePath(HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "getCutPathForHole")]
        internal static extern IntPtr getCutPathForHole(HandleRef pix, HandleRef pta, HandleRef boxinner, out int pdir, out int plen);

        // Border and full image rendering
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaDisplayBorder")]
        internal static extern IntPtr ccbaDisplayBorder(HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaDisplaySPBorder")]
        internal static extern IntPtr ccbaDisplaySPBorder(HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaDisplayImage1")]
        internal static extern IntPtr ccbaDisplayImage1(HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaDisplayImage2")]
        internal static extern IntPtr ccbaDisplayImage2(HandleRef ccba);

        // Serialize for I/O
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaWrite")]
        internal static extern int ccbaWrite([MarshalAs(UnmanagedType.AnsiBStr)]  string filename, HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaWriteStream")]
        internal static extern int ccbaWriteStream(IntPtr fp, HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaRead")]
        internal static extern IntPtr ccbaRead([MarshalAs(UnmanagedType.AnsiBStr)]  string filename);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaReadStream")]
        internal static extern IntPtr ccbaReadStream(IntPtr fp);

        // SVG output
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaWriteSVG")]
        internal static extern int ccbaWriteSVG([MarshalAs(UnmanagedType.AnsiBStr)]  string filename, HandleRef ccba);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ccbaWriteSVGString")]
        internal static extern IntPtr ccbaWriteSVGString([MarshalAs(UnmanagedType.AnsiBStr)]  string filename, HandleRef ccba);

        #endregion

        #region ccthin.c
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixaThinConnected")]
        internal static extern IntPtr pixaThinConnected(HandleRef pixas, int type, int connectivity, int maxiters);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixThinConnected")]
        internal static extern IntPtr pixThinConnected(HandleRef pixs, int type, int connectivity, int maxiters);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixThinConnectedBySet")]
        internal static extern IntPtr pixThinConnectedBySet(HandleRef pixs, int type, HandleRef sela, int maxiters);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "selaMakeThinSets")]
        internal static extern IntPtr selaMakeThinSets(int index, int debug);
        #endregion

        #region classapp.c
        // Top-level jb2 correlation and rank-hausdorff 
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jbCorrelation")]
        internal static extern int jbCorrelation([MarshalAs(UnmanagedType.AnsiBStr)]  string dirin, float thresh, float weight, int components, [MarshalAs(UnmanagedType.AnsiBStr)]  string rootname, int firstpage, int npages, int renderflag );
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jbRankHaus")]
        internal static extern int jbRankHaus([MarshalAs(UnmanagedType.AnsiBStr)]  string dirin, int size, float rank, int components, [MarshalAs(UnmanagedType.AnsiBStr)]  string rootname, int firstpage, int npages, int renderflag );

        // Extract and classify words in textline order 
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jbWordsInTextlines")]
        internal static extern IntPtr  jbWordsInTextlines([MarshalAs(UnmanagedType.AnsiBStr)]  string dirin, int reduction, int maxwidth, int maxheight, float thresh, float weight, out IntPtr pnatl, int firstpage, int npages );
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetWordsInTextlines")]
        internal static extern int pixGetWordsInTextlines(HandleRef pixs, int reduction, int minwidth, int minheight, int maxwidth, int maxheight, out IntPtr pboxad, out IntPtr ppixad, out IntPtr pnai);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixGetWordBoxesInTextlines")]
        internal static extern int pixGetWordBoxesInTextlines(HandleRef pixs, int reduction, int minwidth, int minheight, int maxwidth, int maxheight, out IntPtr pboxad, out IntPtr pnai);

        // Use word bounding boxes to compare page images 
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "boxaExtractSortedPattern")]
        internal static extern IntPtr boxaExtractSortedPattern(HandleRef boxa, HandleRef na);
        [DllImport(leptonicaDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "numaaCompareImagesByBoxes")]
        internal static extern int numaaCompareImagesByBoxes(HandleRef naa1, HandleRef naa2, int nperline, int nreq, int maxshiftx, int maxshifty, int delx, int dely, out int psame, int debugflag);
        #endregion

        /* 
         internal static  extern int pixColorContent(PIX* pixs, int rwhite, int gwhite, int bwhite, int mingray, PIX** ppixr, PIX** ppixg, PIX** ppixb);
         internal static  extern PIX* pixColorMagnitude(PIX* pixs, int rwhite, int gwhite, int bwhite, int type);
         internal static  extern PIX* pixMaskOverColorPixels(PIX* pixs, int threshdiff, int mindist);
         internal static  extern PIX* pixMaskOverColorRange(PIX* pixs, int rmin, int rmax, int gmin, int gmax, int bmin, int bmax);
         internal static  extern int pixColorFraction(PIX* pixs, int darkthresh, int lightthresh, int diffthresh, int factor, l_float32* ppixfract, l_float32* pcolorfract);
         internal static  extern int pixFindColorRegions(PIX* pixs, PIX* pixm, int factor, int lightthresh, int darkthresh, int mindiff, int colordiff, float edgefract, l_float32* pcolorfract, PIX** pcolormask1, PIX** pcolormask2, HandleRef pixadb);
         internal static  extern int pixNumSignificantGrayColors(PIX* pixs, int darkthresh, int lightthresh, float minfract, int factor, l_int32* pncolors);
         internal static  extern int pixColorsForQuantization(PIX* pixs, int thresh, l_int32* pncolors, l_int32* piscolor, int debug);
         internal static  extern int pixNumColors(PIX* pixs, int factor, l_int32* pncolors);
         internal static  extern int pixGetMostPopulatedColors(PIX* pixs, int sigbits, int factor, int ncolors, l_uint32** parray, PIXCMAP** pcmap);
         internal static  extern PIX* pixSimpleColorQuantize(PIX* pixs, int sigbits, int factor, int ncolors);
         internal static  extern NUMA* pixGetRGBHistogram(PIX* pixs, int sigbits, int factor);
         internal static  extern int makeRGBIndexTables(l_uint32** prtab, l_uint32** pgtab, l_uint32** pbtab, int sigbits);
         internal static  extern int getRGBFromIndex(l_uint32 index, int sigbits, l_int32* prval, l_int32* pgval, l_int32* pbval);
         internal static  extern int pixHasHighlightRed(PIX* pixs, int factor, float fract, float fthresh, l_int32* phasred, l_float32* pratio, PIX** ppixdb);
         internal static  extern PIX* pixColorGrayRegions(PIX* pixs, HandleRef boxa, int type, int thresh, int rval, int gval, int bval);
         internal static  extern int pixColorGray(PIX* pixs, HandleRef box, int type, int thresh, int rval, int gval, int bval);
         internal static  extern PIX* pixColorGrayMasked(PIX* pixs, PIX* pixm, int type, int thresh, int rval, int gval, int bval);
         internal static  extern PIX* pixSnapColor(PIX* pixd, PIX* pixs, uint srcval, uint dstval, int diff);
         internal static  extern PIX* pixSnapColorCmap(PIX* pixd, PIX* pixs, uint srcval, uint dstval, int diff);
         internal static  extern PIX* pixLinearMapToTargetColor(PIX* pixd, PIX* pixs, uint srcval, uint dstval);
         internal static  extern int pixelLinearMapToTargetColor(l_uint32 scolor, uint srcmap, uint dstmap, l_uint32* pdcolor);
         internal static  extern PIX* pixShiftByComponent(PIX* pixd, PIX* pixs, uint srcval, uint dstval);
         internal static  extern int pixelShiftByComponent(int rval, int gval, int bval, uint srcval, uint dstval, l_uint32* ppixel);
         internal static  extern int pixelFractionalShift(int rval, int gval, int bval, float fraction, l_uint32* ppixel);
         internal static  extern PIXCMAP* pixcmapCreate(int depth);
         internal static  extern PIXCMAP* pixcmapCreateRandom(int depth, int hasblack, int haswhite);
         internal static  extern PIXCMAP* pixcmapCreateLinear(int d, int nlevels);
         internal static  extern PIXCMAP* pixcmapCopy(PIXCMAP* cmaps);
         internal static  extern void pixcmapDestroy(PIXCMAP** pcmap);
         internal static  extern int pixcmapAddColor(PIXCMAP* cmap, int rval, int gval, int bval);
         internal static  extern int pixcmapAddRGBA(PIXCMAP* cmap, int rval, int gval, int bval, int aval);
         internal static  extern int pixcmapAddNewColor(PIXCMAP* cmap, int rval, int gval, int bval, l_int32* pindex);
         internal static  extern int pixcmapAddNearestColor(PIXCMAP* cmap, int rval, int gval, int bval, l_int32* pindex);
         internal static  extern int pixcmapUsableColor(PIXCMAP* cmap, int rval, int gval, int bval, l_int32* pusable);
         internal static  extern int pixcmapAddBlackOrWhite(PIXCMAP* cmap, int color, l_int32* pindex);
         internal static  extern int pixcmapSetBlackAndWhite(PIXCMAP* cmap, int setblack, int setwhite);
         internal static  extern int pixcmapGetCount(PIXCMAP* cmap);
         internal static  extern int pixcmapGetFreeCount(PIXCMAP* cmap);
         internal static  extern int pixcmapGetDepth(PIXCMAP* cmap);
         internal static  extern int pixcmapGetMinDepth(PIXCMAP* cmap, l_int32* pmindepth);
         internal static  extern int pixcmapClear(PIXCMAP* cmap);
         internal static  extern int pixcmapGetColor(PIXCMAP* cmap, int index, l_int32* prval, l_int32* pgval, l_int32* pbval);
         internal static  extern int pixcmapGetColor32(PIXCMAP* cmap, int index, l_uint32* pval32);
         internal static  extern int pixcmapGetRGBA(PIXCMAP* cmap, int index, l_int32* prval, l_int32* pgval, l_int32* pbval, l_int32* paval);
         internal static  extern int pixcmapGetRGBA32(PIXCMAP* cmap, int index, l_uint32* pval32);
         internal static  extern int pixcmapResetColor(PIXCMAP* cmap, int index, int rval, int gval, int bval);
         internal static  extern int pixcmapSetAlpha(PIXCMAP* cmap, int index, int aval);
         internal static  extern int pixcmapGetIndex(PIXCMAP* cmap, int rval, int gval, int bval, l_int32* pindex);
         internal static  extern int pixcmapHasColor(PIXCMAP* cmap, l_int32* pcolor);
         internal static  extern int pixcmapIsOpaque(PIXCMAP* cmap, l_int32* popaque);
         internal static  extern int pixcmapIsBlackAndWhite(PIXCMAP* cmap, l_int32* pblackwhite);
         internal static  extern int pixcmapCountGrayColors(PIXCMAP* cmap, l_int32* pngray);
         internal static  extern int pixcmapGetRankIntensity(PIXCMAP* cmap, float rankval, l_int32* pindex);
         internal static  extern int pixcmapGetNearestIndex(PIXCMAP* cmap, int rval, int gval, int bval, l_int32* pindex);
         internal static  extern int pixcmapGetNearestGrayIndex(PIXCMAP* cmap, int val, l_int32* pindex);
         internal static  extern int pixcmapGetDistanceToColor(PIXCMAP* cmap, int index, int rval, int gval, int bval, l_int32* pdist);
         internal static  extern int pixcmapGetRangeValues(PIXCMAP* cmap, int select, l_int32* pminval, l_int32* pmaxval, l_int32* pminindex, l_int32* pmaxindex);
         internal static  extern PIXCMAP* pixcmapGrayToColor(l_uint32 color);
         internal static  extern PIXCMAP* pixcmapColorToGray(PIXCMAP* cmaps, float rwt, float gwt, float bwt);
         internal static  extern PIXCMAP* pixcmapConvertTo4(PIXCMAP* cmaps);
         internal static  extern PIXCMAP* pixcmapConvertTo8(PIXCMAP* cmaps);
         internal static  extern PIXCMAP* pixcmapRead( const char* filename );
         internal static  extern PIXCMAP* pixcmapReadStream(FILE* fp);
         internal static  extern PIXCMAP* pixcmapReadMem( const l_uint8* data, size_t size );
         internal static  extern int pixcmapWrite( const char* filename, PIXCMAP *cmap );
         internal static  extern int pixcmapWriteStream(FILE* fp, PIXCMAP* cmap);
         internal static  extern int pixcmapWriteMem(l_uint8** pdata, size_t* psize, PIXCMAP* cmap);
         internal static  extern int pixcmapToArrays(PIXCMAP* cmap, l_int32** prmap, l_int32** pgmap, l_int32** pbmap, l_int32** pamap);
         internal static  extern int pixcmapToRGBTable(PIXCMAP* cmap, l_uint32** ptab, l_int32* pncolors);
         internal static  extern int pixcmapSerializeToMemory(PIXCMAP* cmap, int cpc, l_int32* pncolors, l_uint8** pdata);
         internal static  extern PIXCMAP* pixcmapDeserializeFromMemory(l_uint8* data, int cpc, int ncolors);
         internal static  extern char* pixcmapConvertToHex(l_uint8* data, int ncolors);
         internal static  extern int pixcmapGammaTRC(PIXCMAP* cmap, float gamma, int minval, int maxval);
         internal static  extern int pixcmapContrastTRC(PIXCMAP* cmap, float factor);
         internal static  extern int pixcmapShiftIntensity(PIXCMAP* cmap, float fraction);
         internal static  extern int pixcmapShiftByComponent(PIXCMAP* cmap, uint srcval, uint dstval);
         internal static  extern PIX* pixColorMorph(PIX* pixs, int type, int hsize, int vsize);
         internal static  extern PIX* pixOctreeColorQuant(PIX* pixs, int colors, int ditherflag);
         internal static  extern PIX* pixOctreeColorQuantGeneral(PIX* pixs, int colors, int ditherflag, float validthresh, float colorthresh);
         internal static  extern int makeRGBToIndexTables(l_uint32** prtab, l_uint32** pgtab, l_uint32** pbtab, int cqlevels);
         internal static  extern void getOctcubeIndexFromRGB(int rval, int gval, int bval, l_uint32* rtab, l_uint32* gtab, l_uint32* btab, l_uint32* pindex);
         internal static  extern PIX* pixOctreeQuantByPopulation(PIX* pixs, int level, int ditherflag);
         internal static  extern PIX* pixOctreeQuantNumColors(PIX* pixs, int maxcolors, int subsample);
         internal static  extern PIX* pixOctcubeQuantMixedWithGray(PIX* pixs, int depth, int graylevels, int delta);
         internal static  extern PIX* pixFixedOctcubeQuant256(PIX* pixs, int ditherflag);
         internal static  extern PIX* pixFewColorsOctcubeQuant1(PIX* pixs, int level);
         internal static  extern PIX* pixFewColorsOctcubeQuant2(PIX* pixs, int level, NUMA* na, int ncolors, l_int32* pnerrors);
         internal static  extern PIX* pixFewColorsOctcubeQuantMixed(PIX* pixs, int level, int darkthresh, int lightthresh, int diffthresh, float minfract, int maxspan);
         internal static  extern PIX* pixFixedOctcubeQuantGenRGB(PIX* pixs, int level);
         internal static  extern PIX* pixQuantFromCmap(PIX* pixs, PIXCMAP* cmap, int mindepth, int level, int metric);
         internal static  extern PIX* pixOctcubeQuantFromCmap(PIX* pixs, PIXCMAP* cmap, int mindepth, int level, int metric);
         internal static  extern NUMA* pixOctcubeHistogram(PIX* pixs, int level, l_int32* pncolors);
         internal static  extern l_int32* pixcmapToOctcubeLUT(PIXCMAP* cmap, int level, int metric);
         internal static  extern int pixRemoveUnusedColors(PIX* pixs);
         internal static  extern int pixNumberOccupiedOctcubes(PIX* pix, int level, int mincount, float minfract, l_int32* pncolors);
         internal static  extern PIX* pixMedianCutQuant(PIX* pixs, int ditherflag);
         internal static  extern PIX* pixMedianCutQuantGeneral(PIX* pixs, int ditherflag, int outdepth, int maxcolors, int sigbits, int maxsub, int checkbw);
         internal static  extern PIX* pixMedianCutQuantMixed(PIX* pixs, int ncolor, int ngray, int darkthresh, int lightthresh, int diffthresh);
         internal static  extern PIX* pixFewColorsMedianCutQuantMixed(PIX* pixs, int ncolor, int ngray, int maxncolors, int darkthresh, int lightthresh, int diffthresh);
         internal static  extern l_int32* pixMedianCutHisto(PIX* pixs, int sigbits, int subsample);
         internal static  extern PIX* pixColorSegment(PIX* pixs, int maxdist, int maxcolors, int selsize, int finalcolors, int debugflag);
         internal static  extern PIX* pixColorSegmentCluster(PIX* pixs, int maxdist, int maxcolors, int debugflag);
         internal static  extern int pixAssignToNearestColor(PIX* pixd, PIX* pixs, PIX* pixm, int level, l_int32* countarray);
         internal static  extern int pixColorSegmentClean(PIX* pixs, int selsize, l_int32* countarray);
         internal static  extern int pixColorSegmentRemoveColors(PIX* pixd, PIX* pixs, int finalcolors);
         internal static  extern PIX* pixConvertRGBToHSV(PIX* pixd, PIX* pixs);
         internal static  extern PIX* pixConvertHSVToRGB(PIX* pixd, PIX* pixs);
         internal static  extern int convertRGBToHSV(int rval, int gval, int bval, l_int32* phval, l_int32* psval, l_int32* pvval);
         internal static  extern int convertHSVToRGB(int hval, int sval, int vval, l_int32* prval, l_int32* pgval, l_int32* pbval);
         internal static  extern int pixcmapConvertRGBToHSV(PIXCMAP* cmap);
         internal static  extern int pixcmapConvertHSVToRGB(PIXCMAP* cmap);
         internal static  extern PIX* pixConvertRGBToHue(PIX* pixs);
         internal static  extern PIX* pixConvertRGBToSaturation(PIX* pixs);
         internal static  extern PIX* pixConvertRGBToValue(PIX* pixs);
         internal static  extern PIX* pixMakeRangeMaskHS(PIX* pixs, int huecenter, int huehw, int satcenter, int sathw, int regionflag);
         internal static  extern PIX* pixMakeRangeMaskHV(PIX* pixs, int huecenter, int huehw, int valcenter, int valhw, int regionflag);
         internal static  extern PIX* pixMakeRangeMaskSV(PIX* pixs, int satcenter, int sathw, int valcenter, int valhw, int regionflag);
         internal static  extern PIX* pixMakeHistoHS(PIX* pixs, int factor, NUMA** pnahue, NUMA** pnasat);
         internal static  extern PIX* pixMakeHistoHV(PIX* pixs, int factor, NUMA** pnahue, NUMA** pnaval);
         internal static  extern PIX* pixMakeHistoSV(PIX* pixs, int factor, NUMA** pnasat, NUMA** pnaval);
         internal static  extern int pixFindHistoPeaksHSV(PIX* pixs, int type, int width, int height, int npeaks, float erasefactor, PTA** ppta, NUMA** pnatot, PIXA** ppixa);
         internal static  extern PIX* displayHSVColorRange(int hval, int sval, int vval, int huehw, int sathw, int nsamp, int factor);
         internal static  extern PIX* pixConvertRGBToYUV(PIX* pixd, PIX* pixs);
         internal static  extern PIX* pixConvertYUVToRGB(PIX* pixd, PIX* pixs);
         internal static  extern int convertRGBToYUV(int rval, int gval, int bval, l_int32* pyval, l_int32* puval, l_int32* pvval);
         internal static  extern int convertYUVToRGB(int yval, int uval, int vval, l_int32* prval, l_int32* pgval, l_int32* pbval);
         internal static  extern int pixcmapConvertRGBToYUV(PIXCMAP* cmap);
         internal static  extern int pixcmapConvertYUVToRGB(PIXCMAP* cmap);
         internal static  extern FPIXA* pixConvertRGBToXYZ(PIX* pixs);
         internal static  extern PIX* fpixaConvertXYZToRGB(FPIXA* fpixa);
         internal static  extern int convertRGBToXYZ(int rval, int gval, int bval, l_float32* pfxval, l_float32* pfyval, l_float32* pfzval);
         internal static  extern int convertXYZToRGB(float fxval, float fyval, float fzval, int blackout, l_int32* prval, l_int32* pgval, l_int32* pbval);
         internal static  extern FPIXA* fpixaConvertXYZToLAB(FPIXA* fpixas);
         internal static  extern FPIXA* fpixaConvertLABToXYZ(FPIXA* fpixas);
         internal static  extern int convertXYZToLAB(float xval, float yval, float zval, l_float32* plval, l_float32* paval, l_float32* pbval);
         internal static  extern int convertLABToXYZ(float lval, float aval, float bval, l_float32* pxval, l_float32* pyval, l_float32* pzval);
         internal static  extern FPIXA* pixConvertRGBToLAB(PIX* pixs);
         internal static  extern PIX* fpixaConvertLABToRGB(FPIXA* fpixa);
         internal static  extern int convertRGBToLAB(int rval, int gval, int bval, l_float32* pflval, l_float32* pfaval, l_float32* pfbval);
         internal static  extern int convertLABToRGB(float flval, float faval, float fbval, l_int32* prval, l_int32* pgval, l_int32* pbval);
         internal static  extern int pixEqual(PIX* pix1, PIX* pix2, l_int32* psame);
         internal static  extern int pixEqualWithAlpha(PIX* pix1, PIX* pix2, int use_alpha, l_int32* psame);
         internal static  extern int pixEqualWithCmap(PIX* pix1, PIX* pix2, l_int32* psame);
         internal static  extern int cmapEqual(PIXCMAP* cmap1, PIXCMAP* cmap2, int ncomps, l_int32* psame);
         internal static  extern int pixUsesCmapColor(PIX* pixs, l_int32* pcolor);
         internal static  extern int pixCorrelationBinary(PIX* pix1, PIX* pix2, l_float32* pval);
         internal static  extern PIX* pixDisplayDiffBinary(PIX* pix1, PIX* pix2);
         internal static  extern int pixCompareBinary(PIX* pix1, PIX* pix2, int comptype, l_float32* pfract, PIX** ppixdiff);
         internal static  extern int pixCompareGrayOrRGB(PIX* pix1, PIX* pix2, int comptype, int plottype, l_int32* psame, l_float32* pdiff, l_float32* prmsdiff, PIX** ppixdiff);
         internal static  extern int pixCompareGray(PIX* pix1, PIX* pix2, int comptype, int plottype, l_int32* psame, l_float32* pdiff, l_float32* prmsdiff, PIX** ppixdiff);
         internal static  extern int pixCompareRGB(PIX* pix1, PIX* pix2, int comptype, int plottype, l_int32* psame, l_float32* pdiff, l_float32* prmsdiff, PIX** ppixdiff);
         internal static  extern int pixCompareTiled(PIX* pix1, PIX* pix2, int sx, int sy, int type, PIX** ppixdiff);
         internal static  extern NUMA* pixCompareRankDifference(PIX* pix1, PIX* pix2, int factor);
         internal static  extern int pixTestForSimilarity(PIX* pix1, PIX* pix2, int factor, int mindiff, float maxfract, float maxave, l_int32* psimilar, int printstats);
         internal static  extern int pixGetDifferenceStats(PIX* pix1, PIX* pix2, int factor, int mindiff, l_float32* pfractdiff, l_float32* pavediff, int printstats);
         internal static  extern NUMA* pixGetDifferenceHistogram(PIX* pix1, PIX* pix2, int factor);
         internal static  extern int pixGetPerceptualDiff(PIX* pixs1, PIX* pixs2, int sampling, int dilation, int mindiff, l_float32* pfract, PIX** ppixdiff1, PIX** ppixdiff2);
         internal static  extern int pixGetPSNR(PIX* pix1, PIX* pix2, int factor, l_float32* ppsnr);
         internal static  extern int pixaComparePhotoRegionsByHisto(PIXA* pixa, float minratio, float textthresh, int factor, int nx, int ny, float simthresh, NUMA** pnai, l_float32** pscores, PIX** ppixd);
         internal static  extern int pixComparePhotoRegionsByHisto(PIX* pix1, PIX* pix2, HandleRef box1, HandleRef box2, float minratio, int factor, int nx, int ny, l_float32* pscore, int debugflag);
         internal static  extern int pixGenPhotoHistos(PIX* pixs, HandleRef box, int factor, float thresh, int nx, int ny, NUMAA** pnaa, l_int32* pw, l_int32* ph, int debugflag);
         internal static  extern PIX* pixPadToCenterCentroid(PIX* pixs, int factor);
         internal static  extern int pixCentroid8(PIX* pixs, int factor, l_float32* pcx, l_float32* pcy);
         internal static  extern int pixDecideIfPhotoImage(PIX* pix, int factor, int nx, int ny, float thresh, NUMAA** pnaa, HandleRef pixadebug);
         internal static  extern int compareTilesByHisto(NUMAA* naa1, NUMAA* naa2, float minratio, int w1, int h1, int w2, int h2, l_float32* pscore, HandleRef pixadebug);
         internal static  extern int pixCompareGrayByHisto(PIX* pix1, PIX* pix2, HandleRef box1, HandleRef box2, float minratio, int maxgray, int factor, int nx, int ny, l_float32* pscore, int debugflag);
         internal static  extern int pixCropAlignedToCentroid(PIX* pix1, PIX* pix2, int factor, BOX** pbox1, BOX** pbox2);
         internal static  extern l_uint8* l_compressGrayHistograms(NUMAA* naa, int w, int h, size_t* psize);
         internal static  extern NUMAA* l_uncompressGrayHistograms(l_uint8* bytea, size_t size, l_int32* pw, l_int32* ph);
         internal static  extern int pixCompareWithTranslation(PIX* pix1, PIX* pix2, int thresh, l_int32* pdelx, l_int32* pdely, l_float32* pscore, int debugflag);
         internal static  extern int pixBestCorrelation(PIX* pix1, PIX* pix2, int area1, int area2, int etransx, int etransy, int maxshift, l_int32* tab8, l_int32* pdelx, l_int32* pdely, l_float32* pscore, int debugflag);
         internal static  extern HandleRef pixConnComp(PIX* pixs, PIXA** ppixa, int connectivity);
         internal static  extern HandleRef pixConnCompPixa(PIX* pixs, PIXA** ppixa, int connectivity);
         internal static  extern HandleRef pixConnCompBB(PIX* pixs, int connectivity);
         internal static  extern int pixCountConnComp(PIX* pixs, int connectivity, l_int32* pcount);
         internal static  extern int nextOnPixelInRaster(PIX* pixs, int xstart, int ystart, l_int32* px, l_int32* py);
         internal static  extern int nextOnPixelInRasterLow(l_uint32* data, int w, int h, int wpl, int xstart, int ystart, l_int32* px, l_int32* py);
         internal static  extern HandleRef pixSeedfillBB(PIX* pixs, L_STACK* stack, int x, int y, int connectivity);
         internal static  extern HandleRef pixSeedfill4BB(PIX* pixs, L_STACK* stack, int x, int y);
         internal static  extern HandleRef pixSeedfill8BB(PIX* pixs, L_STACK* stack, int x, int y);
         internal static  extern int pixSeedfill(PIX* pixs, L_STACK* stack, int x, int y, int connectivity);
         internal static  extern int pixSeedfill4(PIX* pixs, L_STACK* stack, int x, int y);
         internal static  extern int pixSeedfill8(PIX* pixs, L_STACK* stack, int x, int y);
         internal static  extern int convertFilesTo1bpp( const char* dirin, const char* substr, int upscaling, int thresh, int firstpage, int npages, const char* dirout, int outformat );
         internal static  extern PIX* pixBlockconv(PIX* pix, int wc, int hc);
         internal static  extern PIX* pixBlockconvGray(PIX* pixs, PIX* pixacc, int wc, int hc);
         internal static  extern PIX* pixBlockconvAccum(PIX* pixs);
         internal static  extern PIX* pixBlockconvGrayUnnormalized(PIX* pixs, int wc, int hc);
         internal static  extern PIX* pixBlockconvTiled(PIX* pix, int wc, int hc, int nx, int ny);
         internal static  extern PIX* pixBlockconvGrayTile(PIX* pixs, PIX* pixacc, int wc, int hc);
         internal static  extern int pixWindowedStats(PIX* pixs, int wc, int hc, int hasborder, PIX** ppixm, PIX** ppixms, FPIX** pfpixv, FPIX** pfpixrv);
         internal static  extern PIX* pixWindowedMean(PIX* pixs, int wc, int hc, int hasborder, int normflag);
         internal static  extern PIX* pixWindowedMeanSquare(PIX* pixs, int wc, int hc, int hasborder);
         internal static  extern int pixWindowedVariance(PIX* pixm, PIX* pixms, FPIX** pfpixv, FPIX** pfpixrv);
         internal static  extern DPIX* pixMeanSquareAccum(PIX* pixs);
         internal static  extern PIX* pixBlockrank(PIX* pixs, PIX* pixacc, int wc, int hc, float rank);
         internal static  extern PIX* pixBlocksum(PIX* pixs, PIX* pixacc, int wc, int hc);
         internal static  extern PIX* pixCensusTransform(PIX* pixs, int halfsize, PIX* pixacc);
         internal static  extern PIX* pixConvolve(PIX* pixs, L_KERNEL* kel, int outdepth, int normflag);
         internal static  extern PIX* pixConvolveSep(PIX* pixs, L_KERNEL* kelx, L_KERNEL* kely, int outdepth, int normflag);
         internal static  extern PIX* pixConvolveRGB(PIX* pixs, L_KERNEL* kel);
         internal static  extern PIX* pixConvolveRGBSep(PIX* pixs, L_KERNEL* kelx, L_KERNEL* kely);
         internal static  extern FPIX* fpixConvolve(FPIX* fpixs, L_KERNEL* kel, int normflag);
         internal static  extern FPIX* fpixConvolveSep(FPIX* fpixs, L_KERNEL* kelx, L_KERNEL* kely, int normflag);
         internal static  extern PIX* pixConvolveWithBias(PIX* pixs, L_KERNEL* kel1, L_KERNEL* kel2, int force8, l_int32* pbias);
         internal static  extern void l_setConvolveSampling(int xfact, int yfact);
         internal static  extern PIX* pixAddGaussianNoise(PIX* pixs, float stdev);
         internal static  extern float gaussDistribSampling();
         internal static  extern int pixCorrelationScore(PIX* pix1, PIX* pix2, int area1, int area2, float delx, float dely, int maxdiffw, int maxdiffh, l_int32* tab, l_float32* pscore);
         internal static  extern int pixCorrelationScoreThresholded(PIX* pix1, PIX* pix2, int area1, int area2, float delx, float dely, int maxdiffw, int maxdiffh, l_int32* tab, l_int32* downcount, float score_threshold);
         internal static  extern int pixCorrelationScoreSimple(PIX* pix1, PIX* pix2, int area1, int area2, float delx, float dely, int maxdiffw, int maxdiffh, l_int32* tab, l_float32* pscore);
         internal static  extern int pixCorrelationScoreShifted(PIX* pix1, PIX* pix2, int area1, int area2, int delx, int dely, l_int32* tab, l_float32* pscore);
         internal static  extern L_DEWARP* dewarpCreate(PIX* pixs, int pageno);
         internal static  extern L_DEWARP* dewarpCreateRef(int pageno, int refpage);
         internal static  extern void dewarpDestroy(L_DEWARP** pdew);
         internal static  extern L_DEWARPA* dewarpaCreate(int nptrs, int sampling, int redfactor, int minlines, int maxdist);
         internal static  extern L_DEWARPA* dewarpaCreateFromPixacomp(PIXAC* pixac, int useboth, int sampling, int minlines, int maxdist);
         internal static  extern void dewarpaDestroy(L_DEWARPA** pdewa);
         internal static  extern int dewarpaDestroyDewarp(L_DEWARPA* dewa, int pageno);
         internal static  extern int dewarpaInsertDewarp(L_DEWARPA* dewa, L_DEWARP* dew);
         internal static  extern L_DEWARP* dewarpaGetDewarp(L_DEWARPA* dewa, int index);
         internal static  extern int dewarpaSetCurvatures(L_DEWARPA* dewa, int max_linecurv, int min_diff_linecurv, int max_diff_linecurv, int max_edgecurv, int max_diff_edgecurv, int max_edgeslope);
         internal static  extern int dewarpaUseBothArrays(L_DEWARPA* dewa, int useboth);
         internal static  extern int dewarpaSetCheckColumns(L_DEWARPA* dewa, int check_columns);
         internal static  extern int dewarpaSetMaxDistance(L_DEWARPA* dewa, int maxdist);
         internal static  extern L_DEWARP* dewarpRead( const char* filename );
         internal static  extern L_DEWARP* dewarpReadStream(FILE* fp);
         internal static  extern L_DEWARP* dewarpReadMem( const l_uint8* data, size_t size );
         internal static  extern int dewarpWrite( const char* filename, L_DEWARP *dew );
         internal static  extern int dewarpWriteStream(FILE* fp, L_DEWARP* dew);
         internal static  extern int dewarpWriteMem(l_uint8** pdata, size_t* psize, L_DEWARP* dew);
         internal static  extern L_DEWARPA* dewarpaRead( const char* filename );
         internal static  extern L_DEWARPA* dewarpaReadStream(FILE* fp);
         internal static  extern L_DEWARPA* dewarpaReadMem( const l_uint8* data, size_t size );
         internal static  extern int dewarpaWrite( const char* filename, L_DEWARPA *dewa );
         internal static  extern int dewarpaWriteStream(FILE* fp, L_DEWARPA* dewa);
         internal static  extern int dewarpaWriteMem(l_uint8** pdata, size_t* psize, L_DEWARPA* dewa);
         internal static  extern int dewarpBuildPageModel(L_DEWARP* dew, const char* debugfile );
         internal static  extern int dewarpFindVertDisparity(L_DEWARP* dew, PTAA* ptaa, int rotflag);
         internal static  extern int dewarpFindHorizDisparity(L_DEWARP* dew, PTAA* ptaa);
         internal static  extern PTAA* dewarpGetTextlineCenters(PIX* pixs, int debugflag);
         internal static  extern PTAA* dewarpRemoveShortLines(PIX* pixs, PTAA* ptaas, float fract, int debugflag);
         internal static  extern int dewarpFindHorizSlopeDisparity(L_DEWARP* dew, PIX* pixb, float fractthresh, int parity);
         internal static  extern int dewarpBuildLineModel(L_DEWARP* dew, int opensize, const char* debugfile );
         internal static  extern int dewarpaModelStatus(L_DEWARPA* dewa, int pageno, l_int32* pvsuccess, l_int32* phsuccess);
         internal static  extern int dewarpaApplyDisparity(L_DEWARPA* dewa, int pageno, PIX* pixs, int grayin, int x, int y, PIX** ppixd, const char* debugfile );
         internal static  extern int dewarpaApplyDisparityBoxa(L_DEWARPA* dewa, int pageno, PIX* pixs, HandleRef boxas, int mapdir, int x, int y, BOXA** pboxad, const char* debugfile );
         internal static  extern int dewarpMinimize(L_DEWARP* dew);
         internal static  extern int dewarpPopulateFullRes(L_DEWARP* dew, PIX* pix, int x, int y);
         internal static  extern int dewarpSinglePage(PIX* pixs, int thresh, int adaptive, int useboth, int check_columns, PIX** ppixd, L_DEWARPA** pdewa, int debug);
         internal static  extern int dewarpSinglePageInit(PIX* pixs, int thresh, int adaptive, int useboth, int check_columns, PIX** ppixb, L_DEWARPA** pdewa);
         internal static  extern int dewarpSinglePageRun(PIX* pixs, PIX* pixb, L_DEWARPA* dewa, PIX** ppixd, int debug);
         internal static  extern int dewarpaListPages(L_DEWARPA* dewa);
         internal static  extern int dewarpaSetValidModels(L_DEWARPA* dewa, int notests, int debug);
         internal static  extern int dewarpaInsertRefModels(L_DEWARPA* dewa, int notests, int debug);
         internal static  extern int dewarpaStripRefModels(L_DEWARPA* dewa);
         internal static  extern int dewarpaRestoreModels(L_DEWARPA* dewa);
         internal static  extern int dewarpaInfo(FILE* fp, L_DEWARPA* dewa);
         internal static  extern int dewarpaModelStats(L_DEWARPA* dewa, l_int32* pnnone, l_int32* pnvsuccess, l_int32* pnvvalid, l_int32* pnhsuccess, l_int32* pnhvalid, l_int32* pnref);
         internal static  extern int dewarpaShowArrays(L_DEWARPA* dewa, float scalefact, int first, int last);
         internal static  extern int dewarpDebug(L_DEWARP* dew, const char* subdirs, int index );
         internal static  extern int dewarpShowResults(L_DEWARPA* dewa, SARRAY* sa, HandleRef boxa, int firstpage, int lastpage, const char* pdfout );
         internal static  extern L_DNA* l_dnaCreate(int n);
         internal static  extern L_DNA* l_dnaCreateFromIArray(l_int32* iarray, int size);
         internal static  extern L_DNA* l_dnaCreateFromDArray(l_float64* darray, int size, int copyflag);
         internal static  extern L_DNA* l_dnaMakeSequence(l_float64 startval, l_float64 increment, int size);
         internal static  extern void l_dnaDestroy(L_DNA** pda);
         internal static  extern L_DNA* l_dnaCopy(L_DNA* da);
         internal static  extern L_DNA* l_dnaClone(L_DNA* da);
         internal static  extern int l_dnaEmpty(L_DNA* da);
         internal static  extern int l_dnaAddNumber(L_DNA* da, l_float64 val);
         internal static  extern int l_dnaInsertNumber(L_DNA* da, int index, l_float64 val);
         internal static  extern int l_dnaRemoveNumber(L_DNA* da, int index);
         internal static  extern int l_dnaReplaceNumber(L_DNA* da, int index, l_float64 val);
         internal static  extern int l_dnaGetCount(L_DNA* da);
         internal static  extern int l_dnaSetCount(L_DNA* da, int newcount);
         internal static  extern int l_dnaGetDValue(L_DNA* da, int index, l_float64* pval);
         internal static  extern int l_dnaGetIValue(L_DNA* da, int index, l_int32* pival);
         internal static  extern int l_dnaSetValue(L_DNA* da, int index, l_float64 val);
         internal static  extern int l_dnaShiftValue(L_DNA* da, int index, l_float64 diff);
         internal static  extern l_int32* l_dnaGetIArray(L_DNA* da);
         internal static  extern l_float64* l_dnaGetDArray(L_DNA* da, int copyflag);
         internal static  extern int l_dnaGetRefcount(L_DNA* da);
         internal static  extern int l_dnaChangeRefcount(L_DNA* da, int delta);
         internal static  extern int l_dnaGetParameters(L_DNA* da, l_float64* pstartx, l_float64* pdelx);
         internal static  extern int l_dnaSetParameters(L_DNA* da, l_float64 startx, l_float64 delx);
         internal static  extern int l_dnaCopyParameters(L_DNA* dad, L_DNA* das);
         internal static  extern L_DNA* l_dnaRead( const char* filename );
         internal static  extern L_DNA* l_dnaReadStream(FILE* fp);
         internal static  extern int l_dnaWrite( const char* filename, L_DNA *da );
         internal static  extern int l_dnaWriteStream(FILE* fp, L_DNA* da);
         internal static  extern L_DNAA* l_dnaaCreate(int n);
         internal static  extern L_DNAA* l_dnaaCreateFull(int nptr, int n);
         internal static  extern int l_dnaaTruncate(L_DNAA* daa);
         internal static  extern void l_dnaaDestroy(L_DNAA** pdaa);
         internal static  extern int l_dnaaAddDna(L_DNAA* daa, L_DNA* da, int copyflag);
         internal static  extern int l_dnaaGetCount(L_DNAA* daa);
         internal static  extern int l_dnaaGetDnaCount(L_DNAA* daa, int index);
         internal static  extern int l_dnaaGetNumberCount(L_DNAA* daa);
         internal static  extern L_DNA* l_dnaaGetDna(L_DNAA* daa, int index, int accessflag);
         internal static  extern int l_dnaaReplaceDna(L_DNAA* daa, int index, L_DNA* da);
         internal static  extern int l_dnaaGetValue(L_DNAA* daa, int i, int j, l_float64* pval);
         internal static  extern int l_dnaaAddNumber(L_DNAA* daa, int index, l_float64 val);
         internal static  extern L_DNAA* l_dnaaRead( const char* filename );
         internal static  extern L_DNAA* l_dnaaReadStream(FILE* fp);
         internal static  extern int l_dnaaWrite( const char* filename, L_DNAA *daa );
         internal static  extern int l_dnaaWriteStream(FILE* fp, L_DNAA* daa);
         internal static  extern int l_dnaJoin(L_DNA* dad, L_DNA* das, int istart, int iend);
         internal static  extern L_DNA* l_dnaaFlattenToDna(L_DNAA* daa);
         internal static  extern NUMA* l_dnaConvertToNuma(L_DNA* da);
         internal static  extern L_DNA* numaConvertToDna(NUMA* na);
         internal static  extern L_DNA* l_dnaUnionByAset(L_DNA* da1, L_DNA* da2);
         internal static  extern L_DNA* l_dnaRemoveDupsByAset(L_DNA* das);
         internal static  extern L_DNA* l_dnaIntersectionByAset(L_DNA* da1, L_DNA* da2);
         internal static  extern L_ASET* l_asetCreateFromDna(L_DNA* da);
         internal static  extern L_DNA* l_dnaDiffAdjValues(L_DNA* das);
         internal static  extern L_DNAHASH* l_dnaHashCreate(int nbuckets, int initsize);
         internal static  extern void l_dnaHashDestroy(L_DNAHASH** pdahash);
         internal static  extern int l_dnaHashGetCount(L_DNAHASH* dahash);
         internal static  extern int l_dnaHashGetTotalCount(L_DNAHASH* dahash);
         internal static  extern L_DNA* l_dnaHashGetDna(L_DNAHASH* dahash, l_uint64 key, int copyflag);
         internal static  extern int l_dnaHashAdd(L_DNAHASH* dahash, l_uint64 key, l_float64 value);
         internal static  extern L_DNAHASH* l_dnaHashCreateFromDna(L_DNA* da);
         internal static  extern int l_dnaRemoveDupsByHash(L_DNA* das, L_DNA** pdad, L_DNAHASH** pdahash);
         internal static  extern int l_dnaMakeHistoByHash(L_DNA* das, L_DNAHASH** pdahash, L_DNA** pdav, L_DNA** pdac);
         internal static  extern L_DNA* l_dnaIntersectionByHash(L_DNA* da1, L_DNA* da2);
         internal static  extern int l_dnaFindValByHash(L_DNA* da, L_DNAHASH* dahash, l_float64 val, l_int32* pindex);
         internal static  extern PIX* pixMorphDwa_2(PIX* pixd, PIX* pixs, int operation, char* selname);
         internal static  extern PIX* pixFMorphopGen_2(PIX* pixd, PIX* pixs, int operation, char* selname);
         internal static  extern int fmorphopgen_low_2(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, int index);
         internal static  extern PIX* pixSobelEdgeFilter(PIX* pixs, int orientflag);
         internal static  extern PIX* pixTwoSidedEdgeFilter(PIX* pixs, int orientflag);
         internal static  extern int pixMeasureEdgeSmoothness(PIX* pixs, int side, int minjump, int minreversal, l_float32* pjpl, l_float32* pjspl, l_float32* prpl, const char* debugfile );
         internal static  extern NUMA* pixGetEdgeProfile(PIX* pixs, int side, const char* debugfile );
         internal static  extern int pixGetLastOffPixelInRun(PIX* pixs, int x, int y, int direction, l_int32* ploc);
         internal static  extern int pixGetLastOnPixelInRun(PIX* pixs, int x, int y, int direction, l_int32* ploc);
         internal static  extern char* encodeBase64(l_uint8* inarray, int insize, l_int32* poutsize);
         internal static  extern l_uint8* decodeBase64( const char* inarray, int insize, l_int32* poutsize );
         internal static  extern char* encodeAscii85(l_uint8* inarray, int insize, l_int32* poutsize);
         internal static  extern l_uint8* decodeAscii85(char* inarray, int insize, l_int32* poutsize);
         internal static  extern char* reformatPacked64(char* inarray, int insize, int leadspace, int linechars, int addquotes, l_int32* poutsize);
         internal static  extern PIX* pixGammaTRC(PIX* pixd, PIX* pixs, float gamma, int minval, int maxval);
         internal static  extern PIX* pixGammaTRCMasked(PIX* pixd, PIX* pixs, PIX* pixm, float gamma, int minval, int maxval);
         internal static  extern PIX* pixGammaTRCWithAlpha(PIX* pixd, PIX* pixs, float gamma, int minval, int maxval);
         internal static  extern NUMA* numaGammaTRC(float gamma, int minval, int maxval);
         internal static  extern PIX* pixContrastTRC(PIX* pixd, PIX* pixs, float factor);
         internal static  extern PIX* pixContrastTRCMasked(PIX* pixd, PIX* pixs, PIX* pixm, float factor);
         internal static  extern NUMA* numaContrastTRC(float factor);
         internal static  extern PIX* pixEqualizeTRC(PIX* pixd, PIX* pixs, float fract, int factor);
         internal static  extern NUMA* numaEqualizeTRC(PIX* pix, float fract, int factor);
         internal static  extern int pixTRCMap(PIX* pixs, PIX* pixm, NUMA* na);
         internal static  extern PIX* pixUnsharpMasking(PIX* pixs, int halfwidth, float fract);
         internal static  extern PIX* pixUnsharpMaskingGray(PIX* pixs, int halfwidth, float fract);
         internal static  extern PIX* pixUnsharpMaskingFast(PIX* pixs, int halfwidth, float fract, int direction);
         internal static  extern PIX* pixUnsharpMaskingGrayFast(PIX* pixs, int halfwidth, float fract, int direction);
         internal static  extern PIX* pixUnsharpMaskingGray1D(PIX* pixs, int halfwidth, float fract, int direction);
         internal static  extern PIX* pixUnsharpMaskingGray2D(PIX* pixs, int halfwidth, float fract);
         internal static  extern PIX* pixModifyHue(PIX* pixd, PIX* pixs, float fract);
         internal static  extern PIX* pixModifySaturation(PIX* pixd, PIX* pixs, float fract);
         internal static  extern int pixMeasureSaturation(PIX* pixs, int factor, l_float32* psat);
         internal static  extern PIX* pixModifyBrightness(PIX* pixd, PIX* pixs, float fract);
         internal static  extern PIX* pixColorShiftRGB(PIX* pixs, float rfract, float gfract, float bfract);
         internal static  extern PIX* pixMultConstantColor(PIX* pixs, float rfact, float gfact, float bfact);
         internal static  extern PIX* pixMultMatrixColor(PIX* pixs, L_KERNEL* kel);
         internal static  extern PIX* pixHalfEdgeByBandpass(PIX* pixs, int sm1h, int sm1v, int sm2h, int sm2v);
         internal static  extern int fhmtautogen(SELA* sela, int fileindex, const char* filename );
         internal static  extern int fhmtautogen1(SELA* sela, int fileindex, const char* filename );
         internal static  extern int fhmtautogen2(SELA* sela, int fileindex, const char* filename );
         internal static  extern PIX* pixHMTDwa_1(PIX* pixd, PIX* pixs, const char* selname );
         internal static  extern PIX* pixFHMTGen_1(PIX* pixd, PIX* pixs, const char* selname );
         internal static  extern int fhmtgen_low_1(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, int index);
         internal static  extern int pixItalicWords(PIX* pixs, HandleRef boxaw, PIX* pixw, BOXA** pboxa, int debugflag);
         internal static  extern int pixOrientDetect(PIX* pixs, l_float32* pupconf, l_float32* pleftconf, int mincount, int debug);
         internal static  extern int makeOrientDecision(float upconf, float leftconf, float minupconf, float minratio, l_int32* porient, int debug);
         internal static  extern int pixUpDownDetect(PIX* pixs, l_float32* pconf, int mincount, int debug);
         internal static  extern int pixUpDownDetectGeneral(PIX* pixs, l_float32* pconf, int mincount, int npixels, int debug);
         internal static  extern int pixOrientDetectDwa(PIX* pixs, l_float32* pupconf, l_float32* pleftconf, int mincount, int debug);
         internal static  extern int pixUpDownDetectDwa(PIX* pixs, l_float32* pconf, int mincount, int debug);
         internal static  extern int pixUpDownDetectGeneralDwa(PIX* pixs, l_float32* pconf, int mincount, int npixels, int debug);
         internal static  extern int pixMirrorDetect(PIX* pixs, l_float32* pconf, int mincount, int debug);
         internal static  extern int pixMirrorDetectDwa(PIX* pixs, l_float32* pconf, int mincount, int debug);
         internal static  extern PIX* pixFlipFHMTGen(PIX* pixd, PIX* pixs, char* selname);
         internal static  extern int fmorphautogen(SELA* sela, int fileindex, const char* filename );
         internal static  extern int fmorphautogen1(SELA* sela, int fileindex, const char* filename );
         internal static  extern int fmorphautogen2(SELA* sela, int fileindex, const char* filename );
         internal static  extern PIX* pixMorphDwa_1(PIX* pixd, PIX* pixs, int operation, char* selname);
         internal static  extern PIX* pixFMorphopGen_1(PIX* pixd, PIX* pixs, int operation, char* selname);
         internal static  extern int fmorphopgen_low_1(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, int index);
         internal static  extern FPIX* fpixCreate(int width, int height);
         internal static  extern FPIX* fpixCreateTemplate(FPIX* fpixs);
         internal static  extern FPIX* fpixClone(FPIX* fpix);
         internal static  extern FPIX* fpixCopy(FPIX* fpixd, FPIX* fpixs);
         internal static  extern int fpixResizeImageData(FPIX* fpixd, FPIX* fpixs);
         internal static  extern void fpixDestroy(FPIX** pfpix);
         internal static  extern int fpixGetDimensions(FPIX* fpix, l_int32* pw, l_int32* ph);
         internal static  extern int fpixSetDimensions(FPIX* fpix, int w, int h);
         internal static  extern int fpixGetWpl(FPIX* fpix);
         internal static  extern int fpixSetWpl(FPIX* fpix, int wpl);
         internal static  extern int fpixGetRefcount(FPIX* fpix);
         internal static  extern int fpixChangeRefcount(FPIX* fpix, int delta);
         internal static  extern int fpixGetResolution(FPIX* fpix, l_int32* pxres, l_int32* pyres);
         internal static  extern int fpixSetResolution(FPIX* fpix, int xres, int yres);
         internal static  extern int fpixCopyResolution(FPIX* fpixd, FPIX* fpixs);
         internal static  extern l_float32* fpixGetData(FPIX* fpix);
         internal static  extern int fpixSetData(FPIX* fpix, l_float32* data);
         internal static  extern int fpixGetPixel(FPIX* fpix, int x, int y, l_float32* pval);
         internal static  extern int fpixSetPixel(FPIX* fpix, int x, int y, float val);
         internal static  extern FPIXA* fpixaCreate(int n);
         internal static  extern FPIXA* fpixaCopy(FPIXA* fpixa, int copyflag);
         internal static  extern void fpixaDestroy(FPIXA** pfpixa);
         internal static  extern int fpixaAddFPix(FPIXA* fpixa, FPIX* fpix, int copyflag);
         internal static  extern int fpixaGetCount(FPIXA* fpixa);
         internal static  extern int fpixaChangeRefcount(FPIXA* fpixa, int delta);
         internal static  extern FPIX* fpixaGetFPix(FPIXA* fpixa, int index, int accesstype);
         internal static  extern int fpixaGetFPixDimensions(FPIXA* fpixa, int index, l_int32* pw, l_int32* ph);
         internal static  extern l_float32* fpixaGetData(FPIXA* fpixa, int index);
         internal static  extern int fpixaGetPixel(FPIXA* fpixa, int index, int x, int y, l_float32* pval);
         internal static  extern int fpixaSetPixel(FPIXA* fpixa, int index, int x, int y, float val);
         internal static  extern DPIX* dpixCreate(int width, int height);
         internal static  extern DPIX* dpixCreateTemplate(DPIX* dpixs);
         internal static  extern DPIX* dpixClone(DPIX* dpix);
         internal static  extern DPIX* dpixCopy(DPIX* dpixd, DPIX* dpixs);
         internal static  extern int dpixResizeImageData(DPIX* dpixd, DPIX* dpixs);
         internal static  extern void dpixDestroy(DPIX** pdpix);
         internal static  extern int dpixGetDimensions(DPIX* dpix, l_int32* pw, l_int32* ph);
         internal static  extern int dpixSetDimensions(DPIX* dpix, int w, int h);
         internal static  extern int dpixGetWpl(DPIX* dpix);
         internal static  extern int dpixSetWpl(DPIX* dpix, int wpl);
         internal static  extern int dpixGetRefcount(DPIX* dpix);
         internal static  extern int dpixChangeRefcount(DPIX* dpix, int delta);
         internal static  extern int dpixGetResolution(DPIX* dpix, l_int32* pxres, l_int32* pyres);
         internal static  extern int dpixSetResolution(DPIX* dpix, int xres, int yres);
         internal static  extern int dpixCopyResolution(DPIX* dpixd, DPIX* dpixs);
         internal static  extern l_float64* dpixGetData(DPIX* dpix);
         internal static  extern int dpixSetData(DPIX* dpix, l_float64* data);
         internal static  extern int dpixGetPixel(DPIX* dpix, int x, int y, l_float64* pval);
         internal static  extern int dpixSetPixel(DPIX* dpix, int x, int y, l_float64 val);
         internal static  extern FPIX* fpixRead( const char* filename );
         internal static  extern FPIX* fpixReadStream(FILE* fp);
         internal static  extern FPIX* fpixReadMem( const l_uint8* data, size_t size );
         internal static  extern int fpixWrite( const char* filename, FPIX *fpix );
         internal static  extern int fpixWriteStream(FILE* fp, FPIX* fpix);
         internal static  extern int fpixWriteMem(l_uint8** pdata, size_t* psize, FPIX* fpix);
         internal static  extern FPIX* fpixEndianByteSwap(FPIX* fpixd, FPIX* fpixs);
         internal static  extern DPIX* dpixRead( const char* filename );
         internal static  extern DPIX* dpixReadStream(FILE* fp);
         internal static  extern DPIX* dpixReadMem( const l_uint8* data, size_t size );
         internal static  extern int dpixWrite( const char* filename, DPIX *dpix );
         internal static  extern int dpixWriteStream(FILE* fp, DPIX* dpix);
         internal static  extern int dpixWriteMem(l_uint8** pdata, size_t* psize, DPIX* dpix);
         internal static  extern DPIX* dpixEndianByteSwap(DPIX* dpixd, DPIX* dpixs);
         internal static  extern int fpixPrintStream(FILE* fp, FPIX* fpix, int factor);
         internal static  extern FPIX* pixConvertToFPix(PIX* pixs, int ncomps);
         internal static  extern DPIX* pixConvertToDPix(PIX* pixs, int ncomps);
         internal static  extern PIX* fpixConvertToPix(FPIX* fpixs, int outdepth, int negvals, int errorflag);
         internal static  extern PIX* fpixDisplayMaxDynamicRange(FPIX* fpixs);
         internal static  extern DPIX* fpixConvertToDPix(FPIX* fpix);
         internal static  extern PIX* dpixConvertToPix(DPIX* dpixs, int outdepth, int negvals, int errorflag);
         internal static  extern FPIX* dpixConvertToFPix(DPIX* dpix);
         internal static  extern int fpixGetMin(FPIX* fpix, l_float32* pminval, l_int32* pxminloc, l_int32* pyminloc);
         internal static  extern int fpixGetMax(FPIX* fpix, l_float32* pmaxval, l_int32* pxmaxloc, l_int32* pymaxloc);
         internal static  extern int dpixGetMin(DPIX* dpix, l_float64* pminval, l_int32* pxminloc, l_int32* pyminloc);
         internal static  extern int dpixGetMax(DPIX* dpix, l_float64* pmaxval, l_int32* pxmaxloc, l_int32* pymaxloc);
         internal static  extern FPIX* fpixScaleByInteger(FPIX* fpixs, int factor);
         internal static  extern DPIX* dpixScaleByInteger(DPIX* dpixs, int factor);
         internal static  extern FPIX* fpixLinearCombination(FPIX* fpixd, FPIX* fpixs1, FPIX* fpixs2, float a, float b);
         internal static  extern int fpixAddMultConstant(FPIX* fpix, float addc, float multc);
         internal static  extern DPIX* dpixLinearCombination(DPIX* dpixd, DPIX* dpixs1, DPIX* dpixs2, float a, float b);
         internal static  extern int dpixAddMultConstant(DPIX* dpix, l_float64 addc, l_float64 multc);
         internal static  extern int fpixSetAllArbitrary(FPIX* fpix, float inval);
         internal static  extern int dpixSetAllArbitrary(DPIX* dpix, l_float64 inval);
         internal static  extern FPIX* fpixAddBorder(FPIX* fpixs, int left, int right, int top, int bot);
         internal static  extern FPIX* fpixRemoveBorder(FPIX* fpixs, int left, int right, int top, int bot);
         internal static  extern FPIX* fpixAddMirroredBorder(FPIX* fpixs, int left, int right, int top, int bot);
         internal static  extern FPIX* fpixAddContinuedBorder(FPIX* fpixs, int left, int right, int top, int bot);
         internal static  extern FPIX* fpixAddSlopeBorder(FPIX* fpixs, int left, int right, int top, int bot);
         internal static  extern int fpixRasterop(FPIX* fpixd, int dx, int dy, int dw, int dh, FPIX* fpixs, int sx, int sy);
         internal static  extern FPIX* fpixRotateOrth(FPIX* fpixs, int quads);
         internal static  extern FPIX* fpixRotate180(FPIX* fpixd, FPIX* fpixs);
         internal static  extern FPIX* fpixRotate90(FPIX* fpixs, int direction);
         internal static  extern FPIX* fpixFlipLR(FPIX* fpixd, FPIX* fpixs);
         internal static  extern FPIX* fpixFlipTB(FPIX* fpixd, FPIX* fpixs);
         internal static  extern FPIX* fpixAffinePta(FPIX* fpixs, PTA* ptad, PTA* ptas, int border, float inval);
         internal static  extern FPIX* fpixAffine(FPIX* fpixs, l_float32* vc, float inval);
         internal static  extern FPIX* fpixProjectivePta(FPIX* fpixs, PTA* ptad, PTA* ptas, int border, float inval);
         internal static  extern FPIX* fpixProjective(FPIX* fpixs, l_float32* vc, float inval);
         internal static  extern int linearInterpolatePixelFloat(l_float32* datas, int w, int h, float x, float y, float inval, l_float32* pval);
         internal static  extern PIX* fpixThresholdToPix(FPIX* fpix, float thresh);
         internal static  extern FPIX* pixComponentFunction(PIX* pix, float rnum, float gnum, float bnum, float rdenom, float gdenom, float bdenom);
         internal static  extern PIX* pixReadStreamGif(FILE* fp);
         internal static  extern int pixWriteStreamGif(FILE* fp, PIX* pix);
         internal static  extern PIX* pixReadMemGif( const l_uint8* cdata, size_t size );
         internal static  extern int pixWriteMemGif(l_uint8** pdata, size_t* psize, PIX* pix);
         internal static  extern GPLOT* gplotCreate( const char* rootname, int outformat, const char* title, const char* xlabel, const char* ylabel );
         internal static  extern void gplotDestroy(GPLOT** pgplot);
         internal static  extern int gplotAddPlot(GPLOT* gplot, NUMA* nax, NUMA* nay, int plotstyle, const char* plottitle );
         internal static  extern int gplotSetScaling(GPLOT* gplot, int scaling);
         internal static  extern int gplotMakeOutput(GPLOT* gplot);
         internal static  extern int gplotGenCommandFile(GPLOT* gplot);
         internal static  extern int gplotGenDataFiles(GPLOT* gplot);
         internal static  extern int gplotSimple1(NUMA* na, int outformat, const char* outroot, const char* title );
         internal static  extern int gplotSimple2(NUMA* na1, NUMA* na2, int outformat, const char* outroot, const char* title );
         internal static  extern int gplotSimpleN(NUMAA* naa, int outformat, const char* outroot, const char* title );
         internal static  extern int gplotSimpleXY1(NUMA* nax, NUMA* nay, int plotstyle, int outformat, const char* outroot, const char* title );
         internal static  extern int gplotSimpleXY2(NUMA* nax, NUMA* nay1, NUMA* nay2, int plotstyle, int outformat, const char* outroot, const char* title );
         internal static  extern int gplotSimpleXYN(NUMA* nax, NUMAA* naay, int plotstyle, int outformat, const char* outroot, const char* title );
         internal static  extern GPLOT* gplotRead( const char* filename );
         internal static  extern int gplotWrite( const char* filename, GPLOT *gplot );
         internal static  extern PTA* generatePtaLine(int x1, int y1, int x2, int y2);
         internal static  extern PTA* generatePtaWideLine(int x1, int y1, int x2, int y2, int width);
         internal static  extern PTA* generatePtaBox(HandleRef box, int width);
         internal static  extern PTA* generatePtaBoxa(HandleRef boxa, int width, int removedups);
         internal static  extern PTA* generatePtaHashBox(HandleRef box, int spacing, int width, int orient, int outline);
         internal static  extern PTA* generatePtaHashBoxa(HandleRef boxa, int spacing, int width, int orient, int outline, int removedups);
         internal static  extern PTAA* generatePtaaBoxa(HandleRef boxa);
         internal static  extern PTAA* generatePtaaHashBoxa(HandleRef boxa, int spacing, int width, int orient, int outline);
         internal static  extern PTA* generatePtaPolyline(HandleRef ptas, int width, int closeflag, int removedups);
         internal static  extern PTA* generatePtaGrid(int w, int h, int nx, int ny, int width);
         internal static  extern PTA* convertPtaLineTo4cc(HandleRef ptas);
         internal static  extern PTA* generatePtaFilledCircle(int radius);
         internal static  extern PTA* generatePtaFilledSquare(int side);
         internal static  extern PTA* generatePtaLineFromPt(int x, int y, l_float64 length, l_float64 radang);
         internal static  extern int locatePtRadially(int xr, int yr, l_float64 dist, l_float64 radang, l_float64* px, l_float64* py);
         internal static  extern int pixRenderPlotFromNuma(PIX** ppix, NUMA* na, int plotloc, int linewidth, int max, uint color);
         internal static  extern PTA* makePlotPtaFromNuma(NUMA* na, int size, int plotloc, int linewidth, int max);
         internal static  extern int pixRenderPlotFromNumaGen(PIX** ppix, NUMA* na, int orient, int linewidth, int refpos, int max, int drawref, uint color);
         internal static  extern PTA* makePlotPtaFromNumaGen(NUMA* na, int orient, int linewidth, int refpos, int max, int drawref);
         internal static  extern int pixRenderPta(PIX* pix, PTA* pta, int op);
         internal static  extern int pixRenderPtaArb(PIX* pix, PTA* pta, byte rval, byte gval, byte bval);
         internal static  extern int pixRenderPtaBlend(PIX* pix, PTA* pta, byte rval, byte gval, byte bval, float fract);
         internal static  extern int pixRenderLine(PIX* pix, int x1, int y1, int x2, int y2, int width, int op);
         internal static  extern int pixRenderLineArb(PIX* pix, int x1, int y1, int x2, int y2, int width, byte rval, byte gval, byte bval);
         internal static  extern int pixRenderLineBlend(PIX* pix, int x1, int y1, int x2, int y2, int width, byte rval, byte gval, byte bval, float fract);
         internal static  extern int pixRenderBox(PIX* pix, HandleRef box, int width, int op);
         internal static  extern int pixRenderBoxArb(PIX* pix, HandleRef box, int width, byte rval, byte gval, byte bval);
         internal static  extern int pixRenderBoxBlend(PIX* pix, HandleRef box, int width, byte rval, byte gval, byte bval, float fract);
         internal static  extern int pixRenderBoxa(PIX* pix, HandleRef boxa, int width, int op);
         internal static  extern int pixRenderBoxaArb(PIX* pix, HandleRef boxa, int width, byte rval, byte gval, byte bval);
         internal static  extern int pixRenderBoxaBlend(PIX* pix, HandleRef boxa, int width, byte rval, byte gval, byte bval, float fract, int removedups);
         internal static  extern int pixRenderHashBox(PIX* pix, HandleRef box, int spacing, int width, int orient, int outline, int op);
         internal static  extern int pixRenderHashBoxArb(PIX* pix, HandleRef box, int spacing, int width, int orient, int outline, int rval, int gval, int bval);
         internal static  extern int pixRenderHashBoxBlend(PIX* pix, HandleRef box, int spacing, int width, int orient, int outline, int rval, int gval, int bval, float fract);
         internal static  extern int pixRenderHashMaskArb(PIX* pix, PIX* pixm, int x, int y, int spacing, int width, int orient, int outline, int rval, int gval, int bval);
         internal static  extern int pixRenderHashBoxa(PIX* pix, HandleRef boxa, int spacing, int width, int orient, int outline, int op);
         internal static  extern int pixRenderHashBoxaArb(PIX* pix, HandleRef boxa, int spacing, int width, int orient, int outline, int rval, int gval, int bval);
         internal static  extern int pixRenderHashBoxaBlend(PIX* pix, HandleRef boxa, int spacing, int width, int orient, int outline, int rval, int gval, int bval, float fract);
         internal static  extern int pixRenderPolyline(PIX* pix, PTA* ptas, int width, int op, int closeflag);
         internal static  extern int pixRenderPolylineArb(PIX* pix, PTA* ptas, int width, byte rval, byte gval, byte bval, int closeflag);
         internal static  extern int pixRenderPolylineBlend(PIX* pix, PTA* ptas, int width, byte rval, byte gval, byte bval, float fract, int closeflag, int removedups);
         internal static  extern int pixRenderGridArb(PIX* pix, int nx, int ny, int width, byte rval, byte gval, byte bval);
         internal static  extern PIX* pixRenderRandomCmapPtaa(PIX* pix, PTAA* ptaa, int polyflag, int width, int closeflag);
         internal static  extern PIX* pixRenderPolygon(HandleRef ptas, int width, l_int32* pxmin, l_int32* pymin);
         internal static  extern PIX* pixFillPolygon(PIX* pixs, PTA* pta, int xmin, int ymin);
         internal static  extern PIX* pixRenderContours(PIX* pixs, int startval, int incr, int outdepth);
         internal static  extern PIX* fpixAutoRenderContours(FPIX* fpix, int ncontours);
         internal static  extern PIX* fpixRenderContours(FPIX* fpixs, float incr, float proxim);
         internal static  extern PTA* pixGeneratePtaBoundary(PIX* pixs, int width);
         internal static  extern PIX* pixErodeGray(PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixDilateGray(PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixOpenGray(PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseGray(PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixErodeGray3(PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixDilateGray3(PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixOpenGray3(PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseGray3(PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixDitherToBinary(PIX* pixs);
         internal static  extern PIX* pixDitherToBinarySpec(PIX* pixs, int lowerclip, int upperclip);
         internal static  extern PIX* pixThresholdToBinary(PIX* pixs, int thresh);
         internal static  extern PIX* pixVarThresholdToBinary(PIX* pixs, PIX* pixg);
         internal static  extern PIX* pixAdaptThresholdToBinary(PIX* pixs, PIX* pixm, float gamma);
         internal static  extern PIX* pixAdaptThresholdToBinaryGen(PIX* pixs, PIX* pixm, float gamma, int blackval, int whiteval, int thresh);
         internal static  extern PIX* pixGenerateMaskByValue(PIX* pixs, int val, int usecmap);
         internal static  extern PIX* pixGenerateMaskByBand(PIX* pixs, int lower, int upper, int inband, int usecmap);
         internal static  extern PIX* pixDitherTo2bpp(PIX* pixs, int cmapflag);
         internal static  extern PIX* pixDitherTo2bppSpec(PIX* pixs, int lowerclip, int upperclip, int cmapflag);
         internal static  extern PIX* pixThresholdTo2bpp(PIX* pixs, int nlevels, int cmapflag);
         internal static  extern PIX* pixThresholdTo4bpp(PIX* pixs, int nlevels, int cmapflag);
         internal static  extern PIX* pixThresholdOn8bpp(PIX* pixs, int nlevels, int cmapflag);
         internal static  extern PIX* pixThresholdGrayArb(PIX* pixs, const char* edgevals, int outdepth, int use_average, int setblack, int setwhite );
         internal static  extern l_int32* makeGrayQuantIndexTable(int nlevels);
         internal static  extern l_int32* makeGrayQuantTargetTable(int nlevels, int depth);
         internal static  extern int makeGrayQuantTableArb(NUMA* na, int outdepth, l_int32** ptab, PIXCMAP** pcmap);
         internal static  extern int makeGrayQuantColormapArb(PIX* pixs, l_int32* tab, int outdepth, PIXCMAP** pcmap);
         internal static  extern PIX* pixGenerateMaskByBand32(PIX* pixs, uint refval, int delm, int delp, float fractm, float fractp);
         internal static  extern PIX* pixGenerateMaskByDiscr32(PIX* pixs, uint refval1, uint refval2, int distflag);
         internal static  extern PIX* pixGrayQuantFromHisto(PIX* pixd, PIX* pixs, PIX* pixm, float minfract, int maxsize);
         internal static  extern PIX* pixGrayQuantFromCmap(PIX* pixs, PIXCMAP* cmap, int mindepth);
         internal static  extern void ditherToBinaryLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, l_uint32* bufs1, l_uint32* bufs2, int lowerclip, int upperclip);
         internal static  extern void ditherToBinaryLineLow(l_uint32* lined, int w, l_uint32* bufs1, l_uint32* bufs2, int lowerclip, int upperclip, int lastlineflag);
         internal static  extern void thresholdToBinaryLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int d, int wpls, int thresh);
         internal static  extern void thresholdToBinaryLineLow(l_uint32* lined, int w, l_uint32* lines, int d, int thresh);
         internal static  extern void ditherTo2bppLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, l_uint32* bufs1, l_uint32* bufs2, l_int32* tabval, l_int32* tab38, l_int32* tab14);
         internal static  extern void ditherTo2bppLineLow(l_uint32* lined, int w, l_uint32* bufs1, l_uint32* bufs2, l_int32* tabval, l_int32* tab38, l_int32* tab14, int lastlineflag);
         internal static  extern int make8To2DitherTables(l_int32** ptabval, l_int32** ptab38, l_int32** ptab14, int cliptoblack, int cliptowhite);
         internal static  extern void thresholdTo2bppLow(l_uint32* datad, int h, int wpld, l_uint32* datas, int wpls, l_int32* tab);
         internal static  extern void thresholdTo4bppLow(l_uint32* datad, int h, int wpld, l_uint32* datas, int wpls, l_int32* tab);
         internal static  extern L_HEAP* lheapCreate(int nalloc, int direction);
         internal static  extern void lheapDestroy(L_HEAP** plh, int freeflag);
         internal static  extern int lheapAdd(L_HEAP* lh, void* item);
         internal static  extern void* lheapRemove(L_HEAP* lh);
         internal static  extern int lheapGetCount(L_HEAP* lh);
         internal static  extern int lheapSwapUp(L_HEAP* lh, int index);
         internal static  extern int lheapSwapDown(L_HEAP* lh);
         internal static  extern int lheapSort(L_HEAP* lh);
         internal static  extern int lheapSortStrictOrder(L_HEAP* lh);
         internal static  extern int lheapPrint(FILE* fp, L_HEAP* lh);
         internal static  extern JBCLASSER* jbRankHausInit(int components, int maxwidth, int maxheight, int size, float rank);
         internal static  extern JBCLASSER* jbCorrelationInit(int components, int maxwidth, int maxheight, float thresh, float weightfactor);
         internal static  extern JBCLASSER* jbCorrelationInitWithoutComponents(int components, int maxwidth, int maxheight, float thresh, float weightfactor);
         internal static  extern int jbAddPages(JBCLASSER* classer, SARRAY* safiles);
         internal static  extern int jbAddPage(JBCLASSER* classer, PIX* pixs);
         internal static  extern int jbAddPageComponents(JBCLASSER* classer, PIX* pixs, HandleRef boxas, HandleRef pixas);
         internal static  extern int jbClassifyRankHaus(JBCLASSER* classer, HandleRef boxa, HandleRef pixas);
         internal static  extern int pixHaustest(PIX* pix1, PIX* pix2, PIX* pix3, PIX* pix4, float delx, float dely, int maxdiffw, int maxdiffh);
         internal static  extern int pixRankHaustest(PIX* pix1, PIX* pix2, PIX* pix3, PIX* pix4, float delx, float dely, int maxdiffw, int maxdiffh, int area1, int area3, float rank, l_int32* tab8);
         internal static  extern int jbClassifyCorrelation(JBCLASSER* classer, HandleRef boxa, HandleRef pixas);
         internal static  extern int jbGetComponents(PIX* pixs, int components, int maxwidth, int maxheight, BOXA** pboxad, PIXA** ppixad);
         internal static  extern int pixWordMaskByDilation(PIX* pixs, int maxdil, PIX** ppixm, l_int32* psize);
         internal static  extern int pixWordBoxesByDilation(PIX* pixs, int maxdil, int minwidth, int minheight, int maxwidth, int maxheight, BOXA** pboxa, l_int32* psize);
         internal static  extern HandleRef jbAccumulateComposites(PIXAA* pixaa, NUMA** pna, PTA** pptat);
         internal static  extern HandleRef jbTemplatesFromComposites(PIXA* pixac, NUMA* na);
         internal static  extern JBCLASSER* jbClasserCreate(int method, int components);
         internal static  extern void jbClasserDestroy(JBCLASSER** pclasser);
         internal static  extern JBDATA* jbDataSave(JBCLASSER* classer);
         internal static  extern void jbDataDestroy(JBDATA** pdata);
         internal static  extern int jbDataWrite( const char* rootout, JBDATA *jbdata );
         internal static  extern JBDATA* jbDataRead( const char* rootname );
         internal static  extern HandleRef jbDataRender(JBDATA* data, int debugflag);
         internal static  extern int jbGetULCorners(JBCLASSER* classer, PIX* pixs, HandleRef boxa);
         internal static  extern int jbGetLLCorners(JBCLASSER* classer);
         internal static  extern int readHeaderJp2k( const char* filename, int *pw, l_int32* ph, int *pbps, l_int32* pspp );
         internal static  extern int freadHeaderJp2k(FILE* fp, l_int32* pw, l_int32* ph, l_int32* pbps, l_int32* pspp);
         internal static  extern int readHeaderMemJp2k( const l_uint8* data, size_t size, l_int32* pw, int *ph, l_int32* pbps, int *pspp );
         internal static  extern int fgetJp2kResolution(FILE* fp, l_int32* pxres, l_int32* pyres);
         internal static  extern PIX* pixReadJp2k( const char* filename, uint reduction, HandleRef box, int hint, int debug );
         internal static  extern PIX* pixReadStreamJp2k(FILE* fp, uint reduction, HandleRef box, int hint, int debug);
         internal static  extern int pixWriteJp2k( const char* filename, PIX *pix, int quality, int nlevels, int hint, int debug );
         internal static  extern int pixWriteStreamJp2k(FILE* fp, PIX* pix, int quality, int nlevels, int hint, int debug);
         internal static  extern PIX* pixReadMemJp2k( const l_uint8* data, size_t size, uint reduction, BOX *box, int hint, int debug );
         internal static  extern int pixWriteMemJp2k(l_uint8** pdata, size_t* psize, PIX* pix, int quality, int nlevels, int hint, int debug);
         internal static  extern PIX* pixReadJpeg( const char* filename, int cmapflag, int reduction, int *pnwarn, int hint );
         internal static  extern PIX* pixReadStreamJpeg(FILE* fp, int cmapflag, int reduction, l_int32* pnwarn, int hint);
         internal static  extern int readHeaderJpeg( const char* filename, int *pw, l_int32* ph, int *pspp, l_int32* pycck, int *pcmyk );
         internal static  extern int freadHeaderJpeg(FILE* fp, l_int32* pw, l_int32* ph, l_int32* pspp, l_int32* pycck, l_int32* pcmyk);
         internal static  extern int fgetJpegResolution(FILE* fp, l_int32* pxres, l_int32* pyres);
         internal static  extern int fgetJpegComment(FILE* fp, l_uint8** pcomment);
         internal static  extern int pixWriteJpeg( const char* filename, PIX *pix, int quality, int progressive );
         internal static  extern int pixWriteStreamJpeg(FILE* fp, PIX* pixs, int quality, int progressive);
         internal static  extern PIX* pixReadMemJpeg( const l_uint8* data, size_t size, int cmflag, int reduction, l_int32* pnwarn, int hint );
         internal static  extern int readHeaderMemJpeg( const l_uint8* data, size_t size, l_int32* pw, int *ph, l_int32* pspp, int *pycck, l_int32* pcmyk );
         internal static  extern int pixWriteMemJpeg(l_uint8** pdata, size_t* psize, PIX* pix, int quality, int progressive);
         internal static  extern int pixSetChromaSampling(PIX* pix, int sampling);
         internal static  extern L_KERNEL* kernelCreate(int height, int width);
         internal static  extern void kernelDestroy(L_KERNEL** pkel);
         internal static  extern L_KERNEL* kernelCopy(L_KERNEL* kels);
         internal static  extern int kernelGetElement(L_KERNEL* kel, int row, int col, l_float32* pval);
         internal static  extern int kernelSetElement(L_KERNEL* kel, int row, int col, float val);
         internal static  extern int kernelGetParameters(L_KERNEL* kel, l_int32* psy, l_int32* psx, l_int32* pcy, l_int32* pcx);
         internal static  extern int kernelSetOrigin(L_KERNEL* kel, int cy, int cx);
         internal static  extern int kernelGetSum(L_KERNEL* kel, l_float32* psum);
         internal static  extern int kernelGetMinMax(L_KERNEL* kel, l_float32* pmin, l_float32* pmax);
         internal static  extern L_KERNEL* kernelNormalize(L_KERNEL* kels, float normsum);
         internal static  extern L_KERNEL* kernelInvert(L_KERNEL* kels);
         internal static  extern l_float32** create2dFloatArray(int sy, int sx);
         internal static  extern L_KERNEL* kernelRead( const char* fname );
         internal static  extern L_KERNEL* kernelReadStream(FILE* fp);
         internal static  extern int kernelWrite( const char* fname, L_KERNEL *kel );
         internal static  extern int kernelWriteStream(FILE* fp, L_KERNEL* kel);
         internal static  extern L_KERNEL* kernelCreateFromString(int h, int w, int cy, int cx, const char* kdata );
         internal static  extern L_KERNEL* kernelCreateFromFile( const char* filename );
         internal static  extern L_KERNEL* kernelCreateFromPix(PIX* pix, int cy, int cx);
         internal static  extern PIX* kernelDisplayInPix(L_KERNEL* kel, int size, int gthick);
         internal static  extern NUMA* parseStringForNumbers( const char* str, const char* seps );
         internal static  extern L_KERNEL* makeFlatKernel(int height, int width, int cy, int cx);
         internal static  extern L_KERNEL* makeGaussianKernel(int halfheight, int halfwidth, float stdev, float max);
         internal static  extern int makeGaussianKernelSep(int halfheight, int halfwidth, float stdev, float max, L_KERNEL** pkelx, L_KERNEL** pkely);
         internal static  extern L_KERNEL* makeDoGKernel(int halfheight, int halfwidth, float stdev, float ratio);
         internal static  extern char* getImagelibVersions();
         internal static  extern void listDestroy(DLLIST** phead);
         internal static  extern int listAddToHead(DLLIST** phead, void* data);
         internal static  extern int listAddToTail(DLLIST** phead, DLLIST** ptail, void* data);
         internal static  extern int listInsertBefore(DLLIST** phead, DLLIST* elem, void* data);
         internal static  extern int listInsertAfter(DLLIST** phead, DLLIST* elem, void* data);
         internal static  extern void* listRemoveElement(DLLIST** phead, DLLIST* elem);
         internal static  extern void* listRemoveFromHead(DLLIST** phead);
         internal static  extern void* listRemoveFromTail(DLLIST** phead, DLLIST** ptail);
         internal static  extern DLLIST* listFindElement(DLLIST* head, void* data);
         internal static  extern DLLIST* listFindTail(DLLIST* head);
         internal static  extern int listGetCount(DLLIST* head);
         internal static  extern int listReverse(DLLIST** phead);
         internal static  extern int listJoin(DLLIST** phead1, DLLIST** phead2);
         internal static  extern L_AMAP* l_amapCreate(int keytype);
         internal static  extern RB_TYPE* l_amapFind(L_AMAP* m, RB_TYPE key);
         internal static  extern void l_amapInsert(L_AMAP* m, RB_TYPE key, RB_TYPE value);
         internal static  extern void l_amapDelete(L_AMAP* m, RB_TYPE key);
         internal static  extern void l_amapDestroy(L_AMAP** pm);
         internal static  extern L_AMAP_NODE* l_amapGetFirst(L_AMAP* m);
         internal static  extern L_AMAP_NODE* l_amapGetNext(L_AMAP_NODE* n);
         internal static  extern L_AMAP_NODE* l_amapGetLast(L_AMAP* m);
         internal static  extern L_AMAP_NODE* l_amapGetPrev(L_AMAP_NODE* n);
         internal static  extern int l_amapSize(L_AMAP* m);
         internal static  extern L_ASET* l_asetCreate(int keytype);
         internal static  extern RB_TYPE* l_asetFind(L_ASET* s, RB_TYPE key);
         internal static  extern void l_asetInsert(L_ASET* s, RB_TYPE key);
         internal static  extern void l_asetDelete(L_ASET* s, RB_TYPE key);
         internal static  extern void l_asetDestroy(L_ASET** ps);
         internal static  extern L_ASET_NODE* l_asetGetFirst(L_ASET* s);
         internal static  extern L_ASET_NODE* l_asetGetNext(L_ASET_NODE* n);
         internal static  extern L_ASET_NODE* l_asetGetLast(L_ASET* s);
         internal static  extern L_ASET_NODE* l_asetGetPrev(L_ASET_NODE* n);
         internal static  extern int l_asetSize(L_ASET* s);
         internal static  extern PIX* generateBinaryMaze(int w, int h, int xi, int yi, float wallps, float ranis);
         internal static  extern PTA* pixSearchBinaryMaze(PIX* pixs, int xi, int yi, int xf, int yf, PIX** ppixd);
         internal static  extern PTA* pixSearchGrayMaze(PIX* pixs, int xi, int yi, int xf, int yf, PIX** ppixd);
         internal static  extern PIX* pixDilate(PIX* pixd, PIX* pixs, SEL* sel);
         internal static  extern PIX* pixErode(PIX* pixd, PIX* pixs, SEL* sel);
         internal static  extern PIX* pixHMT(PIX* pixd, PIX* pixs, SEL* sel);
         internal static  extern PIX* pixOpen(PIX* pixd, PIX* pixs, SEL* sel);
         internal static  extern PIX* pixClose(PIX* pixd, PIX* pixs, SEL* sel);
         internal static  extern PIX* pixCloseSafe(PIX* pixd, PIX* pixs, SEL* sel);
         internal static  extern PIX* pixOpenGeneralized(PIX* pixd, PIX* pixs, SEL* sel);
         internal static  extern PIX* pixCloseGeneralized(PIX* pixd, PIX* pixs, SEL* sel);
         internal static  extern PIX* pixDilateBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixErodeBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixOpenBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseSafeBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern int selectComposableSels(int size, int direction, SEL** psel1, SEL** psel2);
         internal static  extern int selectComposableSizes(int size, l_int32* pfactor1, l_int32* pfactor2);
         internal static  extern PIX* pixDilateCompBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixErodeCompBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixOpenCompBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseCompBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseSafeCompBrick(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern void resetMorphBoundaryCondition(int bc);
         internal static  extern uint getMorphBorderPixelColor(int type, int depth);
         internal static  extern PIX* pixExtractBoundary(PIX* pixs, int type);
         internal static  extern PIX* pixMorphSequenceMasked(PIX* pixs, PIX* pixm, const char* sequence, int dispsep );
         internal static  extern PIX* pixMorphSequenceByComponent(PIX* pixs, const char* sequence, int connectivity, int minw, int minh, BOXA** pboxa );
         internal static  extern HandleRef pixaMorphSequenceByComponent(PIXA* pixas, const char* sequence, int minw, int minh );
         internal static  extern PIX* pixMorphSequenceByRegion(PIX* pixs, PIX* pixm, const char* sequence, int connectivity, int minw, int minh, BOXA** pboxa );
         internal static  extern HandleRef pixaMorphSequenceByRegion(PIX* pixs, HandleRef pixam, const char* sequence, int minw, int minh );
         internal static  extern PIX* pixUnionOfMorphOps(PIX* pixs, SELA* sela, int type);
         internal static  extern PIX* pixIntersectionOfMorphOps(PIX* pixs, SELA* sela, int type);
         internal static  extern PIX* pixSelectiveConnCompFill(PIX* pixs, int connectivity, int minw, int minh);
         internal static  extern int pixRemoveMatchedPattern(PIX* pixs, PIX* pixp, PIX* pixe, int x0, int y0, int dsize);
         internal static  extern PIX* pixDisplayMatchedPattern(PIX* pixs, PIX* pixp, PIX* pixe, int x0, int y0, uint color, float scale, int nlevels);
         internal static  extern HandleRef pixaExtendByMorph(PIXA* pixas, int type, int niters, SEL* sel, int include);
         internal static  extern HandleRef pixaExtendByScaling(PIXA* pixas, NUMA* nasc, int type, int include);
         internal static  extern PIX* pixSeedfillMorph(PIX* pixs, PIX* pixm, int maxiters, int connectivity);
         internal static  extern NUMA* pixRunHistogramMorph(PIX* pixs, int runtype, int direction, int maxsize);
         internal static  extern PIX* pixTophat(PIX* pixs, int hsize, int vsize, int type);
         internal static  extern PIX* pixHDome(PIX* pixs, int height, int connectivity);
         internal static  extern PIX* pixFastTophat(PIX* pixs, int xsize, int ysize, int type);
         internal static  extern PIX* pixMorphGradient(PIX* pixs, int hsize, int vsize, int smoothing);
         internal static  extern PTA* pixaCentroids(PIXA* pixa);
         internal static  extern int pixCentroid(PIX* pix, l_int32* centtab, l_int32* sumtab, l_float32* pxave, l_float32* pyave);
         internal static  extern PIX* pixDilateBrickDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixErodeBrickDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixOpenBrickDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseBrickDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixDilateCompBrickDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixErodeCompBrickDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixOpenCompBrickDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseCompBrickDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixDilateCompBrickExtendDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixErodeCompBrickExtendDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixOpenCompBrickExtendDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern PIX* pixCloseCompBrickExtendDwa(PIX* pixd, PIX* pixs, int hsize, int vsize);
         internal static  extern int getExtendedCompositeParameters(int size, l_int32* pn, l_int32* pextra, l_int32* pactualsize);
         internal static  extern PIX* pixMorphSequence(PIX* pixs, const char* sequence, int dispsep );
         internal static  extern PIX* pixMorphCompSequence(PIX* pixs, const char* sequence, int dispsep );
         internal static  extern PIX* pixMorphSequenceDwa(PIX* pixs, const char* sequence, int dispsep );
         internal static  extern PIX* pixMorphCompSequenceDwa(PIX* pixs, const char* sequence, int dispsep );
         internal static  extern int morphSequenceVerify(SARRAY* sa);
         internal static  extern PIX* pixGrayMorphSequence(PIX* pixs, const char* sequence, int dispsep, int dispy );
         internal static  extern PIX* pixColorMorphSequence(PIX* pixs, const char* sequence, int dispsep, int dispy );
         internal static  extern NUMA* numaCreate(int n);
         internal static  extern NUMA* numaCreateFromIArray(l_int32* iarray, int size);
         internal static  extern NUMA* numaCreateFromFArray(l_float32* farray, int size, int copyflag);
         internal static  extern NUMA* numaCreateFromString( const char* str );
         internal static  extern void numaDestroy(NUMA** pna);
         internal static  extern NUMA* numaCopy(NUMA* na);
         internal static  extern NUMA* numaClone(NUMA* na);
         internal static  extern int numaEmpty(NUMA* na);
         internal static  extern int numaAddNumber(NUMA* na, float val);
         internal static  extern int numaInsertNumber(NUMA* na, int index, float val);
         internal static  extern int numaRemoveNumber(NUMA* na, int index);
         internal static  extern int numaReplaceNumber(NUMA* na, int index, float val);
         internal static  extern int numaGetCount(NUMA* na);
         internal static  extern int numaSetCount(NUMA* na, int newcount);
         internal static  extern int numaGetFValue(NUMA* na, int index, l_float32* pval);
         internal static  extern int numaGetIValue(NUMA* na, int index, l_int32* pival);
         internal static  extern int numaSetValue(NUMA* na, int index, float val);
         internal static  extern int numaShiftValue(NUMA* na, int index, float diff);
         internal static  extern l_int32* numaGetIArray(NUMA* na);
         internal static  extern l_float32* numaGetFArray(NUMA* na, int copyflag);
         internal static  extern int numaGetRefcount(NUMA* na);
         internal static  extern int numaChangeRefcount(NUMA* na, int delta);
         internal static  extern int numaGetParameters(NUMA* na, l_float32* pstartx, l_float32* pdelx);
         internal static  extern int numaSetParameters(NUMA* na, float startx, float delx);
         internal static  extern int numaCopyParameters(NUMA* nad, NUMA* nas);
         internal static  extern SARRAY* numaConvertToSarray(NUMA* na, int size1, int size2, int addzeros, int type);
         internal static  extern NUMA* numaRead( const char* filename );
         internal static  extern NUMA* numaReadStream(FILE* fp);
         internal static  extern NUMA* numaReadMem( const l_uint8* data, size_t size );
         internal static  extern int numaWrite( const char* filename, NUMA *na );
         internal static  extern int numaWriteStream(FILE* fp, NUMA* na);
         internal static  extern int numaWriteMem(l_uint8** pdata, size_t* psize, NUMA* na);
         internal static  extern NUMAA* numaaCreate(int n);
         internal static  extern NUMAA* numaaCreateFull(int nptr, int n);
         internal static  extern int numaaTruncate(NUMAA* naa);
         internal static  extern void numaaDestroy(NUMAA** pnaa);
         internal static  extern int numaaAddNuma(NUMAA* naa, NUMA* na, int copyflag);
         internal static  extern int numaaGetCount(NUMAA* naa);
         internal static  extern int numaaGetNumaCount(NUMAA* naa, int index);
         internal static  extern int numaaGetNumberCount(NUMAA* naa);
         internal static  extern NUMA** numaaGetPtrArray(NUMAA* naa);
         internal static  extern NUMA* numaaGetNuma(NUMAA* naa, int index, int accessflag);
         internal static  extern int numaaReplaceNuma(NUMAA* naa, int index, NUMA* na);
         internal static  extern int numaaGetValue(NUMAA* naa, int i, int j, l_float32* pfval, l_int32* pival);
         internal static  extern int numaaAddNumber(NUMAA* naa, int index, float val);
         internal static  extern NUMAA* numaaRead( const char* filename );
         internal static  extern NUMAA* numaaReadStream(FILE* fp);
         internal static  extern NUMAA* numaaReadMem( const l_uint8* data, size_t size );
         internal static  extern int numaaWrite( const char* filename, NUMAA *naa );
         internal static  extern int numaaWriteStream(FILE* fp, NUMAA* naa);
         internal static  extern int numaaWriteMem(l_uint8** pdata, size_t* psize, NUMAA* naa);
         internal static  extern NUMA* numaArithOp(NUMA* nad, NUMA* na1, NUMA* na2, int op);
         internal static  extern NUMA* numaLogicalOp(NUMA* nad, NUMA* na1, NUMA* na2, int op);
         internal static  extern NUMA* numaInvert(NUMA* nad, NUMA* nas);
         internal static  extern int numaSimilar(NUMA* na1, NUMA* na2, float maxdiff, l_int32* psimilar);
         internal static  extern int numaAddToNumber(NUMA* na, int index, float val);
         internal static  extern int numaGetMin(NUMA* na, l_float32* pminval, l_int32* piminloc);
         internal static  extern int numaGetMax(NUMA* na, l_float32* pmaxval, l_int32* pimaxloc);
         internal static  extern int numaGetSum(NUMA* na, l_float32* psum);
         internal static  extern NUMA* numaGetPartialSums(NUMA* na);
         internal static  extern int numaGetSumOnInterval(NUMA* na, int first, int last, l_float32* psum);
         internal static  extern int numaHasOnlyIntegers(NUMA* na, int maxsamples, l_int32* pallints);
         internal static  extern NUMA* numaSubsample(NUMA* nas, int subfactor);
         internal static  extern NUMA* numaMakeDelta(NUMA* nas);
         internal static  extern NUMA* numaMakeSequence(float startval, float increment, int size);
         internal static  extern NUMA* numaMakeConstant(float val, int size);
         internal static  extern NUMA* numaMakeAbsValue(NUMA* nad, NUMA* nas);
         internal static  extern NUMA* numaAddBorder(NUMA* nas, int left, int right, float val);
         internal static  extern NUMA* numaAddSpecifiedBorder(NUMA* nas, int left, int right, int type);
         internal static  extern NUMA* numaRemoveBorder(NUMA* nas, int left, int right);
         internal static  extern int numaCountNonzeroRuns(NUMA* na, l_int32* pcount);
         internal static  extern int numaGetNonzeroRange(NUMA* na, float eps, l_int32* pfirst, l_int32* plast);
         internal static  extern int numaGetCountRelativeToZero(NUMA* na, int type, l_int32* pcount);
         internal static  extern NUMA* numaClipToInterval(NUMA* nas, int first, int last);
         internal static  extern NUMA* numaMakeThresholdIndicator(NUMA* nas, float thresh, int type);
         internal static  extern NUMA* numaUniformSampling(NUMA* nas, int nsamp);
         internal static  extern NUMA* numaReverse(NUMA* nad, NUMA* nas);
         internal static  extern NUMA* numaLowPassIntervals(NUMA* nas, float thresh, float maxn);
         internal static  extern NUMA* numaThresholdEdges(NUMA* nas, float thresh1, float thresh2, float maxn);
         internal static  extern int numaGetSpanValues(NUMA* na, int span, l_int32* pstart, l_int32* pend);
         internal static  extern int numaGetEdgeValues(NUMA* na, int edge, l_int32* pstart, l_int32* pend, l_int32* psign);
         internal static  extern int numaInterpolateEqxVal(float startx, float deltax, NUMA* nay, int type, float xval, l_float32* pyval);
         internal static  extern int numaInterpolateArbxVal(NUMA* nax, NUMA* nay, int type, float xval, l_float32* pyval);
         internal static  extern int numaInterpolateEqxInterval(float startx, float deltax, NUMA* nasy, int type, float x0, float x1, int npts, NUMA** pnax, NUMA** pnay);
         internal static  extern int numaInterpolateArbxInterval(NUMA* nax, NUMA* nay, int type, float x0, float x1, int npts, NUMA** pnadx, NUMA** pnady);
         internal static  extern int numaFitMax(NUMA* na, l_float32* pmaxval, NUMA* naloc, l_float32* pmaxloc);
         internal static  extern int numaDifferentiateInterval(NUMA* nax, NUMA* nay, float x0, float x1, int npts, NUMA** pnadx, NUMA** pnady);
         internal static  extern int numaIntegrateInterval(NUMA* nax, NUMA* nay, float x0, float x1, int npts, l_float32* psum);
         internal static  extern int numaSortGeneral(NUMA* na, NUMA** pnasort, NUMA** pnaindex, NUMA** pnainvert, int sortorder, int sorttype);
         internal static  extern NUMA* numaSortAutoSelect(NUMA* nas, int sortorder);
         internal static  extern NUMA* numaSortIndexAutoSelect(NUMA* nas, int sortorder);
         internal static  extern int numaChooseSortType(NUMA* nas);
         internal static  extern NUMA* numaSort(NUMA* naout, NUMA* nain, int sortorder);
         internal static  extern NUMA* numaBinSort(NUMA* nas, int sortorder);
         internal static  extern NUMA* numaGetSortIndex(NUMA* na, int sortorder);
         internal static  extern NUMA* numaGetBinSortIndex(NUMA* nas, int sortorder);
         internal static  extern NUMA* numaSortByIndex(NUMA* nas, NUMA* naindex);
         internal static  extern int numaIsSorted(NUMA* nas, int sortorder, l_int32* psorted);
         internal static  extern int numaSortPair(NUMA* nax, NUMA* nay, int sortorder, NUMA** pnasx, NUMA** pnasy);
         internal static  extern NUMA* numaInvertMap(NUMA* nas);
         internal static  extern NUMA* numaPseudorandomSequence(int size, int seed);
         internal static  extern NUMA* numaRandomPermutation(NUMA* nas, int seed);
         internal static  extern int numaGetRankValue(NUMA* na, float fract, NUMA* nasort, int usebins, l_float32* pval);
         internal static  extern int numaGetMedian(NUMA* na, l_float32* pval);
         internal static  extern int numaGetBinnedMedian(NUMA* na, l_int32* pval);
         internal static  extern int numaGetMode(NUMA* na, l_float32* pval, l_int32* pcount);
         internal static  extern int numaGetMedianVariation(NUMA* na, l_float32* pmedval, l_float32* pmedvar);
         internal static  extern int numaJoin(NUMA* nad, NUMA* nas, int istart, int iend);
         internal static  extern int numaaJoin(NUMAA* naad, NUMAA* naas, int istart, int iend);
         internal static  extern NUMA* numaaFlattenToNuma(NUMAA* naa);
         internal static  extern NUMA* numaErode(NUMA* nas, int size);
         internal static  extern NUMA* numaDilate(NUMA* nas, int size);
         internal static  extern NUMA* numaOpen(NUMA* nas, int size);
         internal static  extern NUMA* numaClose(NUMA* nas, int size);
         internal static  extern NUMA* numaTransform(NUMA* nas, float shift, float scale);
         internal static  extern int numaSimpleStats(NUMA* na, int first, int last, l_float32* pmean, l_float32* pvar, l_float32* prvar);
         internal static  extern int numaWindowedStats(NUMA* nas, int wc, NUMA** pnam, NUMA** pnams, NUMA** pnav, NUMA** pnarv);
         internal static  extern NUMA* numaWindowedMean(NUMA* nas, int wc);
         internal static  extern NUMA* numaWindowedMeanSquare(NUMA* nas, int wc);
         internal static  extern int numaWindowedVariance(NUMA* nam, NUMA* nams, NUMA** pnav, NUMA** pnarv);
         internal static  extern NUMA* numaWindowedMedian(NUMA* nas, int halfwin);
         internal static  extern NUMA* numaConvertToInt(NUMA* nas);
         internal static  extern NUMA* numaMakeHistogram(NUMA* na, int maxbins, l_int32* pbinsize, l_int32* pbinstart);
         internal static  extern NUMA* numaMakeHistogramAuto(NUMA* na, int maxbins);
         internal static  extern NUMA* numaMakeHistogramClipped(NUMA* na, float binsize, float maxsize);
         internal static  extern NUMA* numaRebinHistogram(NUMA* nas, int newsize);
         internal static  extern NUMA* numaNormalizeHistogram(NUMA* nas, float tsum);
         internal static  extern int numaGetStatsUsingHistogram(NUMA* na, int maxbins, l_float32* pmin, l_float32* pmax, l_float32* pmean, l_float32* pvariance, l_float32* pmedian, float rank, l_float32* prval, NUMA** phisto);
         internal static  extern int numaGetHistogramStats(NUMA* nahisto, float startx, float deltax, l_float32* pxmean, l_float32* pxmedian, l_float32* pxmode, l_float32* pxvariance);
         internal static  extern int numaGetHistogramStatsOnInterval(NUMA* nahisto, float startx, float deltax, int ifirst, int ilast, l_float32* pxmean, l_float32* pxmedian, l_float32* pxmode, l_float32* pxvariance);
         internal static  extern int numaMakeRankFromHistogram(float startx, float deltax, NUMA* nasy, int npts, NUMA** pnax, NUMA** pnay);
         internal static  extern int numaHistogramGetRankFromVal(NUMA* na, float rval, l_float32* prank);
         internal static  extern int numaHistogramGetValFromRank(NUMA* na, float rank, l_float32* prval);
         internal static  extern int numaDiscretizeRankAndIntensity(NUMA* na, int nbins, NUMA** pnarbin, NUMA** pnam, NUMA** pnar, NUMA** pnabb);
         internal static  extern int numaGetRankBinValues(NUMA* na, int nbins, NUMA** pnarbin, NUMA** pnam);
         internal static  extern int numaSplitDistribution(NUMA* na, float scorefract, l_int32* psplitindex, l_float32* pave1, l_float32* pave2, l_float32* pnum1, l_float32* pnum2, NUMA** pnascore);
         internal static  extern int grayHistogramsToEMD(NUMAA* naa1, NUMAA* naa2, NUMA** pnad);
         internal static  extern int numaEarthMoverDistance(NUMA* na1, NUMA* na2, l_float32* pdist);
         internal static  extern int grayInterHistogramStats(NUMAA* naa, int wc, NUMA** pnam, NUMA** pnams, NUMA** pnav, NUMA** pnarv);
         internal static  extern NUMA* numaFindPeaks(NUMA* nas, int nmax, float fract1, float fract2);
         internal static  extern NUMA* numaFindExtrema(NUMA* nas, float delta, NUMA** pnav);
         internal static  extern int numaCountReversals(NUMA* nas, float minreversal, l_int32* pnr, l_float32* pnrpl);
         internal static  extern int numaSelectCrossingThreshold(NUMA* nax, NUMA* nay, float estthresh, l_float32* pbestthresh);
         internal static  extern NUMA* numaCrossingsByThreshold(NUMA* nax, NUMA* nay, float thresh);
         internal static  extern NUMA* numaCrossingsByPeaks(NUMA* nax, NUMA* nay, float delta);
         internal static  extern int numaEvalBestHaarParameters(NUMA* nas, float relweight, int nwidth, int nshift, float minwidth, float maxwidth, l_float32* pbestwidth, l_float32* pbestshift, l_float32* pbestscore);
         internal static  extern int numaEvalHaarSum(NUMA* nas, float width, float shift, float relweight, l_float32* pscore);
         internal static  extern NUMA* genConstrainedNumaInRange(int first, int last, int nmax, int use_pairs);
         internal static  extern int pixGetRegionsBinary(PIX* pixs, PIX** ppixhm, PIX** ppixtm, PIX** ppixtb, HandleRef pixadb);
         internal static  extern PIX* pixGenHalftoneMask(PIX* pixs, PIX** ppixtext, l_int32* phtfound, int debug);
         internal static  extern PIX* pixGenerateHalftoneMask(PIX* pixs, PIX** ppixtext, l_int32* phtfound, HandleRef pixadb);
         internal static  extern PIX* pixGenTextlineMask(PIX* pixs, PIX** ppixvws, l_int32* ptlfound, HandleRef pixadb);
         internal static  extern PIX* pixGenTextblockMask(PIX* pixs, PIX* pixvws, HandleRef pixadb);
         internal static  extern HandleRef pixFindPageForeground(PIX* pixs, int threshold, int mindist, int erasedist, int pagenum, int showmorph, int display, const char* pdfdir );
         internal static  extern int pixSplitIntoCharacters(PIX* pixs, int minw, int minh, BOXA** pboxa, PIXA** ppixa, PIX** ppixdebug);
         internal static  extern HandleRef pixSplitComponentWithProfile(PIX* pixs, int delta, int mindel, PIX** ppixdebug);
         internal static  extern HandleRef pixExtractTextlines(PIX* pixs, int maxw, int maxh, int minw, int minh, int adjw, int adjh, HandleRef pixadb);
         internal static  extern HandleRef pixExtractRawTextlines(PIX* pixs, int maxw, int maxh, int adjw, int adjh, HandleRef pixadb);
         internal static  extern int pixCountTextColumns(PIX* pixs, float deltafract, float peakfract, float clipfract, l_int32* pncols, HandleRef pixadb);
         internal static  extern int pixDecideIfText(PIX* pixs, HandleRef box, l_int32* pistext, HandleRef pixadb);
         internal static  extern int pixFindThreshFgExtent(PIX* pixs, int thresh, l_int32* ptop, l_int32* pbot);
         internal static  extern int pixDecideIfTable(PIX* pixs, HandleRef box, l_int32* pistable, HandleRef pixadb);
         internal static  extern PIX* pixPrepare1bpp(PIX* pixs, HandleRef box, float cropfract, int outres);
         internal static  extern int pixEstimateBackground(PIX* pixs, int darkthresh, float edgecrop, l_int32* pbg);
         internal static  extern int pixFindLargeRectangles(PIX* pixs, int polarity, int nrect, BOXA** pboxa, PIX** ppixdb);
         internal static  extern int pixFindLargestRectangle(PIX* pixs, int polarity, BOX** pbox, PIX** ppixdb);
         internal static  extern int pixSetSelectCmap(PIX* pixs, HandleRef box, int sindex, int rval, int gval, int bval);
         internal static  extern int pixColorGrayRegionsCmap(PIX* pixs, HandleRef boxa, int type, int rval, int gval, int bval);
         internal static  extern int pixColorGrayCmap(PIX* pixs, HandleRef box, int type, int rval, int gval, int bval);
         internal static  extern int pixColorGrayMaskedCmap(PIX* pixs, PIX* pixm, int type, int rval, int gval, int bval);
         internal static  extern int addColorizedGrayToCmap(PIXCMAP* cmap, int type, int rval, int gval, int bval, NUMA** pna);
         internal static  extern int pixSetSelectMaskedCmap(PIX* pixs, PIX* pixm, int x, int y, int sindex, int rval, int gval, int bval);
         internal static  extern int pixSetMaskedCmap(PIX* pixs, PIX* pixm, int x, int y, int rval, int gval, int bval);
         internal static  extern char* parseForProtos( const char* filein, const char* prestring );
         internal static  extern HandleRef boxaGetWhiteblocks(HandleRef boxas, HandleRef box, int sortflag, int maxboxes, float maxoverlap, int maxperim, float fract, int maxpops);
         internal static  extern HandleRef boxaPruneSortedOnOverlap(HandleRef boxas, float maxoverlap);
         internal static  extern int convertFilesToPdf( const char* dirname, const char* substr, int res, float scalefactor, int type, int quality, const char* title, const char* fileout );
         internal static  extern int saConvertFilesToPdf(SARRAY* sa, int res, float scalefactor, int type, int quality, const char* title, const char* fileout );
         internal static  extern int saConvertFilesToPdfData(SARRAY* sa, int res, float scalefactor, int type, int quality, const char* title, byte **pdata, size_t* pnbytes );
         internal static  extern int selectDefaultPdfEncoding(PIX* pix, l_int32* ptype);
         internal static  extern int convertUnscaledFilesToPdf( const char* dirname, const char* substr, const char* title, const char* fileout );
         internal static  extern int saConvertUnscaledFilesToPdf(SARRAY* sa, const char* title, const char* fileout );
         internal static  extern int saConvertUnscaledFilesToPdfData(SARRAY* sa, const char* title, byte **pdata, size_t* pnbytes );
         internal static  extern int convertUnscaledToPdfData( const char* fname, const char* title, byte **pdata, size_t* pnbytes );
         internal static  extern int pixaConvertToPdf(PIXA* pixa, int res, float scalefactor, int type, int quality, const char* title, const char* fileout );
         internal static  extern int pixaConvertToPdfData(PIXA* pixa, int res, float scalefactor, int type, int quality, const char* title, byte **pdata, size_t* pnbytes );
         internal static  extern int convertToPdf( const char* filein, int type, int quality, const char* fileout, int x, int y, int res, const char* title, L_PDF_DATA **plpd, int position );
         internal static  extern int convertImageDataToPdf(l_uint8* imdata, size_t size, int type, int quality, const char* fileout, int x, int y, int res, const char* title, L_PDF_DATA **plpd, int position );
         internal static  extern int convertToPdfData( const char* filein, int type, int quality, byte **pdata, size_t* pnbytes, int x, int y, int res, const char* title, L_PDF_DATA **plpd, int position );
         internal static  extern int convertImageDataToPdfData(l_uint8* imdata, size_t size, int type, int quality, l_uint8** pdata, size_t* pnbytes, int x, int y, int res, const char* title, L_PDF_DATA **plpd, int position );
         internal static  extern int pixConvertToPdf(PIX* pix, int type, int quality, const char* fileout, int x, int y, int res, const char* title, L_PDF_DATA **plpd, int position );
         internal static  extern int pixWriteStreamPdf(FILE* fp, PIX* pix, int res, const char* title );
         internal static  extern int pixWriteMemPdf(l_uint8** pdata, size_t* pnbytes, PIX* pix, int res, const char* title );
         internal static  extern int convertSegmentedFilesToPdf( const char* dirname, const char* substr, int res, int type, int thresh, BOXAA* baa, int quality, float scalefactor, const char* title, const char* fileout );
         internal static  extern BOXAA* convertNumberedMasksToBoxaa( const char* dirname, const char* substr, int numpre, int numpost );
         internal static  extern int convertToPdfSegmented( const char* filein, int res, int type, int thresh, HandleRef boxa, int quality, float scalefactor, const char* title, const char* fileout );
         internal static  extern int pixConvertToPdfSegmented(PIX* pixs, int res, int type, int thresh, HandleRef boxa, int quality, float scalefactor, const char* title, const char* fileout );
         internal static  extern int convertToPdfDataSegmented( const char* filein, int res, int type, int thresh, HandleRef boxa, int quality, float scalefactor, const char* title, byte **pdata, size_t* pnbytes );
         internal static  extern int pixConvertToPdfDataSegmented(PIX* pixs, int res, int type, int thresh, HandleRef boxa, int quality, float scalefactor, const char* title, byte **pdata, size_t* pnbytes );
         internal static  extern int concatenatePdf( const char* dirname, const char* substr, const char* fileout );
         internal static  extern int saConcatenatePdf(SARRAY* sa, const char* fileout );
         internal static  extern int ptraConcatenatePdf(L_PTRA* pa, const char* fileout );
         internal static  extern int concatenatePdfToData( const char* dirname, const char* substr, byte **pdata, size_t* pnbytes );
         internal static  extern int saConcatenatePdfToData(SARRAY* sa, l_uint8** pdata, size_t* pnbytes);
         internal static  extern int pixConvertToPdfData(PIX* pix, int type, int quality, l_uint8** pdata, size_t* pnbytes, int x, int y, int res, const char* title, L_PDF_DATA **plpd, int position );
         internal static  extern int ptraConcatenatePdfToData(L_PTRA* pa_data, SARRAY* sa, l_uint8** pdata, size_t* pnbytes);
         internal static  extern int convertTiffMultipageToPdf( const char* filein, const char* fileout );
         internal static  extern int l_generateCIDataForPdf( const char* fname, PIX *pix, int quality, L_COMP_DATA **pcid );
         internal static  extern L_COMP_DATA* l_generateFlateDataPdf( const char* fname, PIX *pixs );
         internal static  extern L_COMP_DATA* l_generateJpegData( const char* fname, int ascii85flag );
         internal static  extern int l_generateCIData( const char* fname, int type, int quality, int ascii85, L_COMP_DATA** pcid );
         internal static  extern int pixGenerateCIData(PIX* pixs, int type, int quality, int ascii85, L_COMP_DATA** pcid);
         internal static  extern L_COMP_DATA* l_generateFlateData( const char* fname, int ascii85flag );
         internal static  extern L_COMP_DATA* l_generateG4Data( const char* fname, int ascii85flag );
         internal static  extern int cidConvertToPdfData(L_COMP_DATA* cid, const char* title, byte **pdata, size_t* pnbytes );
         internal static  extern void l_CIDataDestroy(L_COMP_DATA** pcid);
         internal static  extern void l_pdfSetG4ImageMask(int flag);
         internal static  extern void l_pdfSetDateAndVersion(int flag);
         internal static  extern void setPixMemoryManager(alloc_fn allocator, dealloc_fn deallocator);
         internal static  extern PIX* pixCreate(int width, int height, int depth);
         internal static  extern PIX* pixCreateNoInit(int width, int height, int depth);
         internal static  extern PIX* pixCreateTemplate(PIX* pixs);
         internal static  extern PIX* pixCreateTemplateNoInit(PIX* pixs);
         internal static  extern PIX* pixCreateHeader(int width, int height, int depth);
         internal static  extern PIX* pixClone(PIX* pixs);
         internal static  extern void pixDestroy(PIX** ppix);
         internal static  extern PIX* pixCopy(PIX* pixd, PIX* pixs);
         internal static  extern int pixResizeImageData(PIX* pixd, PIX* pixs);
         internal static  extern int pixCopyColormap(PIX* pixd, PIX* pixs);
         internal static  extern int pixSizesEqual(PIX* pix1, PIX* pix2);
         internal static  extern int pixTransferAllData(PIX* pixd, PIX** ppixs, int copytext, int copyformat);
         internal static  extern int pixSwapAndDestroy(PIX** ppixd, PIX** ppixs);
         internal static  extern int pixGetWidth(PIX* pix);
         internal static  extern int pixSetWidth(PIX* pix, int width);
         internal static  extern int pixGetHeight(PIX* pix);
         internal static  extern int pixSetHeight(PIX* pix, int height);
         internal static  extern int pixGetDepth(PIX* pix);
         internal static  extern int pixSetDepth(PIX* pix, int depth);
         internal static  extern int pixGetDimensions(PIX* pix, l_int32* pw, l_int32* ph, l_int32* pd);
         internal static  extern int pixSetDimensions(PIX* pix, int w, int h, int d);
         internal static  extern int pixCopyDimensions(PIX* pixd, PIX* pixs);
         internal static  extern int pixGetSpp(PIX* pix);
         internal static  extern int pixSetSpp(PIX* pix, int spp);
         internal static  extern int pixCopySpp(PIX* pixd, PIX* pixs);
         internal static  extern int pixGetWpl(PIX* pix);
         internal static  extern int pixSetWpl(PIX* pix, int wpl);
         internal static  extern int pixGetRefcount(PIX* pix);
         internal static  extern int pixChangeRefcount(PIX* pix, int delta);
         internal static  extern int pixGetXRes(PIX* pix);
         internal static  extern int pixSetXRes(PIX* pix, int res);
         internal static  extern int pixGetYRes(PIX* pix);
         internal static  extern int pixSetYRes(PIX* pix, int res);
         internal static  extern int pixGetResolution(PIX* pix, l_int32* pxres, l_int32* pyres);
         internal static  extern int pixSetResolution(PIX* pix, int xres, int yres);
         internal static  extern int pixCopyResolution(PIX* pixd, PIX* pixs);
         internal static  extern int pixScaleResolution(PIX* pix, float xscale, float yscale);
         internal static  extern int pixGetInputFormat(PIX* pix);
         internal static  extern int pixSetInputFormat(PIX* pix, int informat);
         internal static  extern int pixCopyInputFormat(PIX* pixd, PIX* pixs);
         internal static  extern int pixSetSpecial(PIX* pix, int special);
         internal static  extern char* pixGetText(PIX* pix);
         internal static  extern int pixSetText(PIX* pix, const char* textstring );
         internal static  extern int pixAddText(PIX* pix, const char* textstring );
         internal static  extern int pixCopyText(PIX* pixd, PIX* pixs);
         internal static  extern PIXCMAP* pixGetColormap(PIX* pix);
         internal static  extern int pixSetColormap(PIX* pix, PIXCMAP* colormap);
         internal static  extern int pixDestroyColormap(PIX* pix);
         internal static  extern l_uint32* pixGetData(PIX* pix);
         internal static  extern int pixSetData(PIX* pix, l_uint32* data);
         internal static  extern l_uint32* pixExtractData(PIX* pixs);
         internal static  extern int pixFreeData(PIX* pix);
         internal static  extern void** pixGetLinePtrs(PIX* pix, l_int32* psize);
         internal static  extern int pixPrintStreamInfo(FILE* fp, PIX* pix, const char* text );
         internal static  extern int pixGetPixel(PIX* pix, int x, int y, l_uint32* pval);
         internal static  extern int pixSetPixel(PIX* pix, int x, int y, uint val);
         internal static  extern int pixGetRGBPixel(PIX* pix, int x, int y, l_int32* prval, l_int32* pgval, l_int32* pbval);
         internal static  extern int pixSetRGBPixel(PIX* pix, int x, int y, int rval, int gval, int bval);
         internal static  extern int pixGetRandomPixel(PIX* pix, l_uint32* pval, l_int32* px, l_int32* py);
         internal static  extern int pixClearPixel(PIX* pix, int x, int y);
         internal static  extern int pixFlipPixel(PIX* pix, int x, int y);
         internal static  extern void setPixelLow(l_uint32* line, int x, int depth, uint val);
         internal static  extern int pixGetBlackOrWhiteVal(PIX* pixs, int op, l_uint32* pval);
         internal static  extern int pixClearAll(PIX* pix);
         internal static  extern int pixSetAll(PIX* pix);
         internal static  extern int pixSetAllGray(PIX* pix, int grayval);
         internal static  extern int pixSetAllArbitrary(PIX* pix, uint val);
         internal static  extern int pixSetBlackOrWhite(PIX* pixs, int op);
         internal static  extern int pixSetComponentArbitrary(PIX* pix, int comp, int val);
         internal static  extern int pixClearInRect(PIX* pix, HandleRef box);
         internal static  extern int pixSetInRect(PIX* pix, HandleRef box);
         internal static  extern int pixSetInRectArbitrary(PIX* pix, HandleRef box, uint val);
         internal static  extern int pixBlendInRect(PIX* pixs, HandleRef box, uint val, float fract);
         internal static  extern int pixSetPadBits(PIX* pix, int val);
         internal static  extern int pixSetPadBitsBand(PIX* pix, int by, int bh, int val);
         internal static  extern int pixSetOrClearBorder(PIX* pixs, int left, int right, int top, int bot, int op);
         internal static  extern int pixSetBorderVal(PIX* pixs, int left, int right, int top, int bot, uint val);
         internal static  extern int pixSetBorderRingVal(PIX* pixs, int dist, uint val);
         internal static  extern int pixSetMirroredBorder(PIX* pixs, int left, int right, int top, int bot);
         internal static  extern PIX* pixCopyBorder(PIX* pixd, PIX* pixs, int left, int right, int top, int bot);
         internal static  extern PIX* pixAddBorder(PIX* pixs, int npix, uint val);
         internal static  extern PIX* pixAddBlackOrWhiteBorder(PIX* pixs, int left, int right, int top, int bot, int op);
         internal static  extern PIX* pixAddBorderGeneral(PIX* pixs, int left, int right, int top, int bot, uint val);
         internal static  extern PIX* pixRemoveBorder(PIX* pixs, int npix);
         internal static  extern PIX* pixRemoveBorderGeneral(PIX* pixs, int left, int right, int top, int bot);
         internal static  extern PIX* pixRemoveBorderToSize(PIX* pixs, int wd, int hd);
         internal static  extern PIX* pixAddMirroredBorder(PIX* pixs, int left, int right, int top, int bot);
         internal static  extern PIX* pixAddRepeatedBorder(PIX* pixs, int left, int right, int top, int bot);
         internal static  extern PIX* pixAddMixedBorder(PIX* pixs, int left, int right, int top, int bot);
         internal static  extern PIX* pixAddContinuedBorder(PIX* pixs, int left, int right, int top, int bot);
         internal static  extern int pixShiftAndTransferAlpha(PIX* pixd, PIX* pixs, float shiftx, float shifty);
         internal static  extern PIX* pixDisplayLayersRGBA(PIX* pixs, uint val, int maxw);
         internal static  extern PIX* pixCreateRGBImage(PIX* pixr, PIX* pixg, PIX* pixb);
         internal static  extern PIX* pixGetRGBComponent(PIX* pixs, int comp);
         internal static  extern int pixSetRGBComponent(PIX* pixd, PIX* pixs, int comp);
         internal static  extern PIX* pixGetRGBComponentCmap(PIX* pixs, int comp);
         internal static  extern int pixCopyRGBComponent(PIX* pixd, PIX* pixs, int comp);
         internal static  extern int composeRGBPixel(int rval, int gval, int bval, l_uint32* ppixel);
         internal static  extern int composeRGBAPixel(int rval, int gval, int bval, int aval, l_uint32* ppixel);
         internal static  extern void extractRGBValues(l_uint32 pixel, l_int32* prval, l_int32* pgval, l_int32* pbval);
         internal static  extern void extractRGBAValues(l_uint32 pixel, l_int32* prval, l_int32* pgval, l_int32* pbval, l_int32* paval);
         internal static  extern int extractMinMaxComponent(l_uint32 pixel, int type);
         internal static  extern int pixGetRGBLine(PIX* pixs, int row, l_uint8* bufr, l_uint8* bufg, l_uint8* bufb);
         internal static  extern PIX* pixEndianByteSwapNew(PIX* pixs);
         internal static  extern int pixEndianByteSwap(PIX* pixs);
         internal static  extern int lineEndianByteSwap(l_uint32* datad, l_uint32* datas, int wpl);
         internal static  extern PIX* pixEndianTwoByteSwapNew(PIX* pixs);
         internal static  extern int pixEndianTwoByteSwap(PIX* pixs);
         internal static  extern int pixGetRasterData(PIX* pixs, l_uint8** pdata, size_t* pnbytes);
         internal static  extern int pixAlphaIsOpaque(PIX* pix, l_int32* popaque);
         internal static  extern l_uint8** pixSetupByteProcessing(PIX* pix, l_int32* pw, l_int32* ph);
         internal static  extern int pixCleanupByteProcessing(PIX* pix, l_uint8** lineptrs);
         internal static  extern void l_setAlphaMaskBorder(float val1, float val2);
         internal static  extern int pixSetMasked(PIX* pixd, PIX* pixm, uint val);
         internal static  extern int pixSetMaskedGeneral(PIX* pixd, PIX* pixm, uint val, int x, int y);
         internal static  extern int pixCombineMasked(PIX* pixd, PIX* pixs, PIX* pixm);
         internal static  extern int pixCombineMaskedGeneral(PIX* pixd, PIX* pixs, PIX* pixm, int x, int y);
         internal static  extern int pixPaintThroughMask(PIX* pixd, PIX* pixm, int x, int y, uint val);
         internal static  extern int pixPaintSelfThroughMask(PIX* pixd, PIX* pixm, int x, int y, int searchdir, int mindist, int tilesize, int ntiles, int distblend);
         internal static  extern PIX* pixMakeMaskFromVal(PIX* pixs, int val);
         internal static  extern PIX* pixMakeMaskFromLUT(PIX* pixs, l_int32* tab);
         internal static  extern PIX* pixMakeArbMaskFromRGB(PIX* pixs, float rc, float gc, float bc, float thresh);
         internal static  extern PIX* pixSetUnderTransparency(PIX* pixs, uint val, int debug);
         internal static  extern PIX* pixMakeAlphaFromMask(PIX* pixs, int dist, BOX** pbox);
         internal static  extern int pixGetColorNearMaskBoundary(PIX* pixs, PIX* pixm, HandleRef box, int dist, l_uint32* pval, int debug);
         internal static  extern PIX* pixInvert(PIX* pixd, PIX* pixs);
         internal static  extern PIX* pixOr(PIX* pixd, PIX* pixs1, PIX* pixs2);
         internal static  extern PIX* pixAnd(PIX* pixd, PIX* pixs1, PIX* pixs2);
         internal static  extern PIX* pixXor(PIX* pixd, PIX* pixs1, PIX* pixs2);
         internal static  extern PIX* pixSubtract(PIX* pixd, PIX* pixs1, PIX* pixs2);
         internal static  extern int pixZero(PIX* pix, l_int32* pempty);
         internal static  extern int pixForegroundFraction(PIX* pix, l_float32* pfract);
         internal static  extern NUMA* pixaCountPixels(PIXA* pixa);
         internal static  extern int pixCountPixels(PIX* pix, l_int32* pcount, l_int32* tab8);
         internal static  extern NUMA* pixCountByRow(PIX* pix, HandleRef box);
         internal static  extern NUMA* pixCountByColumn(PIX* pix, HandleRef box);
         internal static  extern NUMA* pixCountPixelsByRow(PIX* pix, l_int32* tab8);
         internal static  extern NUMA* pixCountPixelsByColumn(PIX* pix);
         internal static  extern int pixCountPixelsInRow(PIX* pix, int row, l_int32* pcount, l_int32* tab8);
         internal static  extern NUMA* pixGetMomentByColumn(PIX* pix, int order);
         internal static  extern int pixThresholdPixelSum(PIX* pix, int thresh, l_int32* pabove, l_int32* tab8);
         internal static  extern l_int32* makePixelSumTab8(void );
         internal static  extern l_int32* makePixelCentroidTab8(void );
         internal static  extern NUMA* pixAverageByRow(PIX* pix, HandleRef box, int type);
         internal static  extern NUMA* pixAverageByColumn(PIX* pix, HandleRef box, int type);
         internal static  extern int pixAverageInRect(PIX* pix, HandleRef box, l_float32* pave);
         internal static  extern NUMA* pixVarianceByRow(PIX* pix, HandleRef box);
         internal static  extern NUMA* pixVarianceByColumn(PIX* pix, HandleRef box);
         internal static  extern int pixVarianceInRect(PIX* pix, HandleRef box, l_float32* prootvar);
         internal static  extern NUMA* pixAbsDiffByRow(PIX* pix, HandleRef box);
         internal static  extern NUMA* pixAbsDiffByColumn(PIX* pix, HandleRef box);
         internal static  extern int pixAbsDiffInRect(PIX* pix, HandleRef box, int dir, l_float32* pabsdiff);
         internal static  extern int pixAbsDiffOnLine(PIX* pix, int x1, int y1, int x2, int y2, l_float32* pabsdiff);
         internal static  extern int pixCountArbInRect(PIX* pixs, HandleRef box, int val, int factor, l_int32* pcount);
         internal static  extern PIX* pixMirroredTiling(PIX* pixs, int w, int h);
         internal static  extern int pixFindRepCloseTile(PIX* pixs, HandleRef box, int searchdir, int mindist, int tsize, int ntiles, BOX** pboxtile, int debug);
         internal static  extern NUMA* pixGetGrayHistogram(PIX* pixs, int factor);
         internal static  extern NUMA* pixGetGrayHistogramMasked(PIX* pixs, PIX* pixm, int x, int y, int factor);
         internal static  extern NUMA* pixGetGrayHistogramInRect(PIX* pixs, HandleRef box, int factor);
         internal static  extern NUMAA* pixGetGrayHistogramTiled(PIX* pixs, int factor, int nx, int ny);
         internal static  extern int pixGetColorHistogram(PIX* pixs, int factor, NUMA** pnar, NUMA** pnag, NUMA** pnab);
         internal static  extern int pixGetColorHistogramMasked(PIX* pixs, PIX* pixm, int x, int y, int factor, NUMA** pnar, NUMA** pnag, NUMA** pnab);
         internal static  extern NUMA* pixGetCmapHistogram(PIX* pixs, int factor);
         internal static  extern NUMA* pixGetCmapHistogramMasked(PIX* pixs, PIX* pixm, int x, int y, int factor);
         internal static  extern NUMA* pixGetCmapHistogramInRect(PIX* pixs, HandleRef box, int factor);
         internal static  extern int pixCountRGBColors(PIX* pixs);
         internal static  extern L_AMAP* pixGetColorAmapHistogram(PIX* pixs, int factor);
         internal static  extern int amapGetCountForColor(L_AMAP* amap, uint val);
         internal static  extern int pixGetRankValue(PIX* pixs, int factor, float rank, l_uint32* pvalue);
         internal static  extern int pixGetRankValueMaskedRGB(PIX* pixs, PIX* pixm, int x, int y, int factor, float rank, l_float32* prval, l_float32* pgval, l_float32* pbval);
         internal static  extern int pixGetRankValueMasked(PIX* pixs, PIX* pixm, int x, int y, int factor, float rank, l_float32* pval, NUMA** pna);
         internal static  extern int pixGetAverageValue(PIX* pixs, int factor, int type, l_uint32* pvalue);
         internal static  extern int pixGetAverageMaskedRGB(PIX* pixs, PIX* pixm, int x, int y, int factor, int type, l_float32* prval, l_float32* pgval, l_float32* pbval);
         internal static  extern int pixGetAverageMasked(PIX* pixs, PIX* pixm, int x, int y, int factor, int type, l_float32* pval);
         internal static  extern int pixGetAverageTiledRGB(PIX* pixs, int sx, int sy, int type, PIX** ppixr, PIX** ppixg, PIX** ppixb);
         internal static  extern PIX* pixGetAverageTiled(PIX* pixs, int sx, int sy, int type);
         internal static  extern int pixRowStats(PIX* pixs, HandleRef box, NUMA** pnamean, NUMA** pnamedian, NUMA** pnamode, NUMA** pnamodecount, NUMA** pnavar, NUMA** pnarootvar);
         internal static  extern int pixColumnStats(PIX* pixs, HandleRef box, NUMA** pnamean, NUMA** pnamedian, NUMA** pnamode, NUMA** pnamodecount, NUMA** pnavar, NUMA** pnarootvar);
         internal static  extern int pixGetRangeValues(PIX* pixs, int factor, int color, l_int32* pminval, l_int32* pmaxval);
         internal static  extern int pixGetExtremeValue(PIX* pixs, int factor, int type, l_int32* prval, l_int32* pgval, l_int32* pbval, l_int32* pgrayval);
         internal static  extern int pixGetMaxValueInRect(PIX* pixs, HandleRef box, l_uint32* pmaxval, l_int32* pxmax, l_int32* pymax);
         internal static  extern int pixGetBinnedComponentRange(PIX* pixs, int nbins, int factor, int color, l_int32* pminval, l_int32* pmaxval, l_uint32** pcarray, int fontsize);
         internal static  extern int pixGetRankColorArray(PIX* pixs, int nbins, int type, int factor, l_uint32** pcarray, int debugflag, int fontsize);
         internal static  extern int pixGetBinnedColor(PIX* pixs, PIX* pixg, int factor, int nbins, NUMA* nalut, l_uint32** pcarray, int debugflag);
         internal static  extern PIX* pixDisplayColorArray(l_uint32* carray, int ncolors, int side, int ncols, int fontsize);
         internal static  extern PIX* pixRankBinByStrip(PIX* pixs, int direction, int size, int nbins, int type);
         internal static  extern PIX* pixaGetAlignedStats(PIXA* pixa, int type, int nbins, int thresh);
         internal static  extern int pixaExtractColumnFromEachPix(PIXA* pixa, int col, PIX* pixd);
         internal static  extern int pixGetRowStats(PIX* pixs, int type, int nbins, int thresh, l_float32* colvect);
         internal static  extern int pixGetColumnStats(PIX* pixs, int type, int nbins, int thresh, l_float32* rowvect);
         internal static  extern int pixSetPixelColumn(PIX* pix, int col, l_float32* colvect);
         internal static  extern int pixThresholdForFgBg(PIX* pixs, int factor, int thresh, l_int32* pfgval, l_int32* pbgval);
         internal static  extern int pixSplitDistributionFgBg(PIX* pixs, float scorefract, int factor, l_int32* pthresh, l_int32* pfgval, l_int32* pbgval, PIX** ppixdb);
         internal static  extern int pixaFindDimensions(PIXA* pixa, NUMA** pnaw, NUMA** pnah);
         internal static  extern int pixFindAreaPerimRatio(PIX* pixs, l_int32* tab, l_float32* pfract);
         internal static  extern NUMA* pixaFindPerimToAreaRatio(PIXA* pixa);
         internal static  extern int pixFindPerimToAreaRatio(PIX* pixs, l_int32* tab, l_float32* pfract);
         internal static  extern NUMA* pixaFindPerimSizeRatio(PIXA* pixa);
         internal static  extern int pixFindPerimSizeRatio(PIX* pixs, l_int32* tab, l_float32* pratio);
         internal static  extern NUMA* pixaFindAreaFraction(PIXA* pixa);
         internal static  extern int pixFindAreaFraction(PIX* pixs, l_int32* tab, l_float32* pfract);
         internal static  extern NUMA* pixaFindAreaFractionMasked(PIXA* pixa, PIX* pixm, int debug);
         internal static  extern int pixFindAreaFractionMasked(PIX* pixs, HandleRef box, PIX* pixm, l_int32* tab, l_float32* pfract);
         internal static  extern NUMA* pixaFindWidthHeightRatio(PIXA* pixa);
         internal static  extern NUMA* pixaFindWidthHeightProduct(PIXA* pixa);
         internal static  extern int pixFindOverlapFraction(PIX* pixs1, PIX* pixs2, int x2, int y2, l_int32* tab, l_float32* pratio, l_int32* pnoverlap);
         internal static  extern HandleRef pixFindRectangleComps(PIX* pixs, int dist, int minw, int minh);
         internal static  extern int pixConformsToRectangle(PIX* pixs, HandleRef box, int dist, l_int32* pconforms);
         internal static  extern HandleRef pixClipRectangles(PIX* pixs, HandleRef boxa);
         internal static  extern PIX* pixClipRectangle(PIX* pixs, HandleRef box, BOX** pboxc);
         internal static  extern PIX* pixClipMasked(PIX* pixs, PIX* pixm, int x, int y, uint outval);
         internal static  extern int pixCropToMatch(PIX* pixs1, PIX* pixs2, PIX** ppixd1, PIX** ppixd2);
         internal static  extern PIX* pixCropToSize(PIX* pixs, int w, int h);
         internal static  extern PIX* pixResizeToMatch(PIX* pixs, PIX* pixt, int w, int h);
         internal static  extern PIX* pixMakeFrameMask(int w, int h, float hf1, float hf2, float vf1, float vf2);
         internal static  extern int pixFractionFgInMask(PIX* pix1, PIX* pix2, l_float32* pfract);
         internal static  extern int pixClipToForeground(PIX* pixs, PIX** ppixd, BOX** pbox);
         internal static  extern int pixTestClipToForeground(PIX* pixs, l_int32* pcanclip);
         internal static  extern int pixClipBoxToForeground(PIX* pixs, HandleRef boxs, PIX** ppixd, BOX** pboxd);
         internal static  extern int pixScanForForeground(PIX* pixs, HandleRef box, int scanflag, l_int32* ploc);
         internal static  extern int pixClipBoxToEdges(PIX* pixs, HandleRef boxs, int lowthresh, int highthresh, int maxwidth, int factor, PIX** ppixd, BOX** pboxd);
         internal static  extern int pixScanForEdge(PIX* pixs, HandleRef box, int lowthresh, int highthresh, int maxwidth, int factor, int scanflag, l_int32* ploc);
         internal static  extern NUMA* pixExtractOnLine(PIX* pixs, int x1, int y1, int x2, int y2, int factor);
         internal static  extern float pixAverageOnLine(PIX* pixs, int x1, int y1, int x2, int y2, int factor);
         internal static  extern NUMA* pixAverageIntensityProfile(PIX* pixs, float fract, int dir, int first, int last, int factor1, int factor2);
         internal static  extern NUMA* pixReversalProfile(PIX* pixs, float fract, int dir, int first, int last, int minreversal, int factor1, int factor2);
         internal static  extern int pixWindowedVarianceOnLine(PIX* pixs, int dir, int loc, int c1, int c2, int size, NUMA** pnad);
         internal static  extern int pixMinMaxNearLine(PIX* pixs, int x1, int y1, int x2, int y2, int dist, int direction, NUMA** pnamin, NUMA** pnamax, l_float32* pminave, l_float32* pmaxave);
         internal static  extern PIX* pixRankRowTransform(PIX* pixs);
         internal static  extern PIX* pixRankColumnTransform(PIX* pixs);
         internal static  extern HandleRef pixaCreate(int n);
         internal static  extern HandleRef pixaCreateFromPix(PIX* pixs, int n, int cellw, int cellh);
         internal static  extern HandleRef pixaCreateFromBoxa(PIX* pixs, HandleRef boxa, l_int32* pcropwarn);
         internal static  extern HandleRef pixaSplitPix(PIX* pixs, int nx, int ny, int borderwidth, uint bordercolor);
         internal static  extern void pixaDestroy(PIXA** ppixa);
         internal static  extern HandleRef pixaCopy(PIXA* pixa, int copyflag);
         internal static  extern int pixaAddPix(PIXA* pixa, PIX* pix, int copyflag);
         internal static  extern int pixaAddBox(PIXA* pixa, HandleRef box, int copyflag);
         internal static  extern int pixaExtendArrayToSize(PIXA* pixa, int size);
         internal static  extern int pixaGetCount(PIXA* pixa);
         internal static  extern int pixaChangeRefcount(PIXA* pixa, int delta);
         internal static  extern PIX* pixaGetPix(PIXA* pixa, int index, int accesstype);
         internal static  extern int pixaGetPixDimensions(PIXA* pixa, int index, l_int32* pw, l_int32* ph, l_int32* pd);
         internal static  extern HandleRef pixaGetBoxa(PIXA* pixa, int accesstype);
         internal static  extern int pixaGetBoxaCount(PIXA* pixa);
         internal static  extern HandleRef pixaGetBox(PIXA* pixa, int index, int accesstype);
         internal static  extern int pixaGetBoxGeometry(PIXA* pixa, int index, l_int32* px, l_int32* py, l_int32* pw, l_int32* ph);
         internal static  extern int pixaSetBoxa(PIXA* pixa, HandleRef boxa, int accesstype);
         internal static  extern PIX** pixaGetPixArray(PIXA* pixa);
         internal static  extern int pixaVerifyDepth(PIXA* pixa, l_int32* pmaxdepth);
         internal static  extern int pixaIsFull(PIXA* pixa, l_int32* pfullpa, l_int32* pfullba);
         internal static  extern int pixaCountText(PIXA* pixa, l_int32* pntext);
         internal static  extern int pixaSetText(PIXA* pixa, SARRAY* sa);
         internal static  extern void*** pixaGetLinePtrs(PIXA* pixa, l_int32* psize);
         internal static  extern int pixaWriteStreamInfo(FILE* fp, HandleRef pixa);
         internal static  extern int pixaReplacePix(PIXA* pixa, int index, PIX* pix, HandleRef box);
         internal static  extern int pixaInsertPix(PIXA* pixa, int index, PIX* pixs, HandleRef box);
         internal static  extern int pixaRemovePix(PIXA* pixa, int index);
         internal static  extern int pixaRemovePixAndSave(PIXA* pixa, int index, PIX** ppix, BOX** pbox);
         internal static  extern int pixaInitFull(PIXA* pixa, PIX* pix, HandleRef box);
         internal static  extern int pixaClear(PIXA* pixa);
         internal static  extern int pixaJoin(PIXA* pixad, HandleRef pixas, int istart, int iend);
         internal static  extern HandleRef pixaInterleave(PIXA* pixa1, HandleRef pixa2, int copyflag);
         internal static  extern int pixaaJoin(PIXAA* paad, PIXAA* paas, int istart, int iend);
         internal static  extern PIXAA* pixaaCreate(int n);
         internal static  extern PIXAA* pixaaCreateFromPixa(PIXA* pixa, int n, int type, int copyflag);
         internal static  extern void pixaaDestroy(PIXAA** ppaa);
         internal static  extern int pixaaAddPixa(PIXAA* paa, HandleRef pixa, int copyflag);
         internal static  extern int pixaaExtendArray(PIXAA* paa);
         internal static  extern int pixaaAddPix(PIXAA* paa, int index, PIX* pix, HandleRef box, int copyflag);
         internal static  extern int pixaaAddBox(PIXAA* paa, HandleRef box, int copyflag);
         internal static  extern int pixaaGetCount(PIXAA* paa, NUMA** pna);
         internal static  extern HandleRef pixaaGetPixa(PIXAA* paa, int index, int accesstype);
         internal static  extern HandleRef pixaaGetBoxa(PIXAA* paa, int accesstype);
         internal static  extern PIX* pixaaGetPix(PIXAA* paa, int index, int ipix, int accessflag);
         internal static  extern int pixaaVerifyDepth(PIXAA* paa, l_int32* pmaxdepth);
         internal static  extern int pixaaIsFull(PIXAA* paa, l_int32* pfull);
         internal static  extern int pixaaInitFull(PIXAA* paa, HandleRef pixa);
         internal static  extern int pixaaReplacePixa(PIXAA* paa, int index, HandleRef pixa);
         internal static  extern int pixaaClear(PIXAA* paa);
         internal static  extern int pixaaTruncate(PIXAA* paa);
         internal static  extern HandleRef pixaRead( const char* filename );
         internal static  extern HandleRef pixaReadStream(FILE* fp);
         internal static  extern HandleRef pixaReadMem( const l_uint8* data, size_t size );
         internal static  extern int pixaWrite( const char* filename, PIXA *pixa );
         internal static  extern int pixaWriteStream(FILE* fp, HandleRef pixa);
         internal static  extern int pixaWriteMem(l_uint8** pdata, size_t* psize, HandleRef pixa);
         internal static  extern HandleRef pixaReadBoth( const char* filename );
         internal static  extern PIXAA* pixaaReadFromFiles( const char* dirname, const char* substr, int first, int nfiles );
         internal static  extern PIXAA* pixaaRead( const char* filename );
         internal static  extern PIXAA* pixaaReadStream(FILE* fp);
         internal static  extern PIXAA* pixaaReadMem( const l_uint8* data, size_t size );
         internal static  extern int pixaaWrite( const char* filename, PIXAA *paa );
         internal static  extern int pixaaWriteStream(FILE* fp, PIXAA* paa);
         internal static  extern int pixaaWriteMem(l_uint8** pdata, size_t* psize, PIXAA* paa);
         internal static  extern PIXACC* pixaccCreate(int w, int h, int negflag);
         internal static  extern PIXACC* pixaccCreateFromPix(PIX* pix, int negflag);
         internal static  extern void pixaccDestroy(PIXACC** ppixacc);
         internal static  extern PIX* pixaccFinal(PIXACC* pixacc, int outdepth);
         internal static  extern PIX* pixaccGetPix(PIXACC* pixacc);
         internal static  extern int pixaccGetOffset(PIXACC* pixacc);
         internal static  extern int pixaccAdd(PIXACC* pixacc, PIX* pix);
         internal static  extern int pixaccSubtract(PIXACC* pixacc, PIX* pix);
         internal static  extern int pixaccMultConst(PIXACC* pixacc, float factor);
         internal static  extern int pixaccMultConstAccumulate(PIXACC* pixacc, PIX* pix, float factor);
         internal static  extern PIX* pixSelectBySize(PIX* pixs, int width, int height, int connectivity, int type, int relation, l_int32* pchanged);
         internal static  extern HandleRef pixaSelectBySize(PIXA* pixas, int width, int height, int type, int relation, l_int32* pchanged);
         internal static  extern NUMA* pixaMakeSizeIndicator(PIXA* pixa, int width, int height, int type, int relation);
         internal static  extern PIX* pixSelectByPerimToAreaRatio(PIX* pixs, float thresh, int connectivity, int type, l_int32* pchanged);
         internal static  extern HandleRef pixaSelectByPerimToAreaRatio(PIXA* pixas, float thresh, int type, l_int32* pchanged);
         internal static  extern PIX* pixSelectByPerimSizeRatio(PIX* pixs, float thresh, int connectivity, int type, l_int32* pchanged);
         internal static  extern HandleRef pixaSelectByPerimSizeRatio(PIXA* pixas, float thresh, int type, l_int32* pchanged);
         internal static  extern PIX* pixSelectByAreaFraction(PIX* pixs, float thresh, int connectivity, int type, l_int32* pchanged);
         internal static  extern HandleRef pixaSelectByAreaFraction(PIXA* pixas, float thresh, int type, l_int32* pchanged);
         internal static  extern PIX* pixSelectByWidthHeightRatio(PIX* pixs, float thresh, int connectivity, int type, l_int32* pchanged);
         internal static  extern HandleRef pixaSelectByWidthHeightRatio(PIXA* pixas, float thresh, int type, l_int32* pchanged);
         internal static  extern HandleRef pixaSelectByNumConnComp(PIXA* pixas, int nmin, int nmax, int connectivity, l_int32* pchanged);
         internal static  extern HandleRef pixaSelectWithIndicator(PIXA* pixas, NUMA* na, l_int32* pchanged);
         internal static  extern int pixRemoveWithIndicator(PIX* pixs, HandleRef pixa, NUMA* na);
         internal static  extern int pixAddWithIndicator(PIX* pixs, HandleRef pixa, NUMA* na);
         internal static  extern HandleRef pixaSelectWithString(PIXA* pixas, const char* str, int *perror );
         internal static  extern PIX* pixaRenderComponent(PIX* pixs, HandleRef pixa, int index);
         internal static  extern HandleRef pixaSort(PIXA* pixas, int sorttype, int sortorder, NUMA** pnaindex, int copyflag);
         internal static  extern HandleRef pixaBinSort(PIXA* pixas, int sorttype, int sortorder, NUMA** pnaindex, int copyflag);
         internal static  extern HandleRef pixaSortByIndex(PIXA* pixas, NUMA* naindex, int copyflag);
         internal static  extern PIXAA* pixaSort2dByIndex(PIXA* pixas, NUMAA* naa, int copyflag);
         internal static  extern HandleRef pixaSelectRange(PIXA* pixas, int first, int last, int copyflag);
         internal static  extern PIXAA* pixaaSelectRange(PIXAA* paas, int first, int last, int copyflag);
         internal static  extern PIXAA* pixaaScaleToSize(PIXAA* paas, int wd, int hd);
         internal static  extern PIXAA* pixaaScaleToSizeVar(PIXAA* paas, NUMA* nawd, NUMA* nahd);
         internal static  extern HandleRef pixaScaleToSize(PIXA* pixas, int wd, int hd);
         internal static  extern HandleRef pixaScaleToSizeRel(PIXA* pixas, int delw, int delh);
         internal static  extern HandleRef pixaScale(PIXA* pixas, float scalex, float scaley);
         internal static  extern HandleRef pixaAddBorderGeneral(PIXA* pixad, HandleRef pixas, int left, int right, int top, int bot, uint val);
         internal static  extern HandleRef pixaaFlattenToPixa(PIXAA* paa, NUMA** pnaindex, int copyflag);
         internal static  extern int pixaaSizeRange(PIXAA* paa, l_int32* pminw, l_int32* pminh, l_int32* pmaxw, l_int32* pmaxh);
         internal static  extern int pixaSizeRange(PIXA* pixa, l_int32* pminw, l_int32* pminh, l_int32* pmaxw, l_int32* pmaxh);
         internal static  extern HandleRef pixaClipToPix(PIXA* pixas, PIX* pixs);
         internal static  extern int pixaClipToForeground(PIXA* pixas, PIXA** ppixad, BOXA** pboxa);
         internal static  extern int pixaGetRenderingDepth(PIXA* pixa, l_int32* pdepth);
         internal static  extern int pixaHasColor(PIXA* pixa, l_int32* phascolor);
         internal static  extern int pixaAnyColormaps(PIXA* pixa, l_int32* phascmap);
         internal static  extern int pixaGetDepthInfo(PIXA* pixa, l_int32* pmaxdepth, l_int32* psame);
         internal static  extern HandleRef pixaConvertToSameDepth(PIXA* pixas);
         internal static  extern int pixaEqual(PIXA* pixa1, HandleRef pixa2, int maxdist, NUMA** pnaindex, l_int32* psame);
         internal static  extern HandleRef pixaRotateOrth(PIXA* pixas, int rotation);
         internal static  extern int pixaSetFullSizeBoxa(PIXA* pixa);
         internal static  extern PIX* pixaDisplay(PIXA* pixa, int w, int h);
         internal static  extern PIX* pixaDisplayOnColor(PIXA* pixa, int w, int h, uint bgcolor);
         internal static  extern PIX* pixaDisplayRandomCmap(PIXA* pixa, int w, int h);
         internal static  extern PIX* pixaDisplayLinearly(PIXA* pixas, int direction, float scalefactor, int background, int spacing, int border, BOXA** pboxa);
         internal static  extern PIX* pixaDisplayOnLattice(PIXA* pixa, int cellw, int cellh, l_int32* pncols, BOXA** pboxa);
         internal static  extern PIX* pixaDisplayUnsplit(PIXA* pixa, int nx, int ny, int borderwidth, uint bordercolor);
         internal static  extern PIX* pixaDisplayTiled(PIXA* pixa, int maxwidth, int background, int spacing);
         internal static  extern PIX* pixaDisplayTiledInRows(PIXA* pixa, int outdepth, int maxwidth, float scalefactor, int background, int spacing, int border);
         internal static  extern PIX* pixaDisplayTiledInColumns(PIXA* pixas, int nx, float scalefactor, int spacing, int border);
         internal static  extern PIX* pixaDisplayTiledAndScaled(PIXA* pixa, int outdepth, int tilewidth, int ncols, int background, int spacing, int border);
         internal static  extern PIX* pixaDisplayTiledWithText(PIXA* pixa, int maxwidth, float scalefactor, int spacing, int border, int fontsize, uint textcolor);
         internal static  extern PIX* pixaDisplayTiledByIndex(PIXA* pixa, NUMA* na, int width, int spacing, int border, int fontsize, uint textcolor);
         internal static  extern PIX* pixaaDisplay(PIXAA* paa, int w, int h);
         internal static  extern PIX* pixaaDisplayByPixa(PIXAA* paa, int xspace, int yspace, int maxw);
         internal static  extern HandleRef pixaaDisplayTiledAndScaled(PIXAA* paa, int outdepth, int tilewidth, int ncols, int background, int spacing, int border);
         internal static  extern HandleRef pixaConvertTo1(PIXA* pixas, int thresh);
         internal static  extern HandleRef pixaConvertTo8(PIXA* pixas, int cmapflag);
         internal static  extern HandleRef pixaConvertTo8Color(PIXA* pixas, int dither);
         internal static  extern HandleRef pixaConvertTo32(PIXA* pixas);
         internal static  extern HandleRef pixaConstrainedSelect(PIXA* pixas, int first, int last, int nmax, int use_pairs, int copyflag);
         internal static  extern int pixaSelectToPdf(PIXA* pixas, int first, int last, int res, float scalefactor, int type, int quality, uint color, int fontsize, const char* fileout );
         internal static  extern HandleRef pixaDisplayMultiTiled(PIXA* pixas, int nx, int ny, int maxw, int maxh, float scalefactor, int spacing, int border);
         internal static  extern int pixaSplitIntoFiles(PIXA* pixas, int nsplit, float scale, int outwidth, int write_pixa, int write_pix, int write_pdf);
         internal static  extern int convertToNUpFiles( const char* dir, const char* substr, int nx, int ny, int tw, int spacing, int border, int fontsize, const char* outdir );
         internal static  extern HandleRef convertToNUpPixa( const char* dir, const char* substr, int nx, int ny, int tw, int spacing, int border, int fontsize );
         internal static  extern HandleRef pixaConvertToNUpPixa(PIXA* pixas, SARRAY* sa, int nx, int ny, int tw, int spacing, int border, int fontsize);
         internal static  extern int pixaCompareInPdf(PIXA* pixa1, HandleRef pixa2, int nx, int ny, int tw, int spacing, int border, int fontsize, const char* fileout );
         internal static  extern int pmsCreate(size_t minsize, size_t smallest, NUMA* numalloc, const char* logfile );
         internal static  extern void pmsDestroy();
         internal static  extern void* pmsCustomAlloc(size_t nbytes);
         internal static  extern void pmsCustomDealloc(void* data);
         internal static  extern void* pmsGetAlloc(size_t nbytes);
         internal static  extern int pmsGetLevelForAlloc(size_t nbytes, l_int32* plevel);
         internal static  extern int pmsGetLevelForDealloc(void* data, l_int32* plevel);
         internal static  extern void pmsLogInfo();
         internal static  extern int pixAddConstantGray(PIX* pixs, int val);
         internal static  extern int pixMultConstantGray(PIX* pixs, float val);
         internal static  extern PIX* pixAddGray(PIX* pixd, PIX* pixs1, PIX* pixs2);
         internal static  extern PIX* pixSubtractGray(PIX* pixd, PIX* pixs1, PIX* pixs2);
         internal static  extern PIX* pixThresholdToValue(PIX* pixd, PIX* pixs, int threshval, int setval);
         internal static  extern PIX* pixInitAccumulate(int w, int h, uint offset);
         internal static  extern PIX* pixFinalAccumulate(PIX* pixs, uint offset, int depth);
         internal static  extern PIX* pixFinalAccumulateThreshold(PIX* pixs, uint offset, uint threshold);
         internal static  extern int pixAccumulate(PIX* pixd, PIX* pixs, int op);
         internal static  extern int pixMultConstAccumulate(PIX* pixs, float factor, uint offset);
         internal static  extern PIX* pixAbsDifference(PIX* pixs1, PIX* pixs2);
         internal static  extern PIX* pixAddRGB(PIX* pixs1, PIX* pixs2);
         internal static  extern PIX* pixMinOrMax(PIX* pixd, PIX* pixs1, PIX* pixs2, int type);
         internal static  extern PIX* pixMaxDynamicRange(PIX* pixs, int type);
         internal static  extern PIX* pixMaxDynamicRangeRGB(PIX* pixs, int type);
         internal static  extern uint linearScaleRGBVal(l_uint32 sval, float factor);
         internal static  extern uint logScaleRGBVal(l_uint32 sval, l_float32* tab, float factor);
         internal static  extern l_float32* makeLogBase2Tab(void );
         internal static  extern float getLogBase2(int val, l_float32* logtab);
         internal static  extern PIXC* pixcompCreateFromPix(PIX* pix, int comptype);
         internal static  extern PIXC* pixcompCreateFromString(l_uint8* data, size_t size, int copyflag);
         internal static  extern PIXC* pixcompCreateFromFile( const char* filename, int comptype );
         internal static  extern void pixcompDestroy(PIXC** ppixc);
         internal static  extern PIXC* pixcompCopy(PIXC* pixcs);
         internal static  extern int pixcompGetDimensions(PIXC* pixc, l_int32* pw, l_int32* ph, l_int32* pd);
         internal static  extern int pixcompDetermineFormat(int comptype, int d, int cmapflag, l_int32* pformat);
         internal static  extern PIX* pixCreateFromPixcomp(PIXC* pixc);
         internal static  extern PIXAC* pixacompCreate(int n);
         internal static  extern PIXAC* pixacompCreateWithInit(int n, int offset, PIX* pix, int comptype);
         internal static  extern PIXAC* pixacompCreateFromPixa(PIXA* pixa, int comptype, int accesstype);
         internal static  extern PIXAC* pixacompCreateFromFiles( const char* dirname, const char* substr, int comptype );
         internal static  extern PIXAC* pixacompCreateFromSA(SARRAY* sa, int comptype);
         internal static  extern void pixacompDestroy(PIXAC** ppixac);
         internal static  extern int pixacompAddPix(PIXAC* pixac, PIX* pix, int comptype);
         internal static  extern int pixacompAddPixcomp(PIXAC* pixac, PIXC* pixc, int copyflag);
         internal static  extern int pixacompReplacePix(PIXAC* pixac, int index, PIX* pix, int comptype);
         internal static  extern int pixacompReplacePixcomp(PIXAC* pixac, int index, PIXC* pixc);
         internal static  extern int pixacompAddBox(PIXAC* pixac, HandleRef box, int copyflag);
         internal static  extern int pixacompGetCount(PIXAC* pixac);
         internal static  extern PIXC* pixacompGetPixcomp(PIXAC* pixac, int index, int copyflag);
         internal static  extern PIX* pixacompGetPix(PIXAC* pixac, int index);
         internal static  extern int pixacompGetPixDimensions(PIXAC* pixac, int index, l_int32* pw, l_int32* ph, l_int32* pd);
         internal static  extern HandleRef pixacompGetBoxa(PIXAC* pixac, int accesstype);
         internal static  extern int pixacompGetBoxaCount(PIXAC* pixac);
         internal static  extern HandleRef pixacompGetBox(PIXAC* pixac, int index, int accesstype);
         internal static  extern int pixacompGetBoxGeometry(PIXAC* pixac, int index, l_int32* px, l_int32* py, l_int32* pw, l_int32* ph);
         internal static  extern int pixacompGetOffset(PIXAC* pixac);
         internal static  extern int pixacompSetOffset(PIXAC* pixac, int offset);
         internal static  extern HandleRef pixaCreateFromPixacomp(PIXAC* pixac, int accesstype);
         internal static  extern int pixacompJoin(PIXAC* pixacd, PIXAC* pixacs, int istart, int iend);
         internal static  extern PIXAC* pixacompInterleave(PIXAC* pixac1, PIXAC* pixac2);
         internal static  extern PIXAC* pixacompRead( const char* filename );
         internal static  extern PIXAC* pixacompReadStream(FILE* fp);
         internal static  extern PIXAC* pixacompReadMem( const l_uint8* data, size_t size );
         internal static  extern int pixacompWrite( const char* filename, PIXAC *pixac );
         internal static  extern int pixacompWriteStream(FILE* fp, PIXAC* pixac);
         internal static  extern int pixacompWriteMem(l_uint8** pdata, size_t* psize, PIXAC* pixac);
         internal static  extern int pixacompConvertToPdf(PIXAC* pixac, int res, float scalefactor, int type, int quality, const char* title, const char* fileout );
         internal static  extern int pixacompConvertToPdfData(PIXAC* pixac, int res, float scalefactor, int type, int quality, const char* title, byte **pdata, size_t* pnbytes );
         internal static  extern int pixacompWriteStreamInfo(FILE* fp, PIXAC* pixac, const char* text );
         internal static  extern int pixcompWriteStreamInfo(FILE* fp, PIXC* pixc, const char* text );
         internal static  extern PIX* pixacompDisplayTiledAndScaled(PIXAC* pixac, int outdepth, int tilewidth, int ncols, int background, int spacing, int border);
         internal static  extern PIX* pixThreshold8(PIX* pixs, int d, int nlevels, int cmapflag);
         internal static  extern PIX* pixRemoveColormapGeneral(PIX* pixs, int type, int ifnocmap);
         internal static  extern PIX* pixRemoveColormap(PIX* pixs, int type);
         internal static  extern int pixAddGrayColormap8(PIX* pixs);
         internal static  extern PIX* pixAddMinimalGrayColormap8(PIX* pixs);
         internal static  extern PIX* pixConvertRGBToLuminance(PIX* pixs);
         internal static  extern PIX* pixConvertRGBToGray(PIX* pixs, float rwt, float gwt, float bwt);
         internal static  extern PIX* pixConvertRGBToGrayFast(PIX* pixs);
         internal static  extern PIX* pixConvertRGBToGrayMinMax(PIX* pixs, int type);
         internal static  extern PIX* pixConvertRGBToGraySatBoost(PIX* pixs, int refval);
         internal static  extern PIX* pixConvertRGBToGrayArb(PIX* pixs, float rc, float gc, float bc);
         internal static  extern PIX* pixConvertRGBToBinaryArb(PIX* pixs, float rc, float gc, float bc, int thresh, int relation);
         internal static  extern PIX* pixConvertGrayToColormap(PIX* pixs);
         internal static  extern PIX* pixConvertGrayToColormap8(PIX* pixs, int mindepth);
         internal static  extern PIX* pixColorizeGray(PIX* pixs, uint color, int cmapflag);
         internal static  extern PIX* pixConvertRGBToColormap(PIX* pixs, int ditherflag);
         internal static  extern PIX* pixConvertCmapTo1(PIX* pixs);
         internal static  extern int pixQuantizeIfFewColors(PIX* pixs, int maxcolors, int mingraycolors, int octlevel, PIX** ppixd);
         internal static  extern PIX* pixConvert16To8(PIX* pixs, int type);
         internal static  extern PIX* pixConvertGrayToFalseColor(PIX* pixs, float gamma);
         internal static  extern PIX* pixUnpackBinary(PIX* pixs, int depth, int invert);
         internal static  extern PIX* pixConvert1To16(PIX* pixd, PIX* pixs, l_uint16 val0, l_uint16 val1);
         internal static  extern PIX* pixConvert1To32(PIX* pixd, PIX* pixs, uint val0, uint val1);
         internal static  extern PIX* pixConvert1To2Cmap(PIX* pixs);
         internal static  extern PIX* pixConvert1To2(PIX* pixd, PIX* pixs, int val0, int val1);
         internal static  extern PIX* pixConvert1To4Cmap(PIX* pixs);
         internal static  extern PIX* pixConvert1To4(PIX* pixd, PIX* pixs, int val0, int val1);
         internal static  extern PIX* pixConvert1To8Cmap(PIX* pixs);
         internal static  extern PIX* pixConvert1To8(PIX* pixd, PIX* pixs, byte val0, byte val1);
         internal static  extern PIX* pixConvert2To8(PIX* pixs, byte val0, byte val1, byte val2, byte val3, int cmapflag);
         internal static  extern PIX* pixConvert4To8(PIX* pixs, int cmapflag);
         internal static  extern PIX* pixConvert8To16(PIX* pixs, int leftshift);
         internal static  extern PIX* pixConvertTo1(PIX* pixs, int threshold);
         internal static  extern PIX* pixConvertTo1BySampling(PIX* pixs, int factor, int threshold);
         internal static  extern PIX* pixConvertTo8(PIX* pixs, int cmapflag);
         internal static  extern PIX* pixConvertTo8BySampling(PIX* pixs, int factor, int cmapflag);
         internal static  extern PIX* pixConvertTo8Color(PIX* pixs, int dither);
         internal static  extern PIX* pixConvertTo16(PIX* pixs);
         internal static  extern PIX* pixConvertTo32(PIX* pixs);
         internal static  extern PIX* pixConvertTo32BySampling(PIX* pixs, int factor);
         internal static  extern PIX* pixConvert8To32(PIX* pixs);
         internal static  extern PIX* pixConvertTo8Or32(PIX* pixs, int copyflag, int warnflag);
         internal static  extern PIX* pixConvert24To32(PIX* pixs);
         internal static  extern PIX* pixConvert32To24(PIX* pixs);
         internal static  extern PIX* pixConvert32To16(PIX* pixs, int type);
         internal static  extern PIX* pixConvert32To8(PIX* pixs, int type16, int type8);
         internal static  extern PIX* pixRemoveAlpha(PIX* pixs);
         internal static  extern PIX* pixAddAlphaTo1bpp(PIX* pixd, PIX* pixs);
         internal static  extern PIX* pixConvertLossless(PIX* pixs, int d);
         internal static  extern PIX* pixConvertForPSWrap(PIX* pixs);
         internal static  extern PIX* pixConvertToSubpixelRGB(PIX* pixs, float scalex, float scaley, int order);
         internal static  extern PIX* pixConvertGrayToSubpixelRGB(PIX* pixs, float scalex, float scaley, int order);
         internal static  extern PIX* pixConvertColorToSubpixelRGB(PIX* pixs, float scalex, float scaley, int order);
         internal static  extern void l_setNeutralBoostVal(int val);
         internal static  extern PIX* pixConnCompTransform(PIX* pixs, int connect, int depth);
         internal static  extern PIX* pixConnCompAreaTransform(PIX* pixs, int connect);
         internal static  extern int pixConnCompIncrInit(PIX* pixs, int conn, PIX** ppixd, PTAA** pptaa, l_int32* pncc);
         internal static  extern int pixConnCompIncrAdd(PIX* pixs, PTAA* ptaa, l_int32* pncc, float x, float y, int debug);
         internal static  extern int pixGetSortedNeighborValues(PIX* pixs, int x, int y, int conn, l_int32** pneigh, l_int32* pnvals);
         internal static  extern PIX* pixLocToColorTransform(PIX* pixs);
         internal static  extern PIXTILING* pixTilingCreate(PIX* pixs, int nx, int ny, int w, int h, int xoverlap, int yoverlap);
         internal static  extern void pixTilingDestroy(PIXTILING** ppt);
         internal static  extern int pixTilingGetCount(PIXTILING* pt, l_int32* pnx, l_int32* pny);
         internal static  extern int pixTilingGetSize(PIXTILING* pt, l_int32* pw, l_int32* ph);
         internal static  extern PIX* pixTilingGetTile(PIXTILING* pt, int i, int j);
         internal static  extern int pixTilingNoStripOnPaint(PIXTILING* pt);
         internal static  extern int pixTilingPaintTile(PIX* pixd, int i, int j, PIX* pixs, PIXTILING* pt);
         internal static  extern PIX* pixReadStreamPng(FILE* fp);
         internal static  extern int readHeaderPng( const char* filename, int *pw, l_int32* ph, int *pbps, l_int32* pspp, int *piscmap );
         internal static  extern int freadHeaderPng(FILE* fp, l_int32* pw, l_int32* ph, l_int32* pbps, l_int32* pspp, l_int32* piscmap);
         internal static  extern int readHeaderMemPng( const l_uint8* data, size_t size, l_int32* pw, int *ph, l_int32* pbps, int *pspp, l_int32* piscmap );
         internal static  extern int fgetPngResolution(FILE* fp, l_int32* pxres, l_int32* pyres);
         internal static  extern int isPngInterlaced( const char* filename, int *pinterlaced );
         internal static  extern int fgetPngColormapInfo(FILE* fp, PIXCMAP** pcmap, l_int32* ptransparency);
         internal static  extern int pixWritePng( const char* filename, PIX *pix, float gamma );
         internal static  extern int pixWriteStreamPng(FILE* fp, PIX* pix, float gamma);
         internal static  extern int pixSetZlibCompression(PIX* pix, int compval);
         internal static  extern void l_pngSetReadStrip16To8(int flag);
         internal static  extern PIX* pixReadMemPng( const l_uint8* filedata, size_t filesize );
         internal static  extern int pixWriteMemPng(l_uint8** pfiledata, size_t* pfilesize, PIX* pix, float gamma);
         internal static  extern PIX* pixReadStreamPnm(FILE* fp);
         internal static  extern int readHeaderPnm( const char* filename, int *pw, l_int32* ph, int *pd, l_int32* ptype, int *pbps, l_int32* pspp );
         internal static  extern int freadHeaderPnm(FILE* fp, l_int32* pw, l_int32* ph, l_int32* pd, l_int32* ptype, l_int32* pbps, l_int32* pspp);
         internal static  extern int pixWriteStreamPnm(FILE* fp, PIX* pix);
         internal static  extern int pixWriteStreamAsciiPnm(FILE* fp, PIX* pix);
         internal static  extern int pixWriteStreamPam(FILE* fp, PIX* pix);
         internal static  extern PIX* pixReadMemPnm( const l_uint8* data, size_t size );
         internal static  extern int readHeaderMemPnm( const l_uint8* data, size_t size, l_int32* pw, int *ph, l_int32* pd, int *ptype, l_int32* pbps, int *pspp );
         internal static  extern int pixWriteMemPnm(l_uint8** pdata, size_t* psize, PIX* pix);
         internal static  extern int pixWriteMemPam(l_uint8** pdata, size_t* psize, PIX* pix);
         internal static  extern PIX* pixProjectiveSampledPta(PIX* pixs, PTA* ptad, PTA* ptas, int incolor);
         internal static  extern PIX* pixProjectiveSampled(PIX* pixs, l_float32* vc, int incolor);
         internal static  extern PIX* pixProjectivePta(PIX* pixs, PTA* ptad, PTA* ptas, int incolor);
         internal static  extern PIX* pixProjective(PIX* pixs, l_float32* vc, int incolor);
         internal static  extern PIX* pixProjectivePtaColor(PIX* pixs, PTA* ptad, PTA* ptas, uint colorval);
         internal static  extern PIX* pixProjectiveColor(PIX* pixs, l_float32* vc, uint colorval);
         internal static  extern PIX* pixProjectivePtaGray(PIX* pixs, PTA* ptad, PTA* ptas, byte grayval);
         internal static  extern PIX* pixProjectiveGray(PIX* pixs, l_float32* vc, byte grayval);
         internal static  extern PIX* pixProjectivePtaWithAlpha(PIX* pixs, PTA* ptad, PTA* ptas, PIX* pixg, float fract, int border);
         internal static  extern int getProjectiveXformCoeffs(HandleRef ptas, PTA* ptad, l_float32** pvc);
         internal static  extern int projectiveXformSampledPt(l_float32* vc, int x, int y, l_int32* pxp, l_int32* pyp);
         internal static  extern int projectiveXformPt(l_float32* vc, int x, int y, l_float32* pxp, l_float32* pyp);
         internal static  extern int convertFilesToPS( const char* dirin, const char* substr, int res, const char* fileout );
         internal static  extern int sarrayConvertFilesToPS(SARRAY* sa, int res, const char* fileout );
         internal static  extern int convertFilesFittedToPS( const char* dirin, const char* substr, float xpts, float ypts, const char* fileout );
         internal static  extern int sarrayConvertFilesFittedToPS(SARRAY* sa, float xpts, float ypts, const char* fileout );
         internal static  extern int writeImageCompressedToPSFile( const char* filein, const char* fileout, int res, l_int32* pfirstfile, int *pindex );
         internal static  extern int convertSegmentedPagesToPS( const char* pagedir, const char* pagestr, int page_numpre, const char* maskdir, const char* maskstr, int mask_numpre, int numpost, int maxnum, float textscale, float imagescale, int threshold, const char* fileout );
         internal static  extern int pixWriteSegmentedPageToPS(PIX* pixs, PIX* pixm, float textscale, float imagescale, int threshold, int pageno, const char* fileout );
         internal static  extern int pixWriteMixedToPS(PIX* pixb, PIX* pixc, float scale, int pageno, const char* fileout );
         internal static  extern int convertToPSEmbed( const char* filein, const char* fileout, int level );
         internal static  extern int pixaWriteCompressedToPS(PIXA* pixa, const char* fileout, int res, int level );
         internal static  extern int pixWritePSEmbed( const char* filein, const char* fileout );
         internal static  extern int pixWriteStreamPS(FILE* fp, PIX* pix, HandleRef box, int res, float scale);
         internal static  extern char* pixWriteStringPS(PIX* pixs, HandleRef box, int res, float scale);
         internal static  extern char* generateUncompressedPS(char* hexdata, int w, int h, int d, int psbpl, int bps, float xpt, float ypt, float wpt, float hpt, int boxflag);
         internal static  extern void getScaledParametersPS(HandleRef box, int wpix, int hpix, int res, float scale, l_float32* pxpt, l_float32* pypt, l_float32* pwpt, l_float32* phpt);
         internal static  extern void convertByteToHexAscii(l_uint8 byteval, char* pnib1, char* pnib2);
         internal static  extern int convertJpegToPSEmbed( const char* filein, const char* fileout );
         internal static  extern int convertJpegToPS( const char* filein, const char* fileout, const char* operation, int x, int y, int res, float scale, int pageno, int endpage );
         internal static  extern int convertJpegToPSString( const char* filein, char** poutstr, int *pnbytes, int x, int y, int res, float scale, int pageno, int endpage );
         internal static  extern char* generateJpegPS( const char* filein, L_COMP_DATA *cid, float xpt, float ypt, float wpt, float hpt, int pageno, int endpage );
         internal static  extern int convertG4ToPSEmbed( const char* filein, const char* fileout );
         internal static  extern int convertG4ToPS( const char* filein, const char* fileout, const char* operation, int x, int y, int res, float scale, int pageno, int maskflag, int endpage );
         internal static  extern int convertG4ToPSString( const char* filein, char** poutstr, int *pnbytes, int x, int y, int res, float scale, int pageno, int maskflag, int endpage );
         internal static  extern char* generateG4PS( const char* filein, L_COMP_DATA *cid, float xpt, float ypt, float wpt, float hpt, int maskflag, int pageno, int endpage );
         internal static  extern int convertTiffMultipageToPS( const char* filein, const char* fileout, float fillfract );
         internal static  extern int convertFlateToPSEmbed( const char* filein, const char* fileout );
         internal static  extern int convertFlateToPS( const char* filein, const char* fileout, const char* operation, int x, int y, int res, float scale, int pageno, int endpage );
         internal static  extern int convertFlateToPSString( const char* filein, char** poutstr, int *pnbytes, int x, int y, int res, float scale, int pageno, int endpage );
         internal static  extern char* generateFlatePS( const char* filein, L_COMP_DATA *cid, float xpt, float ypt, float wpt, float hpt, int pageno, int endpage );
         internal static  extern int pixWriteMemPS(l_uint8** pdata, size_t* psize, PIX* pix, HandleRef box, int res, float scale);
         internal static  extern int getResLetterPage(int w, int h, float fillfract);
         internal static  extern int getResA4Page(int w, int h, float fillfract);
         internal static  extern void l_psWriteBoundingBox(int flag);
         internal static  extern PTA* ptaCreate(int n);
         internal static  extern PTA* ptaCreateFromNuma(NUMA* nax, NUMA* nay);
         internal static  extern void ptaDestroy(PTA** ppta);
         internal static  extern PTA* ptaCopy(HandleRef pta);
         internal static  extern PTA* ptaCopyRange(HandleRef ptas, int istart, int iend);
         internal static  extern PTA* ptaClone(HandleRef pta);
         internal static  extern int ptaEmpty(HandleRef pta);
         internal static  extern int ptaAddPt(HandleRef pta, float x, float y);
         internal static  extern int ptaInsertPt(HandleRef pta, int index, int x, int y);
         internal static  extern int ptaRemovePt(HandleRef pta, int index);
         internal static  extern int ptaGetRefcount(HandleRef pta);
         internal static  extern int ptaChangeRefcount(HandleRef pta, int delta);
         internal static  extern int ptaGetCount(HandleRef pta);
         internal static  extern int ptaGetPt(HandleRef pta, int index, l_float32* px, l_float32* py);
         internal static  extern int ptaGetIPt(HandleRef pta, int index, l_int32* px, l_int32* py);
         internal static  extern int ptaSetPt(HandleRef pta, int index, float x, float y);
         internal static  extern int ptaGetArrays(HandleRef pta, NUMA** pnax, NUMA** pnay);
         internal static  extern PTA* ptaRead( const char* filename );
         internal static  extern PTA* ptaReadStream(FILE* fp);
         internal static  extern PTA* ptaReadMem( const l_uint8* data, size_t size );
         internal static  extern int ptaWrite( const char* filename, PTA *pta, int type );
         internal static  extern int ptaWriteStream(FILE* fp, PTA* pta, int type);
         internal static  extern int ptaWriteMem(l_uint8** pdata, size_t* psize, PTA* pta, int type);
         internal static  extern PTAA* ptaaCreate(int n);
         internal static  extern void ptaaDestroy(PTAA** pptaa);
         internal static  extern int ptaaAddPta(PTAA* ptaa, PTA* pta, int copyflag);
         internal static  extern int ptaaGetCount(PTAA* ptaa);
         internal static  extern PTA* ptaaGetPta(PTAA* ptaa, int index, int accessflag);
         internal static  extern int ptaaGetPt(PTAA* ptaa, int ipta, int jpt, l_float32* px, l_float32* py);
         internal static  extern int ptaaInitFull(PTAA* ptaa, PTA* pta);
         internal static  extern int ptaaReplacePta(PTAA* ptaa, int index, PTA* pta);
         internal static  extern int ptaaAddPt(PTAA* ptaa, int ipta, float x, float y);
         internal static  extern int ptaaTruncate(PTAA* ptaa);
         internal static  extern PTAA* ptaaRead( const char* filename );
         internal static  extern PTAA* ptaaReadStream(FILE* fp);
         internal static  extern PTAA* ptaaReadMem( const l_uint8* data, size_t size );
         internal static  extern int ptaaWrite( const char* filename, PTAA *ptaa, int type );
         internal static  extern int ptaaWriteStream(FILE* fp, PTAA* ptaa, int type);
         internal static  extern int ptaaWriteMem(l_uint8** pdata, size_t* psize, PTAA* ptaa, int type);
         internal static  extern PTA* ptaSubsample(HandleRef ptas, int subfactor);
         internal static  extern int ptaJoin(HandleRef ptad, PTA* ptas, int istart, int iend);
         internal static  extern int ptaaJoin(PTAA* ptaad, PTAA* ptaas, int istart, int iend);
         internal static  extern PTA* ptaReverse(HandleRef ptas, int type);
         internal static  extern PTA* ptaTranspose(HandleRef ptas);
         internal static  extern PTA* ptaCyclicPerm(HandleRef ptas, int xs, int ys);
         internal static  extern PTA* ptaSelectRange(HandleRef ptas, int first, int last);
         internal static  extern HandleRef ptaGetBoundingRegion(HandleRef pta);
         internal static  extern int ptaGetRange(HandleRef pta, l_float32* pminx, l_float32* pmaxx, l_float32* pminy, l_float32* pmaxy);
         internal static  extern PTA* ptaGetInsideBox(HandleRef ptas, HandleRef box);
         internal static  extern PTA* pixFindCornerPixels(PIX* pixs);
         internal static  extern int ptaContainsPt(HandleRef pta, int x, int y);
         internal static  extern int ptaTestIntersection(HandleRef pta1, PTA* pta2);
         internal static  extern PTA* ptaTransform(HandleRef ptas, int shiftx, int shifty, float scalex, float scaley);
         internal static  extern int ptaPtInsidePolygon(HandleRef pta, float x, float y, l_int32* pinside);
         internal static  extern float l_angleBetweenVectors(float x1, float y1, float x2, float y2);
         internal static  extern int ptaGetMinMax(HandleRef pta, l_float32* pxmin, l_float32* pymin, l_float32* pxmax, l_float32* pymax);
         internal static  extern PTA* ptaSelectByValue(HandleRef ptas, float xth, float yth, int type, int relation);
         internal static  extern PTA* ptaCropToMask(HandleRef ptas, PIX* pixm);
         internal static  extern int ptaGetLinearLSF(HandleRef pta, l_float32* pa, l_float32* pb, NUMA** pnafit);
         internal static  extern int ptaGetQuadraticLSF(HandleRef pta, l_float32* pa, l_float32* pb, l_float32* pc, NUMA** pnafit);
         internal static  extern int ptaGetCubicLSF(HandleRef pta, l_float32* pa, l_float32* pb, l_float32* pc, l_float32* pd, NUMA** pnafit);
         internal static  extern int ptaGetQuarticLSF(HandleRef pta, l_float32* pa, l_float32* pb, l_float32* pc, l_float32* pd, l_float32* pe, NUMA** pnafit);
         internal static  extern int ptaNoisyLinearLSF(HandleRef pta, float factor, PTA** pptad, l_float32* pa, l_float32* pb, l_float32* pmederr, NUMA** pnafit);
         internal static  extern int ptaNoisyQuadraticLSF(HandleRef pta, float factor, PTA** pptad, l_float32* pa, l_float32* pb, l_float32* pc, l_float32* pmederr, NUMA** pnafit);
         internal static  extern int applyLinearFit(float a, float b, float x, l_float32* py);
         internal static  extern int applyQuadraticFit(float a, float b, float c, float x, l_float32* py);
         internal static  extern int applyCubicFit(float a, float b, float c, float d, float x, l_float32* py);
         internal static  extern int applyQuarticFit(float a, float b, float c, float d, float e, float x, l_float32* py);
         internal static  extern int pixPlotAlongPta(PIX* pixs, PTA* pta, int outformat, const char* title );
         internal static  extern PTA* ptaGetPixelsFromPix(PIX* pixs, HandleRef box);
         internal static  extern PIX* pixGenerateFromPta(HandleRef pta, int w, int h);
         internal static  extern PTA* ptaGetBoundaryPixels(PIX* pixs, int type);
         internal static  extern PTAA* ptaaGetBoundaryPixels(PIX* pixs, int type, int connectivity, BOXA** pboxa, PIXA** ppixa);
         internal static  extern PTAA* ptaaIndexLabeledPixels(PIX* pixs, l_int32* pncc);
         internal static  extern PTA* ptaGetNeighborPixLocs(PIX* pixs, int x, int y, int conn);
         internal static  extern PTA* numaConvertToPta1(NUMA* na);
         internal static  extern PTA* numaConvertToPta2(NUMA* nax, NUMA* nay);
         internal static  extern int ptaConvertToNuma(HandleRef pta, NUMA** pnax, NUMA** pnay);
         internal static  extern PIX* pixDisplayPta(PIX* pixd, PIX* pixs, PTA* pta);
         internal static  extern PIX* pixDisplayPtaaPattern(PIX* pixd, PIX* pixs, PTAA* ptaa, PIX* pixp, int cx, int cy);
         internal static  extern PIX* pixDisplayPtaPattern(PIX* pixd, PIX* pixs, PTA* pta, PIX* pixp, int cx, int cy, uint color);
         internal static  extern PTA* ptaReplicatePattern(HandleRef ptas, PIX* pixp, PTA* ptap, int cx, int cy, int w, int h);
         internal static  extern PIX* pixDisplayPtaa(PIX* pixs, PTAA* ptaa);
         internal static  extern PTA* ptaSort(HandleRef ptas, int sorttype, int sortorder, NUMA** pnaindex);
         internal static  extern int ptaGetSortIndex(HandleRef ptas, int sorttype, int sortorder, NUMA** pnaindex);
         internal static  extern PTA* ptaSortByIndex(HandleRef ptas, NUMA* naindex);
         internal static  extern PTAA* ptaaSortByIndex(PTAA* ptaas, NUMA* naindex);
         internal static  extern int ptaGetRankValue(HandleRef pta, float fract, PTA* ptasort, int sorttype, l_float32* pval);
         internal static  extern PTA* ptaUnionByAset(HandleRef pta1, PTA* pta2);
         internal static  extern PTA* ptaRemoveDupsByAset(HandleRef ptas);
         internal static  extern PTA* ptaIntersectionByAset(HandleRef pta1, PTA* pta2);
         internal static  extern L_ASET* l_asetCreateFromPta(HandleRef pta);
         internal static  extern PTA* ptaUnionByHash(HandleRef pta1, PTA* pta2);
         internal static  extern int ptaRemoveDupsByHash(HandleRef ptas, PTA** pptad, L_DNAHASH** pdahash);
         internal static  extern PTA* ptaIntersectionByHash(HandleRef pta1, PTA* pta2);
         internal static  extern int ptaFindPtByHash(HandleRef pta, L_DNAHASH* dahash, int x, int y, l_int32* pindex);
         internal static  extern L_DNAHASH* l_dnaHashCreateFromPta(HandleRef pta);
         internal static  extern L_PTRA* ptraCreate(int n);
         internal static  extern void ptraDestroy(L_PTRA** ppa, int freeflag, int warnflag);
         internal static  extern int ptraAdd(L_PTRA* pa, void* item);
         internal static  extern int ptraInsert(L_PTRA* pa, int index, void* item, int shiftflag);
         internal static  extern void* ptraRemove(L_PTRA* pa, int index, int flag);
         internal static  extern void* ptraRemoveLast(L_PTRA* pa);
         internal static  extern void* ptraReplace(L_PTRA* pa, int index, void* item, int freeflag);
         internal static  extern int ptraSwap(L_PTRA* pa, int index1, int index2);
         internal static  extern int ptraCompactArray(L_PTRA* pa);
         internal static  extern int ptraReverse(L_PTRA* pa);
         internal static  extern int ptraJoin(L_PTRA* pa1, L_PTRA* pa2);
         internal static  extern int ptraGetMaxIndex(L_PTRA* pa, l_int32* pmaxindex);
         internal static  extern int ptraGetActualCount(L_PTRA* pa, l_int32* pcount);
         internal static  extern void* ptraGetPtrToItem(L_PTRA* pa, int index);
         internal static  extern L_PTRAA* ptraaCreate(int n);
         internal static  extern void ptraaDestroy(L_PTRAA** ppaa, int freeflag, int warnflag);
         internal static  extern int ptraaGetSize(L_PTRAA* paa, l_int32* psize);
         internal static  extern int ptraaInsertPtra(L_PTRAA* paa, int index, L_PTRA* pa);
         internal static  extern L_PTRA* ptraaGetPtra(L_PTRAA* paa, int index, int accessflag);
         internal static  extern L_PTRA* ptraaFlattenToPtra(L_PTRAA* paa);
         internal static  extern int pixQuadtreeMean(PIX* pixs, int nlevels, PIX* pix_ma, FPIXA** pfpixa);
         internal static  extern int pixQuadtreeVariance(PIX* pixs, int nlevels, PIX* pix_ma, DPIX* dpix_msa, FPIXA** pfpixa_v, FPIXA** pfpixa_rv);
         internal static  extern int pixMeanInRectangle(PIX* pixs, HandleRef box, PIX* pixma, l_float32* pval);
         internal static  extern int pixVarianceInRectangle(PIX* pixs, HandleRef box, PIX* pix_ma, DPIX* dpix_msa, l_float32* pvar, l_float32* prvar);
         internal static  extern BOXAA* boxaaQuadtreeRegions(int w, int h, int nlevels);
         internal static  extern int quadtreeGetParent(FPIXA* fpixa, int level, int x, int y, l_float32* pval);
         internal static  extern int quadtreeGetChildren(FPIXA* fpixa, int level, int x, int y, l_float32* pval00, l_float32* pval10, l_float32* pval01, l_float32* pval11);
         internal static  extern int quadtreeMaxLevels(int w, int h);
         internal static  extern PIX* fpixaDisplayQuadtree(FPIXA* fpixa, int factor, int fontsize);
         internal static  extern L_QUEUE* lqueueCreate(int nalloc);
         internal static  extern void lqueueDestroy(L_QUEUE** plq, int freeflag);
         internal static  extern int lqueueAdd(L_QUEUE* lq, void* item);
         internal static  extern void* lqueueRemove(L_QUEUE* lq);
         internal static  extern int lqueueGetCount(L_QUEUE* lq);
         internal static  extern int lqueuePrint(FILE* fp, L_QUEUE* lq);
         internal static  extern PIX* pixRankFilter(PIX* pixs, int wf, int hf, float rank);
         internal static  extern PIX* pixRankFilterRGB(PIX* pixs, int wf, int hf, float rank);
         internal static  extern PIX* pixRankFilterGray(PIX* pixs, int wf, int hf, float rank);
         internal static  extern PIX* pixMedianFilter(PIX* pixs, int wf, int hf);
         internal static  extern PIX* pixRankFilterWithScaling(PIX* pixs, int wf, int hf, float rank, float scalefactor);
         internal static  extern L_RBTREE* l_rbtreeCreate(int keytype);
         internal static  extern RB_TYPE* l_rbtreeLookup(L_RBTREE* t, RB_TYPE key);
         internal static  extern void l_rbtreeInsert(L_RBTREE* t, RB_TYPE key, RB_TYPE value);
         internal static  extern void l_rbtreeDelete(L_RBTREE* t, RB_TYPE key);
         internal static  extern void l_rbtreeDestroy(L_RBTREE** pt);
         internal static  extern L_RBTREE_NODE* l_rbtreeGetFirst(L_RBTREE* t);
         internal static  extern L_RBTREE_NODE* l_rbtreeGetNext(L_RBTREE_NODE* n);
         internal static  extern L_RBTREE_NODE* l_rbtreeGetLast(L_RBTREE* t);
         internal static  extern L_RBTREE_NODE* l_rbtreeGetPrev(L_RBTREE_NODE* n);
         internal static  extern int l_rbtreeGetCount(L_RBTREE* t);
         internal static  extern void l_rbtreePrint(FILE* fp, L_RBTREE* t);
         internal static  extern SARRAY* pixProcessBarcodes(PIX* pixs, int format, int method, SARRAY** psaw, int debugflag);
         internal static  extern HandleRef pixExtractBarcodes(PIX* pixs, int debugflag);
         internal static  extern SARRAY* pixReadBarcodes(PIXA* pixa, int format, int method, SARRAY** psaw, int debugflag);
         internal static  extern NUMA* pixReadBarcodeWidths(PIX* pixs, int method, int debugflag);
         internal static  extern HandleRef pixLocateBarcodes(PIX* pixs, int thresh, PIX** ppixb, PIX** ppixm);
         internal static  extern PIX* pixDeskewBarcode(PIX* pixs, PIX* pixb, HandleRef box, int margin, int threshold, l_float32* pangle, l_float32* pconf);
         internal static  extern NUMA* pixExtractBarcodeWidths1(PIX* pixs, float thresh, float binfract, NUMA** pnaehist, NUMA** pnaohist, int debugflag);
         internal static  extern NUMA* pixExtractBarcodeWidths2(PIX* pixs, float thresh, l_float32* pwidth, NUMA** pnac, int debugflag);
         internal static  extern NUMA* pixExtractBarcodeCrossings(PIX* pixs, float thresh, int debugflag);
         internal static  extern NUMA* numaQuantizeCrossingsByWidth(NUMA* nas, float binfract, NUMA** pnaehist, NUMA** pnaohist, int debugflag);
         internal static  extern NUMA* numaQuantizeCrossingsByWindow(NUMA* nas, float ratio, l_float32* pwidth, l_float32* pfirstloc, NUMA** pnac, int debugflag);
         internal static  extern HandleRef pixaReadFiles( const char* dirname, const char* substr );
         internal static  extern HandleRef pixaReadFilesSA(SARRAY* sa);
         internal static  extern PIX* pixRead( const char* filename );
         internal static  extern PIX* pixReadWithHint( const char* filename, int hint );
         internal static  extern PIX* pixReadIndexed(SARRAY* sa, int index);
         internal static  extern PIX* pixReadStream(FILE* fp, int hint);
         internal static  extern int pixReadHeader( const char* filename, int *pformat, l_int32* pw, int *ph, l_int32* pbps, int *pspp, l_int32* piscmap );
         internal static  extern int findFileFormat( const char* filename, int *pformat );
         internal static  extern int findFileFormatStream(FILE* fp, l_int32* pformat);
         internal static  extern int findFileFormatBuffer( const l_uint8* buf, int *pformat );
         internal static  extern int fileFormatIsTiff(FILE* fp);
         internal static  extern PIX* pixReadMem( const l_uint8* data, size_t size );
         internal static  extern int pixReadHeaderMem( const l_uint8* data, size_t size, l_int32* pformat, int *pw, l_int32* ph, int *pbps, l_int32* pspp, int *piscmap );
         internal static  extern int writeImageFileInfo( const char* filename, FILE *fpout, int headeronly );
         internal static  extern int ioFormatTest( const char* filename );
         internal static  extern L_RECOG* recogCreateFromRecog(L_RECOG* recs, int scalew, int scaleh, int linew, int threshold, int maxyshift);
         internal static  extern L_RECOG* recogCreateFromPixa(PIXA* pixa, int scalew, int scaleh, int linew, int threshold, int maxyshift);
         internal static  extern L_RECOG* recogCreateFromPixaNoFinish(PIXA* pixa, int scalew, int scaleh, int linew, int threshold, int maxyshift);
         internal static  extern L_RECOG* recogCreate(int scalew, int scaleh, int linew, int threshold, int maxyshift);
         internal static  extern void recogDestroy(L_RECOG** precog);
         internal static  extern int recogGetCount(L_RECOG* recog);
         internal static  extern int recogSetParams(L_RECOG* recog, int type, int min_nopad, float max_wh_ratio, float max_ht_ratio);
         internal static  extern int recogGetClassIndex(L_RECOG* recog, int val, char* text, l_int32* pindex);
         internal static  extern int recogStringToIndex(L_RECOG* recog, char* text, l_int32* pindex);
         internal static  extern int recogGetClassString(L_RECOG* recog, int index, char** pcharstr);
         internal static  extern int l_convertCharstrToInt( const char* str, int *pval );
         internal static  extern L_RECOG* recogRead( const char* filename );
         internal static  extern L_RECOG* recogReadStream(FILE* fp);
         internal static  extern L_RECOG* recogReadMem( const l_uint8* data, size_t size );
         internal static  extern int recogWrite( const char* filename, L_RECOG *recog );
         internal static  extern int recogWriteStream(FILE* fp, L_RECOG* recog);
         internal static  extern int recogWriteMem(l_uint8** pdata, size_t* psize, L_RECOG* recog);
         internal static  extern HandleRef recogExtractPixa(L_RECOG* recog);
         internal static  extern HandleRef recogDecode(L_RECOG* recog, PIX* pixs, int nlevels, PIX** ppixdb);
         internal static  extern int recogCreateDid(L_RECOG* recog, PIX* pixs);
         internal static  extern int recogDestroyDid(L_RECOG* recog);
         internal static  extern int recogDidExists(L_RECOG* recog);
         internal static  extern L_RDID* recogGetDid(L_RECOG* recog);
         internal static  extern int recogSetChannelParams(L_RECOG* recog, int nlevels);
         internal static  extern int recogIdentifyMultiple(L_RECOG* recog, PIX* pixs, int minh, int skipsplit, BOXA** pboxa, PIXA** ppixa, PIX** ppixdb, int debugsplit);
         internal static  extern int recogSplitIntoCharacters(L_RECOG* recog, PIX* pixs, int minh, int skipsplit, BOXA** pboxa, PIXA** ppixa, int debug);
         internal static  extern int recogCorrelationBestRow(L_RECOG* recog, PIX* pixs, BOXA** pboxa, NUMA** pnascore, NUMA** pnaindex, SARRAY** psachar, int debug);
         internal static  extern int recogCorrelationBestChar(L_RECOG* recog, PIX* pixs, BOX** pbox, l_float32* pscore, l_int32* pindex, char** pcharstr, PIX** ppixdb);
         internal static  extern int recogIdentifyPixa(L_RECOG* recog, HandleRef pixa, PIX** ppixdb);
         internal static  extern int recogIdentifyPix(L_RECOG* recog, PIX* pixs, PIX** ppixdb);
         internal static  extern int recogSkipIdentify(L_RECOG* recog);
         internal static  extern void rchaDestroy(L_RCHA** prcha);
         internal static  extern void rchDestroy(L_RCH** prch);
         internal static  extern int rchaExtract(L_RCHA* rcha, NUMA** pnaindex, NUMA** pnascore, SARRAY** psatext, NUMA** pnasample, NUMA** pnaxloc, NUMA** pnayloc, NUMA** pnawidth);
         internal static  extern int rchExtract(L_RCH* rch, l_int32* pindex, l_float32* pscore, char** ptext, l_int32* psample, l_int32* pxloc, l_int32* pyloc, l_int32* pwidth);
         internal static  extern PIX* recogProcessToIdentify(L_RECOG* recog, PIX* pixs, int pad);
         internal static  extern SARRAY* recogExtractNumbers(L_RECOG* recog, HandleRef boxas, float scorethresh, int spacethresh, BOXAA** pbaa, NUMAA** pnaa);
         internal static  extern HandleRef showExtractNumbers(PIX* pixs, SARRAY* sa, BOXAA* baa, NUMAA* naa, PIX** ppixdb);
         internal static  extern int recogTrainLabeled(L_RECOG* recog, PIX* pixs, HandleRef box, char* text, int debug);
         internal static  extern int recogProcessLabeled(L_RECOG* recog, PIX* pixs, HandleRef box, char* text, PIX** ppix);
         internal static  extern int recogAddSample(L_RECOG* recog, PIX* pix, int debug);
         internal static  extern PIX* recogModifyTemplate(L_RECOG* recog, PIX* pixs);
         internal static  extern int recogAverageSamples(L_RECOG** precog, int debug);
         internal static  extern int pixaAccumulateSamples(PIXA* pixa, PTA* pta, PIX** ppixd, l_float32* px, l_float32* py);
         internal static  extern int recogTrainingFinished(L_RECOG** precog, int modifyflag, int minsize, float minfract);
         internal static  extern HandleRef recogFilterPixaBySize(PIXA* pixas, int setsize, int maxkeep, float max_ht_ratio, NUMA** pna);
         internal static  extern PIXAA* recogSortPixaByClass(PIXA* pixa, int setsize);
         internal static  extern int recogRemoveOutliers1(L_RECOG** precog, float minscore, int mintarget, int minsize, PIX** ppixsave, PIX** ppixrem);
         internal static  extern HandleRef pixaRemoveOutliers1(PIXA* pixas, float minscore, int mintarget, int minsize, PIX** ppixsave, PIX** ppixrem);
         internal static  extern int recogRemoveOutliers2(L_RECOG** precog, float minscore, int minsize, PIX** ppixsave, PIX** ppixrem);
         internal static  extern HandleRef pixaRemoveOutliers2(PIXA* pixas, float minscore, int minsize, PIX** ppixsave, PIX** ppixrem);
         internal static  extern HandleRef recogTrainFromBoot(L_RECOG* recogboot, HandleRef pixas, float minscore, int threshold, int debug);
         internal static  extern int recogPadDigitTrainingSet(L_RECOG** precog, int scaleh, int linew);
         internal static  extern int recogIsPaddingNeeded(L_RECOG* recog, SARRAY** psa);
         internal static  extern HandleRef recogAddDigitPadTemplates(L_RECOG* recog, SARRAY* sa);
         internal static  extern L_RECOG* recogMakeBootDigitRecog(int scaleh, int linew, int maxyshift, int debug);
         internal static  extern HandleRef recogMakeBootDigitTemplates(int debug);
         internal static  extern int recogShowContent(FILE* fp, L_RECOG* recog, int index, int display);
         internal static  extern int recogDebugAverages(L_RECOG** precog, int debug);
         internal static  extern int recogShowAverageTemplates(L_RECOG* recog);
         internal static  extern int recogShowMatchesInRange(L_RECOG* recog, HandleRef pixa, float minscore, float maxscore, int display);
         internal static  extern PIX* recogShowMatch(L_RECOG* recog, PIX* pix1, PIX* pix2, HandleRef box, int index, float score);
         internal static  extern int regTestSetup(int argc, char** argv, L_REGPARAMS** prp);
         internal static  extern int regTestCleanup(L_REGPARAMS* rp);
         internal static  extern int regTestCompareValues(L_REGPARAMS* rp, float val1, float val2, float delta);
         internal static  extern int regTestCompareStrings(L_REGPARAMS* rp, l_uint8* string1, size_t bytes1, l_uint8* string2, size_t bytes2);
         internal static  extern int regTestComparePix(L_REGPARAMS* rp, PIX* pix1, PIX* pix2);
         internal static  extern int regTestCompareSimilarPix(L_REGPARAMS* rp, PIX* pix1, PIX* pix2, int mindiff, float maxfract, int printstats);
         internal static  extern int regTestCheckFile(L_REGPARAMS* rp, const char* localname );
         internal static  extern int regTestCompareFiles(L_REGPARAMS* rp, int index1, int index2);
         internal static  extern int regTestWritePixAndCheck(L_REGPARAMS* rp, PIX* pix, int format);
         internal static  extern char* regTestGenLocalFilename(L_REGPARAMS* rp, int index, int format);
         internal static  extern int pixRasterop(PIX* pixd, int dx, int dy, int dw, int dh, int op, PIX* pixs, int sx, int sy);
         internal static  extern int pixRasteropVip(PIX* pixd, int bx, int bw, int vshift, int incolor);
         internal static  extern int pixRasteropHip(PIX* pixd, int by, int bh, int hshift, int incolor);
         internal static  extern PIX* pixTranslate(PIX* pixd, PIX* pixs, int hshift, int vshift, int incolor);
         internal static  extern int pixRasteropIP(PIX* pixd, int hshift, int vshift, int incolor);
         internal static  extern int pixRasteropFullImage(PIX* pixd, PIX* pixs, int op);
         internal static  extern void rasteropVipLow(l_uint32* data, int pixw, int pixh, int depth, int wpl, int x, int w, int shift);
         internal static  extern void rasteropHipLow(l_uint32* data, int pixh, int depth, int wpl, int y, int h, int shift);
         internal static  extern void shiftDataHorizontalLow(l_uint32* datad, int wpld, l_uint32* datas, int wpls, int shift);
         internal static  extern void rasteropUniLow(l_uint32* datad, int dpixw, int dpixh, int depth, int dwpl, int dx, int dy, int dw, int dh, int op);
         internal static  extern void rasteropLow(l_uint32* datad, int dpixw, int dpixh, int depth, int dwpl, int dx, int dy, int dw, int dh, int op, l_uint32* datas, int spixw, int spixh, int swpl, int sx, int sy);
         internal static  extern PIX* pixRotate(PIX* pixs, float angle, int type, int incolor, int width, int height);
         internal static  extern PIX* pixEmbedForRotation(PIX* pixs, float angle, int incolor, int width, int height);
         internal static  extern PIX* pixRotateBySampling(PIX* pixs, int xcen, int ycen, float angle, int incolor);
         internal static  extern PIX* pixRotateBinaryNice(PIX* pixs, float angle, int incolor);
         internal static  extern PIX* pixRotateWithAlpha(PIX* pixs, float angle, PIX* pixg, float fract);
         internal static  extern PIX* pixRotateAM(PIX* pixs, float angle, int incolor);
         internal static  extern PIX* pixRotateAMColor(PIX* pixs, float angle, uint colorval);
         internal static  extern PIX* pixRotateAMGray(PIX* pixs, float angle, byte grayval);
         internal static  extern PIX* pixRotateAMCorner(PIX* pixs, float angle, int incolor);
         internal static  extern PIX* pixRotateAMColorCorner(PIX* pixs, float angle, uint fillval);
         internal static  extern PIX* pixRotateAMGrayCorner(PIX* pixs, float angle, byte grayval);
         internal static  extern PIX* pixRotateAMColorFast(PIX* pixs, float angle, uint colorval);
         internal static  extern void rotateAMColorLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, float angle, uint colorval);
         internal static  extern void rotateAMGrayLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, float angle, byte grayval);
         internal static  extern void rotateAMColorCornerLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, float angle, uint colorval);
         internal static  extern void rotateAMGrayCornerLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, float angle, byte grayval);
         internal static  extern void rotateAMColorFastLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datas, int wpls, float angle, uint colorval);
         internal static  extern PIX* pixRotateOrth(PIX* pixs, int quads);
         internal static  extern PIX* pixRotate180(PIX* pixd, PIX* pixs);
         internal static  extern PIX* pixRotate90(PIX* pixs, int direction);
         internal static  extern PIX* pixFlipLR(PIX* pixd, PIX* pixs);
         internal static  extern PIX* pixFlipTB(PIX* pixd, PIX* pixs);
         internal static  extern PIX* pixRotateShear(PIX* pixs, int xcen, int ycen, float angle, int incolor);
         internal static  extern PIX* pixRotate2Shear(PIX* pixs, int xcen, int ycen, float angle, int incolor);
         internal static  extern PIX* pixRotate3Shear(PIX* pixs, int xcen, int ycen, float angle, int incolor);
         internal static  extern int pixRotateShearIP(PIX* pixs, int xcen, int ycen, float angle, int incolor);
         internal static  extern PIX* pixRotateShearCenter(PIX* pixs, float angle, int incolor);
         internal static  extern int pixRotateShearCenterIP(PIX* pixs, float angle, int incolor);
         internal static  extern PIX* pixStrokeWidthTransform(PIX* pixs, int color, int depth, int nangles);
         internal static  extern PIX* pixRunlengthTransform(PIX* pixs, int color, int direction, int depth);
         internal static  extern int pixFindHorizontalRuns(PIX* pix, int y, l_int32* xstart, l_int32* xend, l_int32* pn);
         internal static  extern int pixFindVerticalRuns(PIX* pix, int x, l_int32* ystart, l_int32* yend, l_int32* pn);
         internal static  extern NUMA* pixFindMaxRuns(PIX* pix, int direction, NUMA** pnastart);
         internal static  extern int pixFindMaxHorizontalRunOnLine(PIX* pix, int y, l_int32* pxstart, l_int32* psize);
         internal static  extern int pixFindMaxVerticalRunOnLine(PIX* pix, int x, l_int32* pystart, l_int32* psize);
         internal static  extern int runlengthMembershipOnLine(l_int32* buffer, int size, int depth, l_int32* start, l_int32* end, int n);
         internal static  extern l_int32* makeMSBitLocTab(int bitval);
         internal static  extern SARRAY* sarrayCreate(int n);
         internal static  extern SARRAY* sarrayCreateInitialized(int n, char* initstr);
         internal static  extern SARRAY* sarrayCreateWordsFromString( const char*string );
         internal static  extern SARRAY* sarrayCreateLinesFromString( const char*string, int blankflag );
         internal static  extern void sarrayDestroy(SARRAY** psa);
         internal static  extern SARRAY* sarrayCopy(SARRAY* sa);
         internal static  extern SARRAY* sarrayClone(SARRAY* sa);
         internal static  extern int sarrayAddString(SARRAY* sa, char*string, int copyflag);
         internal static  extern char* sarrayRemoveString(SARRAY* sa, int index);
         internal static  extern int sarrayReplaceString(SARRAY* sa, int index, char* newstr, int copyflag);
         internal static  extern int sarrayClear(SARRAY* sa);
         internal static  extern int sarrayGetCount(SARRAY* sa);
         internal static  extern char** sarrayGetArray(SARRAY* sa, l_int32* pnalloc, l_int32* pn);
         internal static  extern char* sarrayGetString(SARRAY* sa, int index, int copyflag);
         internal static  extern int sarrayGetRefcount(SARRAY* sa);
         internal static  extern int sarrayChangeRefcount(SARRAY* sa, int delta);
         internal static  extern char* sarrayToString(SARRAY* sa, int addnlflag);
         internal static  extern char* sarrayToStringRange(SARRAY* sa, int first, int nstrings, int addnlflag);
         internal static  extern int sarrayJoin(SARRAY* sa1, SARRAY* sa2);
         internal static  extern int sarrayAppendRange(SARRAY* sa1, SARRAY* sa2, int start, int end);
         internal static  extern int sarrayPadToSameSize(SARRAY* sa1, SARRAY* sa2, char* padstring);
         internal static  extern SARRAY* sarrayConvertWordsToLines(SARRAY* sa, int linesize);
         internal static  extern int sarraySplitString(SARRAY* sa, const char* str, const char* separators );
         internal static  extern SARRAY* sarraySelectBySubstring(SARRAY* sain, const char* substr );
         internal static  extern SARRAY* sarraySelectByRange(SARRAY* sain, int first, int last);
         internal static  extern int sarrayParseRange(SARRAY* sa, int start, l_int32* pactualstart, l_int32* pend, l_int32* pnewstart, const char* substr, int loc );
         internal static  extern SARRAY* sarrayRead( const char* filename );
         internal static  extern SARRAY* sarrayReadStream(FILE* fp);
         internal static  extern SARRAY* sarrayReadMem( const l_uint8* data, size_t size );
         internal static  extern int sarrayWrite( const char* filename, SARRAY *sa );
         internal static  extern int sarrayWriteStream(FILE* fp, SARRAY* sa);
         internal static  extern int sarrayWriteMem(l_uint8** pdata, size_t* psize, SARRAY* sa);
         internal static  extern int sarrayAppend( const char* filename, SARRAY *sa );
         internal static  extern SARRAY* getNumberedPathnamesInDirectory( const char* dirname, const char* substr, int numpre, int numpost, int maxnum );
         internal static  extern SARRAY* getSortedPathnamesInDirectory( const char* dirname, const char* substr, int first, int nfiles );
         internal static  extern SARRAY* convertSortedToNumberedPathnames(SARRAY* sa, int numpre, int numpost, int maxnum);
         internal static  extern SARRAY* getFilenamesInDirectory( const char* dirname );
         internal static  extern SARRAY* sarraySort(SARRAY* saout, SARRAY* sain, int sortorder);
         internal static  extern SARRAY* sarraySortByIndex(SARRAY* sain, NUMA* naindex);
         internal static  extern int stringCompareLexical( const char* str1, const char* str2 );
         internal static  extern SARRAY* sarrayUnionByAset(SARRAY* sa1, SARRAY* sa2);
         internal static  extern SARRAY* sarrayRemoveDupsByAset(SARRAY* sas);
         internal static  extern SARRAY* sarrayIntersectionByAset(SARRAY* sa1, SARRAY* sa2);
         internal static  extern L_ASET* l_asetCreateFromSarray(SARRAY* sa);
         internal static  extern int sarrayRemoveDupsByHash(SARRAY* sas, SARRAY** psad, L_DNAHASH** pdahash);
         internal static  extern SARRAY* sarrayIntersectionByHash(SARRAY* sa1, SARRAY* sa2);
         internal static  extern int sarrayFindStringByHash(SARRAY* sa, L_DNAHASH* dahash, const char* str, int *pindex );
         internal static  extern L_DNAHASH* l_dnaHashCreateFromSarray(SARRAY* sa);
         internal static  extern SARRAY* sarrayGenerateIntegers(int n);
         internal static  extern PIX* pixScale(PIX* pixs, float scalex, float scaley);
         internal static  extern PIX* pixScaleToSizeRel(PIX* pixs, int delw, int delh);
         internal static  extern PIX* pixScaleToSize(PIX* pixs, int wd, int hd);
         internal static  extern PIX* pixScaleGeneral(PIX* pixs, float scalex, float scaley, float sharpfract, int sharpwidth);
         internal static  extern PIX* pixScaleLI(PIX* pixs, float scalex, float scaley);
         internal static  extern PIX* pixScaleColorLI(PIX* pixs, float scalex, float scaley);
         internal static  extern PIX* pixScaleColor2xLI(PIX* pixs);
         internal static  extern PIX* pixScaleColor4xLI(PIX* pixs);
         internal static  extern PIX* pixScaleGrayLI(PIX* pixs, float scalex, float scaley);
         internal static  extern PIX* pixScaleGray2xLI(PIX* pixs);
         internal static  extern PIX* pixScaleGray4xLI(PIX* pixs);
         internal static  extern PIX* pixScaleBySampling(PIX* pixs, float scalex, float scaley);
         internal static  extern PIX* pixScaleBySamplingToSize(PIX* pixs, int wd, int hd);
         internal static  extern PIX* pixScaleByIntSampling(PIX* pixs, int factor);
         internal static  extern PIX* pixScaleRGBToGrayFast(PIX* pixs, int factor, int color);
         internal static  extern PIX* pixScaleRGBToBinaryFast(PIX* pixs, int factor, int thresh);
         internal static  extern PIX* pixScaleGrayToBinaryFast(PIX* pixs, int factor, int thresh);
         internal static  extern PIX* pixScaleSmooth(PIX* pix, float scalex, float scaley);
         internal static  extern PIX* pixScaleRGBToGray2(PIX* pixs, float rwt, float gwt, float bwt);
         internal static  extern PIX* pixScaleAreaMap(PIX* pix, float scalex, float scaley);
         internal static  extern PIX* pixScaleAreaMap2(PIX* pix);
         internal static  extern PIX* pixScaleBinary(PIX* pixs, float scalex, float scaley);
         internal static  extern PIX* pixScaleToGray(PIX* pixs, float scalefactor);
         internal static  extern PIX* pixScaleToGrayFast(PIX* pixs, float scalefactor);
         internal static  extern PIX* pixScaleToGray2(PIX* pixs);
         internal static  extern PIX* pixScaleToGray3(PIX* pixs);
         internal static  extern PIX* pixScaleToGray4(PIX* pixs);
         internal static  extern PIX* pixScaleToGray6(PIX* pixs);
         internal static  extern PIX* pixScaleToGray8(PIX* pixs);
         internal static  extern PIX* pixScaleToGray16(PIX* pixs);
         internal static  extern PIX* pixScaleToGrayMipmap(PIX* pixs, float scalefactor);
         internal static  extern PIX* pixScaleMipmap(PIX* pixs1, PIX* pixs2, float scale);
         internal static  extern PIX* pixExpandReplicate(PIX* pixs, int factor);
         internal static  extern PIX* pixScaleGray2xLIThresh(PIX* pixs, int thresh);
         internal static  extern PIX* pixScaleGray2xLIDither(PIX* pixs);
         internal static  extern PIX* pixScaleGray4xLIThresh(PIX* pixs, int thresh);
         internal static  extern PIX* pixScaleGray4xLIDither(PIX* pixs);
         internal static  extern PIX* pixScaleGrayMinMax(PIX* pixs, int xfact, int yfact, int type);
         internal static  extern PIX* pixScaleGrayMinMax2(PIX* pixs, int type);
         internal static  extern PIX* pixScaleGrayRankCascade(PIX* pixs, int level1, int level2, int level3, int level4);
         internal static  extern PIX* pixScaleGrayRank2(PIX* pixs, int rank);
         internal static  extern int pixScaleAndTransferAlpha(PIX* pixd, PIX* pixs, float scalex, float scaley);
         internal static  extern PIX* pixScaleWithAlpha(PIX* pixs, float scalex, float scaley, PIX* pixg, float fract);
         internal static  extern void scaleColorLILow(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int ws, int hs, int wpls);
         internal static  extern void scaleGrayLILow(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int ws, int hs, int wpls);
         internal static  extern void scaleColor2xLILow(l_uint32* datad, int wpld, l_uint32* datas, int ws, int hs, int wpls);
         internal static  extern void scaleColor2xLILineLow(l_uint32* lined, int wpld, l_uint32* lines, int ws, int wpls, int lastlineflag);
         internal static  extern void scaleGray2xLILow(l_uint32* datad, int wpld, l_uint32* datas, int ws, int hs, int wpls);
         internal static  extern void scaleGray2xLILineLow(l_uint32* lined, int wpld, l_uint32* lines, int ws, int wpls, int lastlineflag);
         internal static  extern void scaleGray4xLILow(l_uint32* datad, int wpld, l_uint32* datas, int ws, int hs, int wpls);
         internal static  extern void scaleGray4xLILineLow(l_uint32* lined, int wpld, l_uint32* lines, int ws, int wpls, int lastlineflag);
         internal static  extern int scaleBySamplingLow(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int ws, int hs, int d, int wpls);
         internal static  extern int scaleSmoothLow(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int ws, int hs, int d, int wpls, int size);
         internal static  extern void scaleRGBToGray2Low(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int wpls, float rwt, float gwt, float bwt);
         internal static  extern void scaleColorAreaMapLow(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int ws, int hs, int wpls);
         internal static  extern void scaleGrayAreaMapLow(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int ws, int hs, int wpls);
         internal static  extern void scaleAreaMapLow2(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int d, int wpls);
         internal static  extern int scaleBinaryLow(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int ws, int hs, int wpls);
         internal static  extern void scaleToGray2Low(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int wpls, l_uint32* sumtab, l_uint8* valtab);
         internal static  extern l_uint32* makeSumTabSG2(void );
         internal static  extern l_uint8* makeValTabSG2(void );
         internal static  extern void scaleToGray3Low(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int wpls, l_uint32* sumtab, l_uint8* valtab);
         internal static  extern l_uint32* makeSumTabSG3(void );
         internal static  extern l_uint8* makeValTabSG3(void );
         internal static  extern void scaleToGray4Low(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int wpls, l_uint32* sumtab, l_uint8* valtab);
         internal static  extern l_uint32* makeSumTabSG4(void );
         internal static  extern l_uint8* makeValTabSG4(void );
         internal static  extern void scaleToGray6Low(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int wpls, l_int32* tab8, l_uint8* valtab);
         internal static  extern l_uint8* makeValTabSG6(void );
         internal static  extern void scaleToGray8Low(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int wpls, l_int32* tab8, l_uint8* valtab);
         internal static  extern l_uint8* makeValTabSG8(void );
         internal static  extern void scaleToGray16Low(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas, int wpls, l_int32* tab8);
         internal static  extern int scaleMipmapLow(l_uint32* datad, int wd, int hd, int wpld, l_uint32* datas1, int wpls1, l_uint32* datas2, int wpls2, float red);
         internal static  extern PIX* pixSeedfillBinary(PIX* pixd, PIX* pixs, PIX* pixm, int connectivity);
         internal static  extern PIX* pixSeedfillBinaryRestricted(PIX* pixd, PIX* pixs, PIX* pixm, int connectivity, int xmax, int ymax);
         internal static  extern PIX* pixHolesByFilling(PIX* pixs, int connectivity);
         internal static  extern PIX* pixFillClosedBorders(PIX* pixs, int connectivity);
         internal static  extern PIX* pixExtractBorderConnComps(PIX* pixs, int connectivity);
         internal static  extern PIX* pixRemoveBorderConnComps(PIX* pixs, int connectivity);
         internal static  extern PIX* pixFillBgFromBorder(PIX* pixs, int connectivity);
         internal static  extern PIX* pixFillHolesToBoundingRect(PIX* pixs, int minsize, float maxhfract, float minfgfract);
         internal static  extern int pixSeedfillGray(PIX* pixs, PIX* pixm, int connectivity);
         internal static  extern int pixSeedfillGrayInv(PIX* pixs, PIX* pixm, int connectivity);
         internal static  extern int pixSeedfillGraySimple(PIX* pixs, PIX* pixm, int connectivity);
         internal static  extern int pixSeedfillGrayInvSimple(PIX* pixs, PIX* pixm, int connectivity);
         internal static  extern PIX* pixSeedfillGrayBasin(PIX* pixb, PIX* pixm, int delta, int connectivity);
         internal static  extern PIX* pixDistanceFunction(PIX* pixs, int connectivity, int outdepth, int boundcond);
         internal static  extern PIX* pixSeedspread(PIX* pixs, int connectivity);
         internal static  extern int pixLocalExtrema(PIX* pixs, int maxmin, int minmax, PIX** ppixmin, PIX** ppixmax);
         internal static  extern int pixSelectedLocalExtrema(PIX* pixs, int mindist, PIX** ppixmin, PIX** ppixmax);
         internal static  extern PIX* pixFindEqualValues(PIX* pixs1, PIX* pixs2);
         internal static  extern int pixSelectMinInConnComp(PIX* pixs, PIX* pixm, PTA** ppta, NUMA** pnav);
         internal static  extern PIX* pixRemoveSeededComponents(PIX* pixd, PIX* pixs, PIX* pixm, int connectivity, int bordersize);
         internal static  extern void seedfillBinaryLow(l_uint32* datas, int hs, int wpls, l_uint32* datam, int hm, int wplm, int connectivity);
         internal static  extern void seedfillGrayLow(l_uint32* datas, int w, int h, int wpls, l_uint32* datam, int wplm, int connectivity);
         internal static  extern void seedfillGrayInvLow(l_uint32* datas, int w, int h, int wpls, l_uint32* datam, int wplm, int connectivity);
         internal static  extern void seedfillGrayLowSimple(l_uint32* datas, int w, int h, int wpls, l_uint32* datam, int wplm, int connectivity);
         internal static  extern void seedfillGrayInvLowSimple(l_uint32* datas, int w, int h, int wpls, l_uint32* datam, int wplm, int connectivity);
         internal static  extern void distanceFunctionLow(l_uint32* datad, int w, int h, int d, int wpld, int connectivity);
         internal static  extern void seedspreadLow(l_uint32* datad, int w, int h, int wpld, l_uint32* datat, int wplt, int connectivity);
         internal static  extern SELA* selaCreate(int n);
         internal static  extern void selaDestroy(SELA** psela);
         internal static  extern SEL* selCreate(int height, int width, const char* name );
         internal static  extern void selDestroy(SEL** psel);
         internal static  extern SEL* selCopy(SEL* sel);
         internal static  extern SEL* selCreateBrick(int h, int w, int cy, int cx, int type);
         internal static  extern SEL* selCreateComb(int factor1, int factor2, int direction);
         internal static  extern l_int32** create2dIntArray(int sy, int sx);
         internal static  extern int selaAddSel(SELA* sela, SEL* sel, const char* selname, int copyflag );
         internal static  extern int selaGetCount(SELA* sela);
         internal static  extern SEL* selaGetSel(SELA* sela, int i);
         internal static  extern char* selGetName(SEL* sel);
         internal static  extern int selSetName(SEL* sel, const char* name );
         internal static  extern int selaFindSelByName(SELA* sela, const char* name, int *pindex, SEL** psel );
         internal static  extern int selGetElement(SEL* sel, int row, int col, l_int32* ptype);
         internal static  extern int selSetElement(SEL* sel, int row, int col, int type);
         internal static  extern int selGetParameters(SEL* sel, l_int32* psy, l_int32* psx, l_int32* pcy, l_int32* pcx);
         internal static  extern int selSetOrigin(SEL* sel, int cy, int cx);
         internal static  extern int selGetTypeAtOrigin(SEL* sel, l_int32* ptype);
         internal static  extern char* selaGetBrickName(SELA* sela, int hsize, int vsize);
         internal static  extern char* selaGetCombName(SELA* sela, int size, int direction);
         internal static  extern int getCompositeParameters(int size, l_int32* psize1, l_int32* psize2, char** pnameh1, char** pnameh2, char** pnamev1, char** pnamev2);
         internal static  extern SARRAY* selaGetSelnames(SELA* sela);
         internal static  extern int selFindMaxTranslations(SEL* sel, l_int32* pxp, l_int32* pyp, l_int32* pxn, l_int32* pyn);
         internal static  extern SEL* selRotateOrth(SEL* sel, int quads);
         internal static  extern SELA* selaRead( const char* fname );
         internal static  extern SELA* selaReadStream(FILE* fp);
         internal static  extern SEL* selRead( const char* fname );
         internal static  extern SEL* selReadStream(FILE* fp);
         internal static  extern int selaWrite( const char* fname, SELA *sela );
         internal static  extern int selaWriteStream(FILE* fp, SELA* sela);
         internal static  extern int selWrite( const char* fname, SEL *sel );
         internal static  extern int selWriteStream(FILE* fp, SEL* sel);
         internal static  extern SEL* selCreateFromString( const char* text, int h, int w, const char* name );
         internal static  extern char* selPrintToString(SEL* sel);
         internal static  extern SELA* selaCreateFromFile( const char* filename );
         internal static  extern SEL* selCreateFromPta(HandleRef pta, int cy, int cx, const char* name );
         internal static  extern SEL* selCreateFromPix(PIX* pix, int cy, int cx, const char* name );
         internal static  extern SEL* selReadFromColorImage( const char* pathname );
         internal static  extern SEL* selCreateFromColorPix(PIX* pixs, char* selname);
         internal static  extern PIX* selDisplayInPix(SEL* sel, int size, int gthick);
         internal static  extern PIX* selaDisplayInPix(SELA* sela, int size, int gthick, int spacing, int ncols);
         internal static  extern SELA* selaAddBasic(SELA* sela);
         internal static  extern SELA* selaAddHitMiss(SELA* sela);
         internal static  extern SELA* selaAddDwaLinear(SELA* sela);
         internal static  extern SELA* selaAddDwaCombs(SELA* sela);
         internal static  extern SELA* selaAddCrossJunctions(SELA* sela, float hlsize, float mdist, int norient, int debugflag);
         internal static  extern SELA* selaAddTJunctions(SELA* sela, float hlsize, float mdist, int norient, int debugflag);
         internal static  extern SELA* sela4ccThin(SELA* sela);
         internal static  extern SELA* sela8ccThin(SELA* sela);
         internal static  extern SELA* sela4and8ccThin(SELA* sela);
         internal static  extern SEL* pixGenerateSelWithRuns(PIX* pixs, int nhlines, int nvlines, int distance, int minlength, int toppix, int botpix, int leftpix, int rightpix, PIX** ppixe);
         internal static  extern SEL* pixGenerateSelRandom(PIX* pixs, float hitfract, float missfract, int distance, int toppix, int botpix, int leftpix, int rightpix, PIX** ppixe);
         internal static  extern SEL* pixGenerateSelBoundary(PIX* pixs, int hitdist, int missdist, int hitskip, int missskip, int topflag, int botflag, int leftflag, int rightflag, PIX** ppixe);
         internal static  extern NUMA* pixGetRunCentersOnLine(PIX* pixs, int x, int y, int minlength);
         internal static  extern NUMA* pixGetRunsOnLine(PIX* pixs, int x1, int y1, int x2, int y2);
         internal static  extern PTA* pixSubsampleBoundaryPixels(PIX* pixs, int skip);
         internal static  extern int adjacentOnPixelInRaster(PIX* pixs, int x, int y, l_int32* pxa, l_int32* pya);
         internal static  extern PIX* pixDisplayHitMissSel(PIX* pixs, SEL* sel, int scalefactor, uint hitcolor, uint misscolor);
         internal static  extern PIX* pixHShear(PIX* pixd, PIX* pixs, int yloc, float radang, int incolor);
         internal static  extern PIX* pixVShear(PIX* pixd, PIX* pixs, int xloc, float radang, int incolor);
         internal static  extern PIX* pixHShearCorner(PIX* pixd, PIX* pixs, float radang, int incolor);
         internal static  extern PIX* pixVShearCorner(PIX* pixd, PIX* pixs, float radang, int incolor);
         internal static  extern PIX* pixHShearCenter(PIX* pixd, PIX* pixs, float radang, int incolor);
         internal static  extern PIX* pixVShearCenter(PIX* pixd, PIX* pixs, float radang, int incolor);
         internal static  extern int pixHShearIP(PIX* pixs, int yloc, float radang, int incolor);
         internal static  extern int pixVShearIP(PIX* pixs, int xloc, float radang, int incolor);
         internal static  extern PIX* pixHShearLI(PIX* pixs, int yloc, float radang, int incolor);
         internal static  extern PIX* pixVShearLI(PIX* pixs, int xloc, float radang, int incolor);
         internal static  extern PIX* pixDeskewBoth(PIX* pixs, int redsearch);
         internal static  extern PIX* pixDeskew(PIX* pixs, int redsearch);
         internal static  extern PIX* pixFindSkewAndDeskew(PIX* pixs, int redsearch, l_float32* pangle, l_float32* pconf);
         internal static  extern PIX* pixDeskewGeneral(PIX* pixs, int redsweep, float sweeprange, float sweepdelta, int redsearch, int thresh, l_float32* pangle, l_float32* pconf);
         internal static  extern int pixFindSkew(PIX* pixs, l_float32* pangle, l_float32* pconf);
         internal static  extern int pixFindSkewSweep(PIX* pixs, l_float32* pangle, int reduction, float sweeprange, float sweepdelta);
         internal static  extern int pixFindSkewSweepAndSearch(PIX* pixs, l_float32* pangle, l_float32* pconf, int redsweep, int redsearch, float sweeprange, float sweepdelta, float minbsdelta);
         internal static  extern int pixFindSkewSweepAndSearchScore(PIX* pixs, l_float32* pangle, l_float32* pconf, l_float32* pendscore, int redsweep, int redsearch, float sweepcenter, float sweeprange, float sweepdelta, float minbsdelta);
         internal static  extern int pixFindSkewSweepAndSearchScorePivot(PIX* pixs, l_float32* pangle, l_float32* pconf, l_float32* pendscore, int redsweep, int redsearch, float sweepcenter, float sweeprange, float sweepdelta, float minbsdelta, int pivot);
         internal static  extern int pixFindSkewOrthogonalRange(PIX* pixs, l_float32* pangle, l_float32* pconf, int redsweep, int redsearch, float sweeprange, float sweepdelta, float minbsdelta, float confprior);
         internal static  extern int pixFindDifferentialSquareSum(PIX* pixs, l_float32* psum);
         internal static  extern int pixFindNormalizedSquareSum(PIX* pixs, l_float32* phratio, l_float32* pvratio, l_float32* pfract);
         internal static  extern PIX* pixReadStreamSpix(FILE* fp);
         internal static  extern int readHeaderSpix( const char* filename, int *pwidth, l_int32* pheight, int *pbps, l_int32* pspp, int *piscmap );
         internal static  extern int freadHeaderSpix(FILE* fp, l_int32* pwidth, l_int32* pheight, l_int32* pbps, l_int32* pspp, l_int32* piscmap);
         internal static  extern int sreadHeaderSpix( const l_uint32* data, int *pwidth, l_int32* pheight, int *pbps, l_int32* pspp, int *piscmap );
         internal static  extern int pixWriteStreamSpix(FILE* fp, PIX* pix);
         internal static  extern PIX* pixReadMemSpix( const l_uint8* data, size_t size );
         internal static  extern int pixWriteMemSpix(l_uint8** pdata, size_t* psize, PIX* pix);
         internal static  extern int pixSerializeToMemory(PIX* pixs, l_uint32** pdata, size_t* pnbytes);
         internal static  extern PIX* pixDeserializeFromMemory( const l_uint32* data, size_t nbytes );
         internal static  extern L_STACK* lstackCreate(int nalloc);
         internal static  extern void lstackDestroy(L_STACK** plstack, int freeflag);
         internal static  extern int lstackAdd(L_STACK* lstack, void* item);
         internal static  extern void* lstackRemove(L_STACK* lstack);
         internal static  extern int lstackGetCount(L_STACK* lstack);
         internal static  extern int lstackPrint(FILE* fp, L_STACK* lstack);
         internal static  extern L_STRCODE* strcodeCreate(int fileno);
         internal static  extern int strcodeCreateFromFile( const char* filein, int fileno, const char* outdir );
         internal static  extern int strcodeGenerate(L_STRCODE* strcode, const char* filein, const char* type );
         internal static  extern int strcodeFinalize(L_STRCODE** pstrcode, const char* outdir );
         internal static  extern int l_getStructStrFromFile( const char* filename, int field, char** pstr );
         internal static  extern int pixFindStrokeLength(PIX* pixs, l_int32* tab8, l_int32* plength);
         internal static  extern int pixFindStrokeWidth(PIX* pixs, float thresh, l_int32* tab8, l_float32* pwidth, NUMA** pnahisto);
         internal static  extern NUMA* pixaFindStrokeWidth(PIXA* pixa, float thresh, l_int32* tab8, int debug);
         internal static  extern HandleRef pixaModifyStrokeWidth(PIXA* pixas, float targetw);
         internal static  extern PIX* pixModifyStrokeWidth(PIX* pixs, float width, float targetw);
         internal static  extern HandleRef pixaSetStrokeWidth(PIXA* pixas, int width, int thinfirst, int connectivity);
         internal static  extern PIX* pixSetStrokeWidth(PIX* pixs, int width, int thinfirst, int connectivity);
         internal static  extern l_int32* sudokuReadFile( const char* filename );
         internal static  extern l_int32* sudokuReadString( const char* str );
         internal static  extern L_SUDOKU* sudokuCreate(l_int32* array);
         internal static  extern void sudokuDestroy(L_SUDOKU** psud);
         internal static  extern int sudokuSolve(L_SUDOKU* sud);
         internal static  extern int sudokuTestUniqueness(l_int32* array, l_int32* punique);
         internal static  extern L_SUDOKU* sudokuGenerate(l_int32* array, int seed, int minelems, int maxtries);
         internal static  extern int sudokuOutput(L_SUDOKU* sud, int arraytype);
         internal static  extern PIX* pixAddSingleTextblock(PIX* pixs, L_BMF* bmf, const char* textstr, uint val, int location, int *poverflow );
         internal static  extern PIX* pixAddTextlines(PIX* pixs, L_BMF* bmf, const char* textstr, uint val, int location );
         internal static  extern int pixSetTextblock(PIX* pixs, L_BMF* bmf, const char* textstr, uint val, int x0, int y0, int wtext, int firstindent, l_int32* poverflow );
         internal static  extern int pixSetTextline(PIX* pixs, L_BMF* bmf, const char* textstr, uint val, int x0, int y0, l_int32* pwidth, int *poverflow );
         internal static  extern HandleRef pixaAddTextNumber(PIXA* pixas, L_BMF* bmf, NUMA* na, uint val, int location);
         internal static  extern HandleRef pixaAddTextlines(PIXA* pixas, L_BMF* bmf, SARRAY* sa, uint val, int location);
         internal static  extern int pixaAddPixWithText(PIXA* pixa, PIX* pixs, int reduction, L_BMF* bmf, const char* textstr, uint val, int location );
         internal static  extern SARRAY* bmfGetLineStrings(L_BMF* bmf, const char* textstr, int maxw, int firstindent, int *ph );
         internal static  extern NUMA* bmfGetWordWidths(L_BMF* bmf, const char* textstr, SARRAY *sa );
         internal static  extern int bmfGetStringWidth(L_BMF* bmf, const char* textstr, int *pw );
         internal static  extern SARRAY* splitStringToParagraphs(char* textstr, int splitflag);
         internal static  extern PIX* pixReadTiff( const char* filename, int n );
         internal static  extern PIX* pixReadStreamTiff(FILE* fp, int n);
         internal static  extern int pixWriteTiff( const char* filename, PIX *pix, int comptype, const char* modestr );
         internal static  extern int pixWriteTiffCustom( const char* filename, PIX *pix, int comptype, const char* modestr, NUMA *natags, SARRAY* savals, SARRAY *satypes, NUMA* nasizes );
         internal static  extern int pixWriteStreamTiff(FILE* fp, PIX* pix, int comptype);
         internal static  extern int pixWriteStreamTiffWA(FILE* fp, PIX* pix, int comptype, const char* modestr );
         internal static  extern PIX* pixReadFromMultipageTiff( const char* fname, size_t *poffset );
         internal static  extern HandleRef pixaReadMultipageTiff( const char* filename );
         internal static  extern int pixaWriteMultipageTiff( const char* fname, PIXA *pixa );
         internal static  extern int writeMultipageTiff( const char* dirin, const char* substr, const char* fileout );
         internal static  extern int writeMultipageTiffSA(SARRAY* sa, const char* fileout );
         internal static  extern int fprintTiffInfo(FILE* fpout, const char* tiffile );
         internal static  extern int tiffGetCount(FILE* fp, l_int32* pn);
         internal static  extern int getTiffResolution(FILE* fp, l_int32* pxres, l_int32* pyres);
         internal static  extern int readHeaderTiff( const char* filename, int n, l_int32* pwidth, int *pheight, l_int32* pbps, int *pspp, l_int32* pres, int *pcmap, l_int32* pformat );
         internal static  extern int freadHeaderTiff(FILE* fp, int n, l_int32* pwidth, l_int32* pheight, l_int32* pbps, l_int32* pspp, l_int32* pres, l_int32* pcmap, l_int32* pformat);
         internal static  extern int readHeaderMemTiff( const l_uint8* cdata, size_t size, int n, int *pwidth, l_int32* pheight, int *pbps, l_int32* pspp, int *pres, l_int32* pcmap, int *pformat );
         internal static  extern int findTiffCompression(FILE* fp, l_int32* pcomptype);
         internal static  extern int extractG4DataFromFile( const char* filein, byte **pdata, size_t* pnbytes, int *pw, l_int32* ph, int *pminisblack );
         internal static  extern PIX* pixReadMemTiff( const l_uint8* cdata, size_t size, int n );
         internal static  extern PIX* pixReadMemFromMultipageTiff( const l_uint8* cdata, size_t size, size_t* poffset );
         internal static  extern HandleRef pixaReadMemMultipageTiff( const l_uint8* data, size_t size );
         internal static  extern int pixaWriteMemMultipageTiff(l_uint8** pdata, size_t* psize, HandleRef pixa);
         internal static  extern int pixWriteMemTiff(l_uint8** pdata, size_t* psize, PIX* pix, int comptype);
         internal static  extern int pixWriteMemTiffCustom(l_uint8** pdata, size_t* psize, PIX* pix, int comptype, NUMA* natags, SARRAY* savals, SARRAY* satypes, NUMA* nasizes);
         internal static  extern int setMsgSeverity(int newsev);
         internal static  extern int returnErrorInt( const char* msg, const char* procname, int ival );
         internal static  extern float returnErrorFloat( const char* msg, const char* procname, float fval );
         internal static  extern void* returnErrorPtr( const char* msg, const char* procname, void* pval );
         internal static  extern int filesAreIdentical( const char* fname1, const char* fname2, int *psame );
         internal static  extern l_uint16 convertOnLittleEnd16(l_uint16 shortin);
         internal static  extern l_uint16 convertOnBigEnd16(l_uint16 shortin);
         internal static  extern uint convertOnLittleEnd32(l_uint32 wordin);
         internal static  extern uint convertOnBigEnd32(l_uint32 wordin);
         internal static  extern int fileCorruptByDeletion( const char* filein, float loc, float size, const char* fileout );
         internal static  extern int fileCorruptByMutation( const char* filein, float loc, float size, const char* fileout );
         internal static  extern int genRandomIntegerInRange(int range, int seed, l_int32* pval);
         internal static  extern int lept_roundftoi(float fval);
         internal static  extern int l_hashStringToUint64( const char* str, l_uint64 *phash );
         internal static  extern int l_hashPtToUint64(int x, int y, l_uint64* phash);
         internal static  extern int l_hashFloat64ToUint64(int nbuckets, l_float64 val, l_uint64* phash);
         internal static  extern int findNextLargerPrime(int start, l_uint32* pprime);
         internal static  extern int lept_isPrime(l_uint64 n, l_int32* pis_prime, l_uint32* pfactor);
         internal static  extern uint convertBinaryToGrayCode(l_uint32 val);
         internal static  extern uint convertGrayCodeToBinary(l_uint32 val);
         internal static  extern char* getLeptonicaVersion();
         internal static  extern void startTimer(void );
         internal static  extern float stopTimer(void );
         internal static  extern L_TIMER startTimerNested(void );
         internal static  extern float stopTimerNested(L_TIMER rusage_start);
         internal static  extern void l_getCurrentTime(l_int32* sec, l_int32* usec);
         internal static  extern L_WALLTIMER* startWallTimer(void );
         internal static  extern float stopWallTimer(L_WALLTIMER** ptimer);
         internal static  extern char* l_getFormattedDate();
         internal static  extern char* stringNew( const char* src );
         internal static  extern int stringCopy(char* dest, const char* src, int n );
         internal static  extern int stringReplace(char** pdest, const char* src );
         internal static  extern int stringLength( const char* src, size_t size );
         internal static  extern int stringCat(char* dest, size_t size, const char* src );
         internal static  extern char* stringConcatNew( const char* first, ... );
         internal static  extern char* stringJoin( const char* src1, const char* src2 );
         internal static  extern int stringJoinIP(char** psrc1, const char* src2 );
         internal static  extern char* stringReverse( const char* src );
         internal static  extern char* strtokSafe(char* cstr, const char* seps, char** psaveptr );
         internal static  extern int stringSplitOnToken(char* cstr, const char* seps, char** phead, char** ptail );
         internal static  extern char* stringRemoveChars( const char* src, const char* remchars );
         internal static  extern int stringFindSubstr( const char* src, const char* sub, int *ploc );
         internal static  extern char* stringReplaceSubstr( const char* src, const char* sub1, const char* sub2, int *pfound, l_int32* ploc );
         internal static  extern char* stringReplaceEachSubstr( const char* src, const char* sub1, const char* sub2, int *pcount );
         internal static  extern L_DNA* arrayFindEachSequence( const l_uint8* data, size_t datalen, const l_uint8* sequence, size_t seqlen );
         internal static  extern int arrayFindSequence( const l_uint8* data, size_t datalen, const l_uint8* sequence, size_t seqlen, l_int32* poffset, int *pfound );
         internal static  extern void* reallocNew(void** pindata, int oldsize, int newsize);
         internal static  extern l_uint8* l_binaryRead( const char* filename, size_t *pnbytes );
         internal static  extern l_uint8* l_binaryReadStream(FILE* fp, size_t* pnbytes);
         internal static  extern l_uint8* l_binaryReadSelect( const char* filename, size_t start, size_t nbytes, size_t *pnread );
         internal static  extern l_uint8* l_binaryReadSelectStream(FILE* fp, size_t start, size_t nbytes, size_t* pnread);
         internal static  extern int l_binaryWrite( const char* filename, const char* operation, void* data, size_t nbytes );
         internal static  extern size_t nbytesInFile( const char* filename );
         internal static  extern size_t fnbytesInFile(FILE* fp);
         internal static  extern l_uint8* l_binaryCopy(l_uint8* datas, size_t size);
         internal static  extern int fileCopy( const char* srcfile, const char* newfile );
         internal static  extern int fileConcatenate( const char* srcfile, const char* destfile );
         internal static  extern int fileAppendString( const char* filename, const char* str );
         internal static  extern FILE* fopenReadStream( const char* filename );
         internal static  extern FILE* fopenWriteStream( const char* filename, const char* modestring );
         internal static  extern FILE* fopenReadFromMemory( const l_uint8* data, size_t size );
         internal static  extern FILE* fopenWriteWinTempfile();
         internal static  extern FILE* lept_fopen( const char* filename, const char* mode );
         internal static  extern int lept_fclose(FILE* fp);
         internal static  extern void* lept_calloc(size_t nmemb, size_t size);
         internal static  extern void lept_free(void* ptr);
         internal static  extern int lept_mkdir( const char* subdir );
         internal static  extern int lept_rmdir( const char* subdir );
         internal static  extern void lept_direxists( const char* dir, int *pexists );
         internal static  extern int lept_rm_match( const char* subdir, const char* substr );
         internal static  extern int lept_rm( const char* subdir, const char* tail );
         internal static  extern int lept_rmfile( const char* filepath );
         internal static  extern int lept_mv( const char* srcfile, const char* newdir, const char* newtail, char** pnewpath );
         internal static  extern int lept_cp( const char* srcfile, const char* newdir, const char* newtail, char** pnewpath );
         internal static  extern int splitPathAtDirectory( const char* pathname, char** pdir, char** ptail );
         internal static  extern int splitPathAtExtension( const char* pathname, char** pbasename, char** pextension );
         internal static  extern char* pathJoin( const char* dir, const char* fname );
         internal static  extern char* appendSubdirs( const char* basedir, const char* subdirs );
         internal static  extern int convertSepCharsInPath(char* path, int type);
         internal static  extern char* genPathname( const char* dir, const char* fname );
         internal static  extern int makeTempDirname(char* result, size_t nbytes, const char* subdir );
         internal static  extern int modifyTrailingSlash(char* path, size_t nbytes, int flag);
         internal static  extern char* l_makeTempFilename();
         internal static  extern int extractNumberFromFilename( const char* fname, int numpre, int numpost );
         internal static  extern PIX* pixSimpleCaptcha(PIX* pixs, int border, int nterms, uint seed, uint color, int cmapflag);
         internal static  extern PIX* pixRandomHarmonicWarp(PIX* pixs, float xmag, float ymag, float xfreq, float yfreq, int nx, int ny, uint seed, int grayval);
         internal static  extern PIX* pixWarpStereoscopic(PIX* pixs, int zbend, int zshiftt, int zshiftb, int ybendt, int ybendb, int redleft);
         internal static  extern PIX* pixStretchHorizontal(PIX* pixs, int dir, int type, int hmax, int operation, int incolor);
         internal static  extern PIX* pixStretchHorizontalSampled(PIX* pixs, int dir, int type, int hmax, int incolor);
         internal static  extern PIX* pixStretchHorizontalLI(PIX* pixs, int dir, int type, int hmax, int incolor);
         internal static  extern PIX* pixQuadraticVShear(PIX* pixs, int dir, int vmaxt, int vmaxb, int operation, int incolor);
         internal static  extern PIX* pixQuadraticVShearSampled(PIX* pixs, int dir, int vmaxt, int vmaxb, int incolor);
         internal static  extern PIX* pixQuadraticVShearLI(PIX* pixs, int dir, int vmaxt, int vmaxb, int incolor);
         internal static  extern PIX* pixStereoFromPair(PIX* pix1, PIX* pix2, float rwt, float gwt, float bwt);
         internal static  extern L_WSHED* wshedCreate(PIX* pixs, PIX* pixm, int mindepth, int debugflag);
         internal static  extern void wshedDestroy(L_WSHED** pwshed);
         internal static  extern int wshedApply(L_WSHED* wshed);
         internal static  extern int wshedBasins(L_WSHED* wshed, PIXA** ppixa, NUMA** pnalevels);
         internal static  extern PIX* wshedRenderFill(L_WSHED* wshed);
         internal static  extern PIX* wshedRenderColors(L_WSHED* wshed);
         internal static  extern PIX* pixReadStreamWebP(FILE* fp);
         internal static  extern PIX* pixReadMemWebP( const l_uint8* filedata, size_t filesize );
         internal static  extern int readHeaderWebP( const char* filename, int *pw, l_int32* ph, int *pspp );
         internal static  extern int readHeaderMemWebP( const l_uint8* data, size_t size, l_int32* pw, int *ph, l_int32* pspp );
         internal static  extern int pixWriteWebP( const char* filename, PIX *pixs, int quality, int lossless );
         internal static  extern int pixWriteStreamWebP(FILE* fp, PIX* pixs, int quality, int lossless);
         internal static  extern int pixWriteMemWebP(l_uint8** pencdata, size_t* pencsize, PIX* pixs, int quality, int lossless);
         internal static  extern int pixaWriteFiles( const char* rootname, PIXA *pixa, int format );
         internal static  extern int pixWrite( const char* fname, PIX *pix, int format );
         internal static  extern int pixWriteAutoFormat( const char* filename, PIX *pix );
         internal static  extern int pixWriteStream(FILE* fp, PIX* pix, int format);
         internal static  extern int pixWriteImpliedFormat( const char* filename, PIX *pix, int quality, int progressive );
         internal static  extern int pixChooseOutputFormat(PIX* pix);
         internal static  extern int getImpliedFileFormat( const char* filename );
         internal static  extern int pixGetAutoFormat(PIX* pix, l_int32* pformat);
         internal static  extern const char* getFormatExtension (int format );
         internal static  extern int pixWriteMem(l_uint8** pdata, size_t* psize, PIX* pix, int format);
         internal static  extern int l_fileDisplay( const char* fname, int x, int y, float scale );
         internal static  extern int pixDisplay(PIX* pixs, int x, int y);
         internal static  extern int pixDisplayWithTitle(PIX* pixs, int x, int y, const char* title, int dispflag );
         internal static  extern int pixSaveTiled(PIX* pixs, HandleRef pixa, float scalefactor, int newrow, int space, int dp);
         internal static  extern int pixSaveTiledOutline(PIX* pixs, HandleRef pixa, float scalefactor, int newrow, int space, int linewidth, int dp);
         internal static  extern int pixSaveTiledWithText(PIX* pixs, HandleRef pixa, int outwidth, int newrow, int space, int linewidth, L_BMF* bmf, const char* textstr, uint val, int location );
         internal static  extern void l_chooseDisplayProg(int selection);
         internal static  extern int pixDisplayWrite(PIX* pixs, int reduction);
         internal static  extern int pixDisplayWriteFormat(PIX* pixs, int reduction, int format);
         internal static  extern int pixDisplayMultiple(int res, float scalefactor, const char* fileout );
         internal static  extern l_uint8* zlibCompress(l_uint8* datain, size_t nin, size_t* pnout);
         internal static  extern l_uint8* zlibUncompress(l_uint8* datain, size_t nin, size_t* pnout);
        */
    }
}
