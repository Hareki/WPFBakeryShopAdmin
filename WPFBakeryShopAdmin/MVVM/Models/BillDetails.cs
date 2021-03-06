using System;
using System.Collections.Generic;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.Models
{
    public class Detail
    {
        public string ProductName { get; set; }
        public string TypeName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string SubTotal
        {
            get
            {
                return StringUtils.FormatCurrency(Quantity * UnitPrice);
            }
        }
        public string FormattedUnitPrice
        {
            get
            {
                return StringUtils.FormatCurrency(UnitPrice);
            }
        }
    }

    public class ReceiverInfo
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
    }
    public class BillDetails
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public bool CanCancel { get; set; }
        public bool PaidByCash { get; set; }
        public ReceiverInfo ReceiverInfo { get; set; }
        public List<Detail> Details { get; set; }
        public string PaidMethodName
        {
            get
            {
                return PaidByCash ? "Thẻ" : "Tiền mặt";
            }
        }
        public bool CanUpdateOrderStatus
        {
            get
            {
                return StatusId <= 2;
            }
        }
        public string FormattedTotal
        {
            get
            {
                return StringUtils.FormatCurrency(Total);
            }
        }
    }
}
