﻿using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public static partial class Program
    {
        private const string tab = "    ";

        public static void Main(string[] args)
        {
            string libraryPath = args[0];

            var pixelsPath = Path.Combine(libraryPath, "Pixels");

            var pixels = Directory.EnumerateFiles(pixelsPath, "*.cs")                
                .Select(x => Path.GetFileNameWithoutExtension(x))
                .ToArray();

            WritePixelHelper(libraryPath, pixels);
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
    }
}





