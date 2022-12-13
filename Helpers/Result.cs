using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PjlpCore.Helpers;

public static class Result {
    [Produces("application/json")]
    public static Dictionary<string, bool> Success() {
        var result = new Dictionary<string, bool>();
        result.Add("success", true);
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, bool> NotFound()
    {
        var result = new Dictionary<string, bool>
        {
            { "notfound", true }
        };
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, bool> TimeUp()
    {
        var result = new Dictionary<string, bool>
        {
            { "timeup", true }
        };
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, string> SuccessUpload(bool isNew, bool isNewFoto, string oldID, string newID, string typeName, string createdAt, string? newpath)
    {
        var result = new Dictionary<string, string>();
        result.Add("success", "yes");
        result.Add("isnew", isNew ? "yes" : "no");
        result.Add("isnewfoto", isNewFoto ? "yes" : "no");
        result.Add("type", typeName);
        result.Add("created_at", createdAt);
        result.Add("path", newpath!);
        result.Add("oldid", oldID);
        result.Add("newid", newID);
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, bool> Failed() {
        var result = new Dictionary<string, bool>();
        result.Add("failed", true);
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, string> ChangeStatus(string StatusName)
    {
        var result = new Dictionary<string, string>();
        result.Add("success", "yes");
        result.Add("status", StatusName);
        return result;
    }
}