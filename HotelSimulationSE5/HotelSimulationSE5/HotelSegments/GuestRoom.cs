﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments

{
    public class GuestRoom : IHSegment
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public int Classification { get; set; }
        public bool Reserved { get; set; }
        public List<Image> MyImages { get; set; }

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

    }
}
