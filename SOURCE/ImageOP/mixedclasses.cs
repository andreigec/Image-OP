﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace ImageOP
{
		public class ICL
		{
			public Image I;
			public Panel output;
			public String filename = "";

			public void clearImages()
			{
				output.BackgroundImage = null;
				I = null;
			}
		}

		public class xy
		{
			public int x;
			public int y;
		}

		public class threadinfo
		{
			public int xmax = -1;
			public int xmin = -1;
			public int ymax = -1;
			public int ymin = -1;

			public threadinfo(int xminIN, int xmaxIN, int yminIN, int ymaxIN)
			{
				xmin = xminIN;
				xmax = xmaxIN;
				ymin = yminIN;
				ymax = ymaxIN;
			}
		}

		public class im
		{
			public byte[] Array;
			public IntPtr Scan0;
			public int Stride;
			public PixelFormat Format;
			public int Width = -1;
			public int Height = -1;

			public im()
			{
				
			}
			public im(ICL inicl)
			{
				if (inicl.I == null)
					return;

				var b = inicl.I as Bitmap;
				if (b == null)
					return;

				var rect = new Rectangle(0, 0, b.Width, b.Height);
				var x=b.GetPixel(3, 3);
				var bmpData =
				b.LockBits(rect, ImageLockMode.ReadOnly,
							b.PixelFormat);
				var PF = b.PixelFormat;

				var by = Math.Abs(bmpData.Stride) * b.Height;
				var byl = new byte[by];

				// Copy the RGB values into the array.
				Marshal.Copy(bmpData.Scan0, byl, 0, by);
				b.UnlockBits(bmpData);

				Array = byl;
				Format = bmpData.PixelFormat;
				Height = b.Height;
				Width = b.Width;
				Scan0 = bmpData.Scan0;
				Stride = bmpData.Stride;
			}
		}

		public class information
		{
			public im pixelsTo;
			public Dictionary<int, im> imlist;
			public int maxh, maxw;

			public information()
			{
			}

		}
	
}
