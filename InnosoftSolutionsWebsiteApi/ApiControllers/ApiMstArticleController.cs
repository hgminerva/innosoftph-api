using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InnosoftSolutionsWebsiteApi.ApiControllers
{
    // Router prefix for web api
    [RoutePrefix("api/article")]
    public class ApiMstArticleController : ApiController
    {
        // database - LinQ to SQL class
        private Data.InnosoftSolutionsDatabaseDataContext db = new Data.InnosoftSolutionsDatabaseDataContext();

        // list article by article type id
        [HttpGet, Route("list/byArticleTypeId/{articleTypeId}")]
        public List<Entities.MstArticle> listArticleByArticleTypeId(String articleTypeId)
        {
            var articles = from d in db.MstArticles.OrderBy(d => d.Article)
                           where d.ArticleTypeId == Convert.ToInt32(articleTypeId)
                           select new Entities.MstArticle
                           {
                               Id = d.Id,
                               ArticleCode = d.ArticleCode,
                               ManualArticleCode = d.ManualArticleCode,
                               Article = d.Article,
                               Category = d.Category,
                               ArticleTypeId = d.ArticleTypeId,
                               ArticleType = d.MstArticleType.ArticleType,
                               ArticleGroupId = d.ArticleGroupId,
                               ArticleGroup = d.MstArticleGroup.ArticleGroup,
                               AccountId = d.AccountId,
                               AccountCode = d.MstAccount.AccountCode,
                               Account = d.MstAccount.Account,
                               SalesAccountId = d.SalesAccountId,
                               SalesAccount = d.MstAccount1.Account,
                               CostAccountId = d.CostAccountId,
                               CostAccount = d.MstAccount2.Account,
                               AssetAccountId = d.AssetAccountId,
                               AssetAccount = d.MstAccount3.Account,
                               ExpenseAccountId = d.ExpenseAccountId,
                               ExpenseAccount = d.MstAccount4.Account,
                               UnitId = d.UnitId,
                               Unit = d.MstUnit.Unit,
                               InputTaxId = d.InputTaxId,
                               InputTax = d.MstTaxType1.TaxType,
                               OutputTaxId = d.OutputTaxId,
                               OutputTax = d.MstTaxType.TaxType,
                               WTaxTypeId = d.WTaxTypeId,
                               WTaxType = d.MstTaxType2.TaxType,
                               Price = d.Price,
                               Cost = d.Cost,
                               IsInventory = d.IsInventory,
                               Particulars = d.Particulars,
                               Address = d.Address,
                               TermId = d.TermId,
                               Term = d.MstTerm.Term,
                               ContactNumber = d.ContactNumber,
                               ContactPerson = d.ContactPerson,
                               TaxNumber = d.TaxNumber,
                               EmailAddress = d.EmailAddress,
                               CreditLimit = d.CreditLimit,
                               DateAcquired = d.DateAcquired.ToShortDateString(),
                               UsefulLife = d.UsefulLife,
                               SalvageValue = d.SalvageValue,
                               ManualArticleOldCode = d.ManualArticleOldCode,
                               Kitting = d.Kitting,
                               IsLocked = d.IsLocked,
                               CreatedById = d.CreatedById,
                               CreatedBy = d.MstUser.FullName,
                               CreatedDateTime = d.CreatedDateTime.ToShortDateString(),
                               UpdatedById = d.UpdatedById,
                               UpdatedBy = d.MstUser1.FullName,
                               UpdatedDateTime = d.UpdatedDateTime.ToShortDateString()
                           };

            return articles.ToList();
        }
    }
}
