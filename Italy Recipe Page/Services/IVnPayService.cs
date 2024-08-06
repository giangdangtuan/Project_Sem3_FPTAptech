using Italy_Recipe_Page.Models.ViewModels;

namespace Italy_Recipe_Page.Services
{
    public interface IVnPayService
    {
        string CreatePaymentURL(HttpContext context, VnPaymentRequestModel model);

        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
