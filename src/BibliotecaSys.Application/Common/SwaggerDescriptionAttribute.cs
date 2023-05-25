namespace BibliotecaSys.Application.Common;

[AttributeUsage(AttributeTargets.Method)]
public class SwaggerDescriptionAttribute : Attribute
{
    public string Description { get; set; }

    public SwaggerDescriptionAttribute(string description)
    {
        Description = description;
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class SwaggerTagsAttribute : Attribute
{
    public string[] Tags { get; set; }

    public SwaggerTagsAttribute(params string[] tags)
    {
        Tags = tags;
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class SwaggerSummaryAttribute : Attribute
{
    public string Summary { get; set; }

    public SwaggerSummaryAttribute(string summary)
    {
        Summary = summary;
    }
}