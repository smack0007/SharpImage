/// <auto-generated />
using System;

namespace ImageDotNet
{
    public partial interface IImage
    {
        bool IsBgr24();

        Image<Bgr24> ToBgr24();

        bool IsBgra32();

        Image<Bgra32> ToBgra32();

        bool IsGray8();

        Image<Gray8> ToGray8();

        bool IsRgb24();

        Image<Rgb24> ToRgb24();

        bool IsRgba32();

        Image<Rgba32> ToRgba32();        
    }
}