using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace ApiChecks
{
    [TestFixture]
    public class APIChecksClass
    {
        [Test]
        public void VerifyGetAllTodoItemsReturns200()
        {
            //arrange
            var client = new RestClient("https://localhost:44367/");
            var request = new RestRequest("api/Todo", Method.Get);

            //act
            //var response = client.Execute(request).Content;
            var content = client.ExecuteAsync(request).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.OK, content.StatusCode);
        }
    }
}