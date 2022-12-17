using LAAuto.Web.Controllers;
using LAAuto.Web.Models;
using LAAuto.Web.Models.Appointments;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LAAuto.Tests.Controllers
{
    [Category("Home")]
    public class HomeControllerTests
    {
        [Fact]
        public void IndexHome_ReturnView()
        {
            #region Arrange

            #endregion

            #region Act

            var controller = new HomeController();
            var result = controller.Index();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            #endregion
        }
    }
}
