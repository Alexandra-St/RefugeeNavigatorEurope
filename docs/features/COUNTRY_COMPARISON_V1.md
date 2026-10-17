# Country Comparison V1

The comparison page aligns existing country articles under eight fixed topics. It does not generate, summarize, rank, or infer legal differences.

## Topic mapping

`CountryComparisonTopicMap` maps each topic and country slug to one existing `InformationEntry.Id`. The eight topics are: temporary-protection application; residence after temporary protection; finding employment; work authorization and employee rights; healthcare; housing; education and language learning; and social assistance. Temporary protection and employment each show two topics; the other categories show one.

The map stores identifiers only, not copied article text. Application startup validates that every mapped article exists and belongs to the expected country and category. To map a future article, add its stable ID to the relevant topic's country mapping, provide the localized topic heading if a new topic is needed, and keep the startup relationship validation passing.

## Rendering and attribution

The comparison reads each selected language's title, summary, content, verification status, update date, and official sources from the existing catalogs. Article bodies use `ArticleContentFormatter`; source links retain their recorded check dates and open safely in a new tab. Article links point to stable entry anchors on the original category page.

Only `Verified` translations are shown as article content. `NeedsReview` and `Outdated` translations show their status and an unavailable message; their title, summary, and body are withheld. Missing mapped content uses the same unavailable state. Source references remain accessible where a source relationship exists.

## Localization and limitations

The interface is available in Ukrainian, English, and Russian. Topic labels and interface text are localized; the compared legal article content remains the catalog's existing translation.

The page presents reviewed information side by side but does not decide which country is preferable, determine personal eligibility, or calculate legal outcomes. Rules and individual circumstances can differ, and displayed article and source dates are historical. A future article must be explicitly mapped; unconfigured articles do not appear automatically.
