using System;
using IKVMExample;


namespace IKVMConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Call into your IKVMExample library
            try
            {
                Console.WriteLine("Starting PDF generation...");
                PdfGenerator.GeneratePdf();
                Console.WriteLine("PDF generation completed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
