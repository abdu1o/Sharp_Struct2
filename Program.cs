using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Struct2
{

    struct RGBColor
    {
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

        public RGBColor(int red, int green, int blue)
        {
            if (red < 0 || red > 255 || green < 0 || green > 255 || blue < 0 || blue > 255)
            {
                red = 255;
                green = 255;
                blue = 255;
            }
                

            Red = red;
            Green = green;
            Blue = blue;
        }


        public string ToHex()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", Red, Green, Blue);
        }

        public (double Hue, double Saturation, double Lightness) ToHSL()
        {
            double red = Red / 255.0;
            double green = Green / 255.0;
            double blue = Blue / 255.0;

            double max = Math.Max(Math.Max(red, green), blue);
            double min = Math.Min(Math.Min(red, green), blue);
            double diff = max - min;

            double hue = 0;
            double saturation = 0;
            double lightness = (max + min) / 2;

            if (diff != 0)
            {
                if (max == red)
                    hue = 60 * ((green - blue) / diff % 6);
                else if (max == green)
                    hue = 60 * ((blue - red) / diff + 2);
                else
                    hue = 60 * ((red - green) / diff + 4);

                saturation = diff / (1 - Math.Abs(2 * lightness - 1));
            }

            return (hue, saturation, lightness);
        }

        
        public (double Cyan, double Magenta, double Yellow, double Key) ToCMYK()
        {
            double red = Red / 255;
            double green = Green / 255;
            double blue = Blue / 255;

            double key = 1 - Math.Max(Math.Max(red, green), blue);

            double cyan = (1 - red - key) / (1 - key);
            double magenta = (1 - green - key) / (1 - key);
            double yellow = (1 - blue - key) / (1 - key);

            return (cyan, magenta, yellow, key);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            RGBColor color = new RGBColor(255, 255, 255);

            string hex = color.ToHex();
            Console.WriteLine("HEX: " + hex);

            var hsl = color.ToHSL();
            Console.WriteLine($"HSL: Hue: {hsl.Hue}, Saturation: {hsl.Saturation}, Lightness: {hsl.Lightness}");

            var cmyk = color.ToCMYK();
            Console.WriteLine($"CMYK: Cyan: {cmyk.Cyan}, Magenta: {cmyk.Magenta}, Yellow: {cmyk.Yellow}, Key: {cmyk.Key}");
        }
    }
}
