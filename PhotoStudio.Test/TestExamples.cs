using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PhotoStudio.ServicesDTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhotoStudio.Test
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class TestExamples
    {
       
        [Theory]
        [InlineData(11,13,19,31)]
        [InlineData(47, 93, 21, 87)]
        public void AllNumbersAreOdd_ExpectedTrue(int a, int b, int c, int d)
        {

            //arrange

            //act
            var result1 = Odd(a);
            var result2 = Odd(b);
            var result3 = Odd(c);
            var result4 = Odd(d);

            //asserts
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
            result4.Should().BeTrue();
        }


        //[Theory]
        //[MemberData(nameof(Numbers))]
        //public void AllNumbersAreOdd_WithMemberData_ExpectedTrue(int a, int b, int c, int d) 
        //{

        //    //arrange

        //    //act
        //    var result1 = Odd(a);
        //    var result2 = Odd(b);
        //    var result3 = Odd(c);
        //    var result4 = Odd(d);

        //    //asserts
        //    result1.Should().BeTrue();
        //    result2.Should().BeTrue();
        //    result3.Should().BeTrue();
        //    result4.Should().BeTrue();
        //}

        //[Theory]
        //[MemberData(nameof(TestDataGenerator.GetNumbersFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        //public void AllNumbersAreOdd_WithMemberDataClass_ExpectedTrue(int a, int b, int c, int d)
        //{

        //    //arrange

        //    //act
        //    var result1 = Odd(a);
        //    var result2 = Odd(b);
        //    var result3 = Odd(c);
        //    var result4 = Odd(d);

        //    //asserts
        //    result1.Should().BeTrue();
        //    result2.Should().BeTrue();
        //    result3.Should().BeTrue();
        //    result4.Should().BeTrue();
        //}

        //[Theory]
        //[MemberData(nameof(TestDataGenerator.GetPersonFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        //public void AllPersonIsMajor_WithMemberDataClass_ExpectedTrue(Person a, Person b, Person c, Person d)
        //{

        //    //arrange

        //    //act
        //    var result1 = IsMajorAge(a);
        //    var result2 = IsMajorAge(b);
        //    var result3 = IsMajorAge(c);
        //    var result4 = IsMajorAge(d);

        //    //asserts
        //    result1.Should().BeTrue();
        //    result2.Should().BeTrue();
        //    result3.Should().BeTrue();
        //    result4.Should().BeTrue();
        //}

        //[Fact]
        //public async Task GetMaterials() {

        //    //arrange
        //    var server = new WebApplicationFactory<Program>();
        //    var client = server.CreateClient();

        //    //act
        //    var  response = await client.GetAsync("https://localhost:7218/api/v1/Material");
        //    var formatResponse = JsonSerializer.Deserialize<List<MaterialDTO>>(await response.Content.ReadAsStringAsync());

        //    formatResponse?.Count().Should().BeGreaterThan(0);

        //}        


        private bool Odd(int value)
        {
            return value % 2 == 1;
        }

        //private bool IsMajorAge(Person person) {
        //    return person.Age > 16;
        //}

        //public static IEnumerable<object[]> Numbers() {

        //    yield return new object[] { 1, 5, 9, 11 };
        //    yield return new object[] { 3, 21, 81, 27 };

        //}

    }
    //public class TestDataGenerator
    //{

    //    public static IEnumerable<object[]> GetNumbersFromDataGenerator()
    //    {
    //        yield return new object[] { 5, 1, 3, 9 };
    //        yield return new object[] { 7, 1, 5, 3 };
    //    }

    //    public static IEnumerable<object[]> GetPersonFromDataGenerator()
    //    {
    //        yield return new object[]
    //        {
    //        new Person {Name = "Tribbiani", Age = 20},
    //        new Person {Name = "Gotti", Age = 58},
    //        new Person {Name = "Sopranos", Age = 18},
    //        new Person {Name = "Corleone", Age = 50}
    //        };

    //        yield return new object[]
    //        {
    //        new Person {Name = "Mancini", Age = 79},
    //        new Person {Name = "Vivaldi", Age = 21},
    //        new Person {Name = "Serpico", Age = 19},
    //        new Person {Name = "Salieri", Age = 20}
    //        };
    //    }

    // }
}
