using APIChecksWithRestSharp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiChecks
{
    public static class Helpers
    {
        public static RestRequest GetAllTodoItemsRequest()
        {
            return new RestRequest(Method.GET);
        }

        public static RestRequest GetSingleTodoItemRequest(long id)
        {
            var request = new RestRequest($"{id}", Method.GET);
            request.AddUrlSegment("id", id);
            return request;
        }

        public static RestRequest PostTodoItemRequest(TodoItem item)
        {
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(item);
            request.AddHeader("CanAccess", "true");
            return request;
        }

        public static RestRequest PutTodoItemRequest(long id, TodoItem item)
        {
            var request = new RestRequest($"{id}", Method.PUT);
            request.AddUrlSegment("id", id);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(item);
            request.AddHeader("CanAccess", "true");
            return request;
        }

        public static RestRequest DeleteTodoItemRequest(long id)
        {
            var request = new RestRequest($"{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.AddHeader("CanAccess", "true");
            return request;
        }

        public static TodoItem GetTestTodoItem(string name = "mow the lawn", bool isComplete = false, DateTime dateDue = default(DateTime))
        {
            if(dateDue == default(DateTime))
            {
                dateDue = new DateTime(2022, 12, 31);
            }
            return new TodoItem
            {
                Name = name,
                DateDue = dateDue,
                IsComplete = isComplete
            };            
        }
    }
}
