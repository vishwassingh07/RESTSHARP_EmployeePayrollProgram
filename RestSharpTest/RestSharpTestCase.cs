using EmployeePayroll_RESTSHARPProgram;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestSharpTest
{
    public class RestSharpTestCase
    {
        RestClient client = new RestClient("http://localhost:6000");
        [SetUp]
        public void Setup()
        {
            client = new RestClient("http://localhost:6000");
        }
        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <returns></returns>
        
        public RestResponse GetEmployeeList()
        {
            //Arrange
            //Initialize the request object with proper method and URL
            //passing rest request for employees list api using get method
            RestRequest request = new RestRequest("/employees", Method.Get);
            //Act
            //Execute the request
            RestResponse response = client.Execute(request);
            return response;
        }
        /// <summary>
        /// UC 1 : Retrieve all employees in the json database
        /// </summary>
        [Test]
        public void OnCallingGetAPI_ReturnEmployeeList()
        {
            RestResponse response = GetEmployeeList();
            //checks if the status code of response equals the employee code for the method requested
            //and checks response as okay or not
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            //convert the response object to list of employees
            //get 
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            //checking whether list is equal to count
            Assert.AreEqual(5, employeeList.Count);

            foreach (Employee employee in employeeList)
            {
                Console.WriteLine("Id: " + employee.Id + "\t" + "Name: " + employee.Name + "\t" + "Salary: " + employee.Salary);
            }
        }
    }
}