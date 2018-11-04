<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Theme_Hotel
{
    public partial class Form1 : Form
    {
        PBLoader Startup = new PBLoader();
        private int _pbammount = 0;
        private int _count = 0;
        private int _roomsize = 65;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Startup.JsonLoader();
            HotelLoader();
        }

        private void HotelLoader()
        {
            PictureBox[] picture = new PictureBox[Startup.Getjsonoutput.Count];

            foreach (var rooms in Startup.Getjsonoutput)
            {
                picture[_count] = new PictureBox();
                picture[_count].Name = rooms.AreaType + _count;
                picture[_count].Size = new Size(rooms.DimX * _roomsize, rooms.DimY * _roomsize);
                picture[_count].Location = new Point(rooms.CordX * _roomsize, rooms.CordY * _roomsize);
                picture[_count].SizeMode = PictureBoxSizeMode.StretchImage;
                
                if (rooms.AreaType == "Cinema")
                {
                    picture[_count].Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\", "cinema.png"));
                }

                else if (rooms.AreaType == "Restaurant")
                {
                    picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "restaurant.png"));
                }

                else if (rooms.AreaType == "Room")
                {
                    if (rooms.Classification == "1 Star")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "room.png"));
                    }

                    else if (rooms.Classification == "2 stars")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "roomsprite.png"));
                    }

                    else if (rooms.Classification == "3 stars")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "room2.png"));
                    }

                    else if (rooms.Classification == "4 stars")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "nummer4.jpg"));
                    }

                    else if (rooms.Classification == "5 stars")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "nummer5.jpg"));
                    }
                }

                else if (rooms.AreaType == "Fitness")
                {
                    picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "fitness.png"));
                }

                this.Controls.Add(picture[_count]);
                _count++;
            }
            _count = 0;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Theme_Hotel
{
    public partial class Form1 : Form
    {
        PBLoader Startup = new PBLoader();
        private int _pbammount = 0;
        private int _count = 0;
        private int _roomsize = 65;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Startup.JsonLoader();
            HotelLoader();
        }

        private void HotelLoader()
        {
            PictureBox[] picture = new PictureBox[Startup.Getjsonoutput.Count];

            foreach (var rooms in Startup.Getjsonoutput)
            {
                picture[_count] = new PictureBox();
                picture[_count].Name = rooms.AreaType + _count;
                picture[_count].Size = new Size(rooms.DimX * _roomsize, rooms.DimY * _roomsize);
                picture[_count].Location = new Point(rooms.CordX * _roomsize, rooms.CordY * _roomsize);
                picture[_count].SizeMode = PictureBoxSizeMode.StretchImage;
                
                if (rooms.AreaType == "Cinema")
                {
                    picture[_count].Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\", "cinema.png"));
                }

                else if (rooms.AreaType == "Restaurant")
                {
                    picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "restaurant.png"));
                }

                else if (rooms.AreaType == "Room")
                {
                    if (rooms.Classification == "1 Star")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "room.png"));
                    }

                    else if (rooms.Classification == "2 stars")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "roomsprite.png"));
                    }

                    else if (rooms.Classification == "3 stars")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "room2.png"));
                    }

                    else if (rooms.Classification == "4 stars")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "nummer4.jpg"));
                    }

                    else if (rooms.Classification == "5 stars")
                    {
                        picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "nummer5.jpg"));
                    }
                }

                else if (rooms.AreaType == "Fitness")
                {
                    picture[_count].Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\", "fitness.png"));
                }

                this.Controls.Add(picture[_count]);
                _count++;
            }
            _count = 0;
        }
    }
}
>>>>>>> origin/sang
