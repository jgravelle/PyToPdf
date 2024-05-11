using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace PyToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // The directory path is taken from the command line arguments
            string rootDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Get the name of the directory
            string directoryName = new DirectoryInfo(rootDirectory).Name;

            // Initialize PDF writer with the directory name as the file name
            var pdfWriter = new PdfWriter($"{directoryName}.pdf");
            var pdf = new PdfDocument(pdfWriter);
            var document = new Document(pdf);

            // Traverse all *.py files in the directory and its subdirectories
            foreach (var file in Directory.GetFiles(rootDirectory, "*.py", SearchOption.AllDirectories))
            {
                // Add the file name as a chapter title
                document.Add(new Paragraph(Path.GetFileName(file))
                    .SetBold()
                    .SetFontSize(12));

                // Read the content of the file
                string content = File.ReadAllText(file);

                // Add the content to the PDF
                document.Add(new Paragraph(content)
                    .SetFontSize(10));
            }

            // Close the PDF document
            document.Close();

            Console.WriteLine($"PDF '{directoryName}.pdf' created successfully!");
        }
    }
}
