using Shared.Models.DataBaseModels.Account;
using Shared.Models.DataBaseModels.Costumers;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;

namespace DataBaseManager.MockData;

public static class MockData
{
    public static List<Sales> MockSalesData = new()
    {
        new Sales
        {
            ProductId = Guid.Parse("ecd5604e-bcae-4ec5-aa46-e25d07de6ad2"),
            
        }
    };
    public static List<Inventory> MockInventoryData = new()
    {
        new Inventory
        {
            Description = "*انبار محصول آماده فروش",
            Code = 31,
            BarcodeEnabled = true,
            Identifiable = false,
            IsMonetary = false,
            ActiveInReceiptRegistration = true,
            ActiveInReports = true,
            LimitByAccountAndDocumentType = false,
            LimitByMarketer = false,
            AffectProductionRequestBySalesOrder = true,
            AffectStockCalculation = true,
            UseAverageOrFIFOFromYearStart = false,
            Manager = "",
                Products = new()
                {
                    new Carpet { DesignName = "گلستان",Color = "سرمه ای",BorderColor = "سفید" ,DesignCode ="ترانه(کد136) ",Shoulder = "700",Density = "2550",Size = "0.8*0.5",WeavePattern = "پادری",ColorCount = 8,genus = "آکریلیک",Grade = "درجه 1",ProjectName = "پروژه  روی بافت نمایشگاه ",Manufacturer = "+340",ColorPalette = "C14",WeaveType = "برجسته",Buyer = "شرکت ساغر",DeviceNumber = "H",ProductCode = "0118808701127131510116020100101070591130015",ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 8رنگ _ درجه 1 +400" ,ProductDesc2 = " فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .",ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 240000000, SellingPrice = 240000000,},
                    new Carpet { DesignName = "گلستان 1", Color = "سرمه ای", BorderColor = "سفید", DesignCode = "ترانه(کد136)", Shoulder = "700", Density = "2550", Size = "0.8*0.5", WeavePattern = "پادری", ColorCount = 8, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+340", ColorPalette = "C14", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130001", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 8رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 240000000, SellingPrice = 240000000 },
                    new Carpet { DesignName = "گلستان 2", Color = "قرمز", BorderColor = "سفید", DesignCode = "ترانه(کد137)", Shoulder = "702", Density = "2555", Size = "0.81*0.51", WeavePattern = "پادری", ColorCount = 9, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+341", ColorPalette = "C15", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130002", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 9رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 240500000, SellingPrice = 240500000 },
                    new Carpet { DesignName = "گلستان 3", Color = "سبز", BorderColor = "سفید", DesignCode = "ترانه(کد138)", Shoulder = "704", Density = "2560", Size = "0.82*0.52", WeavePattern = "پادری", ColorCount = 8, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+342", ColorPalette = "C16", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130003", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 8رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 241000000, SellingPrice = 241000000 },
                    new Carpet { DesignName = "گلستان 4", Color = "آبی", BorderColor = "سفید", DesignCode = "ترانه(کد139)", Shoulder = "706", Density = "2565", Size = "0.83*0.53", WeavePattern = "پادری", ColorCount = 10, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+343", ColorPalette = "C17", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130004", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 10رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 241500000, SellingPrice = 241500000 },
                    new Carpet { DesignName = "گلستان 5", Color = "نارنجی", BorderColor = "سفید", DesignCode = "ترانه(کد140)", Shoulder = "708", Density = "2570", Size = "0.84*0.54", WeavePattern = "پادری", ColorCount = 9, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+344", ColorPalette = "C18", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130005", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 9رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 242000000, SellingPrice = 242000000 },
                    new Carpet { DesignName = "گلستان 6", Color = "بنفش", BorderColor = "سفید", DesignCode = "ترانه(کد141)", Shoulder = "710", Density = "2575", Size = "0.85*0.55", WeavePattern = "پادری", ColorCount = 8, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+345", ColorPalette = "C19", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130006", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 8رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 242500000, SellingPrice = 242500000 },
                    new Carpet { DesignName = "گلستان 7", Color = "سرمه ای", BorderColor = "سفید", DesignCode = "ترانه(کد142)", Shoulder = "712", Density = "2580", Size = "0.86*0.56", WeavePattern = "پادری", ColorCount = 9, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+346", ColorPalette = "C20", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130007", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 9رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 243000000, SellingPrice = 243000000 },
                    new Carpet { DesignName = "گلستان 8", Color = "قرمز", BorderColor = "سفید", DesignCode = "ترانه(کد143)", Shoulder = "714", Density = "2585", Size = "0.87*0.57", WeavePattern = "پادری", ColorCount = 10, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+347", ColorPalette = "C21", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130008", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 10رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 243500000, SellingPrice = 243500000 },
                    new Carpet { DesignName = "گلستان 9", Color = "سبز", BorderColor = "سفید", DesignCode = "ترانه(کد144)", Shoulder = "716", Density = "2590", Size = "0.88*0.58", WeavePattern = "پادری", ColorCount = 8, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+348", ColorPalette = "C22", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130009", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 8رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 244000000, SellingPrice = 244000000 },
                    new Carpet { DesignName = "گلستان 10", Color = "نارنجی", BorderColor = "سفید", DesignCode = "ترانه(کد145)", Shoulder = "718", Density = "2595", Size = "0.89*0.59", WeavePattern = "پادری", ColorCount = 9, genus = "آکریلیک", Grade = "درجه 1", ProjectName = "پروژه روی بافت نمایشگاه", Manufacturer = "+349", ColorPalette = "C23", WeaveType = "برجسته", Buyer = "شرکت ساغر", DeviceNumber = "H", ProductCode = "0118808701127131510116020100101070591130010", ProductDesc = "فرش 4*3 _ گلستان _ 1000 شانه آکریلیک تراکم1800 9رنگ _ درجه 1 +400", ProductDesc2 = "فرش گلستان 4*3 _ _ . آکریلیک 1000 شانه تراکم1800 +400 .", ProductDescBarcode = "", ProductDescLatin = "", ProductIsActive = true, Cost = 244500000, SellingPrice = 244500000 },
                    //new RawMaterial
                    //{

                    //},
                    //new Rug
                    //{

                    //}
                },
            Address = "",
            AllowNegativeStock = false
        }
    };
    public static List<Account> MockAccountData = new()
    {
        new Account
        {
            Name = "فروش",
            Code = "601",
            IsLayerOne = true,
            SubAccounts = new List<Account>
            {
                new Account
                {
                    Name = "J فروش بازرگانی",
                    Code = "0000003",
                    SubAccounts = new List<Account>
                    {
                        new Account
                        {
                            Name = "J فروش بازرگانی فرش",
                            Code = "0000001",
                            SubAccounts = new List<Account>
                            {
                                new Account { inflection = 2,Article =  67, Date = DateTime.SpecifyKind(new DateTime(1400,01,04), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,07), DateTimeKind.Utc), ArticleDescription = "حواله 3  انبار 31  تاریخ 1400/01/04    آقای علی قندیKH   ",Credit = 130000000,OperationName = "حواله",},
                                new Account { inflection = 2, Article = 67, Date = DateTime.SpecifyKind(new DateTime(1400,01,04), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,07), DateTimeKind.Utc), ArticleDescription = "حواله 1  انبار 31  تاریخ 1400/01/04    آقای علی قندی", Credit = 130000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 68, Date = DateTime.SpecifyKind(new DateTime(1400,01,05), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,08), DateTimeKind.Utc), ArticleDescription = "حواله 2  انبار 31  تاریخ 1400/01/05    آقای مهدی کریمی", Credit = 142000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 69, Date = DateTime.SpecifyKind(new DateTime(1400,01,06), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,09), DateTimeKind.Utc), ArticleDescription = "حواله 3  انبار 31  تاریخ 1400/01/06    آقای رضا محمدی", Credit = 135000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 70, Date = DateTime.SpecifyKind(new DateTime(1400,01,07), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,10), DateTimeKind.Utc), ArticleDescription = "حواله 4  انبار 31  تاریخ 1400/01/07    آقای حسن رضایی", Credit = 128000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 71, Date = DateTime.SpecifyKind(new DateTime(1400,01,08), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,11), DateTimeKind.Utc), ArticleDescription = "حواله 5  انبار 31  تاریخ 1400/01/08    آقای ناصر احمدی", Credit = 146000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 72, Date = DateTime.SpecifyKind(new DateTime(1400,01,09), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,12), DateTimeKind.Utc), ArticleDescription = "حواله 6  انبار 31  تاریخ 1400/01/09    آقای امیر حسینی", Credit = 133000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 73, Date = DateTime.SpecifyKind(new DateTime(1400,01,10), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,13), DateTimeKind.Utc), ArticleDescription = "حواله 7  انبار 31  تاریخ 1400/01/10    آقای حمید نادری", Credit = 125000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 74, Date = DateTime.SpecifyKind(new DateTime(1400,01,11), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,14), DateTimeKind.Utc), ArticleDescription = "حواله 8  انبار 31  تاریخ 1400/01/11    آقای مهدی کریمی", Credit = 139000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 75, Date = DateTime.SpecifyKind(new DateTime(1400,01,12), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,15), DateTimeKind.Utc), ArticleDescription = "حواله 9  انبار 31  تاریخ 1400/01/12    آقای رضا محمدی", Credit = 141000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 76, Date = DateTime.SpecifyKind(new DateTime(1400,01,13), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,16), DateTimeKind.Utc), ArticleDescription = "حواله 10  انبار 31  تاریخ 1400/01/13    آقای علی قندی", Credit = 132000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 77, Date = DateTime.SpecifyKind(new DateTime(1400,01,14), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,17), DateTimeKind.Utc), ArticleDescription = "حواله 11  انبار 31  تاریخ 1400/01/14    آقای ناصر احمدی", Credit = 137000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 78, Date = DateTime.SpecifyKind(new DateTime(1400,01,15), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,18), DateTimeKind.Utc), ArticleDescription = "حواله 12  انبار 31  تاریخ 1400/01/15    آقای امیر حسینی", Credit = 148000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 79, Date = DateTime.SpecifyKind(new DateTime(1400,01,16), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,19), DateTimeKind.Utc), ArticleDescription = "حواله 13  انبار 31  تاریخ 1400/01/16    آقای حمید نادری", Credit = 122000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 80, Date = DateTime.SpecifyKind(new DateTime(1400,01,17), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,20), DateTimeKind.Utc), ArticleDescription = "حواله 14  انبار 31  تاریخ 1400/01/17    آقای مهدی کریمی", Credit = 149000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 81, Date = DateTime.SpecifyKind(new DateTime(1400,01,18), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,21), DateTimeKind.Utc), ArticleDescription = "حواله 15  انبار 31  تاریخ 1400/01/18    آقای حسن رضایی", Credit = 136000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 82, Date = DateTime.SpecifyKind(new DateTime(1400,01,19), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,22), DateTimeKind.Utc), ArticleDescription = "حواله 16  انبار 31  تاریخ 1400/01/19    آقای رضا محمدی", Credit = 140000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 83, Date = DateTime.SpecifyKind(new DateTime(1400,01,20), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,23), DateTimeKind.Utc), ArticleDescription = "حواله 17  انبار 31  تاریخ 1400/01/20    آقای علی قندی", Credit = 150000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 84, Date = DateTime.SpecifyKind(new DateTime(1400,01,21), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,24), DateTimeKind.Utc), ArticleDescription = "حواله 18  انبار 31  تاریخ 1400/01/21    آقای ناصر احمدی", Credit = 145000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 85, Date = DateTime.SpecifyKind(new DateTime(1400,01,22), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,25), DateTimeKind.Utc), ArticleDescription = "حواله 19  انبار 31  تاریخ 1400/01/22    آقای امیر حسینی", Credit = 120000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 86, Date = DateTime.SpecifyKind(new DateTime(1400,01,23), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,26), DateTimeKind.Utc), ArticleDescription = "حواله 20  انبار 31  تاریخ 1400/01/23    آقای حمید نادری", Credit = 126000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 87, Date = DateTime.SpecifyKind(new DateTime(1400,01,24), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,27), DateTimeKind.Utc), ArticleDescription = "حواله 21  انبار 31  تاریخ 1400/01/24    آقای حسن رضایی", Credit = 129000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 88, Date = DateTime.SpecifyKind(new DateTime(1400,01,25), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,28), DateTimeKind.Utc), ArticleDescription = "حواله 22  انبار 31  تاریخ 1400/01/25    آقای علی قندی", Credit = 147000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 89, Date = DateTime.SpecifyKind(new DateTime(1400,01,26), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,29), DateTimeKind.Utc), ArticleDescription = "حواله 23  انبار 31  تاریخ 1400/01/26    آقای مهدی کریمی", Credit = 131000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 90, Date = DateTime.SpecifyKind(new DateTime(1400,01,27), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,01,30), DateTimeKind.Utc), ArticleDescription = "حواله 24  انبار 31  تاریخ 1400/01/27    آقای ناصر احمدی", Credit = 134000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 91, Date = DateTime.SpecifyKind(new DateTime(1400,02,01), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,02,04), DateTimeKind.Utc), ArticleDescription = "حواله 25  انبار 31  تاریخ 1400/02/01    آقای رضا محمدی", Credit = 138000000, OperationName = "حواله" },
                                new Account { inflection = 2, Article = 92, Date = DateTime.SpecifyKind(new DateTime(1400,02,02), DateTimeKind.Utc), DocDate = DateTime.SpecifyKind(new DateTime(1400,02,05), DateTimeKind.Utc), ArticleDescription = "حواله 26  انبار 31  تاریخ 1400/02/02    آقای حسن رضایی", Credit = 150000000, OperationName = "حواله" },
                            }
                        }
                    }
                } ,
                new Account
                {
                    Name = "J درآمدهای فروش بازرگانی",
                    Code = "0000004",
                },
                new Account
                {
                    Name = "J فروش (اصلاح موجودی)",
                    Code = "0000005",
                }
            }
        }
    };
    public static List<Customer> MockCustomerData = new()
    {


    };
}
