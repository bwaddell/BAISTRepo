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
    private string FirstNameValue;
    private string LastNameValue;
    private string TitleValue;
    private string OrganizationValue;
    private string LocationValue;
    private string EmailValue;
    private string PasswordValue;
    private string SaltValue;
    private string RolesValue;

    public int FacilitatorID
    {
        get { return FacilitatorIDValue; }
        set { FacilitatorIDValue = value; }
    }
    public string FirstName
    {
        get { return FirstNameValue; }
        set { FirstNameValue = value; }
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
    public string Location
    {
        get { return LocationValue; }
        set { LocationValue = value; }
    }

    public string Password
    {
        get
        {
            return PasswordValue;
        }

        set
        {
            PasswordValue = value;
        }
    }

    public string Salt
    {
        get
        {
            return SaltValue;
        }

        set
        {
            SaltValue = value;
        }
    }

    public string Roles
    {
        get
        {
            return RolesValue;
        }

        set
        {
            RolesValue = value;
        }
    }

    public string Email
    {
        get
        {
            return EmailValue;
        }

        set
        {
            EmailValue = value;
        }
    }

    public string LastName
    {
        get
        {
            return LastNameValue;
        }

        set
        {
            LastNameValue = value;
        }
    }
}