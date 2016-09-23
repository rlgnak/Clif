using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Clif.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Clif.Tests
{
    public class DefaultCommandModuleResolverTests
    {
        [Fact]
        public void Should_detect_classes_that_extend_CommandModule()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;
                using Clif;                

                namespace Test
                {
                    public class TestCommandModule : CommandModule
                    {
                        
                    }
                }"
            );

            var assembly = CreateTestAssembly(new List<SyntaxTree> {syntaxTree});

            var mockAssemblyCatalog = new Mock<IAssemblyCatalog>();
            mockAssemblyCatalog.Setup(x => x.GetAssemblies()).Returns(() => new[] {assembly});
            var serviceProvider = new ServiceCollection().BuildServiceProvider();

            var defaultCommandModuleResolver = new DefaultCommandModuleResolver(serviceProvider, mockAssemblyCatalog.Object);

            var commandModules = defaultCommandModuleResolver.GetCommandModules();

            Assert.Equal(1, commandModules.Count());
        }

        [Fact]
        public void Should_not_detect_abstract_classes_that_extend_CommandModule()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;
                using Clif;                

                namespace Test
                {
                    public abstract class TestCommandModule : CommandModule
                    {
                        
                    }
                }"
            );

            var assembly = CreateTestAssembly(new List<SyntaxTree> { syntaxTree });

            var mockAssemblyCatalog = new Mock<IAssemblyCatalog>();
            mockAssemblyCatalog.Setup(x => x.GetAssemblies()).Returns(() => new[] { assembly });
            var serviceProvider = new ServiceCollection().BuildServiceProvider();

            var defaultCommandModuleResolver = new DefaultCommandModuleResolver(serviceProvider, mockAssemblyCatalog.Object);

            var commandModules = defaultCommandModuleResolver.GetCommandModules();

            Assert.Equal(0, commandModules.Count());
        }

        public static Assembly CreateTestAssembly(List<SyntaxTree> syntaxTrees)
        {
            var systemAssemblyLocation = typeof(object).GetTypeInfo().Assembly.Location;
            var systemDirectory = Directory.GetParent(systemAssemblyLocation);

            var assemblyName = Path.GetRandomFileName();
            var metadataReferences = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(systemAssemblyLocation),
                MetadataReference.CreateFromFile(typeof(CommandModule).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(systemDirectory.FullName + Path.DirectorySeparatorChar + "mscorlib.dll"),
                MetadataReference.CreateFromFile(systemDirectory.FullName + Path.DirectorySeparatorChar + "System.Runtime.dll")
            };

            var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

            var compilation = CSharpCompilation.Create(assemblyName, syntaxTrees, metadataReferences, compilationOptions);

            using (var assemblyStream = new MemoryStream())
            {
                var result = compilation.Emit(assemblyStream);

                if (!result.Success)
                {
                    throw new Exception(string.Join("\n", result.Diagnostics.Select(x => x.GetMessage())));
                }

                assemblyStream.Seek(0, SeekOrigin.Begin);
                return LoadAssembly(assemblyStream);
            }
        }

        public static Assembly LoadAssembly(MemoryStream assemblyStream)
        {
            var assembly =
#if NET451
                Assembly.Load(assemblyStream.ToArray());
#else
                System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromStream(assemblyStream);
#endif
            return assembly;
        }

    }
}