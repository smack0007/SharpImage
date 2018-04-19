﻿using System.IO;

namespace ImageDotNet.Tga
{
    public static class TgaImage
    {
        public static Image Load(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            
            var header = br.ReadStruct<TgaHeader>();

            if (header.DataTypeCode != TgaDataType.UncompressedTrueColor && header.DataTypeCode != TgaDataType.RunLengthEncodedTrueColor)
                throw new ImageDotNetException($"Only {nameof(TgaDataType.UncompressedTrueColor)} and {nameof(TgaDataType.RunLengthEncodedTrueColor)} TGA images are supported.");
            
            byte bytesPerPixel = (byte)(header.BitsPerPixel / 8);

            if (bytesPerPixel != 3 && bytesPerPixel != 4)
                throw new ImageDotNetException("Only 24 and 32 bit TGA images are supported.");

            byte[] pixels = null;
            int dataLength = header.Width * header.Height * bytesPerPixel;

            if (header.DataTypeCode == TgaDataType.UncompressedTrueColor)
            {
                pixels = br.ReadBytes(dataLength);
            }
            else
            {
                pixels = new byte[dataLength];

                int readCount = 0;
                while (readCount < dataLength)
                {
                    byte nextByte = br.ReadByte();

                    if ((nextByte & 0x80) != 0) // Check high bit
                    {
                        nextByte -= 127; 
                        byte[] bytes = br.ReadBytes(bytesPerPixel);

                        for (int i = 0; i < nextByte; i++)
                        {
                            for (int j = 0; j < bytes.Length; j++)
                            {
                                pixels[readCount] = bytes[j];
                                readCount++;
                            }
                        }
                    }
                    else // Raw chunk
                    {
                        nextByte += 1;
                        byte[] bytes = br.ReadBytes(nextByte * bytesPerPixel);

                        for (int i = 0; i < bytes.Length; i++)
                        {
                            pixels[readCount] = bytes[i];
                            readCount++;
                        }
                    }
                }
            }

            // Half height because we have to flip the image.
            for (int y = 0; y < header.Height / 2; y++)
            {
                for (int x = 0; x < header.Width; x++)
                {
                    int top = ((y * header.Width) + x) * bytesPerPixel;
                    int bottom = (((header.Height - 1 - y) * header.Width) + x) * bytesPerPixel;

                    // Change BGR => RGB 
                    byte temp = pixels[top];
                    pixels[top] = pixels[top + 2];
                    pixels[top + 2] = temp;

                    // Change BGR => RGB 
                    temp = pixels[bottom];
                    pixels[bottom] = pixels[bottom + 2];
                    pixels[bottom + 2] = temp;

                    for (int i = 0; i < bytesPerPixel; i++)
                    {
                        temp = pixels[top + i];
                        pixels[top + i] = pixels[bottom + i];
                        pixels[bottom + i] = temp;
                    }
                }
            }

            if (bytesPerPixel == 3)
            {
                return new RgbImage(header.Width, header.Height, pixels);
            }
            else
            {
                return new RgbaImage(header.Width, header.Height, pixels);
            }
        }

        public static void Save(Image image, Stream stream, TgaDataType type = TgaDataType.UncompressedTrueColor)
        {
            BinaryWriter bw = new BinaryWriter(stream);

            byte[] header = new byte[18];
            header[2] = (byte)type;
            BinaryHelper.WriteLittleEndianUInt16(header, 12, (ushort)image.Width);
            BinaryHelper.WriteLittleEndianUInt16(header, 14, (ushort)image.Height);
            header[16] = (byte)(image.BytesPerPixel * 8);

            bw.Write(header);

            byte[] pixels = new byte[image.Width * image.Height * image.BytesPerPixel];

            for (int y = 0; y < image.Height / 2; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int top = ((y * image.Width) + x) * image.BytesPerPixel;
                    int bottom = (((image.Height - 1 - y) * image.Width) + x) * image.BytesPerPixel;

                    if (image.BytesPerPixel >= 3)
                    {
                        // We have to flip RGB to BGR and flip bottom and top
                        pixels[top] = image[bottom + 2];
                        pixels[top + 1] = image[bottom + 1];
                        pixels[top + 2] = image[bottom];

                        pixels[bottom] = image[top + 2];
                        pixels[bottom + 1] = image[top + 1];
                        pixels[bottom + 2] = image[top];

                        if (image.BytesPerPixel == 4)
                        {
                            pixels[top + 3] = image[bottom + 3];
                            pixels[bottom + 3] = image[top + 3];
                        }
                    }
                }
            }

            bw.Write(pixels);

            bw.Flush();
        }
    }
}