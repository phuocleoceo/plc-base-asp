using Microsoft.Extensions.Options;
using AutoMapper;

using PlcBase.Features.Payment.Entities;
using PlcBase.Features.User.Entities;
using PlcBase.Features.Payment.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Utilities;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;
using PlcBase.Shared.Enums;
using PlcBase.Base.Error;

namespace PlcBase.Features.Payment.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly VNPSettings _vnpSettings;

    public PaymentService(IOptions<VNPSettings> vnpSettings, IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
        _vnpSettings = vnpSettings.Value;
    }

    public async Task<string> CreatePayment(ReqUser reqUser, CreatePaymentDTO createPaymentDTO)
    {
        try
        {
            await _uow.CreateTransaction();

            VNPHistory vnpHistory = new VNPHistory();
            vnpHistory.vnp_TxnRef = TimeUtility.Now().Ticks;
            vnpHistory.vnp_OrderInfo = $"{reqUser.Id}|{vnpHistory.vnp_TxnRef}";
            // Must multiply by 100 to send to vnpay system
            vnpHistory.vnp_Amount = createPaymentDTO.Amount * 100;
            vnpHistory.vnp_TmnCode = _vnpSettings.TmnCode;
            vnpHistory.vnp_CreateDate = TimeUtility.GetDateTimeFormatted(
                TimeUtility.Now(),
                "yyyyMMddHHmmss"
            );

            //Build URL for VNPAY
            VNPLibrary vnpay = new VNPLibrary();
            vnpay.AddRequestData("vnp_Version", VNPLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", _vnpSettings.Command);
            vnpay.AddRequestData("vnp_TmnCode", vnpHistory.vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", vnpHistory.vnp_Amount.ToString());
            vnpay.AddRequestData("vnp_CreateDate", vnpHistory.vnp_CreateDate);
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_IpAddr", "8.8.8.8");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_OrderInfo", vnpHistory.vnp_OrderInfo);
            vnpay.AddRequestData("vnp_ReturnUrl", _vnpSettings.ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", vnpHistory.vnp_TxnRef.ToString());

            string paymentUrl = vnpay.CreateRequestUrl(
                _vnpSettings.BaseUrl,
                _vnpSettings.HashSecret,
                vnpHistory
            );

            PaymentEntity paymentEntity = _mapper.Map<PaymentEntity>(vnpHistory);
            paymentEntity.UserId = reqUser.Id;

            _uow.Payment.Add(paymentEntity);
            await _uow.Save();

            await _uow.CommitTransaction();
            return paymentUrl;
        }
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }

    public async Task<bool> SubmitPayment(ReqUser reqUser, SubmitPaymentDTO submitPaymentDTO)
    {
        try
        {
            await _uow.CreateTransaction();

            // Check payment status
            if (
                submitPaymentDTO.vnp_ResponseCode != PaymentStatus.VNP_TRANSACTION_STATUS_SUCCESS
                || submitPaymentDTO.vnp_TransactionStatus
                    != PaymentStatus.VNP_TRANSACTION_STATUS_SUCCESS
            )
                throw new BaseException(HttpCode.BAD_REQUEST, "payment_fail");

            // Check signature and update payment metadata
            // PaymentEntity paymentEntity = await _uow.Payment.GetByTxnRef(
            //     reqUser.Id,
            //     submitPaymentDTO.vnp_TxnRef
            // );

            // if (paymentEntity.vnp_SecureHash != submitPaymentDTO.vnp_SecureHash)
            //     throw new BaseException(HttpCode.BAD_REQUEST, "invalid_payment_secure_hash");

            long txnRef = Convert.ToInt64(submitPaymentDTO.vnp_OrderInfo.Split("|")[1]);
            PaymentEntity paymentEntity = await _uow.Payment.GetByTxnRef(reqUser.Id, txnRef);

            if (paymentEntity.vnp_TransactionStatus == PaymentStatus.VNP_TRANSACTION_STATUS_SUCCESS)
                throw new BaseException(HttpCode.BAD_REQUEST, "payment_already_handled");

            _mapper.Map(submitPaymentDTO, paymentEntity);
            paymentEntity.vnp_TransactionStatus = PaymentStatus.VNP_TRANSACTION_STATUS_SUCCESS;
            paymentEntity.vnp_TxnRef = txnRef;
            _uow.Payment.Update(paymentEntity);
            await _uow.Save();

            // Update user credit
            UserProfileEntity userProfileDb = await _uow.UserProfile.GetProfileByAccountId(
                reqUser.Id
            );

            if (userProfileDb == null)
                throw new BaseException(HttpCode.NOT_FOUND, "user_not_found");

            userProfileDb.CurrentCredit += submitPaymentDTO.vnp_Amount / 100;
            _uow.UserProfile.Update(userProfileDb);
            await _uow.Save();

            await _uow.CommitTransaction();
            return true;
        }
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }
}
