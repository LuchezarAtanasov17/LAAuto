using LAAuto.Services;
using LAAuto.Web.Controllers;
using LAAuto.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.ComponentModel;

namespace LAAuto.Tests.Controllers
{
    [Category("Home")]
    public class HomeControllerTests
    {
        private readonly MockRepository _mockRepository;

        public HomeControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
        }

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

        [Fact]
        public void ObjectNotFoundException_ReturnView()
        {
            #region Arrange

            var featureCollectionMock = _mockRepository.Create<IFeatureCollection>();

            featureCollectionMock.Setup(x => x.Get<IExceptionHandlerFeature>())
                .Returns(new ExceptionHandlerFeature
                {
                    Path = "/",
                    Error = new ObjectNotFoundException("Test Message")
                });

            var httpContextMock = _mockRepository.Create<HttpContext>();
            httpContextMock.Setup(x => x.Features).Returns(featureCollectionMock.Object);

            #endregion

            #region Act

            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));
            var controller = new HomeController()
            {
                ControllerContext = context
            };

            var result = controller.Error();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            var errorViewModelResult = expected.Model as ErrorViewModel;

            Assert.NotNull(errorViewModelResult);

            Assert.Equal(404, errorViewModelResult.StatusCode);
            Assert.Equal("Test Message", errorViewModelResult.Message);

            #endregion
        }

        [Fact]
        public void Exception_ReturnView()
        {
            #region Arrange

            var featureCollectionMock = _mockRepository.Create<IFeatureCollection>();

            featureCollectionMock.Setup(x => x.Get<IExceptionHandlerFeature>())
                .Returns(new ExceptionHandlerFeature
                {
                    Path = "/",
                    Error = new Exception("Test Message")
                });

            var httpContextMock = _mockRepository.Create<HttpContext>();
            httpContextMock.Setup(x => x.Features).Returns(featureCollectionMock.Object);

            #endregion

            #region Act

            var context = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));
            var controller = new HomeController()
            {
                ControllerContext = context
            };

            var result = controller.Error();

            #endregion

            #region Assert

            Assert.NotNull(result);

            var expected = result as ViewResult;

            Assert.NotNull(expected);

            var errorViewModelResult = expected.Model as ErrorViewModel;

            Assert.NotNull(errorViewModelResult);

            Assert.Equal(500, errorViewModelResult.StatusCode);
            Assert.Equal("Test Message", errorViewModelResult.Message);

            #endregion
        }
    }
}