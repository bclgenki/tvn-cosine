﻿using System;
using System.Runtime.InteropServices;

namespace Leptonica
{
    public static class RegUtils
    {
        public static int regTestSetup(int argc, IntPtr argv, out L_RegParams prp)
        {
            throw new NotImplementedException();
        }

        public static int regTestCleanup(this L_RegParams rp)
        {
            throw new NotImplementedException();
        }

        public static int regTestCompareValues(this L_RegParams rp, float val1, float val2, float delta)
        {
            throw new NotImplementedException();
        }

        public static int regTestCompareStrings(this L_RegParams rp, IntPtr string1, IntPtr bytes1, IntPtr string2, IntPtr bytes2)
        {
            throw new NotImplementedException();
        }

        public static int regTestComparePix(this L_RegParams rp, Pix pix1, Pix pix2)
        {
            throw new NotImplementedException();
        }

        public static int regTestCompareSimilarPix(this L_RegParams rp, Pix pix1, Pix pix2, int mindiff, float maxfract, int printstats)
        {
            throw new NotImplementedException();
        }

        public static int regTestCheckFile(this L_RegParams rp, string localname)
        {
            throw new NotImplementedException();
        }

        public static int regTestCompareFiles(this L_RegParams rp, int index1, int index2)
        {
            throw new NotImplementedException();
        }

        public static int regTestWritePixAndCheck(this L_RegParams rp, Pix pix, int format)
        {
            throw new NotImplementedException();
        }

        public static IntPtr regTestGenLocalFilename(this L_RegParams rp, int index, int format)
        {
            throw new NotImplementedException();
        }
    }
}
