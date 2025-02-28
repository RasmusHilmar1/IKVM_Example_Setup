# IKVM .NET/Java Integration Example  
**Leverage Java Libraries in .NET 4.8 or .NET 8 Using IKVM**  


![Build Status](https://img.shields.io/badge/build-passing-brightgreen) ![License](https://img.shields.io/badge/license-Apache%202.0-blue) ![Version](https://img.shields.io/badge/version-1.0.0-orange)  


## **Prerequisites**  
1. **.NET Framework 4.8** or **.NET 8 SDK** installed.  
2. **Java JDK** (for XML/XSL-FO processing with Apache FOP).  
3. **NuGet Package Manager** (for IKVM setup).  


## **Installing IKVM Packages**  
**IKVM is required to bridge Java and .NET.** Add these packages to your `.csproj`:  
### **For .NET 8 (Modern SDK-Style Projects)**  
```xml  
<ItemGroup>  
  <!-- Core IKVM Runtime -->  
  <PackageReference Include="IKVM" Version="8.11.1" />  
  <!-- Automatic Maven Dependency Resolution -->  
  <PackageReference Include="IKVM.Maven.Sdk" Version="1.9.0" />  
</ItemGroup>  
```  
### **For .NET Framework 4.8 (Legacy Projects)**  
```xml  
<ItemGroup>  
  <!-- Core IKVM Runtime (older version for compatibility) -->  
  <PackageReference Include="IKVM" Version="8.1.5717.0" />  
</ItemGroup>  
```  
- **No `IKVM.Maven.Sdk` support**: You must manually convert Java JARs to .NET DLLs using `ikvmc`.  


## **Key Differences: .NET 8 vs .NET 4.8**  
### **1. Dependency Management**  
| **Aspect**               | **.NET 8**                                      | **.NET Framework 4.8**                        |  
|--------------------------|------------------------------------------------|-----------------------------------------------|  
| **Maven Integration**    | Automatic via `IKVM.Maven.Sdk`.                | Manual: Use `ikvmc` to convert JARs to DLLs.  |  
| **Project File**         | SDK-style with `<PackageReference>`.           | Legacy `.csproj` with `<Reference>` to DLLs.  |  
| **Dependency Example**   | `<MavenReference Include="commons-lang3:3.12.0"/>` | `ikvmc -out:commons-lang3.dll commons-lang3.jar` |  
### **2. Tooling & Workflow**  
| **Aspect**               | **.NET 8**                                      | **.NET Framework 4.8**                        |  
|--------------------------|------------------------------------------------|-----------------------------------------------|  
| **Build Process**        | Maven dependencies auto-downloaded during build. | Manually convert JARs and reference DLLs.     |  
| **Cross-Platform**       | Yes (Windows, Linux, macOS).                   | Windows-only.                                 |  
| **Debugging**            | Integrated with modern .NET tools.              | Requires manual configuration in Visual Studio.|  
### **3. Compatibility**  
| **Aspect**               | **.NET 8**                                      | **.NET Framework 4.8**                        |  
|--------------------------|------------------------------------------------|-----------------------------------------------|  
| **IKVM.Maven.Sdk**       | Fully supported.                               | Not supported.                                |  
| **Java Library Support** | Latest versions (e.g., FOP 2.9).               | Older versions (e.g., FOP 2.3) recommended.   |  


## **Project Setup Guide**  
### **For .NET 8**  
1. **Add NuGet Packages**: Include `IKVM` and `IKVM.Maven.Sdk` in your `.csproj`.  
2. **Declare Maven Dependencies**:  
   ```xml  
   <ItemGroup>  
     <MavenReference Include="org.apache.commons:commons-lang3:3.12.0" />  
     <MavenReference Include="com.google.guava:guava:31.1-jre" />  
     <MavenReference Include="org.apache.xmlgraphics:fop:2.9" />  
   </ItemGroup>  
   ```  
3. **Build & Run**:  
   ```bash  
   dotnet build  
   dotnet run  
   ```  
### **For .NET Framework 4.8**  
1. **Install IKVM Tools**:  
   - Download `ikvmc.exe` from [IKVM Releases](https://www.ikvm.net/).  
2. **Convert JARs to DLLs**:  
   ```bash  
   ikvmc -out:commons-lang3.dll commons-lang3-3.12.0.jar  
   ```  
3. **Reference DLLs in Project**:  
   ```xml  
   <ItemGroup>  
     <Reference Include="commons-lang3">  
       <HintPath>path\to\commons-lang3.dll</HintPath>  
     </Reference>  
   </ItemGroup>  
   ```  
4. **Build in Visual Studio**.  


## **Usage Examples**  
### **1. Using Apache Commons Lang3**  
```csharp  
using org.apache.commons.lang3;  
  
string reversed = StringUtils.reverse("Hello, IKVM!"); // Java method in .NET  
```  
### **2. Apache FOP PDF Generation**  
```csharp  
// Java I/O classes interoperate with .NET  
OutputStream outStream = new FileOutputStream("output.pdf");  
Fop fop = fopFactory.newFop(MimeConstants.MIME_PDF, foUserAgent, outStream);  
```  


## **FAQ**  
- **Missing DLLs in .NET 4.8**: Ensure JARs are converted with `ikvmc` and referenced correctly.  
- **FOP Errors**: Verify Java JDK is installed and `JAVA_HOME` is set.  


**License**: Apache 2.0  


**Author**: Rasmus Hilmar  
