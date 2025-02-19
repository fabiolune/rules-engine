﻿using LogicEngine.Internals;
using LogicEngine.Models;
using System.Linq;

namespace LogicEngine.Unit.Tests;

[TestFixture]
public class CatalogCompositionTests
{
    [TestCase(0, 0)]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 3)]
    public void When_AddingTwoCatalogs_ExpectSumOfRules(int ruleSets1, int ruleSets2)
    {
        var c1 = new RulesCatalog(Enumerable
            .Range(0, ruleSets1)
            .Select(cr => new RulesSet([], "ruleset 1")), "name 1");

        var c2 = new RulesCatalog(Enumerable
            .Range(0, ruleSets2)
            .Select(cr => new RulesSet([], "ruleset 2")), "name 2");

        var sumCatalog = c1 + c2;

        sumCatalog.RulesSets.Count().ShouldBe(ruleSets1 + ruleSets2);
        sumCatalog.Name.ShouldBe("(name 1 OR name 2)");
    }

    [TestCase(0, 0)]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 3)]
    public void When_MultiplyingTwoCatalogs_ExpectProductOfRules(int ruleSets1, int ruleSets2)
    {
        var c1 = new RulesCatalog(Enumerable
            .Range(0, ruleSets1)
            .Select(cr => new RulesSet([], "ruleset 1")), "name 1");

        var c2 = new RulesCatalog(Enumerable
            .Range(0, ruleSets2)
            .Select(cr => new RulesSet([], "ruleset 2")), "name 2");

        var sumCatalog = c1 * c2;

        sumCatalog.RulesSets.Count().ShouldBe(ruleSets1 * ruleSets2);
        sumCatalog.Name.ShouldBe("(name 1 AND name 2)");
    }

    [Test]
    public void CatalogsSum_WhenFirstRulesSetIsNull_ShouldReturnProperSum()
    {
        var c1 = new RulesCatalog(null, "catalog 1");
        var c2 = new RulesCatalog(
        [
            new RulesSet
            (
                [
                    new Rule("a", OperatorType.Equal, "b", "code")
                ],
                "ruleset 1"
            )
        ], "catalog 2");

        var sumCatalog1 = c1 + c2;

        sumCatalog1.RulesSets.Count().ShouldBe(1);
        sumCatalog1.Name.ShouldBe("(catalog 1 OR catalog 2)");

        var sumCatalog2 = c2 + c1;

        sumCatalog2.RulesSets.Count().ShouldBe(1);
        sumCatalog2.Name.ShouldBe("(catalog 2 OR catalog 1)");
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void CatalogsProduct_WhenOneRulesSetIsNull_ShouldReturnProperProduct(int ruleSets2)
    {
        var c1 = new RulesCatalog(null, "catalog 1");
        var c2 = new RulesCatalog(Enumerable
            .Range(0, ruleSets2)
            .Select(cr => new RulesSet([], $"ruleset {cr}")), "catalog 2");

        var prodCatalog1 = c1 * c2;

        prodCatalog1.RulesSets.Count().ShouldBe(0);
        prodCatalog1.Name.ShouldBe("(catalog 1 AND catalog 2)");

        var prodCatalog2 = c2 * c1;

        prodCatalog2.RulesSets.Count().ShouldBe(0);
        prodCatalog2.Name.ShouldBe("(catalog 2 AND catalog 1)");

    }

    [Test]
    public void CatalogsProduct_WhenBothRulesSetAreNull_ShouldReturnProperProduct()
    {
        var c1 = new RulesCatalog(null, "catalog 1");
        var c2 = new RulesCatalog(null, "catalog 2");

        var prodCatalog1 = c1 * c2;

        prodCatalog1.RulesSets.Count().ShouldBe(0);
        prodCatalog1.Name.ShouldBe("(catalog 1 AND catalog 2)");
    }
}
