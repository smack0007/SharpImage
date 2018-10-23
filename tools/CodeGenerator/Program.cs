﻿using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public static partial class Program
    {
        private const string tab = "    ";

        private const int MaxTestImageDataSize = 5;

        public static void Main(string[] args)
        {
            var libraryPath = Path.Combine(args[0], "src", "ImageDotNet");
            var testsPath = Path.Combine(args[0], "tests", "ImageDotNet.Tests");

            var pixelsPath = Path.Combine(libraryPath, "Pixels");

            var pixels = Directory.EnumerateFiles(pixelsPath, "*.cs")                
                .Select(x => Path.GetFileNameWithoutExtension(x))
                .ToArray();

            WritePixelHelper(libraryPath, pixels);

            WriteTestData(testsPath, pixels);

            WriteFileFormatTests(testsPath, new string[] { "Png", "Tga" }, pixels);
        }

        private static void WritePixelHelper(string libraryPath, string[] pixels)
        {
            var convert = BuildPixelHelperConvertMethod(pixels);

            File.WriteAllText(Path.Combine(libraryPath, $"PixelHelper.g.cs"),
$@"/// <auto-generated />
using System;

namespace ImageDotNet
{{
    public static partial class PixelHelper
    {{
{convert.ToString().TrimEnd()}        
    }}
}}");
        }

        private static StringBuilder BuildPixelHelperConvertMethod(string[] pixels)
        {
            var convert = new StringBuilder(1024);
            convert.AppendLine($"{tab}{tab}private static unsafe void Convert<T, U>(byte* sourcePtr, byte* destinationPtr, int length)");
            convert.AppendLine($"{tab}{tab}{tab}where T : unmanaged, IPixel");
            convert.AppendLine($"{tab}{tab}{tab}where U : unmanaged, IPixel");
            convert.AppendLine($"{tab}{tab}{{");

            bool first = true;
            foreach (var tPixel in pixels)
            {
                var @if = first ? "if" : "else if";
                first = false;

                convert.AppendLine($"{tab}{tab}{tab}{@if} (typeof(T) == typeof({tPixel}))");
                convert.AppendLine($"{tab}{tab}{tab}{{");

                bool first2 = true;
                foreach (var uPixel in pixels.Where(x => x != tPixel))
                {
                    var @if2 = first2 ? "if" : "else if";
                    first2 = false;

                    convert.AppendLine($"{tab}{tab}{tab}{tab}{@if2} (typeof(U) == typeof({uPixel}))");
                    convert.AppendLine($"{tab}{tab}{tab}{tab}{{");
                    convert.AppendLine($"{tab}{tab}{tab}{tab}{tab}Convert{tPixel}To{uPixel}(sourcePtr, destinationPtr, length);");
                    convert.AppendLine($"{tab}{tab}{tab}{tab}}}");
                }

                convert.AppendLine($"{tab}{tab}{tab}}}");
            }

            convert.AppendLine($"{tab}{tab}}}");

            return convert;
        }

        private static void WriteTestData(string testsPath, string[] pixels)
        {
            var testImageData = BuildTestImageData(pixels);

            File.WriteAllText(Path.Combine(testsPath, $"TestData.g.cs"),
$@"/// <auto-generated />
using System;

namespace ImageDotNet.Tests
{{
    public static partial class TestData
    {{
{testImageData.ToString().TrimEnd()}
    }}
}}");
        }

        private static StringBuilder BuildTestImageData(string[] pixels)
        {
            var output = new StringBuilder(1024);
            
            foreach (var pixel in pixels)
            {
                output.AppendLine($"{tab}{tab}public static class {pixel}Images");
                output.AppendLine($"{tab}{tab}{{");

                var bytesPerPixel = GetByteCount(pixel);

                for (int i = 1; i <= MaxTestImageDataSize; i++)
                {
                    output.AppendLine($"{tab}{tab}{tab}public static readonly {pixel}[] Image{i}x{i} = new {pixel}[]");
                    output.AppendLine($"{tab}{tab}{tab}{{");

                    int byteCount = 0;
                    for (int y = 0; y < i; y++)
                    {
                        output.Append($"{tab}{tab}{tab}{tab}");

                        for (int x = 0; x < i; x++)
                        {
                            output.Append($"new {pixel}(");

                            for (int j = 0; j < bytesPerPixel; j++)
                            {
                                byteCount++;
                                output.Append(byteCount);

                                if (j != bytesPerPixel - 1)
                                    output.Append(", ");
                            }

                            output.Append("),");
                        }

                        output.AppendLine();
                    }

                    output.AppendLine($"{tab}{tab}{tab}}};");
                    output.AppendLine();
                }

                output.AppendLine($"{tab}{tab}}}");
                output.AppendLine();
            }

            return output;
        }

        private static int GetByteCount(string pixel)
        {
            if (pixel.EndsWith("24"))
            {
                return 3;
            }
            else if (pixel.EndsWith("32"))
            {
                return 4;
            }

            throw new NotImplementedException();
        }

        private static void WriteFileFormatTests(string testsPath, string[] fileFormats, string[] pixels)
        {
            foreach (var fileFormat in fileFormats)
            {
                WriteFileFormatTest(testsPath, fileFormat, pixels);
            }
        }

        private static void WriteFileFormatTest(string testsPath, string fileFormat, string[] pixels)
        {
            var saveAndLoadTests = BuildSaveAndLoadTests(fileFormat, pixels);

            File.WriteAllText(Path.Combine(testsPath, $"{fileFormat}Tests.g.cs"),
$@"/// <auto-generated />
using Xunit;

namespace ImageDotNet.Tests
{{
    public partial class {fileFormat}Tests
    {{
{saveAndLoadTests.ToString().TrimEnd()}
    }}
}}");
        }

        private static object BuildSaveAndLoadTests(string fileFormat, string[] pixels)
        {
            var output = new StringBuilder(1024);

            foreach (var pixel in pixels)
            {
                for (int i = 1; i <= MaxTestImageDataSize; i++)
                {

                    output.AppendLine($"{tab}{tab}[Fact]");
                    output.AppendLine($"{tab}{tab}public void SaveAndLoad_{pixel}_{i}x{i}()");
                    output.AppendLine($"{tab}{tab}{{");
                    output.AppendLine($"{tab}{tab}{tab}AssertSaveAndLoad(new Image<{pixel}>({i}, {i}, TestData.{pixel}Images.Image{i}x{i}));");
                    output.AppendLine($"{tab}{tab}}}");
                    output.AppendLine();
                }
            }   

            return output;
        }
    }
}





