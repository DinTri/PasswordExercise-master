using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordExercise.Contracts;

namespace PasswordExercise.Test
{
    [TestClass]
    public class PasswordGeneratorTests
    {
        
        private static IContainer Container { get; set; }

        public PasswordGeneratorTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PasswordGenerator>().As<IPasswordGenerator>();
            Container = builder.Build();
        }

        [TestMethod]
        public void TestGenerateLengthOnly()
        {
            using (Container.BeginLifetimeScope())
            {

                var passwordGenerator = Container.Resolve<IPasswordGenerator>();
                string result = passwordGenerator.GeneratePassword(new PasswordRequirements()
                {
                    MaxLength = 16,
                    MinLength = 8,
                });

                Assert.IsTrue(result.Length >= 8 && result.Length <= 16);
            }
        }

        [TestMethod]
        public void TestGenerateAllRequirements()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var passwordGenerator = scope.Resolve<IPasswordGenerator>();
                string result = passwordGenerator.GeneratePassword(new PasswordRequirements()
                {
                    MaxLength = 16,
                    MinLength = 8,
                    MinLowerAlphaChars = 1,
                    MinUpperAlphaChars = 1,
                    MinNumericChars = 1,
                    MinSpecialChars = 1
                });

                Assert.IsTrue(result.Length >= 8 && result.Length <= 16);
                Assert.IsTrue(result.Any(char.IsUpper));
                Assert.IsTrue(result.Any(char.IsLower));
                Assert.IsTrue(result.Any(char.IsNumber));
                Assert.IsTrue(result.Any(char.IsSymbol) || result.Any(char.IsPunctuation));
            }
           
        }

        [TestMethod]
        public void TestGenerateAllRequirments_Multiple()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var passwordGenerator = scope.Resolve<IPasswordGenerator>();
                string result = passwordGenerator.GeneratePassword(new PasswordRequirements()
                {
                    MaxLength = 8,
                    MinLength = 8,
                    MinLowerAlphaChars = 2,
                    MinUpperAlphaChars = 2,
                    MinNumericChars = 2,
                    MinSpecialChars = 2
                });

                Assert.IsTrue(result.Length == 8);
                Assert.IsTrue(result.Where(char.IsUpper).Count() == 2);
                Assert.IsTrue(result.Where(char.IsLower).Count() == 2);
                Assert.IsTrue(result.Where(char.IsNumber).Count() == 2);

                int countSpecial = result.Count(char.IsSymbol) + result.Count(char.IsPunctuation);
                Assert.IsTrue(countSpecial == 2);
            }
            
        }
        [TestMethod]
        public void TestGenerateAllRequirments_ThreeAlphaUpperOneNumericTwoSpecial()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var passwordGenerator = scope.Resolve<IPasswordGenerator>();
                string result = passwordGenerator.GeneratePassword(new PasswordRequirements()
                {
                    MaxLength = 6,
                    MinLength = 5,
                   
                    MinUpperAlphaChars = 3,
                    MinNumericChars = 1,
                    MinSpecialChars = 2
                });

                Assert.IsTrue(result.Length == 6);
                Assert.IsTrue(result.Where(char.IsUpper).Count() == 3);
                Assert.IsTrue(result.Where(char.IsNumber).Count() == 1);

                int countSpecial = result.Count(char.IsSymbol) + result.Count(char.IsPunctuation);
                Assert.IsTrue(countSpecial == 2);
            }

        }

        [TestMethod]
        public void TestGenerateAllRequirments_FourAlphaLowerOneSpecialTwoNumeric()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var passwordGenerator = scope.Resolve<IPasswordGenerator>();
                string result = passwordGenerator.GeneratePassword(new PasswordRequirements()
                {
                    MaxLength = 7,
                    MinLength = 7,
                    MinLowerAlphaChars = 4,
                    MinNumericChars = 2,
                    MinSpecialChars = 1
                });

                Assert.IsTrue(result.Length == 7);
                Assert.IsTrue(result.Where(char.IsLower).Count() == 4);
                Assert.IsTrue(result.Where(char.IsNumber).Count() == 2);

                int countSpecial = result.Count(char.IsSymbol) + result.Count(char.IsPunctuation);
                Assert.IsTrue(countSpecial == 1);
            }

        }
        [TestMethod]
        public void TestGenerateAllRequirments_FourAlphaLowerOneNumeric()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var passwordGenerator = scope.Resolve<IPasswordGenerator>();
                string result = passwordGenerator.GeneratePassword(new PasswordRequirements()
                {
                    MaxLength = 5,
                    MinLength = 5,
                    MinLowerAlphaChars = 4,
                    MinNumericChars = 1
                });

                Assert.IsTrue(result.Length == 5);
                Assert.IsTrue(result.Where(char.IsLower).Count() == 4);
                Assert.IsTrue(result.Where(char.IsNumber).Count() == 1);
            }

        }
    }
   
}
