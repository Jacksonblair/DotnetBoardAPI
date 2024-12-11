namespace WebApplication1.Models;

/** Value that is part of a set of values **/
public class CategoryValue : BaseDataModel
{
    public string Value;
    public string CategoryID;
}

/** Set of values **/
public class Category : BaseDataModel
{
    public string Name;
}
