using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace XPressMe.Data.XPressMeDemoDB
{

    public partial class XPOGroup
    {
        public XPOGroup(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        public new class Fields : XPOBaseItem.Fields
        {
            protected Fields() : base() { }

            public static OperandProperty Title { get { return new OperandProperty("Title"); } }
            public static OperandProperty Posts { get { return new OperandProperty("Posts"); } }

        }
    }

}
