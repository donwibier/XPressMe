using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace XPressMe.Data.XPressMeDemoDB
{

    public partial class XPOPost
    {
        public XPOPost(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        public new class Fields : XPOBaseItem.Fields
        {
            protected Fields() : base() { }

            public static OperandProperty Title { get { return new OperandProperty("Title"); } }
            public static OperandProperty Active { get { return new OperandProperty("Active"); } }
            public static OperandProperty Article { get { return new OperandProperty("Article"); } }
            public static OperandProperty Group { get { return new OperandProperty("Group"); } }
            public static OperandProperty GroupTitle { get { return new OperandProperty("GroupTitle"); } }
            public static OperandProperty Tags { get { return new OperandProperty("Tags"); } }
            public static OperandProperty PostAttachments { get { return new OperandProperty("PostAttachments"); } }


        }
    }

}
