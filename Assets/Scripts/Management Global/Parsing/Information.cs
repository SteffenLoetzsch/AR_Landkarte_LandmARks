using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information
{
    public string InformationTitle { get; set; } = string.Empty;
    public string AdditionalInfo { get; set; } = string.Empty;
    public string InformationText { get; set; } = string.Empty;
    public string VideoFileLink { get; set; } = string.Empty;
    public string WebsiteURL { get; set; } = string.Empty;

    public Information() { }

    public Information(string title, string addInfo, string text, string video, string url)
    {
        InformationTitle = title;
        AdditionalInfo = addInfo;
        InformationText = text;
        VideoFileLink = video;
        WebsiteURL = url;
    }
}
