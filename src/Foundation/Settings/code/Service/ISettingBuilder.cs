using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VRBYOD.Foundation.Settings.Model;
using Sitecore.Mvc.Presentation;

namespace VRBYOD.Foundation.Settings.Service
{
    public interface ISettingBuilder
    {
        BYODSettings GetSettingItem(Sitecore.Data.Items.Item contextItem,Rendering rendering);
    }
}