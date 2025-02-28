using System;
using System.IO;
using System.Text;
using org.apache.commons.lang3; // From Maven dependency commons-lang3
using org.apache.commons.lang3.math; // For NumberUtils
using com.google.common.collect; // From Maven dependency Guava
using java.util; // Java collection from IKVM core

// Apache FOP imports
using org.apache.fop.apps;
using javax.xml.transform;
using javax.xml.transform.sax;
using javax.xml.transform.stream;
using java.io;

namespace IKVMExample
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("=== IKVM Maven Dependencies Example ===\n");

            // Example 1: Using Apache Commons Lang3 for string manipulation
            System.Console.WriteLine("--- Example 1: Apache Commons Lang3 ---");
            string text = "Hello, IKVM with Maven!";
            System.Console.WriteLine($"Original text: {text}");
            System.Console.WriteLine($"Reversed: {StringUtils.reverse(text)}");
            System.Console.WriteLine($"Capitalized: {StringUtils.capitalize(text)}");

            // Example of using NumberUtils to check if a string is a number
            string numberStr = "42";
            bool isNumber = NumberUtils.isParsable(numberStr);
            System.Console.WriteLine($"Is '{numberStr}' a number? {isNumber}");
            System.Console.WriteLine();

            // Example 2: Using Google Guava Collections
            System.Console.WriteLine("--- Example 2: Google Guava Collections ---");

            // Creating an immutable list using Guava
            ImmutableList guavaList = ImmutableList.of("Apple", "Banana", "Cherry");
            System.Console.WriteLine($"Guava ImmutableList size: {guavaList.size()}");
            System.Console.WriteLine($"First element: {guavaList.get(0)}");

            // Creating a multimap using Guava
            ArrayListMultimap multimap = ArrayListMultimap.create();
            multimap.put("Fruits", "Apple");
            multimap.put("Fruits", "Banana");
            multimap.put("Vegetables", "Carrot");
            System.Console.WriteLine($"Multimap size: {multimap.size()}");

            // Retrieving and iterating over values in the multimap
            Collection fruitValues = multimap.get("Fruits");
            System.Console.WriteLine($"Multimap has {fruitValues.size()} fruits");

            Iterator fruitIterator = fruitValues.iterator();
            System.Console.Write("Fruits: ");
            while (fruitIterator.hasNext())
            {
                System.Console.Write($"{fruitIterator.next()} ");
            }
            System.Console.WriteLine();
            System.Console.WriteLine();

            // Example 3: Mixing Java and .NET collections
            System.Console.WriteLine("--- Example 3: Mixing Java and .NET ---");

            // Creating a Java ArrayList and adding elements
            ArrayList javaList = new ArrayList();
            javaList.add("Item 1");
            javaList.add("Item 2");

            // Converting Java ArrayList to .NET List
            System.Collections.Generic.List<string> dotNetList = new System.Collections.Generic.List<string>();
            for (int i = 0; i < javaList.size(); i++)
            {
                dotNetList.Add((string)javaList.get(i));
            }
            dotNetList.Add("Item 3 (.NET)");
            System.Console.WriteLine($"Java list size: {javaList.size()}");
            System.Console.WriteLine($".NET list count: {dotNetList.Count}");

            // Example 4: Generating a PDF using Apache FOP
            System.Console.WriteLine("\n--- Example 4: Apache FOP PDF Generation ---");
            try
            {
                GeneratePdfFromXslFo();
                System.Console.WriteLine("PDF generated successfully! Check output.pdf in the application directory.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error generating PDF: {ex.Message}");
                System.Console.WriteLine(ex.StackTrace);
            }

            System.Console.WriteLine("\nPress any key to exit...");
            System.Console.ReadKey();
        }

        static void GeneratePdfFromXslFo()
        {
            // XSL-FO content for the PDF
            string xslFoContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
    <fo:root xmlns:fo=""http://www.w3.org/1999/XSL/Format"">
      <fo:layout-master-set>
        <fo:simple-page-master master-name=""A4"" page-height=""297mm"" page-width=""210mm"" margin=""1cm"">
          <fo:region-body margin=""2cm""/>
        </fo:simple-page-master>
      </fo:layout-master-set>
      <fo:page-sequence master-reference=""A4"">
        <fo:flow flow-name=""xsl-region-body"">
          <fo:block font-family=""Helvetica"" font-size=""24pt"" font-weight=""bold"" text-align=""center"" margin-bottom=""20pt"">
            Apache FOP with IKVM in .NET
          </fo:block>
          <fo:block font-family=""Helvetica"" font-size=""12pt"" line-height=""15pt"" space-after=""12pt"">
            This is a sample PDF document generated using Apache FOP from a .NET application using IKVM.
          </fo:block>
          <fo:block font-family=""Helvetica"" font-size=""12pt"" line-height=""15pt"" space-after=""12pt"">
            The integration demonstrates how Java libraries can be used seamlessly within .NET code.
          </fo:block>
          <fo:block font-family=""Helvetica"" font-size=""14pt"" font-weight=""bold"" margin-top=""20pt"" margin-bottom=""10pt"">
            Features included:
          </fo:block>
          <fo:list-block>
            <fo:list-item>
              <fo:list-item-label end-indent=""label-end()"">
                <fo:block>•</fo:block>
              </fo:list-item-label>
              <fo:list-item-body start-indent=""body-start()"">
                <fo:block>XSL-FO to PDF conversion</fo:block>
              </fo:list-item-body>
            </fo:list-item>
            <fo:list-item>
              <fo:list-item-label end-indent=""label-end()"">
                <fo:block>•</fo:block>
              </fo:list-item-label>
              <fo:list-item-body start-indent=""body-start()"">
                <fo:block>Java and .NET interoperability</fo:block>
              </fo:list-item-body>
            </fo:list-item>
            <fo:list-item>
              <fo:list-item-label end-indent=""label-end()"">
                <fo:block>•</fo:block>
              </fo:list-item-label>
              <fo:list-item-body start-indent=""body-start()"">
                <fo:block>Maven dependency management</fo:block>
              </fo:list-item-body>
            </fo:list-item>
          </fo:list-block>
        </fo:flow>
      </fo:page-sequence>
    </fo:root>";

            // Convert XSL-FO content to byte array
            byte[] foBytes = Encoding.UTF8.GetBytes(xslFoContent);
            ByteArrayInputStream foInputStream = new ByteArrayInputStream(foBytes);

            // Initialize FOP factory and user agent
            FopFactory fopFactory = FopFactory.newInstance(new java.io.File(".").toURI());
            FOUserAgent foUserAgent = fopFactory.newFOUserAgent();

            // Create output stream for the PDF file
            OutputStream outStream = new FileOutputStream("output.pdf");

            try
            {
                // Create FOP instance for PDF generation
                Fop fop = fopFactory.newFop(MimeConstants.MIME_PDF, foUserAgent, outStream);

                // Setup transformer for XSL-FO to PDF conversion
                TransformerFactory factory = TransformerFactory.newInstance();
                Transformer transformer = factory.newTransformer();

                // Setup input and output for the transformation
                Source src = new StreamSource(foInputStream);
                Result res = new SAXResult(fop.getDefaultHandler());

                // Perform the transformation
                transformer.transform(src, res);
            }
            finally
            {
                // Close the output stream
                outStream.close();
            }
        }
    }
}