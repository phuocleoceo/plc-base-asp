using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Payment.Entities;

[Table(TableName.PAYMENT_METADATA)]
public class PaymentEntity : BaseEntity
{
    [Column("vnp_txn_ref")]
    public long vnp_TxnRef { get; set; }

    [Column("vnp_order_info")]
    public string vnp_OrderInfo { get; set; }

    [Column("vnp_amount")]
    public long vnp_Amount { get; set; }

    [Column("vnp_bank_code")]
    public string vnp_BankCode { get; set; }

    [Column("vnp_bank_tran_no")]
    public string vnp_BankTranNo { get; set; }

    [Column("vnp_card_type")]
    public string vnp_CardType { get; set; }

    [Column("vnp_pay_date")]
    public long vnp_PayDate { get; set; }

    [Column("vnp_response_code")]
    public string vnp_ResponseCode { get; set; }

    [Column("vnp_tmn_code")]
    public string vnp_TmnCode { get; set; }

    [Column("vnp_transaction_no")]
    public string vnp_TransactionNo { get; set; }

    [Column("vnp_transaction_status")]
    public string vnp_TransactionStatus { get; set; }

    [Column("vnp_secure_hash")]
    public string vnp_SecureHash { get; set; }

    [Column("vnp_create_date")]
    public string vnp_CreateDate { get; set; }

    [ForeignKey(nameof(UserAccount))]
    [Column("user_id")]
    public int UserId { get; set; }
    public UserAccountEntity UserAccount { get; set; }
}
