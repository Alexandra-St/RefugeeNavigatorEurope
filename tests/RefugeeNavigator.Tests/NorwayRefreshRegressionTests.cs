using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;

namespace RefugeeNavigator.Tests;

public sealed class NorwayRefreshRegressionTests
{
    private const string ReviewDate = "2026-11-18T00:00:00+00:00";
    private const string NorwayCountryId = "10000000-0000-0000-0000-000000000019";
    private static readonly IReadOnlyDictionary<string, Guid> Articles = new Dictionary<string, Guid>(StringComparer.Ordinal)
    {
        ["NO-TP-01"] = Guid.Parse("411f9a13-a59d-53e6-a25c-148d63aa3818"),
        ["NO-TP-02"] = Guid.Parse("3030161d-7da6-5e97-9a68-9a410aedecf2"),
        ["NO-EMP-01"] = Guid.Parse("027596b8-606a-58df-aae0-63a040f69064"),
        ["NO-EMP-02"] = Guid.Parse("8fd8c9c0-1b79-5e47-bf21-ce3f84dbe4b7"),
        ["NO-HEALTH-01"] = Guid.Parse("0073bdfa-5d2e-5fc6-a63f-35763fd61032"),
        ["NO-HOUSING-01"] = Guid.Parse("df086f92-efd1-50e6-b2dd-f2af63e00963"),
        ["NO-EDU-01"] = Guid.Parse("e77330ad-938f-5d9e-bdf7-3318ee0a7b96"),
        ["NO-SOC-01"] = Guid.Parse("8bc5e2c5-0a6c-59f8-b37b-3b75300fe817")
    };

