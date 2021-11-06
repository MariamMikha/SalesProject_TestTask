using SalesProject.Models.DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesProject.Models.Modules.Products
{
    public class ProductsCOM
    {
        public List<ProductsModel> GetProducts(int LanguageID, out int NumberOfRecords, int PageID = 1, int PageSize = 10)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                var Data = (from product in db.MW_Products
                            where !product.IsDeleted
                            select new ProductsModel()
                            {
                                MainImage = product.MainImage,
                                Price = product.Price,
                                ProductDescription = product.ProductDescription,
                                ProductID = product.ID,
                                ProductName = product.ProductName,
                                SoldOut = product.SoldOut,
                                Stock = product.Stock,
                                PostedDate = product.PostedDate,
                            });
                NumberOfRecords = Data.Count();

                List<ProductsModel> lstProducts = Data.OrderBy(x=>x.ProductID).Skip(PageSize * (PageID - 1)).Take(PageSize).ToList();

                return lstProducts;
            }
        }

        public ProductsModel GetProductDetails(long ProductID, int LanguageID, long CustomerID = 0, bool IsMobile = false)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                ProductsModel oProduct = (from product in db.MW_Products
                                          where product.ID == ProductID
                                          select new ProductsModel()
                                          {
                                              MainImage = product.MainImage,
                                              Price = product.Price,
                                              ProductDescription = product.ProductDescription,
                                              ProductID = product.ID,
                                              ProductName = product.ProductName,
                                              SoldOut = product.SoldOut,
                                              Stock = product.Stock,
                                              PostedDate = product.PostedDate,
                                          }).FirstOrDefault();


                return oProduct;
            }
        }
        public ProductsModel SaveProduct(ProductsModel oModel, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_Products oProducts = new MW_Products();
                if (oModel.ProductID > 0)
                {
                    oProducts = db.MW_Products.FirstOrDefault(x => x.ID == oModel.ProductID);
                }

                oProducts.IsDeleted = false;
                oProducts.MainImage = oModel.MainImage;
                oProducts.Price = oModel.Price;
                oProducts.ProductDescription = oModel.ProductDescription;
                oProducts.ProductName = oModel.ProductName;
                oProducts.SoldOut = oModel.SoldOut ? true : oModel.Stock <= 0 ? true : false;
                oProducts.Stock = oModel.Stock;
                if (oModel.ProductID == 0)
                {
                    oProducts.PostedDate = DateTime.Now;
                    db.MW_Products.Add(oProducts);
                }
                db.SaveChanges();

                return GetProductDetails(oProducts.ID, LanguageID);
            }
        }

        public bool DeleteProduct(int ProductID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_Products oProduct = db.MW_Products.FirstOrDefault(x => x.ID == ProductID);
                if (oProduct != null)
                {
                    oProduct.IsDeleted = true;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}