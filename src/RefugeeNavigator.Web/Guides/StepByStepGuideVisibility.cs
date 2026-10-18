namespace RefugeeNavigator.Web.Guides;

public static class StepByStepGuideVisibility
{
    public static bool CanRender(GuideEditorialStatus status, bool isDevelopment) =>
        status != GuideEditorialStatus.Draft || isDevelopment;
}
