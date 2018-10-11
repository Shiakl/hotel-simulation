using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments

{
    public class GuestRoom : IHSegment
    {
        public int segment_num { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public string Classification { get; set; }
        public Color MyColor { get; set; }
        public bool Reserved { get; set; }
        public List<Image> MyImages { get; set; }

        public GuestRoom(int number, string classification)
        {
            Reserved = false;
            Classification = classification;
            segment_num = number;
            MyColor = Color.Green;

            switch (classification)
            {
                case "1 Star":
                    MyImages.Add(Image.FromFile(@"..\..\Images\1-star.png"));
                    break;
                case "2 stars":
                    MyImages.Add(Image.FromFile(@"..\..\Images\2-star.png"));
                    break;
                case "3 stars":
                    MyImages.Add(Image.FromFile(@"..\..\Images\3-star1.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\3-star2.png"));
                    break;
                case "4 stars":
                    MyImages.Add(Image.FromFile(@"..\..\Images\4-star1.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\4-star2.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\4-star3.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\4-star4.png"));
                    break;
                case "5 stars":
                    MyImages.Add(Image.FromFile(@"..\..\Images\5-star1.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\5-star2.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\5-star3.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\5-star4.png"));
                    break;
                default:
                    break;
            }
        }

    }
}