    [Fact]
    public async Task NorwayRefreshKeepsProtectionRulesAndDatesDistinct()
    {
        var catalog = await LoadCatalog();
        var tp = Content(catalog, "NO-TP-01");
        var routes = Content(catalog, "NO-TP-02");

        Assert.Contains("national collective protection scheme", tp, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("section 34 of Norway’s Immigration Act", tp, StringComparison.Ordinal);
        Assert.Contains("separate from the EU Temporary Protection Directive", tp, StringComparison.Ordinal);
        Assert.Contains("one year at a time", tp, StringComparison.Ordinal);
        Assert.Contains("five years from the first permit", tp, StringComparison.Ordinal);
        Assert.Contains("proposal remained under consideration", tp, StringComparison.Ordinal);
        Assert.Contains("from five to seven years", tp, StringComparison.Ordinal);
        Assert.Contains("Do not describe a sixth or seventh year as currently available", tp, StringComparison.Ordinal);
        Assert.DoesNotContain("Norway now allows seven years", tp, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("continue the scheme to March 2028", tp, StringComparison.Ordinal);
        Assert.Contains("not an extension of each person’s permit", tp, StringComparison.Ordinal);
        Assert.Contains("reach that limit in spring 2027", tp, StringComparison.Ordinal);
        Assert.Contains("14 October 2026", tp, StringComparison.Ordinal);
        Assert.Contains("10 November 2026", tp, StringComparison.Ordinal);
        Assert.Contains("18 November 2026", tp, StringComparison.Ordinal);
        Assert.Contains("not a current individual entitlement", tp, StringComparison.Ordinal);
        Assert.Contains("does not itself provide a basis for permanent residence", routes, StringComparison.Ordinal);
    }

    [Fact]
    public async Task NorwayRefreshKeepsSafeAreaAndAgeRestrictionsNarrow()
    {
        var catalog = await LoadCatalog();
        var tp = Content(catalog, "NO-TP-01");

        Assert.Contains("For new applicants", tp, StringComparison.Ordinal);
        Assert.Contains("28 September 2024", tp, StringComparison.Ordinal);
        Assert.Contains("effective 13 January 2025", tp, StringComparison.Ordinal);
        Assert.Contains("contains 14 areas: Cherkasy, Chernivtsi, Ivano-Frankivsk, Khmelnytskyi, Kirovohrad, Kyiv oblast (not Kyiv city), Lviv, Poltava, Rivne, Ternopil, Vinnytsia, Volyn, Zakarpattia and Zhytomyr", tp, StringComparison.Ordinal);
        Assert.Contains("Kyiv oblast (not Kyiv city)", tp, StringComparison.Ordinal);
        Assert.Contains("does not automatically end an existing permit", tp, StringComparison.Ordinal);
        Assert.Contains("may still qualify for extension", tp, StringComparison.Ordinal);
        Assert.Contains("Men aged 18–60", tp, StringComparison.Ordinal);
        Assert.Contains("from 5 May 2026", tp, StringComparison.Ordinal);
        Assert.Contains("entered into force on 4 May 2026", tp, StringComparison.Ordinal);
        Assert.Contains("applies to applications submitted after that date", tp, StringComparison.Ordinal);
        Assert.Contains("sole-care situations involving a child", tp, StringComparison.Ordinal);
        Assert.Contains("permanent exemption or inability to serve", tp, StringComparison.Ordinal);
        Assert.Contains("medical evacuation category", tp, StringComparison.Ordinal);
        Assert.Contains("evidence-specific", tp, StringComparison.Ordinal);
        Assert.Contains("not existing permits or their renewals", tp, StringComparison.Ordinal);
        Assert.Contains("UDI may assess your need for protection individually", tp, StringComparison.Ordinal);
        Assert.Contains("refusal to extend collective protection cannot be appealed", tp, StringComparison.Ordinal);
    }

    [Fact]
    public async Task NorwayRefreshKeepsRegistrationAndWorkRightsStatusSpecific()
    {
        var catalog = await LoadCatalog();
        var tp = Content(catalog, "NO-TP-01");
        var work = Content(catalog, "NO-EMP-02");

        Assert.Contains("National Arrival Centre", tp, StringComparison.Ordinal);
        Assert.Contains("The police register applicants and collect identity information", tp, StringComparison.Ordinal);
        Assert.Contains("Do not treat registration as approval", tp, StringComparison.Ordinal);
        Assert.Contains("cannot work or apply for jobs while UDI is processing", tp, StringComparison.Ordinal);
        Assert.Contains("If collective protection is granted", tp, StringComparison.Ordinal);
        Assert.Contains("you have the right to work", tp, StringComparison.Ordinal);
        Assert.Contains("special work-based route remains a proposal", Content(catalog, "NO-TP-02"), StringComparison.Ordinal);
        Assert.Contains("consultation deadline was extended to 5 November 2026", Content(catalog, "NO-TP-02"), StringComparison.Ordinal);
        Assert.Contains("no new rules had been introduced", Content(catalog, "NO-TP-02"), StringComparison.Ordinal);
        Assert.Contains("cannot work or apply for jobs", work, StringComparison.Ordinal);
        Assert.Contains("UDI says you have the right to work", work, StringComparison.Ordinal);
        Assert.Contains("Registering a business by itself is not work authorization", work, StringComparison.Ordinal);
        Assert.Contains("Registration, tax, VAT and regulated-profession requirements are separate", work, StringComparison.Ordinal);
        Assert.DoesNotContain("Ukraine-specific employer notification", work, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("nine months", Content(catalog, "NO-TP-02"), StringComparison.Ordinal);
    }

    [Fact]
    public async Task NorwayRefreshKeepsWagesTaxAndNationalInsuranceQualified()
    {
        var catalog = await LoadCatalog();
        var work = Content(catalog, "NO-EMP-02");
        var research = ReadReviewArtifact("NORWAY_REFRESH_RESEARCH_01.md");

        Assert.Contains("no nationwide statutory minimum wage", work, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("ten specified sectors", work, StringComparison.Ordinal);
        Assert.Contains("15 June 2026", work, StringComparison.Ordinal);
        Assert.Contains("6 November 2026", work, StringComparison.Ordinal);
        Assert.Contains("precise job, age, skill and experience category", work, StringComparison.Ordinal);
        Assert.DoesNotContain("NOK", Regex.Match(work, "## Minimum pay(?<body>.*?)(?:\\n## |$)", RegexOptions.Singleline).Groups["body"].Value, StringComparison.Ordinal);
        Assert.Contains("Tax-card/PAYE", research, StringComparison.Ordinal);
        Assert.Contains("ordinary-tax treatment", research, StringComparison.Ordinal);
        Assert.Contains("National Insurance membership", research, StringComparison.Ordinal);
        Assert.Contains("no blanket tax rate or benefit entitlement", research, StringComparison.Ordinal);
        Assert.Contains("more than six months abroad", research, StringComparison.Ordinal);
        Assert.Contains("case-specific, not automatic universal rules", research, StringComparison.Ordinal);
    }

    [Fact]
    public async Task NorwayRefreshKeepsHealthcareHousingAndBenefitsConditional()
    {
        var catalog = await LoadCatalog();
        var healthcare = Content(catalog, "NO-HEALTH-01");
        var housing = Content(catalog, "NO-HOUSING-01");
        var social = Content(catalog, "NO-SOC-01");
        var employment = Content(catalog, "NO-EMP-01");

        Assert.Contains("the right to healthcare starts when they arrive in Norway and ask for protection", healthcare, StringComparison.Ordinal);
        Assert.Contains("If collective protection is granted", healthcare, StringComparison.Ordinal);
        Assert.Contains("Some services have user fees", healthcare, StringComparison.Ordinal);
        Assert.Contains("Do not assume that all treatment, medicines or dental care are free", healthcare, StringComparison.Ordinal);
        Assert.Contains("116 117", healthcare, StringComparison.Ordinal);
        Assert.Contains("113", healthcare, StringComparison.Ordinal);
        Assert.Contains("National Arrival Centre", housing, StringComparison.Ordinal);
        Assert.Contains("IMDi selects a municipality", housing, StringComparison.Ordinal);
        Assert.Contains("not guaranteed your preferred municipality", housing, StringComparison.Ordinal);
        Assert.Contains("private rental market", housing, StringComparison.Ordinal);
        Assert.Contains("six months’ rent", housing, StringComparison.Ordinal);
        Assert.Contains("NAV assesses applications individually", housing, StringComparison.Ordinal);
        Assert.Contains("housing allowance", housing, StringComparison.Ordinal);
        Assert.Contains("does not guarantee", housing, StringComparison.Ordinal);
        Assert.Contains("18–55", employment, StringComparison.Ordinal);
        Assert.Contains("over 55, you may still have a right", employment, StringComparison.Ordinal);
        Assert.Contains("55–67", employment, StringComparison.Ordinal);
        Assert.Contains("not the same guaranteed right", employment, StringComparison.Ordinal);
        Assert.Contains("not an automatic payment", employment, StringComparison.Ordinal);
        Assert.Contains("individually assessed", social, StringComparison.Ordinal);
        Assert.Contains("If you have nowhere to sleep for the next 24 hours", social, StringComparison.Ordinal);
        Assert.Contains("more than 12 months", social, StringComparison.Ordinal);
        Assert.Contains("13–19 months", social, StringComparison.Ordinal);
        Assert.Contains("five years of membership", social, StringComparison.Ordinal);
        Assert.Contains("does not automatically qualify you for NAV payments", social, StringComparison.Ordinal);
    }

    [Fact]
    public async Task NorwayRefreshKeepsEducationAndFutureChangeAccurate()
    {
        var catalog = await LoadCatalog();
        var education = Content(catalog, "NO-EDU-01");
        var compare = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json"))
            .LoadAsync(await LoadCatalog());
        JsonComparisonFactProvider.Validate(compare, await LoadCatalog());
        var school = compare.Topics.SelectMany(topic => topic.Aspects).Single(aspect => aspect.Id == "education-school-enrolment")
            .Facts.Single(fact => fact.CountrySlug == "norway").Values["en"];

        Assert.Contains("more than three months", education, StringComparison.Ordinal);
        Assert.Contains("as soon as possible", education, StringComparison.Ordinal);
        Assert.Contains("no later than three months after arrival", education, StringComparison.Ordinal);
        Assert.Contains("effective 1 July 2026", education, StringComparison.Ordinal);
        Assert.DoesNotContain("one month after arrival", education, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("scheduled to end on 1 July 2028", education, StringComparison.Ordinal);
        Assert.Contains("1 July 2028", school, StringComparison.Ordinal);
        Assert.Contains("three-month deadline", school, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("1 Apr 2028", ReadReviewArtifact("DATE_EVENT_WATCH_CANDIDATES_01.md"), StringComparison.Ordinal);
        Assert.Contains("1 July 2028", ReadReviewArtifact("NORWAY_REFRESH_REVIEW_01.md"), StringComparison.Ordinal);
    }

    [Fact]
    public async Task NorwayPostReviewClaimCoverageAndWatchArtifactsMatchProduction()
    {
        var catalog = await LoadCatalog();
        var root=FindRepositoryRoot();
        var ledger=ReadCsv(Path.Combine(root,"docs","content-refresh","2026-11","norway","CLAIM_LEDGER_01.csv"));
        var coverage=ReadCsv(Path.Combine(root,"docs","content-refresh","2026-11","norway","CLAIM_SOURCE_COVERAGE_01.csv"));
        Assert.Equal(148, ledger.Count);
        Assert.Equal(148, coverage.Count);
        Assert.Equal(134, ledger.Count(row=>row["Status"]=="CONFIRMED"));
        Assert.Equal(1, ledger.Count(row=>row["Status"]=="UPDATED"));
        Assert.Equal(13, ledger.Count(row=>row["Status"]=="NEW IMPORTANT INFORMATION"));
        Assert.Equal(34, ledger.Count(row=>row["NumericDate"]=="Yes"));
        Assert.Equal(144, coverage.Count(row=>row["Materiality"]=="MATERIAL"));
        Assert.Equal(4, coverage.Count(row=>row["Materiality"]=="SUPPORTING"));
        Assert.Equal(94, coverage.Count(row=>row["Coverage result"]=="COVERED"));
        Assert.Equal(50, coverage.Count(row=>row["Coverage result"]=="COVERED — MULTIPLE SOURCES"));
        Assert.Equal(4, coverage.Count(row=>row["Coverage result"]=="NOT EXTERNALLY SOURCE-DEPENDENT"));
        Assert.Equal(0, coverage.Count(row=>row["Coverage result"].Contains("UNSUPPORTED",StringComparison.OrdinalIgnoreCase)));
        Assert.All(coverage,row=>Assert.Equal("ACCEPT",row["IndependentReview"]));
        var watch=ReadReviewArtifact("DATE_EVENT_WATCH_CANDIDATES_01.md");
        Assert.Equal(11,Regex.Matches(watch,"(?m)^\\| \\d+ \\|").Count);
        Assert.Contains("1 December 2026",watch,StringComparison.Ordinal);
        Assert.Contains("1 Apr 2028",watch,StringComparison.Ordinal);
        Assert.Contains("REVIEW_ON",watch,StringComparison.Ordinal);
        Assert.Contains("not a legal trigger",ReadReviewArtifact("NORWAY_REFRESH_REVIEW_01.md"),StringComparison.OrdinalIgnoreCase);
        Assert.Equal(1559,catalog.OfficialSources.Count);
        Assert.Equal(catalog.OfficialSources.Count,catalog.OfficialSources.Select(source=>source.Id).Distinct().Count());
    }

    [Fact]
    public async Task NorwayRefreshAddsSourcesAndBindingsWithoutApplyingRejectedHusbankenUpdate()
    {
        var catalog=await LoadCatalog();
        var sources=catalog.OfficialSources.ToDictionary(source=>source.Id);
        var norway=Articles.Values.ToHashSet();
        var norwaySources=sources.Values.Where(source=>norway.Contains(source.InformationEntryId)).ToArray();
        Assert.Equal(46,norwaySources.Length);
        Assert.Equal(38,norwaySources.Select(source=>source.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(0,norwaySources.GroupBy(source=>(source.InformationEntryId,source.Url.AbsoluteUri)).Count(group=>group.Count()>1));
        Assert.Equal("https://www.husbanken.no/english/housing-allowance/type-of-housing/",sources[Guid.Parse("5131b9a5-13c9-52db-ba4d-1ec6ae2cf401")].Url.AbsoluteUri);
        Assert.Equal("https://www.udi.no/en/information-ukraine-and-russia/ukraine/waiting-for-a-decision/school-health-dnumber-asylum-seeker-card/",sources[Guid.Parse("c7cd4cf0-0cb2-52ba-94e0-0d2b95bad4eb")].Url.AbsoluteUri);
        Assert.Equal("https://hkdir.no/en/foreign-education/education-from-outside-of-norway/mayete-osvitu-z-ukrayini",sources[Guid.Parse("5bfeeae8-82f9-54bb-ba32-9df08eb405da")].Url.AbsoluteUri);
        var adds=new[]
        {
            "b557071d-a1d5-5477-afc8-7bac81a15ed4","c0004a3c-e283-5cde-9c72-aed63dca5d9e","d97f071b-f63e-57f2-9646-8d9b7573b6fd",
            "cf40560a-9c0d-5868-bb45-250c0bf77dfb","84b1573d-3863-5143-a259-a2df44f9f102","176b7699-9b1f-5b09-9e67-8c6970823253",
            "1a57b6ab-e0ae-5960-bcae-94c99a3e1cbb","2c2d6ed3-a946-53d5-bc14-b24bd1d7f4d6","e41a3ab3-0a8f-5790-94c8-dc7c19368eff",
            "2047a021-cdbd-5b8f-9f00-cc6cdd14004f","cbc89c81-de97-57eb-8b3f-97fbf29225d5","aeec0350-1919-56f6-9a21-872a8fb11f58"
        }.Select(Guid.Parse).ToArray();
        Assert.Equal(12,adds.Distinct().Count());
        Assert.All(adds,id=>Assert.Contains(id,sources.Keys));
        var reg=sources[Guid.Parse("cbc89c81-de97-57eb-8b3f-97fbf29225d5")];
        Assert.Contains("KAPITTEL_7-3",reg.Url.AbsoluteUri,StringComparison.Ordinal);
        Assert.Contains("7-5a",reg.Title,StringComparison.Ordinal);
        Assert.All(norwaySources,source=>Assert.Equal(DateTimeOffset.Parse(ReviewDate,CultureInfo.InvariantCulture),source.LastCheckedAt));

        var facts=await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory,"Data","comparison-facts.json")).LoadAsync(catalog);
        JsonComparisonFactProvider.Validate(facts,catalog);
        var norwayFacts=facts.Topics.SelectMany(topic=>topic.Aspects).SelectMany(aspect=>aspect.Facts).Where(fact=>fact.CountrySlug=="norway").ToArray();
        Assert.Equal(24,norwayFacts.Length);
        Assert.Equal(43,norwayFacts.SelectMany(fact=>fact.OfficialSourceIds).Distinct().Count());
        foreach(var fact in norwayFacts)
            Assert.All(fact.OfficialSourceIds,id=>Assert.Equal(fact.InformationEntryId,sources[id].InformationEntryId));
        Assert.Contains("KEEP 32 / UPDATE 2",ReadReviewArtifact("NORWAY_REFRESH_REVIEW_01.md"),StringComparison.Ordinal);
        var sourcePlan=ReadReviewArtifact("SOURCE_AUDIT_INPUT_LIST_01.md");
        Assert.Contains("CURRENT 34 / LEGACY 0 / BROKEN 0 / UNCLEAR 0",sourcePlan,StringComparison.Ordinal);
        Assert.Contains("KEEP 32 / UPDATE 2 / RETAIN HISTORICAL 0 / REMOVE 0",sourcePlan,StringComparison.Ordinal);
        var compareDraft=ReadReviewArtifact("COMPARE_DRAFT_01.md");
        Assert.Equal(18,Regex.Matches(compareDraft,"\\*\\*KEEP\\*\\*").Count);
        Assert.Equal(6,Regex.Matches(compareDraft,"\\*\\*REVISE\\*\\*").Count);
        foreach(var aspect in new[]{"protection-application","protection-dates","residence-routes","work-permission","work-minimum-pay","education-school-enrolment"})
            Assert.Contains($"`{aspect}`",compareDraft,StringComparison.Ordinal);
        var watch=ReadReviewArtifact("DATE_EVENT_WATCH_CANDIDATES_01.md");
        var decisions=Regex.Matches(watch,"(?m)^\\| \\d+ \\| (APPROVE|EDIT) \\|").Select(match=>match.Groups[1].Value).ToArray();
        Assert.Equal(8,decisions.Count(decision=>decision=="APPROVE"));
        Assert.Equal(3,decisions.Count(decision=>decision=="EDIT"));
    }

    private static async Task<CatalogData> LoadCatalog() =>
        await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();

    private static string Content(CatalogData catalog,string articleId,string language="en")
    {
        var entryId=Articles[articleId];
        return catalog.InformationEntryTranslations.Single(translation=>translation.InformationEntryId==entryId && translation.LanguageCode==language).Content;
    }

    private static string ReadReviewArtifact(string name)=>
        File.ReadAllText(Path.Combine(FindRepositoryRoot(),"docs","content-refresh","2026-11","norway",name));

    private static string FindRepositoryRoot()
    {
        var directory=new DirectoryInfo(AppContext.BaseDirectory);
        while(directory is not null && !File.Exists(Path.Combine(directory.FullName,"RefugeeNavigatorEurope.slnx"))) directory=directory.Parent;
        return directory?.FullName ?? throw new DirectoryNotFoundException("Could not find the repository root.");
    }

    private static List<Dictionary<string,string>> ReadCsv(string path)
    {
        var lines=File.ReadAllLines(path,Encoding.UTF8);
        var headers=ParseCsvLine(lines[0]);
        return lines.Skip(1).Where(line=>!string.IsNullOrWhiteSpace(line)).Select(line=>
        {
            var values=ParseCsvLine(line);
            Assert.Equal(headers.Count,values.Count);
            return headers.Select((header,index)=>(header,index)).ToDictionary(pair=>pair.header,pair=>values[pair.index],StringComparer.Ordinal);
        }).ToList();
    }

    private static List<string> ParseCsvLine(string line)
    {
        var values=new List<string>(); var current=new StringBuilder(); var quoted=false;
        for(var i=0;i<line.Length;i++)
        {
            var ch=line[i];
            if(ch=='"')
            {
                if(quoted && i+1<line.Length && line[i+1]=='"'){current.Append('"');i++;}
                else quoted=!quoted;
            }
            else if(ch==',' && !quoted){values.Add(current.ToString());current.Clear();}
            else current.Append(ch);
        }
        values.Add(current.ToString()); return values;
    }
}
