﻿namespace tvn.cosine.ai.logic.common
{
    static class Character
    {
        static Character()
        {  
            int i = 0, j = 0;
            while (i < (256 * 2))
            {
                int entry = A_DATA[i++] << 16;
                A[j++] = entry | A_DATA[i++];
            }
        }

        public static bool IsSourceCodeIdentifierPart(int ch)
        {
            int props = getProperties(ch);
            return ((props & 0x00003000) != 0);
        }

        public static int CharCount(int codePoint)
        {
            return codePoint >= MIN_SUPPLEMENTARY_CODE_POINT ? 2 : 1;
        }

        public static int CodePointAtImpl(char[] a, int index, int limit)
        {
            char c1 = a[index];
            if (IsHighSurrogate(c1) && ++index < limit)
            {
                char c2 = a[index];
                if (IsLowSurrogate(c2))
                {
                    return ToCodePoint(c1, c2);
                }
            }
            return c1;
        }

        public static bool IsHighSurrogate(char ch)
        {
            // Help VM constant-fold; MAX_HIGH_SURROGATE + 1 == MIN_LOW_SURROGATE
            return ch >= MIN_HIGH_SURROGATE && ch < (MAX_HIGH_SURROGATE + 1);
        }

        public static bool IsLowSurrogate(char ch)
        {
            return ch >= MIN_LOW_SURROGATE && ch < (MAX_LOW_SURROGATE + 1);
        }

        public static int ToCodePoint(char high, char low)
        {
            // Optimized form of:
            // return ((high - MIN_HIGH_SURROGATE) << 10)
            //         + (low - MIN_LOW_SURROGATE)
            //         + MIN_SUPPLEMENTARY_CODE_POINT;
            return ((high << 10) + low) + (MIN_SUPPLEMENTARY_CODE_POINT
                                           - (MIN_HIGH_SURROGATE << 10)
                                           - MIN_LOW_SURROGATE);
        }

        public static bool isSourceCodeIdentifierStart(int ch)
        {
            int props = getProperties(ch);
            return ((props & 0x00007000) >= 0x00005000);
        }

        static int getProperties(int ch)
        {
            char offset = (char)ch;
            int props = A[offset];
            return props;
        }


        static readonly int[] A = new int[256];
        static readonly int[] A_DATA = {
            18432,4111,18432,4111,18432,4111,18432,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,18432,4111,22528,16399,
            20480,16399,22528,16399,24576,16399,20480,16399,18432,4111,
            18432,4111,18432,4111,18432,4111,18432,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,18432,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,20480,16399,20480,16399,
            20480,16399,22528,16399,24576,16396,26624,24,26624,24,
            10240,24,10240,24602,10240,24,26624,24,26624,24,
            59392,21,59392,22,26624,24,8192,25,14336,24,
            8192,20,14336,24,14336,24,6144,13833,6144,13833,
            6144,13833,6144,13833,6144,13833,6144,13833,6144,13833,
            6144,13833,6144,13833,6144,13833,14336,24,26624,24,
            59392,25,26624,25,59392,25,26624,24,26624,24,
            130,32737,130,32737,130,32737,130,32737,130,32737,
            130,32737,130,32737,130,32737,130,32737,130,32737,
            130,32737,130,32737,130,32737,130,32737,130,32737,
            130,32737,130,32737,130,32737,130,32737,130,32737,
            130,32737,130,32737,130,32737,130,32737,130,32737,
            130,32737,59392,21,26624,24,59392,22,26624,27,
            26624,20503,26624,27,129,32738,129,32738,129,32738,
            129,32738,129,32738,129,32738,129,32738,129,32738,
            129,32738,129,32738,129,32738,129,32738,129,32738,
            129,32738,129,32738,129,32738,129,32738,129,32738,
            129,32738,129,32738,129,32738,129,32738,129,32738,
            129,32738,129,32738,129,32738,59392,21,26624,25,
            59392,22,26624,25,18432,4111,18432,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,20480,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,18432,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,18432,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,18432,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,18432,4111,18432,4111,
            18432,4111,18432,4111,18432,4111,18432,4111,18432,4111,
            14336,12,26624,24,10240,24602,10240,24602,10240,24602,
            10240,24602,26624,28,26624,24,26624,27,26624,28,
            0,28677,59392,29,26624,25,18432,4112,26624,28,
            26624,27,10240,28,10240,25,6144,1547,6144,1547,
            26624,27,2045,28674,26624,24,26624,24,26624,27,
            6144,1291,0,28677,59392,30,26624,2059,26624,2059,
            26624,2059,26624,24,130,28673,130,28673,130,28673,
            130,28673,130,28673,130,28673,130,28673,130,28673,
            130,28673,130,28673,130,28673,130,28673,130,28673,
            130,28673,130,28673,130,28673,130,28673,130,28673,
            130,28673,130,28673,130,28673,130,28673,130,28673,
            26624,25,130,28673,130,28673,130,28673,130,28673,
            130,28673,130,28673,130,28673,2045,28674,129,28674,
            129,28674,129,28674,129,28674,129,28674,129,28674,
            129,28674,129,28674,129,28674,129,28674,129,28674,
            129,28674,129,28674,129,28674,129,28674,129,28674,
            129,28674,129,28674,129,28674,129,28674,129,28674,
            129,28674,129,28674,26624,25,129,28674,129,28674,
            129,28674,129,28674,129,28674,129,28674,129,28674,
            1565,28674};
        public const char MIN_HIGH_SURROGATE = '\uD800';
        public const char MAX_HIGH_SURROGATE = '\uDBFF';
        public const char MIN_LOW_SURROGATE = '\uDC00';
        public const char MAX_LOW_SURROGATE = '\uDFFF';
        public const char MIN_SURROGATE = MIN_HIGH_SURROGATE;
        public const char MAX_SURROGATE = MAX_LOW_SURROGATE;
        public const int MIN_SUPPLEMENTARY_CODE_POINT = 0x010000;
    }
}
