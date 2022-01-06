using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Api.ApiModels;
using RapidPay.DL.Contracts;
using RapidPay.DL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public IActionResult CreateCard([FromBody] CreateCardModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.HolderName))
                    throw new Exception("Card holder name is needed.");

                if (model.LimitAmount <= 0)
                    throw new Exception("Enter a valid limit.");

                var newCard = _cardService.CreateCard(model.HolderName, model.LimitAmount);
                return Ok(newCard);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{cardNumber}/balance")]
        public async Task<IActionResult> Get(long cardNumber)
        {
            try
            {
                var result = await _cardService.GetCardBalance(cardNumber);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("newpayment")]
        public async Task<IActionResult> NewPayment([FromBody] PaymentRequest model)
        {
            try
            {
                if (model.CardNumber.ToString().Length != 15)
                    throw new Exception("Please enter a valid card number.");

                if (model.CVV.ToString().Length != 3)
                    throw new Exception("Please enter a valid CVV number.");

                if(string.IsNullOrEmpty(model.ExpirationDate))
                    throw new Exception("Card expiration date is needed.");


                var result = await _cardService.NewPayment(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("pay")]
        public IActionResult PayCard([FromBody] PayCardModel model)
        {
            try
            {
                if (model.Number.ToString().Length != 15)
                    throw new Exception("Please enter a valid card number.");

                _cardService.PayCard(model.Number, model.Amount).Wait();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
