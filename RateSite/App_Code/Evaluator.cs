using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Evaluator
/// </summary>
public class Evaluator
{
    private int EvaluatorIDValue;
    private string NameValue;
    private DateTime DateOfBirthValue;
    private string SexValue;
    private string SchoolValue;
    private string CityValue;
    private string CountryValue;
    private string CriteriaValue;

    public int EvaluatorID
    {
        get { return EvaluatorIDValue; }
        set { EvaluatorIDValue = value; }
    }
    public string Name
    {
        get { return NameValue; }
        set { NameValue = value; }
    }
    public DateTime DateOfBirth
    {
        get { return DateOfBirthValue; }
        set { DateOfBirthValue = value; }
    }
    public string Sex
    {
        get { return SexValue; }
        set { SexValue = value; }
    }
    public string School
    {
        get { return SchoolValue; }
        set { SchoolValue = value; }
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
    public string Criteria
    {
        get { return CriteriaValue; }
        set { CriteriaValue = value; }
    }
}