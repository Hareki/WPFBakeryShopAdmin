﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Models
{
    public class RowItemBill
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }
        public int Total { get; set; }
        public string CustomerName { get; set; }

        public string StatusString
        {
            get
            {
                switch (StatusId)
                {
                    case 1: return "Chờ duyệt";
                    case 2: return "Đang giao";
                    case 3: return "Đã giao";
                    case 4: return "Đã hủy";
                    default:
                        {
                            Console.WriteLine("ID: " + StatusId);
                            return "Trạng thái không hợp lệ";
                        }

                }
            }
        }
    }
}