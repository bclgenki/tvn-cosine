﻿using System;
using System.Collections.Generic;

namespace Leptonica
{
    public static class Sarray1
    {
        // Create/Destroy/Copy
        public static Sarray sarrayCreate(int n)
        {
            throw new NotImplementedException();
        }

        public static Sarray sarrayCreateInitialized(int n, string initstr)
        {
            throw new NotImplementedException();
        }

        public static Sarray sarrayCreateWordsFromString(string str)
        {
            throw new NotImplementedException();
        }

        public static Sarray sarrayCreateLinesFromString(string str, int blankflag)
        {
            throw new NotImplementedException();
        }

        public static void sarrayDestroy(this Sarray psa)
        {
            throw new NotImplementedException();
        }

        public static Sarray sarrayCopy(this Sarray sa)
        {
            throw new NotImplementedException();
        }

        public static Sarray sarrayClone(this Sarray sa)
        {
            throw new NotImplementedException();
        }


        // Add/Remove string
        public static int sarrayAddString(this Sarray sa, string str, int copyflag)
        {
            throw new NotImplementedException();
        }

        public static IntPtr sarrayRemoveString(this Sarray sa, int index)
        {
            throw new NotImplementedException();
        }

        public static int sarrayReplaceString(this Sarray sa, int index, string newstr, int copyflag)
        {
            throw new NotImplementedException();
        }

        public static int sarrayClear(this Sarray sa)
        {
            throw new NotImplementedException();
        }


        // Accessors
        public static int sarrayGetCount(this Sarray sa)
        {
            throw new NotImplementedException();
        }

        public static IntPtr sarrayGetArray(this Sarray sa, out int pnalloc, out int pn)
        {
            throw new NotImplementedException();
        }

        public static IntPtr sarrayGetString(this Sarray sa, int index, int copyflag)
        {
            throw new NotImplementedException();
        }

        public static int sarrayGetRefcount(this Sarray sa)
        {
            throw new NotImplementedException();
        }

        public static int sarrayChangeRefcount(this Sarray sa, int delta)
        {
            throw new NotImplementedException();
        }


        // Conversion back to string
        public static IntPtr sarrayToString(this Sarray sa, int addnlflag)
        {
            throw new NotImplementedException();
        }

        public static IntPtr sarrayToStringRange(this Sarray sa, int first, int nstrings, int addnlflag)
        {
            throw new NotImplementedException();
        }


        // Join 2 sarrays
        public static int sarrayJoin(this Sarray sa1, Sarray sa2)
        {
            throw new NotImplementedException();
        }

        public static int sarrayAppendRange(this Sarray sa1, Sarray sa2, int start, int end)
        {
            throw new NotImplementedException();
        }


        // Pad an sarray to be the same size as another sarray
        public static int sarrayPadToSameSize(this Sarray sa1, Sarray sa2, string padstring)
        {
            throw new NotImplementedException();
        }


        // Convert word sarray to(formatted) line sarray
        public static Sarray sarrayConvertWordsToLines(this Sarray sa, int linesize)
        {
            throw new NotImplementedException();
        }


        // Split string on separator list
        public static int sarraySplitString(this Sarray sa, string str, string separators)
        {
            throw new NotImplementedException();
        }


        // Filter sarray
        public static Sarray sarraySelectBySubstring(this Sarray sain, string substr)
        {
            throw new NotImplementedException();
        }

        public static Sarray sarraySelectByRange(this Sarray sain, int first, int last)
        {
            throw new NotImplementedException();
        }

        public static int sarrayParseRange(this Sarray sa, int start, out int pactualstart, out int pend, out int pnewstart, string substr, int loc)
        {
            throw new NotImplementedException();
        }


        // Serialize for I/O
        public static Sarray sarrayRead(string filename)
        {
            throw new NotImplementedException();
        }

        public static Sarray sarrayReadStream(IntPtr fp)
        {
            throw new NotImplementedException();
        }

        public static Sarray sarrayReadMem(IntPtr data, IntPtr size)
        {
            throw new NotImplementedException();
        }

        public static int sarrayWrite(string filename, Sarray sa)
        {
            throw new NotImplementedException();
        }

        public static int sarrayWriteStream(IntPtr fp, Sarray sa)
        {
            throw new NotImplementedException();
        }

        public static int sarrayWriteMem(out IntPtr pdata, IntPtr psize, Sarray sa)
        {
            throw new NotImplementedException();
        }

        public static int sarrayAppend(string filename, Sarray sa)
        {
            throw new NotImplementedException();
        }


        // Directory filenames
        public static Sarray getNumberedPathnamesInDirectory(string dirname, string substr, int numpre, int numpost, int maxnum)
        {
            throw new NotImplementedException();
        }

        public static Sarray getSortedPathnamesInDirectory(string dirname, string substr, int first, int nfiles)
        {
            throw new NotImplementedException();
        }

        public static Sarray convertSortedToNumberedPathnames(this Sarray sa, int numpre, int numpost, int maxnum)
        {
            throw new NotImplementedException();
        }

        public static Sarray getFilenamesInDirectory(string dirname)
        {
            throw new NotImplementedException();
        } 
    }
}
