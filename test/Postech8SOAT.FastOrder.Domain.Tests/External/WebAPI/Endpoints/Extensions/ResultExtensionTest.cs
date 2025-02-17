using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;
using System.Collections.Generic;
using Microsoft.Identity.Client.Extensibility;
using Xunit;

namespace Postech8SOAT.FastOrder.WebAPI.Tests.Endpoints.Extensions
{
    public class ResultExtensionTest
    {
        [Fact]
        public void GetResult_ShouldReturnOk_WhenResultIsSuccessAndHasValue()
        {
            var value = new { Id = 1, Name = "Test" };
            var result = Result<object>.Succeed(value);

            var actionResult = result.GetResult();

            var okResult = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<object>>(actionResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(value, okResult.Value);
        }

        [Fact]
        public void GetResult_ShouldReturnProblem_WhenResultIsFailureAndNoBadRequestDetails()
        {
            var result = Result<object>.Failure(new List<AppProblemDetails>());
            
            var actionResult = result.GetResult();

            var notFoundResult = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.NotFound>(actionResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetResult_ShouldReturnBadRequest_WhenResultIsFailureAndHasBadRequestDetails()
        {
            var badRequestDetails = new AppBadRequestProblemDetails("title", "detail");
            var result = Result<object>.Failure(new List<AppProblemDetails> { badRequestDetails });

            var actionResult = result.GetResult();

            var badRequestResult = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.BadRequest<AppProblemDetails>>(actionResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal(badRequestDetails, badRequestResult.Value);
        }
    }
}