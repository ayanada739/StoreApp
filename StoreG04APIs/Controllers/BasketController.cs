using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Errors;
using Store.G04.Core.Dtos.Basket;
using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;

namespace Store.G04.APIs.Controllers
{
 
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string? id)
        {
            if (id is null) return BadRequest(error: new ApiErrorResponse(statusCode: 400, message: "Invaid Id !!"));

           var basket = await _basketRepository.GetBasketAsync(id);

            if (basket is null) new CustomerBasket() { Id = id };

            return Ok(basket);

        }

        [HttpPost] //post: /api/basket
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto model)
        {
           var basket = await _basketRepository.UpdateBasketAsync ( _mapper.Map<CustomerBasket>(model));
            if (basket is null) return BadRequest(error: new ApiErrorResponse(statusCode: 400));
            return Ok(basket);

        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }

    }
}
  