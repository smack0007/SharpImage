/// <auto-generated />
using System.IO;
using Xunit;

namespace ImageDotNet.Tests
{
    public partial class PngTests
    {

        private static void AssertSaveAndLoad(Image<Bgr24> expected)
        {
            Image<Bgr24> actual = null;

            using (var memory = new MemoryStream())
            {
                expected.SavePng(memory);
                memory.Position = 0;
                actual = Image.LoadPng(memory).ToBgr24();
            }

            AssertEx.Equal(expected, actual);
        }

        [Fact]
        public void SaveAndLoad_Bgr24_1x1()
        {
            AssertSaveAndLoad(new Image<Bgr24>(1, 1, TestData.Bgr24Images.Image1x1));
        }

        [Fact]
        public void SaveAndLoad_Bgr24_2x2()
        {
            AssertSaveAndLoad(new Image<Bgr24>(2, 2, TestData.Bgr24Images.Image2x2));
        }

        [Fact]
        public void SaveAndLoad_Bgr24_3x3()
        {
            AssertSaveAndLoad(new Image<Bgr24>(3, 3, TestData.Bgr24Images.Image3x3));
        }

        [Fact]
        public void SaveAndLoad_Bgr24_4x4()
        {
            AssertSaveAndLoad(new Image<Bgr24>(4, 4, TestData.Bgr24Images.Image4x4));
        }

        [Fact]
        public void SaveAndLoad_Bgr24_5x5()
        {
            AssertSaveAndLoad(new Image<Bgr24>(5, 5, TestData.Bgr24Images.Image5x5));
        }


        private static void AssertSaveAndLoad(Image<Bgra32> expected)
        {
            Image<Bgra32> actual = null;

            using (var memory = new MemoryStream())
            {
                expected.SavePng(memory);
                memory.Position = 0;
                actual = Image.LoadPng(memory).ToBgra32();
            }

            AssertEx.Equal(expected, actual);
        }

        [Fact]
        public void SaveAndLoad_Bgra32_1x1()
        {
            AssertSaveAndLoad(new Image<Bgra32>(1, 1, TestData.Bgra32Images.Image1x1));
        }

        [Fact]
        public void SaveAndLoad_Bgra32_2x2()
        {
            AssertSaveAndLoad(new Image<Bgra32>(2, 2, TestData.Bgra32Images.Image2x2));
        }

        [Fact]
        public void SaveAndLoad_Bgra32_3x3()
        {
            AssertSaveAndLoad(new Image<Bgra32>(3, 3, TestData.Bgra32Images.Image3x3));
        }

        [Fact]
        public void SaveAndLoad_Bgra32_4x4()
        {
            AssertSaveAndLoad(new Image<Bgra32>(4, 4, TestData.Bgra32Images.Image4x4));
        }

        [Fact]
        public void SaveAndLoad_Bgra32_5x5()
        {
            AssertSaveAndLoad(new Image<Bgra32>(5, 5, TestData.Bgra32Images.Image5x5));
        }


        private static void AssertSaveAndLoad(Image<Gray8> expected)
        {
            Image<Gray8> actual = null;

            using (var memory = new MemoryStream())
            {
                expected.SavePng(memory);
                memory.Position = 0;
                actual = Image.LoadPng(memory).ToGray8();
            }

            AssertEx.Equal(expected, actual);
        }

        [Fact]
        public void SaveAndLoad_Gray8_1x1()
        {
            AssertSaveAndLoad(new Image<Gray8>(1, 1, TestData.Gray8Images.Image1x1));
        }

        [Fact]
        public void SaveAndLoad_Gray8_2x2()
        {
            AssertSaveAndLoad(new Image<Gray8>(2, 2, TestData.Gray8Images.Image2x2));
        }

        [Fact]
        public void SaveAndLoad_Gray8_3x3()
        {
            AssertSaveAndLoad(new Image<Gray8>(3, 3, TestData.Gray8Images.Image3x3));
        }

        [Fact]
        public void SaveAndLoad_Gray8_4x4()
        {
            AssertSaveAndLoad(new Image<Gray8>(4, 4, TestData.Gray8Images.Image4x4));
        }

        [Fact]
        public void SaveAndLoad_Gray8_5x5()
        {
            AssertSaveAndLoad(new Image<Gray8>(5, 5, TestData.Gray8Images.Image5x5));
        }


        private static void AssertSaveAndLoad(Image<Rgb24> expected)
        {
            Image<Rgb24> actual = null;

            using (var memory = new MemoryStream())
            {
                expected.SavePng(memory);
                memory.Position = 0;
                actual = Image.LoadPng(memory).ToRgb24();
            }

            AssertEx.Equal(expected, actual);
        }

        [Fact]
        public void SaveAndLoad_Rgb24_1x1()
        {
            AssertSaveAndLoad(new Image<Rgb24>(1, 1, TestData.Rgb24Images.Image1x1));
        }

        [Fact]
        public void SaveAndLoad_Rgb24_2x2()
        {
            AssertSaveAndLoad(new Image<Rgb24>(2, 2, TestData.Rgb24Images.Image2x2));
        }

        [Fact]
        public void SaveAndLoad_Rgb24_3x3()
        {
            AssertSaveAndLoad(new Image<Rgb24>(3, 3, TestData.Rgb24Images.Image3x3));
        }

        [Fact]
        public void SaveAndLoad_Rgb24_4x4()
        {
            AssertSaveAndLoad(new Image<Rgb24>(4, 4, TestData.Rgb24Images.Image4x4));
        }

        [Fact]
        public void SaveAndLoad_Rgb24_5x5()
        {
            AssertSaveAndLoad(new Image<Rgb24>(5, 5, TestData.Rgb24Images.Image5x5));
        }


        private static void AssertSaveAndLoad(Image<Rgba32> expected)
        {
            Image<Rgba32> actual = null;

            using (var memory = new MemoryStream())
            {
                expected.SavePng(memory);
                memory.Position = 0;
                actual = Image.LoadPng(memory).ToRgba32();
            }

            AssertEx.Equal(expected, actual);
        }

        [Fact]
        public void SaveAndLoad_Rgba32_1x1()
        {
            AssertSaveAndLoad(new Image<Rgba32>(1, 1, TestData.Rgba32Images.Image1x1));
        }

        [Fact]
        public void SaveAndLoad_Rgba32_2x2()
        {
            AssertSaveAndLoad(new Image<Rgba32>(2, 2, TestData.Rgba32Images.Image2x2));
        }

        [Fact]
        public void SaveAndLoad_Rgba32_3x3()
        {
            AssertSaveAndLoad(new Image<Rgba32>(3, 3, TestData.Rgba32Images.Image3x3));
        }

        [Fact]
        public void SaveAndLoad_Rgba32_4x4()
        {
            AssertSaveAndLoad(new Image<Rgba32>(4, 4, TestData.Rgba32Images.Image4x4));
        }

        [Fact]
        public void SaveAndLoad_Rgba32_5x5()
        {
            AssertSaveAndLoad(new Image<Rgba32>(5, 5, TestData.Rgba32Images.Image5x5));
        }
    }
}