using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Enum;

public enum AccountGroupTypeDesc
{
    None = 0,
    CurrentAssets = 1,        // دارایی‌های جاری
    NonCurrentAssets = 2,     // دارایی‌های غیر جاری
    CurrentLiabilities = 3,   // بدهی‌های جاری
    ShareholdersEquity = 4,   // حقوق صاحبان سهام
    Revenue = 5,              // درآمد
    FinishedPrice = 6,        // قیمت تمام شده
    Expense = 7,              // هزینه
    Disciplinary = 8,         // انتظامی
    DisciplinaryAccount = 9   // طرف حساب انتظامی
}
