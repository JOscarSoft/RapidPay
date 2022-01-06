using RapidPay.DAL;
using RapidPay.DL.Contracts;
using RapidPay.DL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentFeeService _paymentFeeService;

        public CardService(ICardRepository cardRepository, IPaymentRepository paymentRepository, IPaymentFeeService paymentFeeService)
        {
            _cardRepository = cardRepository;
            _paymentRepository = paymentRepository;
            _paymentFeeService = paymentFeeService;
        }

        public Card CreateCard(string holderName, decimal limitAmount)
        {
            Card newCard;
            try
            {
                newCard = new Card()
                {
                    CardHolderName = holderName,
                    LimitAmount = limitAmount,
                    Balance = limitAmount,
                    CardNumber = GenerateCardNumber(),
                    CVV = new Random().Next(100, 999),
                    ExpirationDate = DateTime.Now.AddYears(3).ToString("MM/yy"),
                };

                _cardRepository.Add(newCard);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newCard;
        }

        public async Task<CardBalanceResponse> GetCardBalance(long number)
        {
            try
            {
                var Card = await _cardRepository.GetByCardNumber(number);

                if (Card != null)
                    return new CardBalanceResponse { 
                        LimitAmount = Card.LimitAmount,
                        Balance = Card.Balance
                    };
                else
                    throw new Exception($"Card not found with number {number}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaymentResponse> NewPayment(PaymentRequest paymentRequest)
        {
            try
            {
                var Card = await _cardRepository.GetByCardNumber(paymentRequest.CardNumber);

                if (Card != null ||
                    Card.ExpirationDate != paymentRequest.ExpirationDate ||
                    Card.CVV != paymentRequest.CVV)
                {

                    var fee = await _paymentFeeService.CalculatePaymentFee();

                    if ((fee + paymentRequest.Amount) > Card.Balance)
                        throw new Exception("Not enough card balance.");

                    var payment = new Payment
                    {
                        Amount = paymentRequest.Amount,
                        Fee = fee,
                        CreditCardId = Card.Id,
                        Comments = paymentRequest.Comments,
                        Commerce = paymentRequest.Commerce,
                        CreationDate = DateTime.Now
                    };

                    _paymentRepository.Add(payment);

                    Card.Balance -= (fee + paymentRequest.Amount);
                    _cardRepository.Update(Card);

                    return new PaymentResponse
                    {
                        Amount = paymentRequest.Amount,
                        Fee = fee,
                        paymentId = payment.Id
                    };
                }
                else
                    throw new Exception("Card not found with specified Number, CVV and Expiration Date.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task PayCard(long number, decimal Amount)
        {
            try
            {
                var Card = await _cardRepository.GetByCardNumber(number);

                if (Card != null) 
                {
                    Card.Balance += Amount;
                    _cardRepository.Update(Card);
                }
                else
                    throw new Exception($"Card not found with number {number}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long GenerateCardNumber()
        {
            var random = new Random();
            int[] checkArray = new int[14];

            var cardNum = new int[15];

            for (int d = 13; d >= 0; d--)
            {
                cardNum[d] = random.Next(0, 9);
                checkArray[d] = (cardNum[d] * (((d + 1) % 2) + 1)) % 9;
            }

            cardNum[14] = (checkArray.Sum() * 9) % 10;

            var sb = new StringBuilder();

            for (int d = 0; d < 15; d++)
            {
                sb.Append(cardNum[d].ToString());
            }
            return long.Parse(sb.ToString());
        }
    }
}
