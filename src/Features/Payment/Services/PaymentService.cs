using Microsoft.Extensions.Options;

using PlcBase.Features.Payment.DTOs;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;
using PlcBase.Base.Error;
using PlcBase.Features.User.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Enums;

namespace PlcBase.Features.Payment.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _uow;
    private readonly VNPSettings _vnpSettings;

    public PaymentService(IOptions<VNPSettings> vnpSettings, IUnitOfWork uow)
    {
        _uow = uow;
        _vnpSettings = vnpSettings.Value;
    }

    public string CreatePayment(ReqUser reqUser, CreatePaymentDTO createPaymentDTO)
    {
        VNPHistory vnpHistory = new VNPHistory();
        vnpHistory.vnp_TxnRef = DateTime.UtcNow.Ticks;
        vnpHistory.vnp_OrderInfo = $"{reqUser.Id}|{DateTime.UtcNow.ToString()}";
        vnpHistory.vnp_Amount = createPaymentDTO.Amount;
        vnpHistory.vnp_TmnCode = _vnpSettings.TmnCode;
        vnpHistory.vnp_CreateDate = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

        //Build URL for VNPAY
        VNPLibrary vnpay = new VNPLibrary();
        vnpay.AddRequestData("vnp_Version", VNPLibrary.VERSION);
        vnpay.AddRequestData("vnp_Command", "pay");
        vnpay.AddRequestData("vnp_TmnCode", vnpHistory.vnp_TmnCode);
        // Must multiply by 100 to send to vnpay system
        vnpay.AddRequestData("vnp_Amount", (vnpHistory.vnp_Amount * 100).ToString());
        vnpay.AddRequestData("vnp_CreateDate", vnpHistory.vnp_CreateDate);
        vnpay.AddRequestData("vnp_CurrCode", "VND");
        vnpay.AddRequestData("vnp_Locale", "vn");
        vnpay.AddRequestData("vnp_IpAddr", "8.8.8.8");
        vnpay.AddRequestData("vnp_OrderInfo", vnpHistory.vnp_OrderInfo);
        vnpay.AddRequestData("vnp_ReturnUrl", _vnpSettings.ReturnUrl);
        vnpay.AddRequestData("vnp_TxnRef", vnpHistory.vnp_TxnRef.ToString());

        string paymentUrl = vnpay.CreateRequestUrl(
            _vnpSettings.BaseUrl,
            _vnpSettings.HashSecret,
            vnpHistory
        );
        return paymentUrl;
    }

    public async Task<bool> SubmitPayment(ReqUser reqUser, SubmitPaymentDTO submitPaymentDTO)
    {
        int userIdFromPayment = Convert.ToInt32(submitPaymentDTO.vnp_OrderInfo.Split("|")[0]);

        if (userIdFromPayment != reqUser.Id)
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_user_payment");

        if (
            submitPaymentDTO.vnp_ResponseCode != PaymentStatus.VNP_TRANSACTION_STATUS_SUCCESS
            || submitPaymentDTO.vnp_TransactionStatus
                != PaymentStatus.VNP_TRANSACTION_STATUS_SUCCESS
        )
            throw new BaseException(HttpCode.BAD_REQUEST, "payment_fail");

        UserProfileEntity userProfileDb = await _uow.UserProfile.GetProfileByAccountId(reqUser.Id);

        if (userProfileDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "user_not_found");

        userProfileDb.CurrentCredit += submitPaymentDTO.vnp_Amount / 100;

        _uow.UserProfile.Update(userProfileDb);
        return await _uow.Save();
    }
}