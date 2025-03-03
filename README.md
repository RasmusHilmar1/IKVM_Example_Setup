# IKVM .NET/Java Integration Example
**Leverage Java Libraries in .NET 4.8 or .NET 8 Using IKVM**  

![Build Status](https://img.shields.io/badge/build-passing-brightgreen) ![License](https://img.shields.io/badge/license-Apache%202.0-blue) ![Version](https://img.shields.io/badge/version-1.0.0-orange)  

---

## **Installing IKVM Packages**
IKVM is required to bridge Java and .NET. You can add the following packages to your project:

### **.NET 4.8 or .NET 8 (SDK-Style Projects)**
```xml
<ItemGroup>
  <!-- Core IKVM Runtime -->
  <PackageReference Include="IKVM" Version="8.x.x" />
  
  <!-- Automatic Maven Dependency Resolution for Java -->
  <PackageReference Include="IKVM.Maven.Sdk" Version="1.x.x" />
</ItemGroup>
```
> **Tip**  
> For older .NET Framework projects, ensure your `.csproj` is **SDK-style**. You can convert a legacy `.csproj` to SDK-style so Maven references work seamlessly.

## **Prerequisites**
1. **.NET Framework 4.8** or **.NET 8 SDK** installed.  
2. **Java JDK** (for XML/XSL-FO processing with Apache FOP).  
3. **NuGet Package Manager** (for IKVM setup).  

> **Note**  
> IKVM enables .NET projects to use Java libraries by automatically downloading Maven artifacts and converting them into .NET assemblies at build time.

## **Adding Maven References**
Once `IKVM.Maven.Sdk` is added, you can reference Maven artifacts directly in your `.csproj`:

```xml
<ItemGroup>
  <MavenReference Include="org.apache.commons:commons-lang3:3.12.0" />
  <MavenReference Include="org.apache.xmlgraphics:fop:2.9" />
</ItemGroup>
```

Upon build, IKVM automatically:
1. Downloads specified Java JARs from Maven Central (or other Maven repositories).
2. Converts them to .NET assemblies.
3. Integrates them into your project.

No manual `ikvmc` step is needed!

## **Key Differences: .NET 8 vs .NET 4.8**
Both .NET 8 and .NET 4.8 support automatic Maven-based Java library resolution with IKVM. Below is a quick rundown of considerations:

| **Aspect**               | **.NET 8**                                      | **.NET Framework 4.8**                      |
|--------------------------|------------------------------------------------|---------------------------------------------|
| **Project File**         | Modern SDK-style `<Project Sdk="Microsoft.NET.Sdk">`.  | Also uses SDK-style format for Maven refs.  |
| **Maven Integration**    | `<MavenReference>` via `IKVM.Maven.Sdk`.       | Same approach—no manual JAR-to-DLL needed.  |
| **Platform Support**     | Cross-platform (Windows, Linux, macOS).        | Windows-only.                               |
| **Debugging**            | Fully integrated with modern .NET tooling.     | Works in Visual Studio (Windows).           |

## **Project Setup Guide**

### **Step-by-Step**

1. **Add NuGet Packages**  
   - Install packages: `IKVM` and `IKVM.Maven.Sdk`.  
   - Example (Package Manager Console):  
     ```powershell
     PM> Install-Package IKVM
     PM> Install-Package IKVM.Maven.Sdk
     ```
2. **Declare Maven Dependencies**  
   ```xml
   <ItemGroup>
     <MavenReference Include="org.apache.commons:commons-lang3:3.12.0" />
     <MavenReference Include="org.apache.xmlgraphics:fop:2.9" />
   </ItemGroup>
   ```
3. **Build & Run**  
   - **.NET 8**:  
     ```bash
     dotnet build
     dotnet run
     ```  
   - **.NET 4.8**:  
     - Build your solution in Visual Studio or use MSBuild:
       ```bash
       msbuild YourProject.csproj /t:Build
       ```  
     - Run your `.exe` from the output directory.

## **Example Usage: Apache FOP PDF Generation**

Below is a small snippet demonstrating Apache FOP usage in C# via IKVM:

```csharp
using System;
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
            using var output = new FileOutputStream("out.pdf");
            var fopFactory = FopFactory.newInstance(new File(".").toURI());
            var userAgent = fopFactory.newFOUserAgent();
            var fop = fopFactory.newFop(MimeConstants.MIME_PDF, userAgent, output);

            // Transform .fo + .xml into PDF
            var factory = TransformerFactory.newInstance();
            var transformer = factory.newTransformer(new StreamSource("example.fo"));
            var source = new StreamSource("example.xml");
            var saxResult = new SAXResult(fop.getDefaultHandler());
            transformer.transform(source, saxResult);
        }
    }
}
```

### **Call the Method**
```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Generating PDF...");
        PdfGenerator.GeneratePdf();
        Console.WriteLine("PDF generated: out.pdf");
    }
}
```
> **Note**  
> Ensure that `example.fo` and `example.xml` exist in the working directory. Paths can be absolute or relative.

## **FAQ**

**1. Can I still convert JARs to DLLs manually?**  
Yes, by using `ikvmc`. However, you don’t need to if you’ve got `IKVM.Maven.Sdk`. Automatic Maven references are usually easier.

**2. Which Java versions are supported?**  
IKVM primarily targets Java 8 class files (Java 1.8). Libraries compiled with newer Java versions may not be fully supported.

**3. Do I need to ship the IKVM runtime?**  
Yes, your final build output will include IKVM runtime assemblies (e.g., `IKVM.OpenJDK.Core.dll`) in order to run Java classes under .NET.

## **License**
[Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0)  

**Author**: Rasmus Hilmar  

---

## **Contributing**
We welcome contributions! Please read our [Contributing Guidelines](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests.

## **Support**
If you encounter any issues, please raise them in the [Issue Tracker](https://github.com/your-repo/issues). For direct support, reach out via email at support@example.com.
