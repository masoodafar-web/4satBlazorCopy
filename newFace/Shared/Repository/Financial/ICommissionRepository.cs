using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;

namespace newFace.Shared.Repositories.Resource
{
    public interface ICommissionRepository
    {
        ResultCommission GetAll(string selfId,string subsetId);
        Result Create(Commission commission);

        Result CachReferralCode(string parentId);

        string ReadReferralCode();

        Result IncreaseUserCredit(string userId , double amount);

        Result ReferralCommissionCalculator(Commission commission);

        long UniGeneral(GeneologyPlan geneologyPlan, UserGeneology userGeneology, long Amount, int DividendAmountHistoryId);

        long DeltaGeneral(GeneologyPlan geneologyPlan, UserGeneology userGeneology, long Amount, int DeltaPlanId = 0);

        void BinaryGeneral(GeneologyPlan geneologyPlan, UserGeneology userGeneology, long Amount, int DividendAmountHistoryId);

        List<BinaryViewModel> BinaryCondition(GeneologyPlan geneologyPlan, UserGeneology userGeneology, long AmountBalanceBinary, string year, string month);

        void BreakGeneral(GeneologyPlan geneologyPlan);

        void SaveSellUser(string UserId, SystemType SystemType, long Amount, bool isSelfSell = true);

        CommissionPerMonth AddCommissionPerMonth(UserGeneology userGeneology, string Month, string Year, long Amount, FeeType feeType);

        DividendAmountHistory AddDividendAmountHistory(string UserId, string Month, string Year, long Amount);

        UserGeneology FindUserWithLevel(int Level, string UserId, int GeneologyTypeId);

        long PercentCalc(long Amount, int Percent);
    

    }
  
}
