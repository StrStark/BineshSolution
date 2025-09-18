using ClosedXML.Excel;
using DataBaseManager.DbContexts;
using DocumentFormat.OpenXml.Spreadsheet;
using Shared.Enum;
using Shared.Models.DataBaseModels.Sales;

namespace DataBaseManager;

public class ExcelDataInjection
{
    //private readonly InventoryDbContext _appDbContext = default!;
    //private static readonly Dictionary<string, Comb> _mapComb = new()
    //{
    //    {"1200شانه"  , Comb.TwelveHundred},
    //    {"1000 شانه" , Comb.OneThousand},
    //    {"700 شانه"  , Comb.SevenHundred },
    //    {"1500شانه"  , Comb.FifteenHundred},
    //    {"." , Comb.None },
    //    {"320 شانه" , Comb.ThreeHundredTwenty},
    //    {"500 شانه"  , Comb.FiveHundred},
    //    {"340 شانه" , Comb.ThreeHundredForty},
    //    {"440 شانه" , Comb.FourHundredForty},
    //    {"چاپی" , Comb.Chapi },
    //    {"400شانه" , Comb.FourHundred },
    //    {"310شانه" , Comb.ThreeHundredTen },
    //    {"نخ بافت"  , Comb.NakhBaft},
    //    {"  700 شانه " , Comb.SevenHundred},
    //    {"  . " , Comb.None },
    //    {"  1200شانه " , Comb.TwelveHundred },
    //    {"  1000 شانه " , Comb.OneThousand },
    //    {"  1500شانه " , Comb.FiveHundred },
    //    {"  320 شانه " , Comb.ThreeHundredTwenty },
    //    { "  500 شانه " , Comb.FiveHundred},
    //    {"  340 شانه " , Comb.ThreeHundredForty },
    //};
    //private static readonly Dictionary<string, InvoiceType> _mapInvoice = new()
    //{
    //    {"*درخواست فروش محصول"  , InvoiceType.SalesRequest},
    //    {"حواله" , InvoiceType.DeliveryNote},
    //    {"*رسید تولید محصول 700 شانه"  , InvoiceType.ProductionReceipt700},
    //    {"*رسید تولید محصول 1200 شانه"  , InvoiceType.ProductionReceipt1200},
    //    {"تولید محصول" , InvoiceType.Production },
    //    {"فاکتور" , InvoiceType.Invoice },
    //    {"*درخواست فروش محصول ST" , InvoiceType.STSalesRequest }
    //};
    //private readonly Dictionary<string, RequestState> _mapRequestState = new()
    //{
    //    {"پاسخ داده شده" , RequestState.Answered },
    //    {"بی اثر" , RequestState.Ineffective},
    //    {"بی پاسخ" , RequestState.NoAnswered},
    //    {"" , RequestState.None }
    //};

    //public ExcelDataInjection(InventoryDbContext appDbContext)
    //{
    //    _appDbContext = appDbContext;
    //}
    //private RequestState ToRequestState(string? req)
    //{
    //    if (req == null) return RequestState.None;
    //    if (_mapRequestState.TryGetValue(req, out var state))
    //        return state;
    //    return RequestState.None;
    //}
    //private InvoiceType ToType(string? Type)
    //{
    //    if (Type == null) return InvoiceType.None;
    //    if (_mapInvoice.TryGetValue(Type, out var type))
    //        return type;
    //    return InvoiceType.None;
    //}
    //private Comb ToComb(string combDetail)
    //{
    //    if (combDetail == null) return Comb.None;
    //    if (_mapComb.TryGetValue(combDetail, out var comb))
    //        return comb;
    //    return Comb.None;
    //}
}
