﻿using System;

namespace Leptonica
{
    /// <summary>
    /// Top level approximate separable grayscale or color bilateral filtering
    /// </summary>
    public static class Bilateral
    {
        /// <summary>
        ///       (1) This performs a relatively fast, separable bilateral
        ///           filtering operation.The time is proportional to ncomps
        ///  and varies inversely approximately as the cube of the
        ///           reduction factor.  See bilateral.h for algorithm details.
        ///       (2) We impose minimum values for range_stdev and ncomps to
        ///           avoid nasty artifacts when either are too small.We also
        ///  impose a constraint on their product:
        ///                ncomps* range_stdev >= 100.
        ///           So for values of range_stdev >= 25, ncomps can be as small as 4.
        ///           Here is a qualitative, intuitive explanation for this constraint.
        ///  Call the difference in k values between the J(k) == 'delta', where
        ///              'delta' ~ 200 / ncomps
        /// Then this constraint is roughly equivalent to the condition:
        ///               'delta' smaller than 2 * range_stdev
        ///  Note that at an intensity difference of(2 * range_stdev), the
        /// range part of the kernel reduces the effect by the factor 0.14.
        ///           This constraint requires that we have a sufficient number of
        ///           PCBs(i.e, a small enough 'delta'), so that for any value of
        /// image intensity I, there exists a k(and a PCB, J(k), such that
        ///               |I - k|  smaller than range_stdev
        ///  Any fewer PCBs and we don't have enough to support this condition.
        ///       (3) The upper limit of 30 on ncomps is imposed because the
        ///  gain in accuracy is not worth the extra computation.
        /// 
        ///      (4) The size of the gaussian kernel is twice the spatial_stdev
        /// on each side of the origin.The minimum value of
        ///           spatial_stdev, 0.5, is required to have a finite sized
        ///           spatial kernel.In practice, a much larger value is used.
        ///       (5) Computation of the intermediate images goes inversely
        ///           as the cube of the reduction factor.If you can use a
        ///           reduction of 2 or 4, it is well-advised.
        ///       (6) The range kernel is defined over the absolute value of pixel
        ///  grayscale differences, and hence must have size 256 x 1.
        ///           Values in the array represent the multiplying weight
        ///           depending on the absolute gray value difference between
        ///           the source pixel and the neighboring pixel, and should
        ///           be monotonically decreasing.
        ///       (7) Interesting observation.Run this on prog/fish24.jpg, with
        ///  range_stdev = 60, ncomps = 6, and spatial_dev = { 10, 30, 50}.
        ///           As spatial_dev gets larger, we get the counter-intuitive
        ///  result that the body of the red fish becomes less blurry.
        /// </summary>
        /// <param name="pixs">pixs 8 bpp gray or 32 bpp rgb, no colormap</param>
        /// <param name="spatial_stdev">spatial_stdev  of gaussian kernel; in pixels, > 0.5</param>
        /// <param name="range_stdev">range_stdev  of gaussian range kernel; > 5.0; typ. 50.0</param>
        /// <param name="ncomps">ncomps number of intermediate sums J(k,x); in [4 ... 30]</param>
        /// <param name="reduction">reduction  1, 2 or 4</param>
        /// <returns>pixd bilateral filtered image, or NULL on error</returns>
        public static Pix pixBilateral(Pix pixs, float spatial_stdev, float range_stdev, int ncomps, int reduction)
        {
            if (pixs == null)
            {
                return null;
            }

            return (Pix)Native.DllImports.pixBilateral(pixs.handleRef, spatial_stdev, range_stdev, ncomps, reduction);
        }

        /// <summary>
        /// (1) See pixBilateral() for constraints on the input parameters.
        /// (2) See pixBilateral() for algorithm details.
        /// </summary>
        /// <param name="pixs">pixs 8 bpp gray</param>
        /// <param name="spatial_stdev">spatial_stdev  of gaussian kernel; in pixels, > 0.5</param>
        /// <param name="range_stdev">range_stdev  of gaussian range kernel; > 5.0; typ. 50.0</param>
        /// <param name="ncomps">ncomps number of intermediate sums J(k,x); in [4 ... 30]</param>
        /// <param name="reduction">reduction  1, 2 or 4</param>
        /// <returns>pixd 8 bpp bilateral filtered image, or NULL on error</returns>
        public static Pix pixBilateralGray(Pix pixs, float spatial_stdev, float range_stdev, int ncomps, int reduction)
        {
            if (pixs == null)
            {
                return null;
            }

            return (Pix)Native.DllImports.pixBilateralGray(pixs.handleRef, spatial_stdev, range_stdev, ncomps, reduction);
        }

