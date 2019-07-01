﻿using System;
using Agents.Members.Domain.Models;

namespace Agents.Sales.Domain.Models {
    /// <summary>
    /// 订单
    /// </summary>
    public partial class Order {

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init() {
            State = Enums.OrderState.WaitPay;
            OrderTime = DateTime.Now;
            base.Init();
        }

        /// <summary>
        /// 设置会员信息
        /// </summary>
        public void SetMemberInfo(Member member) {
            MemberId = member.Id;
        }

        /// <summary>
        /// 支付订单
        /// </summary>
        public void Pay() {
            State = Enums.OrderState.Payed;
            PayTime = DateTime.Now;
        }
    }
}