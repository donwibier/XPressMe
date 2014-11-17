using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XPressMe.Web
{
    public interface IHomepage
    {
    }

    public interface IPostPage
    {

    }
    public interface IEditorInterface
    {
        void LoadFromObject(object id);
        void SaveToDataObject(object id);
    }
}