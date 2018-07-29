//Gürkan GÖKDEMIR

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D; // for resize image
using System.Drawing.Imaging; //to save image format
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageProcessor
{
    public partial class formImageProcessor : Form
    {
        Bitmap imageBitmap;
        Image imageFile;
        Boolean isFileOpened = false;

        public formImageProcessor()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialogImage.ShowDialog();
            chartHistogram.Hide();

            if (dialogResult == DialogResult.OK)
            {
                imageFile = Image.FromFile(openFileDialogImage.FileName);
                imageBitmap = new Bitmap(openFileDialogImage.FileName);
                pictureBoxOriginalImage.Image = imageFile;
                isFileOpened = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
            DialogResult dialogResult = saveFileDialogImage.ShowDialog();

                        if (dialogResult == DialogResult.OK)
                        {
                            if (isFileOpened)
                            {

                                if (saveFileDialogImage.FileName.Substring(saveFileDialogImage.FileName.Length - 3).ToLower() == "bmp")
                                {
                                    imageFile.Save(saveFileDialogImage.FileName, ImageFormat.Bmp); //imported system.drawing.imaging
                                }
                                if (saveFileDialogImage.FileName.Substring(saveFileDialogImage.FileName.Length - 3).ToLower() == "jpg")
                                {
                                    imageFile.Save(saveFileDialogImage.FileName, ImageFormat.Jpeg); //imported system.drawing.imaging
                                }
                                if (saveFileDialogImage.FileName.Substring(saveFileDialogImage.FileName.Length - 3).ToLower() == "png")
                                {
                                    imageFile.Save(saveFileDialogImage.FileName, ImageFormat.Png); //imported system.drawing.imaging
                                }
                                if (saveFileDialogImage.FileName.Substring(saveFileDialogImage.FileName.Length - 3).ToLower() == "gif")
                                {
                                    imageFile.Save(saveFileDialogImage.FileName, ImageFormat.Gif); //imported system.drawing.imaging
                                }

                            }
                            else
                            {
                                MessageBox.Show("Please choose an image.");
                            }
                        }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
            
        }

        private void buttonGray_Click(object sender, EventArgs e)
        {
            try
            {
             for (int x = 0; x < imageBitmap.Width; x++)
                        {
                            for (int y = 0; y < imageBitmap.Height; y++)
                            {
                                Color originalColor = imageBitmap.GetPixel(x, y);
                                int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .60) + (originalColor.B * .10)); //.num makes all variable smaller
                                Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                                imageBitmap.SetPixel(x, y, grayColor);
                            }
                        }
                pictureBoxOriginalImage.Image = imageBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
           
        }

        private void buttonInvert_Click(object sender, EventArgs e)
        {
            try
            {

            for (int x = 0; x < imageBitmap.Width; x++)
            {
                for (int y = 0; y < imageBitmap.Height; y++)
                {
                    Color invertColor = imageBitmap.GetPixel(x, y);

                    int red = invertColor.R;
                    int green = invertColor.G;
                    int blue = invertColor.B;

                    imageBitmap.SetPixel(x, y, Color.FromArgb(255 - red, 255 - green, 255 - blue));
                }
            }
            pictureBoxOriginalImage.Image = imageBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
        }

        private void buttonTurnL_Click(object sender, EventArgs e)
        {
            try
            {
            if (imageBitmap != null)
                        {
                            imageBitmap.RotateFlip(RotateFlipType.Rotate270FlipY);
                        }
                        pictureBoxOriginalImage.Image = imageBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
            
        }
       
        private void buttonTurnR_Click_1(object sender, EventArgs e)
        {
            try
            {
            if (imageBitmap != null)
                          {
                               imageBitmap.RotateFlip(RotateFlipType.Rotate90FlipY);
                          }
                        pictureBoxOriginalImage.Image = imageBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
            
        }

        private void buttonLeftMirror_Click(object sender, EventArgs e)
        {
            try
            {
            if (imageBitmap != null)
                        {
                            imageBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        pictureBoxOriginalImage.Image = imageBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
            
        }

        private void buttonRightMirror_Click(object sender, EventArgs e)
        {
            try
            {
            if (imageBitmap != null)
                        {
                            imageBitmap.RotateFlip(RotateFlipType.Rotate180FlipY);
                        }
                        pictureBoxOriginalImage.Image = imageBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
            
        }

        private void buttonScale_Click(object sender, EventArgs e)
        {
            try
            {
                        double width;
                        double.TryParse(textBoxScaleX.Text, out width);

                        double height;
                        double.TryParse(textBoxScaleY.Text, out height);


                        var ratioX = (double)width / imageFile.Width;
                        var ratioY = (double)height / imageFile.Height;
                        //var ratio = Math.Min(ratioX, ratioY);

                        var newWidth = (int)(imageFile.Width * ratioX);
                        var newHeight = (int)(imageFile.Height * ratioY);

                        var newImage = new Bitmap(newWidth, newHeight);

                        using (var graphics = Graphics.FromImage(newImage))
                            graphics.DrawImage(imageFile, 0, 0, newWidth, newHeight);

                        pictureBoxNewImage.Image = newImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please insert a number. \n" + ex.Message);
            }

            
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            try
            {
             for (int x = 0; x < imageBitmap.Width; x++)
                        {
                            for (int y = 0; y < imageBitmap.Height; y++)
                            {
                                Color originalColor = imageBitmap.GetPixel(x, y);
                                int red = originalColor.R;
                                int green = originalColor.G;
                                int blue = originalColor.B;

                                imageBitmap.SetPixel(x, y, Color.FromArgb(red, 0, 0));

                            }
                    pictureBoxOriginalImage.Image = imageBitmap;
                }
                buttonBlue.Hide();
                buttonGreen.Hide();
                //buttonRed.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
           
            
        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
            try
            {
            for (int x = 0; x < imageBitmap.Width; x++)
                        {
                            for (int y = 0; y < imageBitmap.Height; y++)
                            {
                                Color originalColor = imageBitmap.GetPixel(x, y);
                                int red = originalColor.R;
                                int green = originalColor.G;
                                int blue = originalColor.B;

                                imageBitmap.SetPixel(x, y, Color.FromArgb(0, green, 0));

                            }
                    pictureBoxOriginalImage.Image = imageBitmap;
                    buttonBlue.Hide();
                    //buttonGreen.Hide();
                    buttonRed.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
            
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            try
            {
            for (int x = 0; x < imageBitmap.Width; x++)
                        {
                            for (int y = 0; y < imageBitmap.Height; y++)
                            {
                                Color originalColor = imageBitmap.GetPixel(x, y);
                                int red = originalColor.R;
                                int green = originalColor.G;
                                int blue = originalColor.B;

                                imageBitmap.SetPixel(x, y, Color.FromArgb(0, 0, blue));

                            }
                    pictureBoxOriginalImage.Image = imageBitmap;
                    //buttonBlue.Hide();
                    buttonGreen.Hide();
                    buttonRed.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an image. \n" + ex.Message);
            }
            
        }

        private void buttonHistogram_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    imageFile = Image.FromFile(openFileDialogImage.FileName);
                    imageBitmap = new Bitmap(openFileDialogImage.FileName);
                    pictureBoxOriginalImage.Image = imageFile;
                    isFileOpened = true;
                    buttonBlue.Show();
                    buttonGreen.Show();
                    buttonRed.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please choose an image. \n" + ex.Message);
                }
                Dictionary<Color, int> histogram = new Dictionary<Color, int>();
                for (int x = 0; x < imageBitmap.Width; x++)
                {


                    for (int y = 0; y < imageBitmap.Height; y++)
                    {
                        Color c = imageBitmap.GetPixel(x, y);
                        if (histogram.ContainsKey(c))
                            histogram[c] = histogram[c] + 1;
                        else
                            histogram.Add(c, 1);
                    }
                }

                foreach (Color key in histogram.Keys)
                {
                    chartHistogram.Show();
                    this.chartHistogram.Series["Histogram"].Points.AddY(histogram[key]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void formImageProcessor_Load(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonGrayHistogram_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    for (int x = 0; x < imageBitmap.Width; x++)
                    {
                        for (int y = 0; y < imageBitmap.Height; y++)
                        {
                            Color originalColor = imageBitmap.GetPixel(x, y);
                            int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .60) + (originalColor.B * .10)); //.num makes all variable smaller
                            Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                            imageBitmap.SetPixel(x, y, grayColor);
                        }
                    }
                    pictureBoxOriginalImage.Image = imageBitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please choose an image. \n" + ex.Message);
                }
                Dictionary<Color, int> histogram = new Dictionary<Color, int>();
                for (int x = 0; x < imageBitmap.Width; x++)
                {
                    for (int y = 0; y < imageBitmap.Height; y++)
                    {
                        Color c = imageBitmap.GetPixel(x, y);
                        if (histogram.ContainsKey(c))
                            histogram[c] = histogram[c] + 1;
                        else
                            histogram.Add(c, 1);
                    }
                }

                foreach (Color key in histogram.Keys)
                {
                    chartHistogram.Show();
                    this.chartHistogram.Series["Histogram"].Points.AddY(histogram[key]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonReOpen_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void buttonReOpen_Click(object sender, EventArgs e)
            {
                try
                {
                    imageFile = Image.FromFile(openFileDialogImage.FileName);
                    imageBitmap = new Bitmap(openFileDialogImage.FileName);
                    pictureBoxOriginalImage.Image = imageFile;
                    isFileOpened = true;
                buttonBlue.Show();
                buttonGreen.Show();
                buttonRed.Show();
                chartHistogram.Hide();
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Please choose an image. \n" + ex.Message);
                }

            }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