        /// <summary>
        ///       (1) The spatial_kel is a conventional smoothing kernel, typically a
        ///           2-d Gaussian kernel or other block kernel.It can be either
        ///           normalized or not, but must be everywhere positive.
        ///       (2) The range_kel is defined over the absolute value of pixel
        ///  grayscale differences, and hence must have size 256 x 1.
        ///           Values in the array represent the multiplying weight for each
        ///  gray value difference between the target pixel and center of the
        ///           kernel, and should be monotonically decreasing.
        ///       (3) If range_kel == NULL, a constant weight is applied regardless
        ///           of the range value difference.This degenerates to a regular
        /// pixConvolve() with a normalized kernel.
        /// </summary>
        /// <param name="pixs">pixs 8 bpp gray or 32 bpp rgb</param>
        /// <param name="spatial_kel">spatial_kel  gaussian kernel</param>
        /// <param name="range_kel">range_kel [optional] 256 x 1, monotonically decreasing</param>
        /// <returns>pixd 8 bpp bilateral filtered image</returns>
        public static Pix pixBilateralExact(Pix pixs, L_Kernel spatial_kel, L_Kernel range_kel)
        {
            if (pixs == null
             || spatial_kel == null
             || range_kel == null)
            {
                return null;
            }

            return (Pix)Native.DllImports.pixBilateralExact(pixs.handleRef, spatial_kel.handleRef, range_kel.handleRef);
        }

        /// <summary>
        ///  (1) See pixBilateralExact().
        /// </summary>
        /// <param name="pixs">pixs 8 bpp gray</param>
        /// <param name="spatial_kel">spatial_kel  gaussian kernel</param>
        /// <param name="range_kel">range_kel [optional] 256 x 1, monotonically decreasing</param>
        /// <returns>pixd 8 bpp bilateral filtered image</returns>
        public static Pix pixBilateralGrayExact(Pix pixs, L_Kernel spatial_kel, L_Kernel range_kel)
        {
            if (pixs == null
             || spatial_kel == null
             || range_kel == null)
            {
                return null;
            }

            return (Pix)Native.DllImports.pixBilateralGrayExact(pixs.handleRef, spatial_kel.handleRef, range_kel.handleRef);
        }

        /// <summary>
        ///       (1) See pixBilateralExact().  This provides an interface using
        ///  the standard deviations of the spatial and range filters.
        ///       (2) The convolution window halfwidth is 2 * spatial_stdev,
        ///  and the square filter size is 4 * spatial_stdev + 1.
        ///  The kernel captures 95% of total energy.This is compensated
        /// by normalization.
        /// 
        ///      (3) The range_stdev is analogous to spatial_halfwidth in the
        /// grayscale domain[0...255], and determines how much damping of the
        /// smoothing operation is applied across edges.The larger this
        ///           value is, the smaller the damping.  The smaller the value, the
        /// more edge details are preserved.  These approximations are useful
        ///           for deciding the appropriate cutoff.
        ///               kernel[1 * stdev] ~= 0.6  * kernel[0]
        ///               kernel[2 * stdev] ~= 0.14 * kernel[0]
        ///               kernel[3 * stdev] ~= 0.01 * kernel[0]
        ///           If range_stdev is infinite there is no damping, and this
        ///           becomes a conventional gaussian smoothing.
        /// 
        /// This value does not affect the run time.
        /// 
        ///      (4) If range_stdev is negative or zero, the range kernel is
        ///           ignored and this degenerates to a straight gaussian convolution.
        ///       (5) This is very slow for large spatial filters.  The time
        ///           on a 3GHz pentium is roughly
        ///              T = 1.2 * 10^-8 * (A * sh^2)  sec
        ///           where A = # of pixels, sh = spatial halfwidth of filter.
        /// </summary>
        /// <param name="pixs">pixs 8 bpp gray or 32 bpp rgb</param>
        /// <param name="spatial_stdev">spatial_stdev > 0.0</param>
        /// <param name="range_stdev">range_stdev > 0.0</param>
        /// <returns>pixd 8 bpp or 32 bpp bilateral filtered image</returns>
        public static Pix pixBlockBilateralExact(Pix pixs, float spatial_stdev, float range_stdev)
        {
            if (pixs == null)
            {
                return null;
            }

            return (Pix)Native.DllImports.pixBlockBilateralExact(pixs.handleRef, spatial_stdev, range_stdev);
        }

        /// <summary>
        ///       (1) Creates a one-sided Gaussian kernel with the given
        ///           standard deviation.At grayscale difference of one stdev,
        ///  the kernel falls to 0.6, and to 0.01 at three stdev.
        ///       (2) A typical input number might be 20.  Then pixels whose
        ///  value differs by 60 from the center pixel have their
        ///           weight in the convolution reduced by a factor of about 0.01.
        /// </summary>
        /// <param name="range_stdev">range_stdev > 0</param>
        /// <returns>kel, or NULL on error</returns>
        public static L_Kernel makeRangeKernel(float range_stdev)
        {
            if (range_stdev <= 0)
                throw new ArgumentOutOfRangeException("range_stdev must be > 0.");

            return (L_Kernel)Native.DllImports.makeRangeKernel(range_stdev);
        }
    }
}
