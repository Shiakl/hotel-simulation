using System.Collections.Generic;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments

{
    public class GuestRoom : IHSegment
    {
        public string Classification { get; set; }
        public bool Reserved { get; set; } //Saves if the room is still free or if it has been reserved

        /// <summary>
        /// Sets the properties of each GuestRoom
        /// </summary>
        /// <param name="number">The ID of this GuestRoom</param>
        /// <param name="xseg">The dimension of the X axes</param>
        /// <param name="yseg">The dimension of the Y axes</param>
        /// <param name="classification">The classification(stars) of this GuestRoom</param>
        public GuestRoom(int number, int xseg, int yseg, string classification)
        {
            MyImages = new List<Image>(); //Some GuestRooms contain more then 1 node. This list will save multiple images if necessary, 1 for each node
            XDim = xseg;
            YDim = yseg;
            Reserved = false;
            Classification = classification;
            ID = number;

            switch (classification) //Different classification requires different images for the rooms
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
