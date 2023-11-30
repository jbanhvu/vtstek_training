using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace CNY_BaseSys.Common
{
	/// <summary>
	/// Summary description for CaptchaImage.
	/// </summary>
	public class CaptchaImage
	{
		public string CaptchaText
		{
			get { return this._text; }
		}
		public Bitmap Image
		{
			get { return this._image; }
		}
		public int Width
		{
			get { return this._width; }
		}
		public int Height
		{
			get { return this._height; }
		}

	
		private readonly string _text;
		private int _width;
		private int _height;
		private string _familyName;
		private Bitmap _image;


		private readonly Random _random = new Random();


        public CaptchaImage(int textLength, bool isNumberText, int width, int height)
		{
            this._text = GetRandomText(textLength, isNumberText);
			this.SetDimensions(width, height);
			this.GenerateImage();
		}


        public CaptchaImage(int textLength, bool isNumberText, int width, int height, string familyName)
        {
            this._text = GetRandomText(textLength, isNumberText);
            this.SetDimensions(width, height);
            this.SetFamilyName(familyName);
            this.GenerateImage();
        }

      
		~CaptchaImage()
		{
			Dispose(false);
		}

	
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				this._image.Dispose();
		}

        private string GetRandomText(int length, bool isNumber)
        {
            StringBuilder randomText = new StringBuilder();
            string code = "";
            string alphabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string numbers = "1234567890";
            Random r = new Random();

            for (int j = 0; j < length; j++)
            {
                randomText.Append(isNumber ? numbers[r.Next(numbers.Length)] : alphabets[r.Next(alphabets.Length)]);
            }

            code = randomText.ToString();
            return code;
        }



		private void SetDimensions(int width, int height)
		{
			if (width <= 0)
				throw new ArgumentOutOfRangeException("width", width, @"Argument out of range, must be greater than zero.");
			if (height <= 0)
				throw new ArgumentOutOfRangeException("height", height, @"Argument out of range, must be greater than zero.");
			this._width = width;
			this._height = height;
		}

	
		private void SetFamilyName(string familyName)
		{
	
			try
			{
				Font font = new Font(this._familyName, 12F);
				this._familyName = familyName;
				font.Dispose();
			}
			catch (Exception)
			{
				this._familyName = FontFamily.GenericSerif.Name;
			}
		}

    
		private void GenerateImage()
		{
			// Create a new 32-bit bitmap image.
			Bitmap bitmap = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb);

			// Create a graphics object for drawing.
			Graphics g = Graphics.FromImage(bitmap);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rect = new Rectangle(0, 0, this._width, this._height);

			// Fill in the background.
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.Cross, Color.Azure, Color.WhiteSmoke);
			g.FillRectangle(hatchBrush, rect);

			// Set up the text font.
			SizeF size;
			float fontSize = rect.Height + 1;
			Font font;
			// Adjust the font size until the text fits within the image.
			do
			{
				fontSize--;
				font = new Font(this._familyName, fontSize, FontStyle.Bold);
				size = g.MeasureString(this._text, font);
			} while (size.Width > rect.Width);

			// Set up the text format.
		    StringFormat format = new StringFormat
		    {
		        Alignment = StringAlignment.Center,
		        LineAlignment = StringAlignment.Center
		    };

		    // Create a path using the text and warp it randomly.
			GraphicsPath path = new GraphicsPath();
			path.AddString(this._text, font.FontFamily, (int) font.Style, font.Size, rect, format);
			float v = 4F;
			PointF[] points =
			{
				new PointF(this._random.Next(rect.Width) / v, this._random.Next(rect.Height) / v),
				new PointF(rect.Width - this._random.Next(rect.Width) / v, this._random.Next(rect.Height) / v),
				new PointF(this._random.Next(rect.Width) / v, rect.Height - this._random.Next(rect.Height) / v),
				new PointF(rect.Width - this._random.Next(rect.Width) / v, rect.Height - this._random.Next(rect.Height) / v)
			};
			Matrix matrix = new Matrix();
			matrix.Translate(0F, 0F);
			path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

			// Draw the text.
			hatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.DarkSlateBlue, Color.DarkGray);
			g.FillPath(hatchBrush, path);

			// Add some random noise.
			int m = Math.Max(rect.Width, rect.Height);
			for (int i = 0; i < (int) (rect.Width * rect.Height / 30F); i++)
			{
				int x = this._random.Next(rect.Width);
				int y = this._random.Next(rect.Height);
				int w = this._random.Next(m / 50);
				int h = this._random.Next(m / 50);
				g.FillEllipse(hatchBrush, x, y, w, h);
			}

			// Clean up.
			font.Dispose();
			hatchBrush.Dispose();
			g.Dispose();

			// Set the image.
			this._image = bitmap;
		}
	}
}
