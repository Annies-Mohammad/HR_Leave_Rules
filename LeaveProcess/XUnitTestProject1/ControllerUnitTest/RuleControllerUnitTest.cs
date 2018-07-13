using LeaveProcess.Models.DTO;
using LeaveProcess.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RuleProcess.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestProject1.ControllerUnitTest
{
    public class RuleControllerUnitTest
    {
        #region Private fields
        Mock<IRuleOperations> _mockRuleOperations = new Mock<IRuleOperations>();
        RuleController _RuleController;

        #endregion

        #region Constructor
        public RuleControllerUnitTest()
        {
            _RuleController = new RuleController(_mockRuleOperations.Object);
        }
        #endregion

        #region Test methods
        [Fact]
        public void Constructor_Test()
        {
            RuleController controller = new RuleController(_mockRuleOperations.Object);
            Assert.NotNull(controller);
        }

        [Fact]
        public void Get_Rules_NoRecords_Test()
        {
            _mockRuleOperations.Setup(x => x.GetRules()).Returns(new List<Rule>());

            IActionResult result = _RuleController.GetRules(11);
            Assert.True(result.GetType() == typeof(JsonResult));
            Assert.NotEmpty(((JsonResult)result).Value.ToString());
            Assert.True((result as JsonResult).Value.ToString() == "No Rules found");
        }

        [Fact]
        public void Get_Rules_Exception_Test()
        {
            _mockRuleOperations.Setup(x => x.GetRules()).Throws(new Exception("Could not connect to DB"));

            IActionResult result = _RuleController.GetRules(1);
            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((result as BadRequestObjectResult).Value.ToString() == "Could not connect to DB");
        }

        [Fact]
        public void Get_Rules_Test()
        {
            IList<Rule> list = new List<Rule>();
            list.Add(new Rule { Id = 1, RuleDescription= "RuleDescription1", ClientId=11});
            list.Add(new Rule { Id = 2, RuleDescription = "RuleDescription2",ClientId = 22 });
            list.Add(new Rule { Id = 3, RuleDescription = "RuleDescription3",ClientId = 33 });
            _mockRuleOperations.Setup(x => x.GetRules()).Returns(list);

            IActionResult result = _RuleController.GetRules(3);
            Assert.True(result.GetType() == typeof(JsonResult));
            Assert.True(((result as JsonResult).Value as IOrderedEnumerable<Rule>).Count() == 3);
            Assert.True(((result as JsonResult).Value as IOrderedEnumerable<Rule>).First().RuleDescription == "RuleDescription3");
        }

        [Fact]
        public void Add_Rules_Null_Input_Test()
        {
            IActionResult result = _RuleController.AddRule(null);
            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
        }

        [Fact]
        public void Add_Rules_Invalid_Input_Test()
        {
            Rule Rule = new Rule();
            MimicModelStateValidation(_RuleController, Rule);

            IActionResult result = _RuleController.AddRule(Rule);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((((BadRequestObjectResult)result).Value as SerializableError).Count() == 3);
            string[] error = (((BadRequestObjectResult)result).Value as SerializableError)["ClientId"] as string[];
            Assert.True(error[0] == "ClientId should not be empty");

            error = (((BadRequestObjectResult)result).Value as SerializableError)["RuleDescription"] as string[];
            Assert.True(error[0] == "RuleDescription should not be empty");

          
        }

        [Fact]
        public void Add_Rules_Null_RuleDescriptionInput_Test()
        {
            Rule Rule = new Rule();
            
            Rule.ClientId = 1;
            MimicModelStateValidation(_RuleController, Rule);

            IActionResult result = _RuleController.AddRule(Rule);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((((BadRequestObjectResult)result).Value as SerializableError).Count() == 1);
            string[] error = (((BadRequestObjectResult)result).Value as SerializableError)["RuleDescription"] as string[];
            Assert.True(error[0] == "RuleDescription should not be empty");
        }

        [Fact]
        public void Add_Rules_Null_ClientIdInput_Test()
        {
            Rule Rule = new Rule();
            Rule.RuleDescription = "test";
            
            MimicModelStateValidation(_RuleController, Rule);

            IActionResult result = _RuleController.AddRule(Rule);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((((BadRequestObjectResult)result).Value as SerializableError).Count() == 1);
            string[] error = (((BadRequestObjectResult)result).Value as SerializableError)["ClientId"] as string[];
            Assert.True(error[0] == "ClientId should not be empty");
        }

        

        [Fact]
        public void Add_Rules_Failure_Test()
        {
            Rule Rule = new Rule();
            Rule.RuleDescription = "test";
            Rule.ClientId= 1;
            

            _mockRuleOperations.Setup(x => x.AddRule(Rule)).Returns(false);

            IActionResult result = _RuleController.AddRule(Rule);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True(((BadRequestObjectResult)result).Value.ToString() == "Rule creation failed!");
        }

        [Fact]
        public void Add_Rules_Success_Test()
        {
            Rule Rule = new Rule();
            Rule.RuleDescription = "test";
            Rule.ClientId= 1;
            

            _mockRuleOperations.Setup(x => x.AddRule(Rule)).Returns(true);

            IActionResult result = _RuleController.AddRule(Rule);

            Assert.True(result.GetType() == typeof(JsonResult));
            Assert.True(((JsonResult)result).Value.ToString() == "Rule created successfully");
        }

        [Fact]
        public void Add_Rules_Exception_Test()
        {
            Rule Rule = new Rule();
            Rule.RuleDescription = "test";
            Rule.ClientId = 1;

            _mockRuleOperations.Setup(x => x.AddRule(Rule)).Throws(new Exception("DB Connection could not be established"));

            IActionResult result = _RuleController.AddRule(Rule);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True(((BadRequestObjectResult)result).Value.ToString() == "DB Connection could not be established");
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Mimics the model state validation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">The control.</param>
        /// <param name="clazz">The clazz.</param>
        void MimicModelStateValidation<T>(Controller control, T clazz) where T : class
        {
            var validationContext = new ValidationContext(clazz, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(clazz, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                control.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }

        #endregion
    }
}
