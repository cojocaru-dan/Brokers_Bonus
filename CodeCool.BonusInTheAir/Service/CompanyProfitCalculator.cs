using CodeCool.BonusInTheAir.Model;

namespace CodeCool.BonusInTheAir.Service;

public class CompanyProfitCalculator
{
    private readonly List<BonusRule> _bis;

    public CompanyProfitCalculator(List<BonusRule> bis)
    {
        _bis = bis;
    }

    public CompanyProfit Calculate(List<Broker> brs)
    {
        int j = 0;
        int i = brs.Count;
        double t = 0;
        double s = 0;
        double r = 0;
        while (i > 0)
        {
            Broker b = brs[brs.Count - i];
            t += b.Profit;
            double s2 = 0;
            j = _bis.Count;
            while (j > 0)
            {
                BonusRule bo = _bis[_bis.Count - j];
                if (b.Profit >= bo.Minimum)
                {
                    s2 = bo.Multiplier;
                    j = 1;
                }

                j--;
            }

            s += b.BaseSalary + (b.Profit * s2);
            i--;
        }

        r = t - s;
        return new CompanyProfit(t, s, r);
    }
}
