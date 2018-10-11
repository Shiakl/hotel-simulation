﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSimulationSE5
{
    public class Guest
    {
        public int guest_x_size = 15;
        public int guest_y_size = 25;
        public HotelSegments.GuestRoom MyRoom { get; set; }
        
        public Image MyImage { get; set; }
        public int speed = -2;

        public Panel MyPanel { get; set; }
        private PictureBox panelPb;

        public Guest(Panel mypanel)
        {
            MyImage = Image.FromFile(@"..\..\Images\TempGuest2.png");
            MyPanel = mypanel;
            panelPb = new PictureBox();
            MyPanel.BackColor = Color.Transparent;
            MyPanel.Controls.Add(panelPb);
        }

        public void Add_panel(Panel mypanel)
        {
        }

        public void Move()
        {
            panelPb.Image = MyImage;
            MyPanel.BringToFront();
            MyPanel.BackColor = Color.Transparent;
        }
    }
}
