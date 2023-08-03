using System;

class Program
{
    static int Utf8ToUtf16(byte[] utf8String, int in_len, ushort[] utf16String, int max_out_size)
    {
        int num_out = 0;
        int num_in = 0;

        while (num_in < in_len)
        {
            uint uni;
            int todo;
            byte ch = utf8String[num_in];
            num_in++;

            if (ch <= 0x7F)
            {
                uni = ch;
                todo = 0;
            }
            else if (ch <= 0xBF)
            {
                return -1;
            }
            else if (ch <= 0xDF)
            {
                uni = (uint)(ch & 0x1F);
                todo = 1;
            }
            else if (ch <= 0xEF)
            {
                uni = (uint)(ch & 0x0F);
                todo = 2;
            }
            else if (ch <= 0xF7)
            {
                uni = (uint)(ch & 0x07);
                todo = 3;
            }
            else
            {
                return -1;
            }

            for (int j = 0; j < todo; ++j)
            {
                if (num_in == in_len)
                    return -1;
                byte ch2 = utf8String[num_in];
                num_in++;
                if (ch2 < 0x80 || ch2 > 0xBF)
                    return -1;
                uni <<= 6;
                uni += (uint)(ch2 & 0x3F);
            }

            if (uni >= 0xD800 && uni <= 0xDFFF)
                return -1;
            if (uni > 0x10FFFF)
                return -1;

            if (uni <= 0xFFFF)
            {
                if (num_out == max_out_size)
                    return -1;
                utf16String[num_out] = (ushort)uni;
                num_out++;
            }
            else
            {
                uni -= 0x10000;
                if (num_out + 1 >= max_out_size)
                    return -1;
                utf16String[num_out] = (ushort)((uni >> 10) + 0xD800);
                utf16String[num_out + 1] = (ushort)((uni & 0x3FF) + 0xDC00);
                num_out += 2;
            }
        }

        if (num_out == max_out_size)
            return -1;
        utf16String[num_out] = 0;
        return num_out;
    }

    static void Main()
    {
        byte[] utf8String = new byte[] { 72, 101, 108, 108, 111, 44, 32, 228, 184, 150, 231, 149, 140, 33 }; // "Hello, 世界!" in UTF-8 encoding

        int max_out_size = 100;
        ushort[] utf16String = new ushort[max_out_size];

        int num_utf16_chars = Utf8ToUtf16(utf8String, utf8String.Length, utf16String, max_out_size);

        if (num_utf16_chars >= 0)
        {
            Console.Write("UTF-16 String: ");
            for (int i = 0; i < num_utf16_chars; ++i)
            {
                Console.Write(char.ConvertFromUtf32(utf16String[i]));
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Conversion failed!");
        }
    }
}
