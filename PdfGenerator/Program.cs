using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;

namespace PdfGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start...");
            MergePagesOfPdfs(@"C:\Users\myuser\Documents\new1.pdf",
                @"C:\Users\myuser\Documents\file1.pdf",
                @"C:\Users\myuser\Documents\file2.pdf");
            Console.WriteLine("finish...");
            Console.ReadKey();
        }

        public static void ImagesToPdf(string[] imagepaths, string[] pdfpaths)
        {
            for (int i = 0; i < 2; i++)
            {
                iTextSharp.text.Rectangle pageSize = null;

                using (var srcImage = new Bitmap(imagepaths[i].ToString()))
                {
                    pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
                }
                using (var ms = new MemoryStream())
                {
                    var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
                    iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                    document.Open();
                    var image = iTextSharp.text.Image.GetInstance(imagepaths[i].ToString());
                    document.Add(image);
                    document.Close();

                    File.WriteAllBytes(pdfpaths[i], ms.ToArray());
                }
            }
        }

        private static void MergePdfs(string outPutFilePath, params string[] filesPath)
        {
            List<PdfReader> readerList = new List<PdfReader>();
            foreach (string filePath in filesPath)
            {
                PdfReader pdfReader = new PdfReader(filePath);
                readerList.Add(pdfReader);
            }

            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Create blank output pdf file and get the stream to write on it.
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.Create));
            document.Open();

            foreach (PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    Image image = iTextSharp.text.Image.GetInstance(page);
                    // Rotate 90 degree
                    image.Rotation = 1.5708F;
                    document.Add(image);
                }
            }
            document.Close();
        }

        private static void MergePagesOfPdfs(string outPutFilePath, params string[] filesPath)
        {
            List<PdfReader> readerList = new List<PdfReader>();
            foreach (string filePath in filesPath)
            {
                PdfReader pdfReader = new PdfReader(filePath);
                readerList.Add(pdfReader);
            }

            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Create blank output pdf file and get the stream to write on it.
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.Create));
            document.Open();

            // page 1 from filesPath[1]
            PdfReader reader1 = readerList[1];
            {
                for (int i = 1; i <= 1; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader1, i);
                    Image image = iTextSharp.text.Image.GetInstance(page);
                    document.Add(image);
                }
            }

            // page 1-6 from filesPath[0]
            PdfReader reader2 = readerList[0];
            {
                for (int i = 1; i <= reader2.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader2, i);
                    Image image = iTextSharp.text.Image.GetInstance(page);
                    document.Add(image);
                }
            }

            document.Close();
        }
    }
}
