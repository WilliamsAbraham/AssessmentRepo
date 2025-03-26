using CustomerServiceApi.Core.Application.Features.Banks;
using CustomerServiceApi.Core.Application.Interfaces;
using CustomerServiceApi.Core.Application.Response;
using HobaxHrApi.Host.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace Api.Product.Controllers
{
    public class CustomerOnBoardingController : BaseApiController
    {
        private readonly IBankServcie _banServcie;
        public CustomerOnBoardingController(IBankServcie bankServcie, ILogger<CustomerOnBoardingController> @object)
        {
            _banServcie = bankServcie;
        }

        [HttpGet("getBanks")]
        public async Task<ActionResult<ApiResponse<BankResponse>>> GetBanks()
        {
            return await HandleApiOperationAsync(async () =>
            {
                var response = await _banServcie.GetBanksAsync();

                return ApiResponseFactory.CreateSuccessResponse(response);
            });
        }



    }
}
