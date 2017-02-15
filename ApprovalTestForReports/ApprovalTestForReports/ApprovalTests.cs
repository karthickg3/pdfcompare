using NUnit.Framework;
using ApprovalTests.Reporters;
using ApprovalTests;
using ApprovalTests.Core.Exceptions;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Threading;
using System.Runtime.Serialization;
using PdfSharp;
using System.Text;

namespace ApprovalTestForReports
{
    [UseReporter(typeof(DiffReporter), typeof(BeyondCompare4Reporter))]
    class ApprovalTests
    {
        ScreenCapture sc = new ScreenCapture();
        string dateTime = DateTime.Now.ToString("hh.mm.ss");
        public string message;

        [Test]
        public void TestPdf_Match()
        {
            try
            {
                Approvals.VerifyPdfFile(@"D:\ApprovalTestForReports\ApprovalTestForReports\ApprovalFiles\pdf.pdf");
            }

            finally
            {
                message = new ApprovalMismatchException(@"D:\ApprovalTestForReports\ApprovalTestForReports\ApprovalFiles\pdf.pdf", "my exception").Received;
                
            }           

        }

        [Test]
        public void TestPdf_Mismatch()
        {
            try
            {
                Approvals.VerifyPdfFile(@"D:\ApprovalTestForReports\ApprovalTestForReports\ApprovalFiles\pdf.pdf");
            }

            finally
            {
                message = new ApprovalMismatchException(@"D:\ApprovalTestForReports\ApprovalTestForReports\ApprovalFiles\pdf.pdf", "my exception").Received;

            }

        }

        [TearDown]
        public void TearDown()
        {
            var aStringBuilder = new StringBuilder(message);
            aStringBuilder.Remove(49, 13);
            aStringBuilder.Insert(49, "ScreenCapture");
            var newstring = aStringBuilder.ToString();

            Thread.Sleep(5000);

            // capture entire screen, and save it to a file
            Image img = sc.CaptureScreen();
            sc.CaptureScreen();
            
            sc.CaptureScreenToFile(newstring + dateTime + ".Gif", ImageFormat.Gif);
            
        }

    }
}
