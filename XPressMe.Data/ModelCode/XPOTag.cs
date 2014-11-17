using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace XPressMe.Data.XPressMeDemoDB
{

    public partial class XPOTag
    {
        public XPOTag(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        public new class Fields : XPOBaseItem.Fields
        {
            protected Fields() : base() { }

            public static OperandProperty Name { get { return new OperandProperty("Name"); } }
            public static OperandProperty Posts { get { return new OperandProperty("Posts"); } }

        }
    }

}
