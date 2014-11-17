using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace XPressMe.Data.XPressMeDemoDB
{

    public partial class XPOPostAttachment
    {
        public XPOPostAttachment(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        public new class Fields : XPOBaseItem.Fields
        {
            protected Fields() : base() { }

            public static OperandProperty FileName { get { return new OperandProperty("FileName"); } }
            public static OperandProperty MimeType { get { return new OperandProperty("MimeType"); } }
            public static OperandProperty Data { get { return new OperandProperty("Data"); } }
            public static OperandProperty Post { get { return new OperandProperty("Post"); } }



        }
    }

}
