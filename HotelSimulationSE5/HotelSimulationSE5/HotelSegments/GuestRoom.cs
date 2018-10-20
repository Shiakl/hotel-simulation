using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments

{
    public class GuestRoom : HSegment
    {
        public int Classification { get; set; }
        public bool Reserved { get; set; }

        public enum CLASSIFICATION
        {
            one_Star,

        }

        public GuestRoom(int number, int xseg, int yseg, int classification)
        {
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            Reserved = false;
            Classification = classification;
            ID = number;

            switch (Classification)
            {
                case 1:
                    MyImages.Add(Image.FromFile(@"..\..\Images\1-star.png"));
                    break;
                case 2:
                    MyImages.Add(Image.FromFile(@"..\..\Images\2-star.png"));
                    break;
                case 3:
                    MyImages.Add(Image.FromFile(@"..\..\Images\3-star1.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\3-star2.png"));
                    break;
                case 4:
                    MyImages.Add(Image.FromFile(@"..\..\Images\4-star1.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\4-star2.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\4-star3.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\4-star4.png"));
                    break;
                case 5:
                    MyImages.Add(Image.FromFile(@"..\..\Images\5-star1.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\5-star2.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\5-star3.png"));
                    MyImages.Add(Image.FromFile(@"..\..\Images\5-star4.png"));
                    break;
                default:
                    break;
            }
        }

        public void Reserved_room()
        {
            Reserved = true;
            switch (Classification)
            {
                case 1:
                    MyImages[(int)Node.SEGMENT_PART.Main]= Image.FromFile(@"..\..\Images\1-starreserved.png");
                    break;
                case 2:
                    MyImages[(int)Node.SEGMENT_PART.Main] = Image.FromFile(@"..\..\Images\2-starreserved.png");
                    break;
                case 3:
                    MyImages[(int)Node.SEGMENT_PART.Main] = Image.FromFile(@"..\..\Images\3-star1reserved.png");
                    break;
                case 4:
                    MyImages[(int)Node.SEGMENT_PART.Main] = Image.FromFile(@"..\..\Images\4-star1reserved.png");
                    break;
                case 5:
                    MyImages[(int)Node.SEGMENT_PART.Main] = Image.FromFile(@"..\..\Images\5-star1reserved.png");
                    break;
                default:
                    break;
            }

            
        }

    }
}
