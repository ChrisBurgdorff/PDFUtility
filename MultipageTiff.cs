using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;



namespace MultipageTiff
{


        int firstpanel = 0;
        private void createPanel(string filename)
        {
            Image loadImage = Image.FromFile(filename);

            Panel p = new Panel();
            if (firstpanel == 0)
            {
                p.BackColor = System.Drawing.Color.PeachPuff;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Location = new System.Drawing.Point(15, 10);
                p.Size = new System.Drawing.Size(112, 100);
                firstpanel = 1;
            }
            else
            {
                p.BackColor = System.Drawing.Color.PeachPuff;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Location = new System.Drawing.Point(((Panel)PanelContainer[PanelContainer.Count - 1]).Left, ((Panel)PanelContainer[PanelContainer.Count - 1]).Bottom + 5);
                p.Size = new System.Drawing.Size(112, 100);

            }
            p.Visible = true;
            p.TabIndex = PanelContainer.Count;
            p.BackgroundImage = loadImage;
            p.BackgroundImageLayout = ImageLayout.Zoom;
            panel2.Controls.Add(p);
            PanelContainer.Add(p);



        }



        private ArrayList PanelContainer = new ArrayList();



        Image[] scannedImages;

        private void btnMultipageTiff_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            //sav.Filter = "*.tif|*.tiff";
            if (sav.ShowDialog() == DialogResult.OK)
            {
                doMultipageTiffSave(sav.FileName);
            }


        }



        private void btnExisting_Click(object sender, EventArgs e)
        {
            OpenFileDialog sav = new OpenFileDialog();
            // sav.Filter = "*.tif|*.tiff";
            if (sav.ShowDialog() == DialogResult.OK)
            {
                doExistingFileSave(sav.FileName);
            }



        }

        private void doMultipageTiffSave(string loc)
        {
            if (PanelContainer.Count > 0)
            {
                scannedImages = new Image[PanelContainer.Count];
                bool isSave = false;
                int j = 0;

                try
                {
                    foreach (Panel p in PanelContainer)
                    {
                        scannedImages[j] = p.BackgroundImage;
                        j++;
                        isSave = true;
                    }


                    bool res = false;
                    if (isSave)
                    {
                        res = saveMultipage(scannedImages, loc, "TIFF");
                    }

                    if (res)
                    {
                        MessageBox.Show("All Images saved successfully");
                    }
                }
                catch (System.Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }

        }
        private void doExistingFileSave(string loc)
        {
            if (PanelContainer.Count > 0)
            {
                scannedImages = new Image[PanelContainer.Count];
                bool isSave = false;
                int j = 0;

                try
                {
                    foreach (Panel p in PanelContainer)
                    {
                        scannedImages[j] = p.BackgroundImage;
                        j++;
                        isSave = true;
                    }
                    bool res = false;
                    if (isSave)
                    {
                        res = saveToExistingFile(loc, scannedImages, "TIFF");
                    }

                    if (res)
                    {
                        MessageBox.Show("All Images saved successfully");
                    }
                }
                catch (System.Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

            }
        }


        public void SaveMultipage(Image[] bmp, string location, string type)
        {
            if (bmp != null)
            {
                try
                {
                    ImageCodecInfo codecInfo = getCodecForstring(type);

                    for (int i = 0; i < bmp.Length; i++)
                    {
                        if (bmp[i] == null)
                            break;
                        bmp[i] = (Image)ConvertToBitonal((Bitmap)bmp[i]);
                    }

                    if (bmp.Length == 1)
                    {

                        EncoderParameters iparams = new EncoderParameters(1);
                        Encoder iparam = Encoder.Compression;
                        EncoderParameter iparamPara = new EncoderParameter(iparam, (long)(EncoderValue.CompressionCCITT4));
                        iparams.Param[0] = iparamPara;
                        bmp[0].Save(location, codecInfo, iparams);


                    }
                    else if (bmp.Length > 1)
                    {

                        Encoder saveEncoder;
                        Encoder compressionEncoder;
                        EncoderParameter SaveEncodeParam;
                        EncoderParameter CompressionEncodeParam;
                        EncoderParameters EncoderParams = new EncoderParameters(2);

                        saveEncoder = Encoder.SaveFlag;
                        compressionEncoder = Encoder.Compression;

                        // Save the first page (frame).
                        SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                        CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                        EncoderParams.Param[0] = CompressionEncodeParam;
                        EncoderParams.Param[1] = SaveEncodeParam;

                        File.Delete(location);
                        bmp[0].Save(location, codecInfo, EncoderParams);


                        for (int i = 1; i < bmp.Length; i++)
                        {
                            if (bmp[i] == null)
                                break;

                            SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                            CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                            EncoderParams.Param[0] = CompressionEncodeParam;
                            EncoderParams.Param[1] = SaveEncodeParam;
                            bmp[0].SaveAdd(bmp[i], EncoderParams);

                        }

                        SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                        EncoderParams.Param[0] = SaveEncodeParam;
                        bmp[0].SaveAdd(EncoderParams);
                    }
                    return true;


                }
                catch (System.Exception ee)
                {
                    throw new Exception(ee.Message + "  Error in saving as multipage ");
                }
            }
            else
                return false;

        }
        private ImageCodecInfo getCodecForstring(string type)
        {
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < info.Length; i++)
            {
                string EnumName = type.ToString();
                if (info[i].FormatDescription.Equals(EnumName))
                {
                    return info[i];
                }
            }

            return null;

        }



        //This function can save newly scanned images on existing single page or multipage file
        public bool saveToExistingFile(string fileName, Image[] bmp, string type)
        {
            try
            {
                //bmp[0] is containing Image from Existing file on which we will append newly scanned Images
                //SO first we will dicide wheter existing file is single page or multipage

                Image origionalFile = null;





                FileStream fr = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite);
                origionalFile = Image.FromStream(fr);
                int PageNumber = getPageNumber(origionalFile);

                if (bmp != null)
                {
                    for (int i = 0; i < bmp.Length; i++)
                    {
                        bmp[i] = (Image)ConvertToBitonal((Bitmap)bmp[i]);
                    }

                    if (PageNumber > 1)//Existing File is multi page tiff file
                    {
                        saveImageExistingMultiplePage(bmp, origionalFile, type, PageNumber, "shreeTemp.tif");

                    }
                    else if (PageNumber == 1)//Existing file is single page file
                    {
                        saveImageExistingSinglePage(bmp, origionalFile, type, "shreeTemp.tif");
                    }

                }
                else
                {
                    throw new Exception("Please give existing File and newly scanned image");
                }
                fr.Flush();
                fr.Close();

                System.IO.File.Replace("shreeTemp.tif", fileName, "Backup.tif", true);


                return true;


            }
            catch (System.Exception ee)
            {
                throw new Exception(ee.Message + "  Error in saving as multipage ");
            }
            return false;

        }

        private void saveImageExistingSinglePage(Image[] bmp, Image origionalFile, string type, string location)
        {
            try
            {
                //Now load the Codecs 
                ImageCodecInfo codecInfo = getCodecForstring(type);

                Encoder saveEncoder;
                Encoder compressionEncoder;
                EncoderParameter SaveEncodeParam;
                EncoderParameter CompressionEncodeParam;
                EncoderParameters EncoderParams = new EncoderParameters(2);

                saveEncoder = Encoder.SaveFlag;
                compressionEncoder = Encoder.Compression;


                //Me._img.SelectActiveFrame(FrameDimension.Page, fromPageIndex)
                //pages = New Bitmap(Me._img)

                // Save the first page (frame).
                SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                EncoderParams.Param[0] = CompressionEncodeParam;
                EncoderParams.Param[1] = SaveEncodeParam;

                origionalFile = ConvertToBitonal((Bitmap)origionalFile);

                origionalFile.Save(location, codecInfo, EncoderParams);


                for (int i = 0; i < bmp.Length; i++)
                {
                    SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                    CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                    EncoderParams.Param[0] = CompressionEncodeParam;
                    EncoderParams.Param[1] = SaveEncodeParam;
                    origionalFile.SaveAdd(bmp[i], EncoderParams);

                }

                SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                EncoderParams.Param[0] = SaveEncodeParam;
                origionalFile.SaveAdd(EncoderParams);
            }
            catch (System.Exception ee)
            {
                throw ee;
            }
        }

        private void saveImageExistingMultiplePage(Image[] bmp, Image origionalFile, string type, int PageNumber, string location)
        {

            try
            {
                //Now load the Codecs 
                ImageCodecInfo codecInfo = getCodecForstring(type);

                Encoder saveEncoder;
                Encoder compressionEncoder;
                EncoderParameter SaveEncodeParam;
                EncoderParameter CompressionEncodeParam;
                EncoderParameters EncoderParams = new EncoderParameters(2);
                Bitmap pages;
                Bitmap NextPage;


                saveEncoder = Encoder.SaveFlag;
                compressionEncoder = Encoder.Compression;

                origionalFile.SelectActiveFrame(FrameDimension.Page, 0);
                pages = new Bitmap(origionalFile);
                pages = ConvertToBitonal(pages);

                // Save the first page (frame).
                SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                EncoderParams.Param[0] = CompressionEncodeParam;
                EncoderParams.Param[1] = SaveEncodeParam;

                pages.Save(location, codecInfo, EncoderParams);


                for (int i = 1; i < PageNumber; i++)
                {
                    SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                    CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                    EncoderParams.Param[0] = CompressionEncodeParam;
                    EncoderParams.Param[1] = SaveEncodeParam;

                    origionalFile.SelectActiveFrame(FrameDimension.Page, i);
                    NextPage = new Bitmap(origionalFile);
                    NextPage = ConvertToBitonal(NextPage);
                    pages.SaveAdd(NextPage, EncoderParams);

                }

                for (int i = 0; i < bmp.Length; i++)
                {
                    SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                    CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                    EncoderParams.Param[0] = CompressionEncodeParam;
                    EncoderParams.Param[1] = SaveEncodeParam;
                    bmp[i] = (Bitmap)ConvertToBitonal((Bitmap)bmp[i]);
                    pages.SaveAdd(bmp[i], EncoderParams);

                }

                SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                EncoderParams.Param[0] = SaveEncodeParam;
                pages.SaveAdd(EncoderParams);
            }
            catch (System.Exception ee)
            {
                throw ee;
            }
        }


        private int getPageNumber(Image img)
        {

            Guid objGuid = img.FrameDimensionsList[0];
            FrameDimension objDimension = new FrameDimension(objGuid);

            //Gets the total number of frames in the .tiff file
            int PageNumber = img.GetFrameCount(objDimension);

            return PageNumber;
        }
        public Bitmap ConvertToBitonal(Bitmap original)
        {
            Bitmap source = null;

            // If original bitmap is not already in 32 BPP, ARGB format, then convert
            if (original.PixelFormat != PixelFormat.Format32bppArgb)
            {
                source = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
                source.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                using (Graphics g = Graphics.FromImage(source))
                {
                    g.DrawImageUnscaled(original, 0, 0);
                }
            }
            else
            {
                source = original;
            }

            // Lock source bitmap in memory
            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // Copy image data to binary array
            int imageSize = sourceData.Stride * sourceData.Height;
            byte[] sourceBuffer = new byte[imageSize];
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize);

            // Unlock source bitmap
            source.UnlockBits(sourceData);

            // Create destination bitmap
            Bitmap destination = new Bitmap(source.Width, source.Height, PixelFormat.Format1bppIndexed);

            // Lock destination bitmap in memory
            BitmapData destinationData = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Create destination buffer
            imageSize = destinationData.Stride * destinationData.Height;
            byte[] destinationBuffer = new byte[imageSize];

            int sourceIndex = 0;
            int destinationIndex = 0;
            int pixelTotal = 0;
            byte destinationValue = 0;
            int pixelValue = 128;
            int height = source.Height;
            int width = source.Width;
            int threshold = 500;

            // Iterate lines
            for (int y = 0; y < height; y++)
            {
                sourceIndex = y * sourceData.Stride;
                destinationIndex = y * destinationData.Stride;
                destinationValue = 0;
                pixelValue = 128;

                // Iterate pixels
                for (int x = 0; x < width; x++)
                {
                    // Compute pixel brightness (i.e. total of Red, Green, and Blue values)
                    pixelTotal = sourceBuffer[sourceIndex + 1] + sourceBuffer[sourceIndex + 2] + sourceBuffer[sourceIndex + 3];
                    if (pixelTotal > threshold)
                    {
                        destinationValue += (byte)pixelValue;
                    }
                    if (pixelValue == 1)
                    {
                        destinationBuffer[destinationIndex] = destinationValue;
                        destinationIndex++;
                        destinationValue = 0;
                        pixelValue = 128;
                    }
                    else
                    {
                        pixelValue >>= 1;
                    }
                    sourceIndex += 4;
                }
                if (pixelValue != 128)
                {
                    destinationBuffer[destinationIndex] = destinationValue;
                }
            }

            // Copy binary image data to destination bitmap
            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize);

            // Unlock destination bitmap
            destination.UnlockBits(destinationData);

            // Return
            return destination;
        }

        //SHIT I NEED

        //END OF SHIT I NEED



}


