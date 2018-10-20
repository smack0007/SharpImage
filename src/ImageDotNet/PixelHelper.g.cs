/// <auto-generated />
using System;

namespace ImageDotNet
{
    public static partial class PixelHelper
    {
        private static unsafe void Convert<T, U>(byte* sourcePtr, byte* destinationPtr, int length)
            where T : unmanaged, IPixel
            where U : unmanaged, IPixel
        {
            if (typeof(T) == typeof(Bgr24))
            {
                if (typeof(U) == typeof(Bgra32))
                {
                    ConvertBgr24ToBgra32(sourcePtr, destinationPtr, length);
                }
                else if (typeof(U) == typeof(Rgb24))
                {
                    ConvertBgr24ToRgb24(sourcePtr, destinationPtr, length);
                }
                else if (typeof(U) == typeof(Rgba32))
                {
                    ConvertBgr24ToRgba32(sourcePtr, destinationPtr, length);
                }
            }
            else if (typeof(T) == typeof(Bgra32))
            {
                if (typeof(U) == typeof(Bgr24))
                {
                    ConvertBgra32ToBgr24(sourcePtr, destinationPtr, length);
                }
                else if (typeof(U) == typeof(Rgb24))
                {
                    ConvertBgra32ToRgb24(sourcePtr, destinationPtr, length);
                }
                else if (typeof(U) == typeof(Rgba32))
                {
                    ConvertBgra32ToRgba32(sourcePtr, destinationPtr, length);
                }
            }
            else if (typeof(T) == typeof(Rgb24))
            {
                if (typeof(U) == typeof(Bgr24))
                {
                    ConvertRgb24ToBgr24(sourcePtr, destinationPtr, length);
                }
                else if (typeof(U) == typeof(Bgra32))
                {
                    ConvertRgb24ToBgra32(sourcePtr, destinationPtr, length);
                }
                else if (typeof(U) == typeof(Rgba32))
                {
                    ConvertRgb24ToRgba32(sourcePtr, destinationPtr, length);
                }
            }
            else if (typeof(T) == typeof(Rgba32))
            {
                if (typeof(U) == typeof(Bgr24))
                {
                    ConvertRgba32ToBgr24(sourcePtr, destinationPtr, length);
                }
                else if (typeof(U) == typeof(Bgra32))
                {
                    ConvertRgba32ToBgra32(sourcePtr, destinationPtr, length);
                }
                else if (typeof(U) == typeof(Rgb24))
                {
                    ConvertRgba32ToRgb24(sourcePtr, destinationPtr, length);
                }
            }
        }        
    }
}