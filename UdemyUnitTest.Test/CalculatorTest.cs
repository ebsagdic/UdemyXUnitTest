using Moq;
using System;
using UdemyUnitTest.APP;
using Xunit;

namespace UdemyUnitTest.Test
{
    public class CalculatorTest
    {
        public Calculator calculator { get; set; }

        public Mock<ICalculatorService> mymock { get; set; }
        public CalculatorTest()
        {
            mymock = new Mock<ICalculatorService>();
            this.calculator = new Calculator(mymock.Object);
            //this.calculator = null;
        }
        //Bu constracture ile her methodda calculater tanıtmana gerek yok
        //------------------------
        //methoda bu şekilde [Fact] ile başlandığı zaman parametre almadığını ve test methodu olduğu anlaşılır.
        //[Fact]  
        //public void AddTest()
        //{
        //    ////Arrange
        //    //int a = 5;
        //    //int b = 20;
        //    //var calculator = new Calculator();
        //    ////Act
        //    //var total = calculator.Add(a, b);
        //    ////Assert
        //    //Assert.Equal<int>(25, total);
        //    //--------------------

        //    //var names = new List<string>() {"KARAM","BEDİRHAN","RECEP"};
        //    //Assert.DoesNotContain("EMRE", "EMRE SAĞDIÇ");
        //    //Assert.Contains(names, x => x == "ÖMER");
        //    //--------------------
        //    //Assert.True("".GetType() == typeof(string));
        //    //--------------------

        //    //var regEx = "^dog";
        //    //Assert.Matches(regEx, "dog Bedo");
        //    //Matches ve DoesNotMatch regex alır , regex de regular example anlamına gelir.
        //    //--------------------
        //    //Assert.StartsWith("Aga", "Aga naptın ya");
        //    //StartsWith ve EndWith vardır, Verilen ile başladığını ya da bittiğini kontrol eder
        //    //--------------------

        //    //Assert.NotEmpty(new List<string> { "Quaresma" });
        //    //Empty hali de mevcuttur, bir dizin alır, dizin boş mu dolu mu kontrol eder
        //    //--------------------

        //    //Assert.InRange(10, 2, 20);
        //    //Bir de NotInRange hali vardır, ilk verilen data, 2.ve 3. arasında mı değil mi kontrolü yapar
        //    //--------------------

        //    //Assert.Single(new List<string> { "fatih" });
        //    //Assert.Single<int>(new List<int> { 1, 2, 3 });
        //    //Üstteki method generic ve tip güvenlidir, IEnumerable<T> ise, .NET Framework içindeki bir generic arayüz (interface) türüdür.
        //    //Bu iki methodda içerisindeki dizinde bir tane veri tutup tutmadığını kontrol eder
        //    //--------------------

        //    //Assert.IsType<int>(1);
        //    //Assert.IsNotType<string>(2);
        //    //değişken türü ve verilenin dürü eşleşme durumu kontrol eder
        //    //--------------------

        //    //Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>());
        //    //Assert.IsAssignableFrom<IEnumerable<Object>>(5);
        //    //Miras alıp alamayacağı için test edilebilir
        //    //--------------------

        //    //Assert.NotNull(null);
        //    //string deger = "Osman";
        //    //Assert.Null(deger);
        //    //null olup olmadığı kontrolünü yapar
        //    //--------------------
        //}
        //[Theory]
        //[InlineData(1,2,3)]
        //[InlineData(5,3,8)]
        //public void AddTest2(int a, int b, int ExpectedTotal)
        //{
        //
        //    var total = calculator.Add(a, b);
        //
        //    Assert.Equal(ExpectedTotal, total);
        //
        //}
        //------------------------

        //bundan sonraki methodlar Calculator methoduna koşul eklendikten sonra yazılmıştır
        [Theory]
        [InlineData(5,0,0)]
        [InlineData(0,10,0)]
        public void Add_ZeroValues_ReturnZeroValue(int a, int b, int exceptioanlTotal) 
        {
            var total = calculator.Add(a, b);
            mymock.Setup(x=>x.Add(a,b)).Returns(exceptioanlTotal);
            Assert.Equal(exceptioanlTotal, total);
        }
        [Theory]
        [InlineData(5, 10, 50)]
        public void Multip_SimpleValues_ReturnExceptedValue(int a,int b ,int exceptioanlValue)
        {
            mymock.Setup(x=>x.Multip(a,b)).Returns(exceptioanlValue);
            Assert.Equal(50, calculator.Multip(a, b));
            mymock.Verify(x=>x.Multip(a, b),Times.Once);
            //Bircok Times.x türevi verify komutu vardır

        }

        [Theory]
        [InlineData(0, 10)]
        public void Multip_ZeroValues_ReturnException(int a, int b)
        {
            mymock.Setup(x => x.Multip(a, b)).Throws(new Exception("a=0 olamaz"));
            Exception exception = Assert.Throws<Exception>(() => calculator.Multip(a, b));
            Assert.Equal("a=0 olamaz", exception.Message);
            

        }

        [Theory]
        [InlineData(5, 10, 50)]
        public void Multip_SimpleValues_ReturnMultipValue(int a, int b, int exceptioanlValue)
        {
            int actualmultip = 0;
            mymock.Setup(x => x.Multip(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>((x, y) => actualmultip = x * y);
            mymock.Setup(x => x.Multip(a, b)).Callback(() => Console.WriteLine("Method çalıştı"));
            
            calculator.Multip(a, b);
            Assert.Equal(exceptioanlValue, actualmultip);

            calculator.Multip(5, 20);
            Assert.Equal(100,actualmultip);
           

        }


    }

}
