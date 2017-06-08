﻿using System.Runtime.InteropServices;

namespace Leptonica
{
    public static class BinReduce
    {
        /// <summary>
        /// Notes:
        ///      (1) After folding, the data is in bytes 0 and 2 of the word,
        ///          and the bits in each byte are in the following order
        ///          (with 0 being the leftmost originating pair and 7 being
        /// the rightmost originating pair):
        ///               0 4 1 5 2 6 3 7
        ///          These need to be permuted to
        ///               0 1 2 3 4 5 6 7
        ///          which is done with an 8-bit table generated by makeSubsampleTab2x().
        /// </pre>
        /// </summary>
        /// <param name="pixs">pixs</param>
        /// <param name="intab">intab [optional]; if null, a table is made here and destroyed before exit</param>
        /// <returns></returns>
        public static Pix pixReduceBinary2(this Pix pixs, byte[] intab)
        {
            if (null == pixs)
            {
                return null;
            }

            return (Pix)Native.DllImports.pixReduceBinary2((HandleRef)pixs, intab);
        }

        /// <summary>
        /// <pre>
        /// Notes:
        ///      (1) This performs up to four cascaded 2x rank reductions.
        ///      (2) Use level = 0 to truncate the cascade.
        /// </pre>
        /// </summary>
        /// <param name="pixs">pixs 1 bpp</param>
        /// <param name="level1">level1 threshold, in the set {0, 1, 2, 3, 4}</param>
        /// <param name="level2">level2 threshold, in the set {0, 1, 2, 3, 4}</param>
        /// <param name="level3">level3 threshold, in the set {0, 1, 2, 3, 4}</param>
        /// <param name="level4">level4 threshold, in the set {0, 1, 2, 3, 4}</param>
        /// <returns></returns>
        public static Pix pixReduceRankBinaryCascade(this Pix pixs, int level1, int level2, int level3, int level4)
        {
            if (null == pixs)
            {
                return null;
            }

            return (Pix)Native.DllImports.pixReduceRankBinaryCascade((HandleRef)pixs, level1, level2, level3, level4);
        }

        /// <summary>
        /// <pre>
        /// Notes:
        ///      (1) pixd is downscaled by 2x from pixs.
        ///      (2) The rank threshold specifies the minimum number of ON
        /// pixels in each 2x2 region of pixs that are required to
        ///          set the corresponding pixel ON in pixd.
        ///      (3) Rank filtering is done to the UL corner of each 2x2 pixel block,
        ///          using only logical operations.Then these pixels are chosen
        ///         in the 2x subsampling process, subsampled, as described
        /// above in pixReduceBinary2().
        /// </pre>
        /// </summary>
        /// <param name="pixs">pixs 1 bpp</param>
        /// <param name="level">level rank threshold: 1, 2, 3, 4</param>
        /// <param name="intab">intab [optional]; if null, a table is made here and destroyed before exit</param>
        /// <returns>pixd 1 bpp, 2x rank threshold reduced, or NULL on error</returns>
        public static Pix pixReduceRankBinary2(this Pix pixs, int level, byte[] intab)
        {
            if (null == pixs)
            {
                return null;
            }

            return (Pix)Native.DllImports.pixReduceRankBinary2((HandleRef)pixs, level, intab);
        } 
    }
}
