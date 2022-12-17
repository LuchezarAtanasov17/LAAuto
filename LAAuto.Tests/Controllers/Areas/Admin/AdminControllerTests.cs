using LAAuto.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LAAuto.Tests.Controllers.Areas.Admin
{
    [Category("Admin")]
    public class AdminControllerTests
    {
        [Fact]
        public void IndexAdmin_ReturnView()
        {
            #region Arrange

            #endregion

            #region Act

            var controller = new AdminController();
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
