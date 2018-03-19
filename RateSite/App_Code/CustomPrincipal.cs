using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;


public class CustomPrincipal : IPrincipal
{
    private IIdentity _identity;
    private string[] _roles;

    public CustomPrincipal(IIdentity identity, string[] roles)
    {
        _identity = identity;
        _roles = new string[roles.Length];
        roles.CopyTo(_roles, 0);
        Array.Sort(_roles);
    }

    public bool IsInRole(string role)
    {
        return Array.BinarySearch(_roles, role) >= 0 ? true : false;
    }
    public IIdentity Identity
    {
        get
        {
            return _identity;
        }
    }

    public bool IsInAllRoles(params string[] roles)
    {
        foreach (string searchrole in roles)
        {
            if (Array.BinarySearch(_roles, searchrole) < 0)
                return false;
        }
        return true;
    }

    public bool IsInAnyRoles(params string[] roles)
    {
        foreach (string searchrole in roles)
        {
            if (Array.BinarySearch(_roles, searchrole) > 0)
                return true;
        }
        return false;
    }
}
