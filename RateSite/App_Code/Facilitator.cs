using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Facilitator
/// </summary>
public class Facilitator
{
    private int FacilitatorIDValue;
    private string NameValue;
    private string TitleValue;
    private string OrganizationValue;
    private string CityValue;
    private string CountryValue;

    public int FacilitatorID
    {
        get { return FacilitatorIDValue; }
        set { FacilitatorIDValue = value; }
    }
    public string Name
    {
        get { return NameValue; }
        set { NameValue = value; }
    }
    public string Title
    {
        get { return TitleValue; }
        set { TitleValue = value; }
    }
    public string Organization
    {
        get { return OrganizationValue; }
        set { OrganizationValue = value; }
    }
    public string City
    {
        get { return CityValue; }
        set { CityValue = value; }
    }
    public string Country
    {
        get { return CountryValue; }
        set { CountryValue = value; }
    }
}