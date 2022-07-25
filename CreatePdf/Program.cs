using IronPdf;

namespace CreatePdf
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = @"<h1>Olá, se você chegou até aqui você é meu convidado!</h1>
            <p> Divirta-se com meu código </p>
            <div style = 'page-break-after: always;' ></div>
            <h2> Esse é um modo de como gerar um PDF utilizando o ironPDF</h2>
            <div style = 'page-break-after: always;' ></div>
            <p> Obrigado!</p>
            <div style = 'page-break-after: always;' ></div>
            <link href=""https://fonts.googleapis.com/css?family=Libre Barcode 128""rel = ""stylesheet"" ><p style = ""font-family: 'Libre Barcode 128', serif; font-size:30px;""> Hello Google Fonts</p>";

            var Renderer = new IronPdf.ChromePdfRenderer();
            using var cover = Renderer.RenderHtmlAsPdf("<h1> This is Cover Page</h1>");

            Renderer.RenderingOptions.FirstPageNumber = 2;

            Renderer.RenderingOptions.HtmlFooter = new IronPdf.HtmlHeaderFooter()
            {

                MaxHeight = 15, //millimeters
                HtmlFragment = "<center><i>{page} of {total-pages}<i></center>",
                DrawDividerLine = true
            };

            using PdfDocument Pdf = Renderer.RenderHtmlAsPdf(html);

            //Merging PDF document with Cover page
            using PdfDocument merge = IronPdf.PdfDocument.Merge(cover, Pdf);

            //PDF Settings
            merge.SecuritySettings.AllowUserCopyPasteContent = false;
            //merge.SecuritySettings.UserPassword = "sharable"; -- Password in file

            merge.SaveAs("combined.pdf");
        }
    }
}
