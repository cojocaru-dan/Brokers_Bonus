using CodeCool.BonusInTheAir.Model;
using CodeCool.BonusInTheAir.Service;
namespace BonusInTheAirTest;

public class Tests
{
    private string[] _brokersNames = new string[] {
            "John Smith", "Alice Johnson", "Michael Davis", "Emily Wilson",
            "David Clark", "Sophia Anderson", "William Taylor", "Olivia Martinez",
            "James Brown", "Emma Garcia", "Daniel Lee", "Mia Rodriguez",
            "Joseph Hernandez", "Ava Miller", "Robert White", "Abigail Moore",
            "Charles Hall", "Samantha Turner", "Thomas Scott", "Ella Lewis"
        };

    private List<BonusRule> _bonusRules;
    private CompanyProfitCalculator? _newCompProfCalc;
    [SetUp]
    public void Setup()
    {
        _bonusRules = CreateBonusRules();
        _newCompProfCalc = new CompanyProfitCalculator(_bonusRules);
    }

    [Test]
    public void Calculate_BrokersNoProfit_NegativeCompanyProfit()
    {
        List<Broker> brokers = CreateBrokersNoProfit();
        int total = 0;
        int salaries = 400 * brokers.Count;
        int remaining = total - salaries;

        CompanyProfit newCompanyProfit = _newCompProfCalc.Calculate(brokers);
        Assert.Multiple(() =>
        {
            Assert.That(newCompanyProfit.Total, Is.EqualTo(total));
            Assert.That(newCompanyProfit.Salaries, Is.EqualTo(salaries));
            Assert.That(newCompanyProfit.Remaining, Is.EqualTo(remaining));
        });
    }
    [Test]
    public void Calculate_BrokersWithProfitLessThanBonusRules_CompanyProfitWithSomeProfit()
    {
        List<Broker> brokers = CreateBrokersWithProfitLessThanBonusRules();
        int total = 90 * 20;
        int salaries = 400 * brokers.Count;
        int remaining = total - salaries;

        CompanyProfit newCompanyProfit = _newCompProfCalc.Calculate(brokers);
        Assert.Multiple(() =>
        {
            Assert.That(newCompanyProfit.Total, Is.EqualTo(total));
            Assert.That(newCompanyProfit.Salaries, Is.EqualTo(salaries));
            Assert.That(newCompanyProfit.Remaining, Is.EqualTo(remaining));
        });
    }
    [Test]
    public void Calculate_SomeBrokersWithProfitBiggerThanBonusRules_CompanyProfitWithSomeProfit()
    {
        List<Broker> brokers = CreateBrokersWithProfitBiggerThanBonusRules();
        int total = 160 * 20;
        int salaries = brokers.Count * (400 + (160 * 2));
        int remaining = total - salaries;

        CompanyProfit newCompanyProfit = _newCompProfCalc.Calculate(brokers);
        Assert.Multiple(() =>
        {
            Assert.That(newCompanyProfit.Total, Is.EqualTo(total));
            Assert.That(newCompanyProfit.Salaries, Is.EqualTo(salaries));
            Assert.That(newCompanyProfit.Remaining, Is.EqualTo(remaining));
        });
    }
    private List<BonusRule> CreateBonusRules()
    {
        List<BonusRule> bonusRules = new List<BonusRule>();
        for (int i = 0; i < 10; i++)
        {
            int minimumForBonus = i < 3 ? 100
                                : i < 6 ? 150
                                : 200;
            BonusRule newBonusRule = new BonusRule(minimumForBonus, 2);
            bonusRules.Add(newBonusRule);
        }
        return bonusRules;
    }

    private List<Broker> CreateBrokersNoProfit()
    {
        List<Broker> brokers = new List<Broker>();
        for (int i = 0; i < 20; i++)
        {
            Broker newBroker = new Broker(_brokersNames[i], 400, 0);
            brokers.Add(newBroker);
        }
        return brokers;
    }

    private List<Broker> CreateBrokersWithProfitLessThanBonusRules()
    {
        List<Broker> brokers = new List<Broker>();
        for (int i = 0; i < 20; i++)
        {
            Broker newBroker = new Broker(_brokersNames[i], 400, 90);
            brokers.Add(newBroker);
        }
        return brokers;
    }
    private List<Broker> CreateBrokersWithProfitBiggerThanBonusRules()
    {
        List<Broker> brokers = new List<Broker>();
        for (int i = 0; i < 20; i++)
        {
            Broker newBroker = new Broker(_brokersNames[i], 400, 160);
            brokers.Add(newBroker);
        }
        return brokers;
    }
}