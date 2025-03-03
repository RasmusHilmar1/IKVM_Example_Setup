using java.io;
using javax.xml.transform;
using javax.xml.transform.sax;
using javax.xml.transform.stream;
using org.apache.fop.apps;

namespace IKVMExample
{
    public static class PdfGenerator
    {
        public static void GeneratePdf()
        {
            // Make sure these files exist or paths are correct
            var outputStream = new FileOutputStream("out.pdf");
            var fopFactory = FopFactory.newInstance(new File(".").toURI());
            var userAgent = fopFactory.newFOUserAgent();
            var fop = fopFactory.newFop(MimeConstants.MIME_PDF, userAgent, outputStream);

            var transformerFactory = TransformerFactory.newInstance();
            var transformer = transformerFactory.newTransformer(new StreamSource(new File("align.fo")));

            // Example parameter, adjust as needed
            transformer.setParameter("version", "1.0");

            var xmlSource = new StreamSource(new File("align.xml"));
            var result = new SAXResult(fop.getDefaultHandler());

            // Perform the transformation
            transformer.transform(xmlSource, result);

            // Cleanup / dispose output if necessary
            outputStream.close();
        }
    }
}
