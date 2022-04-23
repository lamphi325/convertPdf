using ConvertPdf.Interfaces;
using ConvertPdf.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Web;
using DevExpress.Pdf;
using System.Drawing;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace ConvertPdf.Logic
{
#pragma warning disable CS0219
#pragma warning disable CS0618
    public class ConvertPdf : IConvertPdf
    {
        private readonly IHostingEnvironment _webHostEnvironment;
        const float DrawingDpi = 72f;

        public ConvertPdf(IHostingEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void Convert()
        {
            var file = _webHostEnvironment.WebRootPath + "\\08-DL-An-Phat.pdf";
            try
            {
                var searchTexts = new string[] { "ĐẠI LÝ ỦY QUYỀN NHÔM TOPAL PRO", "AN PHÁT", "NO10-LK10-08-Khu đất DV Dọc Bún 1 P.La Khê, Q.Hà Đông, Hà Nội", "0975265678" };
                using (DevExpress.Pdf.PdfDocumentProcessor pdfDocumentProcessor = new DevExpress.Pdf.PdfDocumentProcessor())
                {
                    pdfDocumentProcessor.LoadDocument(file);
                    PdfTextSearchParameters pdfTextSearchParameters = new PdfTextSearchParameters();
                    pdfTextSearchParameters.CaseSensitive = true;
                    pdfTextSearchParameters.WholeWords = true;
                    SolidBrush white = (SolidBrush)Brushes.White;
                    foreach (var word in searchTexts)
                    {
                        PdfTextSearchResults pdfTextSearchResults = pdfDocumentProcessor.FindText(word, pdfTextSearchParameters);
                        PdfPageFacade pdfPageFacade = pdfDocumentProcessor.DocumentFacade.Pages[pdfTextSearchResults.PageIndex];
                        foreach (var rectangle in pdfTextSearchResults.Rectangles)
                        {
                            PdfRectangle pdfRectangle = new PdfRectangle(rectangle.Left, rectangle.Top - rectangle.Height, rectangle.Left + rectangle.Width, rectangle.Top);
                            PdfClearContentOptions options = new PdfClearContentOptions()
                            {
                                ClearAnnotations = false,
                                ClearGraphics = false,
                                ClearImages = false,
                            };
                            pdfPageFacade.ClearContent(pdfRectangle, options);
                            using (PdfGraphics pdfGraphics = pdfDocumentProcessor.CreateGraphics())
                            {
                                var fontName = pdfTextSearchResults.Words[0].Characters[0].Font.FontName;
                                var fontSize = (float)pdfTextSearchResults.Words[0].Characters[0].FontSize;
                                var authorize = "VIỆT NAM";
                                using (Font font = new Font(fontName, fontSize))
                                {
                                    SizeF sizeF = pdfGraphics.MeasureString(authorize, font, PdfStringFormat.GenericDefault);
                                    PointF topleft = new PointF((float)rectangle.Left, (float)rectangle.Top);
                                    PointF bottomright = new PointF((float)(rectangle.Left + rectangle.Width), (float)(rectangle.Top - rectangle.Height));
                                    pdfGraphics.DrawString(authorize, font, white, topleft);
                                    pdfGraphics.DrawString(authorize, font, white, bottomright);
                                    pdfGraphics.AddToPageForeground(pdfDocumentProcessor.Document.Pages[pdfTextSearchResults.PageIndex], DrawingDpi, DrawingDpi);
                                }
                            }
                        }
                    }
                    var save = _webHostEnvironment.WebRootPath + "\\convert_" + DateTime.Now.Ticks + ".pdf";
                    pdfDocumentProcessor.SaveDocument(save);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Convert2()
        {
            try
            {
                var file = _webHostEnvironment.WebRootPath + "\\08-DL-An-Phat.pdf";
                Aspose.Pdf.Document document = new Aspose.Pdf.Document(file);
                Aspose.Pdf.Text.TextFragmentAbsorber textFragment = new Aspose.Pdf.Text.TextFragmentAbsorber("AN PHÁT");
                document.Pages[1].Accept(textFragment);
                Aspose.Pdf.Text.TextFragmentCollection fragmentCollection = textFragment.TextFragments;
                foreach (Aspose.Pdf.Text.TextFragment text in fragmentCollection)
                {
                    text.Text = "VIỆT NAM";
                    text.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial");
                    text.TextState.FontSize = 35;
                    text.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.White);
                }
                var save = _webHostEnvironment.WebRootPath + "\\converted_" + DateTime.Now.Ticks + ".pdf";
                document.Save(save);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Convert3()
        {
            var file = _webHostEnvironment.WebRootPath + "\\08-DL-An-Phat.pdf";
            ZetPDF.Pdf.PdfDocument document = new ZetPDF.Pdf.PdfDocument(file);
            ZetPDF.Pdf.PdfDictionary dictionary = new ZetPDF.Pdf.PdfDictionary(document);
            ZetPDF.Pdf.PdfArray array = new ZetPDF.Pdf.PdfArray(document);
        }

        public void Convert4()
        {
            //string html = @"
            //    <!DOCTYPE html>
            //    <html xmlns=""http://www.w3.org/1999/xhtml"" lang="""" xml:lang="""">
            //    <head>
            //        <title></title>
            //        <meta http-equiv=""Content-Type"" content=""text/html""; charset=""UTF-8"" />
            //        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
            //        <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
            //    </head>
            //    <body style=""margin: 0; padding: 0; width: 892px; height: 1448px; background-image: url(D:\\inetpub\\ConvertPdf\\ConvertPdf\\wwwroot\\image\\PDFtoJPG.me-01.jpg); background-repeat: no-repeat; background-size: cover; object-fit: fill;"">
            //           <div id =""page1-div"" style=""margin: 0; padding: 0; position: relative; width: 892px; height: 1448px;"">
            //           <p style=""position: absolute; top: 1422px; left: 707px; white-space: nowrap; font-size: 13px; font-family: Calibri; color: #07476f; margin: 0; padding: 0;"">Nhôm hệ Topal &#160; | &#160;&#160;<b>1&#160;</b></p>
            //           <p style=""position: absolute; top: 1337px; left: 142px; white-space: nowrap; font-size: 65px; font-family: Calibri; color: #ffffff; margin: 0; padding: 0;""><b>AN&#160;PHÁT</b></p>
            //           <p style=""position: absolute; top: 1307px; left: 142px; white-space: nowrap; font-size: 25px; font-family: Calibri; color: #ffffff; margin: 0; padding: 0;"">ĐẠI LÝ ỦY QUYỀN NHÔM TOPAL PRO</p>
            //           <p style=""position: absolute; top: 1305px; left: 640px; white-space: nowrap; font-size: 20px; font-family: Calibri; color: #ffffff; margin: 0; padding: 0;"">NO10-LK10-08-Khu đất DV Dọc Bún 1</p>
            //           <p style=""position: absolute; top: 1327px; left: 640px; white-space: nowrap; font-size: 20px; font-family: Calibri; color: #ffffff; margin: 0; padding: 0;"">P.La Khê, Q. Hà Đông, Hà Nội</p>
            //           <p style=""position: absolute; top: 1380px; left: 640px; white-space: nowrap; font-size: 20px; font-family: Calibri; color: #ffffff; margin: 0; padding: 0;"">0975265678</p>
            //           <p style=""position: absolute; top: 918px; left: 242px; white-space: nowrap; font-size: 65px; font-family: Calibri; color: #07476f; margin: 0; padding: 0;"">CATALOGUE&#160;</p>
            //           <p style=""position: absolute; top: 918px; left: 584px; white-space: nowrap; font-size: 65px; font-family: Calibri; color: #f58220; margin: 0; padding: 0;"">NHÔM<b>&#160;</b></p>
            //        </div>
            //    </body>
            //    </html>
            //";
            string html = HtmlTemplate.Html;
            var file = _webHostEnvironment.WebRootPath + "\\convert1.pdf";
            SelectPdf.HtmlToPdf htmlToPdf = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(html);
            pdfDocument.Save(file);
            pdfDocument.Close();
        }
    }
}
